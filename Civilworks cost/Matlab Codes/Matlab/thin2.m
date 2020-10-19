function [B,AA ] = thin2( A )
tic
[row1,col1]=size(A);
D=zeros(row1,col1);
finished=0;
iter=0;
count=0;
AA=extendmat1(A);
[row2,col2]=size(AA);
BB=AA;
changed=0;
while changed==0 & iter<=200
% strip north
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
        if((~A3 & A0 & A7)&(chi==2)&(sigma~=1))
                       BB(r,c)=0;
        else
            BB(r,c)=AA(r,c);
          
         
        end
        
       % D(r,c)=sigma;
    end
end
r=2;
c=2;
% strip north
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
        if((A3 & A0 & ~A7)&(chi==2)&(sigma~=1))
                       BB(r,c)=0;
        else
            BB(r,c)=AA(r,c);                  
        end
        % D(r,c)=sigma;
    end
end
%stipr east
r=2;
c=2;

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
        if((A1 & A0 & ~A5)&(chi==2)&(sigma~=1))
                       BB(r,c)=0;
        else
            BB(r,c)=AA(r,c);                  
        end
        % D(r,c)=sigma;
    end
end
r=2;
c=2;
%stipr west
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
        if((~A1 & A0 & A5)&(chi==2)&(sigma~=1))
                       BB(r,c)=0;
        else
            BB(r,c)=AA(r,c) ;                
        end
         % D(r,c)=sigma;
    end
end
iter=iter+1
changed=isequal(AA,BB);
AA=BB;
end
B=shrinkmat(BB);
totaltime=toc
%end
%B=shrinkmat(AA);
%exec=toc
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
