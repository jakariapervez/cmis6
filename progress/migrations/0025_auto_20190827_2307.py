# Generated by Django 2.0.3 on 2019-08-27 17:07

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0024_auto_20190827_2137'),
    ]

    operations = [
        migrations.AddField(
            model_name='measurement',
            name='measurement_authority',
            field=models.DateField(blank=True, null=True),
        ),
        migrations.AddField(
            model_name='measurement',
            name='measurement_type',
            field=models.CharField(blank=True, choices=[('1', 'Peat Measurement'), ('2', 'Final Measurement')], default='1', max_length=10, null=True),
        ),
        migrations.AddField(
            model_name='valueofworkdone',
            name='measuremet_status',
            field=models.CharField(blank=True, choices=[('1', 'Peat Measurement'), ('2', 'Final_by_XEN'), ('3', 'Final_by_CSE'), ('4', 'Final_APproved_by_PD')], default='1', max_length=100, null=True),
        ),
    ]