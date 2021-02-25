from django.shortcuts import render
from django.shortcuts import redirect

from .models import SMS_info
# Create your views here.
def wl_index(request):
    sms_items= SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard2.html', {'sms_items': sms_items})
def data_collect_view(request):
    if request.method == 'GET':
        tbody = request.GET['text']
        cellno = request.GET['number']
        mysms=SMS_info(smsbody=tbody,mobile_no=cellno)
        mysms.save()
    sms_items = SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard2.html', {'sms_items':sms_items})

    #return redirect('wl_index')
def displayData(request):
    return redirect(data_collect_view)
def sendEmail(request):
    return redirect(data_collect_view)
def wl_Logiut(request):
    return redirect(data_collect_view)

