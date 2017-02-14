//#define POLLING

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;


namespace IPC2
{
    public partial class FormMain : Form
    {
#if _WIN64
        const Int32 OS = 64;
#else
        const Int32 OS = 32;
#endif
        public static readonly Int32 S_OK = 0;
        public static readonly Int32 S_FALSE = 1;

        // holds the information for the facedetection
        private CascadeClassifier face_cascade;

        // variables for holding the facedetection rectangle, points/position (x,y,height,width)
        private int x;
        private int y;

        // variables for holding the forehead rectangle points/position (x,y,height,width)
        private int x1;
        private int y1;
        private ushort xForehead;
        private ushort yForehead;

        // variables for holding the Peri-orbital rectangle points/position (x,y,height,width)
        private int xpo;
        private int ypo;
        private ushort xOrbital;
        private ushort yOrbital;

        // variables for holding the nasal rectangle, points/position (x,y,height,width)
        private int x2;
        private int y2;
        private ushort xNasal;
        private ushort yNasal;

        // variables for holding the Cheek rectangle, points/position (x,y,height,width)
        private int xck;
        private int yck;
        private ushort xCheek;
        private ushort yCheek;

        // variables for holding the Maxillary rectangle, points/position (x,y,height,width)
        private int xm;
        private int ym;
        private ushort xMaxillary;
        private ushort yMaxillary;

        // variables for holding the Chin rectangle, points/position (x,y,height,width)
        private int xcn;
        private int ycn;
        private ushort xChin;
        private ushort yChin;

        // the temperature values 
        double foreheadTemperature;
        double orbitalTemperature;
        double nasalTemperature;
        double cheekTemperature;
        double maxillaryTemperature;
        double chinTemperature;

        // stores the x and y coordinates of the forehead location
        Point forehead;

        // stores the x and y coordinates of the nasal location
        Point nasal;

        // stores temperatures
        List<double> foreheadTemperatures = new List<double>();
        List<double> orbitalTemperatures = new List<double>();
        List<double> nasalTemperatures = new List<double>();
        List<double> cheekTemperatures = new List<double>();
        List<double> maxillaryTemperatures = new List<double>();
        List<double> chinTemperatures = new List<double>();



        // the thread for the real time display of forehead and nasal temperatures using a chart
        private Thread temperatureThread;

        // remembers the current time of the live view in seconds, in 2 second steps
        private int currentIndex = 1;

        // sets the flag which shows that no face has been detected so no forehead and nasal temperature can be stored
        // serves the correction of the data processing, this flagged position in the table has the value null
        private bool noFaceDetected;

        // hinders the repeated button push which would otherwise start a new thread which terminates the app
        private bool chartAlreadyStarted;

        // needed to resume the thread in the start chart button to distinguish between first start and resumed start
        private bool chartStopped;

        // makes sure that the new Participant button isn't pushed twice in a row accidently
        private bool newParticipantAlreadyGenerated;

        // holds the conditions for the experiment
        private List<string> conditionList = new List<string>();

        // holds the tasks of the Videos condition for the experiment
        private List<string> taskAudios = new List<string>();

        // holds the tasks of the Reading Text Condition for the experiment
        private List<string> taskReadingText = new List<string>();

        // holds the tasks of the Stroop condition for the experiment
        private List<string> taskStroop = new List<string>();

        // holds the tasks of the Stroop condition for the experiment
        private List<string> taskReadingAudio = new List<string>();

        // guarantuees that the conditions are independent variables
        private static Random random = new Random();

        // the ID of the participant
        private string participantID;

        // counts the tasks, since there are several different generated temperatures for each of the 12 tasks
        private int taskCountForTemperatureLists;

        // guarantuees randomizing tasks only once for each condition when new participant generated
        private bool randomizingTasksPermitted;

        // essential variables for the timer countdown which starts at 3 minutes representing the task duration
        private int minutes;
        private int seconds;

        // has all tasks for the saving as a text file after each task
        private List<string> allTaskList = new List<string>();

        // serves for the selection of an element in the list which holds all tasks
        private int indexForAllTaskList;

        // permits adding tasks to the list which holds all tasks of all conditions for the text file export after each completed task
        private bool addingTasksToListPermitted;

        // guarantuees that the refresh button can not be pushed when the task is completed more precisely when the countdown changed from 3:0 to 0:0
        private bool taskCompleted;

        // permits the export of the listview2 content as a text file
        private bool showListView2;

        // permits the export of the listview1 content as a csv file
        private bool showListView1;

        #region EyeX Variables

        // toggle for logging gaze
        //private bool logGaze;
        //private static FormsEyeXHost _eyeXHost;
        //private CsvExport gazeExport;

        #endregion


        public System.Version Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; }
        }

        Int32 FrameWidth, FrameHeight, FrameDepth, FrameSize;
        public IPC2 ipc;
        bool ipcInitialized = false, frameInitialized = false;
        Bitmap bmp;
        bool Connected = false;
        bool Colors = false;


        byte[] rgbValues;
        Int16[] Values;

        Int32 MainTimerDivider;
        bool Painted = false;

        public void FindLargestContour(/*IInputOutputArray cannyEdges, IInputOutputArray result*/)
        {
            Image<Bgr, Byte> imageFrame1 = new Image<Bgr, Byte>(bmp);
            //CvInvoke.Imshow("SensorFusion", imageFrame1); //Show the image 

            Image<Gray, byte> grayFrame1 = imageFrame1.Convert<Gray, byte>();
            Image<Gray, byte> countourOutput = imageFrame1.Convert<Gray, byte>();
            Image<Gray, byte> result = imageFrame1.Convert<Gray, byte>();

            int largest_contour_index = 0;
            double largest_area = 0;
            VectorOfPoint largestContour;

            using (Mat hierachy = new Mat())
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                IOutputArray hirarchy;

                CvInvoke.FindContours(grayFrame1, contours, hierachy, RetrType.Tree, ChainApproxMethod.ChainApproxNone);

                for (int i = 0; i < contours.Size; i++)
                {
                    MCvScalar color = new MCvScalar(0, 0, 255);

                    double a = CvInvoke.ContourArea(contours[i], false);  //  Find the area of contour
                    if (a > largest_area)
                    {
                        largest_area = a;
                        largest_contour_index = i;                //Store the index of largest contour
                    }

                    CvInvoke.DrawContours(result, contours, largest_contour_index, new MCvScalar(255, 0, 0));
                }

                CvInvoke.DrawContours(result, contours, largest_contour_index, new MCvScalar(0, 255, 0), 10, LineType.EightConnected, hierachy);
                //largestContour = new VectorOfPoint(contours[largest_contour_index].ToArray());
            }

            //return largestContour;
            //CvInvoke.Imshow("SensorFusion", imageFrame1); //Show the image
            //CvInvoke.Imshow("SensorFusion1", result); //Show the image
            //CvInvoke.Imshow("SensorFusion2", countourOutput); //Show the image
            //CvInvoke.WaitKey(3);  //Wait for the key pressing event
        }
        /*
        public static void FindLargestContour(IInputOutputArray cannyEdges, IInputOutputArray result)
        {
            int largest_contour_index = 0;
            double largest_area = 0;
            VectorOfPoint largestContour;

            using (Mat hierachy = new Mat())
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                IOutputArray hirarchy;

                CvInvoke.FindContours(cannyEdges, contours, hierachy, RetrType.Tree, ChainApproxMethod.ChainApproxNone);

                for (int i = 0; i < contours.Size; i++)
                {
                    MCvScalar color = new MCvScalar(0, 0, 255);

                    double a = CvInvoke.ContourArea(contours[i], false);  //  Find the area of contour
                    if (a > largest_area)
                    {
                        largest_area = a;
                        largest_contour_index = i;                //Store the index of largest contour
                    }

                    CvInvoke.DrawContours(result, contours, largest_contour_index, new MCvScalar(255, 0, 0));
                }

                CvInvoke.DrawContours(result, contours, largest_contour_index, new MCvScalar(0, 0, 255), 3, LineType.EightConnected, hierachy);
                largestContour = new VectorOfPoint(contours[largest_contour_index].ToArray());
            }

            return largestContour;
        }
        */
        
        public FormMain()
        {
            InitializeComponent();

            
            /* EyeX Logging Implementation */
            //logGaze = false;
            //gazeExport = new CsvExport();

            //_eyeXHost = new FormsEyeXHost();
            //_eyeXHost.Start();

            //var stream = _eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            //stream.Next += (s, e) => EyeXHost_GazePointDataStream(s, e);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text += String.Format(" (Rel. {0} (x{1}))", Version, OS);
            Init(382, 288, 2);

            ipc = new IPC2(1);
#if POLLING
	        Application.Idle +=  new EventHandler(this.Application_Idle);
#endif
            timer2.Enabled = true;

            // storing the path of the face detection XML
            face_cascade = new CascadeClassifier("haarcascade_frontalface_default.xml");

            // adding the conditions to the list
            conditionList.Add("Audio");
            conditionList.Add("Reading Text");
            conditionList.Add("Stroop");
            conditionList.Add("Reading and Audio");

            // adding the Videos tasks to the list
            taskAudios.Add("Sustained");
            taskAudios.Add("Selective");
            taskAudios.Add("Divided");

            // adding the Reading Text tasks to the list
            taskReadingText.Add("Sustained");
            taskReadingText.Add("Selective");
            taskReadingText.Add("Divided");

            // adding the Stroop tasks to the list
            taskStroop.Add("Sustained");
            taskStroop.Add("Selective");
            taskStroop.Add("Divided");

            //adding the audio and reading taske to the list
            taskReadingAudio.Add("Selective");
            taskReadingAudio.Add("Divided");
        }

        /*
        private void EyeXHost_GazePointDataStream(object s, GazePointEventArgs e)
        {
            // gaze is always being read but only logged data is processed
            if (logGaze)
            {
                DateTime time = DateTime.Now;
                string format = "yyyyMMddhhmmssfff";

                gazeExport.AddRow();
                gazeExport["Participant"] = participantID;
                gazeExport["Timestamp"] = time.ToString(format);
                gazeExport["EyeXTimestamp"] = e.Timestamp;
                gazeExport["EyeXGazePointX"] = e.X;
                gazeExport["EyeXGazePointY"] = e.Y;
            }
        }*/

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chartAlreadyStarted == true)
            {
                temperatureThread.Abort();
            }
#if POLLING
	        Application.Idle -=  new EventHandler(this.Application_Idle);
#endif
            ReleaseIPC();
        }

        void InitIPC()
        {
            Int64 hr;
            if ((ipc != null) && !ipcInitialized)
            {
                hr = IPC2.Init(0, textBoxInstanceName.Text);

                if (hr != S_OK)
                {
                    ipcInitialized = frameInitialized = false;
                }
                else
                {
#if !POLLING 
                    ipc.OnServerStopped = new IPC2.delOnServerStopped(OnServerStopped);
                    IPC2.SetCallback_OnServerStopped(0, ipc.OnServerStopped);

                    ipc.OnFrameInit = new IPC2.delOnFrameInit(OnFrameInit);
                    Int32 u = IPC2.SetCallback_OnFrameInit(0, ipc.OnFrameInit);

                    ipc.OnNewFrameEx = new IPC2.delOnNewFrameEx(OnNewFrameEx);
                    IPC2.SetCallback_OnNewFrameEx(0, ipc.OnNewFrameEx);

                    ipc.OnInitCompleted = new IPC2.delOnInitCompleted(OnInitCompleted);
                    IPC2.SetCallback_OnInitCompleted(0, ipc.OnInitCompleted);
#endif
                    hr = IPC2.Run(0);
                    ipcInitialized = (hr == S_OK);
                }
                label1.Text = (hr != S_OK) ? "NOT CONNECTED" : "OK";

            }
        }

        private void ReleaseIPC()
        {
            Connected = false;
            if ((ipc != null) && ipcInitialized)
            {
                IPC2.Release(0);
                ipcInitialized = false;
            }
        }

        byte LoByte(Int16 val) { return BitConverter.GetBytes(val)[0]; }
        byte HiByte(Int16 val) { return BitConverter.GetBytes(val)[1]; }
        byte clip(Int32 val) { return (byte)((val <= 255) ? ((val > 0) ? val : 0) : 255); }


        void GetBitmap(Bitmap Bmp, Int16[] values)
        {
            Int32 stride_diff;
            // Lock the bitmap's bits.  
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, Bmp.Width, Bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = Bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, Bmp.PixelFormat);
            stride_diff = bmpData.Stride - FrameWidth * 3;

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            if (Colors)
            {
                for (Int32 dst = 0, src = 0, y = 0; y < FrameHeight; y++, dst += stride_diff)
                    for (Int32 x = 0; x < FrameWidth; x++, src++, dst += 3)
                    {
                        Int32 C = (Int32)LoByte(values[src]) - 16;
                        Int32 D = (Int32)HiByte(values[src - (src % 2)]) - 128;
                        Int32 E = (Int32)HiByte(values[src - (src % 2) + 1]) - 128;
                        rgbValues[dst] = clip((298 * C + 516 * D + 128) >> 8);
                        rgbValues[dst + 1] = clip((298 * C - 100 * D - 208 * E + 128) >> 8);
                        rgbValues[dst + 2] = clip((298 * C + 409 * E + 128) >> 8);
                    }
            }
            else
            {
                Int16 mn, mx;
                GetBitmap_Limits(values, out mn, out mx);
                double Fact = 255.0 / (mx - mn);

                for (Int32 dst = 0, src = 0, y = 0; y < FrameHeight; y++, dst += stride_diff)
                    for (Int32 x = 0; x < FrameWidth; x++, src++, dst += 3)
                        rgbValues[dst] = rgbValues[dst + 1] = rgbValues[dst + 2] = (byte)Math.Min(Math.Max((Int32)(Fact * (values[src] - mn)), 0), 255);
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);

            // Unlock the bits.
            Bmp.UnlockBits(bmpData);
        }

        void GetBitmap_Limits(Int16[] Values, out Int16 min, out Int16 max)
        {
            Int32 y;
            double Sum, Mean, Variance;
            min = Int16.MinValue;
            max = Int16.MaxValue;
            if (Values == null) return;

            Sum = 0;
            for (y = 0; y < FrameSize; y++)
                Sum += Values[y];
            Mean = (double)Sum / FrameSize;
            Sum = 0;
            for (y = 0; y < FrameSize; y++)
                Sum += (Mean - Values[y]) * (Mean - Values[y]);
            Variance = Sum / FrameSize;
            Variance = Math.Sqrt(Variance);
            Variance *= 3;  // 3 Sigma
            min = (Int16)(Mean - Variance);
            max = (Int16)(Mean + Variance);
        }

        void Application_Idle(Object sender, EventArgs e)
        {
#if POLLING 
            if(Connected && frameInitialized)
            {
                Int32 Size = FrameWidth * FrameHeight * FrameDepth;
                IntPtr Buffer = Marshal.AllocHGlobal(Size);
                IPC2.FrameMetadata Metadata;
                for (Int32 x = 0; x < FrameSize; x++)
                    Marshal.WriteInt16(Buffer, x * 2, (Int16)x);
                if (IPC2.GetFrameQueue(0) > 0)
                    if (IPC2.GetFrame(0, 0, Buffer, (UInt32)Size, out Metadata) == S_OK)
                        NewFrame(Buffer, Metadata);
                Marshal.FreeHGlobal(Buffer);
            }
#endif
        }

        Int64 MainTimer100ms()
        {
            Painted = false;
#if POLLING
	        if(ipcInitialized)
	        {
                IPC2.IPCState State = IPC2.GetIPCState(0, true);
                if ((State & IPC2.IPCState.ServerStopped) != 0)
			        OnServerStopped(0);
                if (!Connected && ((State & IPC2.IPCState.InitCompleted) != 0))
			        OnInitCompleted();
                if ((State & IPC2.IPCState.FrameInit) != 0)
		        {
			        Int32 frameWidth, frameHeight, frameDepth;
                    Int32 a = IPC2.GetFrameConfig(0, out frameWidth, out frameHeight, out frameDepth);
			        if(a == S_OK)
				        Init(frameWidth, frameHeight, frameDepth);
		        }
		        if(Connected && ((State & IPC2.IPCState.FileCmdReady) != 0))
		        {
			        string Filename = IPC2.GetPathOfStoredFile(0);
                    if(Filename != null)
			            OnFileCommandReady(Filename);
		        }
	        }
#endif
            return S_OK;
        }

        Int64 MainTimer500ms()
        {
            if (Connected)
            {
                labelTempTarget.Text = String.Format("Target-Temp: {0:##0.0}°C", IPC2.GetTempTarget(0));
            }
            return S_OK;
        }

        void Init(Int32 frameWidth, Int32 frameHeight, Int32 frameDepth)
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameSize = FrameWidth * FrameHeight;
            FrameDepth = frameDepth;
            timer1.Enabled = true;
            bmp = new Bitmap(FrameWidth, FrameHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            Int32 stride = bmpData.Stride;
            bmp.UnlockBits(bmpData);
            rgbValues = new Byte[stride * FrameHeight];
            Values = new Int16[FrameSize];
            pictureBox.Size = new System.Drawing.Size(FrameWidth, FrameHeight);
            UpdateSize();
            frameInitialized = true;
            //FindLargestContour();

        }

        void UpdateSize()
        {
            Size = new System.Drawing.Size(pictureBox.Right + 300, Math.Max(buttonFlagRenew.Bottom, pictureBox.Bottom) + 450);
        }

        Int32 OnServerStopped(Int32 reason)
        {
            ReleaseIPC();
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, bmp.Width, bmp.Height);
            pictureBox.Invalidate();
            return 0;
        }

        Int32 OnFrameInit(Int32 frameWidth, Int32 frameHeight, Int32 frameDepth)
        {
            Init(frameWidth, frameHeight, frameDepth);
            return 0;
        }

        // will work with Imager.exe release > 2.0 only:
        Int32 OnNewFrameEx(IntPtr data, IntPtr Metadata)
        {
            if (!frameInitialized)
                return S_FALSE;
            return NewFrame(data, (IPC2.FrameMetadata)Marshal.PtrToStructure(Metadata, typeof(IPC2.FrameMetadata)));
        }

        Int32 NewFrame(IntPtr data, IPC2.FrameMetadata Metadata)
        {
            labelFrameCounter.Text = "Frame counter HW/SW: " + Metadata.CounterHW.ToString() + "/" + Metadata.Counter.ToString();
            labelPIF.Text =
                "PIF   DI:" + ((Metadata.PIFin[0] >> 15) == 0).ToString() +
                "     AI1:" + (Metadata.PIFin[0] & 0x3FF).ToString() +
                "     AI2:" + (Metadata.PIFin[1] & 0x3FF).ToString();

            switch (Metadata.FlagState)
            {
                case IPC2.FlagState.FlagOpen: labelFlag.Text = "open"; labelFlag.ForeColor = Color.OrangeRed; labelFlag.BackColor = labelFlag1.BackColor; break;
                case IPC2.FlagState.FlagClose: labelFlag.Text = "closed"; labelFlag.ForeColor = Color.White; labelFlag.BackColor = Color.Red; break;
                case IPC2.FlagState.FlagOpening: labelFlag.Text = "opening"; labelFlag.ForeColor = SystemColors.WindowText; labelFlag.BackColor = Color.Yellow; break;
                case IPC2.FlagState.FlagClosing: labelFlag.Text = "closing"; labelFlag.ForeColor = SystemColors.WindowText; labelFlag.BackColor = Color.Yellow; break;
                default: labelFlag.Text = ""; labelFlag.ForeColor = labelFlag1.ForeColor; labelFlag.BackColor = labelFlag1.BackColor; break;
            }

            for (Int32 x = 0; x < FrameSize; x++)
                Values[x] = Marshal.ReadInt16(data, x * 2);
            if (!Painted)
            {
                GetBitmap(bmp, Values);
                pictureBox.Invalidate();
                Painted = true;
            }

            return 0;
        }

        Int32 OnInitCompleted()
        {
            label1.Text = "Connected with #" + IPC2.GetSerialNumber(0);
            Colors = ((TIPCMode)IPC2.GetIPCMode(0) == TIPCMode.Colors);
            Connected = true;
            UpdateSize();
            return S_OK;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!ipcInitialized || !Connected) InitIPC();
        }

        /*
         * stores every 2 seconds forehead and nasal temperatures in associated lists if a face has been detected
         * otherwise storing the value 0 in the lists indicating that the face detection didn't work
         * 
         */
        private void timer3_Tick(object sender, EventArgs e)
        {
            //Joshua: Here i write the temperature in the list
            if (noFaceDetected == false)
            {
                foreheadTemperatures.Add(foreheadTemperature);
                orbitalTemperatures.Add(orbitalTemperature);
                nasalTemperatures.Add(nasalTemperature);
                cheekTemperatures.Add(cheekTemperature);
                maxillaryTemperatures.Add(maxillaryTemperature);
                chinTemperatures.Add(chinTemperature);
            }
            else
            {
                foreheadTemperatures.Add(0);
                orbitalTemperatures.Add(0);
                nasalTemperatures.Add(0);
                cheekTemperatures.Add(0);
                maxillaryTemperatures.Add(0);
                chinTemperatures.Add(0);
            }
        }

        /*
         * is called when the chart thread is generated calling the "updateTempChart" method
         * 
         */
        private void getChartTemperatures()
        {
            while (true)
            {
                if (chart1.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { updateTempChart(); });
                }
                else
                {

                }
            }
        }

        /*
         * being called by the "getChartTemperatures" method displaying the saved forehead and nasal area temperature values
         * in the chart in a live view
         * 
         */
        private void updateTempChart()
        {
            for (int i = currentIndex; i <= foreheadTemperatures.Count(); i++)
            {
                int secondIntervalTemp = i * 2;
                string secondInterval = secondIntervalTemp.ToString();
                this.chart1.Series["Forehead"].Points.AddXY(secondInterval, foreheadTemperatures.ElementAt(i - 1));
                this.chart1.Series["Nasal-Area"].Points.AddXY(secondInterval, nasalTemperatures.ElementAt(i - 1));
                currentIndex++;
            }
        }



        /*
         * performed when button "Start Data processing" is pushed storing the forehead and nasal area temperature values
         * in the lists and starting the thread which shows them as a live view in the chart also resetting the countdown
         * 
         */
        private void loadChartButton_Click(object sender, EventArgs e)
        {
            //Joshua: Here i create the thread and start temp storing  
            if (chartAlreadyStarted == false)
            {
                //logGaze = true;

                timer3.Enabled = true;
                chartAlreadyStarted = true;

                temperatureThread = new Thread(new ThreadStart(this.getChartTemperatures));
                temperatureThread.IsBackground = true;
                temperatureThread.Start();
                chartStopped = false;

                // countdown timer
                seconds = 0;
                label6.Text = "0";
                minutes = 4;
                label3.Text = "4";
                timer4.Enabled = true;

                if (noFaceDetected == false)
                {
                    foreheadTemperatures.Add(foreheadTemperature);
                    orbitalTemperatures.Add(orbitalTemperature);
                    nasalTemperatures.Add(nasalTemperature);
                    cheekTemperatures.Add(cheekTemperature);
                    maxillaryTemperatures.Add(maxillaryTemperature);
                    chinTemperatures.Add(chinTemperature);
                }
                else
                {
                    foreheadTemperatures.Add(0);
                    orbitalTemperatures.Add(0);
                    nasalTemperatures.Add(0);
                    cheekTemperatures.Add(0);
                    maxillaryTemperatures.Add(0);
                    chinTemperatures.Add(0);
                }
            }
        }

        /*
         * handles the timer countdown for each task, 
         * and automatically saves the temperature and gaze into csv file 
         * with name as ParticpantID_TaskName
         */
        private void timer4_Tick(object sender, EventArgs e)
        {
            seconds = seconds - 1;

            if (seconds == -1)
            {
                minutes = minutes - 1;
                seconds = 59;
            }

            if (minutes == 0 && seconds == 0)
            {
                timer4.Enabled = false;
                //saveCurrentTask();



                //stopChartButton_Click(sender, e);
                if (chartAlreadyStarted == true && chartStopped == false)
                {
                    //Joshua: Here the thread is aborted
                    //logGaze = false;

                    chartStopped = true;
                    timer3.Enabled = false;
                    timer4.Enabled = false;
                    chartAlreadyStarted = false;
                    temperatureThread.Abort();
                }
                MessageBox.Show("Time is up!", "Time");
                taskCompleted = true;

            }

            // convert the current time into strings
            string mm = Convert.ToString(minutes);
            string ss = Convert.ToString(seconds);

            // display the current time in the labels
            label3.Text = mm;
            label6.Text = ss;


        }

        /*
         * performed when button "Stop Data processing" is pushed but also when the countdown of 3 minutes runs out
         * disabling timers for temperature value saving, countdown and the thread which handles the chart
         * 
         */
        /*
         private void stopChartButton_Click(object sender, EventArgs e)
         {
             if (chartAlreadyStarted == true && chartStopped == false)
             {
                 //Joshua: Here the thread is aborted
                 logGaze = false;

                 chartStopped = true;
                 timer3.Enabled = false;
                 timer4.Enabled = false;
                 chartAlreadyStarted = false;
                 temperatureThread.Abort();
             }
         }
         */
        /*
         * performed when button "Show" is pushed handling the temperature difference calculations and
         * calls the "displayRandomizedTasksInListView2" method which adds all vital information to listview2 
         * for the export as text file
         * 
         */
        private void showTaskButton_Click(object sender, EventArgs e)
        {
            if (chartStopped == true && taskCompleted == true)
            {
                // adding all vital information to listview2 for the export as csv file
                displayRandomizedTasksInListView2();
                indexForAllTaskList++;
                showListView2 = true;

                foreheadTemperatures.Clear();
                nasalTemperatures.Clear();
                currentIndex = 1;
                taskCompleted = false;
            }
        }

        /*
         * serves as the last step for saving the information of each task as a text file,
         * displaying the information in the listview2
         * 
         */
        private void displayRandomizedTasksInListView2()
        {
            // adds all 90 temperature values of forhead and nasal area to the listview inclusive the flag for the times in which the face detection didn't work
            for (int m = 0; m < foreheadTemperatures.Count(); m++)
            {
                // adds the ID of the current participant
                ListViewItem item = new ListViewItem(participantID);
                item.SubItems.Add(allTaskList.ElementAt(indexForAllTaskList));
                if (foreheadTemperatures.ElementAt(m) != 0)
                {
                    item.SubItems.Add(foreheadTemperatures.ElementAt(m).ToString());
                    item.SubItems.Add(orbitalTemperatures.ElementAt(m).ToString());
                    item.SubItems.Add(nasalTemperatures.ElementAt(m).ToString());
                    item.SubItems.Add(cheekTemperatures.ElementAt(m).ToString());
                    item.SubItems.Add(maxillaryTemperatures.ElementAt(m).ToString());
                    item.SubItems.Add(chinTemperatures.ElementAt(m).ToString());


                    item.SubItems.Add("-");
                }
                else
                {
                    item.SubItems.Add("-");
                    item.SubItems.Add("-");
                    item.SubItems.Add("flag");
                }
                listView2.Items.Add(item);
            }
        }

        /*
         * performed when button "save Task" is pushed, saving the current task after pushing the stop data processing 
         * button or after the countdown of 3 minutes runs out
         * 
         */
        private async void saveTask_Click(object sender, EventArgs e)
        {
            displayRandomizedTasksInListView2();
            indexForAllTaskList++;
            showListView2 = true;

            foreheadTemperatures.Clear();
            nasalTemperatures.Clear();
            currentIndex = 1;
            taskCompleted = false;

            if (showListView2 == true)
            {
                // saving the data from listview2 in a csv
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (TextWriter tw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                        {
                            foreach (ListViewItem item in listView2.Items)
                            {
                                await tw.WriteLineAsync(item.SubItems[0].Text + "," + item.SubItems[1].Text + "," + item.SubItems[2].Text + "," + item.SubItems[3].Text + "," + item.SubItems[4].Text);
                            }
                            MessageBox.Show("Data has been successfully exported.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // save gaze as a .csv file
                        //gazeExport.ExportToFile(System.IO.Path.GetDirectoryName(Path.GetFullPath(sfd.FileName)) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + "-gaze" + ".csv");
                    }
                }
                listView2.Items.Clear();
                showListView2 = false;
                //logGaze = false;

                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
            }
        }
        /*
         * performed when button "save Task" is pushed, saving the current task after pushing the stop data processing 
         * button or after the countdown of 3 minutes runs out
         * 
         */
        private async void saveCurrentTask()
        {
            String currentTask = listView2.Items[0].SubItems[1].Text;
            String filename = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + participantID + "-" + currentTask;
            using (TextWriter tw = new StreamWriter(new FileStream(filename + "-temp.csv", FileMode.Create), Encoding.UTF8))
            {
                foreach (ListViewItem item in listView2.Items)
                {
                    await tw.WriteLineAsync(item.SubItems[0].Text + "," + item.SubItems[1].Text + "," + item.SubItems[2].Text + "," + item.SubItems[3].Text + "," + item.SubItems[4].Text);
                }
                MessageBox.Show("Data has been successfully exported.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // save gaze as a .csv file
            //gazeExport.ExportToFile(filename+ "-gaze.csv");

            listView2.Items.Clear();
            showListView2 = false;
            //logGaze = false;

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
        }

        /*
         * performed when the button "New Participant" is pushed specifying, calls method "displayRandomizedConditionsAndTasksInListView"
         * which randomizes conditions and tasks for each participant and shows this in the listview
         * 
         */
        private void newParticipantButton_Click(object sender, EventArgs e)
        {
            if (newParticipantAlreadyGenerated == false)
            {
                // checks the value of the text
                if (participantNumberTextbox.Text.Length != 0)
                {
                    participantID = participantNumberTextbox.Text;
                    participantNumberTextbox.Clear();
                    participantNumberTextbox.Enabled = false;
                }

                newParticipantAlreadyGenerated = true;
                addingTasksToListPermitted = true;

                // randomizes the conditions
                Shuffle<string>(conditionList);
                randomizingTasksPermitted = true;
                displayRandomizedConditionsAndTasksInListView();

                // guarantuees adding tasks and randomizing tasks of each condition for each participant 
                // only once at the start of the experiment, where a new participant is generated
                addingTasksToListPermitted = false;
                randomizingTasksPermitted = false;
            }
        }

        /*
         * shows the order of conduct of the experiment, 
         * displaying the order of condition and task in the listview1
         * 
         */
        private void displayRandomizedConditionsAndTasksInListView()
        {
            foreach (string i in conditionList)
            {
                if (i == "Audios")
                {
                    if (randomizingTasksPermitted == true)
                    {
                        // randomize the tasks for the Videos condition
                        Shuffle<string>(taskAudios);
                    }

                    // adds all tasks to the corresponding participant and condition in the listview
                    foreach (string j in taskAudios)
                    {
                        // adds the ID of the current participant
                        ListViewItem item = new ListViewItem(participantID);
                        item.SubItems.Add(i);
                        item.SubItems.Add(j);
                        listView1.Items.Add(item);

                        if (addingTasksToListPermitted == true)
                        {
                            // adds the tasks to the list for the saving as a text file for each task
                            allTaskList.Add(j);
                        }
                    }
                }
                else if (i == "Reading Text")
                {
                    if (randomizingTasksPermitted == true)
                    {
                        // randomize the tasks for the Reading Text condition
                        Shuffle<string>(taskReadingText);
                    }

                    // adds all tasks to the corresponding participant and condition in the listview
                    foreach (string j in taskReadingText)
                    {
                        // adds the ID of the current participant
                        ListViewItem item = new ListViewItem(participantID);
                        item.SubItems.Add(i);
                        item.SubItems.Add(j);
                        listView1.Items.Add(item);

                        if (addingTasksToListPermitted == true)
                        {
                            allTaskList.Add(j);
                        }
                    }
                }
                else
                {
                    if (randomizingTasksPermitted == true)
                    {
                        // randomize the tasks for the Tasks condition
                        Shuffle<string>(taskStroop);
                    }

                    // adds all tasks to the corresponding participant and condition in the listview
                    foreach (string j in taskStroop)
                    {
                        // adds the ID of the current participant
                        ListViewItem item = new ListViewItem(participantID);
                        item.SubItems.Add(i);
                        item.SubItems.Add(j);
                        listView1.Items.Add(item);

                        if (addingTasksToListPermitted == true)
                        {
                            allTaskList.Add(j);
                        }
                    }
                }
            }
        }

        /*
         * performed when the button "Show for CSV" is pushed. has to be triggered before csv file is exported, 
         * serves as illustration how the exported csv file will look, displays the information in the listview1
         * 
         */
        private void displayInformationInListView1()
        {
            for (int i = 0; i < indexForAllTaskList; i++)
            {
                if (indexForAllTaskList < 5)
                {
                    // adds the ID of the current participant
                    ListViewItem item = new ListViewItem(participantID);
                    item.SubItems.Add(conditionList.ElementAt(0).ToString());
                    item.SubItems.Add(allTaskList.ElementAt(i).ToString());
                    //item = addMissingInformationToListView1ForCSVExport(item);
                    listView1.Items.Add(item);
                }
                else if (indexForAllTaskList < 9)
                {
                    // adds the ID of the current participant
                    ListViewItem item = new ListViewItem(participantID);
                    if (taskCountForTemperatureLists < 4)
                    {
                        item.SubItems.Add(conditionList.ElementAt(0).ToString());
                    }
                    else
                    {
                        item.SubItems.Add(conditionList.ElementAt(1).ToString());
                    }
                    item.SubItems.Add(allTaskList.ElementAt(i).ToString());
                    //item = addMissingInformationToListView1ForCSVExport(item);
                    listView1.Items.Add(item);
                }
                else
                {
                    // adds the ID of the current participant
                    ListViewItem item = new ListViewItem(participantID);
                    if (taskCountForTemperatureLists < 4)
                    {
                        item.SubItems.Add(conditionList.ElementAt(0).ToString());
                    }
                    else if (taskCountForTemperatureLists < 8)
                    {
                        item.SubItems.Add(conditionList.ElementAt(1).ToString());
                    }
                    else
                    {
                        item.SubItems.Add(conditionList.ElementAt(2).ToString());
                    }
                    item.SubItems.Add(allTaskList.ElementAt(i).ToString());
                    //item = addMissingInformationToListView1ForCSVExport(item);
                    listView1.Items.Add(item);
                }
            }
        }



        /* 
         * generates a randomized order of the conditions and tasks
         * 
         * \param ilist   the list which holds conditions or the tasks which should be randomized for the experiment
         *                since they are the independent variables and the order could have some effect on the results
         *                
         */
        public static void Shuffle<T>(IList<T> ilist)
        {
            int iIndex;
            T tTmp;
            for (int i = 1; i < ilist.Count; ++i)
            {
                iIndex = random.Next(i + 1);
                tTmp = ilist[i];
                ilist[i] = ilist[iIndex];
                ilist[iIndex] = tTmp;
            }
        }


        /*
         * Displays the data associated to a participant in the listView1 for confirmation if the data stored in the lists are correct
         * clearing the way for the CSV file export. Calls displayInformationInListView1 which adds the accumulated data if the experiment
         * has not been completed. After displaying the data in listView1 the data in the lists is cleared getting ready for a new participant
         */
        /*
       private void showForCSVButton_Click(object sender, EventArgs e)
       {
           // clears the way for the generation of a new participant
           newParticipantAlreadyGenerated = false;
           participantNumberTextbox.Enabled = true;
           showListView1 = true;
           listView1.Items.Clear();
           displayInformationInListView1();

           // resetting all data for a new participant
           taskCountForTemperatureLists = 0;
           allTaskList.Clear();
           indexForAllTaskList = 0;
       }
       */

        /* 
         * Only possible if the "Show For CSV" button is pressed first, saves the data associated to a participant in a .csv file
         * 
         */
        private async void saveParticipant_Click(object sender, EventArgs e)
        {
            // clears the way for the generation of a new participant
            newParticipantAlreadyGenerated = false;
            participantNumberTextbox.Enabled = true;
            showListView1 = true;
            listView1.Items.Clear();
            displayInformationInListView1();

            // resetting all data for a new participant
            taskCountForTemperatureLists = 0;
            allTaskList.Clear();
            indexForAllTaskList = 0;

            if (showListView1 == true)
            {
                //Joshua: Here i push the list to the .csv file
                // save the listview as a .csv file
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        sfd.ToString();
                        using (StreamWriter sw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Participant ID,Condition,Task,Forehead,Orbital,Nasal,Cheek,Maxillary,Chin");
                            foreach (ListViewItem item in listView1.Items)
                            {
                                //Yomna: Add the timestamp
                                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text));
                            }
                            await sw.WriteLineAsync(sb.ToString());
                            MessageBox.Show("Data has been successfully exported.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // save gaze as a .csv file
                        //gazeExport.ExportToFile(System.IO.Path.GetDirectoryName(Path.GetFullPath(sfd.FileName)) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + "-gaze" + ".csv");
                    }
                }
                listView1.Items.Clear();
                showListView1 = false;

                //logGaze = false;
            }
        }

        /*
         * Handles the action when pressing the "Refresh List" button, it stops Data Processing before
         * with resetting the temperature lists and the timer for restarting the task
         * 
         */
        private void refreshChartButton_Click_1(object sender, EventArgs e)
        {
            if (chartAlreadyStarted == true && chartStopped == false)
            {
                //Joshua: Here the thread is aborted
                //logGaze = false;

                chartStopped = true;
                timer3.Enabled = false;
                timer4.Enabled = false;
                chartAlreadyStarted = false;
                temperatureThread.Abort();
            }

            if (chartStopped == true)
            {
                foreheadTemperatures.Clear();
                nasalTemperatures.Clear();

                // resets the time for the task
                label3.Text = "3";
                label6.Text = "0";
                seconds = 0;
                minutes = 3;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Painted = false;
            MainTimerDivider++;
            MainTimer100ms();
            if ((MainTimerDivider % 5) == 0) MainTimer500ms();
        }

        /*
         * Executing the face detection on the stored images from the live view, followed by drawing the found faces
         * and getting the coordinates of the rectangles helps detecting the curragator muscle area and nasal area
         * which are then marked as red rectangles. Also their temperatures are calculated by using the getPixelTemp
         * method and displaying all this processed information
         * 
         */
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Random rnd = new Random();
            
            // creating an image from bitmap
            Image<Bgr, Byte> imageFrame = new Image<Bgr, Byte>(bmp);
            Image<Bgr, Byte> imageFrame1 = new Image<Bgr, Byte>(bmp);
            Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();


            for (int p=0; p < 5; p++)
            {
                int x = rnd.Next(50, 200);
                int y = rnd.Next(20, 300);    
                int h = rnd.Next(30, 100);
                int w = rnd.Next(40, 100);

                double depth = rnd.Next(1, 5);
                int radius = rnd.Next(20, 40);

                Rectangle radar = new Rectangle(x, y, w, h);
                imageFrame1.Draw(radar, new Bgr(Color.Red), radius);
            }
            //CvInvoke.Imshow("SensorFusion", imageFrame1); //Show the image 
            //CvInvoke.Imshow("SensorFusion V1", grayFrame); //Show the image 
            //CvInvoke.Imshow("SensorFusion V2", imageFrame1); //Show the image 


            var faces = face_cascade.DetectMultiScale(grayFrame, 1.1, 10);
            //FindLargestContour();

            if (faces.Count() != 0)
            {
                noFaceDetected = false;
                foreach (var face in faces)
                {
                    imageFrame.Draw(face, new Bgr(Color.Gold), 3);

                    //calculate point values for forehead for each face rectangle
                    x = face.X;
                    y = face.Y;

                    // calculating the x and y coordinates for the forehead rectangle
                    x1 = x + (4 * face.Width / 7);
                    xForehead = (ushort)x1;
                    y1 = y + (face.Height / 6);
                    yForehead = (ushort)y1;

                    // calculating the x and y coordinates for the nasal rectangle
                    x2 = x + (4 * face.Width / 9);
                    xNasal = (ushort)x2;
                    y2 = y + (face.Height / 2);
                    yNasal = (ushort)y2;

                    // creating point for forehead
                    forehead = new Point(xForehead, yForehead);

                    // creating point for nasal
                    nasal = new Point(xNasal, yNasal);


                    // getting the temperature
                    foreheadTemperature = getPixelTemp(forehead.X, forehead.Y);
                    orbitalTemperature = getPixelTemp(forehead.X, forehead.Y);
                    nasalTemperature = getPixelTemp(nasal.X, nasal.Y);
                    cheekTemperature = getPixelTemp(forehead.X, forehead.Y);
                    maxillaryTemperature = getPixelTemp(forehead.X, forehead.Y);
                    chinTemperature = getPixelTemp(forehead.X, forehead.Y);



                    this.label2.Text = "Forehead Temperature: " + foreheadTemperature;
                    this.label4.Text = "Nasal area Temperature: " + nasalTemperature;

                    Rectangle rectForehead = new Rectangle(xForehead, yForehead, 10, 5);
                    Rectangle rectNasal = new Rectangle(xNasal, yNasal, 10, 5);
                    imageFrame.Draw(rectForehead, new Bgr(Color.Red), 3);
                    imageFrame.Draw(rectNasal, new Bgr(Color.Red), 3);

                }
            }
            else
            {
                noFaceDetected = true;
            }
            bmp = imageFrame.ToBitmap();
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        /*
         * Gets the temperature value of pixel(x,y). The information of extracting the temperature is taken from the "IPC DLL2 documentation" 
         * from the "Optris Software CD" leaned on the method leaned on the method 
         * Marius Kleiner used in his bachelor thesis "Belastung als eine Eingabemodalität zur Interaktion mit graphischen
         * Benutzungsoberflächen". Only possible if the external communication is
         * set to temperature since the pixels contain temperatures in °C and the temperature can 
         * be calculated by the formula: T[°C] = (value-1000)/10
         * The gcnew array Values contains the temperature data. There are two different value-formats
         * explained in mentioned documentation
         * 
         * \param x   X-Coordinate of the pixel
         * \param y   Y-Coordinate of the pixel
         * 
         * \return   the temperature of pixel(x,y) as a double
         */
        private double getPixelTemp(int x, int y)
        {
            // Calculating the index of temperature data for pixel(x,y)
            // the temperatre matrix is 160x120 sized
            int index = (y * 382) + x;
            int value = Values[index];
            int mode = ((int)IPC2.GetTempRangeDecimal(0, false));
            float valueFloat;
            // convert to float (depending on decimal mode)
            // 1 decimal place
            if (mode == 0)
            {
                valueFloat = ((float)(value - 1000) / (float)10);
            }
            else
            // if (mode == 1) 2 decimal places
            {
                valueFloat = ((float)value / (float)100);
            }
            return valueFloat;
        }

        private void buttonFlagRenew_Click(object sender, EventArgs e)
        {
            buttonFlagRenew.Text = String.Format("Renew ({0})", IPC2.RenewFlag(0) ? "Success" : "Failed");
        }

    }
}