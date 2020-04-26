using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
/*
9. Написать программу, которая иллюстрирует работу метода Шелла с одной из
формул вычисления шага сортировки:
г) 2 k  + 1;
*/
namespace Shell_Sort
{
    class VisualiseSort
    {
        public Bitmap bmp { get; }
        Graphics graph;
        SolidBrush wipeBrush;
        SolidBrush columnBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        SolidBrush selectBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        Random rand;
        int margin;
        int columnWidth;
        int delay;
        int maxHeight;
        double maxValue = 1000;
        public int N { get; }
        public int[] mas;

        public VisualiseSort(PictureBox box, int N = 1000, int delay = 50)
        {
            bmp = new Bitmap(box.Width, box.Height);
            graph = Graphics.FromImage(bmp);
            wipeBrush = new SolidBrush(box.BackColor);
            this.N = N;
            this.delay = delay;
            maxHeight = bmp.Height;
            columnWidth = CalcWidth();
            rand = new Random();
            margin = CalcMargin();
            GenerateMas();
        }

        public void StartSort(PictureBox Box)
        {
            for (int step = MaxStep(); step > 0; step = NextStep(step))
            {
                for (int i = step; i < N; i++)
                {
                    bool ok = true;
                    int j = i - step;
                    while (ok && (j >= 0))
                    {
                        Comparison(j + step, j);
                        Box.Image = bmp;
                        Thread.Sleep(delay);

                        ok = (mas[j] > mas[j + step]);
                        if (ok)
                        {
                            int tmp = mas[j];

                            WipeColumns(j + step, j);

                            mas[j] = mas[j + step];
                            mas[j + step] = tmp;

                            DrawColumns(j, j + step);
                            Box.Image = bmp;
                            Thread.Sleep(delay);

                            j -= step;
                        }
                        else
                        {
                            DrawColumns(j + step, j);
                            Box.Image = bmp;
                            Thread.Sleep(delay);
                        }
                    }
                }
            }
        }

        private int MaxStep()
        {
            int result = 1;
            while (result + 1 < N)
                result *= 2;
            return result / 2 + 1;
        }

        private int NextStep(int step)
        {
            if (step <= 3)
                return step - 2;
            return step / 2 + 1;
        }

        public void Clear()
        {
            graph.Clear(wipeBrush.Color);
        }

        public void GenerateMas()
        {
            mas = new int[N];
            for (int i = 0; i < N; i++)
                mas[i] = rand.Next((int)maxValue);
        }

        public void DrawMas()
        {
            Clear();
            for (int i = 0; i < N; i++)
                DrawColumn(i);
        }

        public void Comparison(int index1, int index2)
        {
            DrawSelectedColumn(index1);
            DrawSelectedColumn(index2);
        }

        public void WipeColumns(int index1, int index2)
        {
            WipeColumn(index1);
            WipeColumn(index2);
        }

        public void DrawColumns(int index1, int index2)
        {
            DrawColumn(index1);
            DrawColumn(index2);
        }

        private void DrawColumn(int index)
        {
            InnerDraw(index, columnBrush);
        }

        private void DrawSelectedColumn(int index)
        {
            InnerDraw(index, selectBrush);
        }

        private void WipeColumn(int index)
        {
            InnerDraw(index, wipeBrush);
        }

        private void InnerDraw(int index, Brush brush)
        {
            int height = CalcHeight(mas[index]);
            int position = CalcPosition(index);
            Rectangle column = new Rectangle(position, maxHeight - height, columnWidth - margin, height);
            graph.FillRectangle(brush, column);
        }

        private int CalcPosition(int index)
        {
            return columnWidth * index;
        }

        private int CalcWidth()
        {
            int res = bmp.Width / N;
            return res < 1 ? 1 : res;
        }

        private int CalcHeight(int value)
        {
            double result = value / maxValue * maxHeight;
            return (int)Math.Round(result);
        }

        private int CalcMargin()
        {
            int result = 0;
            if(columnWidth >= 3)
                result = (int)Math.Ceiling(columnWidth * 0.1);
            return result;                
        }
    }
}
