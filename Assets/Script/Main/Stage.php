<?php

$servername="localhost";
$ID="bjh3311";
$Pa="qowlsghks1";
$dbname="bjh3311";

//unity import
$user=$_POST['Input_ID'];
$con= new mysqli($servername,$ID,$Pa,$dbname);
$sql="SELECT * FROM Info WHERE ID='$user' ";
$result=mysqli_query($con,$sql);
if(mysqli_num_rows($result)>0)//입력한 ID와 동일한 ID가 DB에 존재할경우
{
    die("ID가 이미 존재합니다");
}
if(strlen($user)>12)
{
    die("ID는 최대 12자입니다");
}
if($pass!=$check)//두 비밀번호가 다르다면
{
    die("두 비밀번호가 일치하지 않습니다");
}
$valid=passwordCheck($pass);
if($valid[0]== false)
{
    echo $valid[1];
    exit(0);
}
if(!filter_var($email,FILTER_VALIDATE_EMAIL))
{
    die("올바른 이메일 형식이 아닙니다");
}
//모든 조건을 통과했으니 DB에 INSERT한다

$sql="INSERT INTO Info VALUES('$user','$pass','$email',1)";
//유저가 입력한 아이디,비밀번호,이메일 그리고 Stage는 무조건 1부터 시작
mysqli_query($con,$sql);
echo("회원가입 성공!!");
?>