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
    
updateTable(value);
};
/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
$(".js-haor-sort").change(sort_by_haor)
});