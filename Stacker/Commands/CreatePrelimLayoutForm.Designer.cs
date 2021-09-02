
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
            this.cbDrawInteriorLAyout = new System.Windows.Forms.CheckBox();
            this.cbDrawOutlineWalls = new System.Windows.Forms.CheckBox();
            this.cbHallwayAlignment = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbHallwayWidth = new System.Windows.Forms.TextBox();
            this.tbTotalSquareFootage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnApplyFlrDim = new System.Windows.Forms.Button();
            this.gbUnitConfig = new System.Windows.Forms.GroupBox();
            this.cbOptions2Bed = new System.Windows.Forms.ComboBox();
            this.cbOptions1Bed = new System.Windows.Forms.ComboBox();
            this.cbOptionsStudio = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbPercentage2Bed = new System.Windows.Forms.TextBox();
            this.tbPercentage1Bed = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbPercentageStudio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadDataFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddInteriorLayout = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
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
            this.tbOptionsGenerated = new System.Windows.Forms.TextBox();
            this.cbTotalIterations = new System.Windows.Forms.CheckBox();
            this.tbLimitIterations = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnViewJSON = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbFloorDimension.SuspendLayout();
            this.gbUnitConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbModLimitations.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuildLayout
            // 
            this.btnBuildLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuildLayout.Location = new System.Drawing.Point(326, 1013);
            this.btnBuildLayout.Margin = new System.Windows.Forms.Padding(12);
            this.btnBuildLayout.Name = "btnBuildLayout";
            this.btnBuildLayout.Size = new System.Drawing.Size(268, 58);
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
            this.pictureBox1.Location = new System.Drawing.Point(1144, 29);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(142, 50);
            this.tbLength.Margin = new System.Windows.Forms.Padding(6);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(74, 31);
            this.tbLength.TabIndex = 1;
            this.tbLength.Text = "120";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(14, 56);
            this.lblLength.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(116, 25);
            this.lblLength.TabIndex = 2;
            this.lblLength.Text = "Length (ft):";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(362, 50);
            this.tbWidth.Margin = new System.Windows.Forms.Padding(6);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(74, 31);
            this.tbWidth.TabIndex = 3;
            this.tbWidth.Text = "60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Width (ft):";
            // 
            // gbFloorDimension
            // 
            this.gbFloorDimension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFloorDimension.Controls.Add(this.cbDrawInteriorLAyout);
            this.gbFloorDimension.Controls.Add(this.cbDrawOutlineWalls);
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
            this.gbFloorDimension.Location = new System.Drawing.Point(24, 104);
            this.gbFloorDimension.Margin = new System.Windows.Forms.Padding(6);
            this.gbFloorDimension.Name = "gbFloorDimension";
            this.gbFloorDimension.Padding = new System.Windows.Forms.Padding(6);
            this.gbFloorDimension.Size = new System.Drawing.Size(764, 275);
            this.gbFloorDimension.TabIndex = 1;
            this.gbFloorDimension.TabStop = false;
            this.gbFloorDimension.Text = "Floor Geometry";
            // 
            // cbDrawInteriorLAyout
            // 
            this.cbDrawInteriorLAyout.AutoSize = true;
            this.cbDrawInteriorLAyout.Location = new System.Drawing.Point(148, 167);
            this.cbDrawInteriorLAyout.Margin = new System.Windows.Forms.Padding(6);
            this.cbDrawInteriorLAyout.Name = "cbDrawInteriorLAyout";
            this.cbDrawInteriorLAyout.Size = new System.Drawing.Size(236, 29);
            this.cbDrawInteriorLAyout.TabIndex = 12;
            this.cbDrawInteriorLAyout.Text = "Draw Interior Layout";
            this.cbDrawInteriorLAyout.UseVisualStyleBackColor = true;
            // 
            // cbDrawOutlineWalls
            // 
            this.cbDrawOutlineWalls.AutoSize = true;
            this.cbDrawOutlineWalls.Location = new System.Drawing.Point(148, 212);
            this.cbDrawOutlineWalls.Margin = new System.Windows.Forms.Padding(6);
            this.cbDrawOutlineWalls.Name = "cbDrawOutlineWalls";
            this.cbDrawOutlineWalls.Size = new System.Drawing.Size(226, 29);
            this.cbDrawOutlineWalls.TabIndex = 11;
            this.cbDrawOutlineWalls.Text = "Draw Wall Outlines";
            this.cbDrawOutlineWalls.UseVisualStyleBackColor = true;
            // 
            // cbHallwayAlignment
            // 
            this.cbHallwayAlignment.FormattingEnabled = true;
            this.cbHallwayAlignment.Items.AddRange(new object[] {
            "Auto (Select Largest)",
            "Length",
            "Width"});
            this.cbHallwayAlignment.Location = new System.Drawing.Point(448, 100);
            this.cbHallwayAlignment.Margin = new System.Windows.Forms.Padding(6);
            this.cbHallwayAlignment.Name = "cbHallwayAlignment";
            this.cbHallwayAlignment.Size = new System.Drawing.Size(248, 33);
            this.cbHallwayAlignment.TabIndex = 10;
            this.cbHallwayAlignment.Text = "Auto (Select Largest)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 106);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(194, 25);
            this.label10.TabIndex = 9;
            this.label10.Text = "Hallway Alignment:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 25);
            this.label9.TabIndex = 8;
            this.label9.Text = "Hallway (ft):";
            // 
            // tbHallwayWidth
            // 
            this.tbHallwayWidth.Location = new System.Drawing.Point(142, 100);
            this.tbHallwayWidth.Margin = new System.Windows.Forms.Padding(6);
            this.tbHallwayWidth.Name = "tbHallwayWidth";
            this.tbHallwayWidth.ReadOnly = true;
            this.tbHallwayWidth.Size = new System.Drawing.Size(74, 31);
            this.tbHallwayWidth.TabIndex = 7;
            this.tbHallwayWidth.Text = "5";
            // 
            // tbTotalSquareFootage
            // 
            this.tbTotalSquareFootage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTotalSquareFootage.Location = new System.Drawing.Point(660, 208);
            this.tbTotalSquareFootage.Margin = new System.Windows.Forms.Padding(6);
            this.tbTotalSquareFootage.Name = "tbTotalSquareFootage";
            this.tbTotalSquareFootage.ReadOnly = true;
            this.tbTotalSquareFootage.Size = new System.Drawing.Size(74, 31);
            this.tbTotalSquareFootage.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 213);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total Square Footage (sf):";
            // 
            // btnApplyFlrDim
            // 
            this.btnApplyFlrDim.Location = new System.Drawing.Point(20, 175);
            this.btnApplyFlrDim.Margin = new System.Windows.Forms.Padding(12);
            this.btnApplyFlrDim.Name = "btnApplyFlrDim";
            this.btnApplyFlrDim.Padding = new System.Windows.Forms.Padding(6);
            this.btnApplyFlrDim.Size = new System.Drawing.Size(110, 58);
            this.btnApplyFlrDim.TabIndex = 4;
            this.btnApplyFlrDim.Text = "Apply";
            this.btnApplyFlrDim.UseVisualStyleBackColor = true;
            this.btnApplyFlrDim.Click += new System.EventHandler(this.btnApplyFlrDim_Click);
            // 
            // gbUnitConfig
            // 
            this.gbUnitConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUnitConfig.Controls.Add(this.cbOptions2Bed);
            this.gbUnitConfig.Controls.Add(this.cbOptions1Bed);
            this.gbUnitConfig.Controls.Add(this.cbOptionsStudio);
            this.gbUnitConfig.Controls.Add(this.label15);
            this.gbUnitConfig.Controls.Add(this.tbPercentage2Bed);
            this.gbUnitConfig.Controls.Add(this.tbPercentage1Bed);
            this.gbUnitConfig.Controls.Add(this.label21);
            this.gbUnitConfig.Controls.Add(this.tbPercentageStudio);
            this.gbUnitConfig.Controls.Add(this.label4);
            this.gbUnitConfig.Controls.Add(this.label3);
            this.gbUnitConfig.Controls.Add(this.label2);
            this.gbUnitConfig.Location = new System.Drawing.Point(24, 390);
            this.gbUnitConfig.Margin = new System.Windows.Forms.Padding(6);
            this.gbUnitConfig.Name = "gbUnitConfig";
            this.gbUnitConfig.Padding = new System.Windows.Forms.Padding(6);
            this.gbUnitConfig.Size = new System.Drawing.Size(764, 287);
            this.gbUnitConfig.TabIndex = 2;
            this.gbUnitConfig.TabStop = false;
            this.gbUnitConfig.Text = "Unit Configuration Priority";
            // 
            // cbOptions2Bed
            // 
            this.cbOptions2Bed.FormattingEnabled = true;
            this.cbOptions2Bed.Items.AddRange(new object[] {
            "Any 2-Bed"});
            this.cbOptions2Bed.Location = new System.Drawing.Point(236, 181);
            this.cbOptions2Bed.Margin = new System.Windows.Forms.Padding(6);
            this.cbOptions2Bed.Name = "cbOptions2Bed";
            this.cbOptions2Bed.Size = new System.Drawing.Size(352, 33);
            this.cbOptions2Bed.TabIndex = 24;
            this.cbOptions2Bed.Text = "Any 2-Bed";
            // 
            // cbOptions1Bed
            // 
            this.cbOptions1Bed.FormattingEnabled = true;
            this.cbOptions1Bed.Items.AddRange(new object[] {
            "Any 1-Bed"});
            this.cbOptions1Bed.Location = new System.Drawing.Point(236, 127);
            this.cbOptions1Bed.Margin = new System.Windows.Forms.Padding(6);
            this.cbOptions1Bed.Name = "cbOptions1Bed";
            this.cbOptions1Bed.Size = new System.Drawing.Size(352, 33);
            this.cbOptions1Bed.TabIndex = 23;
            this.cbOptions1Bed.Text = "Any 1-Bed";
            // 
            // cbOptionsStudio
            // 
            this.cbOptionsStudio.FormattingEnabled = true;
            this.cbOptionsStudio.Items.AddRange(new object[] {
            "Any Studio"});
            this.cbOptionsStudio.Location = new System.Drawing.Point(236, 75);
            this.cbOptionsStudio.Margin = new System.Windows.Forms.Padding(6);
            this.cbOptionsStudio.Name = "cbOptionsStudio";
            this.cbOptionsStudio.Size = new System.Drawing.Size(352, 33);
            this.cbOptionsStudio.TabIndex = 22;
            this.cbOptionsStudio.Text = "Any Studio";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(314, 42);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(204, 25);
            this.label15.TabIndex = 21;
            this.label15.Text = "Layout Type Priority";
            // 
            // tbPercentage2Bed
            // 
            this.tbPercentage2Bed.Location = new System.Drawing.Point(112, 181);
            this.tbPercentage2Bed.Margin = new System.Windows.Forms.Padding(6);
            this.tbPercentage2Bed.Name = "tbPercentage2Bed";
            this.tbPercentage2Bed.Size = new System.Drawing.Size(86, 31);
            this.tbPercentage2Bed.TabIndex = 20;
            this.tbPercentage2Bed.Text = "30";
            // 
            // tbPercentage1Bed
            // 
            this.tbPercentage1Bed.Location = new System.Drawing.Point(112, 127);
            this.tbPercentage1Bed.Margin = new System.Windows.Forms.Padding(6);
            this.tbPercentage1Bed.Name = "tbPercentage1Bed";
            this.tbPercentage1Bed.Size = new System.Drawing.Size(86, 31);
            this.tbPercentage1Bed.TabIndex = 19;
            this.tbPercentage1Bed.Text = "40";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(140, 42);
            this.label21.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 25);
            this.label21.TabIndex = 18;
            this.label21.Text = "%";
            // 
            // tbPercentageStudio
            // 
            this.tbPercentageStudio.Location = new System.Drawing.Point(112, 75);
            this.tbPercentageStudio.Margin = new System.Windows.Forms.Padding(6);
            this.tbPercentageStudio.Name = "tbPercentageStudio";
            this.tbPercentageStudio.Size = new System.Drawing.Size(86, 31);
            this.tbPercentageStudio.TabIndex = 12;
            this.tbPercentageStudio.Text = "30";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 185);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "2-Bed";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "1-Bed:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Studio:";
            // 
            // btnLoadDataFile
            // 
            this.btnLoadDataFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLoadDataFile.Location = new System.Drawing.Point(466, 29);
            this.btnLoadDataFile.Margin = new System.Windows.Forms.Padding(12);
            this.btnLoadDataFile.Name = "btnLoadDataFile";
            this.btnLoadDataFile.Padding = new System.Windows.Forms.Padding(6);
            this.btnLoadDataFile.Size = new System.Drawing.Size(310, 58);
            this.btnLoadDataFile.TabIndex = 3;
            this.btnLoadDataFile.Text = "Load JSON Data File";
            this.btnLoadDataFile.UseVisualStyleBackColor = true;
            this.btnLoadDataFile.Click += new System.EventHandler(this.btnLoadDataFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnAddInteriorLayout);
            this.groupBox1.Controls.Add(this.btnPrevious);
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
            this.groupBox1.Location = new System.Drawing.Point(24, 688);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(764, 302);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unit Configuration Preview";
            // 
            // btnAddInteriorLayout
            // 
            this.btnAddInteriorLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddInteriorLayout.Location = new System.Drawing.Point(506, 148);
            this.btnAddInteriorLayout.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddInteriorLayout.Name = "btnAddInteriorLayout";
            this.btnAddInteriorLayout.Size = new System.Drawing.Size(232, 58);
            this.btnAddInteriorLayout.TabIndex = 20;
            this.btnAddInteriorLayout.Text = "Add Interior Layout";
            this.btnAddInteriorLayout.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Location = new System.Drawing.Point(506, 217);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(6);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(114, 58);
            this.btnPrevious.TabIndex = 19;
            this.btnPrevious.Text = "<< Prev.";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(628, 217);
            this.btnNext.Margin = new System.Windows.Forms.Padding(6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(114, 58);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = "Next >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnDeleteGeom
            // 
            this.btnDeleteGeom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteGeom.Location = new System.Drawing.Point(628, 31);
            this.btnDeleteGeom.Margin = new System.Windows.Forms.Padding(6);
            this.btnDeleteGeom.Name = "btnDeleteGeom";
            this.btnDeleteGeom.Size = new System.Drawing.Size(114, 58);
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
            this.label13.Location = new System.Drawing.Point(254, 37);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 25);
            this.label13.TabIndex = 15;
            this.label13.Text = "SF";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(236, 181);
            this.textBox3.Margin = new System.Windows.Forms.Padding(6);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(74, 31);
            this.textBox3.TabIndex = 16;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(236, 125);
            this.textBox4.Margin = new System.Windows.Forms.Padding(6);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(74, 31);
            this.textBox4.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(236, 71);
            this.textBox5.Margin = new System.Windows.Forms.Padding(6);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(74, 31);
            this.textBox5.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(136, 37);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 25);
            this.label12.TabIndex = 10;
            this.label12.Text = "UNITS";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(376, 235);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(74, 31);
            this.textBox2.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 240);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(319, 25);
            this.label11.TabIndex = 11;
            this.label11.Text = "Total Square Footage Used (sf):";
            // 
            // tb2BedCount
            // 
            this.tb2BedCount.Location = new System.Drawing.Point(136, 181);
            this.tb2BedCount.Margin = new System.Windows.Forms.Padding(6);
            this.tb2BedCount.Name = "tb2BedCount";
            this.tb2BedCount.ReadOnly = true;
            this.tb2BedCount.Size = new System.Drawing.Size(74, 31);
            this.tb2BedCount.TabIndex = 10;
            // 
            // tb1BedCount
            // 
            this.tb1BedCount.Location = new System.Drawing.Point(136, 125);
            this.tb1BedCount.Margin = new System.Windows.Forms.Padding(6);
            this.tb1BedCount.Name = "tb1BedCount";
            this.tb1BedCount.ReadOnly = true;
            this.tb1BedCount.Size = new System.Drawing.Size(74, 31);
            this.tb1BedCount.TabIndex = 9;
            // 
            // tbStudioCount
            // 
            this.tbStudioCount.Location = new System.Drawing.Point(136, 71);
            this.tbStudioCount.Margin = new System.Windows.Forms.Padding(6);
            this.tbStudioCount.Name = "tbStudioCount";
            this.tbStudioCount.ReadOnly = true;
            this.tbStudioCount.Size = new System.Drawing.Size(74, 31);
            this.tbStudioCount.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 181);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "2-Bed";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 131);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 25);
            this.label7.TabIndex = 7;
            this.label7.Text = "1-Bed:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 77);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 25);
            this.label8.TabIndex = 6;
            this.label8.Text = "Studio:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(642, 1013);
            this.btnClose.Margin = new System.Windows.Forms.Padding(12, 0, 6, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(4);
            this.btnClose.Size = new System.Drawing.Size(184, 58);
            this.btnClose.TabIndex = 83;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gbModLimitations
            // 
            this.gbModLimitations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.gbModLimitations.Location = new System.Drawing.Point(800, 102);
            this.gbModLimitations.Margin = new System.Windows.Forms.Padding(6);
            this.gbModLimitations.Name = "gbModLimitations";
            this.gbModLimitations.Padding = new System.Windows.Forms.Padding(6);
            this.gbModLimitations.Size = new System.Drawing.Size(408, 277);
            this.gbModLimitations.TabIndex = 84;
            this.gbModLimitations.TabStop = false;
            this.gbModLimitations.Text = "Modular Limitations";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 206);
            this.label20.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(143, 25);
            this.label20.TabIndex = 12;
            this.label20.Text = "Set Width (ft):";
            // 
            // tbFixedWidth
            // 
            this.tbFixedWidth.Location = new System.Drawing.Point(178, 200);
            this.tbFixedWidth.Margin = new System.Windows.Forms.Padding(6);
            this.tbFixedWidth.Name = "tbFixedWidth";
            this.tbFixedWidth.Size = new System.Drawing.Size(74, 31);
            this.tbFixedWidth.TabIndex = 11;
            this.tbFixedWidth.Text = "12";
            // 
            // tbModWidthMax
            // 
            this.tbModWidthMax.Location = new System.Drawing.Point(254, 138);
            this.tbModWidthMax.Margin = new System.Windows.Forms.Padding(6);
            this.tbModWidthMax.Name = "tbModWidthMax";
            this.tbModWidthMax.Size = new System.Drawing.Size(74, 31);
            this.tbModWidthMax.TabIndex = 20;
            // 
            // tbModWidthMin
            // 
            this.tbModWidthMin.Location = new System.Drawing.Point(136, 138);
            this.tbModWidthMin.Margin = new System.Windows.Forms.Padding(6);
            this.tbModWidthMin.Name = "tbModWidthMin";
            this.tbModWidthMin.Size = new System.Drawing.Size(74, 31);
            this.tbModWidthMin.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 144);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 25);
            this.label19.TabIndex = 18;
            this.label19.Text = "Width (ft):";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(264, 37);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 25);
            this.label17.TabIndex = 17;
            this.label17.Text = "MAX";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(146, 37);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 25);
            this.label18.TabIndex = 16;
            this.label18.Text = "MIN";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 85);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(116, 25);
            this.label16.TabIndex = 11;
            this.label16.Text = "Length (ft):";
            // 
            // tbModLengthMax
            // 
            this.tbModLengthMax.Location = new System.Drawing.Point(254, 79);
            this.tbModLengthMax.Margin = new System.Windows.Forms.Padding(6);
            this.tbModLengthMax.Name = "tbModLengthMax";
            this.tbModLengthMax.Size = new System.Drawing.Size(74, 31);
            this.tbModLengthMax.TabIndex = 15;
            // 
            // tbModLengthMin
            // 
            this.tbModLengthMin.Location = new System.Drawing.Point(136, 79);
            this.tbModLengthMin.Margin = new System.Windows.Forms.Padding(6);
            this.tbModLengthMin.Name = "tbModLengthMin";
            this.tbModLengthMin.Size = new System.Drawing.Size(74, 31);
            this.tbModLengthMin.TabIndex = 14;
            // 
            // tbOptionsGenerated
            // 
            this.tbOptionsGenerated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOptionsGenerated.Location = new System.Drawing.Point(306, 96);
            this.tbOptionsGenerated.Margin = new System.Windows.Forms.Padding(6);
            this.tbOptionsGenerated.Name = "tbOptionsGenerated";
            this.tbOptionsGenerated.ReadOnly = true;
            this.tbOptionsGenerated.Size = new System.Drawing.Size(74, 31);
            this.tbOptionsGenerated.TabIndex = 19;
            // 
            // cbTotalIterations
            // 
            this.cbTotalIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTotalIterations.AutoSize = true;
            this.cbTotalIterations.Checked = true;
            this.cbTotalIterations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTotalIterations.Location = new System.Drawing.Point(39, 54);
            this.cbTotalIterations.Margin = new System.Windows.Forms.Padding(6);
            this.cbTotalIterations.Name = "cbTotalIterations";
            this.cbTotalIterations.Size = new System.Drawing.Size(243, 29);
            this.cbTotalIterations.TabIndex = 21;
            this.cbTotalIterations.Text = "Limit Total Iterations:";
            this.cbTotalIterations.UseVisualStyleBackColor = true;
            // 
            // tbLimitIterations
            // 
            this.tbLimitIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLimitIterations.Location = new System.Drawing.Point(306, 46);
            this.tbLimitIterations.Margin = new System.Windows.Forms.Padding(6);
            this.tbLimitIterations.Name = "tbLimitIterations";
            this.tbLimitIterations.Size = new System.Drawing.Size(74, 31);
            this.tbLimitIterations.TabIndex = 21;
            this.tbLimitIterations.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.cbTotalIterations);
            this.groupBox2.Controls.Add(this.tbOptionsGenerated);
            this.groupBox2.Controls.Add(this.tbLimitIterations);
            this.groupBox2.Location = new System.Drawing.Point(800, 390);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(410, 287);
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optimization Iteration Setting";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(34, 102);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(253, 25);
            this.label14.TabIndex = 13;
            this.label14.Text = "Total Options Generated:";
            // 
            // btnViewJSON
            // 
            this.btnViewJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewJSON.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnViewJSON.Image = ((System.Drawing.Image)(resources.GetObject("btnViewJSON.Image")));
            this.btnViewJSON.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewJSON.Location = new System.Drawing.Point(800, 29);
            this.btnViewJSON.Margin = new System.Windows.Forms.Padding(12, 0, 6, 0);
            this.btnViewJSON.Name = "btnViewJSON";
            this.btnViewJSON.Padding = new System.Windows.Forms.Padding(4);
            this.btnViewJSON.Size = new System.Drawing.Size(184, 58);
            this.btnViewJSON.TabIndex = 86;
            this.btnViewJSON.Text = "View JSON";
            this.btnViewJSON.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewJSON.UseVisualStyleBackColor = true;
            this.btnViewJSON.Click += new System.EventHandler(this.btnViewJSON_Click);
            // 
            // CreatePrelimLayoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1232, 1100);
            this.Controls.Add(this.btnViewJSON);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbModLimitations);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadDataFile);
            this.Controls.Add(this.gbUnitConfig);
            this.Controls.Add(this.gbFloorDimension);
            this.Controls.Add(this.btnBuildLayout);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "CreatePrelimLayoutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadDataFile;
        private System.Windows.Forms.TextBox tbTotalSquareFootage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnApplyFlrDim;
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
        private System.Windows.Forms.TextBox tbPercentage2Bed;
        private System.Windows.Forms.TextBox tbPercentage1Bed;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbPercentageStudio;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox tbOptionsGenerated;
        private System.Windows.Forms.CheckBox cbDrawOutlineWalls;
        private System.Windows.Forms.CheckBox cbDrawInteriorLAyout;
        private System.Windows.Forms.CheckBox cbTotalIterations;
        private System.Windows.Forms.TextBox tbLimitIterations;
        private System.Windows.Forms.ComboBox cbOptions2Bed;
        private System.Windows.Forms.ComboBox cbOptions1Bed;
        private System.Windows.Forms.ComboBox cbOptionsStudio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnAddInteriorLayout;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnViewJSON;
    }
}