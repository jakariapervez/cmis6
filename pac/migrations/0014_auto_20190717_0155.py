# Generated by Django 2.0.3 on 2019-07-16 19:55

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('pac', '0013_auto_20190717_0151'),
    ]

    operations = [
        migrations.RenameField(
            model_name='budget_allocation',
            old_name='Dpp_allocation_id',
            new_name='Dpp_allocation',
        ),
    ]
