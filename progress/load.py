import os
from django.contrib.gis.utils import LayerMapping
from  .models import Haor
from django.contrib.gis.geos import fromstr,Polygon,GEOSGeometry,fromfile,fromstr,MultiPolygon
import geopandas as gpd
from django.shortcuts import render, get_object_or_404, redirect
haor_mapping={
    'name':'name',
    'area':'area',
    'project_type':'type',
    'population':'population',
    'boundary':'MULTIPOLYGON',

}
haor_shp=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Haor_Area.shp'),)
haor_txt=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Haor_Area.txt'),)
haor_wkt=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','h.wkt'),)

def test_file(vervose=True):
    p=fromfile(haor_wkt)
    myhaor = get_object_or_404(Haor, pk=2)
    myhaor.boundary = p
    print(p)

def test_gpd(verbose=True):
    gdf=gpd.read_file(haor_shp)
    print(gdf)

    print(myhaor)
    geoms=gdf['geometry']
    g=geoms[0]
    g2=Polygon(fromstr(str(g)))
    print(g)


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


def run(verbose=True):
    lm = LayerMapping(Haor,haor_shp,haor_mapping, transform=False)
    #lm.save(strict=True, verbose=verbose)
    print(lm)