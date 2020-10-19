x = [40  55  33  10  0  5  10  40  NaN  10  25  30  25  10 10  NaN  90  80  65  80  90  NaN];
y = [50  20  0  0  15  25  55  50  NaN  20  10  10  20  30 20  NaN  10  0  20  25  10  NaN];

 S=mapshape()
 S.Geometry='polygon'
 S(1).X=x
 S(1).Y=y
 x=[112 124 132 112]
 y=[19 17 26 19]
  S(2).X=x
 S(2).Y=y
  mapshow(S)