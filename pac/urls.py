from django.urls import path,re_path
from django.conf.urls import url
from . import views
urlpatterns=[
    url(r'^$',views.accounts_index,name='pac_index'),
    url('^dpp_allocation/(?P<pk>\d+)/update/$',views.update_DPP_Allocation,name='dpp_allocation_update'),
    url('^dpp_allocation/(?P<pk>\d+)/delete/$',views.delete_DPP_Allocation,name='dpp_allocation_delete'),
    url('^dpp_allocation/create/$', views.create_DPP_Allocation, name='dpp_allocation_create'),
    url(r'^input_budget_allocation', views.input_budget_allocation, name='budget_allocation'),
    url('^budget_allocation/(?P<pk>\d+)/update/$',views.update_Budget_Allocation,name='budget_allocation_update'),
    url('^budget_allocation/(?P<pk>\d+)/delete/$',views.delete_DPP_Allocation,name='budget_allocation_delete'),
    url('^budget_allocation/(?P<pk>\d+)/list/$',views.list_budget_item_sort_by_fy,name='budget_allocation_yearwise'),

    #Invoice Related URL
    url('^upload_invoice_image',views.Invoice_image_upload2,name='upload_invoice_image'),
    url('^invoice_index',views.invoice_list,name='invoice_index'),
    url('^invoice/(?P<pk>\d+)/update/$',views.Edit_invoice,name='update_invoice'),
    url('^invoice/(?P<pk>\d+)/doc/update/$',views.Edit_invoice_doc,name='update_invoice_doc'),
    url('^invoice/(?P<pk>\d+)/delete/$',views.Delete_invoice,name='delete_invoice'),
    url('^invoice/add/$',views.Add_invoice,name='create_invoice'),
    url('^invoice/add2/$',views.Add_invoice2,name='create_invoice2'),
    url('^invoice/(?P<pk>\d+)/yearwise/$',views.list_invoices_sort_by_fy,name='invoice_yearwise'),
    url('^invoice/sort/all/$',views.invoice_list_sort_by_all,name='invoice_sort'),

    #Expediture Related URL
    url('^expenditure/(?P<pk>\d+)/add/$',views.Add_Expenditure,name='create_expediture'),
    url('^expenditure/(?P<pk>\d+)/update/$',views.UpdateExpenditure,name='edit_expenditure'),
    url('^expenditure/(?P<pk>\d+)/delete/$',views.DeleteExpenditure,name='delete_expenditure'),
    url('^expenditure_index',views.expenditure_list,name='expenditure_index'),
    url('^expenditure/(?P<pk>\d+)/list/$',views.expenditure_list_sort_by_fy,name='expenditure_yearwise'),
    url('^expenditure/(?P<pk>\d+)/invoice/$',views.expenditure_list_sort_by_invoice,name='expenditure_invoicewise'),
    url('^expenditure/sort/all/$',views.expenditure_list_sort_by_all,name='expenditure_sort'),
    url('^expenditure/report/$',views.progressReport,name='progress_report'),
    #PD Dash Board for financial progress
    url('^financial/progress/category/$',views.dashboardCategory,name="dashboard_category"),
    url('^report/expenditure/monthly/$', views.MonthlyExpenditure, name="monthly_expenditure"),
    url('^report/expenditure/monthly/yearwise/$', views.MonthlyExpenditureYearwise, name="monthly_expenditure_yearwise"),
    # structure view related function
    path('list_contract_intervention2/', views.contractInterventionList, name="contract_intervention_list2"),
    path('blankmap', views.BlankMap, name="blank_map"),

    #Civil works related url



]
