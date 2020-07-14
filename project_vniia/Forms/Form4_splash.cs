using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace project_vniia
{
    public partial class Form4_splash : Form
    {
        public Form4_splash()
        {
            InitializeComponent();
        }


        static Form4_splash ms_frmSplash = null;
        static Thread ms_oThread = null;
        private double m_dblOpacityIncrement = .05;
        private double m_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 50;
        
        // A static entry point to launch SplashScreen.
        static private void ShowForm()
        {
            ms_frmSplash = new Form4_splash();
            Application.Run(ms_frmSplash);
        }
        // A static method to close the SplashScreen
        static public void CloseForm()
        {
            if (ms_frmSplash != null)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            }
            ms_oThread = null;  // we do not need these any more.
            ms_frmSplash = null;
        }
        private void Form4_splash_Load(object sender, EventArgs e)
        {//??????????????
            this.Opacity = .0;
            UpdateTimer.Interval = TIMER_INTERVAL;
            UpdateTimer.Start();
        }

        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(Form4_splash.ShowForm));
            ms_oThread.IsBackground = true;
            ms_oThread.SetApartmentState(ApartmentState.STA);
            ms_oThread.Start();
            while (ms_frmSplash == null || ms_frmSplash.IsHandleCreated == false)
            {
                System.Threading.Thread.Sleep(TIMER_INTERVAL);
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (m_dblOpacityIncrement > 0)
            {
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                    this.Close();
            }
        }
    }
}
