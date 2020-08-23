$(function () {

var map	
var addBoundary=function (mydata)
{
	
	
	
	
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
mycoords=[[24.510698,90.995575],[ 24.378833,91.088681]]
var myLines = [{"type": "LineString","coordinates":mycoords }]
//L.geoJSON(mydata.boundary,{style: myStyle}).addTo(map);
L.geoJSON(myLines,{style: myStyle}).addTo(map);		
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
	  console.log(data.boundary_coords)
	  showMap(data)
     
	  
	  
	 
      }
	
	
	
	
	
	
});
	
	
	
}
 
//$(".js-map").click(showmap);
$(".js-sort-map").on("click",getMapData)
 
 
 
});