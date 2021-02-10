<?php 

include('includes/database.php');


$query = "SELECT st.station_name, dat.value,dat.date 
		from station_info st JOIN sms_data dat 
		on st.id = dat.station_id WHERE dat.station_id={$_GET['station_id']}";

$res = mysqli_query($connection, $query);



header('Content-Type: text/csv; charset=utf-8');
header('Content-Disposition: attachment;filename="water_level.csv"');
// create a file pointer connected to the output stream
$output = fopen('php://output', 'w');
// output the column headings
fputcsv($output, array('station_name', 'water_level', 'time'));


while ($row = mysqli_fetch_assoc($res)){
	fputcsv($output, $row);
}

?>