import os
from django.contrib.gis.utils import LayerMapping
from  .models import Haor,DPP_Intervention
from django.contrib.gis.geos import fromstr,Polygon,GEOSGeometry,fromfile,fromstr,MultiPolygon,Point
import geopandas as gpd
from django.shortcuts import render, get_object_or_404, redirect
import pandas as pd
haor_mapping={
    'name':'name',
    'area':'area',
    'project_type':'type',
    'population':'population',
    'boundary':'MULTIPOLYGON',

}
haor_shp=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Haor_Area2.shp'),)
haor_txt=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Haor_Area.txt'),)
haor_wkt=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','h.wkt'),)
structure_shp=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Structures.shp'),)

def test_file(vervose=True):
    p=fromfile(haor_wkt)
    myhaor = get_object_or_404(Haor, pk=2)
    myhaor.boundary = p
    print(p)
    myhaor.save()

def haor_gpd(verbose=True):
    gdf=gpd.read_file(haor_shp)
    print(gdf)
    for data in gdf.to_dict("records"):
        Data_id=data.pop("DataID")
        myhaor = get_object_or_404(Haor, pk=Data_id)
        #print(data)
        geometry_str = str(data.pop('geometry'))
        #geometry = MultiPolygon(fromstr(str(data["geometry"])))
        geometry = MultiPolygon(fromstr(geometry_str))
        myhaor.boundary = geometry
        myhaor.save()
        #print(geometry_str)
        #print(geometry_str)
        try:
            #geometry = GEOSGeometry(geometry_str)
            #geometry2=MultiPolygon(fromstr(str(data["geometry"])))
           print(1)

        except (TypeError, ValueError) as exc:
            # If the geometry_str is not a valid WKT, EWKT or HEXEWKB string
            # or is None then either continue, break or do something else.
            # I will go with continue here.
            print("error encountred......")
            continue

        if geometry.geom_type != 'Polygon':
            # If the created geometry is not a Polygon don't pass it on MyModel
            continue
        #print(geometry)
def structures_gpd(verbose=True):
    gdf = gpd.read_file(structure_shp)
    print(gdf)
    for data in gdf.to_dict("records"):
        sid=data.pop("Dpp_ivt_co")
        mystructue = get_object_or_404(DPP_Intervention,pk=sid)
        print(mystructue.name)
        #print(data)
        #geometry_str = str(data.pop('geometry'))
        #geometry = Point(fromstr(geometry_str))
        geometry_str = data.pop('geometry')
        print(geometry_str.x)
        geometry = Point(geometry_str.x,geometry_str.y)
        mystructue.location=geometry
        mystructue.save()
        print(geometry)
        #myhaor.boundary = geometry
        #myhaor.save()
def saveCentroid(verbose=True):
    gdf = gpd.read_file(haor_shp)
    for data in gdf.to_dict("records"):
        Data_id=data.pop("DataID")
        myhaor = get_object_or_404(Haor, pk=Data_id)
        mypoint=Point(91.025781,24.44398)
        myhaor.centroid=mypoint
        myhaor.save()
        print(mypoint)














    #myhaor.boundary=g
    #print(geoms)

def test_geos(verbose=True):
    p=fromstr('Point(5 23)')
    print(p)



def build_poly(verbose=True):
    #area = fromfile(haor_wkt)
    #print(area)
    print(haor_txt)
    myfile = open(haor_txt, 'r')
    myline = myfile.readline()
    splitline=myline.split(":")
    geometry=splitline[1]
    print(geometry)
    #print(geometry)
    myfile.close()
    #p=GEOSGeometry(geometry)
    p=fromstr(geometry,srid=4326)
    print(p)

    #p = Polygon()
    #print(p)
from pac.models import Invoice_details,Expenditure_details
def getInvoiceTotal(invoice):
    invoice_total=0
    expenditures=Expenditure_details.objects.all().filter(Invoice_details=invoice)
    for exp in expenditures:
        invoice_total+=float(exp.Total)
        print("exp_total={} invoice_total={}".format(exp.Total,invoice_total))
    return invoice_total
def updateInvoiceTotal(verbose=True):
    invoices=Invoice_details.objects.all()
    for invoice in invoices:
        print(invoice.Invoice_no)
        invoice_total=getInvoiceTotal(invoice)
        invoice.Total_amount=invoice_total
        invoice.save()


def run(verbose=True):
    lm = LayerMapping(Haor,haor_shp,haor_mapping, transform=False)
    #lm.save(strict=True, verbose=verbose)
    print(lm)



structure_ids_export_file=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Structid.xlsx'),)

def saveDataFrame(dataframes,filepath,names):
    #writer = pd.ExcelWriter(filepath, engine='xlsxwriter')
    writer=pd.ExcelWriter(filepath)
    for df,sname in zip(dataframes,names):
        df.to_excel(writer,sheet_name=sname)
    writer.save()





from .models import Contract_Intervention,Schedule,BoQ
def exportContractIntervention(verbose=True):
    ids=[]
    names=[]
    packageno=[]
    civts=Contract_Intervention.objects.all()
    for civt in civts:
        ids.append(civt.pk)
        names.append(civt.dpp_intervention_id.name)
        packageno.append(civt.contract_id.package_short_name)
    myframe=pd.DataFrame()
    myframe=myframe.assign(id=ids)
    myframe=myframe.assign(structure_name=names)
    myframe=myframe.assign(package=packageno)
    print(myframe)
    myframes=[]
    mynames=[]
    myframes.append(myframe)
    mynames.append("structure_ids")
    """Creating frames for Item code"""
    items=Schedule.objects.all()
    item_code=[]
    item_description=[]
    itemid=[]
    for item in items:
        item_code.append(item.codeNo)
        item_description.append(item.shortDescription)
        itemid.append(item.pk)
    d={"code":item_code,"description":item_description,"dbase_id":itemid}
    item_dframe=pd.DataFrame(data=d)
    myframes.append(item_dframe)
    mynames.append("schedule_item")
    saveDataFrame(myframes,structure_ids_export_file,mynames)
BoQ_path=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','BoQ.xlsx'),)

def inputBoQItemFromExcel(verbose=True):
    sheet='BoQ'
    myframe=pd.read_excel(BoQ_path,sheet_name=sheet)
    myframe.fillna(0,inplace=True)

    for index,row in myframe.iterrows():
        myframe.iloc[index,5]=myframe.iloc[index,4]*myframe.iloc[index,3]
    #quantity=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    #rate=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    #amount=models.DecimalField(null=True, blank=True, decimal_places=3, max_digits=13)
    #schedule_id=models.ForeignKey(Schedule,on_delete=models.CASCADE,null=True,blank=True)
    #structure_id=models.ForeignKey(Contract_Intervention,on_delete=models.CASCADE,null=True,blank=True)
    BoQ.objects.all().delete()

    for index, row in myframe.iterrows():
        icode = row['Item_Code']
        schedule = Schedule.objects.get(codeNo=icode)
        structure=Contract_Intervention.objects.get(pk=row['structure_id'])
        tendered_qunatity=row['Quantity']
        quoted_rate=row['Rate']
        quoted_amount=row['Amount']
        myboq=BoQ(quantity= tendered_qunatity,rate=quoted_rate,amount=quoted_amount,schedule_id=schedule,structure_id=structure,)
        myboq.save()
        print(myboq)
progress_path=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Work Completion Status.xlsx'),)

from .models import qualitativeStatus
from django.shortcuts import get_object_or_404
def inputProgressQuantityFromExcel(verbose=True):
    sheet="Qstatus"
    myframe = pd.read_excel(progress_path, sheet_name=sheet)
    myframe.fillna(0, inplace=True)
    for index,row in myframe.iterrows():
        sid=row['ids']
        civt=Contract_Intervention.objects.get(pk=sid)
        #wname=(row['Structure/Works Name'])
        #print(wname)
        #dpp_ivt=DPP_Intervention.objects.get(name=wname)
        #print(dpp_ivt)
        #civt=Contract_Intervention.objects.get(dpp_intervention_id__name=wname)
        print(civt.dpp_intervention_id.name)
        pitem=qualitativeStatus.objects.get(contract_ivt=civt)
        pitem.current_progress=row['Current Progress']
        pitem.overall_status=row['Current Status']
        print(pitem.overall_status)
        #print(pitem.current_progress)
        pitem.save()

    #print(myframe)
def massDeleteExp(verbose=True):
    script_path = os.path.realpath(__file__)
    print("script path={}".format(script_path))
    myfolder=os.path.dirname(script_path)
    print("folder={}".format(myfolder))
    input_path=os.path.join(myfolder,"input.txt")
    myfile=open(input_path,'r')
    myline=myfile.readline()
    print(myline)
    mydata= myline.split(":")[1]
    myids2=mydata.split(",")
    myids=[int(x) for x in myids2 ]
    for x in  myids:
        expenditures = Expenditure_details.objects.all().filter(Invoice_details=x)
        for exp in expenditures:
            print(exp)
            exp.delete()
    updateInvoiceTotal()









