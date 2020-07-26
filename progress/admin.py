from django.contrib import admin
from import_export.admin import ImportExportModelAdmin
# Register your models here.
from .models import District,Division
admin.site.register(District)
@admin.register(Division)
class Division_Admin(ImportExportModelAdmin):
    pass
from .models import Haor,WorkType,ContractComponent,DPP_Intervention

admin.site.register(Haor)
"""  
class Haor_Admin(ImportExportModelAdmin):
    pass
"""
@admin.register(WorkType)
class WorkType_Admin(ImportExportModelAdmin):
    pass
admin.site.register(ContractComponent)
@admin.register(DPP_Intervention)
class DPP_Intervention_Admin(ImportExportModelAdmin):
    pass
from .models import Contractor,Contract
@admin.register(Contractor)
class Contractor_Admin(ImportExportModelAdmin):
    pass
@admin.register(Contract)
class Contract_Admin(ImportExportModelAdmin):
    pass
from .models import Contract_Intervention
@admin.register(Contract_Intervention)
class Contrtact_Intervention_Admin(ImportExportModelAdmin):
    pass
from .models import Schedule,BoQ,ValueOfWorkDone,Measurement
@admin.register(Schedule)
class Schedule_Admin(ImportExportModelAdmin):
    pass
@admin.register(BoQ)
class BoQ_Admin(ImportExportModelAdmin):
    pass
@admin.register(ValueOfWorkDone)
class ValueOfWorkDone_Admin(ImportExportModelAdmin):
    pass
@admin.register(Measurement)
class Measurement_Admin(ImportExportModelAdmin):
    pass
from .models import ProgressItem,ConstructionImage,Progress_Quantity
@admin.register(Progress_Quantity)
class Progress_Quantity_Admin(ImportExportModelAdmin):
    pass
#admin.site.register(Progress_Quantity)

admin.site.register(ConstructionImage)
admin.site.register(ProgressItem)
"""          
class ProgressItem_Admin(ImportExportModelAdmin):
    pass

"""
from .models import LandAcquisitionData,LandAcquisitionStage,LADocuments
@admin.register(LandAcquisitionData)
class LandAcquisitionData_Admin(ImportExportModelAdmin):
    pass

@admin.register(LandAcquisitionStage)
class LandAcquisitionStage_Admin(ImportExportModelAdmin):
    pass
admin.site.register(LADocuments)
from .models import ReportEvent,ReportSubmissionStatus,Reportdocument
@admin.register(ReportEvent)
class ReportEvent_admin(ImportExportModelAdmin):
    pass
@admin.register(ReportSubmissionStatus)
class ReportSubmissionStatus_Admin(ImportExportModelAdmin):
    pass
admin.site.register(Reportdocument)
