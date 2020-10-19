function [B] =waterseg(A,upper,lower)
%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
[m,n]=size(A);
B=zeros(m,n);
for i=1:m
    for j=1:n
        if( A(i,j)>=lower && A(i,j)<=upper)
            B(i,j)=1;
        
        end
            
    end
end

