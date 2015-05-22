using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace LineGraph
{
    public partial class LinePainter : UserControl
    {
        private List<double> Values;
        private Color _LineColor = Color.LightGreen;
        private Color _BackLineColor = Color.DarkGreen;
        private Color _BackFillColor = Color.Black;

        private byte _Transparency = 100;

        private int _ValueInterval = 2;
        private int _VerticalBarInterval = 13;
        private int _HorizontalBarInterval = 13;
        private Brush BackgroundPainter;
        private int _MaxDataPoints = 1000;

        private uint CurrentInterval = 0;

        public Color FillColor
        {
            get { return Color.FromArgb(Transparency, LineColor.R, LineColor.G, LineColor.B); }
        }

        private Image StoredImage;
        private Bitmap BackgroundGraphic;

        public Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; RedrawGraph(); }
        }

        public Color BackLineColor
        {
            get { return _BackLineColor; }
            set { _BackLineColor = value; RedrawBackground(); RedrawGraph(); }
        }

        public Color BackFillColor
        {
            get { return _BackFillColor; }
            set { _BackFillColor = value; RedrawBackground(); RedrawGraph(); }
        }

        public byte Transparency
        {
            get { return _Transparency; }
            set { _Transparency = value; RedrawGraph(); }
        }

        public int ValueInterval
        {
            get { return _ValueInterval; }
            set { _ValueInterval = Math.Max(value, 1); RedrawGraph(); }
        }

        public int VerticalBarInterval
        {
            get { return _VerticalBarInterval; }
            set { _VerticalBarInterval = Math.Max(value, 1); RedrawBackground(); RedrawGraph(); }
        }

        public int HorizontalBarInterval
        {
            get { return _HorizontalBarInterval; }
            set { _HorizontalBarInterval = Math.Max(value, 1); RedrawBackground(); RedrawGraph(); }
        }

        public int MaxDataPoints
        {
            get { return _MaxDataPoints; }
            set { _MaxDataPoints = value; }
        }

        private IEnumerable<Point> Path
        {
            get
            {
                var MaxVisible = Math.Min(Values.Count, (int)(this.Size.Width / ValueInterval) + 1);
                var Left = this.Size.Width - (MaxVisible-1)*ValueInterval;
                return
                    Values
                        .Skip(Math.Max(0, Values.Count - MaxVisible))
                        .Select(
                        (val, idx) =>
                        {
                            var height = this.Size.Height - 1;
                            return new Point(Left + idx*ValueInterval, (int)(height - height*val));
                        });
            }
        }

        public LinePainter()
        {
            Values = new List<double>();
            Clear(false);
            Values.Add(0.1); Values.Add(0.2); Values.Add(0.3); Values.Add(0.4); Values.Add(0.5); Values.Add(0.6); Values.Add(0.7); Values.Add(0.8); Values.Add(0.9);
            InitializeComponent();
            RedrawBackground();
            RedrawGraph();
        }

        public void RedrawGraph()
        {
            // Trim list to max length
            if (Values.Count > MaxDataPoints)
            {
                Values = Values.Skip((int)(Values.Count - MaxDataPoints)).ToList();
            }

            StoredImage = new Bitmap(this.Size.Width, this.Size.Height);
            using (var g = Graphics.FromImage(StoredImage))
            {
                // Draw background
                g.DrawImage(BackgroundGraphic, new Rectangle(
                    this.Size.Width + VerticalBarInterval - (int)(CurrentInterval * ValueInterval % VerticalBarInterval) - BackgroundGraphic.Width,
                    this.Size.Height - BackgroundGraphic.Height,
                    BackgroundGraphic.Width,
                    BackgroundGraphic.Height
                    ));
                // Draw fill of line
                var path = this.Path.ToArray();
                var poly = path.ToList();
                poly.Add(new Point(this.Size.Width, this.Size.Height));
                poly.Add(new Point(path[0].X, this.Size.Height));
                g.FillPolygon(new SolidBrush(FillColor), poly.ToArray());
                // Draw line
                g.DrawLines(new Pen(LineColor), path);
            }

            this.pictureBox1.Image = StoredImage;
            this.Refresh();
        }

        private void ResetBackgroundBrush()
        {
            var texture = new Bitmap(VerticalBarInterval, HorizontalBarInterval);
            using (var g = Graphics.FromImage(texture))
            {
                g.Clear(BackFillColor);
                var pen = new Pen(BackLineColor);
                g.DrawLine(pen, new Point(0, 0), new Point(0, HorizontalBarInterval - 1));
                g.DrawLine(pen, new Point(0, HorizontalBarInterval - 1), new Point(VerticalBarInterval - 1, HorizontalBarInterval - 1));
                BackgroundPainter = new TextureBrush(texture);
            }
        }

        private void RedrawBackground()
        {
            ResetBackgroundBrush();
            var TextureWidth = ((int)Math.Ceiling(this.Size.Width / (double)VerticalBarInterval) + 1) * VerticalBarInterval;
            var TextureHeight = (int) Math.Ceiling(this.Size.Height/(double) HorizontalBarInterval)*
                                HorizontalBarInterval;
            BackgroundGraphic = new Bitmap(TextureWidth, TextureHeight);
            using (var g = Graphics.FromImage(BackgroundGraphic))
            {
                g.FillRectangle(BackgroundPainter, 0, 0, TextureWidth, TextureHeight);
            }
        }

        private void LinePainter_SizeChanged(object sender, EventArgs e)
        {
            RedrawBackground();
            RedrawGraph();
        }

        public void Clear(bool Redraw=true)
        {
            CurrentInterval = 0;
            Values.Clear();
            Values.Add(0d);
            Values.Add(0d);
            if(Redraw) RedrawGraph();
        }

        public void Add(params double[] NewVals)
        {
            CurrentInterval = (uint)(CurrentInterval + NewVals.Length);
            Values.AddRange(NewVals.Select(v => Math.Min(Math.Max(v, 0d), 1d)));
            RedrawGraph();
        }

        private void LinePainter_Load(object sender, EventArgs e)
        {

        }
    }
}
