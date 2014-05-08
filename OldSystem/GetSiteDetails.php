<?php
	$q = intval($_GET['SID']);
	
	include 'connect.php';

	$sql = "SELECT * FROM VSite WHERE PMSerialNumber='" . $_GET['SID'] . "';";
	echo "<table>";
	foreach ($dbh->query($sql) as $row)
	{
		echo "<tr><td>Full SN: </td><td>" . $row['PMSerialNumber'] . " 	</td></tr>";
		echo "<tr><td>Version: </td><td>" . $row['Version'] . " 	</td></tr>";
		echo "<tr><td>			Status: </td><td>" . $row['Status'] . " 	</td></tr>";
		echo "<tr><td>Organization: </td><td>" . $row['OrgName'] . " 	</td></tr>";
		$dueDate = date("m/d/y h:i:s A", strtotime($row['SupportNextDueDate']));
		echo "<tr><td>Next Due: </td><td>" . $dueDate . " 	</td></tr>";
		echo "<tr><td>Contact: </td><td>" . $row['ContactFirst_Last'] . " 	</td></tr>";
		echo "<tr><td style='vertical-align: top'>Address: </td><td>" . $row['SiteAddress'] . " 	</td></tr>";
		echo "<tr><td style='vertical-align: top'>Phone: </td><td>" . $row['SitePhone'] . " 	</td></tr>";
		echo "<tr><td>Email: </td><td>" . $row['SiteEmail'] . " 	</td></tr>";
		echo "<tr><td style='vertical-align: top'>Notes: </td><td>" . $row['LegacyNotes'] . " 	</td></tr>";

	}
	echo "</table>";
	
	echo "<input type='button' value='More Details' onclick=\"location.href='SiteDetails.php?SID=" . $_GET['SID'] . "'\" style='float: right'>";
?>