import os
from django.contrib.gis.utils import LayerMapping
from  .models import Haor

name = models.CharField(max_length=400)
area = models.DecimalField(null=True, blank=True, decimal_places=2, max_digits=8)
project_type = models.CharField(max_length=50, choices=Types)
population = models.DecimalField(max_digits=8, decimal_places=0, blank=True, null=True, default=0)
boundary = PolygonField(null=True, blank=True)

haor_mapping={
    'name':'name',
    'area':'ara',
    'project_type':'project_type',
    'population':'population',
    'boundary':'MULTIPOLYGON',

}
haor_shp=os.path.abspath(os.path.join(os.path.dirname(__file__),'data','Haor_Area.shp'),)