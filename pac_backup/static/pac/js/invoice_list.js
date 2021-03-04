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
  /*A second Load Form for Expenditure Edit*/
   var loadForm2 = function () {
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
	var fy
	var month
    $.ajax({
      url: form.attr("action"),
      data: form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("Intervention data is Valid")
		  $("#modal-ivt").modal("hide");

         // $("#ivt-table tbody").html(data.html_ivt_list);
          fy=data.fy
		  month=data.month		  
		  console.log("add expenditure was successful...")
		  $("#ivt-table tbody").html("");
		  $("#ivt-table tbody").html(data.html_ivt_list);
		  $("#fy-select").val(fy)
		  $("#month-select").val(month)

		  /*updateTable(value);*/
		  //console.log("initiating second ajax request for sorting table data");
		  
        }
        else {
		  console.log("Intervention data is InValid")
		 //$("#modal-ivt").modal("hide");
         $("#modal-ivt .modal-content").html(data.html_form);
        }
      }
    });
	//target_url=

	//sort_by_all()

	//sort_by_all()

	
	
    return false;
  };

  var saveInvoiceDocForm = function () {
	var value=$("#haor-sort").children(":selected").attr("data-url");
	
    var form = $(this);
	var form2=$("form").get(0)
	var fy
	var month
	fd=new FormData(form2)
	//file=$("#id_invoice_image")[0].files[0]
	//fd.append('file':file)
	
	//console.log(file)
	//console.log(form)
	//console.log($("#id_invoice_image")[0].files[0])
	//var fd=new FormData($("#invoice-doc-update").get[])
	//formData.append('file', $('input[type=file]')[0].files[0]);
	//$("#invoice-doc-update")
	//console.log(fd)
    $.ajax({
      url: form.attr("action"),
	  contentType: false,
		processData: false,
      //data: form.serialize(),
	  data:fd,
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("Intervention data is Valid")
		  $("#modal-ivt").modal("hide");

         // $("#ivt-table tbody").html(data.html_ivt_list);
          fy=data.fy
		  month=data.month		  
		  console.log("Updating Invoice doc was sucessful...")
		  $("#ivt-table tbody").html("");
		  $("#ivt-table tbody").html(data.html_ivt_list);
		  $("#fy-select").val(fy)
		  $("#month-select").val(month)

		  /*updateTable(value);*/
		  //console.log("initiating second ajax request for sorting table data");
		  
        }
        else {
		  console.log("Intervention data is InValid")
          $("#modal-ivt .modal-content").html(data.html_form);
        }
      }
    });
	//target_url=

	//sort_by_all()

	//sort_by_all()

	
	
    return false;
  };





    var saveForm2 = function () {
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
		  $("#ivt-table tbody").html("");
          //$("#ivt-table tbody").html(data.html_ivt_list);
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
	$("#ivt-table tbody").html("");
	var value = $(".js-sort-all").attr("data-url");
	//var fy=$(".js-fy-select").children(":selected").attr("value");
	//var month=$(".js-month-select").children(":selected").attr("value");
	console.log("initiating second ajax request for sorting table data");
	console.log("fy="+fy+"month="+month);
	updateTable3(value,fy,month);
    return false;
  };
  
 /*Save function for Upload Invoice */ 
 var saveForm3 = function () {
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
          //$("#ivt-table tbody").html(data.html_ivt_list);
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


  /* Binding */



  
  /*Ajax Table Sorting function*/
var sort_by_haor=function (){
console.log("Successfully triggered select change event");
/*var btn=$(this);
console.log(btn.options);*/
var selector =$(this);

var value = $(this).children(":selected").attr("data-url");
console.log(value)  
updateTable(value);
};

/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
//$(".js-fy-select").change(sort_by_haor)

/*
#################################################################
*/
function updateTable3(target_url,fy,month)
{
	  //fy=$(".js-fy-select").children(":selected").value;
	  //console.log("FY="+fy);
	  $.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'fy':fy,'month':month},
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




var sort_by_all =function (){
	console.log("Successfully triggered select by all event");
	var selector =$(this);
	var selector =$(this);
	var value = $(this).attr("data-url");
	console.log(value);
	fy=$(".js-fy-select").children(":selected").attr("value");
	month=$(".js-month-select").children(":selected").attr("value")
	//ecode=$(".js-code-select").children(":selected").attr("value")
	
	console.log("FY="+fy+"month="+month)
    updateTable3(value,fy,month);	
	
};
var saveDeleteForm4= function ()
{
	 var value=$("#haor-sort").children(":selected").attr("data-url");
    var form = $(this);
	var fy
	var month
	$.ajax({
	  url: form.attr("action"),
      data: form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
	  sucess:function (data) 
	  {
		 $("#modal-ivt").modal("hide"); 
		 $("#fy-select").val(fy);
		 $("#month-select").val(month)
		  
	  }
		
		
		
		
	})
	
};
var hideErrorForm= function () 
{
var form = $(this);	
 $("#modal-ivt").modal("hide"); 	
	
}
/*Function for saving PDF Report*/
var savePDFTable =function ()
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
var myDataArr=[[]];
total=0
for(r=1;r<rowCount;r++)
{
cells=rows[r].cells
var cols=[]
cellCount=cells.length
//console.log(rows[r].cells[2]);	
//console.log(rows[r].cells[2].firstChild.nodeValue);	
//console.log("Total cells="+cellCount);
for(c=0;c<4;c++)
{
	 
	//console.log(rows[r].cells[c].firstChild.nodeValue);
	cols.push(rows[r].cells[c].firstChild.nodeValue);
	if (c==3)
	{
	total=total+parseFloat(rows[r].cells[c].firstChild.nodeValue)
	}
}
	

//console.log(cols)
myDataArr.push(cols);

	
}
totalcols=["","","Total of The Month",total]
myDataArr.push(totalcols);

//console.log(myDataArr);
//console.log("total rows="+rowCount)	
/*PDF CREATION
*****************************************************************
*******************************************************************
******************************************************************
PDF CREATION */
			 	
var heading=[['Invoice No','Date','Description','Total']]
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
	columnStyles: {0: {columnWidth:70},2: {columnWidth:288},      }
	
});

doc.save("Invoice_details.pdf");

}

/* Binding */

  // Create Invoice
  $(".js-create-ivt").click(loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-create-form", saveForm);

  // Update Invoice
  $("#ivt-table").on("click", ".js-update-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-update-form", saveForm);

  // Delete Invoice
  $("#ivt-table").on("click", ".js-delete-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-delete-form", saveForm);

  // Update Invoice Doc  
    $("#ivt-table").on("click", ".js-update-ivt-doc", loadForm);
	$("#modal-ivt").on("submit",".js-ivt-update-doc-form",saveInvoiceDocForm )
	
  
  //Add Expenditure Function
  $("#ivt-table").on("click", ".js-create-Expenditure", loadForm);  
   $("#modal-ivt").on("submit", ".js-expenditure-create-form", saveForm);
   //Error in Expenditure
   $("#modal-ivt").on("submit",".js-ivt-error-form",hideErrorForm)



/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
//$(".js-fy-select").change(sort_by_haor);
$(".js-sort-all").click(sort_by_all);
$(".js-print-report").on("click",savePDFTable)
//js-print-report
});