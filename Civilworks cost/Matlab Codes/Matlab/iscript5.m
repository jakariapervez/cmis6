filename=input('Enter imagefile name with path:');
sprintf('you have entered file name:\n%s',filename)
upper=input('Enter upper limit of segmentation;');
lower=input('Enter lower limit of segmentation:');
[A,R]=geotiffread(filename);
info=geotiffinfo(filename);
 R=info.SpatialRef;
A=medfilt2(A);
B=waterseg(A,upper,lower);
[labmat,rgroup,largest_group_id,rect_image,distmat,exectime]=imagepro4(B);

outfile=input('Enter the name of output file:');
 X =rect_image;
%X=~rect_image;
%Y=mat2gray(X);
geotiffwrite(outfile,X,R,'GeoKeyDirectoryTag', info.GeoTIFFTags.GeoKeyDirectoryTag);
outfile=input('Enter the name of label image:');
X=labmat;
X=int16(X);
geotiffwrite(outfile,X,R,'GeoKeyDirectoryTag', info.GeoTIFFTags.GeoKeyDirectoryTag);
