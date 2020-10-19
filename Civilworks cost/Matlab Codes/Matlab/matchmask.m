function [ matched] =matchmask(mymat,mymask )
[r1,C1]=size(mymat)
[r2,c2]=size(mymask)
%normalizing rows for matching size of mask
r3=r1-r2+1
c3=c1-c2+1
for r=1:r3
    for c=1:c3
       myout=compareablemat(mymat,r2,c2,r,c) 
    end
end



%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
