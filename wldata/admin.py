from django.contrib import admin
from import_export.admin import ImportExportModelAdmin
from .models import SMS_info,DivisionNames,River,GaugeReader,GaugeLocation,GaugeReading,ReportedGauge,communicationList# Register your models here.
@admin.register(SMS_info)
class SMS_info_Admin(ImportExportModelAdmin):
    pass
@admin.register(DivisionNames)
class DivisionNames_Admin(ImportExportModelAdmin):
    pass
@admin.register(River)
class GaugeAccessibility_Admin(ImportExportModelAdmin):
    pass
@admin.register(GaugeReader)
class GaugeAccessibility_Admin(ImportExportModelAdmin):
    pass
@admin.register(GaugeLocation)
class GaugeLocation_Admin(ImportExportModelAdmin):
    pass
@admin.register(GaugeReading)
class GaugeReading_Admin(ImportExportModelAdmin):
    pass
@admin.register(ReportedGauge)
class GaugeReading_Admin(ImportExportModelAdmin):
    pass
@admin.register(communicationList)
class GaugeReading_Admin(ImportExportModelAdmin):
    pass