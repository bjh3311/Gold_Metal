<?php
$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

$num=$_POST['Stage'];

$con= new mysqli($servername,$ID,$Pa,$dbname);
if($num==="0")
{
    $sql="SELECT ID,EVERY FROM Info ORDER BY EVERY ASC";//Info에 들어있는 모든 아이디를 불러온다
    $result=mysqli_query($con,$sql);
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['ID']);//모든 아이디 정보를 echo한다
        echo(" ");//공백을 기준으로 아이디와 죽은횟수를 나눌것이다
        echo($row['EVERY']);
        echo("^");//^를 기준으로 문장을 나눌것이다.
    }
}
else if($num==="1")
{
    $sql="SELECT ID,ONE FROM Info ORDER BY ONE ASC";//Info에 들어있는 모든 아이디를 불러온다
    $result=mysqli_query($con,$sql);
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['ID']);//모든 아이디 정보를 echo한다
        echo(" ");//공백을 기준으로 아이디와 죽은횟수를 나눌것이다
        echo($row['ONE']);
        echo("^");//^를 기준으로 문장을 나눌것이다.
    }
}
else if($num==="2")
{
    $sql="SELECT ID,TWO FROM Info ORDER BY TWO ASC";//Info에 들어있는 모든 아이디를 불러온다
    $result=mysqli_query($con,$sql);
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['ID']);//모든 아이디 정보를 echo한다
        echo(" ");//공백을 기준으로 아이디와 죽은횟수를 나눌것이다
        echo($row['TWO']);
        echo("^");//^를 기준으로 문장을 나눌것이다.
    }
}
else if($num==="3")
{
    $sql="SELECT ID,THREE FROM Info ORDER BY THREE ASC";//Info에 들어있는 모든 아이디를 불러온다
    $result=mysqli_query($con,$sql);
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['ID']);//모든 아이디 정보를 echo한다
        echo(" ");//공백을 기준으로 아이디와 죽은횟수를 나눌것이다
        echo($row['THREE']);
        echo("^");//^를 기준으로 문장을 나눌것이다.
    }
}
else if($num==="4")
{
    $sql="SELECT ID,FOUR FROM Info ORDER BY FOUR ASC";//Info에 들어있는 모든 아이디를 불러온다
    $result=mysqli_query($con,$sql);
    while($row=mysqli_fetch_assoc($result))
    {
        echo($row['ID']);//모든 아이디 정보를 echo한다
        echo(" ");//공백을 기준으로 아이디와 죽은횟수를 나눌것이다
        echo($row['FOUR']);
        echo("^");//^를 기준으로 문장을 나눌것이다.
    }
}

?>