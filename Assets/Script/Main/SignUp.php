<?php

function passwordCheck($_str)//비밀번호 유효성 검사
{
    $pw = $_str;
    $num = preg_match('/[0-9]/u', $pw);
    $eng = preg_match('/[a-z]/u', $pw);
    $spe = preg_match("/[\!\@\#\$\%\^\&\*\(\)]/u",$pw);
    /*일치하면 1, 일치하지 않으면 0, 
    패턴을 인식하지못하는 오류발생하면 false가 return*/
    if(strlen($pw) < 4 || strlen($pw) > 12)
    {
        return array(false,"비밀번호는 영문, 숫자, 특수문자를 혼합하여 최소 4자리 ~ 최대 12자리 이내로 입력해주세요.");
        exit;
    }
    if(preg_match("/\s/u", $pw) == true)
    {
        return array(false, "비밀번호는 공백없이 입력해주세요.");
        exit;
    }
    if( $num == 0 || $eng == 0 || $spe == 0)//영문,숫자,특수문자가 다 있어야 한다
    {
        return array(false, "영문, 숫자, 특수문자를 혼합하여 입력해주세요.");
        exit;
    }
    return array(true);
}
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
}//4개중 1개라도 비어있다면 위의 메세지를 띄우고 강제 종료
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

$sql="INSERT INTO Info VALUES('$user','$pass','$email',1,0,0,0,0,0)";
//유저가 입력한 아이디,비밀번호,이메일 그리고 Stage는 무조건 -1부터 시작
mysqli_query($con,$sql);
echo("회원가입 성공!!");
?>