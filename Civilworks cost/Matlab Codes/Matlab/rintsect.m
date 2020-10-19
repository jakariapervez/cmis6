function [ z ] = rintsect( A,B )
z=zeros(1,5);
left=max([A(1,1),B(1,1)]);
right=min([A(1,3),B(1,3)]);
top=max([A(1,2),B(1,2)]);
bottom=min([A(1,4),B(1,4)]);
if (right>=left & bottom>=top)
    z(1,1)=1;
    z(1,2)=left;
    z(1,3)=top;
    z(1,4)=right;
    z(1,5)=bottom;
end





%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
