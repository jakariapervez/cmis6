from django import forms
#from .models import DPP_Intervention,Haor,Document,Contract_Intervention,ProgressItem,InvoiceImage
from .models import Dpp_allocation,Budget_allocation
"creating DPP_Intervention_form"
class DPP_intervention_form(forms.ModelForm):
    class Meta:
        model=Dpp_allocation
        fields=('Ecode', 'Description', 'Gob', 'Rpa', 'Dpa','Total')
class Budget_allocation_form(forms.ModelForm):
    class Meta:
        model=Budget_allocation
        fields=( 'Financial_year','Dpp_allocation', 'Gob', 'Rpa', 'Dpa','Total')
from .models import InvoiceImage
class InvoiceImageForm(forms.ModelForm):
    class Meta:
        model = InvoiceImage
        fields = ('description', 'invoice_image','issuing_date', 'uploaded_date',)








from .models import Invoice_details

class Invoice_details_Forms(forms.ModelForm):
    class Meta:
        model=Invoice_details

        fields=('Invoice_no','date','BatchType','Description','document_id', )

        #fields=("__all_")

from .models import Expenditure_details
class Expenditure_details_Forms(forms.ModelForm):
    #date=forms.DateField(required=False)
    class Meta:
        model=Expenditure_details
        #fields=('Gob','Dpa','Rpa','Total','Budget_allocation','date','Invoice_details' )
        fields = ('Gob', 'Dpa', 'Rpa',  'Budget_allocation')
        #widgets = {'Total': forms.NumberInput(attrs={'disabled': True}),'date':forms.DateInput(attrs={'disabled': True}),'Invoice_details':forms.TextInput(attrs={'disabled': True})}
class Expenditure_details_Forms2(forms.ModelForm):
    #date=forms.DateField(required=False)
    dpp_allocation = forms.ModelChoiceField(queryset=Dpp_allocation.objects.all())
    class Meta:
        model=Expenditure_details

        #fields=('Gob','Dpa','Rpa','Total','Budget_allocation','date','Invoice_details' )
        #fields = ('Gob', 'Dpa', 'Rpa',  'Budget_allocation')
        fields = ('Gob', 'Dpa', 'Rpa')
        #widgets = {'Total': forms.NumberInput(attrs={'disabled': True}),'date':forms.DateInput(attrs={'disabled': True}),'Invoice_details':forms.TextInput(attrs={'disabled': True})}




class Expenditure_details_Edit_Forms(forms.ModelForm):
    dpp_allocation = forms.ModelChoiceField(queryset=Dpp_allocation.objects.all())
    class Meta:
        model=Expenditure_details
        fields=('Gob','Dpa','Rpa' )
"""    
class DocumentForm(forms.ModelForm):
    class Meta:
        model = Document
        fields = ('description', 'document', )
from .models import ConstructionImage

class ConstructionImageForm(forms.ModelForm):
    class Meta:
        model = ConstructionImage
        fields = ('description', 'image','acquisition_date', 'structure_id',)

class ConstructionImageFormEnhanced(ConstructionImageForm):
    class Meta:
        model = ConstructionImage
        fields = ('description', 'image','acquisition_date', 'structure_id',)






class ContractAddForm(forms.ModelForm):
    class Meta:
        model=Contract_Intervention
        #fields=("contract_id","dpp_intervention_id","contract_component_id","so_id","sde_id","exen_id","site_eng_id","field_eng_id","consultant_eng_id")
        fields = ("contract_id", "dpp_intervention_id", "contract_component_id",'physical_weight','financial_weight')
        widgets = {'contract_id': forms.HiddenInput(),"dpp_intervention_id":forms.HiddenInput()}
        #fields = ("contract_component_id", "so_id", "sde_id", "exen_id", "site_eng_id", "field_eng_id", "consultant_eng_id")


class AddProgressItemForm(forms.ModelForm):
    class Meta:
        model=ProgressItem
        #fields=("contract_id","dpp_intervention_id","contract_component_id","so_id","sde_id","exen_id","site_eng_id","field_eng_id","consultant_eng_id")
        #widgets = {'contract_id': forms.HiddenInput(),"dpp_intervention_id":forms.HiddenInput()}
        #fields = ("contract_component_id", "so_id", "sde_id", "exen_id", "site_eng_id", "field_eng_id", "consultant_eng_id")
        fields=("intervention_id","item_name","unit","quantity","weight","startdate","finishdate")
        widgets = {'intervention_id': forms.HiddenInput()}
"""
class ProgressQuantityForm (forms.Form):
    quantity=forms.DecimalField()
    item_id=forms.IntegerField()
"""   
description = models.CharField(max_length=350, blank=True, null=True)
invoice_image = models.FileField(upload_to='invoice_docs/%Y/%m/%d/')
issuing_date=models.DateField(default=timezone.now)
uploaded_date = models.DateField(default=timezone.now)
"""
""""
Invoice_no=models.CharField(max_length=65,null=True,blank=True)
    date = models.DateField(default=timezone.now)
    BatchType=models.CharField(max_length=60)
    Description=models.CharField(max_length=60)
    Total_amount= models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=14)
    document_id = models.ForeignKey(InvoiceImage, on_delete=models.CASCADE, null=True, blank=True)
    FinancialYear=models.ForeignKey(FinancialYear,on_delete=models.CASCADE,null=True,blank=True)
    Month=models.CharField(max_length=2,null=True,blank=True)

"""

from django.utils.translation import gettext_lazy as _
class AddInvoiceFormCombined(forms.ModelForm):
    invoice_doc=forms.FileField(label='Upload Documents')
    #gob_val=forms.DecimalField(label="GoB(Tk.)")
    #rpa_val=forms.DecimalField(label="RPA(Tk.)")
    #dpa_val=forms.DecimalField(label="DPA(Tk.)")
    class Meta:
        #model = InvoiceImage
        model=Invoice_details
        fields=('BatchType','Description','date')
        labels={'BatchType':_('Bath No'),'Description':_('Batch Description'),'date':_('Payment Date')}
        #fields = ('description', 'invoice_image','issuing_date', 'uploaded_date',)

class InvoiceDocEditForm(forms.ModelForm):
    class Meta:
        model = InvoiceImage
        fields = ('invoice_image',)
    def clean_invoice_image(self):
        invoice_image=self.cleaned_data['invoice_image']
        return invoice_image










