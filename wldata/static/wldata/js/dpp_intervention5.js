$(function () {

  /* Functions */
select_by_structure2();
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
console.log("successfully initiated sort_by_structure for select2");
var target_url=$(this).children(":selected").attr("data-url");
console.log("ajax request url="+	target_url);
$.ajax({
	url:target_url,
	type:'get',
	dataType:'json',
	beforeSend:function (){
		console.log("sending ajax request to get Progress Item under this structure");
		$("#progress_quantity_table tbody").html("");
		$("#strucTureCarousel").html("");
		
	},
	success:function(data) {
		console.log("Successfully returned ajax request");
		//console.log(data);
	$("#progress_quantity_table tbody").html(data.pitems);
	$("#strucTureCarousel").html(data.image_carousel);
	
		
	}
	
	
	
});	
}
function select_by_structure2()
{
console.log("successfully initiated sort_by_structure for select2");
var target_url=$("#contract-intervention-select2").children(":selected").attr("data-url");
console.log("ajax request url="+	target_url);
$.ajax({
	url:target_url,
	type:'get',
	dataType:'json',
	beforeSend:function (){
		console.log("sending ajax request to get Progress Item under this structure");
		$("#progress_quantity_table tbody").html("");
		$("#strucTureCarousel").html("");
		
	},
	success:function(data) {
		console.log("Successfully returned ajax request");
		//console.log(data);
	$("#progress_quantity_table tbody").html(data.pitems);
	$("#strucTureCarousel").html(data.image_carousel);
	
		
	}
	
	
	
});	
}





var loadForm3=function ()
{
var btn=$(this);
var target_url=btn.attr("data-url");	
console.log(target_url);
 
cell=$(this).parent().parent()
row=$(this).parent().parent()
//console.log(row)
//data=row.find("td").eq(2).text()
console.log(row)
report=row.children(".current-reporting-period")
row.children(".current-reporting-period").html("<input type=\"number\" class =\"myvalue\" value=\"0\" autofocus>")
console.log(report)
};
var calculateItemisedProgress=function()
{
//console.log("successfully initiated itemised progress calculation....")
row=$(this).parent().parent()
val2=row.children(".current-reporting-period").children(".myvalue")
val3=val2.val()
//console.log("value inputed="+val3)
val1=row.children(".cumulative-upto-previous-period").text()
//console.log(val2)
//console.log("previous value="+val1 +" current value="+val3)		
cumulative_value=Number(val1)+Number(val3)	
//console.log("cumulative progress="+cumulative_value)
row.children(".cumulative-upto-current-period").text(cumulative_value)
var tq=row.children(".total-quantity").text()
var  weight=row.children(".weight").text()
var item_progress=(cumulative_value/tq)*weight
item_progress=parseFloat(item_progress).toFixed(4)
row.children(".itemised-progress").text(item_progress)

//console.log(val4)
//val4.text(" a new text")	
//val4.text(cumulative_value)
}
var calculateTotalProgress=function()
{
/*	
var table=$("#progress_quantity_table")
rows=table.rows	
rowCount=rows.length
alert(rowCount)	*/
	
var nrows=$("#progress_quantity_table tr").length
trows=$("#progress_quantity_table tr")
var sumTotal=0
for(i=1;i<nrows-1;i++)
{
	row=$(trows[i])
	cell=row.children(".itemised-progress")
	val=cell.text()
	sumTotal+=Number(val)
	
	
	
}
sumTotal=parseFloat(sumTotal).toFixed(4)
row=$(trows[nrows-1]).children(".total-progress").text(sumTotal)
$(trows[nrows-1]).children(".js-submit-row").children(".js-submit-progress").removeAttr('disabled');

	
}

var validateProgressData=function ()
{
	
row=$(this).parent().parent()
totalQuantity=Number(row.children(".total-quantity").text())
input_value=Number($(this).val())
if(input_value>totalQuantity)
{
alert("input quantity can't be greater than total quantity")
$(this).focus()	
}
}

var submitProgressQuantity=function (){

console.log("sucessfully initiated proress submition")
var target_url=$(this).attr("data-url")

var nrows=$("#progress_quantity_table tr").length
var trows=$("#progress_quantity_table tr")
var ids=[]
var quantity=[]
//console.log(array)
//array.unshift(1)
//console.log(array)
for(i=1;i<nrows-1;i++)
{
	row=$(trows[i])
	//cell=row.children(".itemised-progress")
	//console.log(row.children(".cumulative-upto-current-period").text())
	//q=row.children(".cumulative-upto-current-period").text()
	//console.log(q)
	//q=Number(q)
	id=Number(row.children(".Add").children(".js-input-progress-quantity").attr("civt-id"))
	console.log(id)
	q=Number(row.children(".cumulative-upto-current-period").text())
	console.log(q)
	//ids.push(id)
	ids[i]=id
	quantity[i]=q
	
	$.ajax(
{
url:target_url,
type:'get',
dataType:'json',
data:{'id':id,'quantity':q},	
success:function (data){ console.log("successfully send data to server")}	
//data:{'table':'contract','contract':contract,'civt_id':civt_id},	
	
	
});
	
	
	
	
	
	
	
}
/*ids.shift()
quantity.shift()
console.log(quantity)
console.log(ids)
console.log(target_url)
//iddata=$.serialize(ids)

//console.log(iddata)*/

$("#progress_quantity_table tbody").html("")
select_by_structure2();
}




/*binding*/
/*$(".js-haor-sort").change(function(){alert( $(this).find(":selected").val() );})*/
$(".js-haor-sort").change(sort_by_haor)
$(".js-contract-sort").change(sort_by_contract)
$(".js-contract-select").change(selectContract)
$(".js-contract-intervention-select2").change(select_by_structure)

// update progress Quantiy
  $("#progress_quantity_table").on("click",".js-input-progress-quantity",loadForm3)
  $("#progress_quantity_table").on("click",".js-calcualte-itemised-progress",calculateItemisedProgress)
  $("#progress_quantity_table").on("click",".js-calculate-total-progress",calculateTotalProgress)
  $("#progress_quantity_table").on("click",".js-submit-progress",submitProgressQuantity)
  $("#progress_quantity_table").on("blur",".myvalue",validateProgressData)
  //$("#modal-ivt").on("submit",".js-progress-item-update-form",saveForm)
});