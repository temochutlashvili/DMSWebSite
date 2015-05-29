using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurnMonitor
{
    
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private int SC_MONITORPOWER = 0xF170;
        private int WM_SYSCOMMAND = 0x0112;

        public enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetMonitorState(MonitorState.ON);
            timer1.Stop();
        }

        public void SetMonitorState(MonitorState state)
        {
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, (int)state);
        }

        private void turnOffButton_Click(object sender, EventArgs e)
        {
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;

            //SetMonitorState(MonitorState.OFF);
            //timer1.Start();
        }
    }
}
