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
			<div class="map">
				<div id="LOCATION_MAP" style=""></div>
			</div>
		</div>
	</div>
</div>


<script type="text/javascript">
    
    var map = L.map('LOCATION_MAP', {scrollWheelZoom:true,zoomControl: true}).setView([23.8, 90.4], 7);
      
    map.zoomControl.setPosition('bottomright');
    
    // background tiles
    var tileLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        layers: 'tiles',
        // format: 'image/png',
        // opacity: 0.5
    });



    var ctlTilelayer = L.layerGroup([tileLayer]);
    ctlTilelayer.addTo(map);

    <?php 


        $query = "SELECT * FROM station_info";
        $result = mysqli_query($connection,$query);
        // 

        while ($row = mysqli_fetch_assoc($result)) {
            
            echo "L.marker([{$row['lat']},{$row['lon']}]).addTo(map).bindPopup('<p>Station: {$row['station_name']} </br> Station Code: {$row['station_code']}</br><a href=\"wl_data.php?station_id={$row['id']}\" >View Data</><br>Lat: {$row['lat']}, Lon: {$row['lon']}</p>');";
        }

    ?>


</script>


<?php 


include('includes/footer.php'); 



?>
