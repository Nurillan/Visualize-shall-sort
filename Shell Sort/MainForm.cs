using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shell_Sort
{
    public partial class MainForm : Form
    {
        VisualiseSort sort;
        int masN = 150;
        int delay = 3;

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            sort = new VisualiseSort(PictureBox, PictureBox.Width, delay);
            sort.DrawMas();
            PictureBox.Image = sort.bmp;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Start));
            thread.Start();
        }

        private void Start()
        {
            sort.StartSort(PictureBox);
        }

        private void newMasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort.GenerateMas();
            sort.DrawMas();
            PictureBox.Image = sort.bmp;
        }
    }
}
