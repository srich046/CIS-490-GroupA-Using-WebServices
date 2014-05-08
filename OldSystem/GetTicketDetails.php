<?php
	$q = intval($_GET['TID']);
	
	include 'connect.php';

	
	
	$sql = " WITH VTicketDetail AS
		(
			Select ROW_NUMBER() OVER (ORDER BY priority asc, DateTimeReported) AS 'NumberInQueue',*
			FROM Vincident where Pending=1 
		) 
		SELECT 
			V.SiteName,
			V.IncidentID,
			V.ReportedBycontactName as Contact,
			V.NumberInQueue, V.SiteAddress,
			V.CityStateZip, V.SitePhone,
			V.SiteEmail, V.AssignedToTechRef as TechID,
			Priority,
			Entered,
			DateTimeReported,
			Medium as ContactMedia,
			IssueTypeName,
			AppointmentDateTime,
			LegacyNotes
		FROM VTicketDetail as V
		WHERE IncidentID=" . $q . " Order by V.Priority asc, V.DateTimeReported";
		
	
	foreach ($dbh->query($sql) as $row)
	{
		echo "Site:  <b>".$row['SiteName'] ."</b>       Ticket: <b>".$row['IncidentID']."</b>";
		echo "<br />";
		echo "Contact:  ".$row['Contact']."       Queue #:  <b>".$row['NumberInQueue']."</b>";
		echo "<br />";
		echo"Address:  ".$row['SiteAddress'];
		echo "<br />          ".$row['CityStateZip']."<br/>";
		echo "Phone:  <b>".$row['SitePhone']."</b>";
		echo "<br />";
		echo "Email:  <a href='mailto:".$row['SiteEmail']."' >".$row['SiteEmail']."</a>";
		echo "<br />";
	}
	
	
	


?>