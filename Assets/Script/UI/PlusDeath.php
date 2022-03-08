<?php
$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

$user=$_POST['Input_ID'];
$now=$_POST['Input_Stage'];//현재 몇스테이인지

$con= new mysqli($servername,$ID,$Pa,$dbname);
if($now==="1")
{
    $sql="UPDATE Info SET ONE=ONE+1 , EVERY=EVERY+1  WHERE ID='$user' ";
    $result=mysqli_query($con,$sql);
}
else if($now==="2")
{
    $sql="UPDATE Info SET TWO=TWO+1 , EVERY=EVERY+1 WHERE ID='$user' ";
    $result=mysqli_query($con,$sql);
}
else if($now==="3")
{
    $sql="UPDATE Info SET THREE=THREE+1 , EVERY=EVERY+1 WHERE ID='$user' ";
    $result=mysqli_query($con,$sql);

}
else if($now==="4")
{
    $sql="UPDATE Info SET FOUR=FOUR+1 , EVERY=EVERY+1 WHERE ID='$user' ";
    $result=mysqli_query($con,$sql);
}


?>