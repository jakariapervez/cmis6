filename=input('Enter imagefile name with path:');
sprintf('you have entered file name:\n%s',filename)
[A,R]=geotiffread(filename);
A=medfilt2(A);
extfilename=input('name of the text file containing extend data with path:');
result=dlmread(extfilename,',');
[r1,c1]=map2pix(R,result(1,1),result(1,2));
[r2,c2]=map2pix(R,result(1,3),result(1,4));
%converting coordinates to integer value'
r1=int64(r1);
c1=int64(c1);
r2=int64(r2);
c2=int64(c2);
s=A(r1:r2,c1:c2);
level=graythresh(s)
B=im2bw(s);
[labmat,rgroup,largest_group_id,rect_image,distmat,exectime]=imagepro4(B);
info=geotiffinfo(filename);
outfile=input('Enter the name of output file:');
[m,n]=size(A);
X=zeros(m,n);
 X(r1:r2,c1:c2)=rect_image(1:r2-r1+1,1:c2-c1+1);
%X=~rect_image;
%Y=mat2gray(X);
geotiffwrite(outfile,X,R,'GeoKeyDirectoryTag', info.GeoTIFFTags.GeoKeyDirectoryTag);