function [ clno ] = clnor1(image1,c,curcl )
%UNTITLED1 Summary of this function goes here
%   Detailed explanation goes here
if c==1 
    clno=1
else
    if image1(1,c-1)==0 
        clno=curcl+1
    elseif image1(1,c-1)==1
        clno=curcl
    end
end
        