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

function showSingleSiteDetails(siteID)
{
	xmlhttp = buildHttpRequest();
	xmlhttp.onreadystatechange=function()
	{
		if (xmlhttp.readyState==4 && xmlhttp.status==200)
		{
		document.getElementById("detailsView").innerHTML=xmlhttp.responseText;
		}
	}
	xmlhttp.open("GET","GetSiteDetails.php?SID="+siteID,true);
	xmlhttp.send();
}