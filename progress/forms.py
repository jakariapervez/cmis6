from django import forms
from .models import DPP_Intervention,Haor,Document,Contract_Intervention,ProgressItem
"creating DPP_Intervention_form"
class DPP_intervention_form(forms.ModelForm):
    class Meta:
        model=DPP_Intervention
        fields=('haor_id', 'worktype_id', 'name', 'start_chainage', 'finish_chainage','length','volume','vent_no', 'dpp_cost')
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
        fields=("intervention_id","item_name","unit","quantity","weight","plannedStartDate","plannedFinishDate","actualStartDate","actualFinishDate","workSequenceNumber","executionStatus")
        widgets = {'intervention_id': forms.HiddenInput()}
class ProgressQuantityForm (forms.Form):
    quantity=forms.DecimalField()
    item_id=forms.IntegerField()

from django.utils import timezone
from .models import ReportEvent
"""  
class ReportEventForm(forms.ModelForm):
    class Meta:
        model = ReportEvent
        fields=['reportCode','startingDate']
        #fields="__all__"     
     
        report_event_status = [("Live", "Live"), ("Dead", "Dead")]
        reportCode = models.CharField(max_length=100, null=True, blank=True)
        reportName = models.CharField(max_length=250, null=True, blank=True)
        startingDate = models.DateField(timezone.now)  # starting date of reporting period
        finishDate = models.DateField(timezone.now)  # finishing date of reporting period
        lastSubmissionDate = models.DateField(timezone.now)  # last submission date
        message = models.TextField(null=True, blank=True)
        eventStatus = models.CharField(max_length=100, choices=report_event_status, default="Live")
         """

import datetime

class ReportEventForm(forms.Form):
    personOptions=[("XEN","XEN"),("CSE","CSE"),("FSE","FSE")]
    reportName=forms.CharField(label="Report Name")
    startingPeriod=forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                   widget=forms.DateInput(format="%d%m%Y"),
                                   input_formats=('%d/%m/%Y',), help_text="dd/mm/yyyy")
    finishingPeriod = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                      widget=forms.DateInput(format="%d%m%Y"),
                                      input_formats=('%d/%m/%Y',),help_text="dd/mm/yyyy")
    submissionDate=forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                   widget=forms.DateInput(format="%d%m%Y"),input_formats=('%d/%m/%Y',),
                                   help_text="dd/mm/yyyy")
    reportingPerson = forms.MultipleChoiceField(label="Reporting Person",widget=forms.CheckboxSelectMultiple,
                                        choices=personOptions)
    """  
    def clean(self):
        cleaned_data=super(ReportEventForm,self).clean()
        stdate=cleaned_data.get("startingPeriod")
        name=cleaned_data.get("reportName")
        fdate=cleaned_data.get("finishingPeriod")
        subdate=cleaned_data.get("submissionDate")
        rp=cleaned_data.get("reportingPerson")
        print("name={} stdate={} findate={} subdate={} rp={}".format(name,stdate,fdate,subdate,rp))
    """
    def is_valid(self):
        valid = super(ReportEventForm, self).is_valid()
        return valid
class ReportEventEditForm(forms.Form):
    personOptions = [("XEN", "XEN"), ("CSE", "CSE"), ("FSE", "FSE")]
    statusOptions=[("Live","Live"),("Dead","Dead")]
    #reportName = forms.CharField(label="Report Name")
    startingPeriod = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                     widget=forms.DateInput(format="%d%m%Y"),
                                     input_formats=('%d/%m/%Y',), help_text="dd/mm/yyyy")
    finishingPeriod = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                      widget=forms.DateInput(format="%d%m%Y"),
                                      input_formats=('%d/%m/%Y',), help_text="dd/mm/yyyy")
    submissionDate = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                     widget=forms.DateInput(format="%d%m%Y"), input_formats=('%d/%m/%Y',),
                                     help_text="dd/mm/yyyy")
    reportingPerson = forms.MultipleChoiceField(label="Reporting Person", widget=forms.CheckboxSelectMultiple,
                                                choices=personOptions)
    reportStaus=forms.ChoiceField(label="Report Staus",widget=forms.Select(),choices=statusOptions)
    def set_InitiaValue(self,reportEvent):
        self.startingPeriod=reportEvent.startingDate
        self.finishingPeriod=reportEvent.finishDate
        self.submissionDate=reportEvent.lastSubmissionDate



    def is_valid(self):
        valid = super(ReportEventEditForm, self).is_valid()
        return valid



#label='What is your birth date?', widget=forms.SelectDateWidge
"""" 
report_event_status=[("Live","Live"),("Dead","Dead")]
    reportCode=models.CharField(max_length=100,null=True,blank=True)
    reportName=models.CharField(max_length=250,null=True,blank=True)
    startingDate=models.DateField(timezone.now)#starting date of reporting period
    finishDate=models.DateField(timezone.now) #finishing date of reporting period
    lastSubmissionDate=models.DateField(timezone.now)#last submission date
    message=models.TextField(null=True,blank=True)
    eventStatus=models.CharField(max_length=100,choices=report_event_status,default="Live")
"""
from .models import qualitativeStatus
class qprogresForm(forms.ModelForm):

    class Meta:

        model =qualitativeStatus
        fields = ('overall_status', 'current_progress', 'problems', 'value_of_work_done',)
