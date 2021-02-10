<?php 

    include('includes/database.php');
    $st_name = "";
    $station_id = 1; //default
    if ( isset($_GET['station_id']) && $_GET['station_id']!=""){
    
        $station_id = $_GET['station_id'];

    }

    $query = "SELECT * FROM station_info WHERE id={$station_id}";
    $loc_result = mysqli_query($connection,$query);
    
    if (mysqli_num_rows($loc_result) >=1){
        $_loc_array = mysqli_fetch_assoc($loc_result);
        $st_name = $_loc_array['station_name'];
    }

    $page_tittle = "Water Level Data";
    include('includes/header.php'); 


    // station wl dta
    $plot_data = "";
    $noRecord = false;
    $wl_query = "SELECT * FROM sms_data WHERE station_id={$station_id} ORDER BY date ASC";
    $wl_result = mysqli_query($connection,$wl_query);
    if (mysqli_num_rows($wl_result) > 0 ){

        while ( $row = mysqli_fetch_assoc($wl_result)) { 
        
            $split_date = str_replace(' ',',',$row['date']);
            $split_date = str_replace('-',',',$split_date);
            $split_date = str_replace(':',',',$split_date);
            $date_array = explode(',', $split_date);
            $fin_date = "";
            for ($i=0; $i<sizeof($date_array);$i++){

                if ($i==1){

                    $fin_date .= (string)( (int)($date_array[$i]-1) ).',';
                }else{
                    $fin_date .=$date_array[$i].',';
                }
            }
            $fin_date = rtrim($fin_date,",");
            $plot_data .= "[ Date.UTC({$fin_date}), {$row['value']}],";

        } 

    }else{
        $noRecord = true;
    } 

?>
<div class="content">
    <div class="container-fluid">
        <div class="card">
            <div class="content" style="overflow: hidden;">
                <form>
                    <div class="form-group col-md-10 text-center">      
                        <select class="form-control" name="station_id" required="true" style="border: 2px solid #66615B">
                            <option value="">SELECT LOCATION</option>
                            <?php 

                                $res = mysqli_query($connection,'SELECT * FROM station_info');

                                while($row = mysqli_fetch_assoc($res)){
                                    echo "<option value=\"{$row['id']}\">{$row['station_name']}</option>";
                                }
                            ?>                                
                        </select>
                    </div>
                    <div class="col-md-2 text-center">
                        <button type="submit" class="btn btn-default">Show</button>
                    </div>
                </form>
            </div>
        </div>

        <div class="card">
			<div class="header">
                <h4 class="title">Water Level Data of <?php echo $st_name; ?> <a style="border-radius: 5px;background-color: rgba(0,0,0,0.1);padding: 2px;" href="download.php?station_id=<?php echo $station_id; ?>">Download</a> </h4>
                <p class="category">Unit: Meter</p>

            </div>
			<div class="content">
				<div id="wl_chart"></div>
                <?php 
                    if($noRecord){
                        echo 'No Records Found';
                    } 
                    else{
                        $chart_type='spline';
                        $plot_container = "wl_chart";
                        include('includes/plot_maker.php');
                    }
                ?>            
                
			</div>
		</div>
	</div>
</div>

<?php 






// generate plot_data


include('includes/footer.php'); 



?>
