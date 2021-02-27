$(function () {

  /* Functions */

  var loadForm = function () {
	console.log("success fully initiated contract create/update/delete....")
    var btn = $(this);
	var contract = $("#contract-select").children(":selected").attr('value');
	var ivt_id=btn.attr("ivt-id");
	console.log("current contract="+contract);
	
    $.ajax({
      url: btn.attr("data-url"),
      type: 'get',
      dataType: 'json',
	  data:{'table':'contract','contract':contract,'ivt-id':ivt_id},
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
var loadForm2 = function () {
	console.log("success fully initiated Contract_intervention_update....")
    var btn = $(this);
	var contract = $("#contract-select").children(":selected").attr('value');
	var ivt_id=btn.attr("ivt-id");
	console.log("current contract="+contract);
	
    $.ajax({
      url: btn.attr("data-url"),
      type: 'get',
      dataType: 'json',
	  data:{'table':'contract','contract':contract,'ivt-id':ivt_id},
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
	  
    var form = $(this);
    $.ajax({
      url: form.attr("action"),
      data:form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("Intervention data is Valid")
          $("#contract-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
		  console.log("Sending Ajax request to update Intervention Table")
		  value=$("#haor-sort").children(":selected").attr("data-url");
		  updateTable(value)
		  
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
	  data:{'table':'contract'},
	  beforeSend: function () {
		console.log("Sending Ajax request for updating table");
		$("#ivt-table tbody").html("");
        
      },
      success: function (data) {
         
		  console.log("Intervention data is Valid")
          $("#ivt-table tbody").html(data.html_ivt_list);
         // $("#modal-ivt").modal("hide");
        
        
      }
    })
	
	
	
	
	
	
};
function updateContractTable(target_url)
{
	
	  $.ajax({
      url: target_url,      
      type: 'get',
      dataType: 'json',
	  data:{'table':'contract'},
	  beforeSend: function () {
		console.log("Sending Ajax request for updating table");
		$("#contract-table tbody").html("");
        
      },
      success: function (data) {         
		  console.log("Intervention data is Valid")
          $("#contract-table tbody").html(data.html_ivt_list);
         // $("#modal-ivt").modal("hide");
        
        
      }
    })
	
	
	
	
	
	
};
  /* Binding */

  // Create Contract Intervention
 // $(".js-create-contract").click(loadForm);
  $("#ivt-table").on("click",".js-create-contract",loadForm)
  $("#modal-ivt").on("submit", ".js-contract-create-form", saveForm);

  //Contract Intervention update
  $("#contract-table").on("click", ".js-update-contract-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-contract-update-form", saveForm);

 //Contract Intervention delete
  $("#contract-table").on("click", ".js-delete-contract-ivt", loadForm);
  $("#modal-ivt").on("submit", ".js-contract-delete-form", saveForm);
  /*Ajax Table Sorting function*/
var sort_by_haor=function (){
console.log("Successfully triggered select change event in Haor Select box");
/*var btn=$(this);
console.log(btn.options);*/
var selector =$(this);

var value = $(this).children(":selected").attr("data-url");
var contractUrl=$("#contract-select").children(":selected").attr("data-url")   
updateTable(value);
console.log("Sending Ajax request to update Contract Intervention table")
updateContractTable(contractUrl);
};
var sort_by_contract=function (){
console.log("Successfully triggered select by contract");
/*var btn=$(this);
console.log(btn.options);*/
var selector =$(this);

var value = $(this).children(":selected").attr("data-url");
console.log("url for this sort:"+value)    
updateContractTable(value);
};
/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
$(".js-haor-sort").change(sort_by_haor)
//$(".js-contract-select").change(sort_by_contract)
$("#contract-select").change(sort_by_contract)
});