filename=input('Enter imagefile name with path:');
sprintf('you have entered file name:\n%s',filename)
upper=input('Enter upper limit of segmentation;');
lower=input('Enter lower limit of segmentation:');
[A,R]=geotiffread(filename);
info=geotiffinfo(filename);
 R=info.SpatialRef;
A=medfilt2(A);
B=waterseg(A,upper,lower);
 X = im2uint16(B);
outfile=input('Enter the name of output file:');
geotiffwrite(outfile,X,R,'GeoKeyDirectoryTag', info.GeoTIFFTags.GeoKeyDirectoryTag);
