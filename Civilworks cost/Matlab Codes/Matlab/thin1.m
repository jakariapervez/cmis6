function [B,AA ] = thin1( A )
[row1,col1]=size(A)
tic
D=zeros(row1,col1);
finished=0;
iter=0;
count=0;
AA=extendmat1(A);
[row2,col2]=size(AA)
while finished==0 
    r=2;
    c=2;
    finished=1;
for r= 2:row2-1
    for c=2:col2-1                
        A0=AA(r,c);
        A1=AA(r,c+1);
        A2=AA(r-1,c+1);
        A3=AA(r-1,c);
        A4=AA(r-1,c-1);
        A5=AA(r,c-1);
        A6=AA(r+1,c-1);
        A7=AA(r+1,c);
        A8=AA(r+1,c+1);
        sigma=A1+A2+A3+A4+A5+A6+A7+A8;
        chi=(A1~=A3)+(A3~=A5)+(A5~=A7)+(A7~=A1)+2*((~A1 & A2 & ~A3)+(~A3 & A4 & ~A5)+(~A5 & A6 & ~A7)+(~A7 & A8 & ~A1));
        if(A0==1 & chi==2 & sigma ~=1)
                       AA(r,c)=0;
           finished=0;
         
        end
        
       % D(r,c)=sigma;
    end
end
iter=iter+1
end
B=shrinkmat(AA);
exec=toc
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
