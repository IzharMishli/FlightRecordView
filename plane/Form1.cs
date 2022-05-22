using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using plane.Scripts;

namespace plane
{
    public partial class Form1 : Form
    {
        MainApp mainApp;
        CSVHandle csvHandle;
        GMaps map;
        public GMapMarker planeIcon;
        public GMapOverlay markers = new GMapOverlay("markers");

        public Form1()
        {
            InitializeComponent();
            mainApp = new MainApp(this);
            csvHandle = new CSVHandle(this);
            initMap();
        }
        public void initMap()
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(31.771959, 35.217018);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 50;
            gMapControl1.Zoom = 6;

            GMapMarker planeIcon =
                new GMarkerGoogle(
                    new PointLatLng(32.794044, 34.989571),
                    bitmap: new Bitmap(Properties.Resources.planeIMG));
            markers.Markers.Add(planeIcon);
            gMapControl1.Overlays.Add(markers);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "./";
            //openFileDialog.Filter = "csv File (*.csv)|*.mdb;";
            //openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                csvHandle.ReadCsv(selectedFileName);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            mainApp.startApp(btn);
        }

        public void updateTime(int time)
        {
            InfoBox.AppendText("computer time: " + time.ToString());
            InfoBox.AppendText(Environment.NewLine);
            InfoBox.AppendText("position: " + DataManager.Data2[time].lat + ", " + DataManager.Data2[time].lon);
            InfoBox.AppendText(Environment.NewLine);
            InfoBox.AppendText("errors: " + DataManager.Data2[time].errors);
            InfoBox.AppendText(Environment.NewLine);
            InfoBox.AppendText("------------");
            InfoBox.AppendText(Environment.NewLine);
        }
        public void updatePos(PointLatLng point)
        {
            gMapControl1.Overlays[0].Markers[0].Position = point;
            gMapControl1.Overlays[0].Markers[0].ToolTipText = point.Lat.ToString() + "," + point.Lng.ToString();
            

        }
        public void enableStartBtn()
        {
            button1.Enabled = true;
        }

    }
}
