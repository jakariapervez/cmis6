import os
import pandas as pd
import datetime
import random
import math
from .models import GaugeLocation,GaugeReading
def generateWL(year,month,days,max_val,min_val,gauge_id):
    times=[]
    readings=[]


    for d in days:
        D1=datetime.datetime(year,month,d,6)
        times.append(D1)
        wl=float(random.randint(min_val,max_val)/100)
        readings.append(wl)
        myreading=GaugeReading(gauge_name=gauge_id,reading_time=D1,wlreading=wl)
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
        D4= datetime.datetime(year, month, d, 15)
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
    m30=[4,6,9,11]
    m31=[1,3,5,7,8,10,12]
    m=mybegining.month
    if m in m30:
        mydays=[i for i in range(1,31)]
        print("this month has 30 days")
    elif m in m31:
        mydays = [i for i in range(1, 32)]
        print("this month has 30 days")
    else:
        year=mybegining.year
        mymod=year%4
        if mymod==0:
            mydays = [i for i in range(1, 30)]
            print("this month has 29 days")
        else:
            mydays = [i for i in range(1, 29)]
            print("this month has 28 days")


    return mydays




def inputMonthlyWL(verbose=True):
    mypath=os.path.join(os.path.dirname(__file__),'data.txt')
    myfile=open(mypath,'r')
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    year = int(mytexts[1])
    print(year)
    myline=myfile.readline()
    print(myline)
    mytexts= myline.split(":")
    month=int(mytexts[1])
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
    max_val=math.ceil(max_val*100)
    print(max_val)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    min_val = float(mytexts[1].rstrip("\n"))
    min_val = math.ceil(min_val*100)
    print(min_val)
    mybegining=datetime.datetime(year,month,1)
    print(mybegining)
    day_in_month=daysforOneMonth(mybegining)
    print( day_in_month)
    mygauge_id=GaugeLocation.objects.filter(gauge_code=gauge)[0]
    print(mygauge_id)
    generateWL(year, month, day_in_month, max_val, min_val, mygauge_id)

    myfile.close()
def getTodaysData(gaugeid):
    mydate=datetime.datetime.now()
    myyear=mydate.year
    myday=mydate.day
    mymonth=mydate.month
    mydata=list(GaugeReading.objects.filter(reading_time__year=myyear,reading_time__month=mymonth,
                                            reading_time__day=myday,gauge_name=gaugeid).order_by('reading_time'))
    wl=[]

    for m in mydata:
        wl.append(float(m.wlreading))
    myvalue={"readings":mydata,"wl":wl}
    return myvalue
def getFiveDaysData(gaugeid):
    mydate=datetime.datetime.now()
    myyear=mydate.year
    myday=mydate.day
    mymonth=mydate.month
    substract_days=datetime.timedelta(days=4)
    stdate=mydate-substract_days
    stdate=stdate.replace(hour=6)
    fdate=datetime.datetime(myyear,mymonth,myday,18)
    mydata=list(GaugeReading.objects.filter(reading_time__gte=stdate,
                                            reading_time__lte=fdate,gauge_name=gaugeid).order_by('reading_time'))
    wl=[]
    years=[]
    months=[]
    days=[]
    hours=[]
    for m in mydata:
        wl.append(float(m.wlreading))
        years.append(m.reading_time.year)
        months.append(m.reading_time.month)
        days.append(m.reading_time.day)
        hours.append(m.reading_time.hour)
    myvalue={"wl":wl,"years":years,"days":days,"hours":hours,"months":months}
    return myvalue
from .models import GaugeReading,DivisionNames
from django.shortcuts import get_object_or_404
def getTimeSlotReading2(hour,user_id):
    mydivision=get_object_or_404(DivisionNames,division_user_id=user_id)
    mydate=datetime.datetime.now()
    myyear=mydate.year
    mymonth = mydate.month
    myday=mydate.day
    myquery_date=datetime.datetime(myyear,mymonth,myday)
    print(myquery_date)
    if hour ==6:
        stdate=myquery_date.copy(deep=True)
        fdate=myquery_date.copy(deep=True)
        fdate=fdate.replace(hour=6)
    elif hour==9:
        stdate = datetime.datetime(year=myyear, month=mymonth, day=myday,hour=9)
        fdate = datetime.datetime(myyear, mymonth, myday, 11)
    elif hour==12:
        stdate = datetime.datetime(myyear, mymonth, myday, 12)
        fdate = datetime.datetime(myyear, mymonth, myday, 14)
    elif hour==15:
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
    mydata = list(GaugeReading.objects.filter(reading_time__gte=stdate,reading_time__lte=fdate,))
    return mydata
def getTimeSlotReading(reading_hour,user_id):
    reading_hour=int(reading_hour)
    mydate = datetime.datetime.now()
    myyear = mydate.year
    myday = mydate.day
    mymonth = mydate.month
    if reading_hour ==6:
        h1=6
        h2=8
    elif reading_hour ==9:
        h1 = 9
        h2 = 11
    elif reading_hour ==12:
        h1 = 12
        h2 = 14
    elif reading_hour ==15:
        h1 = 15
        h2 = 17
    elif reading_hour ==18:
        h1 = 18
        h2 = 20
    print("h1={} h2={}".format(h1,h2))
    mydata = list(GaugeReading.objects.filter(reading_time__year=myyear, reading_time__month=mymonth,
                                              reading_time__day=myday,).order_by('reading_time'))
    filtered_reading = []
    for d in mydata:
        gauge_user_id=d.gauge_name.division_name.division_user_id.pk
        print("gauge_user_id={}".format(gauge_user_id))
        if h1 <= d.reading_time.hour <= h2:
            print("found")
            if gauge_user_id==user_id:
                filtered_reading.append(d)



    #myvalue = {"readings": mydata, "wl": wl}
    return filtered_reading





