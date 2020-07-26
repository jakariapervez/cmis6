$(document).ready(function () {
	/*Variables for Progress */
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
var chart = {
               type: 'column'
            };
            var title = {
               text: 'Component Progress'   
            }; 
			var xAxis			
         /*   var xAxis = {
               categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
            }; */
			var xAxis={categories: []}
            var yAxis = {
               min: 0,
               title: {
                  text: 'Progress in Percent'
               },
               stackLabels: {
                  enabled: true,
                  style: {
                     fontWeight: 'bold',
                     color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                  }
               }
            };
            var legend = {
               align: 'right',
               x: -30,
               verticalAlign: 'top',
               y: 25,
               floating: true,
               
               backgroundColor: (
                  Highcharts.theme && Highcharts.theme.background2) || 'white',
               borderColor: '#CCC',
               borderWidth: 1,
               shadow: false
            };   
            var tooltip = {
               formatter: function () {
                  return '<b>' + this.x + '</b><br/>' +
                  this.series.name + ': ' + this.y + '<br/>' +
                  'Total: ' + this.point.stackTotal;
               }
            };
            var plotOptions = {
               column: {
                  stacking: 'normal',
                  dataLabels: {
                     enabled: true,
                     color: (Highcharts.theme && Highcharts.theme.dataLabelsColor)
                        || 'white',
                     style: {
                        textShadow: '0 0 3px black'
                     }
                  }
               }
            };
            var credits = {
               enabled: false
            };
			var series
        /*  var series = [
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
			var series=[];
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
             $.ajax({
      url: myurl,
      type: 'get',
      dataType: 'json',
	  
      beforeSend: function () {
		
      },
      success: function (data) {
        
		names=data['filteredItem']
		target=strtingToFloat(data['target'])
		achievement=strtingToFloat(data['achievement'])		
		if(achievement.length)
			
		{
		target=convertToPercent(target)
		achievement=fillEmptyProgress(target,achievement)
		//achievement=convertToPercent(achievement)
		console.log(names)
		console.log(target)
		console.log(achievement)
		xAxis = {categories:names };
		series=[{name:'Target',data:target},{name:'achievement',data:achievement}]
		for (var i;i<names.length;i++)
		{
		json.xAxis.categories.push(names[i])
			
		}
			
			
		}
		else {
			achievement=fillEmptyProgress(target,achievement)
			names =  ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
			series = [{name: 'John',data: [5, 3, 4, 7, 2]},{name: 'Jane',data: [2, 2, 3, 2, 1]}, 
               {name: 'Joe',data: [3, 4, 4, 2, 5]}]
	    for( var i;i<names.length;i++)
		{
		json.xAxis.categories.push(names[i])	
			
		}	
		}
		
		
      }//ajax success closing
    });
  	
	
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
	console.log("printing xaxis......")
	console.log(json.xAxis.categories)
    });

});