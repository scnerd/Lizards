namespace LizardsGUI
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblEnvironTemp = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHeaterTemp = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblLizards = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gphHeaterTemp = new LineGraph.LabelledLinePainter();
            this.gphEnvironTemp = new LineGraph.LabelledLinePainter();
            this.txtDebugLog = new System.Windows.Forms.TextBox();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnRamp = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnForceStop = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEnvironTemp,
            this.lblHeaterTemp});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(776, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblEnvironTemp
            // 
            this.lblEnvironTemp.Name = "lblEnvironTemp";
            this.lblEnvironTemp.Size = new System.Drawing.Size(57, 19);
            this.lblEnvironTemp.Text = "ENVIRON";
            // 
            // lblHeaterTemp
            // 
            this.lblHeaterTemp.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblHeaterTemp.Name = "lblHeaterTemp";
            this.lblHeaterTemp.Size = new System.Drawing.Size(54, 19);
            this.lblHeaterTemp.Text = "HEATER";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtDebugLog);
            this.splitContainer1.Panel2.Controls.Add(this.btnHold);
            this.splitContainer1.Panel2.Controls.Add(this.btnRamp);
            this.splitContainer1.Panel2.Controls.Add(this.btnStop);
            this.splitContainer1.Panel2.Controls.Add(this.btnForceStop);
            this.splitContainer1.Panel2.Controls.Add(this.btnSave);
            this.splitContainer1.Size = new System.Drawing.Size(776, 424);
            this.splitContainer1.SplitterDistance = 645;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tblLizards);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(645, 424);
            this.splitContainer2.SplitterDistance = 338;
            this.splitContainer2.TabIndex = 0;
            // 
            // tblLizards
            // 
            this.tblLizards.AutoScroll = true;
            this.tblLizards.AutoSize = true;
            this.tblLizards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLizards.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tblLizards.ColumnCount = 1;
            this.tblLizards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLizards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLizards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLizards.Location = new System.Drawing.Point(0, 0);
            this.tblLizards.Name = "tblLizards";
            this.tblLizards.RowCount = 1;
            this.tblLizards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLizards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tblLizards.Size = new System.Drawing.Size(645, 338);
            this.tblLizards.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gphHeaterTemp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gphEnvironTemp, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(645, 82);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gphHeaterTemp
            // 
            this.gphHeaterTemp.AutoAdjustLimits = false;
            this.gphHeaterTemp.BackFillColor = System.Drawing.Color.Black;
            this.gphHeaterTemp.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gphHeaterTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gphHeaterTemp.HorizontalBarInterval = 13;
            this.gphHeaterTemp.LineColor = System.Drawing.Color.Red;
            this.gphHeaterTemp.Location = new System.Drawing.Point(325, 3);
            this.gphHeaterTemp.LowerLimit = 25D;
            this.gphHeaterTemp.MaxDataPoints = 3000;
            this.gphHeaterTemp.Name = "gphHeaterTemp";
            this.gphHeaterTemp.ShowDemoVals = false;
            this.gphHeaterTemp.Size = new System.Drawing.Size(317, 76);
            this.gphHeaterTemp.TabIndex = 1;
            this.gphHeaterTemp.Transparency = ((byte)(100));
            this.gphHeaterTemp.UpperLimit = 100D;
            this.gphHeaterTemp.ValueInterval = 1;
            this.gphHeaterTemp.VerticalBarInterval = 13;
            // 
            // gphEnvironTemp
            // 
            this.gphEnvironTemp.AutoAdjustLimits = false;
            this.gphEnvironTemp.BackFillColor = System.Drawing.Color.Black;
            this.gphEnvironTemp.BackLineColor = System.Drawing.Color.Olive;
            this.gphEnvironTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gphEnvironTemp.HorizontalBarInterval = 13;
            this.gphEnvironTemp.LineColor = System.Drawing.Color.Khaki;
            this.gphEnvironTemp.Location = new System.Drawing.Point(3, 3);
            this.gphEnvironTemp.LowerLimit = 25D;
            this.gphEnvironTemp.MaxDataPoints = 3000;
            this.gphEnvironTemp.Name = "gphEnvironTemp";
            this.gphEnvironTemp.ShowDemoVals = false;
            this.gphEnvironTemp.Size = new System.Drawing.Size(316, 76);
            this.gphEnvironTemp.TabIndex = 0;
            this.gphEnvironTemp.Transparency = ((byte)(100));
            this.gphEnvironTemp.UpperLimit = 60D;
            this.gphEnvironTemp.ValueInterval = 1;
            this.gphEnvironTemp.VerticalBarInterval = 13;
            // 
            // txtDebugLog
            // 
            this.txtDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebugLog.Location = new System.Drawing.Point(0, 0);
            this.txtDebugLog.Multiline = true;
            this.txtDebugLog.Name = "txtDebugLog";
            this.txtDebugLog.Size = new System.Drawing.Size(127, 264);
            this.txtDebugLog.TabIndex = 7;
            // 
            // btnHold
            // 
            this.btnHold.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnHold.Location = new System.Drawing.Point(0, 264);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(127, 32);
            this.btnHold.TabIndex = 6;
            this.btnHold.Text = "Hold Initial Temp";
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // btnRamp
            // 
            this.btnRamp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRamp.Location = new System.Drawing.Point(0, 296);
            this.btnRamp.Name = "btnRamp";
            this.btnRamp.Size = new System.Drawing.Size(127, 32);
            this.btnRamp.TabIndex = 5;
            this.btnRamp.Text = "Start Ramping Temp";
            this.btnRamp.UseVisualStyleBackColor = true;
            this.btnRamp.Click += new System.EventHandler(this.btnRamp_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStop.Location = new System.Drawing.Point(0, 328);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(127, 32);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "End Experiment";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnForceStop
            // 
            this.btnForceStop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnForceStop.ForeColor = System.Drawing.Color.Maroon;
            this.btnForceStop.Location = new System.Drawing.Point(0, 360);
            this.btnForceStop.Name = "btnForceStop";
            this.btnForceStop.Size = new System.Drawing.Size(127, 32);
            this.btnForceStop.TabIndex = 3;
            this.btnForceStop.Text = "Emergency Reset";
            this.btnForceStop.UseVisualStyleBackColor = true;
            this.btnForceStop.Click += new System.EventHandler(this.btnForceStop_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Location = new System.Drawing.Point(0, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Results";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 448);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "Lizards";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private LineGraph.LabelledLinePainter gphEnvironTemp;
        private System.Windows.Forms.TableLayoutPanel tblLizards;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDebugLog;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnRamp;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnForceStop;
        private System.Windows.Forms.ToolStripStatusLabel lblEnvironTemp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LineGraph.LabelledLinePainter gphHeaterTemp;
        private System.Windows.Forms.ToolStripStatusLabel lblHeaterTemp;
    }
}

