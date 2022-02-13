using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Windows.Forms;

namespace mvchat_generator
{
    public partial class Form1 : Form
    {
        private static List<string> Files;
        private static List<VCSound> Sounds;

        public Form1()
        {
            InitializeComponent();
            Files = new List<string>();
            Sounds = new List<VCSound>();
        }

        public void SelectFiles_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files (*.*)|*.*|pk3 files (*.pk3)|*.pk3|zip files (*.zip)|*.zip",
                FilterIndex = 0,
                Title = "Select files"
            };
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                Files.Clear();
                foreach (string file in OFD.FileNames)
                {
                    Files.Add(file);
                }
                Generate_btn.Enabled = true;
                SelectFiles_btn.Text = "Selected " + Files.Count.ToString() + " files";
            }
        }

        public void Generate_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Offset_input.Text))
            {
                MessageBox.Show("Offset is the number that the first sound will have like if your offset is set to 10 the first sound will be 10 and the one after will be 11", "Please enter a offset first", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (int.TryParse(Offset_input.Text, out int Offset) == false)
            {
                MessageBox.Show("The offset has to be a number", "Invalid offset", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                int LastOffset = Offset;
                ZipArchive Archive;
                foreach (string FilePath in Files)
                {
                    try
                    {
                        Archive = ZipFile.OpenRead(FilePath);
                        foreach (ZipArchiveEntry entry in Archive.Entries)
                        {

                            if (entry.FullName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                            {
                                Sounds.Add(new VCSound(Offset, entry.FullName, SoundsText_check.Checked ? Path.GetFileNameWithoutExtension(entry.Name) : ""));
                                Offset++;
                            }
                        }
                        Archive.Dispose();
                    }
                    catch (Exception ex) { MessageBox.Show(FilePath + "\n\n" + ex.Message, "Can't open selected file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                if (Sounds.Count > 0)
                {
                    MessageBox.Show("Added " + (Offset - LastOffset).ToString() + " sounds");
                    Save_btn.Enabled = true;
                    TotalAdded_lbl.Text = "Total added sounds: " + Sounds.Count.ToString();
                    LastOffset_lbl.Text = "Last used offset: " + LastOffset.ToString() + " - " + Offset.ToString() + " is used";
                    Offset_input.Text = "";
                    Sounds = Sounds.OrderBy(sound => sound.Number).ToList();
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
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                string Output = "";
                foreach (VCSound Sound in Sounds)
                {
                    string SoundNumber = Sound.Number.ToString();
                    Output += SoundNumber + "\n{\n\tnumber\t\"" + SoundNumber + "\"\n\ttext\t\"" + Sound.Text + "\"\n\n\ten\n\t{\n\t\tdefault\t\"yes\"\n\n\t\tmale\n\t\t{\n\t\t\tsound\t\"" + Sound.Source + "\"\n\t\t}\n\t}\n}\n\n";
                }
                try
                {
                    StreamWriter SW = new StreamWriter(SFD.FileName);
                    SW.Write(Output);
                    SW.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Can't save your mvchat file",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            }
        }

        public void Reset_btn_Click(object sender, EventArgs e)
        {
            Files.Clear();
            Sounds.Clear();
            Offset_input.Text = "";
            SelectFiles_btn.Text = "Select files";
            LastOffset_lbl.Text = "Last used offset: None";
            TotalAdded_lbl.Text = "Total added sounds: 0";
            Generate_btn.Enabled = false;
            Save_btn.Enabled = false;
        }
    }
    struct VCSound
    {
        public int Number;
        public string Source;
        public string Text;

        public VCSound(int number, string source, string text)
        {
            this.Number = number;
            this.Source = source;
            this.Text = text;
        }
    }
}
