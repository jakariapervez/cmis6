# Generated by Django 3.0.8 on 2020-08-06 10:46

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0037_auto_20200805_2312'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='progressitem',
            name='finishdate',
        ),
        migrations.RemoveField(
            model_name='progressitem',
            name='startdate',
        ),
    ]