$(function () {

  /* Functions */

  var loadForm = function () {
    var btn = $(this);
	
    $.ajax({
      url: btn.attr("data-url"),
      type: 'get',
      dataType: 'json',
      beforeSend: function () {
		console.log(btn.attr("data-url"));
        $("#modal-ivt .modal-content").html("");
        $("#modal-ivt").modal("show");
      },
      success: function (data) {
        $("#modal-ivt .modal-content").html(data.html_form);
      }
    });
  };

  var saveForm = function () {
	  var value=$("#haor-sort").children(":selected").attr("data-url");
    var form = $(this);
    $.ajax({
      url: form.attr("action"),
      data: form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
		  console.log("restoring previous view of the table")
		  /*updateTable(value);*/
		  
        }
        else {
		  console.log("Intervention data is InValid")
          $("#modal-ivt .modal-content").html(data.html_form);
        }
      }
    });
    return false;
  };
  
  
  
  /*Ajax for sorting table by Haor*/
function updateTable(target_url)
{
	
	  $.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'table':'dpp'},
	  beforeSend: function () {
		console.log("Sending Ajax request for updating table");
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
         
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
        
        
      }
    })
	
	
	
	
	
	
};

function updateTable2(target_url)
{
	
	  $.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  /*data:{'table':'dpp'},*/
	  beforeSend: function () {
		console.log("Sending Ajax request for updating table");
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
         
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
        
        
      }
    })
	
	
	
	
	
	
};

function updateTable3(target_url,fy,month,ecode)
{
	  //fy=$(".js-fy-select").children(":selected").value;
	  //console.log("FY="+fy);
	  $.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'fy':fy,'month':month,'ecode':ecode},
	  beforeSend: function () {
		console.log("Sending Ajax request for updating table");
		//console.log(data)
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
         
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
        
        
      }
    })
	
	
	
	
	
	
};


  /* Binding */

  // Create book
  $(".js-create-ivt").click(loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-create-form", saveForm);

  // Update book
  $("#ivt-table").on("click", ".js-update-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-update-form", saveForm);

  // Delete book
  $("#ivt-table").on("click", ".js-delete-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-delete-form", saveForm);
  
  /*Ajax Table Sorting function*/
var sort_by_haor=function (){
console.log("Successfully triggered select change event");
/*var btn=$(this);
console.log(btn.options);*/
var selector =$(this);

var value = $(this).children(":selected").attr("data-url");
console.log(value)  
updateTable2(value);
};
var sort_by_all =function (){
	console.log("Successfully triggered select by all event");
	var selector =$(this);
	var selector =$(this);
	var value = $(this).attr("data-url");
	console.log(value);
	fy=$(".js-fy-select").children(":selected").attr("value");
	month=$(".js-month-select").children(":selected").attr("value")
	ecode=$(".js-code-select").children(":selected").attr("value")
	
	console.log("FY="+fy+"month="+month+"code="+ecode)
    updateTable3(value,fy,month,ecode);	
	
};
var generteReport =function()
{     
	  console.log("Sucessfully Triggered Report Generation");
	  var selector =$(this);
	  var selector =$(this);
	  var value = $(this).attr("data-url");
	  console.log(value);
	  fy=$(".js-fy-select").children(":selected").attr("value");
	  month=$(".js-month-select").children(":selected").attr("value")
	  ecode=$(".js-code-select").children(":selected").attr("value")
	
	console.log("FY="+fy+"month="+month+"code="+ecode)

	  $.ajax({
      url: value,      
      type: 'get',
      dataType: 'json',
	  data:{'fy':fy,'month':month,'ecode':ecode},
	  beforeSend: function () {
		console.log("Sending Ajax request for updating table");
		//console.log(data)
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
         
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
        
        
      },
	  error:function(error)
	  {
		console.log('Error ${error}')  
		  
	  }
	  
    })	
	
	
};
var generateReport2 =function ()
{
console.log("Sucessfully Trigggered PDF Report Generation")
var table=$("#ivt-table");
var rows=$("#ivt-table tr");
//console.log(table);
//console.log(rows);	
/*Data Pulling from Table
*****************************************************************
*******************************************************************
******************************************************************
*/

rowCount=rows.length
var total_gob=0;
var total_rpa=0;
var total_dpa=0;
var grand_total=0;	
var r,c;
var myDataArr=[[]];
for(r=1;r<rowCount;r++)
{
cells=rows[r].cells
var cols=[]
cellCount=cells.length
//console.log(rows[r].cells[2]);	
//console.log(rows[r].cells[2].firstChild.nodeValue);	
//console.log("Total cells="+cellCount);
for(c=0;c<cellCount-3;c++)
{
	 
	//console.log(rows[r].cells[c].firstChild.nodeValue);
	cols.push(rows[r].cells[c].firstChild.nodeValue);
}
total_gob=total_gob+parseFloat(rows[r].cells[4].firstChild.nodeValue);
total_rpa=total_rpa+parseFloat(rows[r].cells[5].firstChild.nodeValue);
total_dpa=total_dpa+parseFloat(rows[r].cells[6].firstChild.nodeValue);
grand_total=grand_total+parseFloat(rows[r].cells[7].firstChild.nodeValue);

//console.log(cols)
myDataArr.push(cols);

	
}
total_gob=total_gob.toFixed(2);
total_rpa=total_rpa.toFixed(2);
total_dpa=total_dpa.toFixed(2);
grand_total=grand_total.toFixed(2);
console.log("GoB Total="+total_gob );
console.log("RPA Total="+total_rpa );
console.log("DPA Total="+total_dpa);
console.log("Grand Tota="+grand_total)
totalcols=["","","","TOTAL",total_gob,total_rpa,total_dpa,grand_total]
myDataArr.push(totalcols);
//console.log(myDataArr);
//console.log("total rows="+rowCount)	
/*PDF CREATION
*****************************************************************
*******************************************************************
******************************************************************
PDF CREATION */
var heading=[['Code','Description','B_GoB','B_RPA','B_DPA','B_Total','DPA','TOTAL']]
var doc= new jsPDF('p','pt','a4');	
doc.autoTable({
	head:heading,
	body:myDataArr,
	tableLineColor: [189, 195, 199],
	tableLineWidth: 0.15,
	styles: {
            font: 'Meta',
            lineColor: [44, 62, 80],
            lineWidth: 0.15
        },
	headerStyles:{
		fillColor:[0,0,0],
		fontSize:8
		
	},
	bodyStyles:{
		fillColor: [216, 216, 216],
        textColor: 50,
		fontSize:8
		
		
	},
	 alternateRowStyles: {
            fillColor: [250, 250, 250]
        },
	columnStyles: {0: {columnWidth:55},1: {columnWidth:65},2: {columnWidth:40},  4: {columnWidth:50},5: {columnWidth:50},6: {columnWidth:50},7: {columnWidth:50}      }
	
});

doc.save("Report.pdf");

}
//builiding report name from data
var buildName =function (fy,month)
{
years=["2014_15","2015_16","2016_17","2017_18","2018_19","2019_20","2020_21","2021_22"]
months=["January","February","March","April","May","June","July","Agust","September","October","November","December"]
mydate=new Date()
ts=mydate.valueOf()
fy1=years[fy-1]
month1=months[month-1]
filename="Report_"+fy1+"_"+month1+ "_"+ts+".pdf"


return filename	
}

var generateReport3= function () 
{
console.log("sucessfully intitiated reporot generations....")
fy=$("#fy-select").children("option:selected").val();
month=$("#month-select").children("option:selected").val();
//filename2="Report_"+fy+"_"+month+".pdf"
//alert(filename2)
//filename="Report_2019_20_7.pdf"
var doc = new jsPDF('l','in',[16.5,11.7]);

//var heading=[["Gauge","Time","WL","EDIT","DELETE" ]]
var heading=[["GAUGE", 	"RIVER NAME" ,	"LOCATION","WL"]]
//var heading=[["Gauge","Time","WL" ]]
/*creating rows and column of Pdf */
var rows=$("#ivt-table tr");
var myDataArr=[[]];
rowCount=rows.length

var r,c;
var myDataArr=[[]];
for(r=1;r<rowCount;r++)
{
cells=rows[r].cells	
myDataArr.push(cells);	
}

doc.autoTable({head:heading,
body:myDataArr,
tableLineColor: [189, 195, 199],
tableLineWidth: 0.03,
headerStyles:{fillColor:[0,0,0],fontSize:12	},
bodyStyles:{fillColor: [216, 216, 216],textColor:50,fontSize:9,},
columnStyles: {0: {columnWidth:1},1: {columnWidth:2},}
})

//filename="report_"+fy+"_"+month+".pdf"
//alert(filename)
//doc.autoTable({html:"#ivt-table",theme: 'grid',});
filename= buildName(fy,month)
doc.save(filename);
	
}
/*Functions for sorting monthly data table*/
var monthlyExpenditureSelect=function ()
{
console.log("Sucessfully Triggered Monthly Expenditure Creation......")	
fy=$("#fy-select").children(":selected").attr("value")
month=$("#month-select").children(":selected").attr("value")
console.log("fy="+fy+"month="+month)
target_url=$(this).attr("data-url")
console.log("url="+target_url)
$.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'fy':fy,'month':month},
	  beforeSend: function () {
		console.log("Sending Ajax request calculating monthly report...");
		//console.log(data)
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
         
		  console.log("Expenditure list is valid......")
		  fy=data.fy
		  month=data.month
          $("#ivt-table tbody").html(data.expenditure_list);
		  //$("#fy-select").val(fy)
		  //$("#month-select").val(month)
          //$("#modal-ivt").modal("hide");
        
        
      }
    })

} 
/* #########################################################################################################*/
/*Functions for sorting WL data*/
var gaugeDataSelect=function ()
{
console.log("Sucessfully Triggered Gauge Data Selection......")	
//fy=$("#fy-select").children(":selected").attr("value")
//month=$("#month-select").children(":selected").attr("value")
//console.log("fy="+fy+"month="+month)
target_url=$(this).attr("data-url")
//var target_url=$("#gauge-select").find('option:selected').val()
gid=$("#gauge-select").find('option:selected').val()
console.log("gauge_id="+gid)
console.log("url="+target_url)
$.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'gid':gid},
	  beforeSend: function () {
		console.log("Sending Ajax request for retrieving gauge data...");
		//console.log(data)
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
		  console.log("sucessfully returned from ajax request for gauge data........");
		  wl=data.wl;
		  years=data.years;
		  months=data.months;
		  days=data.days;
		  hours=data.hours;
		  var times=[];
		  var mydata=[];
		  
		  for (var i=0;i<=wl.length;i++)
		  {
			var d = new Date(years[i], months[i]-1, days[i], hours[i]);
			
			console.log(d.toString());
			times.push(d);
			var dataItem=[d.toString(),wl[i]];
			mydata.push(dataItem);
			  
		  }
		  console.log(wl);
		  drawFiveDayWL(mydata);
          //console.log(data.gauge_readings)
		  //console.log("Expenditure list is valid......")
		  //fy=data.fy
		  //month=data.month
         $("#ivt-table tbody").html(data.gauge_readings);
		  //$("#fy-select").val(fy)
		  //$("#month-select").val(month)
          //$("#modal-ivt").modal("hide");
		  
        return false;
        
      }
    })

}
var drawFiveDayWL= function (wl_data){
	console.log("Sucessfully initiated drawing wl")
	
	var title = {text: "5 Day's WL"	};
	xAxis= {type: 'datetime',
		dateTimeLabelFormats:{month: '%e. %b', year: '%b'}

	};
	var yAxis = {
               title: {
                  text: 'WL in m-PWD'
               },
               
            };
	plotOptions= { area: {
                    fillColor: {linearGradient: {x1: 0,y1: 0,x2: 0, y2: 1},stops: [ [0, Highcharts.getOptions().colors[0]], [1, Highcharts.color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')] ]
                    },
                    marker: { radius: 2  },
                    lineWidth: 1,
                    states: {  hover: { lineWidth: 1 }},
                    threshold: null
                }
            };
	series= [{name:"WL",data:wl_data}];
	mychart={};
	mychart.title=title;
	mychart.yAxis=yAxis;
	mychart.xAxis=xAxis;
	mychart.plotOptions=plotOptions;
	mychart.series=series;
	$('#container').highcharts(mychart);
	
}

/* #########################################################################################################*/
  var saveWLEditForm = function () {
	
    var form = $(this);
    $.ajax({
      url: form.attr("action"),
      data: form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("WL is Valid")          
          $("#modal-ivt").modal("hide");
		  wl=data.wl;
		  years=data.years;
		  months=data.months;
		  days=data.days;
		  hours=data.hours;
		  var times=[];
		  var mydata=[];
		for (var i=0;i<=wl.length;i++)
		  {
			var d = new Date(years[i], months[i]-1, days[i], hours[i]);
			
			console.log(d.toString());
			times.push(d);
			var dataItem=[d.toString(),wl[i]];
			mydata.push(dataItem);
			  
		  }
		  
		 console.log(wl);
		 drawFiveDayWL(mydata);
		 $("#ivt-table tbody").html(data.gauge_readings);
		  
		  
		  
		  
		  
        }
        else {
		  console.log("Intervention data is InValid")
          $("#modal-ivt .modal-content").html(data.html_form);
        }
      }
    });
    return false;
  };
var gaugeTimeSelect =function ()
{
console.log("sucessfully triggered time select event.......");	
var target_url=$(this).attr("data-url")	;
console.log(target_url)
var hour=$("#time-select").find('option:selected').val()
console.log(hour)
$.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'hour':hour},
	  beforeSend: function () {
		console.log("Sending Ajax request for retrieving gauge data at..."+hour);
		//console.log(data)
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
		  console.log("sucessfully returned from ajax request for gauge data........");
		  
         $("#ivt-table tbody").html(data.gauge_readings);

		  
        return false;
        
      }
    })

}



/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
//$(".js-fy-select").change(sort_by_haor)
//$(".js-month-select").change(sort_by_haor)
//$(".js-code-select").change(sort_by_haor)
//$(".js-sort-all").click(sort_by_all);
//$(".js-report").click(generteReport)
/****WL EDIT ***/
$("#ivt-table").on("click", ".js-wl-edit-button", loadForm);
$("#modal-ivt").on("submit",".js-wl-update-form",saveWLEditForm)
/****WL DELETE ***/
$("#ivt-table").on("click", ".js-wl-delete-button", loadForm);
$("#modal-ivt").on("submit",".js-wl-delete-form",saveWLEditForm)
$(".js-wl-edit").on("click",loadForm)

$(".js-sort-all").click(gaugeTimeSelect)
$(".js-report").click(generateReport3)
});