<?php
$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

$user=$_POST['Find_user'];
$email=$_POST['Find_email'];

$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT * FROM Info WHERE ID='$user' ";
$result=mysqli_query($con,$sql);
if(mysqli_num_rows($result)==0)//입력한 ID와 동일한 ID가 DB에 존재할경우
{
    die("ID가 존재하지 않습니다");
}
while($row=mysqli_fetch_assoc($result))
    {
        if($row['E-mail']==$email)
        {
            echo($row['Password']);
        }
        else
        {
            echo("이메일이 일치하지 않습니다");
        }
    }
?>