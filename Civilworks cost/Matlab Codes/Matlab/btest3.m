filename=input('enter name of the grayscale ndwi image:');
I=imread(filename);
I=mat2gray(I);
level=graythresh(I);
BW=im2bw(I,graythresh(I));
[B,L,N,A]=bwboundaries(BW);
   S=mapshape();
       S.Geometry='polygon';
       dlm=NaN;
       for k=1:length(B)    
           boundary=B{k};
           x=boundary(:,2);
           y=boundary(:,1);
           x=x';
           y=y';
           [x,y]=poly2cw(x,y);
          if (~sum(A(k,:)))
          S(k).type='parent';
         
          else
               S(k).type='hole'; 
          end
               for l=find(A(:,k))'
                  holeboundary=B{l};
                   xhole= holeboundary(:,2);
                   yhole=holeboundary(:,1);
                   xhole=xhole';
                   yhole=yhole';
                   [xhole,yhole]=poly2ccw(xhole,yhole);
                   x=horzcat(x,dlm);
                   y=horzcat(y,dlm);
                   x=horzcat(x,xhole);
                   y=horzcat(y,yhole);
               end
         
          
           S(k).X=x;
           S(k).Y=y;
       end
       