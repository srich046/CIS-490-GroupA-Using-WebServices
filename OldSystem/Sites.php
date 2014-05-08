<!-- 	Sites.php
	Author: Kevin novak
	Description: List of customer sites. 
		Able to filter sites by various parameters.
		Clicking on a site will populate an area with more details.
-->

<?php
	include 'connect.php';
?>

<html lang="en">
	<head>
		<title> PC Synergy CRM </title>
		<meta charset="utf-8" />
		
		<link type="text/css" rel="stylesheet" href="CSS/Sites.css" />
		<script src="js/Sites.js"></script>
	</head>
	
	<body>
		<h2>Sites</h2>
		
		<table>
		<form name="filterForm" method="post" action="<?php echo $_SERVER['PHP_SELF']; ?>">
			<tr>
				<td>Store</td>
				<td>Status</td>
				<td>Contact</td>
				<td>Search</td>
			</tr>
			<tr>
				<td  class="border">Serial number:  <input type="text" name="serialNum" size="11" />
					Zip: <input type="text" name="zip" size="11" /> <br />
					Store Name: <input type="text" name="storeName" size="11" /> <br />
					City: <input type="text" name="city" size="11" />
				</td>
				<td  class="border">
					<!--Note that status is an array. Used so you can select multiple. -->
					<?php
					
						//If they selected a status by clicking a link on the left side, make sure the Status checkbox area reflects that.
						if(isset($_GET['Status']))
						{
							$status = $_GET['Status'];
							echo "<input type='checkbox' name='status[]' value='current' " 	. ($status == 'CURRENT'? "checked" : "") . ">	CURRENT <br />";
							echo "<input type='checkbox' name='status[]' value='dead' " 	. ($status == 'DEAD'? "checked" : "") . ">		DEAD <br />";
							echo "<input type='checkbox' name='status[]' value='late' " 	. ($status == 'LATE'? "checked" : "") . ">		LATE <br />";
							echo "<input type='checkbox' name='status[]' value='prospect' " . ($status == 'PROSPECT'? "checked" : "") . ">	PROSPECT <br />";
							echo "<input type='checkbox' name='status[]' value='trial' " 	. ($status == 'TRIAL'? "checked" : "") . ">		TRIAL <br />";
						}
						
						else
						{
							echo "<input type='checkbox' name='status[]' value='current'>	CURRENT <br />";
							echo "<input type='checkbox' name='status[]' value='dead'>		DEAD <br />";
							echo "<input type='checkbox' name='status[]' value='late' checked>		LATE <br />";
							echo "<input type='checkbox' name='status[]' value='prospect'>	PROSPECT <br />";
							echo "<input type='checkbox' name='status[]' value='trial'>		TRIAL <br />";

						}
					?>
				</td>
				<td  class="border">
					Last name:	<input type="text" name="lastName" size="11">		<br />
					Phone number: <input type="text" name="phoneNum" size="11">		<br />
					Email:	<input type="text" name="email" size="11">			<br />
				</td>
				<td  class="border">
					<table>
						<tr>
							<td><input type="submit" name="submit" value="Find" style="width: 100%"></td>
							<td><input type="button" value="Clear" style="width: 100%" disabled></td>
						<tr>
							<td><input type="button" value="Excel" style="width: 100%" disabled></td>
							<td><input type="button" value="Save" style="width: 100%" disabled></td>
						</tr>
					</table>
			</tr>
			</form>
		</table>
		<br />
		<table>
			<tr>
				<td>Sites</td>
				<td>Information</td>
				<td>Details</td>
			</tr>
			
			<tr> 
				<td  class="border" style="width: 150px; vertical-align: top; padding: 5px">
				<?php
				
				//In the "SITES" pane, build links for different statuses.
				echo "Default queries<br />";
				$sql = "SELECT DISTINCT Status from VSite";
				foreach ($dbh->query($sql) as $row)
				{
					echo "&emsp;<a href='" . $_SERVER['PHP_SELF'] . "?Status=" . $row['Status'] . "'>" . $row['Status'] . "</a><br />";
				}
				echo "Saved queries";
				
				?>
					<center><button type="button" id="TicketsButton" onclick="window.location.href='Home.php'">Tickets</button><center>
				
				</td>
				<td  class="border" style="height: 500px">
					<!--This extra table exists so that you can have headers which don't scroll with the results -->
					<table name="sitesHeaders">
						<tr>
							<td style="width: 48px">Serial</td>
							<td style="width: 297">Site Name</td>
							<td style="width: 200">Contact</td>
						</tr>
					</table>
					<div style="overflow-y: scroll; height: 90%"> <!--This div exists for the scrolling. Because it's weird if you try to set this for a td directly -->
					<table name="sitesList">
					
						<!--Ensures the columns line up-->
						<tr style="height: 0px; visibility: hidden; ">
							<td style="width: 48px"></td>
							<td style="width: 297"></td>
							<td style="width: 200"></td>
						</tr>
						<?php
							$ConstraintArray = array();
							
						/*In this PHP section, you are building the DB query.
						It will check if there was a POST. If so, it will go through each form field, and create a string for the DB query (e.g. Name='Bob').
						Each of those strings is added to an array.
						Once finished with the form, it will take that array and create the full string, including ANDs between clauses.
						
						Note: DB has the following: 2469 CURRENT, 2676 DEAD, 17 LATE, 0 PROSPECT, 0 TRIAL
						*/
							if (isset($_POST['submit'])) {	//If POST is set, then they hit the "Find" button, and have some kind of search constraints.
							//STORE
								if (!empty($_POST['serialNum'])) {
									array_push($ConstraintArray, " Serial= '" . $_POST['serialNum'] . "' ");
								}
								
								if (!empty($_POST['storeName'])) {
									array_push($ConstraintArray, " SiteName LIKE '%" . $_POST['storeName'] . "%' ");
								}
								if (!empty($_POST['zip'])) {
									array_push($ConstraintArray, " AddressZip = '" . $_POST['zip'] . "' ");
								}
								if (!empty($_POST['city'])) {
									array_push($ConstraintArray, " AddressCity LIKE '%" . $_POST['city'] . "%' ");
								}
								
							//STATUS
								/*Status has 5 checkboxes. Go through each of the checked ones to create a part of the string, held together with ORs.
								Will end up with something like, "(Status='Current' OR Status='Prospect')". Parens are important because this uses OR operator.
								*/
								if (!empty($_POST['status']))
								{
									$Statuses = $_POST['status'];	//An array of just the boxes that ARE checked.
									$N = count ($Statuses);
									
									$StatusString = " (Status='" . $Statuses[0] . "' ";
									for ($i = 1; $i < $N; $i++)
									{
										$StatusString .= " OR Status='" . $Statuses[$i] . "'";
									}
									$StatusString .= ")";
									array_push($ConstraintArray, $StatusString);
								} 
								
							//CONTACT
								if (!empty($_POST['lastName'])) {
									array_push($ConstraintArray, " ContactFirst_Last LIKE '%" . $_POST['lastName'] . "%' ");
								}
								
								/*Phone numbers are weird because in database they're stored as "Business: (xxx) xxx-xxxx Fax: (yyy) yyy-yyyy". Nobody is going to input that whole thing.
								In fact, you can't even rely on people to do (xxx) xxx-xxxx in that exact format. 
								So what we do here is strip every non-digit from the string, and put it into $TempPhone. Then build the proper format in $Phone from there.
								*/
								if (!empty($_POST['phoneNum'])) {
									$TempPhone = preg_replace ('/\D/', '', $_POST['phoneNum']);	
									$Phone = "(" . substr($TempPhone, 0, 3) . ") " . substr($TempPhone, 3, 3) . "-" . substr($TempPhone, 6, 4);
									array_push($ConstraintArray, " ContactPhone LIKE '%" . $Phone . "%' ");
								}
								if (!empty($_POST['email']))  {
									array_push($ConstraintArray, " SiteEmail = '" . $_POST['email'] . "' ");
								}
							} //end the POST check
							
							//Check if they clicked a Status link on the left pane
							else if (isset($_GET['Status']))
							{
								array_push($ConstraintArray, "Status='" . $_GET['Status'] . "'");
							}
							
							//If nothing has been selected, filter by a status at bare minimum.
							else {
								array_push($ConstraintArray, "Status='Current'");
							}

							
							/*Using the array, build the string.
							Remember to make sure it begins with "WHERE" -- Because, if the entire form is empty, and "WHERE" is hardcoded in, you will have "WHERE ORDER BY..." and that's an error.
							Make sure "AND"s are interspersed, but never on the outside of either side (e.g. "WHERE AND Name='Bob'" or "WHERE Name='Bob' AND"), because that is Errortown, population you.
							*/
							$ConstraintString = "";
							foreach ($ConstraintArray as $Value)
							{
								if (!empty($ConstraintString))
									$ConstraintString .= " AND ";
								else
									$ConstraintString = "WHERE ";
								$ConstraintString .= $Value;
							}
							
							$sql="SELECT PMSerialNumber as Serial, SiteName, ContactFirst_Last as Contact FROM VSite " . $ConstraintString . " ORDER BY PMSerialNumber";
							foreach ($dbh->query($sql) as $row)
							{
								echo "<tr class='lovelyrow' onclick='showSingleSiteDetails(" . $row['Serial'] . ")'>";
								echo "<td>".$row['Serial']."</td>";
								echo "<td>".$row['SiteName']."</td>";
								echo "<td>".$row['Contact']."</td>";
								echo "</tr>";
							}						
						?>
						<tr>
					</table>
					</div>
				</td>
				<td  class="border" id="detailsView" style="width: 500px">
				</td>
			</tr>
		</table>
	</body>
</html>



























