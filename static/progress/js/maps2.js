$(function () {

var map
var getPresentStatus=function (statuscode)
{
var structure_status
if (statuscode=="C")
{
 structure_status="Completed"
}	
else if (statuscode=="OG")	
{
structure_status="Construction on going"	
	
}
else
{
	
structure_status="Construction is Stopped due to problems"	
	
}
return structure_status
}
var addStructureLocation= function (mydata)

{
var marker
var information
lats=mydata.lats
lons=mydata.lons
names=mydata.names
ps=mydata.current_status
pp=mydata.present_progress

for (i=0;i<lats.length;i++)	
{
status_text=getPresentStatus(ps[i])
information="<b>"+names[i]+"</b>"+"<br>"+"<b>"+"present status:" +"</b>"+status_text+"<br>"+"<b>"+"present progress:"+"</b>"+pp[i]*100+"%"

marker = L.marker([lats[i],lons[i]]).addTo(map);	
marker.bindPopup(information)	
}
	
	
}//end of addition of marker	
var createBoundaryPoints=function (mycoords)
{
var lat
var lon
var mypoint
var pointlist= new Array();
coordPoint=mycoords[0]
for (i=0;i<coordPoint.length;i++)
{
//console.log(coordPoint[i])	
lon=coordPoint[i][0]
lat=coordPoint[i][1]
pointlist.push(new L.LatLng(lat,lon))	
//console.log(lat)
//console.log(lon)
	
}
console.log(pointlist)
/*
mycoords.forEach(function (item,index,array)
{
lon=item[index][0]
lat=item[index][1]
mypoint=new L.LatLng(lat,lon)
pointlist.push(new L.LatLng(lat,lon))
//console.log(lat)
//console.log(lon)
}




)*/	

return 	pointlist
	
}	
//center:[24.44398,91.025781],
var mapOptions={

center:[24.450065, 91.039566],
zoom:21	
}	
	
	
var showMap=function (mydata) 
{
	 	
	
		//console.log(mydata.boundary)
accessToken = 'pk.eyJ1IjoiamFrYXJpYXBlcnZleiIsImEiOiJjazY0dnJ4ZmkxMGhzM29wZXUxOWw4dTV4In0.ZFnBYL5QF_8aLrddh2JqBA';	
 map= new L.map('map',mapOptions)
//var layer= new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png')
//'https://api.mapbox.com/styles/v11/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}'
var layer= new L.TileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}')
//map.addLayer(layer)
var mapboxTiles = L.tileLayer('https://api.mapbox.com/styles/v1/mapbox/satellite-v9/tiles/{z}/{x}/{y}?access_token=' + accessToken, {
       attribution: '© <a href="https://www.mapbox.com/feedback/">Mapbox</a> © <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
       tileSize: 512,
       zoomOffset: -1
});
map.addLayer(mapboxTiles)
var myStyle = {
    "color": "#ff7800",
    "weight": 5,
    
};
 var pointA = new L.LatLng(24.510698, 90.995575);
 var pointB = new L.LatLng(24.378833, 91.088681);
//var pointList = [pointA, pointB];
var pointList= createBoundaryPoints(mydata.boundary_coords)
//console.log(pointList)
 var firstpolyline = new L.Polyline(pointList, {
    color: 'red',
    weight: 3,
    opacity: 0.5,
    smoothFactor: 1

    });

map.addLayer(firstpolyline);
addStructureLocation(mydata);

//var myLines = [{"type": "LineString","coordinates":mycoords }]
//L.geoJSON(mydata.boundary,{style: myStyle}).addTo(map);
//L.geoJSON(myLines,{style: myStyle}).addTo(map);		
	map_is_created=true	
		


} //closing of showmap


	
var getMapData=function()

{
myurl=$("#map-haor-sort option:selected").attr("data-url")
console.log(myurl)
$.ajax({
	url:myurl,
    data:{"haorid":$("#map-haor-sort option:selected").val()},
      type: 'get',
      dataType: 'json',
	  beforeSend:function ()
	  {
		if (map != null) {
        map.remove();
        map = null;
		  
		  
	  }},
	  success: function (data) {
      console.log("sucessfully returned from ajax request.....")
	  //console.log(data.boundary)
	  console.log(data.names)
	  showMap(data)
     
	  
	  
	 
      }
	
	
	
	
	
	
});
	
	
	
}
 
//$(".js-map").click(showmap);
$(".js-sort-map").on("click",getMapData)
 
 
 
});