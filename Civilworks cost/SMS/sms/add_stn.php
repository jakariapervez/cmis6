<?php 

    include('includes/database.php');

    $page_tittle = "Add Station";
    include('includes/header.php'); 



    // save data

    if (isset($_POST['stn_name']) && isset($_POST['save'])){

        // print_r($_POST);

        $res = mysqli_query($connection, 
            "INSERT INTO station_info (station_name,station_code,lat,lon) VALUES ('{$_POST['stn_name']}','{$_POST['stn_code']}','{$_POST['lat']}','{$_POST['lon']}')");

        if ($res){
            $_SESSION['stn_add'] = true;
        }

        header("Location: add_stn.php");
    }


?>
<div class="content">
    <div class="container-fluid">
        

        <div class="card card-map">
			<div class="header">
                <h4 class="title">Station List</h4>
                <!-- <p class="category">Unit: Meter</p> -->

            </div>
			<div class="content">
				<table class="table table-striped">
                <tr><th>Station Name</th><th>Station Code</th> <th>Lat</th><th>Lon</th></tr>            
                <?php 
                    $query = "SELECT * FROM station_info";
                    $result = mysqli_query($connection,$query);

                    while($row = mysqli_fetch_assoc($result) ){
                  
                        echo "<tr><td>{$row['station_name']}</td><td>{$row['station_code']}</td><td>{$row['lat']}</td><td>{$row['lon']}</td></tr>";
                    } 
                ?>            
                </table>
			</div>
		</div>


       <div class="card" style="overflow: hidden;">
            <div class="header">
                <h4 class="title">Add New Station</h4>
                <!-- <p class="category">Station-Code will be generated automatically</p> -->
                
            </div>
            <div class="content">
                
                <form method="post" action="add_stn.php">
                    <div class="col-md-3 form-group">
                        <label>Station Name</label>
                        <input class="form-control border-input" type="text" name="stn_name" required>
                    </div>
                    <div class="col-md-3 form-group">
                        <label>Station Code</label>
                        <input class="form-control border-input" type="text" name="stn_code" required>
                    </div>
                    <div class="col-md-2 form-group">
                        <label>Lattitude</label>
                        <input class="form-control border-input" type="number" step="any" name="lat" required>
                    </div>
                    <div class="col-md-2 form-group">
                        <label>Longitude</label>
                        <input class="form-control border-input" type="number" step="any" name="lon" required>
                    </div>
                    
                    <div class="col-md-2 form-group">
                        <label>.</label>
                        <button type="submit" name="save" value="save" class="form-control btn btn-info btn-fill btn-wd">Save</button>
                    </div>

                </form>
            </div>
        </div>
	</div>
</div>

<?php 



include('includes/footer.php'); 



?>