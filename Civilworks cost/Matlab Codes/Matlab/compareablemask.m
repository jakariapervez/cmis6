function [ ooutmat ] = compareablemat(inputmat,r1,c1,i,j )
outmat=zeros(r1,c1)
for r=1:r1
    for c=1:c1
      outmat(r,c)=inputmat(i,j)
      j=j+1
    end
    i=i+1
end

%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
