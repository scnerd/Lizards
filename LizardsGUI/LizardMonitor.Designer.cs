namespace LizardsGUI
{
    partial class LizardMonitor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sptOuter = new System.Windows.Forms.SplitContainer();
            this.gphTempGraph = new LineGraph.LinePainter();
            this.sptInner = new System.Windows.Forms.SplitContainer();
            this.tblEventButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnEvent2 = new System.Windows.Forms.Button();
            this.btnEvent1 = new System.Windows.Forms.Button();
            this.dataRecords = new System.Windows.Forms.DataGridView();
            this.btnNewNote = new System.Windows.Forms.Button();
            this.stpStatus = new System.Windows.Forms.StatusStrip();
            this.lblLizardName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentTemp = new System.Windows.Forms.ToolStripStatusLabel();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.sptOuter)).BeginInit();
            this.sptOuter.Panel1.SuspendLayout();
            this.sptOuter.Panel2.SuspendLayout();
            this.sptOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sptInner)).BeginInit();
            this.sptInner.Panel1.SuspendLayout();
            this.sptInner.Panel2.SuspendLayout();
            this.sptInner.SuspendLayout();
            this.tblEventButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataRecords)).BeginInit();
            this.stpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // sptOuter
            // 
            this.sptOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptOuter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sptOuter.Location = new System.Drawing.Point(0, 0);
            this.sptOuter.Name = "sptOuter";
            // 
            // sptOuter.Panel1
            // 
            this.sptOuter.Panel1.Controls.Add(this.gphTempGraph);
            // 
            // sptOuter.Panel2
            // 
            this.sptOuter.Panel2.Controls.Add(this.sptInner);
            this.sptOuter.Size = new System.Drawing.Size(611, 109);
            this.sptOuter.SplitterDistance = 220;
            this.sptOuter.TabIndex = 0;
            // 
            // gphTempGraph
            // 
            this.gphTempGraph.BackFillColor = System.Drawing.Color.Black;
            this.gphTempGraph.BackLineColor = System.Drawing.Color.DarkGreen;
            this.gphTempGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gphTempGraph.HorizontalBarInterval = 13;
            this.gphTempGraph.LineColor = System.Drawing.Color.LimeGreen;
            this.gphTempGraph.Location = new System.Drawing.Point(0, 0);
            this.gphTempGraph.MaxDataPoints = 1000;
            this.gphTempGraph.Name = "gphTempGraph";
            this.gphTempGraph.Size = new System.Drawing.Size(220, 109);
            this.gphTempGraph.TabIndex = 0;
            this.gphTempGraph.Transparency = ((byte)(100));
            this.gphTempGraph.ValueInterval = 2;
            this.gphTempGraph.VerticalBarInterval = 13;
            // 
            // sptInner
            // 
            this.sptInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sptInner.Location = new System.Drawing.Point(0, 0);
            this.sptInner.Name = "sptInner";
            // 
            // sptInner.Panel1
            // 
            this.sptInner.Panel1.Controls.Add(this.tblEventButtons);
            // 
            // sptInner.Panel2
            // 
            this.sptInner.Panel2.Controls.Add(this.dataRecords);
            this.sptInner.Panel2.Controls.Add(this.btnNewNote);
            this.sptInner.Size = new System.Drawing.Size(387, 109);
            this.sptInner.SplitterDistance = 118;
            this.sptInner.TabIndex = 0;
            // 
            // tblEventButtons
            // 
            this.tblEventButtons.ColumnCount = 1;
            this.tblEventButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEventButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEventButtons.Controls.Add(this.btnStop, 0, 2);
            this.tblEventButtons.Controls.Add(this.btnEvent2, 0, 1);
            this.tblEventButtons.Controls.Add(this.btnEvent1, 0, 0);
            this.tblEventButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEventButtons.Location = new System.Drawing.Point(0, 0);
            this.tblEventButtons.Name = "tblEventButtons";
            this.tblEventButtons.RowCount = 3;
            this.tblEventButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblEventButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblEventButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblEventButtons.Size = new System.Drawing.Size(118, 109);
            this.tblEventButtons.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(3, 75);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 31);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Event 3 (Stop)";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnEvent1_Click);
            // 
            // btnEvent2
            // 
            this.btnEvent2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEvent2.Location = new System.Drawing.Point(3, 39);
            this.btnEvent2.Name = "btnEvent2";
            this.btnEvent2.Size = new System.Drawing.Size(112, 30);
            this.btnEvent2.TabIndex = 1;
            this.btnEvent2.Text = "Event 2";
            this.btnEvent2.UseVisualStyleBackColor = true;
            this.btnEvent2.Click += new System.EventHandler(this.btnEvent1_Click);
            // 
            // btnEvent1
            // 
            this.btnEvent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEvent1.Location = new System.Drawing.Point(3, 3);
            this.btnEvent1.Name = "btnEvent1";
            this.btnEvent1.Size = new System.Drawing.Size(112, 30);
            this.btnEvent1.TabIndex = 0;
            this.btnEvent1.Text = "Event 1";
            this.btnEvent1.UseVisualStyleBackColor = true;
            this.btnEvent1.Click += new System.EventHandler(this.btnEvent1_Click);
            // 
            // dataRecords
            // 
            this.dataRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Timestamp,
            this.Note});
            this.dataRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRecords.Location = new System.Drawing.Point(0, 0);
            this.dataRecords.Name = "dataRecords";
            this.dataRecords.Size = new System.Drawing.Size(265, 73);
            this.dataRecords.TabIndex = 1;
            // 
            // btnNewNote
            // 
            this.btnNewNote.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnNewNote.Location = new System.Drawing.Point(0, 73);
            this.btnNewNote.Name = "btnNewNote";
            this.btnNewNote.Size = new System.Drawing.Size(265, 36);
            this.btnNewNote.TabIndex = 0;
            this.btnNewNote.Text = "Make New Note";
            this.btnNewNote.UseVisualStyleBackColor = true;
            // 
            // stpStatus
            // 
            this.stpStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLizardName,
            this.lblCurrentTemp});
            this.stpStatus.Location = new System.Drawing.Point(0, 109);
            this.stpStatus.Name = "stpStatus";
            this.stpStatus.Size = new System.Drawing.Size(611, 24);
            this.stpStatus.TabIndex = 1;
            this.stpStatus.Text = "statusStrip1";
            // 
            // lblLizardName
            // 
            this.lblLizardName.Name = "lblLizardName";
            this.lblLizardName.Size = new System.Drawing.Size(118, 19);
            this.lblLizardName.Text = "toolStripStatusLabel1";
            this.lblLizardName.ToolTipText = "The name of this lizard";
            // 
            // lblCurrentTemp
            // 
            this.lblCurrentTemp.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblCurrentTemp.Name = "lblCurrentTemp";
            this.lblCurrentTemp.Size = new System.Drawing.Size(122, 19);
            this.lblCurrentTemp.Text = "toolStripStatusLabel1";
            this.lblCurrentTemp.ToolTipText = "The lizard\'s current temperature";
            // 
            // Timestamp
            // 
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            // 
            // LizardMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sptOuter);
            this.Controls.Add(this.stpStatus);
            this.Name = "LizardMonitor";
            this.Size = new System.Drawing.Size(611, 133);
            this.sptOuter.Panel1.ResumeLayout(false);
            this.sptOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptOuter)).EndInit();
            this.sptOuter.ResumeLayout(false);
            this.sptInner.Panel1.ResumeLayout(false);
            this.sptInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sptInner)).EndInit();
            this.sptInner.ResumeLayout(false);
            this.tblEventButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataRecords)).EndInit();
            this.stpStatus.ResumeLayout(false);
            this.stpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer sptOuter;
        private LineGraph.LinePainter gphTempGraph;
        private System.Windows.Forms.SplitContainer sptInner;
        private System.Windows.Forms.TableLayoutPanel tblEventButtons;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnEvent2;
        private System.Windows.Forms.Button btnEvent1;
        private System.Windows.Forms.Button btnNewNote;
        private System.Windows.Forms.DataGridView dataRecords;
        private System.Windows.Forms.StatusStrip stpStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblLizardName;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
    }
}
