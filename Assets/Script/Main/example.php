<?php

$servername="bjh3311.cafe24.com";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Input_user'];
$pass=$_POST['Input_pass'];
$con=new mysqli($servername,$username,$password,$dbname);
if(!$con)
{
    die("Connection Failed.".mysqli_connection_error());
}
else
{
    echo("Connection Success");
}

?>