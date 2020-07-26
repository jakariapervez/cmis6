$(document).ready(function()
{
function editUser()
{
	
console.log("sucessfully Trapped Edit Event....")	
	
}
var editReport=function (pelement)
{
console.log(pelement.attr("data-url"))	
	
	
}
function appendToMessageTable(data)
{
$("#messageTable > tbody").html(data.tbody)	

	
}




$("form#addMessage").submit(function()
{
console.log("Sucessfully Trapped Add Form Submit Event.....");	
console.log($(":button"))	
var nameInput=$('input[name="name"]').val().trim();	
console.log("Name="+nameInput);	
var stDate=$('input[name="start-date"]').val().trim();
var fnDate=$('input[name="finish-date"]').val().trim();
var lsDate=$('input[name="submission-date"]').val().trim();
var reportingPerson=$("#reporting-person").val()
var myurl=$("form#addMessage").attr("data-url")
console.log("Name="+nameInput+"start="+stDate+"finish="+fnDate+"last submission"+lsDate+"reporting person="+reportingPerson);
console.log("myurl="+myurl)
if(stDate && fnDate && lsDate && reportingPerson  )
{
$.ajax({

url:myurl,	
	
data:{
  'name':nameInput,
 'stdate': stDate,
  'fdate': fnDate,
  'subdate':lsDate,
 'persons':reportingPerson },
dataType: 'json',
beforeSend:function (){
	$("#messageTable > tbody").html("")
	},
success:function (data) 
{
console.log("sucessfully returned from AJAX request....")
console.log(data)	
appendToMessageTable(data)
}

	
});
}
else 
{
alert("All fields must have a valid value.");	
}


			
}


);
$("button").click(function (){
console.log("sucessfully trapped edit button clic...")
})	
});
 