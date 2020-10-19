function [ B ] = shrinkmat( A )
[row,col]=size(A);
B=zeros(row-2,col-2);
for r=1:row-2
    for c=1:col-2
        B(r,c)=A(r+1,c+1);
    end
end
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
