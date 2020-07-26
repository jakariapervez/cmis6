from django.contrib import admin
from import_export.admin import ImportExportModelAdmin
# Register your models here.
from .models import Dpp_allocation,Budget_allocation, Expenditure_details,FinancialYear,Invoice_details,InvoiceImage,InvoiceSupporting
admin.site.register(InvoiceImage)
admin.site.register(Invoice_details)
admin.site.register(InvoiceSupporting)
@admin.register(Dpp_allocation)
class Dpp_allocation_Admin(ImportExportModelAdmin):
    pass
@admin.register(Budget_allocation)
class Budget_allocation_Admin(ImportExportModelAdmin):
    pass
@admin.register(Expenditure_details)
class Expenditure_details_Admin(ImportExportModelAdmin):
    pass

@admin.register(FinancialYear)
class FinancialYear_Admin(ImportExportModelAdmin):
    pass