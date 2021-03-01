from django import forms
from .models import GaugeReading

class WL_Edit_Form(forms.ModelForm):
    class Meta:
        model=GaugeReading
        fields=('wlreading',)





