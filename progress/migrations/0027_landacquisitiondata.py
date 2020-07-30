# Generated by Django 2.0.3 on 2019-08-31 06:29

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0026_auto_20190831_1213'),
    ]

    operations = [
        migrations.CreateModel(
            name='LandAcquisitionData',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('laCaseNo', models.IntegerField()),
                ('amountOfLand', models.DecimalField(blank=True, decimal_places=3, max_digits=6, null=True)),
                ('personsInAwl', models.IntegerField()),
                ('compensationAmtInAwl', models.DecimalField(blank=True, decimal_places=3, max_digits=18, null=True)),
                ('personPaidCom', models.IntegerField()),
                ('division', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.Division')),
            ],
        ),
    ]