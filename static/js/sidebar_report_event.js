$(document).ready(function () {
	/*Variables for Progress */
	
	
var loadForm=function ()

{
	var btn = $(this);
$.ajax({
	url:btn.attr("data-url"),
	type:'get',
	dataType:'json',
	beforeSend:function ()
	{
	//
	//$("#reprotmodal").modal("show");	
	},
	success:function (data)
	{
	console.log("Sucessfully returned from ajax form get rerquest");
	//console.log(data.html_form);
    //$("#reprotmodal")	
	$("#reportmodal .modal-content").html("");
	
	//$("#reportmodal .modal-content").html(data.html_form);
	modal=$("#reprotmodal .modal-content")
	//console.log(modal);
	//modal.html("<p> welcom </p>")
	modal.html(data.html_form)
	
    //$("#reprotmodal").modal("show");	
	$("#reprotmodal").modal("show");
	}
});	
	
}
	
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
		  console.log("Sucessfully saved report event")
		  console.log(" data is Valid");
		  //console.log("sending post request");
          //$("#progress_item_table tbody").html(data.html_ivt_list);
		  //$("#book-table tbody").html("");
		  //$("#book-table tbody").html(data.html_ivt_list);
          $("#reprotmodal").modal("hide");
        }
        else {
		  console.log("Intervention data is InValid")
		  modal=$("#reprotmodal .modal-content")
			console.log(modal);
			//modal.html("<p> welcom </p>")
			modal.html(data.html_form)
		  //$("#reprotmodal").modal("hide");
          //$("#reprotmodal .modal-content").html(data.html_form);
        }
      }
    });
    return false;
  };	
	
var saveEditForm = function () {
	  console.log("sending post request")
    var form = $(this);
    $.ajax({
      url: form.attr("action"),
      data:form.serialize(),
      type: form.attr("method"),
      dataType: 'json',
      success: function (data) {
        if (data.form_is_valid) {
		  console.log("Sucessfully saved report event")
		  console.log(" data is Valid");
		  //console.log("sending post request");
          //$("#progress_item_table tbody").html(data.html_ivt_list);
		  //$("#book-table tbody").html("");
		  //$("#book-table tbody").html(data.html_ivt_list);
          $("#reprotmodal").modal("hide");
        }
        else {
		  console.log("Intervention data is InValid")
		  modal=$("#reprotmodal .modal-content")
			console.log(modal);
			//modal.html("<p> welcom </p>")
			modal.html(data.html_form)
		  //$("#reprotmodal").modal("hide");
          //$("#reprotmodal .modal-content").html(data.html_form);
        }
      }
    });
    return false;
  };	



/*Binding Events*/
$(".js-create-report-event").click(loadForm);	
$("#reprotmodal").on("submit",".js-report-event-create-form",saveForm);
//$(".js-view-report-event").click(viewReportEvent);
$(".js-edit-report-event").click(loadForm);
$("#reportmodal").on("submit",".js-report-event-edit-form",saveEditForm);
//$(".js-report-event-edit-form").click(saveEditForm)
//$(".js-create-report-event").click(loadEditForm);




});