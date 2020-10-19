function [ ints,intersect ] = precst( A,B )
%UNTITLED2 Summary of this function goes here
%Detailed explanation goes here
ints=zeros(1,4)
ints(1,1)=(B(1,1)>=A(1,1)) & (B(1,1)>=A(1,3))
ints(1,2)=(B(1,2)>=A(1,2)) & (B(1,1)>=A(1,4))
ints(1,3)=(B(1,1)>=A(1,1)) & (B(1,1)>=A(1,3))
ints(1,4)=(B(1,1)>=A(1,2)) & (B(1,1)>=A(1,4))

