function [Xthin,B ] = dmax(A)
[row,col]=size(A);
B=zeros(row,col);
for r=2:row-1
    for c=2:col-1
        if (A(r,c)>0 && max([A(r,c+1),A(r-1,c+1),A(r-1,c),A(r-1,c-1),A(r,c-1),A(r+1,c-1),A(r+1,c),A(r+1,c+1)]))
            B(r,c)=A(r,c);
        else
            B(r,c)=0; 
        end
    end
end
    Xthin=1
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
