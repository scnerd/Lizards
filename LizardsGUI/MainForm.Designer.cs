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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblLizards = new System.Windows.Forms.TableLayoutPanel();
            this.txtDebugLog = new System.Windows.Forms.TextBox();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnRamp = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnForceStop = new System.Windows.Forms.Button();
            this.btnReserved1 = new System.Windows.Forms.Button();
            this.btnReserved2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblAmbientTemp = new System.Windows.Forms.ToolStripStatusLabel();
            this.gphAmbientTemp = new LineGraph.LinePainter();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAmbientTemp});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(776, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            this.splitContainer1.Panel2.Controls.Add(this.btnReserved1);
            this.splitContainer1.Panel2.Controls.Add(this.btnReserved2);
            this.splitContainer1.Panel2.Controls.Add(this.btnSave);
            this.splitContainer1.Size = new System.Drawing.Size(776, 426);
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
            this.splitContainer2.Panel2.Controls.Add(this.gphAmbientTemp);
            this.splitContainer2.Size = new System.Drawing.Size(645, 426);
            this.splitContainer2.SplitterDistance = 340;
            this.splitContainer2.TabIndex = 0;
            // 
            // tblLizards
            // 
            this.tblLizards.AutoScroll = true;
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
            this.tblLizards.Size = new System.Drawing.Size(645, 340);
            this.tblLizards.TabIndex = 0;
            // 
            // txtDebugLog
            // 
            this.txtDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugLog.Location = new System.Drawing.Point(0, 0);
            this.txtDebugLog.Multiline = true;
            this.txtDebugLog.Name = "txtDebugLog";
            this.txtDebugLog.Size = new System.Drawing.Size(127, 202);
            this.txtDebugLog.TabIndex = 7;
            // 
            // btnHold
            // 
            this.btnHold.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnHold.Location = new System.Drawing.Point(0, 202);
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
            this.btnRamp.Location = new System.Drawing.Point(0, 234);
            this.btnRamp.Name = "btnRamp";
            this.btnRamp.Size = new System.Drawing.Size(127, 32);
            this.btnRamp.TabIndex = 5;
            this.btnRamp.Text = "Start Ramping Temp";
            this.btnRamp.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStop.Location = new System.Drawing.Point(0, 266);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(127, 32);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "End Experiment";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnForceStop
            // 
            this.btnForceStop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnForceStop.ForeColor = System.Drawing.Color.Maroon;
            this.btnForceStop.Location = new System.Drawing.Point(0, 298);
            this.btnForceStop.Name = "btnForceStop";
            this.btnForceStop.Size = new System.Drawing.Size(127, 32);
            this.btnForceStop.TabIndex = 3;
            this.btnForceStop.Text = "Force Stop";
            this.btnForceStop.UseVisualStyleBackColor = true;
            // 
            // btnReserved1
            // 
            this.btnReserved1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReserved1.Location = new System.Drawing.Point(0, 330);
            this.btnReserved1.Name = "btnReserved1";
            this.btnReserved1.Size = new System.Drawing.Size(127, 32);
            this.btnReserved1.TabIndex = 2;
            this.btnReserved1.Text = "???";
            this.btnReserved1.UseVisualStyleBackColor = true;
            // 
            // btnReserved2
            // 
            this.btnReserved2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReserved2.Location = new System.Drawing.Point(0, 362);
            this.btnReserved2.Name = "btnReserved2";
            this.btnReserved2.Size = new System.Drawing.Size(127, 32);
            this.btnReserved2.TabIndex = 1;
            this.btnReserved2.Text = "???";
            this.btnReserved2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Location = new System.Drawing.Point(0, 394);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Results";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblAmbientTemp
            // 
            this.lblAmbientTemp.Name = "lblAmbientTemp";
            this.lblAmbientTemp.Size = new System.Drawing.Size(118, 17);
            this.lblAmbientTemp.Text = "toolStripStatusLabel1";
            // 
            // gphAmbientTemp
            // 
            this.gphAmbientTemp.BackFillColor = System.Drawing.Color.Black;
            this.gphAmbientTemp.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gphAmbientTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gphAmbientTemp.HorizontalBarInterval = 13;
            this.gphAmbientTemp.LineColor = System.Drawing.Color.Red;
            this.gphAmbientTemp.Location = new System.Drawing.Point(0, 0);
            this.gphAmbientTemp.MaxDataPoints = 1000;
            this.gphAmbientTemp.Name = "gphAmbientTemp";
            this.gphAmbientTemp.Size = new System.Drawing.Size(645, 82);
            this.gphAmbientTemp.TabIndex = 0;
            this.gphAmbientTemp.Transparency = ((byte)(100));
            this.gphAmbientTemp.ValueInterval = 1;
            this.gphAmbientTemp.VerticalBarInterval = 13;
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
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private LineGraph.LinePainter gphAmbientTemp;
        private System.Windows.Forms.TableLayoutPanel tblLizards;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDebugLog;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnRamp;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnForceStop;
        private System.Windows.Forms.Button btnReserved1;
        private System.Windows.Forms.Button btnReserved2;
        private System.Windows.Forms.ToolStripStatusLabel lblAmbientTemp;
    }
}

