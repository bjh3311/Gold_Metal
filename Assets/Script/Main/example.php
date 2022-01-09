<?php

$servername="bjh3311.cafe24.com";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Input_user'];
$pass=$_POST['Input_pass'];
$con=new mysqli($servername,$ID,$Pa,$dbname);
$query="SELECT * FROM Info ";
$result=mysqli_query($con,$query);
if($result)
{
    echo("디비연동 성공!!!");
}
else
{
    echo("디비 쿼리 실패 ㅠㅠ");
}
?>