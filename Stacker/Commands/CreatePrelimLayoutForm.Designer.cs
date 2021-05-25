﻿
namespace Stacker.Commands
{
    partial class CreatePrelimLayoutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePrelimLayoutForm));
            this.btnBuildLayout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFloorDimension = new System.Windows.Forms.GroupBox();
            this.cbHallwayAlignment = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbHallwayWidth = new System.Windows.Forms.TextBox();
            this.tbTotalSquareFootage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnApplyFlrDim = new System.Windows.Forms.Button();
            this.gbUnitConfig = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cb2BedPriority = new System.Windows.Forms.ComboBox();
            this.cb1BedPriority = new System.Windows.Forms.ComboBox();
            this.cbStudioPriority = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadDataFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteGeom = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb2BedCount = new System.Windows.Forms.TextBox();
            this.tb1BedCount = new System.Windows.Forms.TextBox();
            this.tbStudioCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbModLimitations = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbFixedWidth = new System.Windows.Forms.TextBox();
            this.tbModWidthMax = new System.Windows.Forms.TextBox();
            this.tbModWidthMin = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbModLengthMax = new System.Windows.Forms.TextBox();
            this.tbModLengthMin = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbFloorDimension.SuspendLayout();
            this.gbUnitConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbModLimitations.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuildLayout
            // 
            this.btnBuildLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuildLayout.Location = new System.Drawing.Point(79, 535);
            this.btnBuildLayout.Margin = new System.Windows.Forms.Padding(6);
            this.btnBuildLayout.Name = "btnBuildLayout";
            this.btnBuildLayout.Size = new System.Drawing.Size(134, 30);
            this.btnBuildLayout.TabIndex = 0;
            this.btnBuildLayout.Text = "Build Floor Layout";
            this.btnBuildLayout.UseVisualStyleBackColor = true;
            this.btnBuildLayout.Click += new System.EventHandler(this.btnBuildLayout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.ErrorImage = global::Stacker.Properties.Resources.floorStack_128;
            this.pictureBox1.Image = global::Stacker.Properties.Resources.floorStack_32;
            this.pictureBox1.InitialImage = global::Stacker.Properties.Resources.floorStack_128;
            this.pictureBox1.Location = new System.Drawing.Point(348, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(71, 26);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(39, 20);
            this.tbLength.TabIndex = 1;
            this.tbLength.Text = "120";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(7, 29);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(58, 13);
            this.lblLength.TabIndex = 2;
            this.lblLength.Text = "Length (ft):";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(181, 26);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(39, 20);
            this.tbWidth.TabIndex = 3;
            this.tbWidth.Text = "60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Width (ft):";
            // 
            // gbFloorDimension
            // 
            this.gbFloorDimension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFloorDimension.Controls.Add(this.cbHallwayAlignment);
            this.gbFloorDimension.Controls.Add(this.label10);
            this.gbFloorDimension.Controls.Add(this.label9);
            this.gbFloorDimension.Controls.Add(this.tbHallwayWidth);
            this.gbFloorDimension.Controls.Add(this.tbTotalSquareFootage);
            this.gbFloorDimension.Controls.Add(this.label5);
            this.gbFloorDimension.Controls.Add(this.btnApplyFlrDim);
            this.gbFloorDimension.Controls.Add(this.label1);
            this.gbFloorDimension.Controls.Add(this.tbWidth);
            this.gbFloorDimension.Controls.Add(this.lblLength);
            this.gbFloorDimension.Controls.Add(this.tbLength);
            this.gbFloorDimension.Controls.Add(this.pictureBox1);
            this.gbFloorDimension.Location = new System.Drawing.Point(12, 54);
            this.gbFloorDimension.Name = "gbFloorDimension";
            this.gbFloorDimension.Size = new System.Drawing.Size(393, 123);
            this.gbFloorDimension.TabIndex = 1;
            this.gbFloorDimension.TabStop = false;
            this.gbFloorDimension.Text = "Floor Geometry";
            // 
            // cbHallwayAlignment
            // 
            this.cbHallwayAlignment.FormattingEnabled = true;
            this.cbHallwayAlignment.Items.AddRange(new object[] {
            "Length",
            "Width"});
            this.cbHallwayAlignment.Location = new System.Drawing.Point(224, 52);
            this.cbHallwayAlignment.Name = "cbHallwayAlignment";
            this.cbHallwayAlignment.Size = new System.Drawing.Size(61, 21);
            this.cbHallwayAlignment.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(123, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Hallway Alignment:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Hallway (ft):";
            // 
            // tbHallwayWidth
            // 
            this.tbHallwayWidth.Location = new System.Drawing.Point(71, 52);
            this.tbHallwayWidth.Name = "tbHallwayWidth";
            this.tbHallwayWidth.ReadOnly = true;
            this.tbHallwayWidth.Size = new System.Drawing.Size(39, 20);
            this.tbHallwayWidth.TabIndex = 7;
            this.tbHallwayWidth.Text = "5";
            // 
            // tbTotalSquareFootage
            // 
            this.tbTotalSquareFootage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTotalSquareFootage.Location = new System.Drawing.Point(341, 93);
            this.tbTotalSquareFootage.Name = "tbTotalSquareFootage";
            this.tbTotalSquareFootage.ReadOnly = true;
            this.tbTotalSquareFootage.Size = new System.Drawing.Size(39, 20);
            this.tbTotalSquareFootage.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total Square Footage (sf):";
            // 
            // btnApplyFlrDim
            // 
            this.btnApplyFlrDim.Location = new System.Drawing.Point(10, 83);
            this.btnApplyFlrDim.Margin = new System.Windows.Forms.Padding(6);
            this.btnApplyFlrDim.Name = "btnApplyFlrDim";
            this.btnApplyFlrDim.Padding = new System.Windows.Forms.Padding(3);
            this.btnApplyFlrDim.Size = new System.Drawing.Size(55, 30);
            this.btnApplyFlrDim.TabIndex = 4;
            this.btnApplyFlrDim.Text = "Apply";
            this.btnApplyFlrDim.UseVisualStyleBackColor = true;
            this.btnApplyFlrDim.Click += new System.EventHandler(this.btnApplyFlrDim_Click);
            // 
            // gbUnitConfig
            // 
            this.gbUnitConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUnitConfig.Controls.Add(this.textBox7);
            this.gbUnitConfig.Controls.Add(this.textBox6);
            this.gbUnitConfig.Controls.Add(this.label21);
            this.gbUnitConfig.Controls.Add(this.textBox1);
            this.gbUnitConfig.Controls.Add(this.label15);
            this.gbUnitConfig.Controls.Add(this.label14);
            this.gbUnitConfig.Controls.Add(this.cb2BedPriority);
            this.gbUnitConfig.Controls.Add(this.cb1BedPriority);
            this.gbUnitConfig.Controls.Add(this.cbStudioPriority);
            this.gbUnitConfig.Controls.Add(this.label4);
            this.gbUnitConfig.Controls.Add(this.label3);
            this.gbUnitConfig.Controls.Add(this.label2);
            this.gbUnitConfig.Location = new System.Drawing.Point(12, 183);
            this.gbUnitConfig.Name = "gbUnitConfig";
            this.gbUnitConfig.Size = new System.Drawing.Size(201, 178);
            this.gbUnitConfig.TabIndex = 2;
            this.gbUnitConfig.TabStop = false;
            this.gbUnitConfig.Text = "Unit Configuration Priority";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 153);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "0: Ignore Unity Type";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "1: Top Priority";
            // 
            // cb2BedPriority
            // 
            this.cb2BedPriority.FormattingEnabled = true;
            this.cb2BedPriority.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cb2BedPriority.Location = new System.Drawing.Point(58, 93);
            this.cb2BedPriority.Name = "cb2BedPriority";
            this.cb2BedPriority.Size = new System.Drawing.Size(35, 21);
            this.cb2BedPriority.TabIndex = 8;
            // 
            // cb1BedPriority
            // 
            this.cb1BedPriority.FormattingEnabled = true;
            this.cb1BedPriority.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cb1BedPriority.Location = new System.Drawing.Point(58, 66);
            this.cb1BedPriority.Name = "cb1BedPriority";
            this.cb1BedPriority.Size = new System.Drawing.Size(35, 21);
            this.cb1BedPriority.TabIndex = 7;
            // 
            // cbStudioPriority
            // 
            this.cbStudioPriority.FormattingEnabled = true;
            this.cbStudioPriority.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbStudioPriority.Location = new System.Drawing.Point(58, 39);
            this.cbStudioPriority.Name = "cbStudioPriority";
            this.cbStudioPriority.Size = new System.Drawing.Size(35, 21);
            this.cbStudioPriority.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "2-Bed";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "1-Bed:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Studio:";
            // 
            // btnLoadDataFile
            // 
            this.btnLoadDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadDataFile.Location = new System.Drawing.Point(142, 17);
            this.btnLoadDataFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnLoadDataFile.Name = "btnLoadDataFile";
            this.btnLoadDataFile.Padding = new System.Windows.Forms.Padding(3);
            this.btnLoadDataFile.Size = new System.Drawing.Size(134, 30);
            this.btnLoadDataFile.TabIndex = 3;
            this.btnLoadDataFile.Text = "Load Data File";
            this.btnLoadDataFile.UseVisualStyleBackColor = true;
            this.btnLoadDataFile.Click += new System.EventHandler(this.btnLoadDataFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnDeleteGeom);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tb2BedCount);
            this.groupBox1.Controls.Add(this.tb1BedCount);
            this.groupBox1.Controls.Add(this.tbStudioCount);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(12, 367);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 154);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unit Configuration Preview";
            // 
            // btnDeleteGeom
            // 
            this.btnDeleteGeom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteGeom.Location = new System.Drawing.Point(312, 16);
            this.btnDeleteGeom.Name = "btnDeleteGeom";
            this.btnDeleteGeom.Size = new System.Drawing.Size(57, 30);
            this.btnDeleteGeom.TabIndex = 17;
            this.btnDeleteGeom.Text = "Delete";
            this.btnDeleteGeom.UseVisualStyleBackColor = true;
            this.btnDeleteGeom.Click += new System.EventHandler(this.btnDeleteGeom_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(127, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "SF";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(118, 94);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(39, 20);
            this.textBox3.TabIndex = 16;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(118, 65);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(39, 20);
            this.textBox4.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(118, 37);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(39, 20);
            this.textBox5.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "UNIT";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(328, 124);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(39, 20);
            this.textBox2.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(166, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Total Square Footage Used (sf):";
            // 
            // tb2BedCount
            // 
            this.tb2BedCount.Location = new System.Drawing.Point(68, 94);
            this.tb2BedCount.Name = "tb2BedCount";
            this.tb2BedCount.ReadOnly = true;
            this.tb2BedCount.Size = new System.Drawing.Size(39, 20);
            this.tb2BedCount.TabIndex = 10;
            // 
            // tb1BedCount
            // 
            this.tb1BedCount.Location = new System.Drawing.Point(68, 65);
            this.tb1BedCount.Name = "tb1BedCount";
            this.tb1BedCount.ReadOnly = true;
            this.tb1BedCount.Size = new System.Drawing.Size(39, 20);
            this.tb1BedCount.TabIndex = 9;
            // 
            // tbStudioCount
            // 
            this.tbStudioCount.Location = new System.Drawing.Point(68, 37);
            this.tbStudioCount.Name = "tbStudioCount";
            this.tbStudioCount.ReadOnly = true;
            this.tbStudioCount.Size = new System.Drawing.Size(39, 20);
            this.tbStudioCount.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "2-Bed";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "1-Bed:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Studio:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(225, 535);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(2);
            this.btnClose.Size = new System.Drawing.Size(92, 30);
            this.btnClose.TabIndex = 83;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gbModLimitations
            // 
            this.gbModLimitations.Controls.Add(this.label20);
            this.gbModLimitations.Controls.Add(this.tbFixedWidth);
            this.gbModLimitations.Controls.Add(this.tbModWidthMax);
            this.gbModLimitations.Controls.Add(this.tbModWidthMin);
            this.gbModLimitations.Controls.Add(this.label19);
            this.gbModLimitations.Controls.Add(this.label17);
            this.gbModLimitations.Controls.Add(this.label18);
            this.gbModLimitations.Controls.Add(this.label16);
            this.gbModLimitations.Controls.Add(this.tbModLengthMax);
            this.gbModLimitations.Controls.Add(this.tbModLengthMin);
            this.gbModLimitations.Location = new System.Drawing.Point(228, 183);
            this.gbModLimitations.Name = "gbModLimitations";
            this.gbModLimitations.Size = new System.Drawing.Size(177, 177);
            this.gbModLimitations.TabIndex = 84;
            this.gbModLimitations.TabStop = false;
            this.gbModLimitations.Text = "Modular Limitations";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 103);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "Set Width (ft):";
            // 
            // tbFixedWidth
            // 
            this.tbFixedWidth.Location = new System.Drawing.Point(123, 100);
            this.tbFixedWidth.Name = "tbFixedWidth";
            this.tbFixedWidth.Size = new System.Drawing.Size(39, 20);
            this.tbFixedWidth.TabIndex = 11;
            this.tbFixedWidth.Text = "12";
            // 
            // tbModWidthMax
            // 
            this.tbModWidthMax.Location = new System.Drawing.Point(123, 69);
            this.tbModWidthMax.Name = "tbModWidthMax";
            this.tbModWidthMax.ReadOnly = true;
            this.tbModWidthMax.Size = new System.Drawing.Size(39, 20);
            this.tbModWidthMax.TabIndex = 20;
            // 
            // tbModWidthMin
            // 
            this.tbModWidthMin.Location = new System.Drawing.Point(73, 69);
            this.tbModWidthMin.Name = "tbModWidthMin";
            this.tbModWidthMin.ReadOnly = true;
            this.tbModWidthMin.Size = new System.Drawing.Size(39, 20);
            this.tbModWidthMin.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "Width (ft):";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(128, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(30, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "MAX";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(78, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "MIN";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Length (ft):";
            // 
            // tbModLengthMax
            // 
            this.tbModLengthMax.Location = new System.Drawing.Point(123, 39);
            this.tbModLengthMax.Name = "tbModLengthMax";
            this.tbModLengthMax.ReadOnly = true;
            this.tbModLengthMax.Size = new System.Drawing.Size(39, 20);
            this.tbModLengthMax.TabIndex = 15;
            // 
            // tbModLengthMin
            // 
            this.tbModLengthMin.Location = new System.Drawing.Point(73, 39);
            this.tbModLengthMin.Name = "tbModLengthMin";
            this.tbModLengthMin.ReadOnly = true;
            this.tbModLengthMin.Size = new System.Drawing.Size(39, 20);
            this.tbModLengthMin.TabIndex = 14;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 20);
            this.textBox1.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(125, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "%";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(111, 66);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(45, 20);
            this.textBox6.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(111, 94);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(45, 20);
            this.textBox7.TabIndex = 20;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(312, 84);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(57, 30);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = "Next >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // CreatePrelimLayoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 574);
            this.Controls.Add(this.gbModLimitations);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadDataFile);
            this.Controls.Add(this.gbUnitConfig);
            this.Controls.Add(this.gbFloorDimension);
            this.Controls.Add(this.btnBuildLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreatePrelimLayoutForm";
            this.Text = "Start Building Modular Layout";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbFloorDimension.ResumeLayout(false);
            this.gbFloorDimension.PerformLayout();
            this.gbUnitConfig.ResumeLayout(false);
            this.gbUnitConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbModLimitations.ResumeLayout(false);
            this.gbModLimitations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBuildLayout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbFloorDimension;
        private System.Windows.Forms.GroupBox gbUnitConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStudioPriority;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadDataFile;
        private System.Windows.Forms.TextBox tbTotalSquareFootage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnApplyFlrDim;
        private System.Windows.Forms.ComboBox cb2BedPriority;
        private System.Windows.Forms.ComboBox cb1BedPriority;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb2BedCount;
        private System.Windows.Forms.TextBox tb1BedCount;
        private System.Windows.Forms.TextBox tbStudioCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbHallwayAlignment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbHallwayWidth;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbModLimitations;
        private System.Windows.Forms.TextBox tbModWidthMax;
        private System.Windows.Forms.TextBox tbModWidthMin;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbModLengthMax;
        private System.Windows.Forms.TextBox tbModLengthMin;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbFixedWidth;
        private System.Windows.Forms.Button btnDeleteGeom;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnNext;
    }
}