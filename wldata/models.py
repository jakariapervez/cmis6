from django.db import models

# Create your models here.
from django.db import models
from django.utils import timezone
from datetime import datetime
from django.conf import settings
# Create your models here.

class SMS_info(models.Model):
    datetime = models.DateTimeField(auto_now=True)
    smsbody=models.CharField(max_length=400,null=True,blank=True)
    mobile_no=models.CharField(max_length=20,null=True,blank=True)


    def __str__(self):
        return str(self.mobile_no)+":"+self.smsbody+":"+self.mobile_no



"""  
class GaugeAccessibility(models.Model):
    gid=models.ForeignKey(GaugeLocation,on_delete=models.CASCADE, null=True, blank=True)
    divid=models.ForeignKey(DivisionNames,on_delete=models.CASCADE, null=True, blank=True)
 """
class DivisionNames(models.Model):
    division_name=models.CharField(max_length=300,null=True,blank=True)
    division_short_code=models.CharField(max_length=40,null=True,blank=True)
    division_user_id=models.ForeignKey(settings.AUTH_USER_MODEL,on_delete=models.CASCADE,null=True,blank=True)
    def __str__(self):
        return str(self.division_short_code)
class River(models.Model):
    river_no=models.CharField(max_length=30,null=True,blank=True)
    river_name=models.CharField(max_length=200,null=True,blank=True)
    def __str__(self):
        return str(self.river_no)+"_"+self.river_name
class GaugeReader(models.Model):
    reader_name=models.CharField(max_length=200,null=True,blank=True)
    mobile_no=models.CharField(max_length=25,null=True,blank=True)
    def __str__(self):
        return str(self.reader_name)+"_"+str(self.mobile_no)
class GaugeLocation(models.Model):
    gauge_code=models.CharField(max_length=25,null=True,blank=True)
    gauge_station_name = models.CharField(max_length=100, null=True, blank=True)
    gauge_upazila= models.CharField(max_length=100, null=True, blank=True)
    gauge_district= models.CharField(max_length=100, null=True, blank=True)
    latitude=models.DecimalField(max_digits=14,decimal_places=6)
    longitude= models.DecimalField(max_digits=14, decimal_places=6)
    reader=models.ForeignKey(GaugeReader,on_delete=models.CASCADE, null=True, blank=True)
    danger_level=models.DecimalField(max_digits=6, decimal_places=2)
    river_name=models.ForeignKey(River,on_delete=models.CASCADE, null=True, blank=True)
    division_name=models.ForeignKey(DivisionNames,on_delete=models.CASCADE, null=True, blank=True)

    def __str__(self):
        return str(self.gauge_code)+"_"+str(self.river_name)+"_"+str(self.gauge_station_name)





class GaugeReading(models.Model):
    gauge_name=models.ForeignKey(GaugeLocation,on_delete=models.CASCADE, null=True, blank=True)
    reading_time=models.DateTimeField(null=True,blank=True)
    wlreading=models.DecimalField(max_digits=10,decimal_places=3)

    def __str__(self):
        return str(self.gauge_name)+"_"+str(self. reading_time)+"_"+str(self.wlreading)

class ReportedGauge(models.Model):
    gauge_name=models.ForeignKey(GaugeLocation,on_delete=models.CASCADE,null=True,blank=True)
    division_name=models.ForeignKey(DivisionNames,on_delete=models.CASCADE,null=True,blank=True)
class communicationList(models.Model):
    person_designation = models.CharField(max_length=200, null=True, blank=True)
    person_email = models.CharField(max_length=200, null=True, blank=True)
    person_mobile= models.CharField(max_length=200, null=True, blank=True)



