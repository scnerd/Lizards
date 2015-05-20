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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.linePainter1 = new LineGraph.LinePainter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.linePainter1);
            this.splitContainer1.Size = new System.Drawing.Size(611, 133);
            this.splitContainer1.SplitterDistance = 284;
            this.splitContainer1.TabIndex = 0;
            // 
            // linePainter1
            // 
            this.linePainter1.BackFillColor = System.Drawing.Color.Black;
            this.linePainter1.BackLineColor = System.Drawing.Color.DarkGreen;
            this.linePainter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linePainter1.HorizontalBarInterval = 13;
            this.linePainter1.LineColor = System.Drawing.Color.LimeGreen;
            this.linePainter1.Location = new System.Drawing.Point(0, 0);
            this.linePainter1.Name = "linePainter1";
            this.linePainter1.Size = new System.Drawing.Size(323, 133);
            this.linePainter1.TabIndex = 0;
            this.linePainter1.Transparency = ((byte)(100));
            this.linePainter1.ValueInterval = 2;
            this.linePainter1.VerticalBarInterval = 13;
            // 
            // LizardMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "LizardMonitor";
            this.Size = new System.Drawing.Size(611, 133);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private LineGraph.LinePainter linePainter1;
    }
}
