/* This is the Interface of the web service. It is an intermediary between the client and the service.
 * It determines how the two can interact: how the client contacts the server, and what the server will return.
 * 
 * Because the service returns JSON objects, this also declares the classes and their members. 
 *      NOTE: The members have to match on the client side in order for it to interpret them.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CRMWebServices
{
    [ServiceContract]
    public interface ICRMWebService
    {
//Ticket features:
        //List
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/tickets/search/{storeName}/{serial}/{phone}/{zip}/{ticketID}/{status}/{priority}/{medium}/")]
        List<TicketListing> searchForTickets(string storeName, string serial, string phone, string zip, string ticketID, string status, string priority, string medium);

        //Individual Details
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/tickets/details/{ticketID}/")]
        TicketDetails getTicketDetails(string ticketID);

        //Create
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/tickets/create/{ticketInformation}/")]
        Boolean createNewTicket(string ticketInformation);

        //Update
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/tickets/update/{ticketID}/{priority}/{technician}/{legacyNotes}/")]
        Boolean updateTicket(string ticketID, string priority, string technician, string legacyNotes);





//Site features
        //List
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sites/search/{serial}/{zip}/{storeName}/{city}/{status}/{lastName}/{phone}/{email}/")]
        List<SiteListing> searchForSites(string serial, string zip, string storeName, string city, string status, string lastName, string phone, string email);

        //Individual details
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sites/details/{siteID}/")]
        SiteDetails getSiteDetails(string siteID);
        
        //Create

        //Update - Split into multiple functions, because of the many different things that can be updated.
        /*
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sites/update/{siteID}/")]
        Boolean updateSite(string siteID);
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sites/update/address/{siteID}/{street}/{suite}/{city}/{zip}/{country}/{state}/")]
        Boolean updateSiteAddress(string siteID, string street, string suite, string city, string zip, string country, string state);
        */

    }


//Ticket objects
    //Fewer details, used when querying for a list of tickets
    [DataContract]
    public class TicketListing
    {
        [DataMember] public string Serial {get; set;}
        [DataMember] public string TicketID {get; set;}
        [DataMember] public string SiteName { get; set; }
        [DataMember] public string TimeZone { get; set; }
        [DataMember] public string IssueType { get; set; }
        [DataMember] public string Priority { get; set; }
    }

    //More details, used when retrieving a single ticket's details.
    [DataContract]
    public class TicketDetails
    {
        [DataMember] public string SiteName { get; set; }
        [DataMember] public string TicketID {get; set;}
        [DataMember] public string QueuePosition { get; set; }
        [DataMember] public string ContactName { get; set; }
        [DataMember] public string Address { get; set; }
        [DataMember] public string Phone {get; set;}
        [DataMember] public string Email {get; set;}
        [DataMember] public string AssignedTo {get; set;}
        [DataMember] public string Priority { get; set; } 
        [DataMember] public string EnteredBy { get; set; } 
        [DataMember] public string ReportedOn { get; set; } 
        [DataMember] public string ContactMedia { get; set; } 
        [DataMember] public string IssueType { get; set; }
        [DataMember] public string Appointment { get; set; } 
        [DataMember] public string IssueDetails { get; set; }
     
        [DataMember] public string Serial {get; set;}
        [DataMember] public string TimeZone { get; set; }
    }

    [DataContract]
    public class TicketGroup
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Count { get; set; }
        [DataMember] public string Link { get; set; }
    }


//Site objects
    //Fewer details, used when querying for a list of tickets
    [DataContract]
    public class SiteListing
    {
        [DataMember] public string SiteID {get; set;}
        [DataMember] public string Name {get; set;}
        [DataMember] public string ContactName {get; set;}
    }

    //More details, used when retrieving a single ticket's details.
    [DataContract]
    public class SiteDetails
    {
        [DataMember] public string FullSN {get; set;}
        [DataMember] public string Version { get; set; }
        [DataMember] public string Status {get; set;}   //Binary representation. Current=1; Dead=2; Late=4; Prospect=8; Trial=16;
        [DataMember] public string Organization{get; set;}
        [DataMember] public string NextDue {get; set;}
        [DataMember] public string ContactName {get; set;}
        [DataMember] public string Address { get; set; }
        [DataMember] public string Phone {get; set;}
        [DataMember] public string Email {get; set;}
        [DataMember] public string Notes { get; set; }
 
        [DataMember] public string Name {get; set;} 
    }
 
}
