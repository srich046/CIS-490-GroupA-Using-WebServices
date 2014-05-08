<?php
	try {			// Address,		Port,		DB name,		  username, Pass
	$dbh = new PDO("sqlsrv:Server=98.175.250.173,22224;Database=PCSynergyCRM", "sa", "CSUSM999!");
	$dbh->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

	$dbh->prepare('SELECT * FROM ADDRESS');
	}	
	catch(Exception $e) {
		echo 'Exception -> ';
		var_dump($e->getMessage());
	}
?>