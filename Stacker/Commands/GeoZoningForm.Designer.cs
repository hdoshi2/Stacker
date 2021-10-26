
namespace Stacker.Commands
{
    partial class GeoZoningForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bAddress = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCountry = new System.Windows.Forms.TextBox();
            this.btnRandomAddress = new System.Windows.Forms.Button();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStreedAddress2 = new System.Windows.Forms.TextBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbZipCode = new System.Windows.Forms.TextBox();
            this.lblStreetAddress = new System.Windows.Forms.Label();
            this.tbStreedAddress1 = new System.Windows.Forms.TextBox();
            this.btnAPICall = new System.Windows.Forms.Button();
            this.cbZoneomics = new System.Windows.Forms.CheckBox();
            this.cbRegrid = new System.Windows.Forms.CheckBox();
            this.lblZoneomicsStatus = new System.Windows.Forms.Label();
            this.lblRegridStatus = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbAPICallData = new System.Windows.Forms.GroupBox();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMultipleAddresses = new System.Windows.Forms.Button();
            this.gbJSONresults = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tbMaxStories = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbGrndFlrSF = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbTotalLength = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbTotalWidth = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbTotalSF = new System.Windows.Forms.TextBox();
            this.lblData5 = new System.Windows.Forms.Label();
            this.tbData5 = new System.Windows.Forms.TextBox();
            this.lblData4 = new System.Windows.Forms.Label();
            this.tbData4 = new System.Windows.Forms.TextBox();
            this.lblData3 = new System.Windows.Forms.Label();
            this.tbData3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbFloorHeight = new System.Windows.Forms.TextBox();
            this.lblData2 = new System.Windows.Forms.Label();
            this.tbData2 = new System.Windows.Forms.TextBox();
            this.lblData1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbData1 = new System.Windows.Forms.TextBox();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDrawPolygon = new System.Windows.Forms.CheckBox();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.btnAnalyzeGeoJSON = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblZonromid = new System.Windows.Forms.Label();
            this.tbRegrid = new System.Windows.Forms.TextBox();
            this.tbZoneonics = new System.Windows.Forms.TextBox();
            this.tbFullAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bAddress.SuspendLayout();
            this.gbAPICallData.SuspendLayout();
            this.gbJSONresults.SuspendLayout();
            this.SuspendLayout();
            // 
            // bAddress
            // 
            this.bAddress.Controls.Add(this.label9);
            this.bAddress.Controls.Add(this.tbCountry);
            this.bAddress.Controls.Add(this.btnRandomAddress);
            this.bAddress.Controls.Add(this.cbState);
            this.bAddress.Controls.Add(this.label3);
            this.bAddress.Controls.Add(this.label2);
            this.bAddress.Controls.Add(this.tbStreedAddress2);
            this.bAddress.Controls.Add(this.cbCity);
            this.bAddress.Controls.Add(this.lblCity);
            this.bAddress.Controls.Add(this.label1);
            this.bAddress.Controls.Add(this.tbZipCode);
            this.bAddress.Controls.Add(this.lblStreetAddress);
            this.bAddress.Controls.Add(this.tbStreedAddress1);
            this.bAddress.Location = new System.Drawing.Point(12, 74);
            this.bAddress.Name = "bAddress";
            this.bAddress.Size = new System.Drawing.Size(310, 195);
            this.bAddress.TabIndex = 2;
            this.bAddress.TabStop = false;
            this.bAddress.Text = "Address Data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Country:";
            // 
            // tbCountry
            // 
            this.tbCountry.Location = new System.Drawing.Point(93, 158);
            this.tbCountry.Name = "tbCountry";
            this.tbCountry.ReadOnly = true;
            this.tbCountry.Size = new System.Drawing.Size(48, 20);
            this.tbCountry.TabIndex = 25;
            this.tbCountry.Text = "USA";
            // 
            // btnRandomAddress
            // 
            this.btnRandomAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRandomAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandomAddress.Image = global::Stacker.Properties.Resources.floorStack_32;
            this.btnRandomAddress.Location = new System.Drawing.Point(268, 152);
            this.btnRandomAddress.Margin = new System.Windows.Forms.Padding(6);
            this.btnRandomAddress.Name = "btnRandomAddress";
            this.btnRandomAddress.Size = new System.Drawing.Size(33, 30);
            this.btnRandomAddress.TabIndex = 24;
            this.btnRandomAddress.UseVisualStyleBackColor = true;
            this.btnRandomAddress.Click += new System.EventHandler(this.btnRandomAddress_Click);
            // 
            // cbState
            // 
            this.cbState.FormattingEnabled = true;
            this.cbState.Items.AddRange(new object[] {
            "AL",
            "AK",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DC",
            "DE",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"});
            this.cbState.Location = new System.Drawing.Point(93, 105);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(126, 21);
            this.cbState.TabIndex = 14;
            this.cbState.Text = "Select";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "State:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Address Line 2:";
            // 
            // tbStreedAddress2
            // 
            this.tbStreedAddress2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStreedAddress2.Location = new System.Drawing.Point(93, 52);
            this.tbStreedAddress2.Name = "tbStreedAddress2";
            this.tbStreedAddress2.Size = new System.Drawing.Size(211, 20);
            this.tbStreedAddress2.TabIndex = 11;
            // 
            // cbCity
            // 
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Items.AddRange(new object[] {
            "Atlanta",
            "Jacksonville",
            "Miami",
            "San Antonio",
            "San Mateo",
            "Salt Lake City",
            "Stamford",
            "Washington"});
            this.cbCity.Location = new System.Drawing.Point(93, 78);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(126, 21);
            this.cbCity.TabIndex = 10;
            this.cbCity.Text = "Select";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(60, 81);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(27, 13);
            this.lblCity.TabIndex = 9;
            this.lblCity.Text = "City:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Zip:";
            // 
            // tbZipCode
            // 
            this.tbZipCode.Location = new System.Drawing.Point(93, 132);
            this.tbZipCode.Name = "tbZipCode";
            this.tbZipCode.Size = new System.Drawing.Size(76, 20);
            this.tbZipCode.TabIndex = 3;
            // 
            // lblStreetAddress
            // 
            this.lblStreetAddress.AutoSize = true;
            this.lblStreetAddress.Location = new System.Drawing.Point(7, 29);
            this.lblStreetAddress.Name = "lblStreetAddress";
            this.lblStreetAddress.Size = new System.Drawing.Size(80, 13);
            this.lblStreetAddress.TabIndex = 2;
            this.lblStreetAddress.Text = "Address Line 1:";
            // 
            // tbStreedAddress1
            // 
            this.tbStreedAddress1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStreedAddress1.Location = new System.Drawing.Point(93, 26);
            this.tbStreedAddress1.Name = "tbStreedAddress1";
            this.tbStreedAddress1.Size = new System.Drawing.Size(211, 20);
            this.tbStreedAddress1.TabIndex = 1;
            // 
            // btnAPICall
            // 
            this.btnAPICall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAPICall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAPICall.Location = new System.Drawing.Point(10, 75);
            this.btnAPICall.Margin = new System.Windows.Forms.Padding(6);
            this.btnAPICall.Name = "btnAPICall";
            this.btnAPICall.Size = new System.Drawing.Size(290, 30);
            this.btnAPICall.TabIndex = 15;
            this.btnAPICall.Text = "Request Zoning Data";
            this.btnAPICall.UseVisualStyleBackColor = true;
            this.btnAPICall.Click += new System.EventHandler(this.btnAPICall_Click);
            // 
            // cbZoneomics
            // 
            this.cbZoneomics.AutoSize = true;
            this.cbZoneomics.Checked = true;
            this.cbZoneomics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbZoneomics.Location = new System.Drawing.Point(13, 26);
            this.cbZoneomics.Name = "cbZoneomics";
            this.cbZoneomics.Size = new System.Drawing.Size(206, 17);
            this.cbZoneomics.TabIndex = 17;
            this.cbZoneomics.Text = "Request Zoning Data from Zoneomics";
            this.cbZoneomics.UseVisualStyleBackColor = true;
            // 
            // cbRegrid
            // 
            this.cbRegrid.AutoSize = true;
            this.cbRegrid.Checked = true;
            this.cbRegrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRegrid.Location = new System.Drawing.Point(13, 49);
            this.cbRegrid.Name = "cbRegrid";
            this.cbRegrid.Size = new System.Drawing.Size(200, 17);
            this.cbRegrid.TabIndex = 16;
            this.cbRegrid.Text = "Request GeoJSON Data from Regrid";
            this.cbRegrid.UseVisualStyleBackColor = true;
            // 
            // lblZoneomicsStatus
            // 
            this.lblZoneomicsStatus.AutoSize = true;
            this.lblZoneomicsStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblZoneomicsStatus.Location = new System.Drawing.Point(226, 29);
            this.lblZoneomicsStatus.Name = "lblZoneomicsStatus";
            this.lblZoneomicsStatus.Size = new System.Drawing.Size(59, 13);
            this.lblZoneomicsStatus.TabIndex = 18;
            this.lblZoneomicsStatus.Text = "Successful";
            this.lblZoneomicsStatus.Visible = false;
            // 
            // lblRegridStatus
            // 
            this.lblRegridStatus.AutoSize = true;
            this.lblRegridStatus.ForeColor = System.Drawing.Color.Red;
            this.lblRegridStatus.Location = new System.Drawing.Point(226, 50);
            this.lblRegridStatus.Name = "lblRegridStatus";
            this.lblRegridStatus.Size = new System.Drawing.Size(35, 13);
            this.lblRegridStatus.TabIndex = 19;
            this.lblRegridStatus.Text = "Failed";
            this.lblRegridStatus.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(65, 435);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(222, 30);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear Form and Delete Saved API Data";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // gbAPICallData
            // 
            this.gbAPICallData.Controls.Add(this.cbZoneomics);
            this.gbAPICallData.Controls.Add(this.lblRegridStatus);
            this.gbAPICallData.Controls.Add(this.cbRegrid);
            this.gbAPICallData.Controls.Add(this.btnAPICall);
            this.gbAPICallData.Controls.Add(this.lblZoneomicsStatus);
            this.gbAPICallData.Location = new System.Drawing.Point(12, 295);
            this.gbAPICallData.Name = "gbAPICallData";
            this.gbAPICallData.Size = new System.Drawing.Size(310, 115);
            this.gbAPICallData.TabIndex = 21;
            this.gbAPICallData.TabStop = false;
            this.gbAPICallData.Text = "Zoning and GeoJSON Data API";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(22, 487);
            this.btnLoadData.Margin = new System.Windows.Forms.Padding(6);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(175, 30);
            this.btnLoadData.TabIndex = 22;
            this.btnLoadData.Text = "Load API Data and Close";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(209, 487);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 30);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMultipleAddresses
            // 
            this.btnMultipleAddresses.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMultipleAddresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMultipleAddresses.Location = new System.Drawing.Point(58, 23);
            this.btnMultipleAddresses.Margin = new System.Windows.Forms.Padding(6);
            this.btnMultipleAddresses.Name = "btnMultipleAddresses";
            this.btnMultipleAddresses.Size = new System.Drawing.Size(222, 30);
            this.btnMultipleAddresses.TabIndex = 24;
            this.btnMultipleAddresses.Text = "Load Multiple Address As Text File";
            this.btnMultipleAddresses.UseVisualStyleBackColor = true;
            this.btnMultipleAddresses.Click += new System.EventHandler(this.btnMultipleAddresses_Click);
            // 
            // gbJSONresults
            // 
            this.gbJSONresults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbJSONresults.Controls.Add(this.btnRefresh);
            this.gbJSONresults.Controls.Add(this.label19);
            this.gbJSONresults.Controls.Add(this.tbMaxStories);
            this.gbJSONresults.Controls.Add(this.label20);
            this.gbJSONresults.Controls.Add(this.tbGrndFlrSF);
            this.gbJSONresults.Controls.Add(this.label16);
            this.gbJSONresults.Controls.Add(this.tbTotalLength);
            this.gbJSONresults.Controls.Add(this.label17);
            this.gbJSONresults.Controls.Add(this.tbTotalWidth);
            this.gbJSONresults.Controls.Add(this.label18);
            this.gbJSONresults.Controls.Add(this.tbTotalSF);
            this.gbJSONresults.Controls.Add(this.lblData5);
            this.gbJSONresults.Controls.Add(this.tbData5);
            this.gbJSONresults.Controls.Add(this.lblData4);
            this.gbJSONresults.Controls.Add(this.tbData4);
            this.gbJSONresults.Controls.Add(this.lblData3);
            this.gbJSONresults.Controls.Add(this.tbData3);
            this.gbJSONresults.Controls.Add(this.label12);
            this.gbJSONresults.Controls.Add(this.tbFloorHeight);
            this.gbJSONresults.Controls.Add(this.lblData2);
            this.gbJSONresults.Controls.Add(this.tbData2);
            this.gbJSONresults.Controls.Add(this.lblData1);
            this.gbJSONresults.Controls.Add(this.label8);
            this.gbJSONresults.Controls.Add(this.tbData1);
            this.gbJSONresults.Controls.Add(this.tbLength);
            this.gbJSONresults.Controls.Add(this.label7);
            this.gbJSONresults.Controls.Add(this.tbWidth);
            this.gbJSONresults.Controls.Add(this.label6);
            this.gbJSONresults.Controls.Add(this.cbDrawPolygon);
            this.gbJSONresults.Controls.Add(this.tbArea);
            this.gbJSONresults.Controls.Add(this.btnAnalyzeGeoJSON);
            this.gbJSONresults.Controls.Add(this.label5);
            this.gbJSONresults.Controls.Add(this.lblZonromid);
            this.gbJSONresults.Controls.Add(this.tbRegrid);
            this.gbJSONresults.Controls.Add(this.tbZoneonics);
            this.gbJSONresults.Controls.Add(this.tbFullAddress);
            this.gbJSONresults.Controls.Add(this.label4);
            this.gbJSONresults.Location = new System.Drawing.Point(341, 12);
            this.gbJSONresults.Name = "gbJSONresults";
            this.gbJSONresults.Size = new System.Drawing.Size(682, 519);
            this.gbJSONresults.TabIndex = 25;
            this.gbJSONresults.TabStop = false;
            this.gbJSONresults.Text = "JSON File Results";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(645, 367);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.TabIndex = 58;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(549, 465);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 13);
            this.label19.TabIndex = 57;
            this.label19.Text = "Max Stories:";
            // 
            // tbMaxStories
            // 
            this.tbMaxStories.Location = new System.Drawing.Point(616, 462);
            this.tbMaxStories.Name = "tbMaxStories";
            this.tbMaxStories.ReadOnly = true;
            this.tbMaxStories.Size = new System.Drawing.Size(60, 20);
            this.tbMaxStories.TabIndex = 56;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(550, 439);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(63, 13);
            this.label20.TabIndex = 55;
            this.label20.Text = "Grnd Flr SF:";
            // 
            // tbGrndFlrSF
            // 
            this.tbGrndFlrSF.Location = new System.Drawing.Point(616, 436);
            this.tbGrndFlrSF.Name = "tbGrndFlrSF";
            this.tbGrndFlrSF.ReadOnly = true;
            this.tbGrndFlrSF.Size = new System.Drawing.Size(60, 20);
            this.tbGrndFlrSF.TabIndex = 54;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(419, 491);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 53;
            this.label16.Text = "Bldg Length:";
            // 
            // tbTotalLength
            // 
            this.tbTotalLength.Location = new System.Drawing.Point(487, 488);
            this.tbTotalLength.Name = "tbTotalLength";
            this.tbTotalLength.ReadOnly = true;
            this.tbTotalLength.Size = new System.Drawing.Size(61, 20);
            this.tbTotalLength.TabIndex = 52;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(423, 465);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 13);
            this.label17.TabIndex = 51;
            this.label17.Text = "Bldg Width:";
            // 
            // tbTotalWidth
            // 
            this.tbTotalWidth.Location = new System.Drawing.Point(487, 462);
            this.tbTotalWidth.Name = "tbTotalWidth";
            this.tbTotalWidth.ReadOnly = true;
            this.tbTotalWidth.Size = new System.Drawing.Size(61, 20);
            this.tbTotalWidth.TabIndex = 50;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(410, 439);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 13);
            this.label18.TabIndex = 49;
            this.label18.Text = "Bldg SF Total:";
            // 
            // tbTotalSF
            // 
            this.tbTotalSF.Location = new System.Drawing.Point(487, 436);
            this.tbTotalSF.Name = "tbTotalSF";
            this.tbTotalSF.ReadOnly = true;
            this.tbTotalSF.Size = new System.Drawing.Size(61, 20);
            this.tbTotalSF.TabIndex = 48;
            // 
            // lblData5
            // 
            this.lblData5.AutoSize = true;
            this.lblData5.Location = new System.Drawing.Point(61, 487);
            this.lblData5.Name = "lblData5";
            this.lblData5.Size = new System.Drawing.Size(88, 13);
            this.lblData5.TabIndex = 47;
            this.lblData5.Text = "JSON Data Point";
            // 
            // tbData5
            // 
            this.tbData5.Location = new System.Drawing.Point(11, 484);
            this.tbData5.Name = "tbData5";
            this.tbData5.Size = new System.Drawing.Size(41, 20);
            this.tbData5.TabIndex = 46;
            // 
            // lblData4
            // 
            this.lblData4.AutoSize = true;
            this.lblData4.Location = new System.Drawing.Point(61, 460);
            this.lblData4.Name = "lblData4";
            this.lblData4.Size = new System.Drawing.Size(88, 13);
            this.lblData4.TabIndex = 45;
            this.lblData4.Text = "JSON Data Point";
            // 
            // tbData4
            // 
            this.tbData4.Location = new System.Drawing.Point(11, 456);
            this.tbData4.Name = "tbData4";
            this.tbData4.Size = new System.Drawing.Size(41, 20);
            this.tbData4.TabIndex = 44;
            // 
            // lblData3
            // 
            this.lblData3.AutoSize = true;
            this.lblData3.Location = new System.Drawing.Point(61, 433);
            this.lblData3.Name = "lblData3";
            this.lblData3.Size = new System.Drawing.Size(88, 13);
            this.lblData3.TabIndex = 43;
            this.lblData3.Text = "JSON Data Point";
            // 
            // tbData3
            // 
            this.tbData3.Location = new System.Drawing.Point(11, 429);
            this.tbData3.Name = "tbData3";
            this.tbData3.Size = new System.Drawing.Size(41, 20);
            this.tbData3.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(271, 412);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 41;
            this.label12.Text = "Floor Height:";
            // 
            // tbFloorHeight
            // 
            this.tbFloorHeight.Location = new System.Drawing.Point(342, 408);
            this.tbFloorHeight.Name = "tbFloorHeight";
            this.tbFloorHeight.Size = new System.Drawing.Size(42, 20);
            this.tbFloorHeight.TabIndex = 40;
            this.tbFloorHeight.Text = "12";
            // 
            // lblData2
            // 
            this.lblData2.AutoSize = true;
            this.lblData2.Location = new System.Drawing.Point(61, 406);
            this.lblData2.Name = "lblData2";
            this.lblData2.Size = new System.Drawing.Size(88, 13);
            this.lblData2.TabIndex = 39;
            this.lblData2.Text = "JSON Data Point";
            // 
            // tbData2
            // 
            this.tbData2.Location = new System.Drawing.Point(11, 402);
            this.tbData2.Name = "tbData2";
            this.tbData2.Size = new System.Drawing.Size(42, 20);
            this.tbData2.TabIndex = 38;
            // 
            // lblData1
            // 
            this.lblData1.AutoSize = true;
            this.lblData1.Location = new System.Drawing.Point(61, 379);
            this.lblData1.Name = "lblData1";
            this.lblData1.Size = new System.Drawing.Size(88, 13);
            this.lblData1.TabIndex = 28;
            this.lblData1.Text = "JSON Data Point";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 491);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Length:";
            // 
            // tbData1
            // 
            this.tbData1.Location = new System.Drawing.Point(11, 375);
            this.tbData1.Name = "tbData1";
            this.tbData1.Size = new System.Drawing.Size(42, 20);
            this.tbData1.TabIndex = 27;
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(342, 488);
            this.tbLength.Name = "tbLength";
            this.tbLength.ReadOnly = true;
            this.tbLength.Size = new System.Drawing.Size(66, 20);
            this.tbLength.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 465);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Width:";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(342, 462);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.ReadOnly = true;
            this.tbWidth.Size = new System.Drawing.Size(66, 20);
            this.tbWidth.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(304, 439);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Area:";
            // 
            // cbDrawPolygon
            // 
            this.cbDrawPolygon.AutoSize = true;
            this.cbDrawPolygon.Location = new System.Drawing.Point(499, 375);
            this.cbDrawPolygon.Name = "cbDrawPolygon";
            this.cbDrawPolygon.Size = new System.Drawing.Size(143, 17);
            this.cbDrawPolygon.TabIndex = 33;
            this.cbDrawPolygon.Text = "Draw GeoJSON Polygon";
            this.cbDrawPolygon.UseVisualStyleBackColor = true;
            // 
            // tbArea
            // 
            this.tbArea.Location = new System.Drawing.Point(342, 436);
            this.tbArea.Name = "tbArea";
            this.tbArea.ReadOnly = true;
            this.tbArea.Size = new System.Drawing.Size(66, 20);
            this.tbArea.TabIndex = 27;
            // 
            // btnAnalyzeGeoJSON
            // 
            this.btnAnalyzeGeoJSON.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalyzeGeoJSON.Location = new System.Drawing.Point(350, 367);
            this.btnAnalyzeGeoJSON.Margin = new System.Windows.Forms.Padding(6);
            this.btnAnalyzeGeoJSON.Name = "btnAnalyzeGeoJSON";
            this.btnAnalyzeGeoJSON.Size = new System.Drawing.Size(144, 30);
            this.btnAnalyzeGeoJSON.TabIndex = 20;
            this.btnAnalyzeGeoJSON.Text = "Analyze GeoJSON";
            this.btnAnalyzeGeoJSON.UseVisualStyleBackColor = true;
            this.btnAnalyzeGeoJSON.Click += new System.EventHandler(this.btnAnalyzeGeoJSON_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(347, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Regrid JSON Result";
            // 
            // lblZonromid
            // 
            this.lblZonromid.AutoSize = true;
            this.lblZonromid.Location = new System.Drawing.Point(8, 52);
            this.lblZonromid.Name = "lblZonromid";
            this.lblZonromid.Size = new System.Drawing.Size(123, 13);
            this.lblZonromid.TabIndex = 31;
            this.lblZonromid.Text = "Zoneomics JSON Result";
            // 
            // tbRegrid
            // 
            this.tbRegrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegrid.Location = new System.Drawing.Point(350, 70);
            this.tbRegrid.Multiline = true;
            this.tbRegrid.Name = "tbRegrid";
            this.tbRegrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRegrid.Size = new System.Drawing.Size(326, 289);
            this.tbRegrid.TabIndex = 29;
            // 
            // tbZoneonics
            // 
            this.tbZoneonics.Location = new System.Drawing.Point(6, 70);
            this.tbZoneonics.Multiline = true;
            this.tbZoneonics.Name = "tbZoneonics";
            this.tbZoneonics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbZoneonics.Size = new System.Drawing.Size(326, 289);
            this.tbZoneonics.TabIndex = 28;
            // 
            // tbFullAddress
            // 
            this.tbFullAddress.Location = new System.Drawing.Point(75, 23);
            this.tbFullAddress.Name = "tbFullAddress";
            this.tbFullAddress.ReadOnly = true;
            this.tbFullAddress.Size = new System.Drawing.Size(601, 20);
            this.tbFullAddress.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Full Address:";
            // 
            // GeoZoningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1034, 543);
            this.Controls.Add(this.gbJSONresults);
            this.Controls.Add(this.btnMultipleAddresses);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.gbAPICallData);
            this.Controls.Add(this.bAddress);
            this.MaximizeBox = false;
            this.Name = "GeoZoningForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GeoJSON and Zoning Data ";
            this.bAddress.ResumeLayout(false);
            this.bAddress.PerformLayout();
            this.gbAPICallData.ResumeLayout(false);
            this.gbAPICallData.PerformLayout();
            this.gbJSONresults.ResumeLayout(false);
            this.gbJSONresults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox bAddress;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbZipCode;
        private System.Windows.Forms.Label lblStreetAddress;
        private System.Windows.Forms.TextBox tbStreedAddress1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStreedAddress2;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAPICall;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblRegridStatus;
        private System.Windows.Forms.Label lblZoneomicsStatus;
        private System.Windows.Forms.CheckBox cbZoneomics;
        private System.Windows.Forms.CheckBox cbRegrid;
        private System.Windows.Forms.GroupBox gbAPICallData;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRandomAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCountry;
        private System.Windows.Forms.Button btnMultipleAddresses;
        private System.Windows.Forms.GroupBox gbJSONresults;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFullAddress;
        private System.Windows.Forms.TextBox tbZoneonics;
        private System.Windows.Forms.TextBox tbRegrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblZonromid;
        private System.Windows.Forms.Button btnAnalyzeGeoJSON;
        private System.Windows.Forms.Label lblData5;
        private System.Windows.Forms.TextBox tbData5;
        private System.Windows.Forms.Label lblData4;
        private System.Windows.Forms.TextBox tbData4;
        private System.Windows.Forms.Label lblData3;
        private System.Windows.Forms.TextBox tbData3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbFloorHeight;
        private System.Windows.Forms.Label lblData2;
        private System.Windows.Forms.TextBox tbData2;
        private System.Windows.Forms.Label lblData1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbData1;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbDrawPolygon;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbMaxStories;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbGrndFlrSF;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbTotalLength;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbTotalWidth;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbTotalSF;
        private System.Windows.Forms.Button btnRefresh;
    }
}