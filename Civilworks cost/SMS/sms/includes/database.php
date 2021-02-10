<?php
// connection params
$dbhost = "localhost";
$dbuser = "root2";
$dbpass = "hmrcdb!@#$";
$dbname = "smsdb";

$connection = mysqli_connect($dbhost, $dbuser, $dbpass, $dbname);

// die if connection error
if(!$connection) { die("connection error");}

//setting_charecterset_to_utf8_unicode//
mysqli_query($connection,'SET CHARACTER SET utf8');
mysqli_query($connection,"SET SESSION collation_connection ='utf8_general_ci'");
?>


