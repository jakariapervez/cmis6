function [ insmat ] = insmat(ra1,ca1,ra2,ca2,rb1,cb1,rb2,cb2 )
insmat=zeros(1,5)
cx1=(ra1+ra2)/2
cy1=(ca1+ca2)/2
rad1=sqrt((ra1-ra2)^2+(ca1-ca2)^2)
cx2=(rb1+rb2)/2
cy2=(cb1+cb2)/2
rad2=sqrt((rb1-rb2)^2+(cb1-cb2)^2)
[xout,yout]=circ(cx1,cy1,rad1,cx2,cy2,rad2)
if ~nan 
    cx=(x(1)+x(2))/2
    cy=(y(1)+y(2))/2


%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
