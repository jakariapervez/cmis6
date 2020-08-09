$(function () {
	var showStatus=false;
	mapboxgl.accessToken = 'pk.eyJ1IjoiamFrYXJpYXBlcnZleiIsImEiOiJjazY0dnJ4ZmkxMGhzM29wZXUxOWw4dTV4In0.ZFnBYL5QF_8aLrddh2JqBA';
	var map
var showmap= function (){
	if (showStatus)
	{
	 console.log(showStatus)
	 map.remove()
	 showStatus=false;
	}
	else
	{
	console.log(showStatus)
	 map = new mapboxgl.Map({
container: 'map',
style: 'mapbox://styles/mapbox/streets-v11',
center: [91.2781751,24.39824583],
 zoom:12

});	
	showStatus=true;	
	}
	
	
	
	
	
}
 
$(".js-map").click(showmap);
 
 
 
 
});