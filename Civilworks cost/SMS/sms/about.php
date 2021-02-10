<?php 

    include('includes/database.php');

    $page_tittle = "About";
    include('includes/header.php'); 

?>
<div class="content">
    <div class="container-fluid">
        

        <div class="card card-map">
			<div class="header" style="padding-bottom: 10px;">
				<h4 class="title">FAQ</h4>
				</br>
				</br>
				<h5>- How can I send data?</h5>
				<p>&nbsp;&nbsp;&nbsp;&nbsp;Write station_name&lt;space&gt;water_level and send the sms to <span style="background: #f0f0f0;padding: 3px;">01678-208-528</span>, for example:
				&nbsp;&nbsp;<span style='font-family:monospace;background: #f0f0f0;padding: 3px;'>bd01&nbsp;12.11</span></p>
		        </br>
		        <h5>- Will I be charged for sending data?</h5>
				<p>&nbsp;&nbsp;&nbsp;&nbsp;Yes, standard sms charges will apply.</p>
				</br>
		        <h5>- How can I download waterlevel data of a station?</h5>
				<p>&nbsp;&nbsp;&nbsp;&nbsp;Go to <span style="background: #f8f8f8;padding: 3px;">Water Level Data</span> from the top menu then select desired station and click 'Show'. Then clicking 'Download' button will initiate downloading water-level data as a csv file.</p>
				</br>
		        <h5>- Is there any api available for downloading data with filtered date?</h5>
				<p>&nbsp;&nbsp;&nbsp;&nbsp;Yes, there is a JSON api. 
				</br></br>
				&nbsp;&nbsp;&nbsp;&nbsp;URL:<span style="background: #f8f8f8;padding: 3px;font-family:monospace;">http://api.hmrcweb.com/sms_data.php?start_date=START_DATE_YYYY-MM-DD&end_date=END_DATE_YYYY-MM-DD</span>
				</br></br>
				&nbsp;&nbsp;&nbsp;&nbsp;Example request: 
				<span style="background: #f8f8f8;padding: 3px;font-family: 'monospace'">http://api.hmrcweb.com/sms_data.php?start_date=2019-04-27&end_date=2019-04-28</span>
				</p>


            </div>
			
		</div>
	</div>
</div>

<?php 
// http://api.hmrcweb.com/sms_data.php?start_date=2019-04-27&end_date=2019-04-28

include('includes/footer.php'); 



?>
