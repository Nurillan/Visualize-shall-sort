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
        int delay = 300;//задержка при рисовании
        int masN;//кол-во эллементов в массиве
        Thread thread;

        public MainForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            masN = 20;

            sort = new VisualiseSort(PictureBox, masN, delay);
            sort.DrawMas();
            thread = new Thread(new ThreadStart(Start));
            timer1.Tick += new EventHandler(Update);
            timer1.Interval = delay;
            timer1.Start();
        }

        private void Update(Object myObject, EventArgs myEventArgs)
        {
            PictureBox.Image = sort.bmp;
            if ((bool)thread?.IsAlive)
            {
                startToolStripMenuItem.Enabled = true;
                newMasToolStripMenuItem.Enabled = true;
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startToolStripMenuItem.Enabled = false;
            newMasToolStripMenuItem.Enabled = false;
            thread = new Thread(new ThreadStart(Start));
            thread.Start();
        }

        private void Start()
        {
            sort.StartSort();
        }

        private void newMasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort.GenerateMas();
            sort.DrawMas();
            PictureBox.Image = sort.bmp;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((bool)thread?.IsAlive)
            {
                thread.Abort();
            }
            sort.bmp.Dispose();
            sort = null;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)thread?.IsAlive)
            {
                thread.Abort();
            }
        }
    }
}
