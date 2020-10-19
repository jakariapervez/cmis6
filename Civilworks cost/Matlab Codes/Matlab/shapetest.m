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
