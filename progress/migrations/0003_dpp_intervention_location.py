# Generated by Django 3.1 on 2020-08-13 12:25

import django.contrib.gis.db.models.fields
from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0002_remove_dpp_intervention_location'),
    ]

    operations = [
        migrations.AddField(
            model_name='dpp_intervention',
            name='location',
            field=django.contrib.gis.db.models.fields.PointField(blank=True, null=True, srid=4326),
        ),
    ]
