from django.urls import path,re_path
from django.conf.urls import url
from . import views
urlpatterns=[
    url('^$',views.list_DPP_Intervention,name='dpp_intervention_list'),
    url('^dpp_intervention/(?P<pk>\d+)/update/$',views.update_DPP_Intervention,name='dpp_intervention_update'),
    url('^dpp_intervention/(?P<pk>\d+)/delete/$',views.delete_DPP_Intervention,name='dpp_intervention_delete'),
    url('^dpp_intervention/create/$', views.create_DPP_Intervention, name='dpp_intervention_create'),
    url('^sort_by_haor/(?P<pk>\d+)/ajax/$',views.list_DPP_Intervention_sort_by_haor,name='DPP_Intervention_sort_by_haor'),
    url('^sort_by_haor/all/ajax/$',views.list_DPP_Intervention_sort_by_haor_all,name='DPP_Intervention_sort_by_haor_all'),
    url('^sort_by_contract/(?P<pk>\d+)/ajax/$', views.list_contract_Intervention_sort_by_contract,
        name='contract_Intervention_sort_by_contract'),
    url('^sort_by_contract/all/ajax/$', views.list_contract_Intervention_sort_by_contract_all,
        name='Contract_Intervention_sort_by_contract_all'),
    url(r'^uploads/form/$', views.model_form_upload, name='model_form_upload'),
    url(r'^uploads/construction_image/$', views.construction_image_upload, name='construction_image_upload'),
    url(r'^uploads/construction_image/(?P<pk>\d+)/$', views.construction_image_upload_by_contract, name='construction_image_upload_by_contract'),
    url(r'^home/$', views.home, name='home'),
    url(r'^home2/$', views.home2, name='home2'),
    url(r'^home2/update/(?P<pk>\d+)/ajax/$', views.home2_update_ajax, name='home2_update_ajax'),

    url(r'^list_contract_intervention/$',views.list_contract_Intervention,name='list_contract_intervetion'),
    url('^contract_intervention/create/$', views.add_contract_Intervention, name='contract_intervention_create'),
    url('^contract_intervention/(?P<pk>\d+)/update/$', views.update_contract_Intervention, name='contract_intervention_update'),
    url('^contract_intervention/(?P<pk>\d+)/delete/$', views.delete_contract_Intervention, name='contract_intervention_delete'),



    url(r'^progres_item/$',views.list_progress_item,name='list_progress_items'),
    url(r'^progres_item/create/$', views.create_progress_item, name='progress_item_create'),
    url('^progres_item/(?P<pk>\d+)/update/$', views.update_progress_item, name='progress_item_update'),
    url('^progres_item/(?P<pk>\d+)/delete/$', views.delete_progress_item, name='progress_item_delete'),
    url(r'^progress_item/progres_item/(?P<pk>\d+)/ajax/$',views.get_progress_item,name="get_progress_item"),
    url(r'^progress_item/contract_invention/(?P<pk>\d+)/ajax/$',views.get_contract_interventions,name="get_contract_intervention"),
    url(r'^update_progress_quantity/(?P<pk>\d+)/ajax/$',views.get_progress_report_items2,name='get_progress_report_item'),
    url(r'^update_progress_quantity', views.update_progrss_quantity,name='update_progress_quantity'),
    url(r'^contract/update_progress_quantity/(?P<pk>\d+)/$', views.update_progrss_quantity2,name='update_progress_quantity2'),
    url(r'^update_progress_quantity/input/(?P<pk>\d+)/', views.input_progrss_quantity, name='input_progress_quantity'),
    url(r'^input_progress_quantity/current/ajax/$', views.input_progress_quantity_ajax, name="input_progress_quantity_ajax"),
    #Report Generation URLS
    url(r'^report/$', views.progressReport, name='report'),
      #Dashboard G URLS
    url(r'^dashboard/$',views.dashBoard,name='dashboard'),
    path('dashboard/graph/<int:pk>/',views.dashBoard_Graph,name='dashboard_graph'),
    #path('dashboard/',views.dashBoard_Graph,name='dashboard_graph'),
    #view reports
    path('dashboard/reportevent/view/',views.viewReportEvent.as_view(),name='vierw_report_event'),
    path('dashboard/reportevent/create/',views.createReportEvent2.as_view(),name='create_report_event'),
    #path('dashboard/reportevent/view/',views.viewReportEvent,name='vierw_report_event'),
    path('dashboard/reportevent/edit/<int:pk>/',views.editReportEvent,name='edit_report_event'),
    path('dashboard/reportevent/edit/',views.editReportEvent2,name='edit_report_event2'),
    #url for qualitative progrss
    path ('qualitative_progress/',views.Qualitative_progress,name='qualitative_progress'),
    path ('qualitative_progress/update/<int:pk>',views.Qualitative_progress_update,name='qualitative_progress_update'),
    path('qualitative_progress/sort/',views.Qualitative_progress_sort,name='qualitative_progress_sort'),
    path('blankmap', views.BlankMap, name="blank_map"),
    path('map/haor/<int:pk>',views.HaorMap,name='haor_map'),
    #url for qualitative progrss
    path('ipc/addIPCquantity/',views.AddIPCQuantity,name="add_ipc_quantity")


    #A Temporary URL for viewing contract Interventioin

]
