# Generated by Django 2.2.5 on 2019-10-05 18:26

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0033_auto_20191005_0634'),
    ]

    operations = [
        migrations.AlterField(
            model_name='progressitem',
            name='intervention_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.Contract_Intervention'),
        ),
    ]