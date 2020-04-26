using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
/*
9. Написать программу, которая иллюстрирует работу метода Шелла с одной из
формул вычисления шага сортировки:
г) 2 k  + 1;
*/
namespace Shell_Sort
{
    class VisualiseSort
    {
        public Bitmap bmp;
        Graphics graph;
        public SolidBrush wipeBrush;
        public SolidBrush columnBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public SolidBrush selectBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        int margin;
        int columnWidth;
        Random rand;

        int maxHeight;
        public int N = 1000;
        public double maxValue = 1000;
        public int[] mas;

        public VisualiseSort(PictureBox box)
        {
            bmp = new Bitmap(box.Width, box.Height);
            box.Image = bmp;
            graph = Graphics.FromImage(bmp);
            wipeBrush = new SolidBrush(box.BackColor);
            maxHeight = bmp.Height;

            rand = new Random();
            N = bmp.Width;
            columnWidth = CalcWidth();
            margin = CalcMargin();
            GenerateMas();
        }
        
        public void Clear()
        {
            graph.Clear(wipeBrush.Color);
        }

        public void DrawMas()
        {
            Clear();
            for (int i = 0; i < N; i++)
                DrawColumn(i);
        }

        public void DrawColumn(int index)
        {
            InnerDraw(index, columnBrush);
        }

        public void DrawSelectedColumn(int index)
        {
            InnerDraw(index, selectBrush);
        }

        public void WipeColumn(int index)
        {
            InnerDraw(index, wipeBrush);
        }

        public void InnerDraw(int index, Brush brush)
        {
            int height = CalcHeight(mas[index]);
            int position = CalcPosition(index);
            Rectangle column = new Rectangle(position, bmp.Height - height, columnWidth - margin, height);
            graph.FillRectangle(brush, column);
        }

        private void GenerateMas()
        {
            mas = new int[N];
            for (int i = 0; i < N; i++)
                mas[i] = rand.Next((int)maxValue);
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
