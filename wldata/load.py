import os
import pandas as pd

def inputMonthlyWL(verbose=True):
    mypath=os.path.join(os.path.dirname(__file__),'data.txt')
    myfile=open(mypath,'r')
    myline=myfile.readline()
    print(myline)
    mytexts= myline.split(":")
    month=int(mytexts[1])
    print(month)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    year = int(mytexts[1])
    print(year)
    myline = myfile.readline()
    print(myline)
    mytexts = myline.split(":")
    gauge = mytexts[1].rstrip("\n")
    print(gauge)

    myfile.close()