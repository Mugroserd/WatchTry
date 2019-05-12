using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatchTry
{
    public partial class Form1 : Form
    {
        private SolidBrush backgroundBrush = new SolidBrush(Color.White); // Кисть фона
        private SolidBrush segmentBrush = new SolidBrush(Color.White); // Кисть сегмента
        const int SEGMENTAMOUNT = 12;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw(bmp);
            pictureBox1.Image = bmp;
            button1.Text = Convert.ToString(DateTime.Now.TimeOfDay.Seconds);
        }

        public void Draw(Bitmap bmp)
        {
            int boxWidth = bmp.Width;
            int boxHeight = bmp.Height;
            Random rand = new Random();
            Rectangle rect = new Rectangle(0, 0, boxWidth, boxHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(backgroundBrush, rect);

                for (int segmentArrayNum = 0; segmentArrayNum < SEGMENTAMOUNT; segmentArrayNum++)
                {
                    float step = 3 * (float)Math.PI / 2 + 2 * (float)Math.PI * segmentArrayNum / SEGMENTAMOUNT; // Шаг сегмента
                    float nextStep = 3 * (float)Math.PI / 2 + 2 * (float)Math.PI * (segmentArrayNum + 1) / SEGMENTAMOUNT; // Расчёт следующей точки сегмента
                    double stepCos = Math.Cos(step);
                    double stepSin = Math.Sin(step);
                    double nextstepCos = Math.Cos(nextStep);
                    double nextstepSin = Math.Sin(nextStep);

                    for (int segmentNum = 0; segmentNum < 5; segmentNum++)
                    {
                        int innerSide = segmentNum * boxHeight / 12;
                        int outerSide = segmentNum * boxHeight / 12 + boxWidth / 12;

                        Color customColor = Color.FromArgb(128, 128, 128);
                        if (DateTime.Now.TimeOfDay.Seconds >= 5 * segmentArrayNum + segmentNum) {
                            customColor = Color.FromArgb(10, 250, 10);
                        }

                        segmentBrush.Color = customColor;

                        PointF innCirc1 = new PointF(boxHeight / 2 + Convert.ToInt32(innerSide * stepCos),
                                                     boxHeight / 2 + Convert.ToInt32(innerSide * stepSin));
                        PointF innCirc2 = new PointF(boxHeight / 2 + Convert.ToInt32(innerSide * nextstepCos),
                                                     boxHeight / 2 + Convert.ToInt32(innerSide * nextstepSin));

                        PointF outCirc1 = new PointF(boxHeight / 2 + Convert.ToInt32(outerSide * stepCos),
                                                     boxHeight / 2 + Convert.ToInt32(outerSide * stepSin));
                        PointF outCirc2 = new PointF(boxHeight / 2 + Convert.ToInt32(outerSide * nextstepCos),
                                                     boxHeight / 2 + Convert.ToInt32(outerSide * nextstepSin));
                        PointF[] curvePoints = { innCirc2, innCirc1, outCirc1, outCirc2 };
                        
                        g.FillPolygon(segmentBrush, curvePoints);
                    }
                }

            }
        }
    }
}
