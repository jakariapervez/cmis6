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