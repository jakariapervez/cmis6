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
    invoices=Invoice_details.objects.all().filter(FinancialYear=5)
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
from .models import Contract_Intervention
def exportContractIntervention(verbose=True):
    ids=[]
    names=[]
    civts=Contract_Intervention.objects.all()
    for civt in civts:
        ids.append(civt.pk)
        names.append(civt.dpp_intervention_id.name)
    myframe=pd.DataFrame()
    myframe=myframe.assign(id=ids)
    myframe=myframe.assign(structure_name=names)
    print(myframe)

