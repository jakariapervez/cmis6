function [chi ] =findchi(A )
r=2;
c=2;
A1=A(r,c+1);
        A2=A(r-1,c+1);
        A3=A(r-1,c);
        A4=A(r-1,c-1);
        A5=A(r,c-1);
        A6=A(r+1,c-1);
        A7=A(r+1,c);
        A8=A(r+1,c+1);
chi=(A1~=A3)+(A3~=A5)+(A5~=A7)+(A7~=A1)+2*((~A1 & A2 & ~A3)+(~A3 & A4 & ~A5)+(~A5 & A6 & ~A7)+(~A7 & A8 & ~A1))
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
