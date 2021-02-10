from django.urls import path,re_path
from django.conf.urls import url
from . import views
urlpatterns=[
    url(r'^$',views.wl_index,name='wl_index'),
    url(r'^data_collect',views.data_collect_view,name='wl_data_collect'),



]
