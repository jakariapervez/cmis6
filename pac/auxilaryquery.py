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

def displayBudgetAllocation(budgetAllocation):
    #Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    #Dpa = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    #Rpa = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    #Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    #Dpp_allocation = models.ForeignKey(Dpp_allocation, on_delete=models.SET_NULL, null=True, blank=True)
    #Financial_year = models.ForeignKey(FinancialYear, on_delete=models.SET_NULL, null=True, blank=True
    Gob=budgetAllocation.Gob
    Dpa=budgetAllocation.Dpa
    Rpa=budgetAllocation.Rpa
    Total=budgetAllocation.Total
    Dppitem=budgetAllocation.Dpp_alloication.Shortdescription
    fy=budgetAllocation.Financial_year
    print("displaying budget allocation..........")
    print("gob={} dpa={} rpa={} total={} Dppitem={} fy={}".format(Gob,Dpa,Rpa,Total,Dppitem,fy))







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
    print("printing budget allocation.....................")
    print("tyype of budget allocation={}".format(type(budget_allocation)))
    print(budget_allocation)
    print("gob={} dpa={} rpa={} total={}".format(budget_allocation.Gob,budget_allocation.Dpa,budget_allocation.Rpa,budget_allocation.Total))
    #displayBudgetAllocation(budget_allocation)

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
    invoice_total=invoice_total+float(total)
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
    displayBudgetAllocation(budget_allocation)
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











