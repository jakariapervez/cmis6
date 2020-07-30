# Generated by Django 2.0.3 on 2019-03-09 13:49

import datetime
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('progress', '0015_auto_20190309_1907'),
    ]

    operations = [
        migrations.AddField(
            model_name='constructionimage',
            name='acquisition_date',
            field=models.DateTimeField(default=datetime.datetime.now),
        ),
        migrations.AlterField(
            model_name='constructionimage',
            name='image',
            field=models.ImageField(upload_to='structures/%Y/%m/%d/'),
        ),
    ]