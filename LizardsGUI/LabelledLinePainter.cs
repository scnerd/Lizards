using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LineGraph
{
    public partial class LabelledLinePainter : LinePainter
    {
        protected GroupBox Border;

        public string Text
        {
            get { return Border.Text; }
            set { Border.Text = value; }
        }

        public LabelledLinePainter()
            : base()
        {
            InitializeComponent();

            this.SuspendLayout();
            Border = new GroupBox();
            this.Controls.Remove(picDisplay);
            Border.Controls.Add(picDisplay);
            picDisplay.Dock = DockStyle.Fill;
            this.Controls.Add(Border);

            Border.Dock = DockStyle.Fill;

            this.ResumeLayout();
        }
    }
}
