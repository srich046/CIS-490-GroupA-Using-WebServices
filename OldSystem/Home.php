<!-- PCSynergyCRMHome.html
	Author: Michelle Jaro, Guilherme Fontes, Kevin Novak
	Description: CRM Main Page.
	-->
<?php
	//Need to be implemented the redirect to Login Page if the username cookie is empty
	/*if(isset($_COOKIE['username'])){  
	$username = $_COOKIE['username'];  
	}
	else{
	die("You need to be logged to accces this page");
	}*/

	$username='Training';

		//connecting to the DB
	include 'connect.php';
?>
	
<!DOCTYPE html>
<html lang="en">
	<head>
	
		<title> PC Synergy CRM </title>
		<meta charset="utf-8" />
	
	<link type="text/css" rel="stylesheet" href="CSS/Home.css" />
	<script type="text/javascript" src="js/Home.js"></script>
	<script type="text/javascript" src="js/jquery-1.10.2.js"></script>
	</head>
	<body>
	<h2> Tickets </h2>
	
	<p>
	<table>
	<form name="filterForm" method="post" action="">
		<tr>
			<td>Tools</td>
			<td>Store</td>
			<td>Ticket</td>
			<td>Search</td>
		</tr>
		<tr>
			<td class="border">
				<input name="quickTicket" type="button" value="Quick&#x00A;Ticket" style="width: 100%" disabled>		<br />
				<input name="refresh" type="button" value="Refresh" style="width: 100%" disabled>		
			</td>
			<td class="border">Store Name: <input type="text" name="storeName" size="11" />
				<br />
				Serial #: <input type="text" name="serialNum" size="11" />
				<br/>
				Phone: <input type="text" name="phone" size="11" />
				Zip: <input type="text" name="zip" size="11" />
				<br />
				<br />
			</td>

			<td class="border">
				<br />
				Ticket #: <input type="text" name="ticketNum" size="11" />
				Status:
					<select name="status">
						<option value = "open">Open</option>
						<option value = "closed">Closed (last 7 days)</option>
					</select>
				<br />
				Priority: 
					<select name="priorityLevel">
						<option value = "any">0 - Any Priority</option>
						<option value = "1">1 - High</option>
						<option value = "2">2 - Medium</option>
						<option value = "3">3 - Low</option>
					</select>
				<br />
				Medium:
					<select name="medium">
						<option value = "anyMed">Any Medium</option>
						<option value = "na">N/A</option>
						<option value = "liveCall">Live Call</option>
						<option value = "voiceMail">Voice Mail</option>
						<option value = "fax">Fax</option>
						<option value = "email">E-mail</option>
						<option value = "returnCall">Return Call</option>
						<option value = "corporateRequest">Corporate Request</option>
						<option value = "apptCall">Appointment Call</option>
						<option value = "beta">Beta</option>
						<option value = "discussionGroup">Discussion Group</option>
						<option value = "internalRequest">Internal Request</option>
						<option value = "mail">Mail</option>
						<option value = "followUp">Follow Up</option>
						<option value = "billing">Billing</option>
						<option value = "training">Training</option>
					</select>
				<br />
				<br />
			</td>
			<td class = "border">
				<input type="submit" name="submit" value="Find" style="width: 100%" onclick="showTicketList(); return false;"><br />
				<input name="clearFilter" type="button" value="Clear" style="width: 100%" disabled>		
			</td>
		</tr>
	</form>
	</table>

	<table style="max-height: 500px; overflow: hidden"> <!--Main content table-->
		<tr>
			<td>Tickets</td>
			<td>Information</td>
			<td>Ticket</td>	
		</tr>

		<tr>
			<td class="border TDTop" style="vertical-align: top; padding: 5px">
				<div class="TicketsLeft">
				<?php
					//Get the number of Open Tickets
					$sql = "SELECT COUNT(*) as OPENTICKETS FROM VIncident where Pending=1";
					foreach ($dbh->query($sql) as $row)
					{
						print 'Open Tickets('.$row['OPENTICKETS'] . ')<br />';
						$openTicket=$row['OPENTICKETS'];
					}
					
					//Get the number of Open Tickets designed to the user is logged
					$sql = "SELECT  COUNT(*) AS OpenTicketUser FROM VIncident where Tech='". $username . "' and Pending=1";
					foreach ($dbh->query($sql) as $row)
					{
						print '&emsp;Me ('.$row['OpenTicketUser'] . ')<br />';
						$openTicketUser=$row['OpenTicketUser'];
					}

					//Get the number of Open Tickets designed to anyone
					$sql = "SELECT  COUNT(*) AS OpenTicketAnyone FROM VIncident where Tech='anyone' and Pending=1";
					foreach ($dbh->query($sql) as $row)
					{
						print '&emsp;Anyone ('.$row['OpenTicketAnyone'] . ')<br />';
						$OpenTicketAnyone=$row['OpenTicketAnyone'];
					}
					
					//Get the number of Appointments
					$sql = "select count(*) as Appointments from VReminders";
					foreach ($dbh->query($sql) as $row)
					{
						print '&emsp;Appointments ('.$row['Appointments'] . ')<br />';
						$Appointments=$row['Appointments'];
					}
					
					//Get the number of Tickets Assigned
					$sql = "SELECT count(*) AS TicketsAssigned FROM VIncident where AssignedToTechRef<>4 and Pending=1";
					foreach ($dbh->query($sql) as $row)
					{
						print 'Assigned Tickets ('.$row['TicketsAssigned'] . ')<br />';
						$TicketsAssigned=$row['TicketsAssigned'];
					}
					
					
					//Get the number of Tickets Assigned
					$sql = "SELECT AssignedToTechRef  FROM VIncident WHERE Pending=1 AND AssignedToTechRef<>4  GROUP BY AssignedToTechRef ORDER BY AssignedToTechRef";
					foreach ($dbh->query($sql) as $row)
					{
						//print 'Assigned Tickets ('.$row['AssignedToTechRef'] . ')<br />';
						$AssignedToTechRef=$row['AssignedToTechRef'];
						//Each result of the query above, gonna be executed and gonna count the number of lines.
						$sqlCounter=" SELECT count(*) as AssignedNumber FROM VIncident WHERE Pending=1 AND AssignedToTechRef=".$AssignedToTechRef;
						$sqlName="SELECT TechFirst as Name From Technician where TechID=".$AssignedToTechRef;
						
						foreach ($dbh->query($sqlCounter) as $getTickes)
						{
							$AssignedNumber=$getTickes['AssignedNumber'];
						}
						foreach ($dbh->query($sqlName) as $getName)
						{
							$TechName=$getName['Name'];
						}
						
						print '&emsp;' . $TechName." (".$AssignedNumber . ')<br />';
						
					}
				?>
				</div>
				<center><div class="Sites">
					<button type="button" id="SitesButton" onclick="window.location.href='Sites.php'">Sites</button>
				</div></center>
			</td>	
			<td class="border nogap TDTop" style="height: 432px" >
				<div id="selectedTickets" style="overflow-y: scroll; height: inherit">
				<table name="tickets">
					<tr class="border">
						<td>Serial</td>
						<td>Ticket</td>
						<td>Site Name</td>
						<td>Zone</td>
						<td>Issue Type</td>
						<td>Prty</td>
					</tr>
					<?php 
					//<td>Serial</td><td>	Ticket</td>	<td>Site Name</td><td>Zone</td><td>	Issue Type	</td>	<td>Prty</td></tr>	
					//Show all the open tickets in rows.
					$ConstraintArray = array();
					if (isset($_POST['submit']))
					{
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
					}
					
					else //If they have not manually selected any constraints for searching, then by default, show all open tickets
					{
						array_push ($ConstraintArray, "pending=1");

					}
					
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
					?>
				</table>
				</div>
			</td>
			<td class="border TDTop">
				<div id="ticketResults">
				</div>
				<button type="button">Edit</button>
				<button type="button">OK</button>
				<button type="button">Respond</button>
			</td>
		</tr>
	</table>
	</body>
</html>