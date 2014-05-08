/* This is the web service. 
 * It takes input strings from the client (the format of which is determined by the Interface file), queries the database, and returns appropriate JSON objects
 * Right now it has...
 * Tickets:
 *      Retrieve a list (with all filtering options)
 *      Retrieve detials on an individual
 *      Update details (limited -- Assigned technician, Notes, and Priority)
 *      
 * Sites:
 *      Retrieve a list (with all filtering options)
 *      Retrieve detials on an individual
 *      
 * It has two separate functions (one for tickets, one for sites) for doing the filtering. 
 *      
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.Data.SqlClient;

namespace CRMWebServices
{   
    public class CRMWebService : ICRMWebService
    {
        //Database connections all require a login string. For the time being, this is hardcoded. In the future, it should be based off of the login on the website
        private string loginInfo = "user id=sa; password=CSUSM999!; server=98.175.250.173,22224; database=PCSynergyCRM; connection timeout=30";

//Ticket functions
        /*Gives a list of tickets, based on any filtration that the client put in.
         *Builds a WHERE clause for the SQL query, connects to the database, executes the query (with the WHERE clause)
         *Iterates through the result rows, creating TicketListing objects, populating them with the columns, and adding those items to a List
         *Returns that List at the end of the function.
        */
        public List<TicketListing> searchForTickets(string storeName, string serial, string phone, string zip, string ticketID, string status, string priority, string medium)
        {

            //Takes the parameters sent by the user and creates a WHERE clause.
            string whereClause = buildTicketConstraints(storeName, serial, phone, zip, ticketID, status, priority, medium);

            //Create a list for tickets
            List<TicketListing> searchResults = new List<TicketListing>();

            //Log into the database
            using (SqlConnection conn = new SqlConnection(loginInfo))
            {
                conn.Open();

                //Create the query that you'll use (note the whereClause variable was built above)
                string query = "SELECT PMSerialNumber AS Serial, IncidentID AS Ticket, SiteName,StateZone AS Zone, IssueTypeName, Priority  FROM VIncident " + whereClause + " ORDER BY priority ASC, DateTimeReported";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    //Execute the query
                    SqlDataReader reader = command.ExecuteReader(); 

                    //Read through results, creating TicketListing objects and filling in the details, then appending the object to the List.
                    while (reader.Read())
                    {
                        TicketListing item = new TicketListing();
                        item.Serial = reader["Serial"].ToString();
                        item.TicketID = reader["Ticket"].ToString();
                        item.SiteName = reader["SiteName"].ToString();
                        item.TimeZone = reader["Zone"].ToString();
                        item.IssueType = reader["IssueTypeName"].ToString();
                        item.Priority = reader["Priority"].ToString();

                        searchResults.Add(item);
                    }//end while
                }//end command
            }//end connection

            return searchResults;
        }//end searchForTickets()
//---------------------------------------------------------

        /*Get the details on an individual ticket, based on a ticket ID.
         *Connects to the database, builds a query for the ticket ID, executes it, and reads the results into an item, which gets returned at the end
         */
        public TicketDetails getTicketDetails(string ticketID)
        {
            TicketDetails details = new TicketDetails();

            //Connect to the database
            using (SqlConnection conn = new SqlConnection(loginInfo))
            {
                conn.Open();
                string query = " WITH VTicketDetail AS (Select ROW_NUMBER() OVER (ORDER BY priority asc, DateTimeReported) AS 'NumberInQueue',* FROM Vincident where Pending=1)  SELECT V.SiteName, V.IncidentID,V.ReportedBycontactName as Contact, V.NumberInQueue, V.SiteAddress, V.CityStateZip, V.SitePhone, V.SiteEmail, V.AssignedToTechRef as TechID, Priority, Entered, DateTimeReported, Medium as ContactMedia, IssueTypeName, AppointmentDateTime, LegacyNotes FROM VTicketDetail as V WHERE IncidentID=" + ticketID + " Order by V.Priority asc, V.DateTimeReported";

                //Set up the command for query
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    //Execute the query
                    SqlDataReader reader = command.ExecuteReader();

                    //Read through the results, adding the columns into the details object
                    while (reader.Read())
                    {
                        details.SiteName = reader["SiteName"].ToString();
                        details.TicketID = reader["IncidentID"].ToString();
                        details.QueuePosition = reader["NumberINQueue"].ToString();
                        details.ContactName = reader["Contact"].ToString();
                        details.Address = reader["SiteAddress"].ToString();
                        details.Phone = reader["SitePhone"].ToString();
                        details.Email = reader["SiteEmail"].ToString();
                        details.AssignedTo = reader["TechID"].ToString();
                        details.Priority = reader["Priority"].ToString();
                        details.EnteredBy = reader["Entered"].ToString();
                        details.ReportedOn = reader["DateTimeReported"].ToString();
                        details.ContactMedia = reader["ContactMedia"].ToString();
                        details.IssueType = reader["IssueTypeName"].ToString();
                        details.Appointment = reader["AppointmentDateTime"].ToString();
                        details.IssueDetails = reader["LegacyNotes"].ToString();
                    }//end while loop
                }//end command
            }//end connection

            return details;
        }//end getTicketDetails()
//---------------------------------------------------------

        //Create a new ticket
        public bool createNewTicket(string ticketInformation)
        {
            throw new NotImplementedException();
        }
//---------------------------------------------------------

        /*Attempts to update details on a specific ticket.
         * Returns a boolean value -- if the query fails for any reason, it returns false.
         * Like all the other commands with varying queries, we build pieces of the query string and then put them together, and then execute.
         */
        public bool updateTicket(string ticketID, string priority, string technician, string legacyNotes)
        {
            //Connect to the database
            using (SqlConnection conn = new SqlConnection(loginInfo))
            {
                conn.Open();

                string query = buildUpdateTicketQuery(ticketID, priority, technician, legacyNotes);
                //Execute the query
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
                catch (Exception e) { return false; }
            }//end connection

            return true;
        }//end updateTicket()
//---------------------------------------------------------

        /*Called by updateTicket(), constructs the string to use for updating a ticket.
         */
        private string buildUpdateTicketQuery(string ticketID, string priority, string technician, string legacyNotes)
        {
            //Construct the pieces of the query string here. "null" means the user entered nothing.
            List<string> updates = new List<string>();
            if (!priority.Equals("null"))
                updates.Add("dbo.Incident.Priority='" + priority + "'");

            if (!technician.Equals("null"))
                updates.Add("dbo.Incident.AssignedToTechRef='" + technician + "'");

            if (!legacyNotes.Equals("null"))
                updates.Add("dbo.Incident.LegacyNotes='" + legacyNotes + "'");

            //Here's where we build up the whole string.
            string query = "";
            if (updates.Count > 0)
            {
                foreach (string piece in updates)
                {
                    //query = "UPDATE dbo.Incident SET ";
                    //query += piece + ", ";

                    query += ((query.Length == 0) ? "UPDATE dbo.Incident SET " : ", ") + piece;
                }
                query += " WHERE dbo.Incident.IncidentID='" + ticketID + "'";
            }

            return query;
        }

        /*Return a string used as a WHERE clause for searching for tickets.
         *Creates a List of strings, each of which is a piece of the WHERE clause
         *  "null" is the value input by the client if the user didn't specify anything.
         *After the List is totally built, iterate through the strings in it and build the proper string
         *  The first item in it will be preceeded by "WHERE " and then each other item will be preceeded by " AND "
         */
        private string buildTicketConstraints(string storeName, string serial, string phone, string zip, string ticketID, string status, string priority, string medium)
        {
            
            List<string> constraintList = new List<string>();

            //Name
            if (!storeName.Equals("null"))
                constraintList.Add("SiteName LIKE '%" + storeName + "%'");
            
            //Serial
            if (!serial.Equals("null"))
                constraintList.Add("PMSerialNumber='" + serial + "'");
        
            //Phone
            if (phone.Length == 10)
            {
                string phoneProper = "ContactPhone LIKE '%(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6, 4) +"%'";
                constraintList.Add(phoneProper);
            }

            //Zip

            /*ATTENTION: I don't think zip is the right name.
             if (zip.Length != 0)
                constraintList.Add("Zip='" + zip + "'");
            */
            
            //Ticket
            if (!ticketID.Equals("null"))
                constraintList.Add("IncidentID='" + ticketID + "'");

            //Status
            if (!status.Equals("null"))
                constraintList.Add("pending='" + status + "'");
            
            //Priority
            if (!priority.Equals("any"))
                constraintList.Add("Priority='" + priority + "'");
            
            //Medium
            /*ATTENTION: not sure what the query is
            if (!medium.Equals("null"))
                constraintList.Add("Medium='" + medium + "'");
            */

            //Now that we have an array of pieces, put it together into a proper statement, started with "WHERE" and joined by "AND" clauses.
            string constraintString = "";
            foreach (string constraint in constraintList)
            {
                constraintString += (constraintString.Length == 0)? " WHERE " : " AND ";
                constraintString += constraint;
            }

            return constraintString;
        }//end buildTicketConstraints()
//---------------------------------------------------------

//---------------------------------------------------------


//Site functions
        /*Gives a list of sites, based on any filtration that the client put in.
         *Builds a WHERE clause for the SQL query, connects to the database, executes the query (with the WHERE clause)
         *Iterates through the result rows, creating SiteListing objects, populating them with the columns, and adding those items to a List
         *Returns that List at the end of the function.
        */
        public List<SiteListing> searchForSites(string serial, string zip, string storeName, string city, string status, string lastName, string phone, string email)
        {
            //Takes the parameters sent by the user and creates a WHERE clause.
            string whereClause = buildSiteConstraints(serial, zip, storeName, city, status, lastName, phone, email);

            //Create a list for Sites
            List<SiteListing> searchResults = new List<SiteListing>();

            //Connect to the database
            using (SqlConnection conn = new SqlConnection(loginInfo)) 
            {
                conn.Open();

                //Build the SQL query (note that we built whereClause above)
                string query = "SELECT PMSerialNumber, SiteName, ContactFirst_Last FROM VSite " + whereClause + " ORDER BY PMSerialNumber";

                //Create the command
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    //Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    //Read through results, creating SiteListing objects and filling in the details, then appending the object to the List.
                    while (reader.Read())
                    {
                        SiteListing site = new SiteListing();
                        site.ContactName = reader["ContactFirst_Last"].ToString();
                        site.Name = reader["SiteName"].ToString();
                        site.SiteID = reader["PMSerialNumber"].ToString();
                        searchResults.Add(site);
                    }//end while
                }//end command
            }//end conn
            return searchResults;
        }//end searchForSites()

//---------------------------------------------------------
        /*Get the details on an individual sites, based on a site ID.
         *Connects to the database, builds a query for the site ID, executes it, and reads the results into an item, which gets returned at the end
         */
        public SiteDetails getSiteDetails(string siteID)
        {
            SiteDetails details = new SiteDetails();

            //Connect to the database
            using (SqlConnection conn = new SqlConnection(loginInfo))
            {
                conn.Open();
                string query = "SELECT * FROM VSite WHERE PMSerialNumber='" + siteID + "';";

                //Create the command
                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    //Execute the command
                    SqlDataReader reader = command.ExecuteReader();
                    
                    //Read the results and put them into an object
                    while (reader.Read())
                    {
                        details.FullSN = reader["PMSerialNumber"].ToString();
                        details.Version = reader["Version"].ToString();
                        details.Status = reader["Status"].ToString();
                        details.Organization = reader["OrgName"].ToString();
                        details.NextDue = reader["SupportNextDueDate"].ToString();
                        details.ContactName = reader["ContactFirst_Last"].ToString();
                        details.Address = reader["SiteAddress"].ToString();
                        details.Phone = reader["SitePhone"].ToString();
                        details.Email = reader["SiteEmail"].ToString();
                        details.Notes = reader["LegacyNotes"].ToString();
                        details.Name = reader["LegacyNotes"].ToString(); 
                    }//end while loop
                }//end command
            }//end connection
            return details;
        }//end getSiteDetails()
//---------------------------------------------------------

        /*Returns a string to be used as a WHERE clause in the sql query for site listing.
         *Goes through all of the input values, for those which aren't "null", it creates a piece of the clause for each.
         *Afterwards, it creates the entire string (prepended by "WHERE " and connected by " AND ") and returns that.
         */
        private string buildSiteConstraints(string serial, string zip, string storeName, string city, string status, string lastName, string phone, string email)
        {
            List<string> constraintList = new List<string>();
            
            if (!serial.Equals("null"))
                constraintList.Add("Serial='" + serial + "'");

            if (!zip.Equals("null"))
                constraintList.Add("AddressZip='" + zip + "'");

            if (!storeName.Equals("null"))
                constraintList.Add("SiteName LIKE'%" + storeName + "%'");

            if (!city.Equals("null"))
                constraintList.Add("AddressCity LIKE '%" + city + "%'");


            /*Status is slightly complicated. We want to give the option to check, for example, sites that are either Current OR Late. So we have to create a more complex string ORing the things together.
             *For simpler representation, I'm using binary representation. Current=1, Dead=2, Late=4, Prospect=8, Trial=16. 
             *For example, Current OR Late would be 5 (1+4). 
            */

            /*For Status, since you can query multiple (e.g. "sites that are either current or late"), we need to do some trickery.
             *Treat each status as a binary value (1, 2, 4, 8, 16 for current, dead, late, prospect, and trial, respectively)
             *Do a bitwise "&" to check that the input value has each of the values.
             *Build a string separated by " OR "
             */
            if (!status.Equals("null"))
            {
                int statusNum = Convert.ToInt32(status);
                List<string> statusList = new List<string>();
                //& (single ampersand) is a bitwise AND. "1 & 1" (0001 & 0001) is true, but "2 & 1" (0010 & 0001) is false.
                if ((statusNum & 1) == 1)       
                    statusList.Add("Current");
                if ((statusNum & 2) == 2)
                    statusList.Add("Dead");
                if ((statusNum & 4) == 4)
                    statusList.Add("Late");
                if ((statusNum & 8) == 8)
                    statusList.Add("Prospect");
                if ((statusNum & 16) == 16)
                    statusList.Add("Trial");

                //Once you've got all of the selected statuses, build the proper string from it.
                //It'll be something like: (Status='Dead' OR Status='Trial')
                string statusString = "(";
                foreach (string constraint in statusList)
                {
                    statusString += (statusString.Length > 1) ? " OR " : "";
                    statusString += "Status='" + constraint + "'";
                }
                statusString += ")";

                constraintList.Add(statusString);
            }

            
            if (!lastName.Equals("null"))
                constraintList.Add("ContactFirst_Last LIKE '%" + lastName + "%'");

            if (!phone.Equals("null"))
            {
                string phoneProper = "ContactPhone LIKE '%(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6, 4) + "%'";
                constraintList.Add(phoneProper);
            }

            if (!email.Equals("null"))
                constraintList.Add("SiteEmail='" + email + "'");
            
            //Now that we have all of the pieces, combine them to build the WHERE clause.
            //Note that if nothing was added, there will be a completely blank clause.
            string constraintString = "";
            foreach (string constraint in constraintList)
            {
                constraintString += (constraintString.Length == 0) ? " WHERE " : " AND ";
                constraintString += constraint;
            }

            return constraintString;
        } //end buildSiteConstraints()

//---------------------------------------------------------

    }//end CRMWebService class
}
