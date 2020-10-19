function [A] = extendmatrix(B)
[row,col]=size(B);
C=zeros(1,col);
A=[B;C];

%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
