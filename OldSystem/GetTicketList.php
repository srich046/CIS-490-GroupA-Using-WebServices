<?php
	include 'connect.php';
	
	$ConstraintArray = array();
	//STORE
	if(!empty($_POST['storeName'])) 
	{
		array_push($ConstraintArray, "SiteName LIKE '%" . $_POST['storeName'] . "%' ");
	}

	if(!empty($_POST['serialNum'])) 
	{
		array_push($ConstraintArray, "PMSerialNumber='" . $_POST['serialNum'] . "' ");
	}
	
	/*Phone numbers are weird because in database they're stored as "Business: (xxx) xxx-xxxx Fax: (yyy) yyy-yyyy". Nobody is going to input that whole thing.
	In fact, you can't even rely on people to do (xxx) xxx-xxxx in that exact format. 
	So what we do here is strip every non-digit from the string, and put it into $TempPhone. Then build the proper format in $Phone from there.
	*/
	if(!empty($_POST['phone'])) 
	{
		$TempPhone = preg_replace ('/\D/', '', $_POST['phone']);	
		$Phone = "(" . substr($TempPhone, 0, 3) . ") " . substr($TempPhone, 3, 3) . "-" . substr($TempPhone, 6, 4);
		array_push($ConstraintArray, " ContactPhone LIKE '%" . $Phone . "%' ");
	}

	if(!empty($_POST['zip'])) 
	{
		array_push($ConstraintArray, "???='" . $_POST['zip'] . "' ");
	}

//TICKET
	if(!empty($_POST['ticketNum'])) 
	{
		array_push($ConstraintArray, "IncidentID='" . $_POST['ticketNum'] . "' ");
	}

	if(!empty($_POST['status'])) 
	{
		array_push($ConstraintArray, "pending=" . ($_POST['status'] == 'open'? '1' : '0') . " ");
	}

	if(!empty($_POST['priorityLevel'])) 
	{
		$PriorityLevel = $_POST['priorityLevel'];
		
		//We only want to have a priority constraint if it isn't set to "any". Otherwise, don't bother.
		if (!($PriorityLevel == 'any'))
		{
			array_push($ConstraintArray, "Priority='" . $PriorityLevel . "' ");
		}
	}
	
	/*
	if(!empty($_POST['medium'])) 
	{
		array_push($ConstraintArray, "???='" . $_POST['medium'] . "' ");
	}*/
	

	$ConstraintString = "";
	foreach ($ConstraintArray as $Value)
	{
		if (!empty($ConstraintString))
			$ConstraintString .= " AND ";
		else
			$ConstraintString .= "WHERE ";
		$ConstraintString .= $Value;
	}

	$sql = "SELECT PMSerialNumber AS Serial, IncidentID AS Ticket, SiteName,StateZone AS Zone, IssueTypeName, Priority  FROM VIncident " . $ConstraintString . " ORDER BY priority ASC, DateTimeReported";
	$green="class='GreenPriority'";
	echo "<table>";
	foreach ($dbh->query($sql) as $row)
	{
		echo "<tr class='lovelyrow' onclick='showSingleTicketDetails(" . $row['Ticket'] . ")'> ";
		echo "<td>".$row['Serial']."</td>";
		echo "<td>".$row['Ticket'] ."</td>";
		echo "<td>".$row['SiteName']."</td>";
		echo "<td>".$row['Zone']."</td>";
		echo "<td>".$row['IssueTypeName']."</td>";
		if ($row['Priority']==3){
			echo "<td ".$green." >" .$row['Priority']."</td>";
		}
		else{
			echo "<td >" . $row['Priority']."</td>";
		}
		echo "</a></tr>";
	}
	echo "</table>";
?>