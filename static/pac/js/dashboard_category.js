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
var heading=[['Date','Batch No','Code','Description','GoB','RPA','DPA','TOTAL']]
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
/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
$(".js-fy-select").change(sort_by_haor)
$(".js-sort-all").click(sort_by_all);
//$(".js-report").click(generteReport)
$(".js-report").click(generateReport2)
/*****************************************************************************************
Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard,Highcharts Dashboard
**********************************************************************************************/

var title={text:'Monthly Average Temperature'};
<<<<<<< HEAD
<<<<<<< HEAD
var subtitle={text:'ARC-LT(JV)'};
var xAxis={categories:['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec']};
var yAxis={title:{text:'Progres in %'},plotLines:[{value:0,width:1,color:'#808080'}]};
=======
var subtitle={text:'Source:world.climate.com'};
var xAxis={categories:['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec']};
var yAxis={title:{text:'Temperature (\xB0C)'},plotLines:[{value:0,width:1,color:'#808080'}]};
>>>>>>> 65827334fbf1f5d1e3508b179fb305e81b4c3651
=======
var subtitle={text:'Source:world.climate.com'};
var xAxis={categories:['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec']};
var yAxis={title:{text:'Temperature (\xB0C)'},plotLines:[{value:0,width:1,color:'#808080'}]};
>>>>>>> 65827334fbf1f5d1e3508b179fb305e81b4c3651
var tooltip={valueSuffix:'\xB0C'};
var legend={layout:'vertical',align:'right',verticalAlign:'middle',boderWidth:0};
var series=[{
	name:'Tokyo',
	data:[7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
            },
          {name:'New York',
		   data:[-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
	
          },
		  {name:'Berlin',
		  data:[-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
			  
		  },
		  
		  {name:'London',
		  data:[3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
			  
			  
		  }
		  
		  ]
var json={};
//building Json data
json.title=title;
json.subtitle=subtitle;
json.xAxis=xAxis;
json.yAxis=yAxis;
json.tooltip=tooltip;
json.legend=legend;
json.series=series;

<<<<<<< HEAD
<<<<<<< HEAD
//$("#container1").highcharts(json);
/*Drawing Second Chart*/
var chart = {type:'column'}
var plotOptions={
	pointPadding:0.4,
	boderWidth:0
     }
json.chart=chart;
$("#container2").highcharts(json);
var plotOptions=
{
column:{stacking:'percent'}	
};
var series=[{name:'Not Start',data:[0,0,2.790,0,0,0]},{name:'Ongoing',data:[0,0.315,0,1,0]},{name:'complete',data:[0,0,10.48,0,0,0]}]
var xAxis={categories:['Embankment','Sub Emb','Khal','River','Reg/Casw/Box Cul','Inlet']};
var tooltip={pointFormat:'<span style="color:{series:color}">{series.name}</span>:<b>{point.y}</b>({point.percentage:.0f}%)</br>',
shared:true
};
var title={text:'Physical Progress W-02/Chandpur Haor'};
var subtitle="FC-MGS JV"
json.title=title
json.plotOptions=plotOptions
json.series=series
json.xAxis=xAxis
json.tooltip=tooltip
json.subtitle=subtitle
$('#container2').highcharts(json)
//package W-03
var title={text:'Physical Progress W-03/Nunnir Haor'};
var subtitle="ARC-LT(JV)"
var series=[{name:'Not Start',data:[0,0.5,0,0,0,0]},{name:'Ongoing',data:[0,0,0,0,1,0]},{name:'complete',data:[0,17.249,0,0,0,0]}]
json.title=title
json.series=series
json.subtitle=subtitle
$('#container3').highcharts(json)
//package W-04
var title={text:'Physical Progress W-04/Nunnir Haor(Part B+C)'};
var subtitle="M/S Amin & Co"
var series=[{name:'Not Start',data:[0,0,0,0,0,0]},{name:'Ongoing',data:[0,6.471,0,0,3,0]},{name:'complete',data:[0,0,0,0,0,0]}]
json.title=title
json.series=series
json.subtitle=subtitle
$('#container5').highcharts(json)
//W-05
var title={text:'Physical Progress W-05/Nunnir Haor(Part A&C)'};
var subtitle="SA-SI & Israt Enterprise"
var series=[{name:'Not Start',data:[0,8,0,0,0,0]},{name:'Ongoing',data:[0,0,0,0,1,0]},{name:'complete',data:[0,4.214,0,0,0,0]}]
json.title=title
json.series=series
json.subtitle=subtitle
$('#container6').highcharts(json)
//W-06
var title={text:'Physical Progress W-06/Nunnir Haor(Part A)'};
var subtitle="Binimoy Construction (JV)"
var series=[{name:'Not Start',data:[0,0,0,0,0,0]},{name:'Ongoing',data:[0,0,0,0,0,0]},{name:'complete',data:[0,0,20,0,1,0]}]
json.title=title
json.series=series
json.subtitle=subtitle
$('#container7').highcharts(json)
//W-07
var title={text:'Physical Progress W-07/Bor Haor'};
var subtitle="M/S Liton Traders"
var series=[{name:'Not Start',data:[0,3.879,32.406,0,0,0]},{name:'Ongoing',data:[0,0,0,0,0,0]},{name:'complete',data:[0,0,0,0,0,0]}]
json.title=title
json.series=series
json.subtitle=subtitle
$('#container8').highcharts(json)
=======
$("#container").highcharts(json);

>>>>>>> 65827334fbf1f5d1e3508b179fb305e81b4c3651
=======
$("#container").highcharts(json);

>>>>>>> 65827334fbf1f5d1e3508b179fb305e81b4c3651
}
);