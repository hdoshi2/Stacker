using Autodesk.Revit.UI;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string city = cbCity.SelectedItem.ToString();
            string state = cbState.SelectedItem.ToString();
            string zip = tbZipCode.Text;

            string fullAddress = $"{addressLine1}, {city}, {state} {zip}, USA";
            if(addressLine2 != "")
                fullAddress = $"{addressLine1}, {addressLine2},{city}, {state} {zip}, USA";

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
                JsonRegrid = JsonConvert.SerializeObject(QueryResultRegrid);

                if (JsonRegrid != null && JsonRegrid.Length > 0)
                {
                    lblRegridStatus.Visible = true;
                    lblRegridStatus.ForeColor = System.Drawing.Color.Lime;
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
                JsonZoneomics = JsonConvert.SerializeObject(QueryResultZoneomics);


                if (JsonZoneomics != null && JsonZoneomics.Length > 0)
                {
                    lblZoneomicsStatus.Visible = true;
                    lblZoneomicsStatus.ForeColor = System.Drawing.Color.Lime;
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


        private void btnRandomAddress_Click(object sender, EventArgs e)
        {
            tbStreedAddress1.Text = "17 Hanover St";
            tbStreedAddress2.Text = "";
            cbCity.Text = "Stamford";
            cbState.Text = "CT";
            tbZipCode.Text = "06902";
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
    }
}
