using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Verk1
{
    public partial class Form1 : Form
    {
        List<Ball> balls = new List<Ball>();
        private Random generator = new Random();
        public int counter = 0;
        // Random litir sem hægt er að velja úr
        Color[] colors = new Color[10] { Color.Blue, Color.Red, Color.Tomato, Color.Yellow, Color.HotPink, Color.Purple, Color.IndianRed, Color.LimeGreen, Color.SteelBlue, Color.Gold };

        public Form1()
        {
            InitializeComponent();
            Thread painter = new Thread(new ThreadStart(paint));
            painter.Start();
        }
        private void checkCollision()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int a = 0; a < balls.Count; a++)
                {
                    if (!(balls.Count == 1))
                    {
                        if (a == i)
                        {
                            a++;
                            
                        }
                        if (a > (balls.Count-1))
                        {
                            break;
                        }
                        if (balls[a].X + balls[a].Radius + balls[i].Radius > balls[i].X && balls[a].X < balls[i].X + balls[a].Radius + balls[i].Radius && balls[a].Y + balls[a].Radius + balls[i].Radius > balls[i].Y && balls[a].Y < balls[i].Y + balls[a].Radius + balls[i].Radius)
                        {
                            if (Math.Sqrt(((balls[a].X - balls[i].X) * (balls[a].X - balls[i].X)) + ((balls[a].Y - balls[i].Y) * (balls[a].Y - balls[i].Y))) < balls[a].Radius + balls[i].Radius)//ef punkturinn sem er lagst eda hast a bolta A er inni bolta i
                            {
                                //tegar hingad er komid er stadfest ad tad hefur veird collision
                                //herna er reiknad hvadan teri koma, hvert teir eiga ad fara og teir eru sendir tangad

                                float newVelX1 = (balls[a].Dx * (balls[a].Mass - balls[i].Mass) + (2 * balls[i].Mass * balls[i].Dx)) / (balls[a].Mass + balls[i].Mass);
                                float newVelY1 = (balls[a].Dy * (balls[a].Mass - balls[i].Mass) + (2 * balls[i].Mass * balls[i].Dy)) / (balls[a].Mass + balls[i].Mass);
                                float newVelX2 = (balls[i].Dx * (balls[i].Mass - balls[a].Mass) + (2 * balls[a].Mass * balls[a].Dx)) / (balls[a].Mass + balls[i].Mass);
                                float newVelY2 = (balls[i].Dy * (balls[i].Mass - balls[a].Mass) + (2 * balls[a].Mass * balls[a].Dy)) / (balls[a].Mass + balls[i].Mass);

                                balls[a].Dx = newVelX1;
                                balls[a].Dy = newVelY1;
                                balls[i].Dx = newVelX2;
                                balls[i].Dy = newVelY2;

                                Thread.Sleep(10);
                            }
                        }
                    }
                   
                }
            }
        }
        private void paint()
        {
            while (true)
            {
                Thread.Sleep(16);
                /*
                * Faster than BallPanel.Invalidate();
                */
                checkCollision();
                this.Invoke((MethodInvoker)delegate
                {
                    this.Refresh();
                    
                });
            }
        }
        private void addBalls(int amount, MouseEventArgs e)
        {
                // Býr til bolta og bætir honum í listann til að teikna
                // Boltinn byrjar þar sem músinn er og hefur random hraða, lit og stærð
                // Svo byrjar boltinn að hreyfast samkvæmt hinum þráðinum
                Ball ball = new Ball(e.X, e.Y, generator.Next(30, 60), colors[generator.Next(0, 10)], generator.Next(2, 20), generator.Next(2, 20), BallPanel.Size.Width, BallPanel.Size.Height);
                balls.Add(ball);

                Thread baller = new Thread(new ThreadStart(ball.Run));
                baller.Start();
                counter++; // Bolta teljari
        }

        private void BallPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (counter != 10)
            {
                Thread ballAdder = new Thread(() => addBalls((int)1, e));
                ballAdder.Start();
            } 
        }

        private void BallPanel_Paint(object sender, PaintEventArgs e)
        {
            // Virkar bara þegar það eru til boltar
            Graphics gr = e.Graphics;
            foreach (var ball in balls)
            {
                SolidBrush sb = new SolidBrush(ball.Color);
                gr.FillEllipse(sb, ball.X, ball.Y, ball.Radius, ball.Radius);
            }
        }

        // delegate to call BallPanel.Refresh() through Invoke()
        private delegate void RefreshDelegate();

        private void Repainter()
        {
            while (true)
            {
                Thread.Sleep(20); //sleep for 20 milliseconds

                // redraw the containing Panel
                Invoke(new RefreshDelegate(BallPanel.Refresh));
            } // end while
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread repaint = new Thread(new ThreadStart(Repainter));
            repaint.Start();
        }


        //Terminate all threads associated with this applications
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        
        
    }
}
