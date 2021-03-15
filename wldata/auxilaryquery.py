import os
import pandas as pd
import datetime
import random
import math
from .models import GaugeLocation, GaugeReading


def generateWL(year, month, days, max_val, min_val, gauge_id):
    times = []
    readings = []

    for d in days:
        D1 = datetime.datetime(year, month, d, 6)
        times.append(D1)
        wl = float(random.randint(min_val, max_val) / 100)
        readings.append(wl)
        myreading = GaugeReading(gauge_name=gauge_id, reading_time=D1, wlreading=wl)
        myreading.save()
        D2 = datetime.datetime(year, month, d, 9)
        times.append(D2)
        wl = float(random.randint(min_val, max_val) / 100)
        readings.append(wl)
        myreading = GaugeReading(gauge_name=gauge_id, reading_time=D2, wlreading=wl)
        myreading.save()
        D3 = datetime.datetime(year, month, d, 12)
        times.append(D3)
        wl = float(random.randint(min_val, max_val) / 100)
        readings.append(wl)
        myreading = GaugeReading(gauge_name=gauge_id, reading_time=D3, wlreading=wl)
        myreading.save()
        D4 = datetime.datetime(year, month, d, 15)
        times.append(D4)
        wl = float(random.randint(min_val, max_val) / 100)
        readings.append(wl)
        myreading = GaugeReading(gauge_name=gauge_id, reading_time=D4, wlreading=wl)
        myreading.save()
        D5 = datetime.datetime(year, month, d, 18)
        times.append(D5)
        wl = float(random.randint(min_val, max_val) / 100)
        readings.append(wl)
        myreading = GaugeReading(gauge_name=gauge_id, reading_time=D5, wlreading=wl)
        myreading.save()


def daysforOneMonth(mybegining):
    m30 = [4, 6, 9, 11]
    m31 = [1, 3, 5, 7, 8, 10, 12]
    m = mybegining.month
    if m in m30:
        mydays = [i for i in range(1, 31)]
        print("this month has 30 days")
    elif m in m31:
        mydays = [i for i in range(1, 32)]
        print("this month has 30 days")
    else:
        year = mybegining.year
        mymod = year % 4
        if mymod == 0:
            mydays = [i for i in range(1, 30)]
            print("this month has 29 days")
        else:
            mydays = [i for i in range(1, 29)]
            print("this month has 28 days")

    return mydays


def inputMonthlyWL(verbose=True):
    mypath = os.path.join(os.path.dirname(__file__), 'data.txt')
    myfile = open(mypath, 'r')
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    year = int(mytexts[1])
    print(year)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    month = int(mytexts[1])
    print(month)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    gauge = mytexts[1].rstrip("\n")
    print(gauge)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    max_val = float(mytexts[1].rstrip("\n"))
    max_val = math.ceil(max_val * 100)
    print(max_val)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    min_val = float(mytexts[1].rstrip("\n"))
    min_val = math.ceil(min_val * 100)
    print(min_val)
    mybegining = datetime.datetime(year, month, 1)
    print(mybegining)
    day_in_month = daysforOneMonth(mybegining)
    print(day_in_month)
    mygauge_id = GaugeLocation.objects.filter(gauge_code=gauge)[0]
    print(mygauge_id)
    generateWL(year, month, day_in_month, max_val, min_val, mygauge_id)

    myfile.close()


def getTodaysData(gaugeid):
    mydate = datetime.datetime.now()
    myyear = mydate.year
    myday = mydate.day
    mymonth = mydate.month
    mydata = list(GaugeReading.objects.filter(reading_time__year=myyear, reading_time__month=mymonth,
                                              reading_time__day=myday, gauge_name=gaugeid).order_by('reading_time'))
    wl = []

    for m in mydata:
        wl.append(float(m.wlreading))
    myvalue = {"readings": mydata, "wl": wl}
    return myvalue


def getFiveDaysData(gaugeid):
    mydate = datetime.datetime.now()
    myyear = mydate.year
    myday = mydate.day
    mymonth = mydate.month
    substract_days = datetime.timedelta(days=4)
    stdate = mydate - substract_days
    stdate = stdate.replace(hour=6)
    fdate = datetime.datetime(myyear, mymonth, myday, 18)
    mydata = list(GaugeReading.objects.filter(reading_time__gte=stdate,
                                              reading_time__lte=fdate, gauge_name=gaugeid).order_by('reading_time'))
    wl = []
    years = []
    months = []
    days = []
    hours = []
    for m in mydata:
        wl.append(float(m.wlreading))
        years.append(m.reading_time.year)
        months.append(m.reading_time.month)
        days.append(m.reading_time.day)
        hours.append(m.reading_time.hour)
    myvalue = {"wl": wl, "years": years, "days": days, "hours": hours, "months": months}
    return myvalue


from .models import GaugeReading, DivisionNames
from django.shortcuts import get_object_or_404


def getTimeSlotReading2(hour, user_id):
    mydivision = get_object_or_404(DivisionNames, division_user_id=user_id)
    mydate = datetime.datetime.now()
    myyear = mydate.year
    mymonth = mydate.month
    myday = mydate.day
    myquery_date = datetime.datetime(myyear, mymonth, myday)
    print(myquery_date)
    if hour == 6:
        stdate = myquery_date.copy(deep=True)
        fdate = myquery_date.copy(deep=True)
        fdate = fdate.replace(hour=6)
    elif hour == 9:
        stdate = datetime.datetime(year=myyear, month=mymonth, day=myday, hour=9)
        fdate = datetime.datetime(myyear, mymonth, myday, 11)
    elif hour == 12:
        stdate = datetime.datetime(myyear, mymonth, myday, 12)
        fdate = datetime.datetime(myyear, mymonth, myday, 14)
    elif hour == 15:
        stdate = datetime.datetime(myyear, mymonth, myday, 15)
        fdate = datetime.datetime(myyear, mymonth, myday, 17)
    else:
        stdate = datetime.datetime(myyear, mymonth, myday, 18)
        fdate = datetime.datetime(myyear, mymonth, myday, 22)
    """    
    mydata = list(GaugeReading.objects.filter(reading_time__gte=stdate,
                                              reading_time__lte=fdate,
                                              gauge_name__division_name= mydivision,gauge_name__reported_gauge=True))
    """
    print(stdate)
    print(fdate)
    mydata = list(GaugeReading.objects.filter(reading_time__gte=stdate, reading_time__lte=fdate, ))
    return mydata


def getTimeSlotReading(reading_hour, user_id):
    reading_hour = int(reading_hour)
    mydate = datetime.datetime.now()
    myyear = mydate.year
    myday = mydate.day
    mymonth = mydate.month
    if reading_hour == 6:
        h1 = 6
        h2 = 8
    elif reading_hour == 9:
        h1 = 9
        h2 = 11
    elif reading_hour == 12:
        h1 = 12
        h2 = 14
    elif reading_hour == 15:
        h1 = 15
        h2 = 17
    elif reading_hour == 18:
        h1 = 18
        h2 = 20
    print("h1={} h2={}".format(h1, h2))
    mydata = list(GaugeReading.objects.filter(reading_time__year=myyear, reading_time__month=mymonth,
                                              reading_time__day=myday, ).order_by('reading_time'))
    filtered_reading = []
    for d in mydata:
        gauge_user_id = d.gauge_name.division_name.division_user_id.pk
        print("gauge_user_id={}".format(gauge_user_id))
        if h1 <= d.reading_time.hour <= h2:
            print("found")
            if gauge_user_id == user_id:
                filtered_reading.append(d)

    # myvalue = {"readings": mydata, "wl": wl}
    return filtered_reading


def getTimeSlotFromHour(hour):
    start_hours = [6, 9, 12, 15, 18]
    fisnish_hours = [8, 11, 14, 17, 20]
    time_slot = [6, 9, 12, 15, 18]
    idx = time_slot.index(hour)
    h1 = start_hours[idx]
    h2 = fisnish_hours[idx]
    myvalue = {"h1": h1, "h2": h2}
    return myvalue
def getTimeSlotReading(h1,h2):
    pass
class DailyGaugeReading():
    def __init__(self):
        pass

from .models import GaugeLocation,DivisionNames
def getTimeSlotReadingCumulative(reading_hour, user_id):
    reading_hour = int(reading_hour)
    mydate = datetime.datetime.now()
    myyear = mydate.year
    myday = mydate.day
    mymonth = mydate.month
    gtimes = [6, 9, 12, 15, 18]
    mydivision=DivisionNames.objects.get(division_user_id=user_id)
    print("{}".format(mydivision.division_name))
    mygauges=list(GaugeLocation.objects.filter(division_name=mydivision ))
    gnames=[g.gauge_code for g in mygauges]
    rivers=[g.river_name.river_name for g in mygauges]
    locs=[g.gauge_station_name  for g in mygauges]
    print("{}".format(mygauges))
    #print("mydivision={} mygauges={}".format(mydivision.division_name,mygauges))
    td_6 = []
    td_9 = []
    td_12 = []
    td_15 = []
    td_18 = []
    for hour in gtimes:
        myvalue = getTimeSlotFromHour(hour)
        h1 = myvalue['h1']
        h2 = myvalue['h2']
        print("h1={} h2={}".format(h1, h2))
        mydata = list(GaugeReading.objects.filter(reading_time__year=myyear, reading_time__month=mymonth,
                                                  reading_time__day=myday,reading_time__hour__gte=h1,
                                                  reading_time__hour__lte=h2,gauge_name__in=mygauges).order_by('-gauge_name__gauge_code'))
        for d in mydata:
            if hour == 6:
                td_6.append(d.wlreading)
            elif hour == 9:
                td_9.append(d.wlreading)
            elif hour == 12:
                td_12.append(d.wlreading)
            elif hour == 15:
                td_15.append(d.wlreading)
            elif hour == 18:
                td_18.append(d.wlreading)

        """  
        for d in mydata:
            gauge_user_id = d.gauge_name.division_name.division_user_id.pk
            if gauge_user_id == user_id:
                

       """







    print(td_6)
    print(td_18)
    """getting yesterday data"""
    myvalue = getTimeSlotFromHour(18)
    h1 = myvalue['h1']
    h2 = myvalue['h2']

    mydata = list(GaugeReading.objects.filter(reading_time__year=myyear, reading_time__month=mymonth,
                                              reading_time__day=myday-1, reading_time__hour__gte=h1,
                                              reading_time__hour__lte=h2, gauge_name__in=mygauges).order_by(
        '-gauge_name__gauge_code'))
    td_yt=[]
    for d in mydata:
        td_yt.append(d.wlreading)

    mylist ={"td_6":td_6,"td_9":td_9,"td_12":td_12,"td_15":td_15,"td_18":td_18,"gn":gnames,"rivers":rivers,"locs":locs,"yd":td_yt}

    return mylist

    """          
    if reading_hour == 6:
        h1 = 6
        h2 = 8
    elif reading_hour == 9:
        h1 = 9
        h2 = 11
    elif reading_hour == 12:
        h1 = 12
        h2 = 14
    elif reading_hour == 15:
        h1 = 15
        h2 = 17
    elif reading_hour == 18:
        h1 = 18
        h2 = 20
    """

    # myvalue = {"readings": mydata, "wl": wl}



from reportlab.lib.units import cm
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Image, Table, TableStyle
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch
from django.core.files.storage import FileSystemStorage
from reportlab.lib.styles import getSampleStyleSheet
from io import BytesIO
from reportlab.lib.pagesizes import A4, landscape
from reportlab.lib import colors


def getDivisionName(uid):
    mydivision = get_object_or_404(DivisionNames, division_user_id=uid)
    return mydivision.division_name


def createWLReport(hour, uid):
    wl_data = getTimeSlotReading(hour, uid)
    #wl_data=getTimeSlotReadingCumulative(hour,uid)
    """ Basic Setup for Report Lab   """
    pdf_buffer = BytesIO()
    doc = SimpleDocTemplate(pdf_buffer)
    doc.pagesize = landscape(A4)
    flowables = []  # overall flowables
    structural_summary = []  # for structural summary
    contract_summary = []
    sample_style_sheet = getSampleStyleSheet()
    # sample_style_sheet.list()
    """Creating Style for Paragraph  """
    custom_body_style = sample_style_sheet['Heading5']
    # custom_body_style.listAttrs()
    custom_body_style.spaceAfter = 0
    custom_body_style.spaceBefore = 0
    """Creating Header for WL Reporting"""
    division_name = getDivisionName(uid)
    h1 = Paragraph(division_name)
    flowables.append(h1)
    # flowables.append(Spacer(1,0.2*inch))

    """Creating Table widths"""
    col_widths = [60 * cm, 60 * cm, 60 * cm, 60 * cm]
    tableHeading = [["GAUGE", "RIVER", "LOCATION", "WL(m-pwd)"], ]
    for wl in wl_data:
        gauge = wl.gauge_name.gauge_code
        river = wl.gauge_name.river_name.river_name
        location = wl.gauge_name.gauge_station_name
        readings = wl.wlreading
        mycuurent_data = [gauge, river, location, readings]
        tableHeading.append(mycuurent_data)

        pass
    mydate = wl_data[0].reading_time
    mydate = mydate.replace(hour=6)
    reporting_time = mydate.strftime('%x')
    reporting_time = reporting_time + " at " + str(hour) + ":00 hours"
    h2 = Paragraph(reporting_time)
    flowables.append(h2)
    # t=Table(data=tableHeading,colWidths=col_widths,repeatRows=1)

    t = Table(tableHeading, 4 * [2 * inch],
              style=[('GRID', (0, 0), (-1, -1), 1.5, colors.green), ('ALIGN', (0, 0), (-1, -1), "CENTER")])
    t.hAlign = 'LEFT'
    flowables.append(t)

    doc.build(flowables)

    pdf_value = pdf_buffer.getvalue()
    pdf_buffer.close()
    myvalue = {'report': pdf_value, 'time': reporting_time}
    return myvalue
def createWLReport2(hour, uid):
    #wl_data = getTimeSlotReading(hour, uid)
    wl_data=getTimeSlotReadingCumulative(hour,uid)

    """ Basic Setup for Report Lab   """
    pdf_buffer = BytesIO()
    doc = SimpleDocTemplate(pdf_buffer)
    doc.pagesize = landscape(A4)
    flowables = []  # overall flowables
    structural_summary = []  # for structural summary
    contract_summary = []
    sample_style_sheet = getSampleStyleSheet()
    # sample_style_sheet.list()
    """Creating Style for Paragraph  """
    custom_body_style = sample_style_sheet['Heading5']
    # custom_body_style.listAttrs()
    custom_body_style.spaceAfter = 0
    custom_body_style.spaceBefore = 0
    """Creating Header for WL Reporting"""
    division_name = getDivisionName(uid)
    h1 = Paragraph(division_name)
    flowables.append(h1)
    # flowables.append(Spacer(1,0.2*inch))

    """Creating Table widths"""
    col_widths = [60 * cm, 60 * cm, 60 * cm, 60 * cm]
    tableHeading = [["GAUGE", "RIVER", "LOCATION", "WL(m-pwd)"], ]
    for wl in wl_data:
        gauge = wl.gauge_name.gauge_code
        river = wl.gauge_name.river_name.river_name
        location = wl.gauge_name.gauge_station_name
        readings = wl.wlreading
        mycuurent_data = [gauge, river, location, readings]
        tableHeading.append(mycuurent_data)

        pass
    mydate = wl_data[0].reading_time
    mydate = mydate.replace(hour=6)
    reporting_time = mydate.strftime('%x')
    reporting_time = reporting_time + " at " + str(hour) + ":00 hours"
    h2 = Paragraph(reporting_time)
    flowables.append(h2)
    # t=Table(data=tableHeading,colWidths=col_widths,repeatRows=1)

    t = Table(tableHeading, 4 * [2 * inch],
              style=[('GRID', (0, 0), (-1, -1), 1.5, colors.green), ('ALIGN', (0, 0), (-1, -1), "CENTER")])
    t.hAlign = 'LEFT'
    flowables.append(t)

    doc.build(flowables)

    pdf_value = pdf_buffer.getvalue()
    pdf_buffer.close()
    myvalue = {'report': pdf_value, 'time': reporting_time}
    return myvalue


from .models import communicationList


def addContactFormSave(form, uid):
    cname = form.cleaned_data['person_designation']
    cemail = form.cleaned_data['person_email']
    cmobile = form.cleaned_data['person_mobile']
    mycontact = communicationList(person_designation=cname, person_email=cemail, person_mobile=cmobile, added_by=uid)
    mycontact.save()


def getRecipients(uid):
    mycontacts = communicationList.objects.filter(added_by=uid)
    mymails = []
    for c in mycontacts:
        mymails.append(c.person_email)
    return mymails
