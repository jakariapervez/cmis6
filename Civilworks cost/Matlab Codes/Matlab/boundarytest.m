filename=input('enter name of the grayscale ndwi image:');
I=imread(filename);
I=mat2gray(I);
level=graythresh(I)
BW=im2bw(I,graythresh(I));
[B,L,N,A]=bwboundaries(BW);
 
     S=mapshape();
     S.Geometry='polygon';
     for k=1:length(B)
        boundary = B{k};
        x=boundary(:,1);
        y=boundary(:,2);
        S(k).X=x;
        S(k).Y=y;
        S(k).number=k;
     end
       shapewrite(S,'jamuna.shp');
        