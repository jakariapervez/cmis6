from django.contrib import admin
from django.contrib.gis.admin import OSMGeoAdmin
from .models import pointsLocation

from django.contrib.gis import admin


admin.site.register(pointsLocation, admin.GeoModelAdmin)