<?php
$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

$user=$_POST['Input_ID'];
$now=$_POST['Input_Stage'];//현재 몇스테이인지

$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT ONE,TWO,THREE,FOUR FROM Info WHERE ID='$user' ";//User아이디
$result=mysqli_query($con,$sql);
if($now==="1")
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['ONE']);//모든 아이디 정보를 echo한다
    }
}
else if($now==="2")
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['TWO']);//모든 아이디 정보를 echo한다
    }
}
else if($now==="3")
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['THREE']);//모든 아이디 정보를 echo한다
    }
}
else if($now==="4")
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['FOUR']);//모든 아이디 정보를 echo한다
    }
}


?>