from django.test import TestCase

# Create your tests here.
from .models  import Dpp_allocation,Budget_allocation,Invoice_details,Expenditure_details
""" Making Query set  """

myexp=Expenditure_details.objescts.all()
print(myexp)