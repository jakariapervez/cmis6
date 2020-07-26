$(document).ready(function () {
	/*Variables for Progress */
/*	var chart = {
               type: 'column'
            };
            var title = {
               text: 'Component Progress'   
            };    
            var xAxis = {
               categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
            };
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
            var series = [
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
            ];     
      var json = {};   
            json.chart = chart; 
            json.title = title;   
            json.xAxis = xAxis;
            json.yAxis = yAxis;
            json.legend = legend;
            json.tooltip = tooltip;
            json.plotOptions = plotOptions;
            json.credits = credits;
            json.series = series; */
	objarray=[]
	//var chart= new Highcharts




	
	
	
	
	
    $("#sidebar").mCustomScrollbar({
         theme: "minimal"
    });
	;

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
	 $(".graph-container").each(function (index,value) {
         //console.log($(this).attr("data-url"))
		 //console.log($(this).attr("id"))
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
		//console.log("Sending Ajax Request to Get Progress Data");
      },
      success: function (data) {
        //console.log("Successfully Retrieved Ajax Request");
		//console.log("printing data..........")
		//console.log(data)
		names=data['filteredItem']
		target=data['target']
		achievement=data['achievement']
		
		for(i=0;i<names.length;i++){
			console.log(names[i]);
			weight={};
			progress={};
			//sdata=[]
			//sdata.push(target[i]*100);
			//sdata.push(achievement[i]*100);
			
			//obj={name:names[i],data:[target[i],achievement[i]]}
			weight.name='Target'
			weight.data=target
			progress.name='Achievment'
			progress.data=achievement
			series=[weight,progress]
			//console.log(obj)
			//objarray.push(obj)
			
			//xAxis.categories=names;
			
		     console.log(series)
			 chart =new Highcharts.chart( {
       title: {
              text: 'My chart'
       },
       series: [{
           data: [1, 3, 2, 4]
       }]
})
			//json.series = series;
			//json.xAxis = xAxis;
			//highchart graph
			/*
			var myChart = Highcharts.chart($(this),{
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Fruit Consumption'
        },
        xAxis: {
            categories: ['Apples', 'Bananas', 'Oranges']
        },
        yAxis: {
            title: {
                text: 'Fruit eaten'
            }
        },
        series: [{
            name: 'Jane',
            data: [1, 0, 4]
        }, {
            name: 'John',
            data: [5, 7, 3]
        }]
    });
			
		*/	
			
			
			//

			
			}
		
		//console.log(json)
		
		//$(this).highcharts(json);
      }
    });
	//console.log(objarray)
	//json.series=objarray
	console.log(chart)
	//chart.renderTo=$(myid);
	//$(myid).highcharts(chart);
    });

});