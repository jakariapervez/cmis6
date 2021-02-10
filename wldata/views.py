from django.shortcuts import render
from django.shortcuts import redirect

from .models import SMS_info
# Create your views here.
def wl_index(request):
    dppitems = SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard.html', {'dppitems': dppitems})
def data_collect_view(request):
    dppitems = SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard.html', {'dppitems': dppitems})

    #return redirect('wl_index')

