# Generated by Django 3.1 on 2020-08-13 13:14

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0004_auto_20200813_1912'),
    ]

    operations = [
        migrations.RenameField(
            model_name='contract_intervention',
            old_name='dpp_cost',
            new_name='contract_value',
        ),
    ]
