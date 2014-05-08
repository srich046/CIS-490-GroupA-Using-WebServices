<?php 
//This file checks if the user has valid login information.

$username = $_POST['username'];
if($username==null) {
		echo "<a href='Login.php'> Click here to try again. </a>";
		die ("You must enter a valid username");
}
$password = $_POST['password']; 
if($password==null) {
		echo "<a href='Login.php'> Click here to try again. </a>";
		die ("This is not a password valid my friend.");
}

		include 'connect.php';
		//echo $username .$password;
		$password='CSUSM999!';
$sql = "SELECT * FROM Technician WHERE isActive=1 and techFirst='".$username."' and password='".sha1($password)."'"; 

//echo sha1($salt . $pwd);
die($sql);
foreach ($dbh->query($sql) as $row)
{
	if ($Row[0]==null){
	//There is no username that match this password
		echo "<a href='Login.php'> Click here to try again. </a>";
		die ("Wrong username or password.");
	}
	die ("dsd");
}
		
//$SQLstring = "SELECT * FROM PPERSON WHERE username='" .$username."'// and password='".$password."'";

//queries the database for the users information.

//$Row = mysql_fetch_row($QueryResult);
//if ($Row[0]==null){
	//There is no username that match this password
	//	echo "<a href='Mainpage.php'> Click here to try again. </a>";
	//	die ("Wrong username or password.");
	//}
//else{
	//userlogged
	
	//$IDperson=$Row[0];
	//$name=$Row[1];
	//$lastname=$Row[2];
	//$username=$Row[3];
	//$email=	$Row[4];
	//$answer=$Row[5];
	//$admin=$Row[6];
	//set logout timers.
	//setcookie("IDperson", $IDperson, time()+3600);  /* expire in 1 hour */
	//setcookie("name", $name, time()+3600);  /* expire in 1 hour */
	//setcookie("lastname", $lastname, time()+3600);  /* expire in 1 hour */
	//setcookie("username", $username, time()+3600);  /* expire in 1 hour */
	//setcookie("email", $email, time()+3600);  /* expire in 1 hour */
	//setcookie("answer", $answer, time()+3600);  /* expire in 1 hour */
	//setcookie("admin", $admin, time()+3600);  /* expire in 1 hour */
	
	//header('Location: Mainpage.php'); 
//} 
?>