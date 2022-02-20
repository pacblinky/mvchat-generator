﻿using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Windows.Forms;
using System.ComponentModel;
using NAudio.Wave;

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
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePaths.Clear();
                foreach (string FileName in FileDialog.FileNames)
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
                MessageBox.Show("Select something to generate from dummy", "Nothing selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                TimeSpan Minimum = new TimeSpan();
                TimeSpan Maximum = new TimeSpan();
                if (AudioLimit_box.Enabled)
                {
                    if (string.IsNullOrEmpty(FolderDialog.SelectedPath))
                    {
                        MessageBox.Show("Select a directory to temporary extract audio files for audio time limit checking first", "Audio time limiter is on", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    DateTime dt = MinimumTime_input.Value;
                    DateTime dt2 = MaximumTime_input.Value;
                    Minimum = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                    Maximum = new TimeSpan(dt2.Hour, dt2.Minute, dt2.Second);
                }
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
                                    if (NoDup_check.Checked)
                                    {
                                        if (Sounds.ToList().FindIndex(Sound => Sound.Source == entry.FullName) > -1)
                                        {
                                            continue;
                                        }
                                    }

                                    if (AudioLimit_box.Enabled)
                                    {
                                        string FilePath = Path.Combine(FolderDialog.SelectedPath, entry.Name);
                                        if (!File.Exists(FilePath))
                                        {
                                            try
                                            {
                                                entry.ExtractToFile(FilePath);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show($"Can't extract {entry.FullName} at {FilePath} for audio limit checking{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Can't extract", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                continue;
                                            }
                                        }

                                        try
                                        {
                                            TimeSpan Audiotime = new TimeSpan();
                                            Audiotime = GetAudioTime(FilePath)
                                            if(!(Audiotime >= Minimum && Audiotime <= Maximum))
                                            {
                                                continue;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"\"{FilePath}\"{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Can't read audio file", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    catch (Exception ex) { MessageBox.Show($"\"{FilePath}\"{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Can't open selected file", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                if (SaveDialog.ShowDialog() == DialogResult.OK)
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
                        StreamWriter SW = new StreamWriter(SaveDialog.FileName);
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
            MaximumTime_input.Text = "00:00:00";
            MinimumTime_input.Text = "00:00:00";
            AudioLimit_check.Checked = false;
            NoDup_check.Checked = false;
            SoundsText_check.Checked = false;
            SelectFiles_btn.Text = "Select";
            LastOffset_lbl.Text = "Last used offset: None";
            TotalAdded_lbl.Text = "Total added sounds: 0";
        }

        private void AudioLimit_check_CheckedChanged(object sender, EventArgs e)
        {
            AudioLimit_box.Enabled = AudioLimit_check.Checked;
        }

        private static TimeSpan GetAudioTime(string FileName)
        {
            TimeSpan AudioTime = new TimeSpan();
            if (FileName.EndsWith(".mp3"))
            {
                AudioTime = new Mp3FileReader(FileName).TotalTime;
            }
            else if (FileName.EndsWith(".wav"))
            {
                AudioTime = new AudioFileReader(FileName).TotalTime;
            }

            return AudioTime;
        }

        private void SelectDirectory_btn_Click(object sender, EventArgs e)
        {
            FolderDialog.SelectedPath = Path.GetTempPath();
            if (FolderDialog.ShowDialog() == DialogResult.OK)
            {
                FolderDialog.SelectedPath = FolderDialog.SelectedPath;
                SelectDirectory_btn.Text = $"Selected{Environment.NewLine}{FolderDialog.SelectedPath}";
            }
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
