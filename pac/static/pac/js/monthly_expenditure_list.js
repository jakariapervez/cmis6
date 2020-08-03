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
var generateReport3= function () 
{
console.log("sucessfully intitiated reporot generations....")
var doc = new jsPDF('l','in',[16.5,11.7]);

var heading=[['Code','Description','B_GoB','B_RPA','B_DPA','B_Total',
'PM_GoB','PM_RPA','PM_DPA','PM_Total','CM_GoB','CM_RPA','CM_DPA','CM_Total',
'TEC_GoB','TEC_RPA','TEC_DPA','TEC_Total',
'RM_GoB','RM_RPA','RM_DPA','RM_Total']]
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


//doc.autoTable({html:"#ivt-table",theme: 'grid',});
doc.save("Report.pdf");
	
}
/*Functions for sorting monthly data table*/
var monthlyExpenditureSelect=function ()
{
console.log("Sucessfully Triggered Monthly Expenditure Creation......")	
fy=$("#fy-select").children(":selected").attr("value")
month=$("#fy-select").children(":selected").attr("value")
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
         
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
        
        
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
$(".js-sort-all").click(monthlyExpenditureSelect)
$(".js-report").click(generateReport3)
});