$(function () {

  var uploadInvoiceForm = function () {
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
  var saveInvoiceDocument=function ()
  {
	console.log("Sucessfully Triggered Save Invoice...")  
	 $("#modal-ivt").modal("hide");  
  }
  /* Binding invoice creation form */
  $(".js-create-invoice").click(uploadInvoiceForm);
  $("#modal-ivt").on("submit", ".js-invoice-create-form",saveInvoiceDocument);
  
  

});