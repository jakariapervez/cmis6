# Generated by Django 3.1 on 2020-08-13 12:06

import datetime
from django.conf import settings
import django.contrib.gis.db.models.fields
from django.db import migrations, models
import django.db.models.deletion
import django.utils.timezone
import phonenumber_field.modelfields


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
    ]

    operations = [
        migrations.CreateModel(
            name='BoQ',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('quantity', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('rate', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('amount', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
            ],
        ),
        migrations.CreateModel(
            name='ConstructionImage',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('description', models.CharField(blank=True, max_length=255, null=True)),
                ('image', models.ImageField(upload_to='structures/%Y/%m/%d/')),
                ('acquisition_date', models.DateTimeField(default=datetime.datetime.now)),
                ('uploaded_at', models.DateTimeField(auto_now_add=True)),
            ],
        ),
        migrations.CreateModel(
            name='Contract',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('package_short_name', models.CharField(default='xxx', max_length=200)),
                ('package_detail_name', models.CharField(default='xxx', max_length=1000)),
                ('contractor_short_name', models.CharField(default='xxx', max_length=200)),
                ('start_date', models.DateField(default=django.utils.timezone.now)),
                ('finish_date', models.DateField(default=datetime.datetime.now)),
                ('extended_date', models.DateField(blank=True, null=True)),
                ('contract_amount', models.DecimalField(blank=True, decimal_places=2, max_digits=13, null=True)),
                ('billed_amount', models.DecimalField(blank=True, decimal_places=2, max_digits=13, null=True)),
                ('estimated_amount', models.DecimalField(blank=True, decimal_places=2, max_digits=13, null=True)),
                ('physical_progress', models.DecimalField(blank=True, decimal_places=2, max_digits=6, null=True)),
                ('financial_progress', models.DecimalField(blank=True, decimal_places=2, max_digits=6, null=True)),
            ],
        ),
        migrations.CreateModel(
            name='Contract_Intervention',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('physical_weight', models.DecimalField(blank=True, decimal_places=6, default=0.1, max_digits=8)),
                ('financial_weight', models.DecimalField(blank=True, decimal_places=6, default=0.1, max_digits=8)),
            ],
        ),
        migrations.CreateModel(
            name='ContractComponent',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('component_name', models.CharField(choices=[('A', 'A'), ('B', 'B'), ('C', 'C'), ('D', 'D'), ('E', 'E'), ('F', 'F'), ('G', 'G'), ('H', 'H')], max_length=30)),
            ],
        ),
        migrations.CreateModel(
            name='Contractor',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('farm_name', models.CharField(max_length=200)),
                ('first_name', models.CharField(max_length=30)),
                ('last_name', models.CharField(max_length=30)),
                ('phone_number', phonenumber_field.modelfields.PhoneNumberField(blank=True, max_length=128, null=True, region=None)),
                ('email', models.EmailField(blank=True, max_length=254, null=True)),
                ('address', models.CharField(blank=True, max_length=250, null=True)),
                ('tradelicense', models.CharField(blank=True, max_length=10, null=True)),
                ('Vat_registration', models.CharField(blank=True, max_length=11, null=True)),
                ('TIN_No', models.CharField(blank=True, max_length=12, null=True)),
                ('egp_id', models.CharField(blank=True, max_length=14, null=True)),
                ('national_id', models.ImageField(blank=True, null=True, upload_to='')),
            ],
        ),
        migrations.CreateModel(
            name='District',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=50)),
            ],
        ),
        migrations.CreateModel(
            name='Division',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('division_name', models.CharField(max_length=100)),
                ('div_address', models.CharField(blank=True, max_length=250, null=True)),
                ('div_phone', phonenumber_field.modelfields.PhoneNumberField(blank=True, max_length=128, null=True, region=None)),
                ('district', models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.district')),
            ],
        ),
        migrations.CreateModel(
            name='Haor',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=100)),
                ('area', models.DecimalField(blank=True, decimal_places=2, max_digits=8, null=True)),
                ('project_type', models.CharField(choices=[('Old', 'Rehablitation'), ('New', 'New Haor')], max_length=50)),
                ('population', models.DecimalField(blank=True, decimal_places=0, default=0, max_digits=8, null=True)),
            ],
        ),
        migrations.CreateModel(
            name='LandAcquisitionData',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('laCaseNo', models.CharField(max_length=60)),
                ('amountOfLand', models.DecimalField(blank=True, decimal_places=3, default=0, max_digits=6, null=True)),
                ('personsInAwl', models.IntegerField(default=0)),
                ('compensationAmtInAwl', models.DecimalField(blank=True, decimal_places=3, max_digits=18, null=True)),
                ('personPaidCom', models.IntegerField(default=0)),
                ('division', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.division')),
            ],
        ),
        migrations.CreateModel(
            name='ReportEvent',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('reportCode', models.CharField(blank=True, max_length=100, null=True)),
                ('reportName', models.CharField(blank=True, max_length=250, null=True)),
                ('startingDate', models.DateField(verbose_name=django.utils.timezone.now)),
                ('finishDate', models.DateField(verbose_name=django.utils.timezone.now)),
                ('lastSubmissionDate', models.DateField(verbose_name=django.utils.timezone.now)),
                ('message', models.TextField(blank=True, null=True)),
                ('eventStatus', models.CharField(choices=[('Live', 'Live'), ('Dead', 'Dead')], default='Live', max_length=100)),
            ],
        ),
        migrations.CreateModel(
            name='Schedule',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('codeNo', models.CharField(blank=True, max_length=100, null=True)),
                ('itemDescription', models.TextField()),
                ('unit', models.CharField(blank=True, max_length=50, null=True)),
            ],
        ),
        migrations.CreateModel(
            name='WorkType',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('wtype', models.CharField(choices=[('IRINCN', 'Irigation Inlet Construction'), ('REGRR', 'Reinstallation/Repair of Regulator/causeway Rehab'), ('REGCN', 'Regulator Construction New Haor'), ('CASWCN', 'Causeway Connstruction New Haor'), ('BRIDGCN', 'Bridge Connstruction New Haor'), ('BOXSLCN', 'Box Sluice Constuction New'), ('EXKHLN', 'Khal Reexcavation New Haor'), ('EXRIVN', 'River Reexcavation New Haor'), ('EXKHLR', 'Khal Reexcavation Rehab Haor'), ('EXRIVR', 'River Reexcavation Rehab Haor'), ('EMBR', 'Full Embankment Repair Rehab Haor'), ('SEMBRN', 'Submersiblesible Embankment Repair New Haor'), ('SEMBCN', 'Submersiblesible Embankment Construction New Haor'), ('REGRN', 'Regulator  Repair New Haor'), ('EMBSPW', 'Embankment Slope Protection Work'), ('WMGOC', 'WMG Office Construction')], max_length=30)),
            ],
        ),
        migrations.CreateModel(
            name='ValueOfWorkDone',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('startDate', models.DateField(blank=True, null=True)),
                ('finishDate', models.DateField(blank=True, null=True)),
                ('status', models.CharField(blank=True, choices=[('OG', 'OG'), ('COMP', 'COMP'), ('TO_BE_STARTED', 'TO_BE_STARTED')], default='TO_BE_STARTED', max_length=100, null=True)),
                ('measuremet_status', models.CharField(blank=True, choices=[('1', 'Peat Measurement'), ('2', 'Final_by_XEN'), ('3', 'Final_by_CSE'), ('4', 'Final_APproved_by_PD')], default='1', max_length=100, null=True)),
                ('quantity', models.DecimalField(blank=True, decimal_places=3, default=0.0, max_digits=13, null=True)),
                ('boq_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.boq')),
            ],
        ),
        migrations.CreateModel(
            name='ReportSubmissionStatus',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('submissionDate', models.DateField(verbose_name=django.utils.timezone.now)),
                ('submissionStats', models.CharField(choices=[('Submitted', 'Submitted'), ('NotSubmitte', 'NotSubmitted')], default='NotSubmitted', max_length=100)),
                ('reportEvent', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.reportevent')),
                ('reportingPerson', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.CreateModel(
            name='Reportdocument',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('description', models.CharField(blank=True, max_length=255, null=True)),
                ('document', models.FileField(upload_to='ProgressReport/%Y/%m/%d/')),
                ('issuing_date', models.DateField(default=django.utils.timezone.now)),
                ('reportEvent_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.reportevent')),
                ('reportingPerson', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.CreateModel(
            name='qualitativeStatus',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('overall_status', models.CharField(choices=[('C', 'Completed'), ('OG', 'On Going'), ('P', 'Problamatic')], default='OG', max_length=100)),
                ('current_progress', models.DecimalField(blank=True, decimal_places=5, max_digits=6, null=True)),
                ('problems', models.CharField(choices=[('LA', 'Land Acquisition'), ('SR', 'Structure Relocated'), ('LP', "Local Peoplles's Obstruction"), ('RE', 'River Erosion'), ('GC', ' inappropriate Sub-surface Condition'), ('NP', 'None'), ('OT', 'Others')], default='NP', max_length=100)),
                ('value_of_work_done', models.DecimalField(blank=True, decimal_places=3, default=0, max_digits=7, null=True)),
                ('contract_ivt', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.contract_intervention')),
            ],
        ),
        migrations.CreateModel(
            name='ProgressItem',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('item_name', models.CharField(default='EW', max_length=200)),
                ('unit', models.CharField(max_length=10)),
                ('quantity', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('weight', models.DecimalField(blank=True, decimal_places=3, max_digits=4, null=True)),
                ('actualStartDate', models.DateField(blank=True, default=django.utils.timezone.now, null=True)),
                ('actualFinishDate', models.DateField(blank=True, default=django.utils.timezone.now, null=True)),
                ('plannedStartDate', models.DateField(blank=True, default=django.utils.timezone.now, null=True)),
                ('plannedFinishDate', models.DateField(blank=True, default=django.utils.timezone.now, null=True)),
                ('workSequenceNumber', models.IntegerField(default=0)),
                ('plannedDuration', models.IntegerField(default=0)),
                ('actualDuration', models.IntegerField(default=0)),
                ('executionStatus', models.CharField(choices=[('OG', 'OG'), ('COMP', 'COMP'), ('TO_BE_STARTED', 'TO_BE_STARTED'), ('HALTED', 'HALTED'), ('ABANDONED', 'ABANDONED')], default='TO_BE_STARTED', max_length=20)),
                ('actualQuantity', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('intervention_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.contract_intervention')),
            ],
        ),
        migrations.CreateModel(
            name='ProgresSchedule',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('date', models.DateField(default=datetime.datetime.now)),
                ('quantity', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('progress_item_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.progressitem')),
            ],
        ),
        migrations.CreateModel(
            name='Progress_Quantity',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('date', models.DateField(default=django.utils.timezone.now)),
                ('quantity', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('document_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.constructionimage')),
                ('progress_item_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.progressitem')),
                ('user_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.CreateModel(
            name='Measurement',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('measurement_date', models.DateField(blank=True, null=True)),
                ('measurement_authority', models.DateField(blank=True, null=True)),
                ('measurement_type', models.CharField(blank=True, choices=[('1', 'Peat Measurement'), ('2', 'Final Measurement')], default='1', max_length=10, null=True)),
                ('quantity', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('vwd', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.valueofworkdone')),
            ],
        ),
        migrations.CreateModel(
            name='LandAcquisitionStage',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('stepName', models.CharField(choices=[('0', 'Budget_Allocation'), ('1', 'Approval_From_MOWR'), ('2', 'Proposal_Submission'), ('3', 'DLAC_Meeting'), ('4', 'Notice_Under_Serctioin_4(1)'), ('5', 'Joint_List_Under_Sec_3(6)'), ('6', 'Approval_MOL'), ('7', 'Notice_Under_Sec_7'), ('8', 'Award_List_Under_Sec_8'), ('9', 'Fund_Transfer_DC'), ('10', 'Payment_Of_Compensation'), ('11', 'Possesioin_Of_Land')], default='0', max_length=50)),
                ('stepStaus', models.CharField(choices=[('OG', 'OG'), ('COMP', 'COMP'), ('TO_BE_STARTED', 'TO_BE_STARTED'), ('HALTED', 'HALTED'), ('ABANDONED', 'ABANDONED'), ('RFUSED', 'REFUSED')], default='TO_BE_STARTED', max_length=50)),
                ('dateOfCompletion', models.DateField(blank=True, null=True)),
                ('laCase', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.landacquisitiondata')),
            ],
        ),
        migrations.CreateModel(
            name='LADocuments',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('description', models.CharField(blank=True, max_length=255, null=True)),
                ('document', models.FileField(upload_to='LA/%Y/%m/%d/')),
                ('issuing_date', models.DateField(default=django.utils.timezone.now)),
                ('LandAcquisitionStage', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.landacquisitionstage')),
            ],
        ),
        migrations.CreateModel(
            name='DPP_Intervention',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=400)),
                ('start_chainage', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('finish_chainage', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('length', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('volume', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('vent_no', models.IntegerField(blank=True, null=True)),
                ('dpp_cost', models.DecimalField(blank=True, decimal_places=3, max_digits=13, null=True)),
                ('contract_status', models.CharField(blank=True, choices=[('HAVE_CONTRACT', 'HAVE_CONTRACT'), ('HAVE_NO_CONTRACT', 'HAVE_NO_CONTRACT')], default='HAVE_NO_CONTRACT', max_length=100, null=True)),
                ('work_status', models.CharField(blank=True, choices=[('OG', 'OG'), ('COMP', 'COMP'), ('TO_BE_STARTED', 'TO_BE_STARTED')], default='OG', max_length=100, null=True)),
                ('location', django.contrib.gis.db.models.fields.PointField(blank=True, null=True, srid=4326)),
                ('lines', django.contrib.gis.db.models.fields.LineStringField(blank=True, null=True, srid=4326)),
                ('haor_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.haor')),
                ('worktype_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.worktype')),
            ],
        ),
        migrations.CreateModel(
            name='Document',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('description', models.CharField(blank=True, max_length=255, null=True)),
                ('document', models.ImageField(upload_to='documents/%Y/%m/')),
                ('uploaded_at', models.DateTimeField(auto_now_add=True)),
                ('uploaded_by', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.AddField(
            model_name='contract_intervention',
            name='contract_component_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.contractcomponent'),
        ),
        migrations.AddField(
            model_name='contract_intervention',
            name='contract_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.contract'),
        ),
        migrations.AddField(
            model_name='contract_intervention',
            name='dpp_intervention_id',
            field=models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='progress.dpp_intervention'),
        ),
        migrations.AddField(
            model_name='contract',
            name='contractor_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.contractor'),
        ),
        migrations.AddField(
            model_name='contract',
            name='cse_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='consultant_engg', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='contract',
            name='division_id',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='progress.division'),
        ),
        migrations.AddField(
            model_name='contract',
            name='fse_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='field_engg', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='contract',
            name='partner_contractor1_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='partner_contractor1_id', to='progress.contractor'),
        ),
        migrations.AddField(
            model_name='contract',
            name='partner_contractor2_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='partner_contractor2_id', to='progress.contractor'),
        ),
        migrations.AddField(
            model_name='contract',
            name='sde_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='sde_id', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='contract',
            name='so_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='so_id', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='contract',
            name='xen_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='xen_id', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AddField(
            model_name='constructionimage',
            name='structure_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.contract_intervention'),
        ),
        migrations.AddField(
            model_name='constructionimage',
            name='uploaded_by',
            field=models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
        ),
        migrations.CreateModel(
            name='CalculationSheet',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('description', models.CharField(blank=True, max_length=255, null=True)),
                ('image', models.FileField(upload_to='measurements/structures/%Y/%m/%d/')),
                ('measurement_id', models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.contract_intervention')),
            ],
        ),
        migrations.AddField(
            model_name='boq',
            name='schedule_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.schedule'),
        ),
        migrations.AddField(
            model_name='boq',
            name='structure_id',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='progress.contract_intervention'),
        ),
    ]
