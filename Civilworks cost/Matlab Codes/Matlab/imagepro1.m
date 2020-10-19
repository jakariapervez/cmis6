%imports an image from specfied file location
path=input('Enter path of Image:')
A=imread(path);
B=im2bw(A);
C=B;
figure1= imshow(B);
[J K L M]=bwboundaries(A,8);
%counting the maximum pixel containgin group
maxsize=1
grno=1
for P=1:L
    CC=find(K==P)
    COUNT=size(CC,1);
    if COUNT>maxsize
        maxsize=COUNT
        grno=P
    end
end

[row,col]=size(K)

for i=1:row
    for j=1:col
     if (K(i,j)==grno)
         C(i,j)=1;
     else
          C(i,j)=0;
     end
     
    end
end
figure2=imshow(C);



