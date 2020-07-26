# Generated by Django 2.0.3 on 2019-03-19 16:43

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('progress', '0017_auto_20190317_0021'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='contract_intervention',
            name='consultant_eng_id',
        ),
        migrations.RemoveField(
            model_name='contract_intervention',
            name='exen_id',
        ),
        migrations.RemoveField(
            model_name='contract_intervention',
            name='field_eng_id',
        ),
        migrations.RemoveField(
            model_name='contract_intervention',
            name='sde_id',
        ),
        migrations.RemoveField(
            model_name='contract_intervention',
            name='site_eng_id',
        ),
        migrations.RemoveField(
            model_name='contract_intervention',
            name='so_id',
        ),
        migrations.AddField(
            model_name='contract',
            name='cse_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='consultant_engg', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='contract',
            name='fse_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='field_engg', to=settings.AUTH_USER_MODEL),
        ),
    ]
