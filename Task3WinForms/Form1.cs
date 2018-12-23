using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDrawing;
using MyDrawing.VisualObjects;

namespace Task3WinForms
{
    public sealed partial class Form1 : Form
    {
        private Bitmap bmp;
        private bool _isDown = false;
        private Line _l1 = new Line(new List<PointF>(), Color.Blue, 2);
        private Line _l2 = new Line(new List<PointF>(), Color.DarkRed, 2);
        private Line _lineToUse;
        private bool _newLine = false;
        private bool _closeLine = false;
        private readonly Drawer _drawer;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(Field.Width, Field.Height);            
            _drawer = new Drawer(Field, bmp, DrawType.Standart);
        }

        private void NewLineChB_CheckedChanged(object sender, EventArgs e)
        {
            _newLine = NewLineChB.Checked;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
               _l1.NormalizeWithNewLine(_l2);
               _drawer.MoveObject(_l1, _l2, _closeLine);   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Field_MouseUp(object sender, MouseEventArgs e)
        {
            _isDown = false;
        }

        private void Field_MouseDown(object sender, MouseEventArgs e)
        {
            var line = NewLineChB.Checked ? _l2 : _l1;
            line.Points.Add(e.Location);
            _drawer.Clear();
            _drawer.DrawPoint(e.Location, Color.Red);
            _drawer.Draw(_l1);
            _drawer.Draw(_l2);
        }

        private void Field_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            _drawer.Clear();
            _l1 = new Line(new List<PointF>(), Color.Blue, 2);
            _l2 = new Line(new List<PointF>(), Color.DarkRed, 2);
        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _drawer.Clear();
        }

        private void CloseLine_CheckedChanged(object sender, EventArgs e)
        {
            _closeLine = CloseLine.Checked;
            _l2.Points.Add(_l2.Points[0]);
            _drawer.Draw(_l2);
        }
    }
}
