import os
import pandas as pd
def disPlayDF(mydf):
    for index,row in mydf.iterrows():
        print(row)

def createLivelihoodExpenditure(verbose=True):
    myfolder=os.path.abspath(os.path.dirname(__file__))
    print(myfolder)
    exp_input_path=os.path.join('LH.csv')
    input_df=pd.read_csv(exp_input_path)
    disPlayDF(input_df)