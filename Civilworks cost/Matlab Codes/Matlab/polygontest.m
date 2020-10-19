[mypolygon(1:1).Geometry]=deal('polygon');
x = [40  55  33  10  0  5  10  40  NaN  10  25  30  25  10,10  NaN  90  80  65  80  90  NaN];
y = [50  20  0  0  15  25  55  50  NaN  20  10  10  20  30,20  NaN  10  0  20  25  10  NaN];
mypolygon(1).X=x;
mypolygon(1).Y=y;
mypolygon(1).number=1;
shapewrite(mypolygon,'mypoly.shp')
 mapshow(mypolygon(1))