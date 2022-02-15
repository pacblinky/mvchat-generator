using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Windows.Forms;

namespace mvchat_generator
{
    public partial class Form1 : Form
    {
        private static List<string> FilePaths;
        private static List<VCSound> Sounds;

        public Form1()
        {
            InitializeComponent();
            FilePaths = new List<string>();
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
                FilePaths.Clear();
                foreach (string FileName in OFD.FileNames)
                {
                    FilePaths.Add(FileName);
                }
                SelectFiles_btn.Text = $"Selected {FilePaths.Count} files";
            }
        }

        public void Generate_btn_Click(object sender, EventArgs e)
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
                                    Sounds.Add(new VCSound(Offset, entry.FullName, SoundsText_check.Checked ? Path.GetFileNameWithoutExtension(entry.Name) : ""));
                                    Offset++;
                                }
                            }
                            Archive.Dispose();
                        }
                    }
                    catch (Exception ex) { MessageBox.Show($"{FilePath}{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Can't open selected file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                if (Sounds.Count > 0)
                {
                    MessageBox.Show($"Added {Offset - LastOffset} sounds", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TotalAdded_lbl.Text = $"Total added sounds: {Sounds.Count}";
                    LastOffset_lbl.Text = $"Last used offset: {LastOffset} - {Offset} is used";
                    Offset_input.Text = "";
                    Sounds = Sounds.OrderBy(sound => sound.Number).ToList();
                }
            }
        }

        public void Save_btn_Click(object sender, EventArgs e)
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
                        SB.AppendFormat("{1}{0}{{{0}\tnumber\t\"{1}\"{0}\ttext\t\"{2}\"{0}{0}\ten{0}\t{{{0}\t\tdefault\t\"yes\"{0}{0}\t\tmale{0}\t\t{{{0}\t\t\tsound\t\"{3}\"{0}\t\t}}{0}\t}}{0}}}{0}{0}", Environment.NewLine,Sound.Number.ToString(),Sound.Text,Sound.Source);
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

        public void Reset_btn_Click(object sender, EventArgs e)
        {
            FilePaths.Clear();
            Sounds.Clear();
            Offset_input.Text = "";
            SelectFiles_btn.Text = "Select";
            LastOffset_lbl.Text = "Last used offset: None";
            TotalAdded_lbl.Text = "Total added sounds: 0";
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
