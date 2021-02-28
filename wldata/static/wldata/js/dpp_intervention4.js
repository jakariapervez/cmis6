$(function () {

  /* Functions */

  var loadForm = function () {
	console.log("success fully initiated contract create/update/delete....")
    var btn = $(this);
	var contract = $("#contract-select").children(":selected").attr('value');
	var civt_id=$("#contract-intervention-select").children(":selected").attr('value');
	console.log("current contract="+contract);
	console.log("selected contract_intervetion_id="+civt_id);
	
    $.ajax({
      url: btn.attr("data-url"),
      type: 'get',
      dataType: 'json',
	  data:{'table':'contract','contract':contract,'civt_id':civt_id},
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
	  console.log("sending post request")
    var form = $(this);
    $.ajax({
      url: form.attr("action"),
      data:form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log(" data is Valid");
		  console.log("sending post request");
          $("#progress_item_table tbody").html(data.html_ivt_list);
		  //$("#book-table tbody").html("");
		  //$("#book-table tbody").html(data.html_ivt_list);
          $("#modal-ivt").modal("hide");
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
function update(target_url)
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

  // Create Progress Item
  $(".js-create-progress-item").click(loadForm);
  $("#modal-ivt").on("submit",".js-progress-item-create-form",saveForm)
  // update progress Item
  $("#progress_item_table").on("click",".js-update-progress-item",loadForm)
  $("#modal-ivt").on("submit",".js-progress-item-update-form",saveForm)
  //Delete Progress Item
  $("#progress_item_table").on("click",".js-delete-progress-item",loadForm)
  $("#modal-ivt").on("submit",".js-progress-item-delete-form",saveForm)

var sort_by_haor=function (){
console.log("Successfully triggered select change event");
/*var btn=$(this);
console.log(btn.options);*/
var selector =$(this);

var value = $(this).children(":selected").attr("data-url");
    
updateTable(value);
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

var selectContract=function()
{
console.log("sucessfully traped change event");	
var target_url=$(this).children(":selected").attr("data-url");	
console.log("target_url:"+target_url);
$.ajax({
	url:target_url,
	type:'get',
	dataType:'json',
	beforeSend:function (){
		console.log("sending ajax request to get Strucres under this contract");
		$("#contract-intervention-select").html("");
		
	},
	success:function(data) {
		console.log("Successfully returned ajax request");
		console.log(data);
	$("#contract-intervention-select").html(data.html_civt_list);
	
		
	}
	
	
	
});	
}
var select_by_structure=function ()
{
console.log("successfully initiated sort_by_structure");
var target_url=$(this).children(":selected").attr("data-url");
console.log("ajax request ur="+	target_url);
$.ajax({
	url:target_url,
	type:'get',
	dataType:'json',
	beforeSend:function (){
		console.log("sending ajax request to get Progress Item under this structure");
		$("#progress_item_table tbody").html("");
		
	},
	success:function(data) {
		console.log("Successfully returned ajax request");
		console.log(data);
	$("#progress_item_table tbody").html(data.html_civt_list);
	
		
	}
	
	
	
});	
}

/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
$(".js-haor-sort").change(sort_by_haor)
$(".js-contract-sort").change(sort_by_contract)
$(".js-contract-select").change(selectContract)
$(".js-contract-intervention-select").change(select_by_structure)
});