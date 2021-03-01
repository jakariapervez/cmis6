from django.urls import path,re_path
from django.conf.urls import url
from . import views
urlpatterns=[
    url(r'^$',views.wl_index,name='wl_index'),
    url(r'^data_collect',views.data_collect_view,name='wl_data_collect'),
    url(r'^view_data',views.displayData,name='wl_data_display'),
    re_path(r'^view_data/gaugewise/$',views.displayDataGaugewise,name='wl_data_display_gaugewise'),
    url(r'^send_email',views.sendEmail,name='send_email'),
    url(r'^wl_logout',views.wl_Logiut,name='wl_logout'),
    path('gauge_data/',views.gaugeData,name='gauge_data'),
    path('gauge_data_edit/<int:pk>/',views.gaugeDataEdit,name='gauge_data_edit'),
    path('gauge_data_delete/<int:pk>',views.gaugeDataDelete,name='gauge_data_delete'),





]
