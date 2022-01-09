<?php

$servername="bjh3311.cafe24.com";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Input_user'];
$pass=$_POST['Input_pass'];


$con=new mysqli($servername,$username,$password,$dbname);
echo("Connection 성공!!");
if(!$con)
{
    die(mysqli_connection_error());
}

?>