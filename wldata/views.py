from django.shortcuts import render
from django.shortcuts import redirect

from .models import SMS_info
# Create your views here.
def wl_index(request):
    dppitems = SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard.html', {'dppitems': dppitems})
def data_collect_view(request):
    if request.method == 'GET':
        tbody = request.GET['text']
        cellno = request.GET['number']
        mysms=SMS_info(smsbody=tbody,mobile_no=cellno)
        mysms.save()
    dppitems = SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard.html', {'dppitems': dppitems})

    #return redirect('wl_index')

