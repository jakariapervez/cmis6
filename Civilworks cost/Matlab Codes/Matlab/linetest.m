latparis =  48.87084; lonparis =   2.41306;   % Paris coords
latsant  = -33.36907; lonsant  = -70.82851;   % Santiago
latnyc   =  40.69746; lonnyc   = -73.93008;   % New York City
[Cities(1:3).Geometry] = deal('Point');
Cities(1).Lat = latparis; Cities(1).Lon = lonparis;
Cities(2).Lat = latsant;  Cities(2).Lon = lonsant;
Cities(3).Lat = latnyc;   Cities(3).Lon = lonnyc;
Cities(1).Name = 'Paris';
Cities(2).Name = 'Santiago';
Cities(3).Name = 'New York';
axesm('mercator','grid','on','MapLatLimit',[-75 75]); tightmap; 
% Map the geostruct with the continent outlines
geoshow('landareas.shp')

% Map the City locations with filled circular markers
geoshow(Cities,'Marker','o',...
    'MarkerFaceColor','c','MarkerEdgeColor','k');

% Display the city names using data in the geostruct field Name.
% Note that you must treat the Name field as a cell array.
textm([Cities(:).Lat],[Cities(:).Lon],...
    {Cities(:).Name},'FontWeight','bold');
[Tracks(1:3).Geometry]=deal('line')

% Create a text field identifying kind of track each entry is.
% Here they all will be great circles, identified as 'gc'
% (string signifying great circle arc to certain functions)
trackType = 'gc';
[Tracks.Type] = deal(trackType);
% Give each track an identifying name
Tracks(1).Name = 'Paris-Santiago';
[Tracks(1).Lat, Tracks(1).Lon] = ...
        track2(trackType,latparis,lonparis,latsant,lonsant);

Tracks(2).Name = 'Santiago-New York';
[Tracks(2).Lat, Tracks(2).Lon] = ...
        track2(trackType,latsant,lonsant,latnyc,lonnyc);

Tracks(3).Name = 'New York-Paris';
[Tracks(3).Lat, Tracks(3).Lon] = ...
        track2(trackType,latnyc,lonnyc,latparis,lonparis);
    % The distance function computes distance and azimuth between
% given points, in degrees. Store both in the geostruct.
for j = 1:numel(Tracks)
    [dist, az] = ...
        distance(trackType,Tracks(j).Lat(1),...
                           Tracks(j).Lon(1),...
                           Tracks(j).Lat(end),...
                           Tracks(j).Lon(end));
    [Tracks(j).Length] = dist;
    [Tracks(j).Azimuth] = az;
end
    % Inspect the first member of the completed geostruct
Tracks(1)
% On cylindrical projections like Mercator, great circle tracks
% are curved except those that follow the Equator or a meridian.

% Graphically differentiate the tracks by creating a symbolspec;
% key line color to track length, using the 'summer' colormap.
% Symbolspecs make it easy to vary color and linetype by
% attribute values. You can also specify default symbologies.

colorRange = makesymbolspec('Line',...
            {'Length',[min([Tracks.Length]) ...
              max([Tracks.Length])],...
             'Color',winter(3)});
%geoshow(Tracks,'SymbolSpec',colorRange);

fname=[pwd '\' 'citylocs.shp'];
shapewrite(Cities,fname);
fname=[pwd '\' 'citytracks.shp'];
shapewrite(Tracks,fname);