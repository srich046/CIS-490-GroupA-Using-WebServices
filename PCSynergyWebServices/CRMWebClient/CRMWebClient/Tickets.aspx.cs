/* This is the code for the client page, specifically for their Tickets site.
 * NOTE: Make sure on the main .aspx file, in that very first line, you have Async="true" as one of those attributes
 * 
 * This page is capable of searching for tickets, and filtering by ALMOST all of the parameters (zip and media do nothing)
 * Clicking the "Select" button to the left of an entry will retrieve details that get printed on the right side.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//You have to include these.
using System.Net.Http; //For contacting web service
using System.IO; //For Encoding
using System.Runtime.Serialization.Json; //For JSON
using System.Text;
using System.Reflection; //for PropertyInfo
using System.Text.RegularExpressions;
using System.Data;

namespace CRMWebClient
{
    public partial class _Default : Page
    {

        private HttpClient client = new HttpClient(); //For doing HTTP requests

        private DataTable dt; //This is going to be connected to the GridView on the page.

        //This event is triggered when the website loads. 
        //In this case, it sets up the columns for that GridView, and performs a cursory search (which will get all OPEN tickets)
        protected void Page_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("Serial", typeof(string));
            dt.Columns.Add("Ticket", typeof(string));
            dt.Columns.Add("Site Name", typeof(string));
            dt.Columns.Add("Zone", typeof(string));
            dt.Columns.Add("Issue Type", typeof(string));
            dt.Columns.Add("Prty", typeof(string));

            searchTickets();
        }

        //When the "Find" button is clicked, will call the function to search for tickets.
        protected void searchButton_Click(object sender, EventArgs e)
        {
            searchTickets();
        }

        //This is the function for searching for tickets.
        private async void searchTickets()
        {

            //Go build a very specific string to send to the Web Service which will query the DB, based off of the filtering options the user put in
            string constraints = buildConstraints();

            //Contact the web service. It will return an array of JSON objects, which can all be represented as a single string.
            string result = await client.GetStringAsync(new Uri("http://localhost:21954/Service1.svc/tickets/search/" + constraints));

            //Figure out how to interpret JSON objects.
            //NOTE: The "TicketListing" class is defined below. It needs to have all the same parameters as are defined on the Web Service side.
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(List<TicketListing>));
            
            //Then, interpret them. In this case, since it's a bunch of objects, we're storing them into a List.
            List<TicketListing> searchResults = (List<TicketListing>)JSONSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));

            //Iterate through the list of results, taking the data and putting it into a row in that GridView on the web page.
            foreach (TicketListing ticket in searchResults)
            {
                DataRow row1 = dt.NewRow();
                row1["Serial"] = ticket.Serial;
                row1["Ticket"] = ticket.TicketID;
                row1["Site Name"] = ticket.SiteName;
                row1["Zone"] = ticket.TimeZone;
                row1["Issue Type"] = ticket.IssueType;
                row1["Prty"] = ticket.Priority;
                dt.Rows.Add(row1);  //Make sure you .Add(), like popping into a stack
            }

            //Once you're done adding to the DataTable object, set the GridView to read from the object, and bind it.
            ticketsGridView.DataSource = dt;
            ticketsGridView.DataBind();             
        }

        //This function gets very verbose details on a specific ticket. Used for filling in that panel on the right side.
        private async void getDetails(string ticketID)
        {
            //Contact the web service with a specific URI, which includes the ticket ID. The JSON object returned gets stored into "result"
            string result = await client.GetStringAsync(new Uri("http://localhost:21954/Service1.svc/tickets/details/" + ticketID + "/"));

            //Figure out how to interpret JSON objects
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(TicketDetails));

            //Then, interpret them, and store them into an object.
            TicketDetails ticket = (TicketDetails)JSONSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));

            //Once there's an object with all the details, update the text fields on the webpage. This is just like doing something.innerHTML = 'whatever';
            detailsSiteName.Text = ticket.SiteName;
            detailsTicketID.Text = ticket.TicketID;
            detailsContact.Text = ticket.ContactName;
            detailsQueue.Text = ticket.QueuePosition;
            detailsAddress.Text = ticket.Address;
            detailsPhone.Text = ticket.Phone;
            detailsEmail.Text = ticket.Email;
            detailsAssignedTo.Text = ticket.AssignedTo;
            detailsPriority.Text = ticket.Priority;
            detailsReportedBy.Text = ticket.EnteredBy;
            detailsReportedOn.Text = ticket.ReportedOn;
            detailsMedia.Text = ticket.ContactMedia;
            detailsIssueType.Text = ticket.IssueType;
            detailsAppointment.Text = ticket.Appointment;
            detailsIssue.Text = ticket.IssueDetails;
        }


        //This creates a string for contacting the web service, with all of the filtering options used.
        //Anything not used gets a "null" instead. The way that the web service works, it can't handle an actual null value, so I'm using the string "null" to represent that instead.
        private string buildConstraints()
        {
            string constraintString = "" +
                ((filterSiteName.Text.Length > 0) ? filterSiteName.Text : "null") + "/" +
                ((filterSerial.Text.Length > 0) ? filterSerial.Text : "null") + "/" +
                ((filterPhone.Text.Length > 0) ? Regex.Replace(filterPhone.Text, @"\D", "") : "null") + "/" +   //For the phone, strip out any non-digit characters. Accounting for (123)456-7890, or 123-456-7890, or 123 . 456 . 7890, etc.
                ((filterZip.Text.Length > 0) ? filterZip.Text : "null") + "/" +
                ((filterTicket.Text.Length > 0) ? filterTicket.Text : "null") + "/" +
                filterStatus.SelectedValue + "/" +
                filterPriority.SelectedValue + "/" +
                filterMedium.SelectedValue + "/";

            return constraintString;
        }

        //When someone clicks the "Select" button next to a row, we want to get the details for that ticket.
        protected void ticketsGridView_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = ticketsGridView.SelectedRow;
            getDetails(row.Cells[2].Text);
        }

        //This was the button I made in our meeting to show yall how to do it. It doesn't need to stay.
        protected void Button5_Click(object sender, EventArgs e)
        {
            Literal1.Text = "hi friends.";
        }

    }//end page class




    [Serializable] //You MUST declare as serializable, or you're gonna have a bad time.
    public class TicketDetails
    {
        public string SiteName;
        public string TicketID;
        public string QueuePosition;
        public string ContactName;
        public string Address;
        public string Phone;
        public string Email;
        public string AssignedTo;
        public string Priority;
        public string EnteredBy;
        public string ReportedOn;
        public string ContactMedia;
        public string IssueType;
        public string Appointment;
        public string IssueDetails;

        public string Serial;
        public string TimeZone;
    }

    [Serializable]
    public class TicketListing
    {
        public string Serial;
        public string TicketID;
        public string SiteName;
        public string TimeZone;
        public string IssueType;
        public string Priority;
    }

}