# Generated by Django 3.1 on 2020-08-13 12:23

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0001_initial'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='dpp_intervention',
            name='location',
        ),
    ]
