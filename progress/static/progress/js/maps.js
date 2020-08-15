$(function () {
	
var addBoundary=function (mydata)
{
	
	
	
	
}	

	
	
	
var showMap=function (mydata) 
{
//console.log(mydata.boundary)
mapboxgl.accessToken = 'pk.eyJ1IjoiamFrYXJpYXBlcnZleiIsImEiOiJjazY0dnJ4ZmkxMGhzM29wZXUxOWw4dTV4In0.ZFnBYL5QF_8aLrddh2JqBA';	
var map= new mapboxgl.Map({
	
container: 'map',
style: 'mapbox://styles/mapbox/satellite-streets-v11',
center: [ 91.27817518,24.39824583],
 zoom:17
	
});	
map.on('load',function(mydata)
{
map.addSource('maine', {
	'type': 'geojson',
	'data':mydata.boundary
	
	
}) //closing of addsoure	

map.addLayer({
'id': 'maine',
'type': 'fill',
'source': 'maine',
'layout': {},
'paint': {
'fill-color': '#088',
'fill-opacity': 0.8
}
});



	
	
})//clising of on load


} //closing of showmap


	
var getMapData=function()

{
myurl=$("#map-haor-sort option:selected").attr("data-url")
console.log(myurl)
$.ajax({
	url:myurl,
    data:{"pk":$("#map-haor-sort option:selected").val()},
      type: 'get',
      dataType: 'json',
	  success: function (data) {
      console.log("sucessfully returned from ajax request.....")
	  showMap(data)
	 
      }
	
	
	
	
	
	
});
	
	
	
}
 
//$(".js-map").click(showmap);
$(".js-sort-map").on("click",getMapData)
 
 
 
});