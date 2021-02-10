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

});