using Autodesk.Revit.UI;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stacker.Commands
{
    public partial class GeoZoningForm : Form
    {
        public object QueryResultRegrid { get; set; }
        public string JsonRegrid { get; set; }
        public object QueryResultZoneomics { get; set; }
        public string JsonZoneomics { get; set; }


        public GeoZoningForm()
        {
            InitializeComponent();
        }

        private void btnAPICall_Click(object sender, EventArgs e)
        {
            string addressLine1 = tbStreedAddress1.Text;
            string addressLine2 = tbStreedAddress2.Text;
            string city = cbCity.Text;
            string state = cbState.Text;
            string zip = tbZipCode.Text;
            string country = tbCountry.Text;

            string fullAddress = $"{addressLine1}, {city}, {state} {zip}, {country}";
            if (addressLine2 != "")
                fullAddress = $"{addressLine1}, {addressLine2},{city}, {state} {zip}, {country}";

            if (cbRegrid.Checked)
            {
                var clietRegrid = new RestClient("https://app.regrid.com/api/v1/search.json?");
                var requestRegrid = new RestRequest(Method.GET);

                requestRegrid.AddParameter("query", fullAddress);
                requestRegrid.AddParameter("strict", "1");
                requestRegrid.AddParameter("limit", "1");
                requestRegrid.AddParameter("token", "ay49YmoCTj_sV_p4MRpqnF9wwPKPRxpzSSK-EbGaMwpKipxZYy43oQoseSFMXECy");

                requestRegrid.AddHeader("content-type", "application/json");
                QueryResultRegrid = clietRegrid.Execute<Object>(requestRegrid).Data;
                JsonRegrid = JsonConvert.SerializeObject(QueryResultRegrid, Formatting.Indented);

                string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Regrid";
                if (Directory.Exists(directory))
                {
                    string path = $"{directory}\\JSON_Regrid_{fullAddress}.json";
                    System.IO.File.WriteAllText(@path, JsonRegrid);
                }

                if (JsonRegrid != null && JsonRegrid.Length > 30)
                {
                    lblRegridStatus.Visible = true;
                    lblRegridStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblRegridStatus.Text = "Successful";
                }
                else
                {
                    lblRegridStatus.Visible = true;
                    lblRegridStatus.ForeColor = System.Drawing.Color.Red;
                    lblRegridStatus.Text = "Failed";
                }
            }

            if (cbZoneomics.Checked)
            {
                var clientZoneomics = new RestClient("https://www.zoneomics.com/api/get_zone_details");
                var requestZoneomics = new RestRequest(Method.GET);

                requestZoneomics.AddParameter("api_key", "a9454176416681b8051d7ee5479d3e85c2afd650");
                requestZoneomics.AddParameter("address", fullAddress);
                requestZoneomics.AddParameter("output_fields", "all");

                requestZoneomics.AddHeader("content-type", "application/json");
                QueryResultZoneomics = clientZoneomics.Execute<Object>(requestZoneomics).Data;
                JsonZoneomics = JsonConvert.SerializeObject(QueryResultZoneomics, Formatting.Indented);
                string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Zoneomics";
                if (Directory.Exists(directory))
                {
                    string path = $"{directory}\\JSON_Zoneomics_{fullAddress}.json";
                    System.IO.File.WriteAllText(@path, JsonZoneomics);
                }



                if (JsonZoneomics != null && JsonZoneomics.Length > 30)
                {
                    lblZoneomicsStatus.Visible = true;
                    lblZoneomicsStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblZoneomicsStatus.Text = "Successful";
                }
                else
                {
                    lblZoneomicsStatus.Visible = true;
                    lblZoneomicsStatus.ForeColor = System.Drawing.Color.Red;
                    lblZoneomicsStatus.Text = "Failed";
                }
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TaskDialog dialog2 = new TaskDialog("Clear Data");
            dialog2.Title = "Clear Data";
            dialog2.MainInstruction = "Are you sre you would like to clear the previously entered data?";
            dialog2.MainContent = "";
            dialog2.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Yes, Clear Data");
            dialog2.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Cancel");
            TaskDialogResult dialogResult2 = dialog2.Show();

            if (dialogResult2 == TaskDialogResult.CommandLink1)
            {
                tbStreedAddress1.Text = "";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Select";
                cbState.Text = "Select";
                tbZipCode.Text = "";
                lblZoneomicsStatus.Visible = false;
                lblRegridStatus.Visible = false;
            }
            else
            {
                return;
            }
        }


        int selectPrefilledAddressesCount = 0;
        private void btnRandomAddress_Click(object sender, EventArgs e)
        {
            int totalAddresses = 6;
            int interval = selectPrefilledAddressesCount % totalAddresses;

            if (interval == 0)
            {
                tbStreedAddress1.Text = "17 Hanover St";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Stamford";
                cbState.Text = "CT";
                tbZipCode.Text = "06902";
            }
            else if (interval == 1)
            {
                tbStreedAddress1.Text = "153 E Lowell Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Salt Lake-City";
                cbState.Text = "UT";
                tbZipCode.Text = "84111";
            }
            else if (interval == 2)
            {
                tbStreedAddress1.Text = "108 E Fairground Dr";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Tuscon";
                cbState.Text = "AZ";
                tbZipCode.Text = "85714";
            }
            else if (interval == 3)
            {
                tbStreedAddress1.Text = "114 Whitney Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "New Haven";
                cbState.Text = "CT";
                tbZipCode.Text = "06511";
            }
            else if (interval == 4)
            {
                tbStreedAddress1.Text = "7915 Karlov Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Skokie";
                cbState.Text = "IL";
                tbZipCode.Text = "60076";
            }
            else if (interval == 5)
            {
                tbStreedAddress1.Text = "2546 Southern Ave";
                tbStreedAddress2.Text = "";
                cbCity.Text = "Jacksonville ";
                cbState.Text = "FL";
                tbZipCode.Text = "32207";
            }

            selectPrefilledAddressesCount++;

        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            QueryResultRegrid = null;
            JsonRegrid = null;

            QueryResultZoneomics = null;
            JsonZoneomics = null;

            this.Close();
        }




        private void btnMultipleAddresses_Click(object sender, EventArgs e)
        {
            string multipleAddressStr = "815 LINN DR, CLEVELAND, OH 44108, USA;8611 SUPERIOR AVE, CLEVELAND, OH 44106, USA;8701 BIRCHDALE AVE, CLEVELAND, OH 44106, USA;9511 LAMONT AVE, CLEVELAND, OH 44106, USA;9620 HOUGH AVE, CLEVELAND, OH 44106, USA;9700 SHAKER BLVD, CLEVELAND, OH 44104, USA;9701 LAMONT AVE, CLEVELAND, OH 44106, USA";
            
            OpenFileDialog openFileDialog = null;
            openFileDialog = new OpenFileDialog() { Filter = "Text file|*.txt", Title = "Select Address Text file" };
            
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var filePath = openFileDialog.FileName;
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@filePath))
            {
                multipleAddressStr = line;
                System.Console.WriteLine(line);
            }


            var multipleAddresses = multipleAddressStr.Split(';').ToList();
            int count = 0;

            foreach (var address in multipleAddresses)
            {
                string fullAddress = address;

                if (cbRegrid.Checked)
                {
                    var clietRegrid = new RestClient("https://app.regrid.com/api/v1/search.json?");
                    var requestRegrid = new RestRequest(Method.GET);

                    requestRegrid.AddParameter("query", fullAddress);
                    requestRegrid.AddParameter("strict", "1");
                    requestRegrid.AddParameter("limit", "1");
                    requestRegrid.AddParameter("token", "ay49YmoCTj_sV_p4MRpqnF9wwPKPRxpzSSK-EbGaMwpKipxZYy43oQoseSFMXECy");

                    requestRegrid.AddHeader("content-type", "application/json");
                    QueryResultRegrid = clietRegrid.Execute<Object>(requestRegrid).Data;
                    JsonRegrid = JsonConvert.SerializeObject(QueryResultRegrid, Formatting.Indented);

                    string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Regrid";
                    if (Directory.Exists(directory))
                    {
                        string path = $"{directory}\\JSON_Regrid_{fullAddress}.json";
                        System.IO.File.WriteAllText(@path, JsonRegrid);
                    }

                }

                if (cbZoneomics.Checked)
                {
                    var clientZoneomics = new RestClient("https://www.zoneomics.com/api/get_zone_details");
                    var requestZoneomics = new RestRequest(Method.GET);

                    requestZoneomics.AddParameter("api_key", "a9454176416681b8051d7ee5479d3e85c2afd650");
                    requestZoneomics.AddParameter("address", fullAddress);
                    requestZoneomics.AddParameter("output_fields", "all");

                    requestZoneomics.AddHeader("content-type", "application/json");
                    QueryResultZoneomics = clientZoneomics.Execute<Object>(requestZoneomics).Data;
                    JsonZoneomics = JsonConvert.SerializeObject(QueryResultZoneomics, Formatting.Indented);
                    string directory = $"C:\\Users\\hdosh\\Desktop\\JSON_Data\\Zoneomics";
                    if (Directory.Exists(directory))
                    {
                        string path = $"{directory}\\JSON_Zoneomics_{fullAddress}.json";
                        System.IO.File.WriteAllText(@path, JsonZoneomics);
                    }

                }

                count++;

            }


        }
    }
}
