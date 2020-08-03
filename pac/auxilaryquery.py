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
def getCumulative(myyear,economicCode):
    dpa=0
    gob=0
    rpa=0
    total=0
    #expenditures=Expenditure_details.objects.filter( Budget_allocation__Financial_year=myyear).filter(Budget_allocation_Dpp_allocation_ Ecode=economicCode)
    #expenditures = Expenditure_details.objects.filter(Budget_allocation__Dpp_allocation__Ecode=economicCode).filter(Budget_allocation__Financial_year=myyear)
    expenditures = Expenditure_details.objects.filter(Budget_allocation__Dpp_allocation__Ecode=economicCode).filter(
    Budget_allocation__Financial_year=myyear)
    print(expenditures)
    for expenditure in expenditures:
        dpa+=expenditure.Dpa
        rpa+=expenditure.Rpa
        gob+=expenditure.Gob
        total+=expenditure.Total
    return {'Gob':gob,'Dpa':dpa,'Rpa':rpa,'Total':total}
def checkCumulative(fy,economicCode,expenditure,budget_allocation):
    cumulative = getCumulative(fy,economicCode)
    if(budget_allocation.Gob-cumulative['Gob'])>=expenditure.Gob:
        isValid=True
    else:
        isValid=False
    if (budget_allocation.Dpa - cumulative['Dpa']) >= expenditure.Dpa:
        isValid = True
    else:
        isValid = False
    if (budget_allocation.Rpa - cumulative['Rpa']) >= expenditure.Rpa:
        isValid = True
    else:
        isValid = False
    if (budget_allocation.Total - cumulative['Total']) >= expenditure.Total:
        isValid = True
    else:
        isValid = False
    if isValid:
        cumTotal=cumulative['Total']+expenditure.Total
    else:
        cumTotal = cumulative['Total']

    return {'isValid':isValid,'Total':cumTotal}
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
    budget_allocation=exp_form.cleaned_data['Budget_allocation']
    economicCode=budget_allocation.Dpp_allocation.Ecode
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

    myyear = financialYearFromDate(invoice.date)

    validity=checkCumulative(myyear,economicCode,expenditure,budget_allocation)
    invoice_total = getInvoiceTotal(myyear,invoice)
    invoice_total=invoice_total+float(expenditure.Total)
    print("Expenditure={} Cumtotal={}".format(validity['isValid'],validity['Total']))
    #myyear = financialYearFromDate(expenditure.date)
    fy = financialYearFromDate(expenditure.date)
    month = monthFromDate(expenditure.date)
    #isValid=checkCumulative(myyear,expenditure)
    return {'expenditure': expenditure, 'validity': validity['isValid'], 'cumtotal':invoice_total,'fyear':fy,'month':month }

    #return {'expenditure':expenditure,'validity':validity['isValid'],'cumtotal':validity['Total']}
def validateExpenditureEditForm(exp_form,expenditure):
    gob = exp_form.cleaned_data['Gob']
    rpa = exp_form.cleaned_data['Rpa']
    dpa = exp_form.cleaned_data['Dpa']
    total = gob + dpa + rpa
    print("GoB={} DPA={} RPA={} Total={}".format(gob, dpa, rpa, total))
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
        self.gob_allocation=(float(budget.Gob)/100000.00)
        self.rpa_allocation=(float(budget.Rpa)/100000.00)
        self.dpa_allocation=(float(budget.Dpa)/100000.00)
        self.total_allocation=(float(budget.Total)/100000.00)
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
        self.gob_pm = round(gob,2)
        self.rpa_pm = round(rpa,2)
        self.dpa_pm = round(dpa,2)
        self.total_pm = round(total,2)
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




        """"   
        Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
        Dpa = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
        Rpa = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
        Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
        """
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
                gob=float(exp.Gob)/100000.00
                rpa=float(exp.Rpa)/100000.00
                dpa=float(exp.Dpa)/100000.00
                total=float(exp.Total)/100000.00
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
    print("prevoius month={}".format(monthindex-1))

    for i in  range(2,monthindex):
        pm_gob=pm_gob+myframe_gob.iloc[dataIndex,i]
        pm_rpa=pm_rpa+myframe_rpa.iloc[dataIndex,i]
        pm_dpa=pm_dpa+myframe_dpa.iloc[dataIndex,i]
        pm_total=pm_total+myframe_total.iloc[dataIndex,i]
    pmexpenditure={"pm_gob":pm_gob,'pm_rpa':pm_rpa,'pm_dpa':pm_dpa,'pm_total':pm_total }
    return pmexpenditure



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
        #print(pmexpenditure)

        #print("index={} description={}".format(index,description))
        report_items.append(ri)
        #disPlayTotalExpenditure(budget,6)
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











