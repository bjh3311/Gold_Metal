<?php

$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Input_ID'];
$Stage=$_POST['Input_Stage'];
$con= new mysqli($servername,$ID,$Pa,$dbname);//db에 연결
$sql="UPDATE Info SET Stage='$Stage' WHERE ID='$user' ";
mysqli_query($con,$sql);//최대 Stage를 클리어시 다음 Stage를 활성화해주는 php문이다
?>