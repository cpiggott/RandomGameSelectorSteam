using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace SteamGameSelector
{
    public partial class RandomGameGenerator : Form
    {

        public RandomGameGenerator()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        List<string> idss = new List<string>();

        private void selectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //System.Environment.SpecialFolder root = new Environment.SpecialFolder();
            fbd.SelectedPath = "C:\\Program Files (x86)\\Steam\\steamapps";
            if (DialogResult.OK == fbd.ShowDialog())
            {
                string path = fbd.SelectedPath.ToString();
                idss = getrandomfile2(path);
                textBox1.Text = path;
                button1.Enabled = true;
                

            }
        }

        public static List<string> getrandomfile2(string path)
        {
            string file = null;
            List<string> appIDs = new List<string>();
            StreamReader lookThrough;
            if (!string.IsNullOrEmpty(path))
            {
                var extensions = new string[] { ".acf" };
                try
                {
                    var di = new DirectoryInfo(path);
                    var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                    //Random R = new Random();
                    //file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                    string tempLine = "";
                    for (int i = 0; i < rgFiles.Count(); i++)
                    {
                        lookThrough = new StreamReader(rgFiles.ElementAt(i).FullName.ToString());
                        while (!lookThrough.EndOfStream)
                        {
                            tempLine = lookThrough.ReadLine().ToLower();
                            if (tempLine.Contains("appid"))
                            {
                                string[] splitdata = tempLine.Split('"');
                                
                                appIDs.Add(splitdata[3]);
                                break;
                            }

                        }
                        //store the appid in the array of appID's

                    }


                }
                // probably should only catch specific exceptions
                // throwable by the above methods.
                catch { }
            }
            return appIDs;
        }

        private static void startApp(List<string> ids)
        {
            Random r = new Random();
            int fileLoc = r.Next(0, ids.Count - 1);
            string id = ids[fileLoc];
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "C:\\Program Files (x86)\\Steam\\steam.exe";
            startInfo.Arguments = " -applaunch " + id;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startApp(idss);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
