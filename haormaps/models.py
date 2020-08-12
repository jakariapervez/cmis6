# from django.db import models
from django.contrib.gis.db import models


# Create your models here.
from progress.models import DPP_Intervention
class pointsLocation(models.Model):
    name = models.CharField(max_length=200, null=True, blank=True)
    structure_id = models.ForeignKey(DPP_Intervention, on_delete=models.CASCADE, null=True, blank=True)
    location = models.PointField(null=True, blank=True)
    #objects=models.GeoManager()
