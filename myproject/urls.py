from django.conf.urls import url
from django.contrib import admin
from django.contrib.auth import views as auth_views

from accounts import views as accounts_views
from boards import views
from progress import views as progress_view
from django.urls import include, path, re_path
from django.views.generic import RedirectView

urlpatterns = [
    url(r'^$', progress_view.siteHome, name='sitehome'),
    # url(r'^$',auth_views.LoginView.as_view(template_name='login.html'), name='login'),
    # url(r'^$',progress_view.dashBoard,name="sitehome"),
    url(r'^signup/$', accounts_views.signup, name='signup'),
    url(r'^edit/profile/', accounts_views.editpProfile, name='edit_profile'),
    url(r'^login/$', auth_views.LoginView.as_view(template_name='login.html'), name='login'),
    url(r'^logout/$', auth_views.LogoutView.as_view(), name='logout'),

    path('password_reset/',
         auth_views.PasswordResetView.as_view(
             template_name='password_reset.html',
             email_template_name='password_reset_email.html',
             subject_template_name='password_reset_subject.txt'
         ),
         name='password_reset'),
    path('password_reset/done/',
         auth_views.PasswordResetDoneView.as_view(template_name='password_reset_done.html'),
         name='password_reset_done'),
    path('reset/<uidb64>/<token>/',
         auth_views.PasswordResetConfirmView.as_view(template_name='password_reset_confirm.html'),
         name='password_reset_confirm'),
    path('password_reset/complete/',
         auth_views.PasswordResetCompleteView.as_view(template_name='password_reset_complete.html'),
         name='password_reset_complete'),

    path('password_change/', auth_views.PasswordChangeView.as_view(template_name='password_change.html'),
         name='password_change'),
    path('password_change/done/',
         auth_views.PasswordChangeDoneView.as_view(template_name='password_change_done.html'),
         name='password_change_done'),

    url(r'^boards/(?P<pk>\d+)/$', views.board_topics, name='board_topics'),
    url(r'^boards/(?P<pk>\d+)/new/$', views.new_topic, name='new_topic'),
    url(r'^progress$/', include('progress.urls')),
    url(r'^admin/', admin.site.urls),
    url(r'^favicon\.ico$', RedirectView.as_view(url='/static/images/cmis.Icon')),
]
urlpatterns += [
    path('progress/', include('progress.urls')),
]
urlpatterns += [
    path('pac/', include('pac.urls')),
]
urlpatterns += [
    path('wldata/', include('wldata.urls')),
]
from django.conf import settings
from django.conf.urls.static import static

urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
urlpatterns += static(settings.STATIC_URL, document_root=settings.STATIC_ROOT)
