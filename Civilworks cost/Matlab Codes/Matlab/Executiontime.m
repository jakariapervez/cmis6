function [B,C,D ] =Executiontime( A )

%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
tic
[J K L M]=bwboundaries(A,8);
[row,col]=size(A)
C=toc
tic
for i=1:row
    for j=1:col
     if (A(i,j)==1)
         C(i,j)=0;
     else
          C(i,j)=1;
     end
     
    end
end

B=toc
D=C/B