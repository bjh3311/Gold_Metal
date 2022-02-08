<?php
$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

$user=$_POST['Find_user'];

$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT * FROM Info WHERE ID='$user' ";
$result=mysqli_query($con,$sql);
while($row=mysqli_fetch_assoc($result))
{
    echo($row['Stage']);//현재 Stage 정보를 echo한다
}
?>