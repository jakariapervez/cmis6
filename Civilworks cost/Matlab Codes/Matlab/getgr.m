function [grno ] = getgr( origmat,r,c )
[row,col]=size(origmat);
if row>=2 & col>=3
if r~=1
if c==1
scanval=zeros(1,2);
scanval(1,1)=origmat(r-1,c);
scanval(1,2)=origmat(r-1,c+1);
grno=min(scanval);    
elseif c==col   
scanval=zeros(1,3);
scanval(1,1)=origmat(r-1,c-1);
scanval(1,2)=origmat(r-1,c);
scanval(1,3)=origmat(r,c-1);
grno=min(scanval);
else
scanval=zeros(1,4);
scanval(1,1)=origmat(r-1,c-1);
scanval(1,2)=origmat(r-1,c);
scanval(1,3)=origmat(r-1,c+1);
scanval(1,4)=origmat(r,c-1);    
grno=min(scanval);
end
else
if c~=1
grno=origmat(r,c-1);
else
grno=0;
end
end
else
grno =-1;
end

    

%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
