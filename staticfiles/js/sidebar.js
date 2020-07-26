$(document).ready(function () {
	/*Variables for Progress */
	
	
var loadForm=function ()

{
	var btn = $(this);
$.ajax({
	url:btn.attr("data-url"),
	type:'get',
	dataType:'json',
	beforeSend:function ()
	{
	//
	//$("#reprotmodal").modal("show");	
	},
	success:function (data)
	{
	console.log("Sucessfully returned from ajax form get rerquest");
	//console.log(data.html_form);
    //$("#reprotmodal")	
	$("#reportmodal .modal-content").html("");
	
	//$("#reportmodal .modal-content").html(data.html_form);
	modal=$("#reprotmodal .modal-content")
	console.log(modal);
	//modal.html("<p> welcom </p>")
	modal.html(data.html_form)
	
    //$("#reprotmodal").modal("show");	
	$("#reprotmodal").modal("show");
	}
});	
	
}
	
  var saveForm = function () {
	  console.log("sending post request")
    var form = $(this);
    $.ajax({
      url: form.attr("action"),
      data:form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("Sucessfully saved report event")
		  console.log(" data is Valid");
		  //console.log("sending post request");
          //$("#progress_item_table tbody").html(data.html_ivt_list);
		  //$("#book-table tbody").html("");
		  //$("#book-table tbody").html(data.html_ivt_list);
          $("#reprotmodal").modal("hide");
        }
        else {
		  console.log("Intervention data is InValid")
		  modal=$("#reprotmodal .modal-content")
			console.log(modal);
			//modal.html("<p> welcom </p>")
			modal.html(data.html_form)
		  //$("#reprotmodal").modal("hide");
          //$("#reprotmodal .modal-content").html(data.html_form);
        }
      }
    });
    return false;
  };	
	
var createChart=function(myid,mydata)
{
	console.log(mydata)
	var chart = {
		backgroundColor: '#FFFFFF',
               type: 'column'
            };
            var title = {
               text: 'Component Progress'   
            };    
            var xAxis = {
               categories: mydata.name,
			   labels: {
            style: {
				fontWeight: 'bold',
                color: 'black',
				fontSize: '16px'
            }
        }
            };
	
	
	            var yAxis = {
               min: 0,
               title: {
                  text: 'Progress in Percent',
				  style: {
                     fontWeight: 'bold',
                    // color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					color: 'black',
					fontSize: '18px'
                  }
               },
               stackLabels: {
                  enabled: true,
                  style: {
                     fontWeight: 'bold',					 
                    // color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					color: 'black',
					fontSize: '24px'
                  }
               },
			    labels: {
            format: '{value} %',
			style: {
                     fontWeight: 'bold',					 
                    // color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					color: 'black',
					fontSize: '16px'
                  }
        }
			   
			   
            };
	   var legend = {
               align: 'right',
               x: -30,
               verticalAlign: 'top',
               y: 0,
               floating: true,
               
               backgroundColor: (
                  Highcharts.theme && Highcharts.theme.background2) || 'white',
               borderColor: '#CCC',
               borderWidth: 2,
               shadow: false,
			   itemStyle: {
                     fontWeight: 'bold',					 
                    // color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					color: 'black',
					fontSize: '18px'
                  }
			   
			  
            }; 
			var tooltip = {
               formatter: function () {
                  return '<b>' + this.x + '</b><br/>' +
                  this.series.name + ': ' + this.y + '<br/>' +
                  'Total: ' + this.point.stackTotal;
               },
			   style: {
                     fontWeight: 'bold',					 
                    // color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					color: 'black',
					fontSize: '18px'
                  }
			   
            };
            var plotOptions = {
               column: {
				  maxPointWidth: 75,
                  stacking: 'percent',
                  dataLabels: {
                     enabled: true,
					 
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor)
                        || 'white', 
					//color:'black',
                     style: {
						fontSize: '18px',
                        textShadow: '0 0 3px black'
                     }
                  }
               }
            };
			var credits = {
               enabled: false
            };
			//tar:target,achiev:achievement
			var series=[
			{name:'Reamaining',data:mydata.rest},{name:'Completed',data:mydata.completed},
			];
			
		/*	var series = [
               {
                  name: 'John',
                  data: [5, 3, 4, 7, 2]
               }, 
               {
                  name: 'Jane',
                  data: [2, 2, 3, 2, 1]
               }, 
               {
                  name: 'Joe',
                  data: [3, 4, 4, 2, 5]      
               }
            ];*/
			var json = {};   
            json.chart = chart; 
            json.title = title;   
            json.xAxis = xAxis;
            json.yAxis = yAxis;
            json.legend = legend;
            json.tooltip = tooltip;
            json.plotOptions = plotOptions;
            json.credits = credits;
			json.series = series; 	
		
$(myid).highcharts(json);			
}
var getChartData=function (myurl,myid,tbody)

{
	
	
	var mydata
             $.ajax({
      url: myurl,
      type: 'get',
      dataType: 'json',
	  
      beforeSend: function () {
		console.log("Sending ajax request.....")
      },
      success: function (data) {
        
		names=data['filteredItem']
		target=strtingToFloat(data['completed'])
		achiv=strtingToFloat(data['rest'])
        tablehtml=data['tabledata']	
		tbody.html(tablehtml)
		mydata={name:names,completed:target,rest:achiv};
		//$(myid).highcharts(json)
		console.log(mydata.name)
		//console.log( tablehtml)
		createChart(myid,mydata)
		//return mydata	
			
		}
		
      
    });	
	//console.log(mydata)
	//console.log(mydata)
	return mydata
	
}
var strtingToFloat= function (stringArray)
		{
			myNumber=[]
			for(var i=0;i<stringArray.length;i++)
			{
			 myNumber[i]=parseFloat(stringArray[i])	
				
			}
	      return myNumber
	
		}
var fillEmptyProgress=function (target,achiv)
		{
	     myNumber=[]
	     for(var i=0;i<target.length;i++)
			{
			 myNumber[i]=Math.floor(Math.random() *target[i])
				
			}
	      return myNumber
		}
var convertToPercent =function (target)
{
	 myNumber=[]
	     for(var i=0;i<target.length;i++)
			{
			 myNumber[i]=Math.floor(100*target[i])
				
			}
	      return myNumber
	
	
	
}


			/*/
			var series=[];
	   	
     
         */   
	objarray=[]
	var chart1
    $("#sidebar").mCustomScrollbar({
         theme: "minimal"
    });
	;

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
	 $(".graph-container").each(function (index,value) {
       
		 var chart
         myurl=$(this).attr("data-url")
		 myid2=$(this).attr("id")
		 head="#"
		 myid=head.concat(myid2)		 
		 console.log(myid)
		 table=$(this).parent().children(".card-table").children("tbody")
		 mydata=getChartData(myurl,myid,table)
		 
		 console.log(table.innerHTML)
		 //console.log(mydata)

  	
	
	//$(myid).highcharts(json);
	/*
	var mychart = $(myid).highcharts();
	n=mychart.series.length
	for(var i=0;i<n; i++){
	 if (mychart.series.length) {
        mychart.series[0].remove();
    }}
	 var xAxis2 = {
               categories: ['A', 'Oranges', 'Pears', 'Grapes', 'B']
            };
	mychart.xAxis=xAxis2
	*/
	//console.log("printing xaxis......")
	//console.log(json.xAxis.categories)
    });
/* Function to activate view Report Events  */
var viewReportEvent=function ()
{
myurl=$(this).attr("data-url");
//console.log("sucessfully trapped click event for view report");
//console.log("url:"+myurl);
$.ajax({
	url:myurl,
	type:'get',
	dataType:'json',
	beforeSend: function () {
		console.log("Sending Ajax request to get Event Table Data........")
		$(".event-table tbody").html("")
      },
	success: function (data)
	{
	console.log("sucessfully returned from Ajax request....")
	$(".event-table tbody").html(data. html_list)	
		
		
	}
	
	
	
})
	
}

var editReportEvent=function ()
{

console.log("sucessfully traped editReport event.... ")	
console.log("url:"+myurl)
myurl=$(this).attr("data-url")	
}
var deleteReportEvent=function ()
{
myurl=$(this).attr("data-url")
console.log("sucessfully traped editReport event.... ")	
console.log("url:"+myurl)	
}



/*Binding Events*/
$(".js-create-report-event").click(loadForm);	
$("#reprotmodal").on("submit",".js-report-event-create-form",saveForm);
$(".js-view-report-event").click(viewReportEvent)
$(".myclass").click(function ()
{
console.log("sucessfully traped editReport event.... ")		
	
})




});