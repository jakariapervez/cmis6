# Generated by Django 2.0.3 on 2019-03-19 18:01

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0018_auto_20190319_2243'),
    ]

    operations = [
        migrations.AlterField(
            model_name='contract',
            name='contractor_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.Contractor'),
        ),
    ]