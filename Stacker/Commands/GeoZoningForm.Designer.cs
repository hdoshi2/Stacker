
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
            this.label4 = new System.Windows.Forms.Label();
            this.tbFullAddress = new System.Windows.Forms.TextBox();
            this.tbZoneonics = new System.Windows.Forms.TextBox();
            this.tbRegrid = new System.Windows.Forms.TextBox();
            this.lblZonromid = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.bAddress.Location = new System.Drawing.Point(12, 53);
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
            "Miami",
            "San Mateo",
            "Salt Lake City",
            "Stamford"});
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
            this.btnClear.Location = new System.Drawing.Point(65, 379);
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
            this.gbAPICallData.Location = new System.Drawing.Point(12, 256);
            this.gbAPICallData.Name = "gbAPICallData";
            this.gbAPICallData.Size = new System.Drawing.Size(310, 115);
            this.gbAPICallData.TabIndex = 21;
            this.gbAPICallData.TabStop = false;
            this.gbAPICallData.Text = "Zoning and GeoJSON Data API";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(22, 420);
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
            this.btnCancel.Location = new System.Drawing.Point(209, 420);
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
            this.btnMultipleAddresses.Location = new System.Drawing.Point(58, 12);
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
            this.gbJSONresults.Controls.Add(this.label5);
            this.gbJSONresults.Controls.Add(this.lblZonromid);
            this.gbJSONresults.Controls.Add(this.tbRegrid);
            this.gbJSONresults.Controls.Add(this.tbZoneonics);
            this.gbJSONresults.Controls.Add(this.tbFullAddress);
            this.gbJSONresults.Controls.Add(this.label4);
            this.gbJSONresults.Location = new System.Drawing.Point(341, 12);
            this.gbJSONresults.Name = "gbJSONresults";
            this.gbJSONresults.Size = new System.Drawing.Size(682, 437);
            this.gbJSONresults.TabIndex = 25;
            this.gbJSONresults.TabStop = false;
            this.gbJSONresults.Text = "JSON File Results";
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
            // tbFullAddress
            // 
            this.tbFullAddress.Location = new System.Drawing.Point(75, 23);
            this.tbFullAddress.Name = "tbFullAddress";
            this.tbFullAddress.ReadOnly = true;
            this.tbFullAddress.Size = new System.Drawing.Size(601, 20);
            this.tbFullAddress.TabIndex = 27;
            // 
            // tbZoneonics
            // 
            this.tbZoneonics.Location = new System.Drawing.Point(6, 70);
            this.tbZoneonics.Multiline = true;
            this.tbZoneonics.Name = "tbZoneonics";
            this.tbZoneonics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbZoneonics.Size = new System.Drawing.Size(326, 350);
            this.tbZoneonics.TabIndex = 28;
            // 
            // tbRegrid
            // 
            this.tbRegrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegrid.Location = new System.Drawing.Point(350, 70);
            this.tbRegrid.Multiline = true;
            this.tbRegrid.Name = "tbRegrid";
            this.tbRegrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRegrid.Size = new System.Drawing.Size(326, 350);
            this.tbRegrid.TabIndex = 29;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(347, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Regrid JSON Result";
            // 
            // GeoZoningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1034, 465);
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
    }
}