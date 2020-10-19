function [B] = setNan( A )
%UNTITLED2 Summary of this function goes here
%this function set nan value to zero in landsat image.
%   Detailed explanation goes here
[m,n]=size(A);
B=NaN(m,n);
for i=1:m
    for j=1:n
        if A(i,j)~=0 
            B(i,j)=A(i,j);
                  
        end
        
    end
end

end

