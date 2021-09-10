using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using static System.Threading.Timer;
using System.Timers;
using System.IO.Ports;

namespace HandMovement
{
    public partial class Form1 : Form
    {
        int timeElapsed = 0;

        SerialPort port;


        public Form1()
        {
            InitializeComponent();
            timer1.Start();

            initServo();

        }

        private void initServo()
        {
            port = new SerialPort();
            port.PortName = "COM5";
            port.BaudRate = 9600;


            try
            {
                port.Open();
            }
            catch(Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed += timer1.Interval;
            //Console.WriteLine(timeElapsed);
        }



        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Setup the camera device 
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboCamera.Items.Add(filterInfo.Name);
            cboCamera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
            
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }


        //Start the video feed to the picture box
        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        //Stop the video feed
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
            pic.Image = new Bitmap(100, 100);

        }

        //Video new frame function
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
                Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
                if(timeElapsed%100==0)
                    screenAnalyzer(bmp);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        


        /* private void screenAnalyzer(Bitmap bmp)
         {
             bool firstInstancex = false;
             bool lastInstancex = false;
             System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
             int objectHeight=0;
             int objectWidth=0;
             int leftSideBuffer=0;
             int spacesBetweenChecks = 5;
             Point BR = new Point(0, 0);
             Point LU = new Point(0, 0);




             for (int i = 0; i < bmp.Height; i+=spacesBetweenChecks)
                 {
                     for (int j = 0; j < bmp.Width; j+=spacesBetweenChecks)
                     {
                         if (bmp.GetPixel(j, i).R >200 || bmp.GetPixel(j, i).G > 200 || bmp.GetPixel(j, i).B > 200)
                         {
                             if (firstInstancex == false)
                             {

                                LU = new Point(j, i);
                                 firstInstancex = true;
                             }

                             if (j > objectWidth)
                             {
                                 BR.X = j;
                             }

                         if (i > objectHeight)
                             {
                                 BR.Y = i;
                             }

                                 bmp.SetPixel(j, i, Color.Red);
                         }

                     else
                             {
                                 leftSideBuffer++;
                             }
                     }
                 }

             gr.DrawLine(new Pen(Brushes.Purple, 10), new Point(LU.X,LU.Y), new Point(BR.X, LU.Y));
             gr.DrawLine(new Pen(Brushes.Purple, 10), new Point(LU.X, LU.Y), new Point(LU.X, BR.Y));
             gr.DrawLine(new Pen(Brushes.Purple, 10), new Point(BR.X, LU.Y), new Point(BR.X, BR.Y));
             gr.DrawLine(new Pen(Brushes.Purple, 10), new Point(LU.X, BR.Y), new Point(BR.X, BR.Y));



             objectWidth = LU.X - BR.X;
             objectHeight = LU.Y - BR.Y;

             Center = new Point(LU.X+(objectWidth/2),LU.Y+(objectHeight/2));

             int centerRectangle = 10;



             pic.Image = bmp;

             }*/


        Point previousPoint;
        Point currentPoint;

        private void screenAnalyzer(Bitmap bmp)
        {

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            int spacesBetweenChecks = 5;
            bool firstInstanceX = false;
            bool firstInstanceY = false;
            int firstX = 0;
            int firstY = 0;





            for (int j = 0; j < bmp.Height; j += spacesBetweenChecks)
            {
                for (int i = 0; i < bmp.Width; i += spacesBetweenChecks)
                {
                    if (bmp.GetPixel(i, j).R > 180 || bmp.GetPixel(i, j).G > 180 || bmp.GetPixel(i, j).B > 180)
                    {
                        if (firstInstanceX == false)
                        {
                            firstX = i;
                            firstInstanceX = true;
                           
                            if (firstInstanceY == false)
                            {
                                firstY = j;
                                firstInstanceY = true;

                                currentPoint = new Point(i, j);
                            }
                        }

                       

                        
                        bmp.SetPixel(i, j, Color.Red);
                    }


                }
            }

            
            
            gr.DrawLine(new Pen(Brushes.Purple, 10), new Point(0,0), new Point(firstX,firstY));
            movementAnalyzer(previousPoint, currentPoint, bmp);
            pic.Image = bmp;

            previousPoint = currentPoint;
        }


        public void movementAnalyzer(Point previousPoint, Point currentPoint, Bitmap bmp)
        {

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            bool upDirection=false;
            bool rightDirection=false;

            int screenWidth = bmp.Width;
            int screenHeight = bmp.Height;

            

            float xDistance;
            float yDistance;

            xDistance = currentPoint.X - previousPoint.X;
            yDistance = currentPoint.Y - previousPoint.Y;

            if (xDistance < 0)
            {
                rightDirection = false;
                Console.WriteLine("Left");           }
            else if (xDistance > 0)
            {
                rightDirection = true;
                Console.WriteLine("Right");
            }

            if (yDistance < 0)
            {
                upDirection = true;
                Console.WriteLine("Up");
            }
            else if (yDistance > 0) 
            {
                upDirection = false;
                Console.WriteLine("Down");
            }

            //Console.WriteLine(screenHeight + " " + screenWidth);


            //Console.WriteLine("Previous point: " +previousPoint.X.ToString() + ", " + previousPoint.Y.ToString());
            //Console.WriteLine("CurrentPoint: " + currentPoint.X.ToString() + ", " + currentPoint.Y.ToString());
            //Console.WriteLine(xDistance.ToString(".0#")+ " " + yDistance.ToString(".0#"));

            float xMovementFactor = xDistance / screenWidth;
            float yMovementFactor = yDistance / screenHeight;

            Console.WriteLine(xMovementFactor.ToString()+" AND " + yMovementFactor.ToString());

            float positionX = (currentPoint.X * 180) / screenWidth;


            gr.DrawLine(new Pen(Brushes.Red, 10), previousPoint, currentPoint);

            if (port.IsOpen)
            {
                port.WriteLine(positionX.ToString());
            }
        }

        
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                port.WriteLine(0.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                port.WriteLine(155.ToString());
            }
        }
    } 
        
    }

