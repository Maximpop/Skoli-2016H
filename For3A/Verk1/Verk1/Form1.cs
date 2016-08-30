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

namespace Verk1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //lets the program make a random color for every ball 
        private static Random rand = new Random();
        private static KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
        private KnownColor randomColorName = names[rand.Next(names.Length)];


        private float maxNumBalls = 10;
        private int clickCount = 0;
        private List<Ball> balls = new List<Ball>();
        private List<Thread> running = new List<Thread>();
        Graphics graphics = BallPanel.CreateGraphics();
        

        private void BallPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (running.Count <= maxNumBalls)
            {
                switch (clickCount)
                {
                    case 0:
                        // generate a random color ball
                        Ball blueBall0 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 1:
                        // generate a random color ball
                        Ball blueBall1 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 2:
                        // generate a random color ball
                        Ball blueBall2 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 3:
                        // generate a random color ball
                        Ball blueBall3 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 4:
                        // generate a random color ball
                        Ball blueBall4 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 5:
                        // generate a random color ball
                        Ball blueBall5 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 6:
                        // generate a random color ball
                        Ball blueBall6 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 7:
                        // generate a random color ball
                        Ball blueBall7 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 8:
                        // generate a random color ball
                        Ball blueBall8 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);
                        break;
                    case 9:
                        // generate a random color ball
                        Ball blueBall9 = new Ball(e.X, e.Y, 10, Color.FromKnownColor(randomColorName),
                            rand.Next(1, 11), rand.Next(1, 11),
                            BallPanel.Size.Width, BallPanel.Size.Height);

                        graphics.FillEllipse(new SolidBrush(blueBall9.Color),
                        blueBall9.X - blueBall9.Radius, blueBall9.Y - blueBall9.Radius,
                        blueBall9.Radius * 2, blueBall9.Radius * 2);
                        break;

                    default:
                        break;
                }
            }
        }

        private Graphics graphic(object sender, PaintEventArgs e, Ball blueBall)
        {
            Graphics graphics = BallPanel.CreateGraphics();

                // draw the blue ball as an ellipse
            graphics.FillEllipse(new SolidBrush(blueBall.Color),
            blueBall.X - blueBall.Radius, blueBall.Y - blueBall.Radius,
            blueBall.Radius * 2, blueBall.Radius * 2);


            return graphics;
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
