namespace LizardsGUI
{
    partial class InitExperiment
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
            this.lblNumLizards = new System.Windows.Forms.Label();
            this.numNumLizards = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.lblComPorts = new System.Windows.Forms.Label();
            this.numReport = new System.Windows.Forms.NumericUpDown();
            this.lblReport = new System.Windows.Forms.Label();
            this.btnComPorts = new System.Windows.Forms.Button();
            this.btnNumLizards = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMaxTemp = new System.Windows.Forms.Button();
            this.btnRampTemp = new System.Windows.Forms.Button();
            this.btnHoldTemp = new System.Windows.Forms.Button();
            this.numMaxTemp = new System.Windows.Forms.NumericUpDown();
            this.numRampTemp = new System.Windows.Forms.NumericUpDown();
            this.numHoldTemp = new System.Windows.Forms.NumericUpDown();
            this.lblMaxTemp = new System.Windows.Forms.Label();
            this.lblRampTemp = new System.Windows.Forms.Label();
            this.lblHoldTemp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numNumLizards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReport)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRampTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoldTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumLizards
            // 
            this.lblNumLizards.AutoSize = true;
            this.lblNumLizards.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNumLizards.Location = new System.Drawing.Point(3, 38);
            this.lblNumLizards.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblNumLizards.Name = "lblNumLizards";
            this.lblNumLizards.Size = new System.Drawing.Size(120, 13);
            this.lblNumLizards.TabIndex = 7;
            this.lblNumLizards.Text = "Number of Lizards:";
            this.lblNumLizards.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numNumLizards
            // 
            this.numNumLizards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numNumLizards.Location = new System.Drawing.Point(129, 34);
            this.numNumLizards.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numNumLizards.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumLizards.Name = "numNumLizards";
            this.numNumLizards.Size = new System.Drawing.Size(120, 20);
            this.numNumLizards.TabIndex = 1;
            this.numNumLizards.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numNumLizards.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Location = new System.Drawing.Point(3, 222);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(378, 68);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Confirm Settings";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(129, 3);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(120, 21);
            this.cmbComPorts.TabIndex = 0;
            // 
            // lblComPorts
            // 
            this.lblComPorts.AutoSize = true;
            this.lblComPorts.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblComPorts.Location = new System.Drawing.Point(3, 7);
            this.lblComPorts.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblComPorts.Name = "lblComPorts";
            this.lblComPorts.Size = new System.Drawing.Size(120, 13);
            this.lblComPorts.TabIndex = 6;
            this.lblComPorts.Text = "Arduino Port:";
            this.lblComPorts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numReport
            // 
            this.numReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numReport.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numReport.Location = new System.Drawing.Point(129, 65);
            this.numReport.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numReport.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReport.Name = "numReport";
            this.numReport.Size = new System.Drawing.Size(120, 20);
            this.numReport.TabIndex = 2;
            this.numReport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numReport.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReport.Location = new System.Drawing.Point(3, 69);
            this.lblReport.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(120, 13);
            this.lblReport.TabIndex = 8;
            this.lblReport.Text = "Report Interval (s):";
            this.lblReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnComPorts
            // 
            this.btnComPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnComPorts.Location = new System.Drawing.Point(262, 3);
            this.btnComPorts.Margin = new System.Windows.Forms.Padding(10, 3, 10, 5);
            this.btnComPorts.Name = "btnComPorts";
            this.btnComPorts.Size = new System.Drawing.Size(106, 23);
            this.btnComPorts.TabIndex = 3;
            this.btnComPorts.Text = "Refresh";
            this.btnComPorts.UseVisualStyleBackColor = true;
            this.btnComPorts.Click += new System.EventHandler(this.btnComPorts_Click);
            // 
            // btnNumLizards
            // 
            this.btnNumLizards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNumLizards.Location = new System.Drawing.Point(262, 34);
            this.btnNumLizards.Margin = new System.Windows.Forms.Padding(10, 3, 10, 5);
            this.btnNumLizards.Name = "btnNumLizards";
            this.btnNumLizards.Size = new System.Drawing.Size(106, 23);
            this.btnNumLizards.TabIndex = 4;
            this.btnNumLizards.Text = "Reset";
            this.btnNumLizards.UseVisualStyleBackColor = true;
            this.btnNumLizards.Click += new System.EventHandler(this.btnNumLizards_Click);
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReport.Location = new System.Drawing.Point(262, 65);
            this.btnReport.Margin = new System.Windows.Forms.Padding(10, 3, 10, 5);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(106, 23);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "Reset";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 293);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btnMaxTemp, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnRampTemp, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnHoldTemp, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.numMaxTemp, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.numRampTemp, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.numHoldTemp, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblMaxTemp, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblComPorts, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnReport, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblNumLizards, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnNumLizards, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblReport, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnComPorts, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbComPorts, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.numReport, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numNumLizards, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblRampTemp, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblHoldTemp, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(378, 189);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnMaxTemp
            // 
            this.btnMaxTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMaxTemp.Location = new System.Drawing.Point(262, 158);
            this.btnMaxTemp.Margin = new System.Windows.Forms.Padding(10, 3, 10, 8);
            this.btnMaxTemp.Name = "btnMaxTemp";
            this.btnMaxTemp.Size = new System.Drawing.Size(106, 23);
            this.btnMaxTemp.TabIndex = 16;
            this.btnMaxTemp.Text = "Reset";
            this.btnMaxTemp.UseVisualStyleBackColor = true;
            this.btnMaxTemp.Click += new System.EventHandler(this.btnMaxTemp_Click);
            // 
            // btnRampTemp
            // 
            this.btnRampTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRampTemp.Location = new System.Drawing.Point(262, 127);
            this.btnRampTemp.Margin = new System.Windows.Forms.Padding(10, 3, 10, 5);
            this.btnRampTemp.Name = "btnRampTemp";
            this.btnRampTemp.Size = new System.Drawing.Size(106, 23);
            this.btnRampTemp.TabIndex = 15;
            this.btnRampTemp.Text = "Reset";
            this.btnRampTemp.UseVisualStyleBackColor = true;
            this.btnRampTemp.Click += new System.EventHandler(this.btnRampTemp_Click);
            // 
            // btnHoldTemp
            // 
            this.btnHoldTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHoldTemp.Location = new System.Drawing.Point(262, 96);
            this.btnHoldTemp.Margin = new System.Windows.Forms.Padding(10, 3, 10, 5);
            this.btnHoldTemp.Name = "btnHoldTemp";
            this.btnHoldTemp.Size = new System.Drawing.Size(106, 23);
            this.btnHoldTemp.TabIndex = 14;
            this.btnHoldTemp.Text = "Reset";
            this.btnHoldTemp.UseVisualStyleBackColor = true;
            this.btnHoldTemp.Click += new System.EventHandler(this.btnHoldTemp_Click);
            // 
            // numMaxTemp
            // 
            this.numMaxTemp.DecimalPlaces = 2;
            this.numMaxTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMaxTemp.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numMaxTemp.Location = new System.Drawing.Point(129, 158);
            this.numMaxTemp.Name = "numMaxTemp";
            this.numMaxTemp.Size = new System.Drawing.Size(120, 20);
            this.numMaxTemp.TabIndex = 11;
            this.numMaxTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMaxTemp.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numRampTemp
            // 
            this.numRampTemp.DecimalPlaces = 2;
            this.numRampTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numRampTemp.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numRampTemp.Location = new System.Drawing.Point(129, 127);
            this.numRampTemp.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRampTemp.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numRampTemp.Name = "numRampTemp";
            this.numRampTemp.Size = new System.Drawing.Size(120, 20);
            this.numRampTemp.TabIndex = 12;
            this.numRampTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRampTemp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numHoldTemp
            // 
            this.numHoldTemp.DecimalPlaces = 2;
            this.numHoldTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numHoldTemp.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numHoldTemp.Location = new System.Drawing.Point(129, 96);
            this.numHoldTemp.Name = "numHoldTemp";
            this.numHoldTemp.Size = new System.Drawing.Size(120, 20);
            this.numHoldTemp.TabIndex = 13;
            this.numHoldTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numHoldTemp.Value = new decimal(new int[] {
            27,
            0,
            0,
            0});
            // 
            // lblMaxTemp
            // 
            this.lblMaxTemp.AutoSize = true;
            this.lblMaxTemp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMaxTemp.Location = new System.Drawing.Point(3, 162);
            this.lblMaxTemp.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblMaxTemp.Name = "lblMaxTemp";
            this.lblMaxTemp.Size = new System.Drawing.Size(120, 13);
            this.lblMaxTemp.TabIndex = 11;
            this.lblMaxTemp.Text = "Max Temp (C):";
            this.lblMaxTemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRampTemp
            // 
            this.lblRampTemp.AutoSize = true;
            this.lblRampTemp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRampTemp.Location = new System.Drawing.Point(3, 131);
            this.lblRampTemp.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblRampTemp.Name = "lblRampTemp";
            this.lblRampTemp.Size = new System.Drawing.Size(120, 13);
            this.lblRampTemp.TabIndex = 9;
            this.lblRampTemp.Text = "Ramp (C / min):";
            this.lblRampTemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHoldTemp
            // 
            this.lblHoldTemp.AutoSize = true;
            this.lblHoldTemp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHoldTemp.Location = new System.Drawing.Point(3, 100);
            this.lblHoldTemp.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblHoldTemp.Name = "lblHoldTemp";
            this.lblHoldTemp.Size = new System.Drawing.Size(120, 13);
            this.lblHoldTemp.TabIndex = 10;
            this.lblHoldTemp.Text = "Hold / Start Temp (C):";
            this.lblHoldTemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InitExperiment
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 293);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InitExperiment";
            this.Text = "InitExperiment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InitExperiment_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numNumLizards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReport)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRampTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHoldTemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNumLizards;
        private System.Windows.Forms.NumericUpDown numNumLizards;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Label lblComPorts;
        private System.Windows.Forms.NumericUpDown numReport;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Button btnComPorts;
        private System.Windows.Forms.Button btnNumLizards;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnMaxTemp;
        private System.Windows.Forms.Button btnRampTemp;
        private System.Windows.Forms.Button btnHoldTemp;
        private System.Windows.Forms.NumericUpDown numMaxTemp;
        private System.Windows.Forms.NumericUpDown numRampTemp;
        private System.Windows.Forms.NumericUpDown numHoldTemp;
        private System.Windows.Forms.Label lblMaxTemp;
        private System.Windows.Forms.Label lblRampTemp;
        private System.Windows.Forms.Label lblHoldTemp;
    }
}