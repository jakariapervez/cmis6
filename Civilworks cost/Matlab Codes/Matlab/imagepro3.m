function [C,D,E,R,F,z] = imagepro3(A)
%parameter identification
% C is the labeled matrix
% D is the region group matrix contains information about Each region 
% E is the number of the largest group
% R is the correcteted image containing the largest group i.e. river
% network
% z is the execution time
%input the path of image
% F is the Distance Matrix
tic
%path=input('Enter path of Image:')
%A=imread(path);
%figure1=imshow(A);
B=im2bw(A);
C=bwlabel(B);
D=regionprops(C,'All');
[M,N]=size([D.Area]);
maxarea=max([D.Area])
for i=1:N
    if D(i).Area==maxarea;
        E=i;
    end
    
    
end
% seprating the largest contious group from other gropus
[row1,col1]=size(C);
maskmat=ones(row1,col1);
maskmat=maskmat*E;
R=C == maskmat;
rprime=~R;
%Calculating the distance matrix
F=bwdist(rprime,'euclidean');
z=toc;



%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
