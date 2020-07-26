from django.shortcuts import render,redirect
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth import login as auth_login
from .forms import SignUpForm,UserEditForm,ProfileEditForm
from .models import Profile
from django.contrib.auth.decorators import login_required
from django.http import response
from .decorators import sp_admin_required
def signup(request):
    if request.method == 'POST':
        form = SignUpForm(request.POST)
        if form.is_valid():
            user = form.save()
            Profile.objects.create(user=user)
            auth_login(request, user)
            return redirect('home')
    else:
        form = SignUpForm()
    return render(request, 'signup.html', {'form': form})
@login_required()
@sp_admin_required
def editpProfile(request):
   if request.method=='POST':
       user_form = UserEditForm(instance=request.user,data=request.POST)
       profile_form = ProfileEditForm(instance=request.user.profile,data=request.POST)
       if user_form.is_valid() and profile_form.is_valid():
           user_form.save()
           profile_form.save()
           return redirect('home')
   else:
       user_form=UserEditForm(instance=request.user)
       profile_form=ProfileEditForm(instance=request.user.profile)
   return render(request, 'edit_profile.html', {'user_form': user_form,'profile_form':profile_form})




