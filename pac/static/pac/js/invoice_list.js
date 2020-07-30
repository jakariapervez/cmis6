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
          $("#modal-ivt .modal-content").html(data.html_form);
        }
      }
    });
	//target_url=
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
$(".js-fy-select").change(sort_by_haor)

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
/* Binding */

  // Create Invoice
  $(".js-create-ivt").click(loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-create-form", saveForm3);

  // Update Invoice
  $("#ivt-table").on("click", ".js-update-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-update-form", saveForm2);

  // Delete Invoice
  $("#ivt-table").on("click", ".js-delete-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-ivt-delete-form", saveForm2);
  
  //Add Expenditure Function
  $("#ivt-table").on("click", ".js-create-Expenditure", loadForm);  
   $("#modal-ivt").on("submit", ".js-expenditure-create-form", saveForm);

/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
//$(".js-fy-select").change(sort_by_haor);
$(".js-sort-all").click(sort_by_all);

});