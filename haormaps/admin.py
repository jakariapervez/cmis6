from django.contrib import admin
from django.contrib.gis.admin import GeoModelAdmin
from .models import pointsLocation
from django.contrib.gis.admin import OSMGeoAdmin





@admin.site.register(pointsLocation)
class pointsLocationAdmin(GeoModelAdmin):
    list_display = ['name', 'location']
