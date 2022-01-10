<?php

$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Sign_user'];
$pass=$_POST['Sign_pass'];
$check=$_POST['Sign_check'];
$email=$_POST['Sign_email'];
if($user==null||$pass==null||$check==null||$email==null)
{
    die("반드시 다 입력해야 합니다");
}
$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT * FROM Info WHERE ID='$user' ";
$result=mysqli_query($con,$sql);
if(mysqli_num_rows($result)>0)//입력한 ID와 동일한 ID가 DB에 존재할경우
{
    die("ID가 이미 존재합니다");
}
if($pass!=$check)
{
    die("두 비밀번호가 일치하지 않습니다");
}

?>