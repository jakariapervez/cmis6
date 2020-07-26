from django import forms
from django.contrib.auth.forms import UserCreationForm
from  django.contrib.auth.models import User

class SignUpForm(UserCreationForm):
    email=forms.CharField(max_length=254,required=True,widget=forms.EmailInput())
    class Meta:
        model=User
        fields=('username', 'email', 'password1', 'password2')
"Form related to profile update"
from .models import Profile

class UserEditForm(forms.ModelForm):
    class Meta:
        model=User
        fields=('first_name','last_name','email')
class ProfileEditForm(forms.ModelForm):
    class Meta:
        model=Profile
        fields=('cellNo',)