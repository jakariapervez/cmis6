from django.db import models
from django.utils import timezone
from datetime import datetime
# Create your models here.
class Dpp_allocation(models.Model):
    Ecode=models.IntegerField(null=True,blank=True)
    Description=models.CharField(max_length=400)
    Shortdescription=models.CharField(max_length=250,null=True,blank=True)
    Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Dpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Rpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)

    def __str__(self):
        return f'{self.Description}'

class FinancialYear(models.Model):
    startDate=models.DateField()
    finishDate=models.DateField()

    def __str__(self):
       return(str(self.startDate.year)+"-"+str(self.finishDate.year))




class Budget_allocation(models.Model):
    Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Dpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Rpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Dpp_allocation = models.ForeignKey(Dpp_allocation, on_delete=models.SET_NULL, null=True, blank=True)
    Financial_year = models.ForeignKey(FinancialYear, on_delete=models.SET_NULL, null=True, blank=True)

    def __str__(self):
        dppallocation=self.Dpp_allocation
        #return(str(dppallocation.description))
        return (str(self.Dpp_allocation.Ecode)+"_"+str(self.Dpp_allocation.Shortdescription) +"_"+str(self.Financial_year))

class InvoiceImage(models.Model):
    description = models.CharField(max_length=350, blank=True, null=True)
    invoice_image = models.FileField(upload_to='invoice_docs/%Y/%m/%d/')
    issuing_date=models.DateField(default=timezone.now)
    uploaded_date = models.DateField(default=timezone.now)

    def __str__(self):
        return f'{self.description}'




class Invoice_details(models.Model):
    Invoice_no=models.CharField(max_length=65,null=True,blank=True)
    date = models.DateField(default=timezone.now)
    BatchType=models.CharField(max_length=60)
    Description=models.CharField(max_length=60)
    Total_amount= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=14)
    document_id = models.ForeignKey(InvoiceImage, on_delete=models.CASCADE, null=True, blank=True)
    FinancialYear=models.ForeignKey(FinancialYear,on_delete=models.CASCADE,null=True,blank=True)
    Month=models.CharField(max_length=2,null=True,blank=True)

    def __str__(self):
        return f'{self.Invoice_no}'


class InvoiceSupporting(models.Model):
    description = models.CharField(max_length=255, blank=True, null=True)
    image = models.FileField(upload_to='invoice_docs/%Y/%m/%d/')
    issuing_date = models.DateField(default=timezone.now)
    uploaded_date = models.DateField(default=timezone.now)





class Expenditure_details(models.Model):
    Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Dpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Rpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Budget_allocation= models.ForeignKey(Budget_allocation, on_delete=models.CASCADE, null=True, blank=True)
    date = models.DateField(default=timezone.now)
    Invoice_details= models.ForeignKey(Invoice_details, on_delete=models.CASCADE, null=True, blank=True)







class Expenditure_details2(models.Model):
    Gob = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Dpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Rpa= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Total = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=18)
    Budget_allocation = models.ForeignKey(Budget_allocation, on_delete=models.SET_NULL, null=True, blank=True)
    date = models.DateField(default=timezone.now)
    Invoice_details_id= models.ForeignKey(Invoice_details, on_delete=models.SET_NULL, null=True, blank=True)



