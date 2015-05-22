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
            this.txtNumLizards = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNumLizards
            // 
            this.lblNumLizards.AutoSize = true;
            this.lblNumLizards.Location = new System.Drawing.Point(12, 12);
            this.lblNumLizards.Name = "lblNumLizards";
            this.lblNumLizards.Size = new System.Drawing.Size(91, 13);
            this.lblNumLizards.TabIndex = 0;
            this.lblNumLizards.Text = "Number of lizards:";
            // 
            // txtNumLizards
            // 
            this.txtNumLizards.Location = new System.Drawing.Point(109, 9);
            this.txtNumLizards.Name = "txtNumLizards";
            this.txtNumLizards.Size = new System.Drawing.Size(107, 20);
            this.txtNumLizards.TabIndex = 0;
            this.txtNumLizards.Text = "5";
            this.txtNumLizards.Leave += new System.EventHandler(this.txtNumLizards_Leave);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 35);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(204, 32);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Begin Holding Start Temp";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // InitExperiment
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 79);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtNumLizards);
            this.Controls.Add(this.lblNumLizards);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InitExperiment";
            this.Text = "InitExperiment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InitExperiment_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumLizards;
        private System.Windows.Forms.TextBox txtNumLizards;
        private System.Windows.Forms.Button btnOk;
    }
}