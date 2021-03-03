from django import forms
from .models import GaugeReading, communicationList


class WL_Edit_Form(forms.ModelForm):
    class Meta:
        model = GaugeReading
        fields = ('wlreading',)


"""Form Related to Contact"""
"""    
person_designation = models.CharField(max_length=200, null=True, blank=True)
    person_email = models.CharField(max_length=200, null=True, blank=True)
    person_mobile= models.CharField(max_length=200, null=True, blank=True)
    added_by=models.ForeignKey(settings.AUTH_USER_MODEL,on_delete=models.CASCADE,null=True,blank=True)

"""
from django.utils.translation import gettext_lazy as _


class contactAddForm(forms.ModelForm):
    class Meta:
        model = communicationList
        fields = ('person_designation', 'person_email', 'person_mobile',)
        labels = {'person_designation': _('Name_Designation'), 'person_email': _('EMAIL'),
                  'person_mobile': _("CELL PHONE"),
                  }
        help_texts = {'person_designation': _('Name and Designation within 200 characters'),
                      'person_email': _('Valid Email Addess'),
                      'person_mobile': _("Valid Mobile Phone Number"), }


class contactUpdateForm(forms.ModelForm):
    class Meta:
        model = communicationList
        fields = ('person_designation', 'person_email', 'person_mobile',)


class contactDeleteForm(forms.ModelForm):
    class Meta:
        model = communicationList
        fields = ('person_designation', 'person_email', 'person_mobile',)
