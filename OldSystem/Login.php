<!-- login.php
	Author: Guilherme Fontes
	Description: CRM Login input.
	-->
<?php
	//Need to be implemented the redirect to Homepage if the username is already logged.
	/*if(isset($_COOKIE['username'])){  
	$username = $_COOKIE['username'];  
	//header( 'Location: /new_page.html' ) ;
	}
	else{
	//usuer not logged
	}*/
	
	//connecting to the DB
	//include 'connect.php';
?>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <link type="text/css" rel="stylesheet" href="CSS/Home.css" />
    <title>Cougar Sports Interactive</title>
</head>
<body id="page">
	<!--User Login.-->
			<h2> Login </h2>
				<br><br>
			<form name="login" method="post" action=validlogin.php>
				Username: <input type="text" name="username" id="username"><br>
				Password: <input type="password" name="password" id="password"><br>
				<br>
				
				<input type="submit" id="btnSubmit" value="   Submit   ">         <input type="submit" id="btnCancel" value="   Cancel   " class="cancel">
				
				<br><br>
			</form>

</body>
</html>
