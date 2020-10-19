function [cl,clw] = thin3(A )
%input image with river pixel only
%cl of the channel
% cl of the channel after removing spurr

A2=medfilt2(A,[3 3]);
cl=bwmorph(A2,'skel',Inf);
clw=bwmorph(cl,'spur',Inf);
%UNTITLED3 Summary of this function goes here
%   Detailed explanation goes here
