function [ B ] = extendmat1( A )
[row,col]=size(A);
B=zeros(row+2,col+2);
for r=1:row
    for c=1:col
        B(r+1,c+1)=A(r,c);
    end
end

%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
