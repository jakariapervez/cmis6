from django.db import models

# Create your models here.
from django.db import models
from django.utils import timezone
from datetime import datetime
# Create your models here.

class SMS_info(models.Model):
    datetime = models.DateTimeField(auto_now=True)
    smsbody=models.CharField(max_length=400,null=True,blank=True)
    mobile_no=models.CharField(max_length=20,null=True,blank=True)


    def __str__(self):
        return str(self.mobile_no)+":"+self.smsbody+":"+self.mobile_no
class GaugeLocation(models.Model):
    gauge_name=models.CharField(max_length=50,null=True,blank=True)
    river_name=models.CharField(max_length=100,null=True,blank=True)
    latitude=models.DecimalField(max_digits=10,decimal_places=6)
    longitude= models.DecimalField(max_digits=10, decimal_places=6)

class DivisionNames(models.Model):
    division_name=models.CharField(max_length=300,null=True,blank=True)
    division_short_code=models.CharField(max_length=40,null=True,blank=True)

class GaugeAccessibility(models.Model):
    gid=models.ForeignKey(GaugeLocation,on_delete=models.CASCADE, null=True, blank=True)
    divid=models.ForeignKey(DivisionNames,on_delete=models.CASCADE, null=True, blank=True)
class GaugeReading(models.Model):
    gauge_name=models.ForeignKey(GaugeLocation,on_delete=models.CASCADE, null=True, blank=True)
    datetime=models.DateTimeField(auto_now=True)
    wlreading=models.DecimalField(max_digits=10,decimal_places=3)

