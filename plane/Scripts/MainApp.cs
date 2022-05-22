using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace plane.Scripts
{
    public class MainApp
    {
        public Timer timer;
        public Form1 mainForm;
        public int counter;
        public MainApp(Form1 form)
        {
            mainForm = form;
        }
        public void SetTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += refreshScreen;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void refreshScreen(Object obj, ElapsedEventArgs a)
        {
            counter += 1;
            int currentTime = DataManager.Data2.Keys.Min() + counter;
            mainForm.Invoke(new MethodInvoker(delegate() { mainForm.updateTime(currentTime); }));

            DataManager.Data2.TryGetValue(currentTime, out DataPiece data);
            PointLatLng p = new PointLatLng(
                Convert.ToDouble(data.lat), Convert.ToDouble(data.lon));
            mainForm.Invoke(new MethodInvoker(delegate () { mainForm.updatePos(p); }));
        }

        public void startApp(Button btn)
        {
            SetTimer();
            btn.Enabled = false;
        }
    }
}
