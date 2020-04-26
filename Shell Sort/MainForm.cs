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
        int delay = 50;
        int[] mas;

        public MainForm()
        {
            InitializeComponent();
            sort = new VisualiseSort(PictureBox);
            mas = sort.mas;
            sort.DrawMas();
            PictureBox.Image = sort.bmp;

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Enabled = false;

            Thread sorting = new Thread(new ThreadStart(StartSort));
            sorting.Start();
        }

        public void StartSort()
        {
            for (int step = MaxStep(); step > 0; step /= 2)
            {
                for (int i = step; i < sort.N; i++)
                {
                    bool ok = true;
                    int j = i - step;
                    while (ok && (j >= 0))
                    {
                        sort.DrawSelectedColumn(i);
                        sort.DrawSelectedColumn(j);
                        PictureBox.Image = sort.bmp;
                        Thread.Sleep(delay);

                        ok = (mas[j] > mas[i]);
                        if (ok)
                        {
                            int tmp = mas[j];

                            sort.WipeColumn(i);
                            sort.WipeColumn(j);
                            PictureBox.Image = sort.bmp;

                            mas[j] = mas[i];
                            sort.DrawColumn(j);
                            PictureBox.Image = sort.bmp;

                            mas[i] = tmp;
                            sort.DrawColumn(i);
                            PictureBox.Image = sort.bmp;

                            Thread.Sleep(delay);
                            j -= step;
                        }
                        else
                        {
                            sort.DrawColumn(i);
                            sort.DrawColumn(j);
                            PictureBox.Image = sort.bmp;
                        }
                    }
                }
            }
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Enabled = true;
            PictureBox.Image = sort.bmp;
        }

        private int MaxStep()
        {
            int result = 1;
            while (result + 1 < sort.N)
                result *= 2;
            return result / 2 / 2;
        }
    }
}
