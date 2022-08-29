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
using System.Net;
using Renci.SshNet;
using System.Security.Cryptography;

namespace Xmas_Grotto_Information
{
    public partial class frmGrottoInfo : Form
    {
        bool bexit,bLoaded;
        int i, intUnique;
        string strRef,b,c;
        string plainText, cipherText;
        string Host = Properties.Settings.Default.giFTPHost.ToString();
        int Port = Properties.Settings.Default.giFTPPort;
        string Username = Properties.Settings.Default.giFTPUsername.ToString();
        string Password = Properties.Settings.Default.giFTPPassword.ToString();
        List<string> child_list = new List<string>();
        List<string> adult_list = new List<string>();
        List<string> family_list = new List<string>();
        public frmGrottoInfo()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to close the Grotto Information?", "Close Controller", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                bexit = true;
                this.Close();
            }
        }

        private void frmGrottoInfo_Load(object sender, EventArgs e)
        {
            bLoaded = false;
            loadSettings();
        }

        private void tmMonitor_Tick(object sender, EventArgs e)
        {
            var list = new List<string>();
            bool bRead = false;
            string b = Properties.Settings.Default.giMonitorLoc.ToString() + "\\Grotto" + Properties.Settings.Default.giNo.ToString()+".txt";

            try
            {
                var fileStream = new FileStream(b, FileMode.Open, System.IO.FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                fileStream.Close();
                bRead = true;
            }
            catch
            {
                bRead = false;
                tmMonitor.Stop();
                MessageBox.Show("Can't read the stage/monitor file, please check if the address is valid in the settings menu", "Table read error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                
            }

            if (bRead == true)
            {
                if (list[0].ToString() != Properties.Settings.Default.giUnique.ToString())
                {
                    lblStatus.Text = "Family Found";
                    lblStatus.Refresh();
                    lblFamily.Text = list[2].ToString();
                    lblRef.Text = list[1].ToString();
                    strRef = list[1].ToString();
                    Properties.Settings.Default.giUnique = list[0].ToString();
                    cmdLeft.Visible = true;
                    findFamily();
                }
            }
        }

        private void findFamily()
        {
            //variables
            var list = new List<string>();
            int intChildCount = 0;
            int intAdultCount = 0;
            int intFamilyCount = 0;


            //check if directories for fmamiles, children, adults exists otherwise create them
            string gc = Properties.Settings.Default.giLocalSave.ToString() + "\\Child Info";
            System.IO.Directory.CreateDirectory(gc);
            string ga = Properties.Settings.Default.giLocalSave.ToString() + "\\Adult Info";
            System.IO.Directory.CreateDirectory(ga);
            string gf = Properties.Settings.Default.giLocalSave.ToString() + "\\Family Info";
            System.IO.Directory.CreateDirectory(gf);

            lblStatus.Text = "Downloading Family";
            lblStatus.Refresh();
            //check local directoris to see if files are already downloaded
            //children
            bool bCExists = false;
            DirectoryInfo dc = new DirectoryInfo(gc);
            FileInfo[] cfiles = dc.GetFiles();
            var lstChildFiles = new List<string>();
            for (i = 0; i < cfiles.Count(); i++)
            {
                if (cfiles[i].Name.ToString().Contains(strRef))
                {
                    lstChildFiles.Add(cfiles[i].FullName.ToString());
                    bCExists = true;
                }
            }
            //adults
            bool bAExists = false;
            DirectoryInfo da = new DirectoryInfo(ga);
            FileInfo[] afiles = da.GetFiles();
            var lstAdultFiles = new List<string>();
            for (i = 0; i < afiles.Count(); i++)
            {
                if (afiles[i].Name.ToString().Contains(strRef))
                {
                    lstAdultFiles.Add(afiles[i].FullName.ToString());
                    bAExists = true;
                }
            }
            //family
            bool bFExists = false;
            DirectoryInfo df = new DirectoryInfo(gf);
            FileInfo[] ffiles = df.GetFiles();
            var lstFamilyFiles = new List<string>();
            for (i = 0; i < ffiles.Count(); i++)
            {
                if (ffiles[i].Name.ToString().Contains(strRef))
                {
                    lstFamilyFiles.Add(ffiles[i].FullName.ToString());
                    bFExists = true;
                }
            }



            //if files exist for children, adults and family then
            if (bCExists == true && bAExists == true && bFExists == true)
            {
                dgvChild.RowCount = lstChildFiles.Count();
                dgvAdult.RowCount = lstAdultFiles.Count();
                dgvFamily.RowCount = lstFamilyFiles.Count();
                //read child files and load into datagridview
                var cList = new List<string>();
                for (i = 0; i < lstChildFiles.Count(); i++)
                {
                    string strread = lstChildFiles[i];
                    intChildCount++;
                    var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            cList.Add(line);
                        }
                    }
                    fileStream.Close();

                    cipherText = cList[0].ToString();
                    decryptData();
                    string[] splitDD = plainText.ToString().Split(',');
                    int j = 0;
                    while (j < splitDD.Count())
                    {
                        dgvChild.Rows[intChildCount - 1].Cells[j].Value = splitDD[j];
                        j = j + 1;
                    }
                    dgvChild.Refresh();

                    dgvChild.Height = (intChildCount * 50) + 60;
                }
                //read adult files and load into datagridview
                var aList = new List<string>();
                for (i = 0; i < lstAdultFiles.Count(); i++)
                {
                    string strread = lstAdultFiles[i];
                    intAdultCount++;
                    var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            aList.Add(line);
                        }
                    }
                    fileStream.Close();

                    cipherText = aList[0].ToString();
                    decryptData();
                    string[] splitDD = plainText.ToString().Split(',');
                    int j = 0;
                    while (j < splitDD.Count())
                    {
                        dgvAdult.Rows[intAdultCount - 1].Cells[j].Value = splitDD[j];
                        j = j + 1;
                    }
                    dgvAdult.Refresh();

                    lblAdult.Top = dgvChild.Top + dgvChild.Height + 20;
                    dgvAdult.Top = lblAdult.Top + lblAdult.Height + 10;
                    dgvAdult.Height = (intAdultCount * 50) + 60;
                }
                //read family files and load into datagridview
                var fList = new List<string>();
                for (i = 0; i < lstFamilyFiles.Count(); i++)
                {
                    string strread = lstFamilyFiles[i];
                    intFamilyCount++;
                    var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            fList.Add(line);
                        }
                    }
                    fileStream.Close();

                    cipherText = fList[0].ToString();
                    decryptData();
                    string[] splitDD = plainText.ToString().Split(',');
                    int j = 0;
                    while (j < splitDD.Count())
                    {
                        dgvFamily.Rows[intFamilyCount - 1].Cells[j].Value = splitDD[j];
                        j = j + 1;
                    }
                    dgvFamily.Refresh();

                    lblFamily.Top = dgvAdult.Top + dgvAdult.Height + 20;
                    dgvFamily.Top = lblFamily.Top + lblFamily.Height + 10;
                    dgvFamily.Height = (intFamilyCount * 50) + 60;
                }
            }
            else
            {
                //search ftp site, download files and display on datagridview
                using (var sftp = new SftpClient(Host, Port, Username, Password))
                {
                    try
                    {
                        sftp.Connect(); //connect to server
                        child_list = sftp.ListDirectory(Properties.Settings.Default.giRFChild).Where(f => !f.IsDirectory).Select(f => f.Name).ToList();
                        adult_list = sftp.ListDirectory(Properties.Settings.Default.giRFAdult).Where(f => !f.IsDirectory).Select(f => f.Name).ToList();
                        family_list = sftp.ListDirectory(Properties.Settings.Default.giRFFamily).Where(f => !f.IsDirectory).Select(f => f.Name).ToList();

                        for (i = 0; i < child_list.Count; i++)
                        {
                            if (child_list[i].ToString().Contains(lblRef.Text.ToString()) && child_list[i].ToString().Contains(".txt"))
                            {
                                var cList = new List<string>();
                                intChildCount = intChildCount + 1;
                                dgvChild.RowCount = intChildCount;
                                c = Properties.Settings.Default.giRFChild + "/" + child_list[i]; //update download file from sftp
                                b = gc + "\\" + child_list[i];//update download folder to pc 
                                                              //  try
                                                              //  {
                                using (var file = File.OpenWrite(b))
                                {
                                    sftp.DownloadFile(c, file);//download file
                                }

                                string strread = b;
                                var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        cList.Add(line);
                                    }
                                }
                                fileStream.Close();

                                cipherText = cList[0].ToString();
                                decryptData();
                                string[] splitDD = plainText.ToString().Split(',');
                                int j = 0;
                                while (j < splitDD.Count())
                                {
                                    dgvChild.Rows[intChildCount - 1].Cells[j].Value = splitDD[j];
                                    j = j + 1;
                                }
                                dgvChild.Refresh();

                                dgvChild.Height = (intChildCount * 50) + 60;
                            }
                        }

                        for (i = 0; i < adult_list.Count; i++)
                        {
                            if (adult_list[i].ToString().Contains(lblRef.Text.ToString()) && adult_list[i].ToString().Contains(".txt"))
                            {
                                var aList = new List<string>();
                                intAdultCount = intAdultCount + 1;
                                dgvAdult.RowCount = intAdultCount;
                                c = Properties.Settings.Default.giRFAdult + "/" + adult_list[i]; //update download file from sftp
                                b = ga + "\\" + adult_list[i];//update download folder to pc 
                                                              //  try
                                                              //  {
                                using (var file = File.OpenWrite(b))
                                {
                                    sftp.DownloadFile(c, file);//download file
                                }

                                string strread = b;
                                var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        aList.Add(line);
                                    }
                                }
                                fileStream.Close();

                                cipherText = aList[0].ToString();
                                decryptData();
                                string[] splitDD = plainText.ToString().Split(',');
                                int j = 0;
                                while (j < splitDD.Count())
                                {
                                    dgvAdult.Rows[intAdultCount - 1].Cells[j].Value = splitDD[j];
                                    j = j + 1;

                                }
                                dgvAdult.Refresh();

                                lblAdult.Top = dgvChild.Top + dgvChild.Height + 20;
                                dgvAdult.Top = lblAdult.Top + lblAdult.Height + 10;
                                dgvAdult.Height = (intAdultCount * 50) + 60;

                            }
                        }

                        for (i = 0; i < family_list.Count; i++)
                        {
                            if (family_list[i].ToString().Contains(lblRef.Text.ToString()) && family_list[i].ToString().Contains(".txt"))
                            {
                                var fList = new List<string>();
                                intFamilyCount = intFamilyCount + 1;
                                dgvChild.RowCount = intFamilyCount;
                                c = Properties.Settings.Default.giRFFamily + "/" + family_list[i]; //update download file from sftp
                                b = gf + "\\" + family_list[i];//update download folder to pc 
                                                               //  try
                                                               //  {
                                using (var file = File.OpenWrite(b))
                                {
                                    sftp.DownloadFile(c, file);//download file
                                }

                                string strread = b;
                                var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        fList.Add(line);
                                    }
                                }
                                fileStream.Close();

                                cipherText = fList[0].ToString();
                                decryptData();
                                string[] splitDD = plainText.ToString().Split(',');
                                int j = 0;
                                while (j < splitDD.Count())
                                {
                                    dgvFamily.Rows[intFamilyCount -1].Cells[j+2].Value = splitDD[j];
                                    j = j + 1;
                                }
                                dgvFamily.Refresh();

                                lblFam.Top = dgvAdult.Top + dgvAdult.Height + 20;
                                dgvFamily.Top = lblFam.Top + lblFamily.Height + 10;
                                dgvFamily.Height = (intFamilyCount * 50) + 60;
                            }
                        }


                        sftp.Disconnect();
                    }
                    catch
                    {
                        MessageBox.Show("Problem connecting to host, opening file and decrypting, please check settings and connection before trying again", "Error ash1912", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void decryptData()
        {
            //decrypt the file

            string password = Properties.Settings.Default.giDCPW;

            // Create sha256 hash
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

            // Create secret IV
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }
        }

        private void tmClock_Tick(object sender, EventArgs e)
        {

        }

        private void cmdLeft_Click(object sender, EventArgs e)
        {
            string strMessage = "Are you sure the " + lblFamily.Text + " family has left?";
            DialogResult dg = MessageBox.Show(strMessage, "Family Left", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                lblFamily.Text = "";
                lblRef.Text = "";
                cmdLeft.Visible = false;
                dgvChild.Rows.Clear();
                dgvChild.Refresh();
                dgvAdult.Rows.Clear();
                dgvAdult.Refresh();
                dgvFamily.Rows.Clear();
                dgvFamily.Refresh();
                lblStatus.Text = "Awaiting Family";
                updateController();
            }
        }

        private void updateController()
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmdShowHidden_Click(object sender, EventArgs e)
        {
            if (cmdShowHidden.Text == "Show Hidden")
            {
                i = 0;
                while (i < dgvChild.ColumnCount)
                {
                    dgvChild.Columns[i].Visible = true;
                    i = i + 1;
                }
                i = 0;
                while (i < dgvAdult.ColumnCount)
                {
                    dgvAdult.Columns[i].Visible = true;
                    i = i + 1;
                }
                i = 0;
                while (i < dgvFamily.ColumnCount)
                {
                    dgvFamily.Columns[i].Visible = true;
                    i = i + 1;
                }
                cmdShowHidden.Text = "Hide Information";
            }
            else
            {
                cmdShowHidden.Text = "Show Hidden";
                loadQuestions();
            }
        }

        private void loadSettings()
        {
            this.Text = "Grotto " + Properties.Settings.Default.giNo.ToString() + " Information";
            tmMonitor.Interval = Properties.Settings.Default.giMonitorInt;
            tmMonitor.Start();
            loadQuestions();
            loadStatus();

            lblStatus.Text = "Awaiting Family";
            txtGrottoNo.Text = Properties.Settings.Default.giNo.ToString();
            txtSantaActor.Text = Properties.Settings.Default.giSantaActor.ToString();
            txtElfActor.Text = Properties.Settings.Default.giElfActor.ToString();
            txtElfName.Text = Properties.Settings.Default.giElfName.ToString();

            bLoaded = true;
        }

        private void cmdUpdateStatus_Click(object sender, EventArgs e)
        {
            if (bLoaded==true && cboStatus.Text != "")
            {
                var list = new List<string>();
                bool bRead = false;
                string b = Properties.Settings.Default.giMonitorLoc.ToString() + "\\GrottoInfo.txt";

                try
                {
                    var fileStream = new FileStream(b, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            list.Add(line);
                        }
                    }
                    fileStream.Close();
                    bRead = true;
                }
                catch
                {
                    bRead = false;
                    tmMonitor.Stop();
                    MessageBox.Show("Can't read grotto Info File", "File read error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                if (bRead==true)
                {
                    Random rand = new Random();
                    int intRand = rand.Next(99999);
                    string strTemp = intRand.ToString() + "," + txtSantaActor.Text + "," + txtElfName.Text + "," + txtElfActor.Text + "," + cboStatus.Text;
                    list[Properties.Settings.Default.giNo - 1] = strTemp;

                    TextWriter tw = new StreamWriter(b);
                    foreach (string s in list)
                    {
                        tw.WriteLine(s);
                    }
                    tw.Close();
                }
            }
        }

        private void loadStatus()
        {
            cboStatus.Items.Clear();
            string[] strTemp = Properties.Settings.Default.giStatusList.ToString().Split(',');
            for (i=0;i<strTemp.Count();i++)
            {
                cboStatus.Items.Add(strTemp[i]);
            }
            cboStatus.Text = "Closed";
        }

        private void loadQuestions()
        {
            var list = new List<string>();

            string strread = Properties.Settings.Default.giQuestionFile + "\\Questions.txt";
            var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            fileStream.Close();

            string[] lstChild = list[0].ToString().Split(',');
            string[] lstAdult = list[1].ToString().Split(',');
            string[] lstFamily = list[2].ToString().Split(',');

            for (i = 0; i < lstChild.Count(); i++)
            {
                dgvChild.Columns[i].HeaderText = lstChild[i].ToString();
            }
            string[] strChildHidden = Properties.Settings.Default.giChildChecked.ToString().Split(',');
            for (i = 0; i < strChildHidden.Count(); i++)
            {
                if (strChildHidden[i] == "0")
                {
                    dgvChild.Columns[i].Visible = false;
                }
            }

            for (i = 0; i < lstAdult.Count(); i++)
            {
                dgvAdult.Columns[i].HeaderText = lstAdult[i].ToString();
            }
            string[] strAdultHidden = Properties.Settings.Default.giAdultChecked.ToString().Split(',');
            for (i = 0; i < strAdultHidden.Count(); i++)
            {
                if (strAdultHidden[i] == "0")
                {
                    dgvAdult.Columns[i].Visible = false;
                }
            }

            for (i = 0; i < lstFamily.Count(); i++)
            {
                dgvFamily.Columns[i].HeaderText = lstFamily[i].ToString();
            }
            string[] strFamilyHidden = Properties.Settings.Default.giFamilyChecked.ToString().Split(',');
            for (i = 0; i < strFamilyHidden.Count(); i++)
            {
                if (strFamilyHidden[i] == "0")
                {
                    dgvFamily.Columns[i].Visible = false;
                }
            }
        }
    }
}
