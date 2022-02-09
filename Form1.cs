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
        private readonly List<string> _filePaths; // C# convention
        private readonly List<Sound> _sounds;
        private int _lastOffset; 
        private int _offset;

        public Form1()
        {
            _filePaths = new List<string>();
            _sounds = new List<Sound>();
            
            InitializeComponent();
        }

        public void SelectFiles_btn_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files (*.*)|*.*|pk3 files (*.pk3)|*.pk3|zip files (*.zip)|*.zip",
                FilterIndex = 0,
                Title = "Select files"
            };
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // can be single line
                // ofd.FileNames.ForEach(x => _filePaths.Add(file));
                foreach (var file in ofd.FileNames)
                {
                    _filePaths.Add(file);
                }
                
                Generate_btn.Enabled = true;
                SelectFiles_btn.Text = $"Selected {_filePaths.Count} files";
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
                // int is short for int32 (default)
                _offset = int.Parse(Offset_input.Text); // didn't check out ui but if you can enter strings here, use TryParse
                _lastOffset = _offset;
                
                if (_filePaths.Any())
                {
                    // should use "using" when using IO
                    // something like this: using (var zipArchive = ZipFile.OpenRead(FilePath))
                    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
                    
                    ZipArchive Archive;
                    
                    foreach (var filePath in _filePaths)
                    {
                        Archive = ZipFile.OpenRead(filePath);
                        
                        foreach (var entry in Archive.Entries)
                        {
                            if (entry.FullName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                            {
                                _sounds.Add(new Sound 
                                {
                                    Offset = _offset,
                                    Source = entry.FullName,
                                    Text = SoundsText_check.Checked ? Path.GetFileNameWithoutExtension(entry.Name) : string.empty
                                });
                                
                                _offset++;
                            }
                        }
                        
                        Archive.Dispose(); // can be removed if you use "using", it'll automatically dispose
                    }
                    
                    Save_btn.Enabled = true;
                    TotalAdded_lbl.Text = $"Total added sounds: {_sounds.Count}";
                    LastOffset_lbl.Text = $"Last used offset: {_lastOffset} - {_offset} is used";
                }
            }
        }

        public void Save_btn_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Title = "Save file",
                FileName = "Untitled.mvchat",
                Filter = "mvchat (*.mvchat)|*.mvchat|All files (*.*)|*.*"
            };
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var output = string.empty;
                
                foreach (var sound in _sounds)
                {
                    var soundNumber = sound.Number.ToString();
                    
                    // nice single line, but it's unreadable --> maybe use stringbuilder?
                    output += soundNumber + "\n{\n\tnumber\t\"" + soundNumber + "\"\n\ttext\t\"" + sound.Text + "\"\n\n\ten\n\t{\n\t\tdefault\t\"yes\"\n\n\t\tmale\n\t\t{\n\t\t\tsound\t\"" + sound.Source + "\"\n\t\t}\n\t}\n}\n\n";
                }
                
                // should use "using" when using IO
                // something like this: using (var streamwriter = new StreamWriter(sfd.FileName))
                // 
                var sw = new StreamWriter(sfd.FileName);
                sw.Write(output);
                sw.Close(); // can be removed if you use "using"
            }
        }

        public void Reset_btn_Click(object sender, EventArgs e)
        {
            _filePaths.Clear();
            _sounds.Clear();
            Offset_input.Text = string.empty;
            SelectFiles_btn.Text = "Select files";
            LastOffset_lbl.Text = "Last used offset: None";
            TotalAdded_lbl.Text = "Total added sounds: 0";
            Generate_btn.Enabled = false;
            Save_btn.Enabled = false;
        }

        public class Sound // should be separated in a new file
        {
            public int Number { get; set; }
            public string Source { get; set; }
            public string Text { get; set; }
        }
    }
}
