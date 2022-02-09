using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;
using System.IO;

namespace mvchat_generator
{
    public partial class Form1 : Form
    {
        public static List<string> FilePaths;
        public static List<Sound> Sounds;
        public int LastOffset;
        public int Offset;

        public Form1()
        {
            FilePaths = new List<string>();
            Sounds = new List<Sound>();
            InitializeComponent();
        }

        public void SelectFiles_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "pk3 files (*.pk3)|*.pk3|zip files (*.zip)|*.zip|All files (*.*)|*.*",
                Title = "Select files"
            };
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in OFD.FileNames)
                {
                    FilePaths.Add(file);
                }
                Generate_btn.Enabled = true;
                SelectFiles_btn.Text = "Selected " + FilePaths.Count.ToString() + " files";
            }
        }

        public void Generate_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Offset_input.Text))
            {
                MessageBox.Show("Please enter a offset first");
            }
            else
            {
                Offset = Decimal.ToInt32(Offset_input.Value);
                LastOffset = Offset;
                if (FilePaths.Count > 0)
                {
                    ZipArchive Archive;
                    foreach (string FilePath in FilePaths)
                    {
                        Archive = ZipFile.OpenRead(FilePath);
                        foreach (ZipArchiveEntry entry in Archive.Entries)
                        {

                            if (entry.FullName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                            {
                                Sounds.Add(new Sound(Offset,entry.FullName,SoundsText_check.Checked?Path.GetFileNameWithoutExtension(entry.Name):""));
                                Offset++;
                            }
                        }
                        Archive.Dispose();
                    }
                    Save_btn.Enabled = true;
                    TotalAdded_lbl.Text = "Total added sounds: " + Sounds.Count.ToString();
                    LastOffset_lbl.Text = "Last used offset: " + LastOffset.ToString() + " - " + Offset.ToString() + " is used";
                }
            }
        }

        public void Save_btn_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog
            {
                Title = "Save file",
                FileName = "Untitled.mvchat",
                Filter = "mvchat (*.mvchat)|*.mvchat|All files (*.*)|*.*"
            };
            if(SFD.ShowDialog() == DialogResult.OK)
            {
                string Output = "";
                foreach(Sound Soundd in Sounds)
                {
                    string SoundNumber = Soundd.Number.ToString();
                    Output += SoundNumber + "\n{\n\tnumber\t\"" + SoundNumber + "\"\n\ttext\t\"" + Soundd.Text + "\"\n\n\ten\n\t{\n\t\tdefault\t\"yes\"\n\n\t\tmale\n\t\t{\n\t\t\tsound\t\"" + Soundd.Source + "\"\n\t\t}\n\t}\n}\n\n";
                }
                StreamWriter SW = new StreamWriter(SFD.FileName);
                SW.Write(Output);
                SW.Close();
            }
        }

        public void Reset_btn_Click(object sender, EventArgs e)
        {
            FilePaths.Clear();
            Sounds.Clear();
            Offset_input.Value = 0;
            SelectFiles_btn.Text = "Select files";
            LastOffset_lbl.Text = "Last used offset: None";
            TotalAdded_lbl.Text = "Total added sounds: 0";
            Generate_btn.Enabled = false;
            Save_btn.Enabled = false;
        }

        public class Sound
        {
            public int Number;
            public string Source;
            public string Text;

            public Sound(int number, string source, string text)
            {
                this.Number = number;
                this.Source = source;
                this.Text = text;
            }
        }
    }
}
