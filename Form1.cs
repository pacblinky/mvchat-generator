using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Windows.Forms;
using NAudio.Wave;
using System.ComponentModel;

namespace mvchat_generator
{
    public partial class Form1 : Form
    {
        public static List<string> FilePaths { get; set; }
        public static BindingList<VCSound> Sounds { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FilePaths = new List<string>();
            Sounds = new BindingList<VCSound>();
            SoundsList.DataSource = Sounds;
        }

        private void SelectFiles_btn_Click(object sender, EventArgs e)
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
                FilePaths.Clear();
                foreach (string FileName in OFD.FileNames)
                {
                    FilePaths.Add(FileName);
                }
                SelectFiles_btn.Text = $"Selected {FilePaths.Count} files";
            }
        }

        private void Generate_btn_Click(object sender, EventArgs e)
        {
            if (FilePaths.Count <= 0)
            {
                MessageBox.Show("Select something to generate from dummy", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (string.IsNullOrEmpty(Offset_input.Text))
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
                foreach (string FilePath in FilePaths)
                {
                    try
                    {
                        using (ZipArchive Archive = ZipFile.OpenRead(FilePath))
                        {
                            foreach (ZipArchiveEntry entry in Archive.Entries)
                            {

                                if (entry.FullName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                                {
                                    if(NoDup_check.Checked == true)
                                    {
                                        if(Sounds.ToList().FindIndex(Sound => Sound.Source == entry.FullName) > -1)
                                        {
                                            continue;
                                        }
                                    }

                                    Sounds.Add(new VCSound(Offset, entry.FullName, SoundsText_check.Checked ? Path.GetFileNameWithoutExtension(entry.Name) : ""));
                                    Offset++;
                                }
                            }
                            Archive.Dispose();
                        }
                    }
                    catch (Exception ex) { MessageBox.Show($"{FilePath}{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Can't open selected file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                if (Offset != LastOffset)
                {
                    MessageBox.Show($"Added {Offset - LastOffset} sounds", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TotalAdded_lbl.Text = $"Total added sounds: {Sounds.Count}";
                    LastOffset_lbl.Text = $"Last used offset: {LastOffset} - {Offset} is used";
                    Offset_input.Text = "";
                }
            }
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            if (Sounds.Count <= 0)
            {
                MessageBox.Show("You have nothing to save (0 sounds)", "Generate something first", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SaveFileDialog SFD = new SaveFileDialog
                {
                    Title = "Save file",
                    FileName = "Untitled.mvchat",
                    Filter = "mvchat (*.mvchat)|*.mvchat|All files (*.*)|*.*"
                };
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder SB = new StringBuilder();
                    foreach (VCSound Sound in Sounds)
                    {
                        SB.AppendFormat("{1}{0}{{{0}\tnumber\t\"{1}\"{0}\ttext\t\"{2}\"{0}{0}\ten{0}\t{{{0}\t\tdefault\t\"yes\"{0}{0}\t\tmale{0}\t\t{{{0}\t\t\tsound\t\"{3}\"{0}\t\t}}{0}\t}}{0}}}{0}{0}", Environment.NewLine, Sound.Number.ToString(), Sound.Text, Sound.Source);
                    }
                    string Output = SB.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                    SB.Clear();
                    try
                    {
                        StreamWriter SW = new StreamWriter(SFD.FileName);
                        SW.Write(Output);
                        SW.Dispose();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Can't save your mvchat file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void Reset_btn_Click(object sender, EventArgs e)
        {
            FilePaths.Clear();
            Sounds.Clear();
            Offset_input.Text = "";
            SelectFiles_btn.Text = "Select";
            LastOffset_lbl.Text = "Last used offset: None";
            TotalAdded_lbl.Text = "Total added sounds: 0";
        }

        private void AudioLimit_check_CheckedChanged(object sender, EventArgs e)
        {
            AudioLimit_box.Enabled = AudioLimit_check.Checked;
        }

        private static TimeSpan GetAudioLength(string FileName)
        {
            TimeSpan AudioTime = new TimeSpan();
            if(FileName.EndsWith(".mp3"))
            {
                AudioTime = new Mp3FileReader(FileName).TotalTime;
            }
            else if (FileName.EndsWith(".wav"))
            {
                AudioTime = new AudioFileReader(FileName).TotalTime;
            }

            return AudioTime;
        }
    }
    public struct VCSound
    {
        public int Number { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }

        public VCSound(int number, string source, string text)
        {
            this.Number = number;
            this.Source = source;
            this.Text = text;
        }
    }
}
