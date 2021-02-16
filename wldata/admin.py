from django.contrib import admin
from import_export.admin import ImportExportModelAdmin
from .models import SMS_info,GaugeLocation,GaugeReading,GaugeAccessibility,DivisionNames
# Register your models here.
@admin.register(SMS_info)
class SMS_info_Admin(ImportExportModelAdmin):
    pass



@admin.register(GaugeLocation)
class GaugeLocation_Admin(ImportExportModelAdmin):
    pass
@admin.register(GaugeReading)
class GaugeReading_Admin(ImportExportModelAdmin):
    pass
@admin.register(DivisionNames)
class DivisionNames_Admin(ImportExportModelAdmin):
    pass
@admin.register(GaugeAccessibility)
class GaugeAccessibility_Admin(ImportExportModelAdmin):
    pass
