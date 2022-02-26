<?php
$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

$num=$_POST['Stage'];

$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT ID FROM Info";//Info에 들어있는 모든 아이디를 불러온다
$result=mysqli_query($con,$sql);
while($row=mysqli_fetch_assoc($result))
{
    echo($row['ID']);//모든 아이디 정보를 echo한다
    echo("^");//^를 기준으로 아이디를 나눌것이다
}
?>