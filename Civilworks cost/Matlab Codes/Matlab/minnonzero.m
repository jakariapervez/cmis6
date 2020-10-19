function [min1 ] = nonzerominumum( val )
[row,col]=size(val);
rplacevalue=max(val);
for c=1:col
    if val(1,c)==0 
        val(1,c)= rplacevalue;
    end
end
min1=min(val);


%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
