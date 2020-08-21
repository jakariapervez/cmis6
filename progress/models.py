from __future__ import unicode_literals




from django.contrib.gis.db.models import PointField,LineStringField,PolygonField,MultiPolygonField
from django.contrib.gis.db import models as gismodel
from django.db import models
from phonenumber_field.modelfields import PhoneNumberField
from datetime import datetime
from django.urls import reverse
from django.conf import settings
from django.utils import timezone

class District(models.Model):
    name=models.CharField(max_length=50)
    def __str__(self):
        return f' {self.name}'

class Division(models.Model):
    division_name=models.CharField(max_length=100)
    district=models.ForeignKey('District',on_delete=models.SET_NULL,null=True)
    div_address=models.CharField(max_length=250,null=True,blank=True)
    div_phone=PhoneNumberField(null=True,blank=True)
    #address=AddressField()

    def __str__(self):
        return f' {self.division_name} {self.div_address}'
class Contractor(models.Model):
    farm_name=models.CharField(max_length=200)
    first_name = models.CharField(max_length=30)
    last_name = models.CharField(max_length=30)
    phone_number = PhoneNumberField(blank=True,null=True)
    email = models.EmailField(blank=True,null=True)
    address=models.CharField(max_length=250,null=True,blank=True)
    tradelicense=models.CharField(max_length=10,blank=True,null=True)
    Vat_registration=models.CharField(max_length=11,blank=True,null=True)
    TIN_No=models.CharField(max_length=12,blank=True,null=True)
    egp_id=models.CharField(max_length=14,blank=True,null=True)
    national_id=models.ImageField(null=True,blank=True)
    def __str__(self):
        return f' {self.farm_name} '
class Haor(models.Model):
    Old ='Old'
    New= 'New'
    Types = (
        (Old, 'Rehablitation'),
        (New, 'New Haor'),    )

    name=gismodel.CharField(max_length=400)
    area=gismodel.DecimalField(null=True,blank=True,decimal_places=2,max_digits=8)
    project_type=gismodel.CharField(max_length=50,choices=Types)
    population=gismodel.DecimalField(max_digits=8,decimal_places=0,blank=True,null=True,default=0)
    boundary=gismodel.MultiPolygonField(null=True,blank=True)
    centroid=gismodel.PointField(null=True,blank=True)

    def __str__(self):
        return f' {self.name},{self.project_type},'
class WorkType(models.Model):
    """
    Types=[('EMBRR','Fulll Embnakment Rehab-Rehab Haor'),('SUBEMBCN', 'Submersible,Embankment Construction New-Haor'),
           ('SUBEMBRN', 'Submersible,Embankment Rehablitation New-Haor')
        ,('EXKHLN','Khal Rexcavation New Haor'),('EXRIVN','River Excavtion New Haor'),
           ('REGCN','Regulator Constyruction New Haor'),
           ('CASWCN','Causeway Connstruction New Haor'),
           ('IRINC','Irigation Inlet Construction'),('WMGO','WMG Office'),
           ('BOXSLCN','Box Sluice Constuction New'),
           ('EXKHLR', 'Khal Rexcavation Rehablitation Haor'), ('EXRIVR', 'River Excavtion Rehab Haor')
           ]
    """
    Types=[('IRINCN','Irigation Inlet Construction'),('REGRR','Reinstallation/Repair of Regulator/causeway Rehab'),
           ('REGCN','Regulator Construction New Haor'),('CASWCN','Causeway Connstruction New Haor'),
           ('BRIDGCN','Bridge Connstruction New Haor'),('BOXSLCN','Box Sluice Constuction New'),
           ('EXKHLN','Khal Reexcavation New Haor'),('EXRIVN','River Reexcavation New Haor'),
           ('EXKHLR','Khal Reexcavation Rehab Haor'),('EXRIVR','River Reexcavation Rehab Haor'),
           ('EMBR','Full Embankment Repair Rehab Haor'),('SEMBRN','Submersiblesible Embankment Repair New Haor'),
           ('SEMBCN','Submersiblesible Embankment Construction New Haor'),('REGRN','Regulator  Repair New Haor'),
           ('EMBSPW','Embankment Slope Protection Work'),('WMGOC','WMG Office Construction')]
    wtype=models.CharField(max_length=30,choices=Types)
    def __str__(self):
        return f' {self.wtype}'
# Create your models here.
class ContractComponent(models.Model):
    Types=[('A','A'),('B', 'B'),('C','C'),('D','D'),('E','E'),('F','F'),
           ('G','G'),('H','H'),]
    component_name=models.CharField(max_length=30,choices=Types)
    def __str__(self):
        return f' {self.component_name}'
# Create your models here.

class Contract(models.Model):
    #Package name
    package_short_name=models.CharField(max_length=200,default='xxx')
    package_detail_name=models.CharField(max_length=1000,default='xxx')
    contractor_short_name=models.CharField(max_length=200,default='xxx')
    #parties
    division_id=models.ForeignKey(Division,on_delete=models.SET_NULL,null=True)
    contractor_id=models.ForeignKey(Contractor,on_delete=models.SET_NULL,null=True,blank=True)
    partner_contractor1_id=models.ForeignKey(Contractor,on_delete=models.SET_NULL,null=True,related_name='partner_contractor1_id',blank=True)
    partner_contractor2_id = models.ForeignKey(Contractor, on_delete=models.SET_NULL, null=True,related_name='partner_contractor2_id', blank=True)
    #dates
    start_date=models.DateField(default=timezone.now)
    finish_date=models.DateField(default=datetime.now)
    extended_date=models.DateField(null=True,blank=True)
    #contract amount
    contract_amount=models.DecimalField(null=True,blank=True,decimal_places=2,max_digits=13)
    billed_amount = models.DecimalField(null=True, blank=True,decimal_places=2,max_digits=13)
    estimated_amount = models.DecimalField(null=True, blank=True,decimal_places=2,max_digits=13)
    #progress
    physical_progress=models.DecimalField(null=True,blank=True,decimal_places=2,max_digits=6)
    financial_progress = models.DecimalField(null=True, blank=True,decimal_places=2,max_digits=6)
    #reporting authority
    #xen_id=models.ForeignKey(Personal,on_delete=models.SET_NULL,null=True,related_name='issuer_id',blank=True)
    xen_id = models.ForeignKey(settings.AUTH_USER_MODEL,on_delete=models.SET_NULL, null=True, related_name='xen_id', blank=True)
    sde_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True, related_name='sde_id',
                               blank=True)
    so_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True, related_name='so_id',
                               blank=True)
    fse_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True,
                                     related_name='field_engg', blank=True)
    cse_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True,
                                          related_name='consultant_engg', blank=True)
    #checker_id = models.ForeignKey(Personal, on_delete=models.SET_NULL, null=True, related_name='checker_id',blank=True)
    #approver_id = models.ForeignKey(Personal, on_delete=models.SET_NULL, null=True, related_name='approver_id',blank=True)

    def __str__(self):
        return f'{self.package_short_name}'

    def get_absolute_url(self):
        """Returns the url to access a particular book instance."""
        #return reverse('contract_detail', args=[str(self.id)])
        return reverse('contract_detail',args=[str(self.package_id)])

class DPP_Intervention(models.Model):
    contract_status_choices = [("HAVE_CONTRACT", "HAVE_CONTRACT"), ("HAVE_NO_CONTRACT", "HAVE_NO_CONTRACT")]
    work_status_choices = [("OG", "OG"), ("COMP", "COMP"), ("TO_BE_STARTED", "TO_BE_STARTED")]
    haor_id = models.ForeignKey(Haor, on_delete=models.SET_NULL, null=True, blank=True)
    name = models.CharField(max_length=400)
    start_chainage = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    finish_chainage = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    length = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    volume = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    vent_no = models.IntegerField(null=True, blank=True)
    dpp_cost=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    worktype_id = models.ForeignKey(WorkType, on_delete=models.SET_NULL, null=True, blank=True)
    contract_status = models.CharField(max_length=100, choices=contract_status_choices, null=True, blank=True,
                                       default= "HAVE_NO_CONTRACT")
    work_status = models.CharField(max_length=100, choices=work_status_choices, blank=True, null=True,
                                   default= "OG")
    location=PointField(null=True,blank=True)
    lines=LineStringField(null=True,blank=True)


    def __str__(self):
        return f'{self.name}'+ ' in ' +f'{self.haor_id.name}'


class Contract_Intervention(models.Model):
    contract_id=models.ForeignKey(Contract,on_delete=models.CASCADE,null=True,blank=True)
    dpp_intervention_id=models.ForeignKey(DPP_Intervention,on_delete=models.CASCADE)
    contract_component_id = models.ForeignKey(ContractComponent, on_delete=models.SET_NULL, null=True, blank=True)
    physical_weight = models.DecimalField(blank=True,default=0.10,max_digits=8,decimal_places=6)
    financial_weight = models.DecimalField(blank=True, default=0.10,max_digits=8,decimal_places=6)
    contract_value=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)

    """       
    so_id=models.ForeignKey(settings.AUTH_USER_MODEL,on_delete=models.SET_NULL, null=True,
                                  related_name='so', blank=True)
    sde_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True,
                              related_name='sde', blank=True)
    exen_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True,
                              related_name='exen', blank=True)
    site_eng_id=models.ForeignKey(settings.AUTH_USER_MODEL,on_delete=models.SET_NULL, null=True,related_name='site_engg', blank=True)
    field_eng_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True, related_name='field_engg', blank=True)
    consultant_eng_id = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True,related_name='consultant_engg', blank=True)
     """

    def __str__(self):
        return f'{self.dpp_intervention_id.name}'


"""Use camelCase Variable Name    """


from django.contrib.auth.models import User

class Document(models.Model):
    description = models.CharField(max_length=255, blank=True,null=True)
    document = models.ImageField(upload_to='documents/%Y/%m/')
    uploaded_at = models.DateTimeField(auto_now_add=True)
    uploaded_by=models.ForeignKey(User,on_delete=models.CASCADE)

class ConstructionImage(models.Model):
    description = models.CharField(max_length=255, blank=True, null=True)
    image = models.ImageField(upload_to='structures/%Y/%m/%d/')
    acquisition_date=models.DateTimeField(default=datetime.now)
    uploaded_at = models.DateTimeField(auto_now_add=True)
    uploaded_by = models.ForeignKey(User, on_delete=models.CASCADE)
    structure_id = models.ForeignKey(Contract_Intervention, on_delete=models.CASCADE, null=True, blank=True)





class ProgressItem(models.Model):
    work_status_choices=[("OG", "OG"), ("COMP", "COMP"), ("TO_BE_STARTED", "TO_BE_STARTED"),("HALTED","HALTED"),("ABANDONED","ABANDONED")]
    intervention_id=models.ForeignKey(Contract_Intervention,on_delete=models.CASCADE,null=True,blank=True)
    item_name=models.CharField(max_length=200,default="EW")
    unit=models.CharField(max_length=10)
    quantity=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)#Quantity planned to be executed
    weight=models.DecimalField(null=True, blank=True,max_digits=4,decimal_places=3)
    actualStartDate=models.DateField(default=timezone.now,null=True,blank=True)
    actualFinishDate=models.DateField(default=timezone.now,null=True,blank=True)
    plannedStartDate=models.DateField(default=timezone.now,null=True,blank=True)
    plannedFinishDate = models.DateField(default=timezone.now,null=True,blank=True)
    workSequenceNumber=models.IntegerField(default=0)#for drawing project GANNTT CHART AND NETWORK DIAGRAM
    plannedDuration=models.IntegerField(default=0)
    actualDuration=models.IntegerField(default=0)
    executionStatus=models.CharField(choices=work_status_choices,max_length=20,default="TO_BE_STARTED")
    actualQuantity=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)#quantity actually executed
    #startdate=models.DateField(default=timezone.now,null=True,blank=True)
    #finishdate=models.DateField(default=timezone.now,null=True,blank=True)
    #"startdate"
    #"finishdate"

    def __str__(self):
        return f'{self.item_name}'
class Progress_Quantity(models.Model):
    progress_item_id=models.ForeignKey(ProgressItem,on_delete=models.CASCADE,null=True,blank=True)
    date=models.DateField(default=timezone.now)
    quantity=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    user_id=models.ForeignKey(User,on_delete=models.CASCADE,null=True,blank=True)
    document_id=models.ForeignKey(ConstructionImage,on_delete=models.CASCADE,null=True,blank=True)
    def __str__(self):
        return f'{self.quantity}'

class ProgresSchedule(models.Model):
    progress_item_id = models.ForeignKey(ProgressItem, on_delete=models.SET_NULL, null=True, blank=True)
    date = models.DateField(default=datetime.now)
    quantity = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)

    def __str__(self):
        return f'{self.quantity}'













""""Classes for Value of Workdone      """
class SheduleItemGroup(models.Model):
    codeNo=models.CharField(max_length=100,null=True,blank=True)
    description=models.CharField(max_length=100,null=True,blank=True)
    def __str__(self):
        return f'{self.codeNo+":"+self.description}'





class Schedule(models.Model):
    group=models.ForeignKey(SheduleItemGroup,on_delete=models.CASCADE,null=True,blank=True)
    codeNo=models.CharField(max_length=100,null=True,blank=True)
    itemDescription=models.TextField()
    unit=models.CharField(max_length=50,null=True,blank=True)
    shortDescription=models.CharField(max_length=400,null=True,blank=True)

    def __str__(self):
        return f'{self.codeNo+":"+self.shortDescription}'




class BoQ(models.Model):
    quantity=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    rate=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    amount=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    schedule_id=models.ForeignKey(Schedule,on_delete=models.CASCADE,null=True,blank=True)
    structure_id=models.ForeignKey(Contract_Intervention,on_delete=models.CASCADE,null=True,blank=True)
    def __str__(self):
        return f'{self.schedule_id.codeNo+" quatity "+str(self.quantity)+" "+self.schedule_id.unit}'

class ValueOfWorkDone(models.Model):
    work_status_choices = [("OG", "OG"), ("COMP", "COMP"), ("TO_BE_STARTED", "TO_BE_STARTED")]
    measuremet_status_choices=[("1","Peat Measurement"),("2","Final_by_XEN"),("3","Final_by_CSE"),("4","Final_APproved_by_PD")]
    startDate=models.DateField(null=True,blank=True)
    finishDate=models.DateField(null=True,blank=True)
    status=models.CharField(max_length=100, choices=work_status_choices, blank=True, null=True,
                                   default= "TO_BE_STARTED")
    measuremet_status=models.CharField(max_length=100, choices=measuremet_status_choices, blank=True, null=True,
                                   default= "1")
    quantity = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13,default=0.0)
    boq_id=models.ForeignKey(BoQ,on_delete=models.CASCADE,null=True,blank=True)
    def __str__(self):
        return f'{self.boq_id.schedule_id.codeNo+" quatity "+str(self.quantity)+" "+self.boq_id.schedule_id.unit+" "+self.measuremet_status}'

class CalculationSheet(models.Model):
    description = models.CharField(max_length=255, blank=True, null=True)
    image = models.FileField(upload_to='measurements/structures/%Y/%m/%d/')
    measurement_id = models.ForeignKey(Contract_Intervention, on_delete=models.CASCADE, null=True, blank=True)

class Measurement(models.Model):
    measurement_type_choices = [("1", 'Peat Measurement'), ("2", 'Final Measurement')]
    measurement_date=models.DateField(null=True,blank=True)
    measurement_authority=models.DateField(null=True,blank=True)
    measurement_type=models.CharField(max_length=10, choices=measurement_type_choices, blank=True, null=True,
                                   default= "1")
    quantity=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    vwd=models.ForeignKey(ValueOfWorkDone,on_delete=models.CASCADE,null=True,blank=True)

    def __str__(self):
        return f'{self.measurement_type+" quantity "+str(self.quantity)}'


""""Land Acquisition """
class LandAcquisitionData(models.Model):
    laCaseNo=models.CharField(max_length=60)
    division=models.ForeignKey(Division,on_delete=models.CASCADE,null=True,blank=True)
    amountOfLand=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=6,default=0)
    personsInAwl=models.IntegerField(default=0)#number of persons in award list
    compensationAmtInAwl=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=18)
    personPaidCom=models.IntegerField(default=0)
    def __str__(self):
        return f'{"LA CASENO:"+self.laCaseNo+" of "+self.division.division_name}'

class LandAcquisitionStage(models.Model):
    step_name_choices=[("0",'Budget_Allocation'),("1","Approval_From_MOWR"),("2","Proposal_Submission"),
                       ("3","DLAC_Meeting"),("4","Notice_Under_Serctioin_4(1)"),("5","Joint_List_Under_Sec_3(6)"),
                       ("6","Approval_MOL"),("7","Notice_Under_Sec_7"),("8","Award_List_Under_Sec_8"),
                       ("9","Fund_Transfer_DC"),("10","Payment_Of_Compensation"),("11","Possesioin_Of_Land")]
    step_staus_choices=[("OG", "OG"), ("COMP", "COMP"), ("TO_BE_STARTED", "TO_BE_STARTED"),("HALTED","HALTED"),
                        ("ABANDONED","ABANDONED"),("RFUSED","REFUSED")]
    laCase=models.ForeignKey(LandAcquisitionData,on_delete=models.CASCADE,null=True,blank=True)
    stepName=models.CharField(max_length=50,choices=step_name_choices,default="0")
    stepStaus=models.CharField(max_length=50,choices=step_staus_choices,default="TO_BE_STARTED")
    dateOfCompletion=models.DateField(null=True,blank=True)
    def __str__(self):
        return f'{str(self.laCase)+"_"+self.stepName+"_"+self.stepStaus}'


class LADocuments(models.Model):
    description = models.CharField(max_length=255, blank=True, null=True)
    document = models.FileField(upload_to='LA/%Y/%m/%d/')
    issuing_date=models.DateField(default=timezone.now)
    LandAcquisitionStage= models.ForeignKey(LandAcquisitionStage, on_delete=models.CASCADE, null=True, blank=True)

""" Progress Report """
class ReportEvent(models.Model):
    report_event_status=[("Live","Live"),("Dead","Dead")]
    reportCode=models.CharField(max_length=100,null=True,blank=True)
    reportName=models.CharField(max_length=250,null=True,blank=True)
    startingDate=models.DateField(timezone.now)#starting date of reporting period
    finishDate=models.DateField(timezone.now) #finishing date of reporting period
    lastSubmissionDate=models.DateField(timezone.now)#last submission date
    message=models.TextField(null=True,blank=True)
    eventStatus=models.CharField(max_length=100,choices=report_event_status,default="Live")


    def __str__(self):
        return f'{str(self.reportCode)+"_"+self.reportName}'

class ReportSubmissionStatus(models.Model):
    submission_status_choices=[("Submitted","Submitted"),("NotSubmitte","NotSubmitted")]
    reportEvent=models.ForeignKey(ReportEvent,null=True,blank=True,on_delete=models.CASCADE)
    reportingPerson= models.ForeignKey(settings.AUTH_USER_MODEL,on_delete=models.SET_NULL, null=True, blank=True)
    submissionDate=models.DateField(timezone.now)
    submissionStats=models.CharField(max_length=100,choices=submission_status_choices,default="NotSubmitted")
    def __str__(self):
        return f'{str(self.reportEvent.reportCode)+" to be submitted by "+str(self.reportingPerson)+" within: "+str(self.submissionDate)+" statsu= "+self.submissionStats}'

class Reportdocument(models.Model):
    description = models.CharField(max_length=255, blank=True, null=True)
    document = models.FileField(upload_to='ProgressReport/%Y/%m/%d/')
    issuing_date = models.DateField(default=timezone.now)
    #submissionStatus = models.ForeignKey(ReportSubmissionStatus, on_delete=models.CASCADE, null=True, blank=True)
    reportEvent_id=models.ForeignKey(ReportEvent,on_delete=models.CASCADE,null=True,blank=True)
    reportingPerson = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL,
                                        null=True, blank=True)

"""A New model for keep tracking of Qualitative progress"""
class qualitativeStatus(models.Model):
    contract_status_choices = [("C", "Completed"), ("OG", "On Going"),("P","Problamatic")]
    problems_status_choices=[("LA", "Land Acquisition"), ("SR", "Structure Relocated"),
                             ("LP","Local Peoplles's Obstruction"),("RE","River Erosion"),
                             ("AB","Structure is Abandoned"),("OV","Overlap With Other Project"),
                             ("GC"," inappropriate Sub-surface Condition"),("NP","None"),("OT","Others"),]
    contract_ivt=models.ForeignKey(Contract_Intervention,on_delete=models.CASCADE,null=True,blank=True)
    overall_status=models.CharField(max_length=100,choices=contract_status_choices,default="OG")
    current_progress= models.DecimalField(null=True, blank=True, decimal_places=5, max_digits=6)
    problems=models.CharField(max_length=100,choices= problems_status_choices,default="NP")
    value_of_work_done=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=7,default=0)

    def __str__(self):
        return f'{str(self.contract_ivt)+"_"+str(self.overall_status)}'
"""Model for Keep Track of IPC   """
class IPC(models.Model):
    contract=models.ForeignKey(Contract,on_delete=models.CASCADE,null=True,blank=True)
    ipcNo=models.IntegerField(null=True,blank=True)
    ipcName=models.CharField(max_length=500,null=True,blank=True)
    ipcAmount=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)

class IPC_Item(models.Model):
    ipc=models.ForeignKey(IPC,on_delete=models.CASCADE,null=True,blank=True)
    boq=models.ForeignKey(BoQ,on_delete=models.CASCADE,null=True,blank=True)
    quantity_paid=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)








