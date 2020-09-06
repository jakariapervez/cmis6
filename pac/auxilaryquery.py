import datetime
from .models import FinancialYear
def financialYearFromDate(mydate):
    year = mydate.year
    month = mydate.month
    if month > 6:
        stdate = datetime.datetime(year, 7, 1)
        stdate = datetime.datetime.date(stdate)
    else:
        stdate = datetime.datetime(year - 1, 7, 1)
        stdate = datetime.datetime.date(stdate)
    years=list(FinancialYear.objects.all().filter( startDate=stdate))
    myyear=years[0]
    return myyear
def monthFromDate(mydate):
    month=mydate.month
    return month

def getInvoiceTotal(fy,invoice):
    total=0.0
    expenditures = Expenditure_details.objects.filter(Invoice_details=invoice).filter(Budget_allocation__Financial_year=fy)
    for exp in expenditures:
        total+=float(exp.Total)
    return total
def getInvoiceTotal2(invoice):
    fy=financialYearFromDate(invoice.date)
    total=0.0
    #expenditures = Expenditure_details.objects.filter(Invoice_details=invoice).filter(Budget_allocation__Financial_year=fy)
    expenditures = Expenditure_details.objects.filter(Invoice_details=invoice)
    for exp in expenditures:
        print("total={}".format(exp.Total))
        total +=float(exp.Total)
    return total
from .models import Expenditure_details,Budget_allocation,Dpp_allocation,Invoice_details
def getCumulative(myyear,economicCode,budget_allocation):
    dpa=0
    gob=0
    rpa=0
    total=0
    #expenditures=Expenditure_details.objects.filter( Budget_allocation__Financial_year=myyear).filter(Budget_allocation_Dpp_allocation_ Ecode=economicCode)
    #expenditures = Expenditure_details.objects.filter(Budget_allocation__Dpp_allocation__Ecode=economicCode).filter(Budget_allocation__Financial_year=myyear)
    expenditures = Expenditure_details.objects.filter(Budget_allocation=budget_allocation)
    print(expenditures)
    for expenditure in expenditures:
        dpa+=expenditure.Dpa
        rpa+=expenditure.Rpa
        gob+=expenditure.Gob
        total+=expenditure.Total
    return {'Gob':gob,'Dpa':dpa,'Rpa':rpa,'Total':total}
def checkCumulative(fy,economicCode,expenditure,budget_allocation):
    cumulative = getCumulative(fy,economicCode,budget_allocation)
    print(cumulative)
    print("budget allocation gob={} dpa={} rpa={} total={}".format(budget_allocation.Gob,budget_allocation.Dpa,
                                                                   budget_allocation.Rpa,budget_allocation.Total))

    error_message=[]
    if budget_allocation.Gob>=expenditure.Gob+cumulative['Gob']:
        isValid=True
    else:
        isValid=False
        error_message.append("Gob exceeds budget allocation")
        print("fails at gob")
    if budget_allocation.Dpa  >= expenditure.Dpa+cumulative['Dpa']:
        isValid = True
    else:
        isValid = False
        error_message.append("Dpa exceeds budget allocation")
        print("fails at dpa")
    if budget_allocation.Rpa >= expenditure.Rpa+cumulative['Rpa']:
        isValid = True
    else:
        isValid = False
        error_message.append("Rpa exceeds budget allocation")
        print("fails at rpa")
    if budget_allocation.Total  >= expenditure.Total+cumulative['Total']:
        isValid = True
    else:
        isValid = False
        error_message.append("Total exceeds budget allocation")
        print("fails at Total")
    print(isValid)
    if isValid:
        cumTotal=cumulative['Total']+expenditure.Total
    else:
        cumTotal = cumulative['Total']
    return {'isValid':isValid,'Total':cumTotal,
            "meassage":error_message,"cum_gob": cumulative['Gob']+expenditure.Gob,
            "cum_dpa":cumulative['Dpa']+expenditure.Dpa,"cum_rpa":cumulative['Rpa']+expenditure.Rpa}
def displayInvoice(invoice):
    print("Invoice_no={} date={} BatchType={} Description={}".format(invoice.Invoice_no,invoice.date,invoice.BatchType,invoice.Description))
    print("Financial year={}".format(invoice.FinancialYear))                                                                






def validateExpenditure(exp_form,invoice):
    #print(exp_form)
    expenditure=Expenditure_details()
    gob = exp_form.cleaned_data['Gob']
    rpa = exp_form.cleaned_data['Rpa']
    dpa = exp_form.cleaned_data['Dpa']
    total = gob + dpa + rpa
    print("GoB={} DPA={} RPA={} Total={}".format(gob, dpa, rpa, total))

    # form.fields['Total']=total
    # form.cleaned_data['Total']=total
    expenditure.Invoice_details = invoice
    expenditure.Gob = gob
    expenditure.Dpa = dpa
    expenditure.Rpa = rpa
    expenditure.Total = total
    dpp_allocation=exp_form.cleaned_data['dpp_allocation']
    budget_allocation=Budget_allocation.objects.get(Dpp_allocation=dpp_allocation,Financial_year=expenditure.Invoice_details.FinancialYear)
    #budget_allocation=exp_form.cleaned_data['Budget_allocation']
    economicCode=budget_allocation.Dpp_allocation.Description
    print("Economic code={} description={}".format(economicCode,budget_allocation.Dpp_allocation.Description))
    expenditure.Budget_allocation =budget_allocation
    expenditure.date = invoice.date
    if budget_allocation.Total>=expenditure.Total:
        isValid=True
    else:
        isValid=False
    if budget_allocation.Gob>=expenditure.Gob:
        isValid=True
    else:
        isValid=False
    if budget_allocation.Rpa>=expenditure.Rpa:
        isValid=True
    else:
        isValid=False
    if budget_allocation.Dpa>=expenditure.Dpa:
        isValid=True
    else:
        isValid=False
    print("Individula test for validation......")
    myyear = financialYearFromDate(invoice.date)

    validity=checkCumulative(myyear,economicCode,expenditure,budget_allocation)
    invoice_total = getInvoiceTotal(myyear,invoice)
    invoice_total=invoice_total+float(expenditure.Total)
    print("Expenditure={} Cumtotal={}".format(validity['isValid'],validity['Total']))
    #myyear = financialYearFromDate(expenditure.date)
    fy = financialYearFromDate(expenditure.date)
    month = monthFromDate(expenditure.date)
    #isValid=checkCumulative(myyear,expenditure)
    allocation=[]
    allocation.append(budget_allocation.Gob)
    allocation.append(budget_allocation.Dpa)
    allocation.append(budget_allocation.Rpa)
    cum_exp=[]
    cum_exp.append(validity['cum_gob'])
    cum_exp.append(validity['cum_dpa'])
    cum_exp.append(validity['cum_rpa'])
    economicCode = str(budget_allocation.Dpp_allocation.Ecode)+":"+str(budget_allocation.Dpp_allocation.Shortdescription)

    return {'expenditure': expenditure, 'validity': validity['isValid'],
            'cumtotal':invoice_total,'fyear':fy,'month':month,"message":validity['meassage'],
            "cum_gob":validity['cum_gob'],"cum_dpa":validity['cum_dpa'],"cum_rpa":validity['cum_rpa'],
            "allocation":allocation,'cum_exp':cum_exp,"Ecode":economicCode}

    #return {'expenditure':expenditure,'validity':validity['isValid'],'cumtotal':validity['Total']}

def validateExpenditureEditForm(exp_form,expenditure):
    gob = exp_form.cleaned_data['Gob']
    rpa = exp_form.cleaned_data['Rpa']
    dpa = exp_form.cleaned_data['Dpa']
    total = gob + dpa + rpa
    print("GoB={} DPA={} RPA={} Total={}".format(gob, dpa, rpa, total))
    expenditure.Invoice_details = invoice
    budget_allocation = expenditure.Budget_allocation
    invoice = expenditure.Invoice_details
    if budget_allocation.Total>=total:
        isValid=True
    else:
        isValid=False
    if budget_allocation.Gob>=expenditure.Gob:
        isValid=True
    else:
        isValid=False
    if budget_allocation.Rpa>=expenditure.Rpa:
        isValid=True
    else:
        isValid=False
    if budget_allocation.Dpa>=expenditure.Dpa:
        isValid=True
    else:
        isValid=False
    myyear = financialYearFromDate(invoice.date)
    fy=financialYearFromDate(invoice.date)
    month=monthFromDate(invoice.date)

    economicCode=expenditure.Budget_allocation.Dpp_allocation.Ecode
    cumulative = getCumulative(fy, economicCode)
    cumulative['Gob']=cumulative['Gob']-expenditure.Gob+gob
    cumulative['Dpa'] = cumulative['Dpa'] - expenditure.Dpa + dpa
    cumulative['Rpa'] = cumulative['Gob'] - expenditure.Rpa + rpa
    cumulative['Total']=cumulative['Total']-expenditure.Total+total
    if  cumulative['Gob']<= budget_allocation.Gob:
        isValid=True
    else:
        isValid=False
    if  cumulative['Dpa']<=budget_allocation.Dpa:
        isValid=True
    else:
        isValid=False
    if  cumulative['Rpa']<=budget_allocation.Rpa:
        isValid=True
    else:
        isValid=False
    if  cumulative['Total']<=budget_allocation.Total:
        isValid=True
    else:
        isValid=False
    expenditure.Gob = gob
    expenditure.Dpa = dpa
    expenditure.Rpa = rpa
    expenditure.Total = total
    return{"expenditure":expenditure,'isValid':isValid}
    #invoice_total=getInvoiceTotal(fy,invoice)
"""
########################################################################################################################
#######################################################################################################################
Progress Report
#######################################################################################################################
#######################################################################################################################

"""
from reportlab.lib.enums import TA_JUSTIFY
from reportlab.lib.units import cm
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Image, Table, TableStyle
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch
from django.core.files.storage import FileSystemStorage
from reportlab.lib.styles import getSampleStyleSheet
from io import BytesIO
from reportlab.lib.pagesizes import A4, landscape
from reportlab.lib import colors


"""creating Cass for monthly Report"""
class MonthlyReportItem:
    def __init__(self,budget):
        self.Ecode=budget.Dpp_allocation.Ecode
        self.description=budget.Dpp_allocation.Shortdescription
        self.gob_allocation=float(budget.Gob)
        self.rpa_allocation=float(budget.Rpa)
        self.dpa_allocation=float(budget.Dpa)
        self.total_allocation=float(budget.Total)
        """Expenditure upto previous month"""
        self.gob_pm =0.0
        self.rpa_pm =0.0
        self.dpa_pm =0.0
        self.total_pm =0.0
        """Expenditure of the current month month"""
        self.gob_cm =0.0
        self.rpa_cm = 0.0
        self.dpa_cm = 0.0
        self.total_cm = 0.0
        """Expenditue upto current month"""
        self.gob_cmt = 0.0
        self.rpa_cmt = 0.0
        self.dpa_cmt = 0.0
        self.total_cmt = 0.0
        """budget remaining"""
        self.gob_rm = 0.0
        self.rpa_rm = 0.0
        self.dpa_rm = 0.0
        self.total_rm = 0.0
    def setCurrentMonthExpenditure(self,gob,rpa,dpa,total):
        self.gob_cm = gob
        self.rpa_cm = rpa
        self.dpa_cm = dpa
        self.total_cm = total
    def setPreviousMonthExpenditure(self,gob,rpa,dpa,total):
        self.gob_pm = gob
        self.rpa_pm = rpa
        self.dpa_pm = dpa
        self.total_pm = total
    def calCulateTotalUptoMonth(self):
        self.gob_cmt =  self.gob_pm+self.gob_cm
        self.rpa_cmt = self.rpa_pm+self.rpa_cm
        self.dpa_cmt = self.dpa_pm+self.dpa_cm
        self.total_cmt =self.total_pm+self.total_cm
    def  calCulateRemainingBudget(self):
        self.gob_rm =self.gob_allocation-self.gob_cmt
        self.rpa_rm = self.rpa_allocation-self.rpa_cmt
        self.dpa_rm = self.dpa_allocation-self.dpa_cmt
        self.total_rm =self.total_allocation-self.total_cmt

    def round2Lakh(self):
        self.gob_allocation =round(( self.gob_allocation/100000.00),2)
        self.rpa_allocation = round(( self.rpa_allocation/100000.00),2)
        self.dpa_allocation = round(( self.dpa_allocation/100000.00),2)
        self.total_allocation = round(( self.total_allocation/100000.00),2)
        """Expenditure upto previous month"""
        self.gob_pm = round( self.gob_pm/100000.00,2)
        #self.rpa_pm=self.rpa_pm/100000.0
        self.rpa_pm = round( self.rpa_pm/100000.00,2)
        self.dpa_pm =round( self.dpa_pm/100000.00,2)
        self.total_pm =round( self.total_pm/100000.00,2)
        """Expenditure of the current month month"""
        self.gob_cm = round( self.gob_cm/100000.00,2)
        self.rpa_cm = round( self.rpa_cm/100000.00,2)
        self.dpa_cm = round( self.dpa_cm/100000.00,2)
        self.total_cm =round( self.total_cm/100000.00,2)
        """Expenditue upto current month"""
        self.gob_cmt = round( self.gob_cmt/100000.00,2)
        self.rpa_cmt =round( self.rpa_cmt/100000.00,2)
        self.dpa_cmt = round( self.dpa_cmt/100000.00,2)
        self.total_cmt = round( self.total_cmt/100000.00,2)
        """budget remaining"""
        self.gob_rm = round( self.gob_rm/100000.00,2)
        self.rpa_rm = round( self.rpa_rm/100000.00,2)
        self.dpa_rm = round( self.dpa_rm/100000.00,2)
        self.total_rm = round( self.total_rm/100000.00,2)

    def roundToDigit(self,digit):
        self.gob_allocation =round(( self.gob_allocation/1.0),digit)
        self.rpa_allocation = round(( self.rpa_allocation/1.0),digit)
        self.dpa_allocation = round(( self.dpa_allocation/1.0),digit)
        self.total_allocation = round(( self.total_allocation/1.0),digit)
        """Expenditure upto previous month"""
        self.gob_pm = round( self.gob_pm/1.0,digit)
        #self.rpa_pm=self.rpa_pm/100000.0
        self.rpa_pm = round( self.rpa_pm/1.0,digit)
        self.dpa_pm =round( self.dpa_pm/1.0,digit)
        self.total_pm =round( self.total_pm/1.0,digit)
        """Expenditure of the current month month"""
        self.gob_cm = round( self.gob_cm/1.0,digit)
        self.rpa_cm = round( self.rpa_cm/1.0,digit)
        self.dpa_cm = round( self.dpa_cm/1.0,digit)
        self.total_cm =round( self.total_cm/1.0,digit)
        """Expenditue upto current month"""
        self.gob_cmt = round( self.gob_cmt/1.0,digit)
        self.rpa_cmt =round( self.rpa_cmt/1.0,digit)
        self.dpa_cmt = round( self.dpa_cmt/1.0,digit)
        self.total_cmt = round( self.total_cmt/1.0,digit)
        """budget remaining"""
        self.gob_rm = round( self.gob_rm/1.0,digit)
        self.rpa_rm = round( self.rpa_rm/1.0,digit)
        self.dpa_rm = round( self.dpa_rm/1.0,digit)
        self.total_rm = round( self.total_rm/1.0,digit)


    def initialize2zero(self):
            self.gob_allocation = 0.0
            self.rpa_allocation = 0.0
            self.dpa_allocation = 0.0
            self.total_allocation = 0.0
            """Expenditure upto previous month"""
            self.gob_pm = 0.0
            self.rpa_pm = 0.0
            self.dpa_pm = 0.0
            self.total_pm = 0.0
            """Expenditure of the current month month"""
            self.gob_cm = 0.0
            self.rpa_cm = 0.0
            self.dpa_cm = 0.0
            self.total_cm = 0.0
            """Expenditue upto current month"""
            self.gob_cmt = 0.0
            self.rpa_cmt = 0.0
            self.dpa_cmt = 0.0
            self.total_cmt = 0.0
            """budget remaining"""
            self.gob_rm = 0.0
            self.rpa_rm = 0.0
            self.dpa_rm = 0.0
            self.total_rm =0.0



import pandas as pd
from django_pandas.io import read_frame

from .models import Expenditure_details



def calculateCodeWiseMonthlyExpenditure(fy):
    monthly_gob=createMonthlyExpenditureDF()
    monthly_rpa = createMonthlyExpenditureDF()
    monthly_dpa = createMonthlyExpenditureDF()
    monthly_total = createMonthlyExpenditureDF()
    months=[7,8,9,10,11,12,1,2,3,4,5,6]
    code_list = list(monthly_gob.iloc[:, 1])
    for month in months:
        invoices=Invoice_details.objects.all().filter(FinancialYear=fy,Month=month)
        month_index=months.index(month)+2
        for invoice in invoices:
            #expenditures=list(Expenditure_details.ojects.all().filter(Invoice_details=invoice))
            expenditures=Expenditure_details.objects.all().filter(Invoice_details=invoice)
            for exp in expenditures:
                code=exp.Budget_allocation.Dpp_allocation.Shortdescription
                index = code_list.index(code)
                gob=float(exp.Gob)
                rpa=float(exp.Rpa)
                dpa=float(exp.Dpa)
                total=float(exp.Total)
                monthly_gob.iloc[index,month_index]=monthly_gob.iloc[index,month_index]+gob
                monthly_rpa.iloc[index, month_index] = monthly_rpa.iloc[index, month_index] + rpa
                monthly_dpa.iloc[index, month_index] = monthly_dpa.iloc[index, month_index] + dpa
                monthly_total.iloc[index, month_index] = monthly_total.iloc[index, month_index] + total
                print("item={} gob={} rpa={} dpa={} total={}".format(code,exp.Gob,exp.Rpa,exp.Dpa,exp.Total))
    myframes={"gob":monthly_gob,'rpa':monthly_rpa,'dpa': monthly_dpa,'total':monthly_total}
    return myframes

            #print("invoice no={} total exp entries={}".format(invoice.Invoice_no,len(expenditures)))
        #invoices = list(Invoice_details.objects.all().filter(FinancialYear=fy,Month=month))
        #print("month={} total invoices={}".format(month,len(invoices)))

def createMonthlyExpenditureDF():
    """
    print("ecode={} description={} total={}".format(budget.Dpp_allocation.Ecode,
                            budget.Dpp_allocation.Shortdescription,budget.Total))
     """
    item_codes=Dpp_allocation.objects.all().order_by('pk').values("Ecode","Shortdescription")
    myframe = read_frame(item_codes)
    months=['7','8','9','10','11','12','1','2','3','4','5','6']
    myframe['7']=0
    myframe['8'] = 0
    myframe['9'] = 0
    myframe['10'] = 0
    myframe['11'] = 0
    myframe['12'] = 0
    myframe['1'] = 0
    myframe['2'] = 0
    myframe['3'] = 0
    myframe['4'] = 0
    myframe['5'] = 0
    myframe['6'] = 0
    #invoices=Invoice_details.objects.all().filter(Month=month)
    #print(myframe)
    return myframe

def displayMonthlyExpenditure(myframe):
    for index,row in myframe.iterrows():
        print(row)


def caclculateExpenditureUptoPM(myframe_gob,myframe_rpa,myframe_dpa,myframe_total,dataIndex,monthindex):
    pm_gob = 0.0
    pm_rpa = 0.0
    pm_dpa = 0.0
    pm_total = 0.0
    months = [7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5, 6]
    #print("prevoius month={}".format(monthindex-1))
    for i in  range(2,monthindex):
        pm_gob=pm_gob+myframe_gob.iloc[dataIndex,i]
        pm_rpa=pm_rpa+myframe_rpa.iloc[dataIndex,i]
        pm_dpa=pm_dpa+myframe_dpa.iloc[dataIndex,i]
        pm_total=pm_total+myframe_total.iloc[dataIndex,i]
    pmexpenditure={"pm_gob":pm_gob,'pm_rpa':pm_rpa,'pm_dpa':pm_dpa,'pm_total':pm_total }
    return pmexpenditure


import copy
def subtotalAllownaces(ritems):
    sub_total_item=copy.deepcopy(ritems[0])
    sub_total_item.initialize2zero()
    sub_total_item.Ecode="Sub-Total"
    sub_total_item.description="Allownaces"
    for i in range(0,3):
        sub_total_item.gob_allocation = sub_total_item.gob_allocation+ritems[i].gob_allocation
        sub_total_item.rpa_allocation = sub_total_item.rpa_allocation+ritems[i].rpa_allocation
        sub_total_item.dpa_allocation = sub_total_item.dpa_allocation+ritems[i].dpa_allocation
        sub_total_item.total_allocation = sub_total_item.total_allocation+ritems[i].total_allocation
        """Expenditure upto previous month"""
        sub_total_item.gob_pm = sub_total_item.gob_pm+ritems[i].gob_pm
        sub_total_item.rpa_pm = sub_total_item.rpa_pm+ritems[i].rpa_pm
        sub_total_item.dpa_pm = sub_total_item.dpa_pm+ritems[i].dpa_pm
        sub_total_item.total_pm = sub_total_item.total_pm+ritems[i].total_pm
        """Expenditure of the current month month"""
        sub_total_item.gob_cm = sub_total_item.gob_cm+ritems[i].gob_cm
        sub_total_item.rpa_cm = sub_total_item.rpa_cm+ritems[i].rpa_cm
        sub_total_item.dpa_cm = sub_total_item.dpa_cm+ritems[i].dpa_cm
        sub_total_item.total_cm = sub_total_item.total_cm+ritems[i].total_cm
        """Expenditue upto current month"""
        sub_total_item.gob_cmt = sub_total_item.gob_cmt+ritems[i].gob_cmt
        sub_total_item.rpa_cmt = sub_total_item.rpa_cmt+ritems[i].rpa_cmt
        sub_total_item.dpa_cmt = sub_total_item.dpa_cmt+ritems[i].dpa_cmt
        sub_total_item.total_cmt = sub_total_item.total_cmt+ritems[i].total_cmt
        """budget remaining"""
        sub_total_item.gob_rm = sub_total_item.gob_rm+ritems[i].gob_rm
        sub_total_item.rpa_rm = sub_total_item.rpa_rm+ritems[i].rpa_rm
        sub_total_item.dpa_rm = sub_total_item.dpa_rm+ritems[i].dpa_rm
        sub_total_item.total_rm = sub_total_item.total_rm+ritems[i].total_rm
    print("Allownances Total={} ".format(sub_total_item.total_allocation))
    #print("item11={} item2={}".format( sub_total_item.Ecode,ritems[0].Ecode))
    sub_total_item.roundToDigit(2)
    return sub_total_item

def subtotalSupplyAndServices(ritems):
    sub_total_item=copy.deepcopy(ritems[0])
    sub_total_item.initialize2zero()
    sub_total_item.Ecode = "Sub-Total"
    sub_total_item.description ="Supply & Services"
    for i in range(3,31):
        sub_total_item.gob_allocation = sub_total_item.gob_allocation+ritems[i].gob_allocation
        sub_total_item.rpa_allocation = sub_total_item.rpa_allocation+ritems[i].rpa_allocation
        sub_total_item.dpa_allocation = sub_total_item.dpa_allocation+ritems[i].dpa_allocation
        sub_total_item.total_allocation = sub_total_item.total_allocation+ritems[i].total_allocation
        """Expenditure upto previous month"""
        sub_total_item.gob_pm = sub_total_item.gob_pm+ritems[i].gob_pm
        sub_total_item.rpa_pm = sub_total_item.rpa_pm+ritems[i].rpa_pm
        sub_total_item.dpa_pm = sub_total_item.dpa_pm+ritems[i].dpa_pm
        sub_total_item.total_pm = sub_total_item.total_pm+ritems[i].total_pm
        """Expenditure of the current month month"""
        sub_total_item.gob_cm = sub_total_item.gob_cm+ritems[i].gob_cm
        sub_total_item.rpa_cm = sub_total_item.rpa_cm+ritems[i].rpa_cm
        sub_total_item.dpa_cm = sub_total_item.dpa_cm+ritems[i].dpa_cm
        sub_total_item.total_cm = sub_total_item.total_cm+ritems[i].total_cm
        """Expenditue upto current month"""
        sub_total_item.gob_cmt = sub_total_item.gob_cmt+ritems[i].gob_cmt
        sub_total_item.rpa_cmt = sub_total_item.rpa_cmt+ritems[i].rpa_cmt
        sub_total_item.dpa_cmt = sub_total_item.dpa_cmt+ritems[i].dpa_cmt
        sub_total_item.total_cmt = sub_total_item.total_cmt+ritems[i].total_cmt
        """budget remaining"""
        sub_total_item.gob_rm = sub_total_item.gob_rm+ritems[i].gob_rm
        sub_total_item.rpa_rm = sub_total_item.rpa_rm+ritems[i].rpa_rm
        sub_total_item.dpa_rm = sub_total_item.dpa_rm+ritems[i].dpa_rm
        sub_total_item.total_rm = sub_total_item.total_rm+ritems[i].total_rm
    print("Allownances Total={} ".format(sub_total_item.total_allocation))
    #print("item11={} item2={}".format( sub_total_item.Ecode,ritems[0].Ecode))
    sub_total_item.roundToDigit(2)
    return sub_total_item


def subtotalRepairAndMaintenance(ritems):
    sub_total_item=copy.deepcopy(ritems[0])
    sub_total_item.initialize2zero()
    sub_total_item.Ecode = "Sub-Total"
    sub_total_item.description = "Repair & Maintenance"

    for i in range(31,41):
        sub_total_item.gob_allocation = sub_total_item.gob_allocation+ritems[i].gob_allocation
        sub_total_item.rpa_allocation = sub_total_item.rpa_allocation+ritems[i].rpa_allocation
        sub_total_item.dpa_allocation = sub_total_item.dpa_allocation+ritems[i].dpa_allocation
        sub_total_item.total_allocation = sub_total_item.total_allocation+ritems[i].total_allocation
        """Expenditure upto previous month"""
        sub_total_item.gob_pm = sub_total_item.gob_pm+ritems[i].gob_pm
        sub_total_item.rpa_pm = sub_total_item.rpa_pm+ritems[i].rpa_pm
        sub_total_item.dpa_pm = sub_total_item.dpa_pm+ritems[i].dpa_pm
        sub_total_item.total_pm = sub_total_item.total_pm+ritems[i].total_pm
        """Expenditure of the current month month"""
        sub_total_item.gob_cm = sub_total_item.gob_cm+ritems[i].gob_cm
        sub_total_item.rpa_cm = sub_total_item.rpa_cm+ritems[i].rpa_cm
        sub_total_item.dpa_cm = sub_total_item.dpa_cm+ritems[i].dpa_cm
        sub_total_item.total_cm = sub_total_item.total_cm+ritems[i].total_cm
        """Expenditue upto current month"""
        sub_total_item.gob_cmt = sub_total_item.gob_cmt+ritems[i].gob_cmt
        sub_total_item.rpa_cmt = sub_total_item.rpa_cmt+ritems[i].rpa_cmt
        sub_total_item.dpa_cmt = sub_total_item.dpa_cmt+ritems[i].dpa_cmt
        sub_total_item.total_cmt = sub_total_item.total_cmt+ritems[i].total_cmt
        """budget remaining"""
        sub_total_item.gob_rm = sub_total_item.gob_rm+ritems[i].gob_rm
        sub_total_item.rpa_rm = sub_total_item.rpa_rm+ritems[i].rpa_rm
        sub_total_item.dpa_rm = sub_total_item.dpa_rm+ritems[i].dpa_rm
        sub_total_item.total_rm = sub_total_item.total_rm+ritems[i].total_rm
    print("Allownances Total={} ".format(sub_total_item.total_allocation))
    #print("item11={} item2={}".format( sub_total_item.Ecode,ritems[0].Ecode))
    sub_total_item.roundToDigit(2)
    return sub_total_item



def subtotalCapital(ritems):
    sub_total_item=copy.deepcopy(ritems[0])
    sub_total_item.initialize2zero()
    sub_total_item.Ecode = "Sub-Total"
    sub_total_item.description = "Capital"
    for i in range(41,67):
        sub_total_item.gob_allocation = sub_total_item.gob_allocation+ritems[i].gob_allocation
        sub_total_item.rpa_allocation = sub_total_item.rpa_allocation+ritems[i].rpa_allocation
        sub_total_item.dpa_allocation = sub_total_item.dpa_allocation+ritems[i].dpa_allocation
        sub_total_item.total_allocation = sub_total_item.total_allocation+ritems[i].total_allocation
        """Expenditure upto previous month"""
        sub_total_item.gob_pm = sub_total_item.gob_pm+ritems[i].gob_pm
        sub_total_item.rpa_pm = sub_total_item.rpa_pm+ritems[i].rpa_pm
        sub_total_item.dpa_pm = sub_total_item.dpa_pm+ritems[i].dpa_pm
        sub_total_item.total_pm = sub_total_item.total_pm+ritems[i].total_pm
        """Expenditure of the current month month"""
        sub_total_item.gob_cm = sub_total_item.gob_cm+ritems[i].gob_cm
        sub_total_item.rpa_cm = sub_total_item.rpa_cm+ritems[i].rpa_cm
        sub_total_item.dpa_cm = sub_total_item.dpa_cm+ritems[i].dpa_cm
        sub_total_item.total_cm = sub_total_item.total_cm+ritems[i].total_cm
        """Expenditue upto current month"""
        sub_total_item.gob_cmt = sub_total_item.gob_cmt+ritems[i].gob_cmt
        sub_total_item.rpa_cmt = sub_total_item.rpa_cmt+ritems[i].rpa_cmt
        sub_total_item.dpa_cmt = sub_total_item.dpa_cmt+ritems[i].dpa_cmt
        sub_total_item.total_cmt = sub_total_item.total_cmt+ritems[i].total_cmt
        """budget remaining"""
        sub_total_item.gob_rm = sub_total_item.gob_rm+ritems[i].gob_rm
        sub_total_item.rpa_rm = sub_total_item.rpa_rm+ritems[i].rpa_rm
        sub_total_item.dpa_rm = sub_total_item.dpa_rm+ritems[i].dpa_rm
        sub_total_item.total_rm = sub_total_item.total_rm+ritems[i].total_rm
    print("Allownances Total={} ".format(sub_total_item.total_allocation))
    #print("item11={} item2={}".format( sub_total_item.Ecode,ritems[0].Ecode))
    sub_total_item.roundToDigit(2)
    return sub_total_item

def subtotalRevenue(ritems):
    sub_total_item=copy.deepcopy(ritems[0])
    sub_total_item.initialize2zero()
    sub_total_item.Ecode = "Sub-Total"
    sub_total_item.description = "Revenue"

    for i in range(0,40):
        sub_total_item.gob_allocation = sub_total_item.gob_allocation+ritems[i].gob_allocation
        sub_total_item.rpa_allocation = sub_total_item.rpa_allocation+ritems[i].rpa_allocation
        sub_total_item.dpa_allocation = sub_total_item.dpa_allocation+ritems[i].dpa_allocation
        sub_total_item.total_allocation = sub_total_item.total_allocation+ritems[i].total_allocation
        """Expenditure upto previous month"""
        sub_total_item.gob_pm = sub_total_item.gob_pm+ritems[i].gob_pm
        sub_total_item.rpa_pm = sub_total_item.rpa_pm+ritems[i].rpa_pm
        sub_total_item.dpa_pm = sub_total_item.dpa_pm+ritems[i].dpa_pm
        sub_total_item.total_pm = sub_total_item.total_pm+ritems[i].total_pm
        """Expenditure of the current month month"""
        sub_total_item.gob_cm = sub_total_item.gob_cm+ritems[i].gob_cm
        sub_total_item.rpa_cm = sub_total_item.rpa_cm+ritems[i].rpa_cm
        sub_total_item.dpa_cm = sub_total_item.dpa_cm+ritems[i].dpa_cm
        sub_total_item.total_cm = sub_total_item.total_cm+ritems[i].total_cm
        """Expenditue upto current month"""
        sub_total_item.gob_cmt = sub_total_item.gob_cmt+ritems[i].gob_cmt
        sub_total_item.rpa_cmt = sub_total_item.rpa_cmt+ritems[i].rpa_cmt
        sub_total_item.dpa_cmt = sub_total_item.dpa_cmt+ritems[i].dpa_cmt
        sub_total_item.total_cmt = sub_total_item.total_cmt+ritems[i].total_cmt
        """budget remaining"""
        sub_total_item.gob_rm = sub_total_item.gob_rm+ritems[i].gob_rm
        sub_total_item.rpa_rm = sub_total_item.rpa_rm+ritems[i].rpa_rm
        sub_total_item.dpa_rm = sub_total_item.dpa_rm+ritems[i].dpa_rm
        sub_total_item.total_rm = sub_total_item.total_rm+ritems[i].total_rm
    print("Allownances Total={} ".format(sub_total_item.total_allocation))
    #print("item11={} item2={}".format( sub_total_item.Ecode,ritems[0].Ecode))
    sub_total_item.roundToDigit(2)
    return sub_total_item

def grandTotal(ritems):
    sub_total_item=copy.deepcopy(ritems[0])
    sub_total_item.initialize2zero()
    sub_total_item.Ecode = "Grand Total"
    sub_total_item.description = "Grand Total"

    for i in range(0,69):
        sub_total_item.gob_allocation = sub_total_item.gob_allocation+ritems[i].gob_allocation
        sub_total_item.rpa_allocation = sub_total_item.rpa_allocation+ritems[i].rpa_allocation
        sub_total_item.dpa_allocation = sub_total_item.dpa_allocation+ritems[i].dpa_allocation
        sub_total_item.total_allocation = sub_total_item.total_allocation+ritems[i].total_allocation
        """Expenditure upto previous month"""
        sub_total_item.gob_pm = sub_total_item.gob_pm+ritems[i].gob_pm
        sub_total_item.rpa_pm = sub_total_item.rpa_pm+ritems[i].rpa_pm
        sub_total_item.dpa_pm = sub_total_item.dpa_pm+ritems[i].dpa_pm
        sub_total_item.total_pm = sub_total_item.total_pm+ritems[i].total_pm
        """Expenditure of the current month month"""
        sub_total_item.gob_cm = sub_total_item.gob_cm+ritems[i].gob_cm
        sub_total_item.rpa_cm = sub_total_item.rpa_cm+ritems[i].rpa_cm
        sub_total_item.dpa_cm = sub_total_item.dpa_cm+ritems[i].dpa_cm
        sub_total_item.total_cm = sub_total_item.total_cm+ritems[i].total_cm
        """Expenditue upto current month"""
        sub_total_item.gob_cmt = sub_total_item.gob_cmt+ritems[i].gob_cmt
        sub_total_item.rpa_cmt = sub_total_item.rpa_cmt+ritems[i].rpa_cmt
        sub_total_item.dpa_cmt = sub_total_item.dpa_cmt+ritems[i].dpa_cmt
        sub_total_item.total_cmt = sub_total_item.total_cmt+ritems[i].total_cmt
        """budget remaining"""
        sub_total_item.gob_rm = sub_total_item.gob_rm+ritems[i].gob_rm
        sub_total_item.rpa_rm = sub_total_item.rpa_rm+ritems[i].rpa_rm
        sub_total_item.dpa_rm = sub_total_item.dpa_rm+ritems[i].dpa_rm
        sub_total_item.total_rm = sub_total_item.total_rm+ritems[i].total_rm
    print("Allownances Total={} ".format(sub_total_item.total_allocation))
    #print("item11={} item2={}".format( sub_total_item.Ecode,ritems[0].Ecode))
    sub_total_item.roundToDigit(2)
    return sub_total_item






def displayRitems(ritems):
    for ritem in ritems:
        print(ritem.description)



def createMonthlyReportItems(budgets,fy,month):
    myframes=calculateCodeWiseMonthlyExpenditure(fy)
    monthly_gob=myframes['gob']
    #displayMonthlyExpenditure(monthly_gob)
    monthly_rpa = myframes['rpa']
    monthly_dpa = myframes['dpa']
    monthly_total = myframes['total']
    item_codes = Dpp_allocation.objects.all().order_by('pk').values("Ecode", "Shortdescription")
    report_items=[]
    #month=6
    myExpenditureDf=createMonthlyExpenditureDF()
    #print(monthly_gob)
    code_list=list(monthly_gob.iloc[:,1])
    months = [7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5, 6]
    print(monthly_gob)
    month_index = months.index(month) + 2
    #print(code_list)
    for budget in budgets:
        ri=MonthlyReportItem(budget)
        """setting current months data"""
        description = budget.Dpp_allocation.Shortdescription
        index=code_list.index(description)
        gob_cm=round(monthly_gob.iloc[index,month_index],2)
        rpa_cm=round(monthly_rpa.iloc[index,month_index],2)
        dpa_cm=round(monthly_dpa.iloc[index,month_index],2)
        total_cm=round(monthly_total.iloc[index,month_index],2)
        ri.setCurrentMonthExpenditure(gob_cm,rpa_cm,dpa_cm,total_cm)
        """calculating previous month expenditure"""
        pmexpenditure=caclculateExpenditureUptoPM(monthly_gob,monthly_rpa,monthly_dpa, monthly_total,index,month_index)
        pmexpenditure['pm_gob']
        ri.setPreviousMonthExpenditure( pmexpenditure['pm_gob'], pmexpenditure['pm_rpa'], pmexpenditure['pm_dpa'], pmexpenditure['pm_total'])
        ri.calCulateTotalUptoMonth()
        ri.calCulateRemainingBudget()
        ri. round2Lakh()
        #print(pmexpenditure)

        #print("index={} description={}".format(index,description))
        report_items.append(ri)
        #disPlayTotalExpenditure(budget,6)
    print("total expenditure entries={}".format(len(report_items)))
    #report_items=report_items[::-1] revesing of a list
    #displayRitems(report_items)
    allownances=subtotalAllownaces(report_items)
    services=subtotalSupplyAndServices(report_items)
    repair_maintain=subtotalRepairAndMaintenance(report_items)
    capital=subtotalCapital(report_items)
    revenue=subtotalRevenue(report_items)
    grandtotal=grandTotal(report_items)
    report_items.insert(3,allownances)
    report_items.insert(32,services)
    report_items.insert(43,repair_maintain)
    report_items.insert(44,revenue)
    report_items.insert(71,capital)
    report_items.append(grandtotal)

    return report_items


def createProgressReport():
    """ Basic Setup for Report Lab   """
    pdf_buffer = BytesIO()
    doc = SimpleDocTemplate(pdf_buffer)
    doc.pagesize = landscape(A4)
    flowables = []  # overall flowables
    structural_summary = []  # for structural summary
    contract_summary = []
    sample_style_sheet = getSampleStyleSheet()
    # sample_style_sheet.list()
    """Creating Style for Paragraph  """
    custom_body_style = sample_style_sheet['Heading5']
    # custom_body_style.listAttrs()
    custom_body_style.spaceAfter = 0
    custom_body_style.spaceBefore = 0
    myheading1="HFMLIP:Monthly Expenditure Report"
    myparagraph1 = Paragraph(myheading1, custom_body_style)
    flowables.append(myparagraph1)
    doc.build(flowables)
    pdf_value = pdf_buffer.getvalue()
    pdf_buffer.close()
    return pdf_value











