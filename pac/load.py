import os
import pandas as pd
from .models import Invoice_details
from django.shortcuts import get_object_or_404
def disPlayDF(mydf):
    for index,row in mydf.iterrows():
        print(row)

"""  
class Invoice_details(models.Model):
    Invoice_no=models.CharField(max_length=65,null=True,blank=True)
    date = models.DateField(default=timezone.now)
    BatchType=models.CharField(max_length=60)
    Description=models.CharField(max_length=600)
    Total_amount= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=21)
    document_id = models.ForeignKey(InvoiceImage, on_delete=models.SET_NULL, null=True, blank=True)
    FinancialYear=models.ForeignKey(FinancialYear,on_delete=models.CASCADE,null=True,blank=True)
    Month=models.CharField(max_length=2,null=True,blank=True)

    def __str__(self):
        return f'{self.Invoice_no}'
 """
""""   
class Expenditure_details(models.Model):
    Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Dpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Rpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Budget_allocation= models.ForeignKey(Budget_allocation, on_delete=models.CASCADE, null=True, blank=True)
    date = models.DateField(default=timezone.now)
    Invoice_details= models.ForeignKey(Invoice_details, on_delete=models.CASCADE, null=True, blank=True)
"""
from .models import Invoice_details,Expenditure_details,InvoiceImage,FinancialYear
from django.utils.dateparse import parse_date
def createDate(date_string):
    return parse_date(date_string)
def getInvoiceId(row):
    dstring=row['Batch Date']
    mydate=createDate(dstring)
    invoiceNo=row['InvoiceNo']
    btype="Livelihood"
    des=row['Training Detail']
    tl=0
    #doc_id=128
    #doc_id=get_object_or_404(InvoiceImage,id=128)
    doc_id= InvoiceImage.objects.get(pk=80)
    Fy=FinancialYear(id=2)
    month=mydate.month
    gob=float(row['GOB'])
    rpa=float(row['RPA'])
    tl=gob+rpa



    ivd=Invoice_details(Invoice_no=invoiceNo, date=mydate,BatchType=btype,Total_amount=tl,
                        Description=des,document_id=doc_id, FinancialYear=Fy,Month=month)
    ivd.save()
    print("primary key of created invoice={}".format(ivd.pk))




    print(mydate)


def createLivelihoodExpenditure(verbose=True):
    myfolder=os.path.abspath(os.path.dirname(__file__))
    print(myfolder)
    exp_input_path=os.path.join(myfolder,'LH.csv')
    budget_input_path=os.path.join(myfolder,'Budget.csv')
    print(exp_input_path)
    input_df=pd.read_csv(exp_input_path,encoding= 'unicode_escape')
    budget_df=pd.read_csv(budget_input_path,encoding= 'unicode_escape')
    #disPlayDF(input_df)
    #disPlayDF(budget_df)
    row=input_df.iloc[1]
    print(row)
    getInvoiceId(row)

    """  
    for index, row in input_df.iterrows():
        getInvoiceId(row)
    """

