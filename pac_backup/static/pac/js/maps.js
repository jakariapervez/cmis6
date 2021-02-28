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
style: 'mapbox://styles/mapbox/satellite-streets-v11',
center: [ 91.27817518,24.39824583],
 zoom:17

});	
var popup =new mapboxgl.Popup({offset:25}).setText("Box Drainage Outlet")
var marker=new mapboxgl.Marker()
.setLngLat([91.27817518, 24.39824583])
.setPopup(popup)
.addTo(map);
	showStatus=true;	
	}
	
	
	
	
	
}
 
$(".js-map").click(showmap);
 
 
 
 
});