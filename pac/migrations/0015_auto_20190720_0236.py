# Generated by Django 2.0.3 on 2019-07-19 20:36

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('pac', '0014_auto_20190717_0155'),
    ]

    operations = [
        migrations.AddField(
            model_name='invoice_details',
            name='FinancialYear',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='pac.FinancialYear'),
        ),
        migrations.AddField(
            model_name='invoice_details',
            name='Month',
            field=models.CharField(blank=True, max_length=2, null=True),
        ),
    ]
