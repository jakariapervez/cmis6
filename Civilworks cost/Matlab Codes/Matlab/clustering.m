function [A,B,cmat ] = clustering( myimage )
%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
[row,col]=size(myimage);
%[A,B]=imhist(myimage)
D=zeros(row,col);
clustermat=zeros(1,6);
 for r=1:row
     for c=1:col          
         if myimage(r,c)==1;
             r;
             c;
             grno=getgr3(D,r,c);
             if grno==0
                 %creating a  new record clustermatrix which keep tracks of
                 %group
              clustermat=extendmatrix(clustermat);
             [ lr,lc]=size(clustermat);
             newgr=clustermat(lr-1,1)+1;
              clustermat(lr,1)=newgr;  % increasing group no from last no
              clustermat(lr,2)= 1;
              clustermat(lr,3)= r;
              clustermat(lr,4)= c;
              clustermat(lr,5)= r;
              clustermat(lr,6)= c;
              D(r,c)= newgr; %assining values to the pixel
             D(r,c);
            
             else
                D(r,c)=grno;
                clustermat(grno+1,2)=clustermat(grno+1,2)+1;
                clustermat(grno+1,5)=r;
                if clustermat(grno+1,6)<c
                clustermat(grno+1,6)=c;
                end
             end    
         end
     end
     
 end
[row1,col1]=size(clustermat);
r=1;
c=1;
mmat=zeros(row1-1,col1);
for r=2:row-1
    for c=1:col1
        mmat(r-1,c)=clustermat(r,c);
    end
end
 
A=myimage;
B=D;
cmat=mmat;
    

