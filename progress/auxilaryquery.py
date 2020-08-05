from .models import Contract_Intervention,DPP_Intervention,ProgressItem,Progress_Quantity
from decimal import *
import random
"""   
intervention_id = models.ForeignKey(Contract_Intervention, on_delete=models.SET_NULL, null=True, blank=True)
item_name = models.CharField(max_length=200, default="EW")
unit = models.CharField(max_length=10)
quantity = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
weight = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
startdate = models.DateField(default=timezone.now)
finishdate = models.DateField(default=timezone.now)

"""
import datetime
"""   
progress_item_id = models.ForeignKey(ProgressItem, on_delete=models.SET_NULL, null=True, blank=True)
date = models.DateField(default=timezone.now)
quantity = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
user_id = models.ForeignKey(User, on_delete=models.CASCADE, null=True, blank=True)
document_id = models.ForeignKey(ConstructionImage, on_delete=models.CASCADE, null=True, blank=True)
"""


def createProgressQuantity(pitem,contract):
    now = datetime.datetime.now()
    user1=contract.xen_id
    user2=contract.cse_id
    user3=contract.fse_id
    multiplier = random.randint(30,45) / 100.0
    tq=float(pitem.quantity)
    pq=tq*multiplier
    quantity1=Progress_Quantity(progress_item_id=pitem,date=now,quantity=pq,user_id=user1)
    quantity1.save()
    quantity2 = Progress_Quantity(progress_item_id=pitem, date=now, quantity=pq, user_id=user2)
    quantity2.save()
    quantity3 = Progress_Quantity(progress_item_id=pitem, date=now, quantity=pq, user_id=user3)
    quantity3.save()

def progresItemEmb(civt):
    contract=civt.contract_id
    length=civt.dpp_intervention_id.length
    #volume=civt.dpp_intervention_id.volume
    volume=random.uniform(75000.00, 350000.00)
    area=0.61*volume
    item1=ProgressItem( intervention_id=civt,item_name="Earth work in filling",unit="Cum",quantity=volume,weight=0.85)
    item1.save()
    createProgressQuantity(item1,contract)

    item2 = ProgressItem(intervention_id=civt, item_name="Close Turfing", unit="sqm", quantity=area,
                         weight=0.15)
    item2.save()
    createProgressQuantity(item2, contract)
    item3 = ProgressItem(intervention_id=civt, item_name="Length Completion", unit="Km", quantity=length,
                         weight=0.00)
    item3.save()
    createProgressQuantity(item3, contract)
def progresItemKhal(civt):
    contract = civt.contract_id
    length=civt.dpp_intervention_id.length
    #volume=civt.dpp_intervention_id.volume
    volume = random.uniform(10000.00, 75000.00)
    item1=ProgressItem( intervention_id=civt,item_name="Earth work in cutting",unit="Cum",quantity=volume,weight=0.85)
    item1.save()
    createProgressQuantity(item1,contract)

    item3 = ProgressItem(intervention_id=civt, item_name="Length Completion", unit="Km", quantity=length,
                         weight=0.00)
    item3.save()
    createProgressQuantity(item3, contract)



def progresItemRiver(civt):
    contract = civt.contract_id
    length = civt.dpp_intervention_id.length
    #volume = civt.dpp_intervention_id.volume
    volume = random.uniform(750000.00, 450000.00)
    item1 = ProgressItem(intervention_id=civt, item_name="Earth work in cutting", unit="Cum", quantity=volume,
                         weight=0.85)
    item1.save()
    createProgressQuantity(item1, contract)


    item3 = ProgressItem(intervention_id=civt, item_name="Length Completion", unit="Km", quantity=length,
                         weight=0.00)
    item3.save()
    createProgressQuantity(item3, contract)


def progresItemReg(civt):
    contract = civt.contract_id
    item1 = ProgressItem(intervention_id=civt, item_name="Earth work in Foundation Excavation", unit="Cum", quantity=5208.35,
                         weight=0.05)
    item1.save()
    createProgressQuantity(item1, contract)
    item2 = ProgressItem(intervention_id=civt, item_name="Sheet Pile Driving", unit="meter", quantity=134.68,
                         weight=0.15)
    item2.save()
    createProgressQuantity(item2, contract)

    item3 = ProgressItem(intervention_id=civt, item_name="CC Block manufacturing", unit="Nos", quantity=13344,
                         weight=0.20)
    item3.save()
    createProgressQuantity(item3, contract)
    item4= ProgressItem(intervention_id=civt, item_name="Foundation and Apron Construction", unit="Cum", quantity=152.89,
                         weight=0.20)
    item4.save()
    createProgressQuantity(item4, contract)
    item5 = ProgressItem(intervention_id=civt, item_name="Barrel and Wall Construction", unit="cum", quantity=121.79,
                         weight=0.20)
    item5.save()
    createProgressQuantity(item5, contract)
    item6 = ProgressItem(intervention_id=civt, item_name="Diversion channel and Approach Embankment", unit="cum", quantity=11022.13,
                         weight=0.10)
    item6.save()
    createProgressQuantity(item6, contract)
    item7 = ProgressItem(intervention_id=civt, item_name="Loose Apron", unit="sqm", quantity=326.18,weight=0.05)

    item7.save()
    createProgressQuantity(item7, contract)
    item8= ProgressItem(intervention_id=civt, item_name="Gate Installation", unit="Nos", quantity=2, weight=0.05)

    item8.save()
    createProgressQuantity(item8, contract)
def progresItemWMGOffice(civt):
    contract = civt.contract_id
    item1 = ProgressItem(intervention_id=civt, item_name="Earth work in Foundation Excavation", unit="Cum", quantity=5208.35,
                         weight=0.05)
    item1.save()
    createProgressQuantity(item1, contract)
    item2 = ProgressItem(intervention_id=civt, item_name="Column Foundation", unit="No", quantity=14,
                         weight=0.15)
    item2.save()
    createProgressQuantity(item2, contract)

    item3 = ProgressItem(intervention_id=civt, item_name="Column Construction", unit="Nos", quantity=14,
                         weight=0.15)
    item3.save()
    createProgressQuantity(item3, contract)
    item4= ProgressItem(intervention_id=civt, item_name="Roof Casting", unit="Sqm", quantity=120,
                         weight=0.10)
    item4.save()
    createProgressQuantity(item4, contract)
    item5 = ProgressItem(intervention_id=civt, item_name="Bricwork", unit="Sqm", quantity=121.79,
                         weight=0.20)
    item5.save()
    createProgressQuantity(item5, contract)
    item6 = ProgressItem(intervention_id=civt, item_name="Painting", unit="Sqm", quantity=300,
                         weight=0.10)
    item6.save()
def progresItemSlopeProtection(civt):
    contract=civt.contract_id
    length=civt.dpp_intervention_id.length
    volume=civt.dpp_intervention_id.volume
    item1=ProgressItem( intervention_id=civt,item_name="Earth work in cutting/filling",unit="Cum",quantity=volume,weight=0.10)
    item1.save()
    createProgressQuantity(item1,contract)

    item2 = ProgressItem(intervention_id=civt, item_name="CC Block Manufaturing", unit="Nos", quantity=100000,
                         weight=0.70)
    item2.save()
    createProgressQuantity(item2, contract)
    item3 = ProgressItem(intervention_id=civt, item_name="Block Placing", unit="Sqm", quantity=1000,
                         weight=0.20)
    item3.save()
def creteProgressItem(civt):
    """
    intervention_id=civt
    item_name="Earth Work in Foundation"
    unit="Cum"
    quantity=1000
    weight=0.15
    item1=ProgressItem( intervention_id=civt,item_name=item_name,unit=unit,quantity=quantity,weight=weight)
 Types=[('EMB','Embankment'),('SUBEMB', 'Submersible,Embankment'),('EXKHL','Khal Rexcavation'),('EXRIV','River Reexcavation'),('REG','Regulator'),('CASW','Cause Way'),
           ('IRIN','Irigation Inlet'),('WMGO','WMG Office'),('BOXSL','Box Sluice')]
    item1.save()  """
    codelist = ['IRINC','REGRR','REGCN','CASWCN','BRIDGCN','BOXSLCN','EXKHLN','EXRIVN','EXKHLR','EXRIVR','EMBR',
                'SEMBRN','SEMBCN','REGRN','EMBSPW','WMGOC']
    emb=['EMBR','SEMBRN','SEMBCN']
    khal= ['EXKHLN','EXKHLR']
    river=['EXRIVN','EXRIVR']
    structure=['IRINC','REGRR','REGCN','CASWCN','BRIDGCN','BOXSLCN','REGRN']
    wmgoffice=['WMGOC']
    slopeprotection=['EMBSPW']

    wtype=civt.dpp_intervention_id.worktype_id.wtype
    if wtype in emb:
        progresItemEmb(civt)
    elif wtype in khal:
        progresItemKhal(civt)
    elif wtype in  river:
        progresItemRiver(civt)
    elif wtype in structure:
        progresItemReg(civt)
    elif wtype in  wmgoffice:
        progresItemWMGOffice(civt)
    elif wtype in slopeprotection:
        progresItemSlopeProtection(civt)

    else:
        progresItemEmb(civt)



    """ 
    
    if(civt.dpp_intervention_id.worktype_id.wtype=="REG"):
        print("this item is")
        progresItemReg(civt)
    elif civt.dpp_intervention_id.worktype_id.wtype=="BOXL":
        progresItemReg(civt)
    elif civt.dpp_intervention_id.worktype_id.wtype=="CASW":
        progresItemReg(civt)
    elif  civt.dpp_intervention_id.worktype_id.wtype=="IRIN":
        progresItemReg(civt)
    elif civt.dpp_intervention_id.worktype_id.wtype == "EMB":
        progresItemEmb(civt)
    elif civt.dpp_intervention_id.worktype_id.wtype== "SUBEMB":
        progresItemEmb(civt)
    elif civt.dpp_intervention_id.worktype_id.wtype == "EXKHL":
        progresItemKhal(civt)
    elif civt.dpp_intervention_id.worktype_id.wtype == "EXRIV":
        progresItemRiver(civt)
    else:
        progresItemEmb(civt)
    """




def deleteProgressItem(civt):
    pitems=ProgressItem.objects.filter(intervention_id=civt)
    pitems.delete()

def prepareMultiline(mystr,cpl):
   nos=int(len(mystr)/cpl)

   for i in range(1,nos+1):
       index=cpl*(i)
       mystr=mystr[:index]+chr(10)+mystr[index:]
   return mystr






from .models import Progress_Quantity
def calculateProgressQuantity(pitem,user,mylist):
    pquantity=list(Progress_Quantity.objects.filter(progress_item_id=pitem,user_id=user).order_by('-date')[:2])
    print(pquantity)
    if pquantity:
        p1=pquantity[0]
        p2=pquantity[1]
        duration=p1.date-p2.date
        q1=float(p1.quantity)
        q2=float(p2.quantity)
        delq=q1-q2
        w=float(mylist[7])
        tq=float(mylist[6])
        print("prev_date={} current_date={} duration={} q1={} q2={} delq={}".format(p1.date, p2.date, duration, q1, q2, delq))
        """   
        tableHeading = ['Sl', 'Prev_Rep_Date', 'curr_date', 'duration' 'Item Name', 'Unit', 'Est_Qua', 'weight', 'Prev_Qua', 'Qua_Curr', 'Qua_Exe_RP', 'Itemised_prog']
         """
        mylist[1]=p2.date
        mylist[2]=p1.date
        mylist[3]=duration
        mylist[8]=q2
        mylist[9]=q1
        mylist[10]=delq
        if tq !=0:
            mylist[11]=round(q1*w/tq,3)
        else:
            mylist[11]=0.0
        print(mylist)
    return mylist
    """    #p1=Progress_Quantity.objects.filter(progress_item_id=pitem,user_id=user).order_by('-date')[2]
    #p2=Progress_Quantity.objects.filter(progress_item_id=pitem,user_id=user).order_by('-date')[1]
    duration=p1.date-p2.date
    prog1=p1.quantiy
    prog2=p2.quantiy
    del_q=prog1-prog2
    print("prev_date={} current_date={} duration={} q1={} q2={} delq={}".foramt(p1.date,p2.date,duration,prog1,prog2,del_q))
    """
    """  
    for p in pquantity:
        print("item={} date={} quantity={}".format(pitem.item_name, p.date,p.quantity))
    """





from reportlab.lib.enums import TA_JUSTIFY
from reportlab.lib.units import cm
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Image,Table, TableStyle
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch
from django.core.files.storage import FileSystemStorage
from reportlab.lib.styles import getSampleStyleSheet
from io import BytesIO
from reportlab.lib.pagesizes import A4, landscape
from reportlab.lib import colors
def createProgressReport(contracts,user):
    """ Basic Setup for Report Lab   """
    pdf_buffer = BytesIO()
    doc = SimpleDocTemplate(pdf_buffer)
    doc.pagesize =landscape(A4)
    flowables = []#overall flowables
    structural_summary=[]#for structural summary
    contract_summary=[]
    sample_style_sheet = getSampleStyleSheet()
    #sample_style_sheet.list()
    """Creating Style for Paragraph  """
    custom_body_style = sample_style_sheet['Heading5']
    #custom_body_style.listAttrs()
    custom_body_style.spaceAfter=0
    custom_body_style.spaceBefore=0

    for contract in contracts:
        structures=Contract_Intervention.objects.filter(contract_id=contract)
        """Variable for Holding Total progress in  structure  """
        #mylist = ['', '', '', '', "Overall Progress",'','', '', '', '', '', '']
        contract_data=[]
        item_progress="itemized"+chr(10)+"progress"
        contract_heading=['sl','name','weight','progress',item_progress]
        contract_data.append(contract_heading)
        contract_sl=1
        contract_sum=0

        myheading2 = "Contract Name : " + contract.package_short_name
        myparagraph2 = Paragraph(myheading2, custom_body_style)
        myparagraph2.hAlign = 'LEFT'
        contract_summary.append(myparagraph2)
        for structure in structures:
            """ Structurl Progress Rreport      """
            contractName=""
            contract_row=[contract_sl,prepareMultiline(structure.dpp_intervention_id.name,25),structure.physical_weight,0,0]
            contract_sl +=1
            """ Adding Header Section For Report """
            myheading1 = "Haor Name : " + structure.dpp_intervention_id.haor_id.name
            myparagraph1 = Paragraph(myheading1, custom_body_style)
            myparagraph1.hAlign = 'LEFT'

            myheading3="Structure Name : "+structure.dpp_intervention_id.name
            myheading4="Reproting Date : "+"24/3/2019" # Latter A Proper Date Will be Created
            #mystr=contract.package_short_name+"/"+structure.dpp_intervention_id.name

            flowables.append(myparagraph1)

            flowables.append(myparagraph2)
            myparagraph3 = Paragraph(myheading3, custom_body_style)
            myparagraph1.hAlign = 'LEFT'
            flowables.append(myparagraph3)
            myparagraph4 = Paragraph(myheading4,  custom_body_style)
            myparagraph1.hAlign = 'LEFT'
            flowables.append(myparagraph4)
            flowables.append(Spacer(1, 0.25 * cm))
            """ Table Section of the report  """
            pitems=ProgressItem.objects.filter(intervention_id=structure)
            mydata=[]
            iprog="item"+chr(10)+ "progress"
            qua_exe="Quan"+chr(10)+"Executed"+chr(10)+"RPeriod"
            tableHeading=['Sl','Pre_date','curr_date','duration','Item Name','Unit','Est_Qua','weight','Prev_Qua','Qua_Curr', qua_exe,iprog]
            mydata.append(tableHeading)
            sl=1
            sum=0.0
            total_structural = ['', '', '', '', "Overall Progress", '', '', '', '', '', '', '']
            for pitem in pitems:
                iname=prepareMultiline(pitem.item_name,25)
                mylist=[sl,'','','',iname, pitem.unit,pitem.quantity,pitem.weight,'','','',0]
                mylist=calculateProgressQuantity(pitem,user,mylist)
                mydata.append(mylist)
                sl+=1
                sum+=float(mylist[11])
                print("total for={}".format(mylist[11]))
            total_structural[11]=sum
            mydata.append(total_structural)
            colwidth=[1.0*cm,1.8*cm,1.8*cm,1.8*cm,4.9*cm,2.0*cm,2.2*cm,2.2*cm,2.2*cm,2.0*cm,2.0*cm,2.0*cm]
            t = Table(mydata,repeatRows=1,colWidths=colwidth)
            t.setStyle(TableStyle([('INNERGRID', (0,0), (-1,-1), 0.25, colors.black),('BOX', (0,0), (-1,-1), 0.25, colors.black),]))
            t.hAlign = 'LEFT'
            #t.wrapOn()
            flowables.append(t)
            flowables.append(Spacer(1, 0.5* cm))
            contract_row[3]= total_structural[11]
            contract_row[4]=float(contract_row[3])*float(contract_row[2])
            contract_sum +=contract_row[4]
            contract_data.append(contract_row)
        total_contract=['',prepareMultiline('Overall physical Progress',60),'','',contract_sum ]
        contract_data.append(total_contract)
        contract_colwidth=[1.0*cm,12*cm,3*cm,3*cm,4*cm]
        t_contract=Table(contract_data,repeatRows=1,colWidths=contract_colwidth)
        t_contract.setStyle(TableStyle(
            [('INNERGRID', (0, 0), (-1, -1), 0.25, colors.black), ('BOX', (0, 0), (-1, -1), 0.25, colors.black), ]))
        t_contract.hAlign='LEFT'
        contract_summary.append(t_contract)



    signature_table_data=[]
    #myheading1 = "This is a test paragraph"
    #myparagraph1 = Paragraph(myheading1, custom_body_style)
    for item in reversed(contract_summary):
        flowables.insert(0,item)
    #custom_body_style.listAttrs()
    #sample_style_sheet.list()



    """
    item_name = models.CharField(max_length=200, default="EW")
    unit = models.CharField(max_length=10)
    quantity = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    weight = models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    startdate = models.DateField(default=timezone.now)
    finishdate = models.DateField(default=timezone.now)



    
    styles = getSampleStyleSheet()
    Story = [Spacer(1, 2 * inch)]
    style = styles["Normal"]
    for i in range(100):
        bogustext = ("This is Paragraph number %s.  " % i) * 20
        p = Paragraph(bogustext, style)
        Story.append(p)
        Story.append(Spacer(1, 0.2 * inch))
    """
    doc.build(flowables)

    pdf_value = pdf_buffer.getvalue()
    pdf_buffer.close()
    return pdf_value
def buildIdForContractCardLink(contracts):
	clid=[]
	i=1
	for contract in contracts:
		id="#c"+str(i)
		clid.append(id)
	return clid
def buildIdForContractCard(contracts):
	cid=[]
	i=1
	for contract in contracts:
		id="c"+str(i)
		cid.append(id)
	return cid
def buildIdForContractCard2(contracts):
    cid = []
    i = 1
    for contract in contracts:
        id = "c" + str(i)
        lid = "#c" + str(i)
        chid="ch-"+str(i)
        contract_item = {'item1': lid, 'item2': id, 'item3': contract,'item4':chid}
        cid.append(contract_item)
        i+=1
    return cid
import random
def getProgressData(contracts):
    i=1
    mydic=dict()
    for contract in contracts:
        id="c"+str(i)
        myrange=range(6)
        value=[random.randint(0,100) for x in  myrange]
        mydic[id]=value
        i=i+1
    return mydic

def calculateStructureProgress(contrcactIvt,user):
    pitems = ProgressItem.objects.filter(intervention_id=contrcactIvt)
    structuralprog=0
    if ProgressItem.objects.filter(intervention_id=contrcactIvt).exists():
        for pitem in pitems:
            #pquantity = list(Progress_Quantity.objects.filter(progress_item_id=pitem, user_id=user).order_by('-date')[:2])
            if user.profile.role.role_name == "SP_ADMIN":
                pquantity = list(
                    Progress_Quantity.objects.filter(progress_item_id=pitem).order_by('-date')[:2])

            else:
                pquantity = list(Progress_Quantity.objects.filter(progress_item_id=pitem, user_id=user).order_by('-date')[:2])
            #print(pquantity)
            if pquantity:
                p1 =float(pquantity[0].quantity)
                w = float(pitem.weight)
                tq=float(pitem.quantity)
                structuralprog +=(p1/tq)*w
    return structuralprog


def findProgressIndex(dpp_item):
    #codelist=['EMBRR','SUBEMBCN','SUBEMBRN','EXKHLN','EXRIVN','REGCN','CASWCN','IRINC','BOXSLCN','EXKHLR','EXRIVR']
    codelist=['IRINC','REGRR','REGCN','CASWCN','BRIDGCN','BOXSLCN','EXKHLN','EXRIVN','EXKHLR','EXRIVR','EMBR','SEMBRN',
              'SEMBCN','REGRN','EMBSPW','WMGOC']
    code=str(dpp_item.worktype_id)
    code=code.strip()
    index=codelist.index(code)
    #print("item code={} inndex={}".format(code,index))

    #if(code in codelist ):
        #index=codelist.index(code)
    return index
def findProgressInStructuralUnit(civt):
    emb=['EMBR','SEMBRN','SEMBCN']
    khals=['EXKHLN','EXKHLR','EXRIVN','EXRIVR']

def calculatePackageProgressData(civts,user):
    """
    names = ['Fulll Embnakment Rehab-Rehab Haor', 'Submersible Embankment Construction New-Haor',
             'Submersible Embankment Construction New-Haor', 'Khal Rexcavation New Haor',
             'River Excavtion New Haor',
             'Regulator Constyruction New Haor', 'Causeway Connstruction New Haor', 'Irigation Inlet Construction',
             'Box Sluice Constuction New', 'Khal Rexcavation Rehablitation Haor', 'River Excavtion Rehab Haor']
     """
    names=["Inlet","Regulator Repair Rehab","Regulator New Haor","Causeway New Haor",
           "Brdige New Haor","Box Outlet New "
         ,"Khal New Haor","River New Haor","Khal Rehab Haor","River Rehab Haor",'Full Embankment Repair Rehab Haor',
           'Submersible Emb Repair New Haor','Submersible Emb Const New Haor','Regulator Repair New Haor',
           'Slope Protection Work','WMG Office Building']
    units=['Nos','Nos','Nos','Nos','Nos','Nos','Km','Km','Km','Km','Km','Km',
           'Km','Nos','meter','Nos']
    #number=['IRINC','REGRR','REGCN','CASWCN','BRIDGCN','BOXSLCN','REGRN','WMGOC']
    codelist = ['IRINC', 'REGRR', 'REGCN', 'CASWCN', 'BRIDGCN', 'BOXSLCN',
                 'REGRN',  'WMGOC']
    padtas=[]
    n=len(names)
    progress=[0 for i in range(n)]
    target=[0 for i in range(n)]
    totalQuantity=[0 for i in range(n)]
    completedQuantity=[0 for i in range(n)]
    fiteredProg=[]
    filteredTarget=[]
    filteredItem=[]
    filteredTotalQuantity=[]
    filteredCompletedQuantity=[]
    filteredRemainingItem=[]
    filteredUnit=[]
    #conntractivts=Contract_Intervention.objects.filter(contract_id=contract)
    pdatas=[]

    for ivt in civts:
        dpp_item=ivt.dpp_intervention_id
        code = str(dpp_item.worktype_id)
        code = code.strip()
        #print("printing code...")
        #print("nname={} code={}".format(dpp_item.name,code))
        index=findProgressIndex(dpp_item)
        #print("index of item={}".format(index))
        prog=calculateStructureProgress(ivt,user)
        #print("structue={}  structural progress={}".format(ivt,prog))
        """calculating unit progress"""
        if code in codelist:
            length=1.0
        else:
            length=float(dpp_item.length)
        #length=dpp_item.length
        completion=length*prog
        #print("index={} structure={} length={} completed={}".format(index,ivt,length,completion))
        q1=completedQuantity[index]
        completedQuantity[index]=q1+completion
        #print(completedQuantity)
        q2=totalQuantity[index]
        totalQuantity[index] =q2+length
        weight=float(ivt.physical_weight)
        pp=prog*weight
        progress[index]+=pp
        target[index] +=weight
    #print("Printing total quantity..............")
    #print("total={} completed={}".format(totalQuantity,completedQuantity))

    for i in range(n):
        if target[i]!=0:
            fiteredProg.append(progress[i])
            filteredTarget.append(target[i])
            filteredItem.append(names[i])
            filteredTotalQuantity.append(round(totalQuantity[i],3))
            filteredCompletedQuantity.append(round(completedQuantity[i],3))
            remaining=round((totalQuantity[i]-completedQuantity[i]),3)
            filteredRemainingItem.append(remaining)
            filteredUnit.append(units[i])

    n2=len(filteredItem)
    sum1=0
    sum2=0
    sum3=0
    for i in range(n2):
        pdata=[]
        pdata.append(filteredItem[i])
        pdata.append(filteredUnit[i])
        pdata.append(filteredTotalQuantity[i])
        itemprog=filteredCompletedQuantity[i]/filteredTotalQuantity[i]
        itemprog=round((itemprog*100),3)
        pdata.append("-")
        pdata.append(itemprog)
        weight=filteredTarget[i]
        weight=round(weight,3)
        pdata.append(weight)
        packprog=itemprog*weight
        packprog=round(packprog,3)
        pdata.append(packprog)
        sum1+=packprog
        pdata.append("-")
        pdata.append("-")
        pdatas.append(pdata)
    pdata=[]
    pdata.append("Package Total")
    pdata.append("")
    pdata.append("")
    pdata.append("")
    pdata.append("")
    pdata.append("")
    pdata.append(sum1)
    pdata.append("")
    pdata.append("")
    pdatas.append(pdata)





    mydata={'target':filteredTarget,'achievement':fiteredProg,'filteredItem':filteredItem,
            'total':filteredTotalQuantity,'completed':filteredCompletedQuantity,
            'rest':filteredRemainingItem,'unit':filteredUnit,'pdatas':pdatas
            }
    return mydata

def fillBlankProgressQuantity(pitems,contract):
    for item in pitems:
        pq=list(Progress_Quantity.objects.filter(progress_item_id=item.id
                                                 ).order_by('-date')[:2])
        if not pq:
            createProgressQuantity(item,contract)

def createDummyProgressQuantity(pitem,contract):
    now = datetime.datetime.now()
    user1=contract.xen_id
    user2=contract.cse_id
    user3=contract.fse_id
    weight=float(pitem.weight)
    tq=float(pitem.quantity)
    multiplier=random.randint(20,73)/100.0
    q=tq*multiplier

    quantity1=Progress_Quantity(progress_item_id=pitem,date=now,quantity=q,user_id=user1)
    print( "item={}  quantity={}".format(pitem,quantity1.quantity))
    quantity1.save()
    quantity2 = Progress_Quantity(progress_item_id=pitem, date=now, quantity=q, user_id=user2)
    quantity2.save()
    quantity3 = Progress_Quantity(progress_item_id=pitem, date=now, quantity=q, user_id=user3)
    quantity3.save()

def dummyProgressQuantity(civts,user):
    for ivt in civts:
        pitems = ProgressItem.objects.filter(intervention_id=ivt)
        for item in pitems:
            createDummyProgressQuantity(item,ivt.contract_id)
    mydata = {'target': [0,1,2], 'achievement': [0,1,2], 'filteredItem': ['x','y','z'],
              'tenderd': [0,1,2], 'completed': [0,1,2]
              }
    return mydata
def crerateProgressReportEventMessage(lastDate,rptCode):
    msg=""
    msg +="Dear Sir,\n"
    msg +="Please Submit the Report No:"
    msg +=str(rptCode)
    msg +=" within " +lastDate.strftime("%d/%m/%Y")+"\n"
    msg +="Regards,\n"
    msg +="Project Director,\n"
    msg +="Haor Flood Management & Livelihood Improvement Project."
    return msg
from datetime import date,datetime
from .models import ReportEvent,ReportSubmissionStatus
from django.conf import settings
from django.contrib.auth.models import User
from accounts.models import Profile
from .models import ReportEvent,ReportSubmissionStatus
def crerateProgressReportEventData(myform):
    first=random.randint(1,9)
    second=random.randint(1,9)
    third=random.randint(1,9)
    isocal=date.today().isocalendar()
    timestamp=datetime.timestamp(datetime.now())
    code="Y"+str(isocal[0])+"W"+str(isocal[1])+"C"+str(first)+str(second)+str(third)+"T"+str(timestamp)
    print("code={}".format(code))
    users=User.objects.all()
    for user in users:
        if user.profile is None:
            print("user={}".format(user))
        else:
            print("user={} role={}".format(user,user.profile.role.role_name))
    print(users)
    name=myform.cleaned_data.get("reportName")
    stdate=myform.cleaned_data.get("startingPeriod")
    print("starting date={}".format(stdate))
    fdate=myform.cleaned_data.get("finishingPeriod")
    subDate=myform.cleaned_data.get("submissionDate")
    message=crerateProgressReportEventMessage(subDate,code)
    #status=myform.cleaned_data.get("eventStatus")
    event1=ReportEvent(reportCode=code,reportName=name,startingDate=stdate,finishDate=fdate,
                       lastSubmissionDate= subDate,message=message,eventStatus="Live" )
    event1.save()
    reportingPersons=myform.cleaned_data.get("reportingPerson")
    print("name={} code={} stdate={} finbshdate={} lastssub={}".format(name,code,stdate,fdate,subDate))
    print(message)
    print(event1)
    print(reportingPersons)
    myusers=User.objects.all()
    roles=["XEN","CSE_CONSL","FIELD_ENGG_CONSL"]
    rolemap=["XEN","CSE","FSE"]
    for user in myusers:
        rolename = str(user.profile.role.role_name)
        if rolename in roles:
            idx= roles.index(rolename)
            reportee=rolemap[idx]
            if reportee in reportingPersons:
                print("user={} designation={}".format(user,user.profile.role.role_name))
                staus="NotSubmitted"
                rptSubStatus=ReportSubmissionStatus(reportEvent=event1,
                                                    reportingPerson=user,
                                                    submissionDate=event1.lastSubmissionDate,
                                                    submissionStats=staus )
                print(rptSubStatus)
                rptSubStatus.save()





    print(myusers)

def crerateProgressReportEventData2(data):
    first = random.randint(1, 9)
    second = random.randint(1, 9)
    third = random.randint(1, 9)
    isocal = date.today().isocalendar()
    timestamp = datetime.timestamp(datetime.now())
    code = "Y" + str(isocal[0]) + "W" + str(isocal[1]) + "C" + str(first) + str(second) + str(third) + "T" + str(
        timestamp)
    print("code={}".format(code))
    users = User.objects.all()
    for user in users:
        if user.profile is None:
            print("user={}".format(user))
        else:
            print("user={} role={}".format(user, user.profile.role.role_name))
    print(users)
    #name = myform.cleaned_data.get("reportName")
    name=data['name']
    #stdate = myform.cleaned_data.get("startingPeriod")
    stdate=datetime.strptime(data['stdate'],'%Y-%m-%d')
    print("starting date={}".format(stdate))
    #fdate = myform.cleaned_data.get("finishingPeriod")
    fdate=datetime.strptime(data['fdate'],'%Y-%m-%d')
    #subDate = myform.cleaned_data.get("submissionDate")
    subDate=datetime.strptime(data['subdate'],'%Y-%m-%d')
    message = crerateProgressReportEventMessage(subDate, code)
    # status=myform.cleaned_data.get("eventStatus")
    event1 = ReportEvent(reportCode=code, reportName=name, startingDate=stdate, finishDate=fdate,
                         lastSubmissionDate=subDate, message=message, eventStatus="Live")
    event1.save()
    #reportingPersons = myform.cleaned_data.get("reportingPerson")
    reportingPersons=data["reportingPerson"]
    print("name={} code={} stdate={} finbshdate={} lastssub={}".format(name, code, stdate, fdate, subDate))
    print(message)
    print(event1)
    print(reportingPersons)
    myusers = User.objects.all()
    roles = ["XEN", "CSE_CONSL", "FIELD_ENGG_CONSL"]
    rolemap = ["XEN", "CSE", "FSE"]
    for user in myusers:
        rolename = str(user.profile.role.role_name)
        if rolename in roles:
            idx = roles.index(rolename)
            reportee = rolemap[idx]
            if reportee in reportingPersons:
                print("user={} designation={}".format(user, user.profile.role.role_name))
                staus = "NotSubmitted"
                rptSubStatus = ReportSubmissionStatus(reportEvent=event1,
                                                      reportingPerson=user,
                                                      submissionDate=event1.lastSubmissionDate,
                                                      submissionStats=staus)
                print(rptSubStatus)
                rptSubStatus.save()

    print(myusers)
    return event1
    """  
    report_event_status = [("Live", "Live"), ("Dead", "Dead")]
    reportCode = models.CharField(max_length=100, null=True, blank=True)
    reportName = models.CharField(max_length=250, null=True, blank=True)
    startingDate = models.DateField(timezone.now)  # starting date of reporting period
    finishDate = models.DateField(timezone.now)  # finishing date of reporting period
    lastSubmissionDate = models.DateField(timezone.now)  # last submission date
    message = models.TextField(null=True, blank=True)
    eventStatus = models.CharField(max_length=100, choices=report_event_status, default="Live")
     """
    """  
    personOptions = [("XEN", "XEN"), ("CSE", "CSE"), ("FSE", "FSE")]
    reportName = forms.CharField(label="Report Name")
    startingPeriod = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                     widget=forms.DateInput(format="%d%m%Y"),
                                     input_formats=('%d/%m/%Y',), help_text="dd/mm/yyyy")
    finishingPeriod = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                      widget=forms.DateInput(format="%d%m%Y"),
                                      input_formats=('%d/%m/%Y',), help_text="dd/mm/yyyy")
    submissionDate = forms.DateField(label="Finishing Date of Report(dd/mm/yyyyy)",
                                     widget=forms.DateInput(format="%d%m%Y"), input_formats=('%d/%m/%Y',),
                                     help_text="dd/mm/yyyy")
    reportingPerson = forms.MultipleChoiceField(label="Reporting Person", widget=forms.CheckboxSelectMultiple,
                                                choices=personOptions)
    """
    """
    submission_status_choices = [("Submitted", "Submitted"), ("NotSubmitte", "NotSubmitted")]
    reportEvent = models.ForeignKey(ReportEvent, null=True, blank=True, on_delete=models.CASCADE)
    reportingPerson = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.SET_NULL, null=True, blank=True)
    submissionDate = models.DateField(timezone.now)
    submissionStats = models.CharField(max_length=100, choices=submission_status_choices, default="NotSubmitted")
    """

def convertReportObject_to_dic(myreportEvent):
    data = dict()
    data['name'] = myreportEvent.reportName
    data['stdate'] = myreportEvent.startingDate
    data['fdate'] = myreportEvent.finishDate
    data['subdate'] = myreportEvent.lastSubmissionDate
    data['status'] =myreportEvent.eventStatus
    data["message"]=myreportEvent.message
    return data
class structure:

    def __init__(self,name):
        self.name=name




def build_structure_list(contract_interventions):
    for ivt in contract_interventions:
        print(ivt.contract_id)


































