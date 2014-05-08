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
    public partial class Sites : System.Web.UI.Page
    {
        private HttpClient client = new HttpClient();

        private DataTable dt;

        //When the page loads:
        protected void Page_Load(object sender, EventArgs e)
        {
            //Prepare the column names off the DataTable
            dt = new DataTable();
            dt.Columns.Add("Serial", typeof(string));
            dt.Columns.Add("Site Name", typeof(string));
            dt.Columns.Add("Contact", typeof(string));

            //And then send an initial search for sites.
           searchSites();
        }
        

        //Search for sites, potentially based off of a filtering form.
        private async void searchSites()
        {
            //Go build a very specific string to send to the Web Service which will query the DB, based off of the filtering options the user put in
            string constraints = buildConstraints(); 

            //Contact the web service. It will return an array of JSON objects, which can all be represented as a single string.
            //Note: On the web service's side, it's expecting this format: /sites/search/{serial}/{zip}/{storeName}/{city}/{status}/{lastName}/{phone}/{email}/
            string result = await client.GetStringAsync(new Uri("http://localhost:21954/Service1.svc/sites/search/" + constraints));

            //Figure out how to interpret JSON objects.
            //NOTE: The "SitesListing" class is defined below. It needs to have all the same parameters as are defined on the Web Service side.
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(List<SiteListing>));

            //Then, interpret them. In this case, since it's a bunch of objects, we're storing them into a List.
            List<SiteListing> searchResults = (List<SiteListing>)JSONSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));

            //Iterate through the list of results, taking the data and putting it into a row in that GridView on the web page.
            foreach (SiteListing site in searchResults)
            {
                DataRow row1 = dt.NewRow();
                row1["Serial"] = site.SiteID;
                row1["Site Name"] = site.Name;
                row1["Contact"] = site.ContactName;
                dt.Rows.Add(row1);  //Make sure you .Add(), like popping into a stack
            }

            //Once you're done adding to the DataTable object, set the GridView to read from the object, and bind it.
            sitesGridView.DataSource = dt;
            sitesGridView.DataBind();          
        }
        private string buildConstraints()
        {
            string constraintString = "" +
                ((serialNumberTextBox.Text.Length > 0) ? serialNumberTextBox.Text : "null") + "/" +      //Serial
                ((zipTextBox.Text.Length > 0) ? zipTextBox.Text : "null") + "/" +      //Zip
                ((storeNameTextBox.Text.Length > 0) ? storeNameTextBox.Text : "null") + "/" +      //Storename
                ((cityTextBox.Text.Length > 0) ? cityTextBox.Text : "Phoenix") + "/" +   //City, currently set to phoenix if nothing is entered for initialization abilities
                //these null statements do not have a text box to filter for but are needed because of the structure required in the JSON serializer
                "null/" +//status
                "null/" +//lastname
                "null/" +//phone
                "null/"; //email
                //((filterSiteID.Text.Length > 0) ? filterSiteID.Text : "null") + "/";       //siteID

            return constraintString;
        }

        //Here's a sample of how to get details on an individual item. 
        protected void Button1_Click(object sender, EventArgs e)
        {
            searchSites();
        }

        //Get details for a specific site
        private async void getDetails(string siteID)
        {
            //Contact the web service with a specific URI, which includes the site ID. The JSON object returned gets stored into "result"
            //Note: on the web service side, the format of /sites/details/{siteID}/
            string result = await client.GetStringAsync(new Uri("http://localhost:21954/Service1.svc/sites/details/" + siteID + "/"));

            //Figure out how to interpret JSON objects
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(SiteDetails));

            //Then, interpret them, and store them into an object.
            SiteDetails site = (SiteDetails)JSONSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));

            SNLabel.Text = site.FullSN;
            VersionLabel.Text = site.Version;
            organizationLabel.Text = site.Organization;
            nextDueLabel.Text = site.NextDue;
            contactLabel.Text = site.ContactName;
            addressLabel.Text = site.Address;
            phoneLabel.Text = site.Phone;
            emailLabel.Text = site.Email;

            
            //sampleOutputSection.Text = "here are some details: " + site.Name + ",\n " + site.Status + "! " + site.Phone;
        }

        protected void sitesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = sitesGridView.SelectedRow;
            getDetails(row.Cells[1].Text);
        }



    }//end page class

    //These classes have to match the structure of the JSON files that we read from the web service.
    [Serializable]
    public class SiteListing
    {
        public string SiteID;
        public string Name;
        public string ContactName;
    }

    [Serializable]
    public class SiteDetails
    {
        public string FullSN;
        public string Version;
        public string Status;   //Binary representation. Current=1; Dead=2; Late=4; Prospect=8; Trial=16;
        public string Organization;
        public string NextDue;
        public string ContactName;
        public string Address;
        public string Phone;
        public string Email;
        public string Notes;
        public string Name;
    }
}