# Generated by Django 3.1 on 2021-03-02 10:49

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('wldata', '0008_divisionnames_division_user_id'),
    ]

    operations = [
        migrations.AddField(
            model_name='gaugelocation',
            name='reported_gauge',
            field=models.BooleanField(blank=True, default=True, null=True),
        ),
    ]
