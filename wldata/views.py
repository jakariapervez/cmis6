from django.shortcuts import render
from django.shortcuts import redirect
from django.http import HttpResponse,JsonResponse
from .models import SMS_info,GaugeLocation
from django.template.loader import render_to_string
# Create your views here.
def wl_index(request):
    sms_items= SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard.html', {'sms_items': sms_items})
def data_collect_view(request):
    if request.method == 'GET':
        tbody = request.GET['text']
        cellno = request.GET['number']
        mysms=SMS_info(smsbody=tbody,mobile_no=cellno)
        mysms.save()
    sms_items = SMS_info.objects.all()
    gauges =GaugeLocation.objects.all()
    for g in gauges:
        print(g.gauge_code)
    return render(request, 'wldata/accounts_dashboard.html', {'sms_items':sms_items,'gauges':gauges})

    #return redirect('wl_index')
def displayData(request):
    sms_items = SMS_info.objects.all()
    gauges = GaugeLocation.objects.all()
    for g in gauges:
        print(g.gauge_code)

    return render(request, 'wldata/display.html', {'sms_items': sms_items, 'gauges': gauges})
def displayDataGaugewise(request):

    print("pass")
    data=dict()
    return JsonResponse(data)
from .auxilaryquery import getTodaysData,getFiveDaysData
def gaugeData(request):
    data = dict()
    gid=request.GET['gid']
    print(gid)
    mydata=getTodaysData(gid)
    myFiveDayData=getFiveDaysData(gid)
    greadings= mydata["readings"]
    context={"greadings":greadings}
    mytable=render_to_string('wldata/includes/gauges/partial_gauge_wl.html',context)
    data['gauge_readings']=mytable
    data['years']=myFiveDayData["years"]
    data["months"] = myFiveDayData["months"]
    data["days"]=myFiveDayData["days"]
    data["hours"]=myFiveDayData["hours"]
    data["wl"] = myFiveDayData["wl"]


    #print(mytable)
    print(myFiveDayData)

    return JsonResponse(data)






def sendEmail(request):
    return redirect(data_collect_view)
def wl_Logiut(request):
    return redirect(data_collect_view)

