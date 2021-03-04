from django.shortcuts import render
from django.shortcuts import redirect, get_object_or_404
from django.http import HttpResponse, JsonResponse
from .models import SMS_info, GaugeLocation
from django.template.loader import render_to_string


# Create your views here.
def wl_index(request):
    sms_items = SMS_info.objects.all()
    return render(request, 'wldata/accounts_dashboard.html', {'sms_items': sms_items})


def data_collect_view(request):
    if request.method == 'GET':
        tbody = request.GET['text']
        cellno = request.GET['number']
        mysms = SMS_info(smsbody=tbody, mobile_no=cellno)
        mysms.save()
    sms_items = SMS_info.objects.all()
    gauges = GaugeLocation.objects.all()
    for g in gauges:
        print(g.gauge_code)
    return render(request, 'wldata/accounts_dashboard.html', {'sms_items': sms_items, 'gauges': gauges})

    # return redirect('wl_index')


def displayData(request):
    sms_items = SMS_info.objects.all()
    gauges = GaugeLocation.objects.all()
    for g in gauges:
        print(g.gauge_code)

    return render(request, 'wldata/display.html', {'sms_items': sms_items, 'gauges': gauges})


def displayDataGaugewise(request):
    print("pass")
    data = dict()
    return JsonResponse(data)


from .auxilaryquery import getTodaysData, getFiveDaysData


def gaugeData(request):
    data = dict()
    gid = request.GET['gid']
    print(gid)
    mydata = getTodaysData(gid)
    myFiveDayData = getFiveDaysData(gid)
    greadings = mydata["readings"]
    context = {"greadings": greadings}
    mytable = render_to_string('wldata/includes/gauges/partial_gauge_wl.html', context)
    data['gauge_readings'] = mytable
    data['years'] = myFiveDayData["years"]
    data["months"] = myFiveDayData["months"]
    data["days"] = myFiveDayData["days"]
    data["hours"] = myFiveDayData["hours"]
    data["wl"] = myFiveDayData["wl"]

    # print(mytable)
    print(myFiveDayData)

    return JsonResponse(data)


from .forms import WL_Edit_Form
from .models import GaugeReading


def gaugeDataEdit(request, pk):
    data = dict()
    gaugereading = get_object_or_404(GaugeReading, pk=pk)
    form = WL_Edit_Form(instance=gaugereading)
    if request.method == 'POST':
        form = WL_Edit_Form(request.POST, instance=gaugereading)
        if form.is_valid:
            form.save()
            data['form_is_valid'] = True
            gid = gaugereading.gauge_name.id
            print("gauge id=".format(id))
            mydata = getTodaysData(gid)
            myFiveDayData = getFiveDaysData(gid)
            greadings = mydata["readings"]
            context = {"greadings": greadings}
            mytable = render_to_string('wldata/includes/gauges/partial_gauge_wl.html', context)
            data['gauge_readings'] = mytable
            data['years'] = myFiveDayData["years"]
            data["months"] = myFiveDayData["months"]
            data["days"] = myFiveDayData["days"]
            data["hours"] = myFiveDayData["hours"]
            data["wl"] = myFiveDayData["wl"]

        else:
            data['form_is_valid'] = False
    else:
        context = {'form': form, }
        data = dict()
        data['html_form'] = render_to_string('wldata/includes/gauges/wl_edit_form.html', context, request=request)
    return JsonResponse(data)


def gaugeDataDelete(request, pk):
    data = dict()
    gaugereading = get_object_or_404(GaugeReading, pk=pk)
    form = WL_Edit_Form(instance=gaugereading)
    if request.method == 'POST':
        gaugereading.delete()
        data['form_is_valid'] = True
        gid = gaugereading.gauge_name.id
        print("gauge id=".format(id))
        mydata = getTodaysData(gid)
        myFiveDayData = getFiveDaysData(gid)
        greadings = mydata["readings"]
        context = {"greadings": greadings}
        mytable = render_to_string('wldata/includes/gauges/partial_gauge_wl.html', context)
        data['gauge_readings'] = mytable
        data['years'] = myFiveDayData["years"]
        data["months"] = myFiveDayData["months"]
        data["days"] = myFiveDayData["days"]
        data["hours"] = myFiveDayData["hours"]
        data["wl"] = myFiveDayData["wl"]
    else:
        context = {'form': form, }
        data = dict()
        data['html_form'] = render_to_string('wldata/includes/gauges/wl_delete_form.html', context, request=request)
    return JsonResponse(data)


from .auxilaryquery import getTimeSlotReading


def selectWLatParticularTime(request):
    data = dict()
    hour = request.GET['hour']
    user = request.user
    uid = user.pk
    print("hour={} user={}".format(hour, user))
    mydata = getTimeSlotReading(hour, uid)
    print(mydata)
    context = {"greadings": mydata}
    mytable = render_to_string('wldata/includes/gauges/partial_time_wl.html', context)
    data['gauge_readings'] = mytable
    return JsonResponse(data)


def sendEmail(request):
    sms_items = SMS_info.objects.all()
    gauges = GaugeLocation.objects.all()
    for g in gauges:
        print(g.gauge_code)
    return render(request, 'wldata/send_email.html', {'sms_items': sms_items, 'gauges': gauges})


"""Import related to sending email """
from django.conf import settings as mysettings

from django.core.mail import send_mail, EmailMessage

from .auxilaryquery import createWLReport, getDivisionName


def sendWLByEmail(request):
    data = dict()
    hour = request.GET['hour']
    user = request.user
    uid = user.pk
    myvalue = createWLReport(hour, uid)
    myreport = myvalue['report']
    reporting_time = myvalue['time']
    # print(myreport)
    # print("Send Email has hit server....")
    # print("Email will send from={}".format(mysettings.EMAIL_HOST_USER))
    sub = "WL at different gauge stations of " + getDivisionName(uid) + " on " + reporting_time
    message = "Attached herewith "
    sender = mysettings.EMAIL_HOST_USER

    reciepients = ["jakariapervez@gmail.com", "sarfarazbanda48@yahoo.com", "skkader404@gmail.com"]
    # send_mail(subject=sub,message=message,from_email=mysettings.EMAIL_HOST_USER,recipient_list=reciepients,fail_silently=False)
    draft_email = EmailMessage(sub, message, sender, reciepients)
    draft_email.attach("wl.pdf", myreport)
    draft_email.send()
    return JsonResponse(data)


from .models import communicationList


def communicationListIndex(request):
    user = request.user
    uid = user.pk
    sms_items = SMS_info.objects.all()
    gauges = GaugeLocation.objects.all()
    contacts = communicationList.objects.filter(added_by=uid)
    context = {'contacts': contacts}

    return render(request, 'wldata/view_edit_contacts.html', context)


from .forms import contactAddForm



def communicationListAddContact(request):
    data = dict()
    print("sucessfully hitted server for adding contact....")
    form = contactAddForm()

    if request.method == "POST":
        print("I am in post request .................")
        form = contactAddForm(request.POST)
        ##data['html_form'] = form
        if form.is_valid:
            user = request.user
            uid = user.pk
            print("this is a vaild form.....................")
            cname = request.POST['person_designation']

            cemail = request.POST['person_email']
            cmobile = request.POST['person_mobile']
            mycontact = communicationList(person_designation=cname, person_email=cemail, person_mobile=cmobile,
                                          added_by=user)
            mycontact.save()
            #addContactFormSave(form, uid)
            contacts = communicationList.objects.filter(added_by=uid)
            context = {'contacts': contacts}
            mytable = render_to_string('wldata/includes/gauges/partial_contact_list.html', context)
            data['form_is_valid'] = True
            data['html_ivt_list'] = mytable
        else:

            data['form_is_valid'] = False


    else:
        print("I am in get request .................")
        context = {'form': form, }         
        data['html_form'] = render_to_string('wldata/includes/gauges/contact_add_form.html', context, request=request)
        print(data['html_form'])

    return JsonResponse(data)



def communicationListEditContact(request, pk):
    data = dict()
    user = request.user
    uid = user.pk
    mycontact = get_object_or_404(communicationList, pk=pk)
    form=contactAddForm(instance=mycontact)
    if request.method=='POST':
        form = contactAddForm(request.POST)
        if form.is_valid:
            cname = request.POST['person_designation']
            cemail = request.POST['person_email']
            cmobile = request.POST['person_mobile']
            mycontact.person_designation=cname
            mycontact.person_email=cemail
            mycontact.person_mobile=cmobile
            mycontact.save()
            contacts = communicationList.objects.filter(added_by=uid)
            context = {'contacts': contacts}
            mytable = render_to_string('wldata/includes/gauges/partial_contact_list.html', context)
            data['form_is_valid'] = True
            data['html_ivt_list'] = mytable
        else:
            data['form_is_valid'] = False

    else:
        print("I am in get request of edit contact......")
        context={'form':form,}
        data['html_form']=render_to_string('wldata/includes/gauges/contact_update_form.html', context, request=request)
    return JsonResponse(data)


def communicationListDeleteContact(request, pk):
    data = dict()
    user = request.user
    uid = user.pk
    mycontact= get_object_or_404(communicationList, pk=pk)
    form = contactAddForm(instance=mycontact)
    if request.method == 'POST':
        mycontact.delete()
        data['form_is_valid'] = True
        contacts = communicationList.objects.filter(added_by=uid)
        context = {'contacts': contacts}
        mytable = render_to_string('wldata/includes/gauges/partial_contact_list.html', context)
        data['form_is_valid'] = True
        data['html_ivt_list'] = mytable
    else:
        context = {'form': form, }
        data['html_form'] = render_to_string('wldata/includes/gauges/contact_delete_form.html', context, request=request)
    return JsonResponse(data)



def wl_Logiut(request):
    return redirect(data_collect_view)
