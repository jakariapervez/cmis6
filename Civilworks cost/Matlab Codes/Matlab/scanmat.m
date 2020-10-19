function [found,row,col ] =scanmat( mymat,scanval )
[row1,col1]=size(mymat);
for r=1:row1
    for c=1:col1
      if scanval==mymat(r,c)
         found=1;
         row=r;
         col=c;
      end
    end
end
%UNTITLED2 Summary of this function goes here
%Detailed explanation goes here
