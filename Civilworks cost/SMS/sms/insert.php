<?php header('Content-Type: application/json');
	
	include('includes/database.php');
	$data_stat = null;
	if ( isset($_GET['number']) && isset($_GET['text']) ){

		//save raw sms for log
		$sql = "INSERT INTO raw_sms(`number`,`text`) VALUES('{$_GET['number']}','{$_GET['text']}' )" ;
		mysqli_query($connection,$sql);

		$data_stat['dbstat_raw']=mysqli_error($connection);
		//$data_stat['raw_sql'] = $sql;

		// save as data
                $msg_data = trim($_GET['text']);
                $parts = preg_split('/\s+/', $msg_data);

		if(sizeof($parts)==2){
			$data_date = date('Y-m-d H:i');
			$stn_code = strtolower($parts[0]);
			// check if station code exists in database|| otherwise a rubbish;
			$value = $parts[1];
			$sender = $_GET['number'];
			$data_sql = "insert into sms_data(station_id,sender_no,value,date)values( (select id from station_info where station_code='{$stn_code}'),'{$sender}', {$value} ,'{$data_date}')";
			mysqli_query($connection,$data_sql);
			$data_stat['dbstat_parts']=mysqli_error($connection);
			//$data_stat['data_sql']=$data_sql;

		}else{
			$data_stat['debug_info'] = 'too many items after split';
		}

	}

	echo json_encode($data_stat,JSON_PRETTY_PRINT);


 ?> 
