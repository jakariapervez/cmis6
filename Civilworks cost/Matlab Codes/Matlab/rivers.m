function [ output_args ] = rivers()
%RIVERS Summary of this function goes here
%   Detailed explanation goes here
fin=input('name of the nwdi image with full path');
fout=input('name of the output image with full path');
A=imread(fin,'tif');
 level=graythresh(A);
  B=im2bw(A,level);
  [C,D,E,R,F,z)=imagepro3(B);
X=R;
info=geotiffinfo(fin);
R1=info.SpatialRef;
ccode=info.GeoTIFFCodes.PCS;
geotiffwrite(fout,X,R1, 'CoordRefSysCode' ,ccode);
A=imread(fout,'tiff');
imtool(A);
end

