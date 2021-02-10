<?php 

    include('includes/database.php');

    $page_tittle = "Home";
    include('includes/header.php'); 

?>
<div class="content">
    <div class="container-fluid">
        

        <div class="card card-map">
			<div class="header">
                <h4 class="title">Station Location Map</h4>
                <!-- <p class="category">Unit: Meter</p> -->

            </div>
			<div class="content">
				<div id="leaflet_map"></div>
			</div>
		</div>
	</div>
</div>

<?php 


include('includes/footer.php'); 



?>