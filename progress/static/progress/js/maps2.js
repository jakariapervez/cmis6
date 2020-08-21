$(function () {
	
var addBoundary=function (mydata)
{
	
	
	
	
}	

	
	
	
var showMap=function (mydata) 
{
//console.log(mydata.boundary)
accessToken = 'pk.eyJ1IjoiamFrYXJpYXBlcnZleiIsImEiOiJjazY0dnJ4ZmkxMGhzM29wZXUxOWw4dTV4In0.ZFnBYL5QF_8aLrddh2JqBA';	
var mymap=L.map("map").setView([[51.505, -0.09],13)
L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox/streets-v11',
    tileSize: 512,
    zoomOffset: -1,
    accessToken:accessToken
}).addTo(mymap);

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