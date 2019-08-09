using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFMPEGUI
{
    public partial class Form1 : Form
    {

        public string newFileName;
        public string defaultFileName;
        public string defaultName;
        public string defaultDirectory;
        public string editedFileName;
        public string editedName;
        public string editedDirectory;
        public string oldFileName;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new object[] { ".avi", ".mp4", ".mpeg4", ".mts" });
            comboBox1.SelectedIndex = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckPathExists = true;
            fileDialog.CheckFileExists = true;
            //tylko jeden film póki co 
            fileDialog.Multiselect = false;

            fileDialog.Title = "Browse Video Files";
            fileDialog.Filter
                = "Video Files|*.mpeg4;*.avi;*.mkv;*.mp4;*.mts" ;
            fileDialog.ShowDialog();

            oldFileName = textBox1.Text = fileDialog.FileName;

            char[] separator = { '.' };
            
            String[] separated= fileDialog.FileName.Split(separator);

            defaultFileName = separated[0];
            for (int i = 1; i < separated.Length - 1; i++)
            {
                defaultFileName = defaultFileName + "." + separated[i];
            }

            editedFileName = defaultFileName;

            char[] separatorName = { '\\' };

            String[] separatedFolders = defaultFileName.Split(separatorName);

            defaultDirectory = separatedFolders[0];
            for(int i = 1; i < separatedFolders.Length - 1; i++)
            {
                defaultDirectory = defaultDirectory + "\\" + separatedFolders[i];
            }
            defaultName = separatedFolders[separatedFolders.Length - 1];
            
            textBox2.Text = defaultName;
            textBox3.Text = defaultDirectory;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.Enabled = true;
                textBox2.Text = editedName;
            }
            else
            {
                textBox2.Enabled = false;
                editedName = textBox2.Text;
                textBox2.Text = defaultName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            oldFileName = "\"" + textBox1.Text + "\"";
            newFileName = "\"" + textBox3.Text + textBox2.Text + comboBox1.SelectedItem.ToString() + "\"";

            if (oldFileName == "\"\"")
            {
                MessageBox.Show("Source file name can't be empty");
                return;
            }
            if(newFileName == "\"" + comboBox1.SelectedItem.ToString() + "\"")
            {
                MessageBox.Show("Destination file name can't be empty");
                return;
            }

            ProcessStartInfo info = new ProcessStartInfo("ffmpeg.exe");

            info.Arguments = "-i " + oldFileName + " " + newFileName;

            MessageBox.Show(info.Arguments);

            info.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = info;
            process.Start();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.ShowNewFolderButton = true;
            folderDialog.ShowDialog();
            string temp = folderDialog.SelectedPath;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox3.Enabled = true;
                textBox3.Text = editedDirectory;
            }
            else
            {
                textBox3.Enabled = false;
                editedDirectory = textBox3.Text;
                textBox3.Text = defaultDirectory;
            }
        }
    }
}
