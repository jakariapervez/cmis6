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
	var myurl=$(this).attr("data-url")
	var contract=$("#package-sort").val()
	console.log(myurl)
	
$.ajax({
		url:myurl,
		type:'get',
		dataType:'json',
		data:{'contract_id':contract},
		beforeSend:function ()
		{
		console.log("Sending Ajax request for updating table");
		//console.log(data)
		$("#ivt-table tbody").html("");
			
		},
		success:function (data)
		{
			
		console.log("sucessfully returned from ajax request......");
		$("#ivt-table tbody").html(data.html_ivt_list);
			
		}
		
		
		
		
	});
	
	
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
for(c=0;c<cellCount;c++)
{
	 
	//console.log(rows[r].cells[c].firstChild.nodeValue);
	cols.push(rows[r].cells[c].firstChild.nodeValue);
}


//console.log(cols)
myDataArr.push(cols);

	
}

//console.log(myDataArr);
//console.log("total rows="+rowCount)	
/*PDF CREATION
*****************************************************************
*******************************************************************
******************************************************************
PDF CREATION */
/*var heading=[['Packge','Name',Type','Start','Finish','Length','Vents','Current Status','Progress','VWD','Problem',]]*/
var heading=[['Packge','Name','Type','Start','Finish','Length','Vents','Current Status','Progress','VWD','Problem']]
var doc= new jsPDF('l','in',[16.5,11.7]);	
doc.autoTable({
	head:heading,
	body:myDataArr,
	tableLineColor: [189, 195, 199],
	tableLineWidth: 0.01,
	styles: {
            font: 'Meta',
            lineColor: [44, 62, 80],
            lineWidth: 0.01
        },
	headerStyles:{
		fillColor:[0,0,0],
		fontSize:12
		
	},
	bodyStyles:{
		fillColor: [216, 216, 216],
        textColor: 50,
		fontSize:10
		
		
	},
	 alternateRowStyles: {
            fillColor: [250, 250, 250]
        },
	
	columnStyles: {1: {columnWidth:3},9: {columnWidth:2},10: {columnWidth:2}}
	
});
mydate=new Date()
ts=mydate.valueOf()

filename="Status_"+ts+".pdf"
doc.save(filename);

}
var loadUpdateForm= function ()
{
console.log("sucessfully triggered progress update event.....")
var btn = $(this);	
myurl=$(this).attr("data-url");
console.log(myurl);
    $.ajax({
      url: myurl,
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
}
var saveUpdateForm=function ()
{
console.log("sucessfully Initiated form Submission Event.......")
 var form = $(this);
 myurl=form.attr("action")
 console.log(myurl)
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
	
	
	
}

var structureSelect=function () 
{
console.log("sucessfuly triggered structure select....")	
myurl=$(this).attr('data-url')
console.log(myurl)
package_name=$(this).val()
console.log(package_name)

$.ajax({
url: myurl,      
type: 'get',
dataType: 'json',
data:{'package_name':package_name},
success: function (data)
{
console.log("sucessfully returned from ajax request......")	
console.log(data.package_names)
console.log(data.ids)
$("#structure-sort ").empty()
for(i=0;i<data.ids.length;i++)
{
optionValue=data.ids[i]
optionText=data.package_names[i]
myhtml="<option value="+optionValue+">"+optionText+"</option>"
console.log(myhtml)
$("#structure-sort ").append(myhtml)
//$("#structure-sort ").append('<option value="${optionValue}">${optionText}</option>')	
	
	
}

	
} 	
	
	
});

	
}



/*binding*/
$("#package-sort").on("change",structureSelect);
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
//$(".js-fy-select").change(sort_by_haor)
//$(".js-month-select").change(sort_by_haor)
//$(".js-code-select").change(sort_by_haor)
//progres update
//$(".js-update-qualitative-progress").click(loadUpdateForm);
$("#ivt-table").on("click",".js-update-qualitative-progress",loadUpdateForm)
$("#modal-ivt").on("submit", ".js-progress-update-form", saveUpdateForm);
$(".js-sort-all").click(sort_by_all);
//$(".js-report").click(generteReport)
$(".js-report").click(generateReport2)


});