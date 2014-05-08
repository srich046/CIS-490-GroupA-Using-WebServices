function showSingleTicketDetails(ticketID)
{
	xmlhttp = buildHttpRequest();
	xmlhttp.onreadystatechange=function()
	{
		if (xmlhttp.readyState==4 && xmlhttp.status==200)
		{
		document.getElementById("ticketResults").innerHTML=xmlhttp.responseText;
		}
	}
	xmlhttp.open("GET","GetTicketDetails.php?TID="+ticketID,true);
	xmlhttp.send();
}

function showTicketList()
{
	xmlhttp = buildHttpRequest();
	xmlhttp.onreadystatechange=function()
	{
		if (xmlhttp.readyState==4 && xmlhttp.status==200)
		{
		document.getElementById("selectedTickets").innerHTML=xmlhttp.responseText;
		}
	}
	xmlhttp.open("POST","GetTicketList.php",true);
	xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");
	xmlhttp.send(getRequestString());
}

function getRequestString()
{
	var requestString = "";
	var constraintArray = new Array();
	//constraintArray.push("whatever");
	
	//Iterate through every input element. 
	//This even works for Select types. select's name is this.name, and the selected input value is this.value
	$(":input").each(function() {
		if (this.value != "")
		{
			console.log(this.name + ": " + this.value);
		}
	});
	
	
	
	
	return "priorityLevel=3&storeName=Post&status=open";
}


function buildHttpRequest()
{
	if (window.XMLHttpRequest)
	{// code for IE7+, Firefox, Chrome, Opera, Safari
		xmlhttp=new XMLHttpRequest();
	}
	else
	{// code for IE6, IE5
		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	}
	
	return xmlhttp;
}
