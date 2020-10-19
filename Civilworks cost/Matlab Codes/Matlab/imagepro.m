%A= imread('F:\matImages\LW4.tif','TIFF');
p=input('image path:')
A=imread(p);
%imshow(A);
bw=im2bw(A);
%imshow(bw);
[X,Y]=clustering(bw);
