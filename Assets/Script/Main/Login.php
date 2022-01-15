<?php

$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Input_user'];
$pass=$_POST['Input_pass'];
$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT * FROM Info WHERE ID='$user' ";
$result=mysqli_query($con,$sql);
if(mysqli_num_rows($result)>0)//입력한 ID와 동일한 ID가 DB에 존재할경우
{
    while($row=mysqli_fetch_assoc($result))
    {
        if($row['Password']==$pass)
        {
            echo("로그인 성공!!^");
            echo($user);
            echo("^");
            echo($row['Stage']);
        }
        else
        {
            echo("비밀번호가 일치하지 않습니다^");
        }
    }
}
else
{
    echo("ID가 존재하지 않습니다^");
}
?>