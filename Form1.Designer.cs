namespace mvchat_generator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Generate_btn = new System.Windows.Forms.Button();
            this.SoundsText_check = new System.Windows.Forms.CheckBox();
            this.Offset_lbl = new System.Windows.Forms.Label();
            this.LastOffset_lbl = new System.Windows.Forms.Label();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Reset_btn = new System.Windows.Forms.Button();
            this.SelectFiles_btn = new System.Windows.Forms.Button();
            this.TotalAdded_lbl = new System.Windows.Forms.Label();
            this.Offset_input = new System.Windows.Forms.TextBox();
            this.NoDup_check = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AudioLimit_check = new System.Windows.Forms.CheckBox();
            this.AudioLimit_box = new System.Windows.Forms.GroupBox();
            this.SelectDirectory_btn = new System.Windows.Forms.Button();
            this.MaximumTime_input = new System.Windows.Forms.DateTimePicker();
            this.MaximumTime_lbl = new System.Windows.Forms.Label();
            this.MinimumTime_lbl = new System.Windows.Forms.Label();
            this.MinimumTime_input = new System.Windows.Forms.DateTimePicker();
            this.SoundsList = new System.Windows.Forms.DataGridView();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.AudioLimit_box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoundsList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Generate from pk3 and zip files";
            // 
            // Generate_btn
            // 
            this.Generate_btn.Location = new System.Drawing.Point(169, 58);
            this.Generate_btn.Name = "Generate_btn";
            this.Generate_btn.Size = new System.Drawing.Size(97, 47);
            this.Generate_btn.TabIndex = 3;
            this.Generate_btn.Text = "Generate";
            this.Generate_btn.UseVisualStyleBackColor = true;
            this.Generate_btn.Click += new System.EventHandler(this.Generate_btn_Click);
            // 
            // SoundsText_check
            // 
            this.SoundsText_check.AutoSize = true;
            this.SoundsText_check.Location = new System.Drawing.Point(272, 57);
            this.SoundsText_check.Name = "SoundsText_check";
            this.SoundsText_check.Size = new System.Drawing.Size(206, 21);
            this.SoundsText_check.TabIndex = 4;
            this.SoundsText_check.Text = "Use sounds filename for text";
            this.SoundsText_check.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SoundsText_check.UseVisualStyleBackColor = true;
            // 
            // Offset_lbl
            // 
            this.Offset_lbl.AutoSize = true;
            this.Offset_lbl.Location = new System.Drawing.Point(51, 156);
            this.Offset_lbl.Name = "Offset_lbl";
            this.Offset_lbl.Size = new System.Drawing.Size(50, 17);
            this.Offset_lbl.TabIndex = 6;
            this.Offset_lbl.Text = "Offset:";
            // 
            // LastOffset_lbl
            // 
            this.LastOffset_lbl.AutoSize = true;
            this.LastOffset_lbl.Location = new System.Drawing.Point(213, 156);
            this.LastOffset_lbl.Name = "LastOffset_lbl";
            this.LastOffset_lbl.Size = new System.Drawing.Size(151, 17);
            this.LastOffset_lbl.TabIndex = 8;
            this.LastOffset_lbl.Text = "Last used offset: None";
            // 
            // Save_btn
            // 
            this.Save_btn.Location = new System.Drawing.Point(110, 428);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(97, 47);
            this.Save_btn.TabIndex = 16;
            this.Save_btn.Text = "Save";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Reset_btn
            // 
            this.Reset_btn.Location = new System.Drawing.Point(223, 428);
            this.Reset_btn.Name = "Reset_btn";
            this.Reset_btn.Size = new System.Drawing.Size(97, 47);
            this.Reset_btn.TabIndex = 17;
            this.Reset_btn.Text = "Reset";
            this.Reset_btn.UseVisualStyleBackColor = true;
            this.Reset_btn.Click += new System.EventHandler(this.Reset_btn_Click);
            // 
            // SelectFiles_btn
            // 
            this.SelectFiles_btn.Location = new System.Drawing.Point(63, 58);
            this.SelectFiles_btn.Name = "SelectFiles_btn";
            this.SelectFiles_btn.Size = new System.Drawing.Size(97, 47);
            this.SelectFiles_btn.TabIndex = 2;
            this.SelectFiles_btn.Text = "Select";
            this.SelectFiles_btn.UseVisualStyleBackColor = true;
            this.SelectFiles_btn.Click += new System.EventHandler(this.SelectFiles_btn_Click);
            // 
            // TotalAdded_lbl
            // 
            this.TotalAdded_lbl.AutoSize = true;
            this.TotalAdded_lbl.Location = new System.Drawing.Point(142, 490);
            this.TotalAdded_lbl.Name = "TotalAdded_lbl";
            this.TotalAdded_lbl.Size = new System.Drawing.Size(150, 17);
            this.TotalAdded_lbl.TabIndex = 18;
            this.TotalAdded_lbl.Text = "Total added sounds: 0";
            // 
            // Offset_input
            // 
            this.Offset_input.Location = new System.Drawing.Point(107, 153);
            this.Offset_input.Name = "Offset_input";
            this.Offset_input.Size = new System.Drawing.Size(100, 23);
            this.Offset_input.TabIndex = 7;
            // 
            // NoDup_check
            // 
            this.NoDup_check.AutoSize = true;
            this.NoDup_check.Location = new System.Drawing.Point(272, 84);
            this.NoDup_check.Name = "NoDup_check";
            this.NoDup_check.Size = new System.Drawing.Size(165, 21);
            this.NoDup_check.TabIndex = 5;
            this.NoDup_check.Text = "Do not add duplicates";
            this.NoDup_check.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AudioLimit_check);
            this.groupBox1.Controls.Add(this.AudioLimit_box);
            this.groupBox1.Controls.Add(this.SelectFiles_btn);
            this.groupBox1.Controls.Add(this.TotalAdded_lbl);
            this.groupBox1.Controls.Add(this.Offset_input);
            this.groupBox1.Controls.Add(this.Reset_btn);
            this.groupBox1.Controls.Add(this.NoDup_check);
            this.groupBox1.Controls.Add(this.Save_btn);
            this.groupBox1.Controls.Add(this.Generate_btn);
            this.groupBox1.Controls.Add(this.LastOffset_lbl);
            this.groupBox1.Controls.Add(this.SoundsText_check);
            this.groupBox1.Controls.Add(this.Offset_lbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 566);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // AudioLimit_check
            // 
            this.AudioLimit_check.AutoSize = true;
            this.AudioLimit_check.Location = new System.Drawing.Point(63, 207);
            this.AudioLimit_check.Name = "AudioLimit_check";
            this.AudioLimit_check.Size = new System.Drawing.Size(162, 21);
            this.AudioLimit_check.TabIndex = 9;
            this.AudioLimit_check.Text = "Use audio time limiter";
            this.AudioLimit_check.UseVisualStyleBackColor = true;
            this.AudioLimit_check.CheckedChanged += new System.EventHandler(this.AudioLimit_check_CheckedChanged);
            // 
            // AudioLimit_box
            // 
            this.AudioLimit_box.AutoSize = true;
            this.AudioLimit_box.Controls.Add(this.SelectDirectory_btn);
            this.AudioLimit_box.Controls.Add(this.MaximumTime_input);
            this.AudioLimit_box.Controls.Add(this.MaximumTime_lbl);
            this.AudioLimit_box.Controls.Add(this.MinimumTime_lbl);
            this.AudioLimit_box.Controls.Add(this.MinimumTime_input);
            this.AudioLimit_box.Enabled = false;
            this.AudioLimit_box.Location = new System.Drawing.Point(63, 220);
            this.AudioLimit_box.Name = "AudioLimit_box";
            this.AudioLimit_box.Size = new System.Drawing.Size(313, 174);
            this.AudioLimit_box.TabIndex = 10;
            this.AudioLimit_box.TabStop = false;
            // 
            // SelectDirectory_btn
            // 
            this.SelectDirectory_btn.AutoSize = true;
            this.SelectDirectory_btn.Location = new System.Drawing.Point(6, 105);
            this.SelectDirectory_btn.Name = "SelectDirectory_btn";
            this.SelectDirectory_btn.Size = new System.Drawing.Size(118, 47);
            this.SelectDirectory_btn.TabIndex = 15;
            this.SelectDirectory_btn.Text = "Select Directory";
            this.SelectDirectory_btn.UseVisualStyleBackColor = true;
            this.SelectDirectory_btn.Click += new System.EventHandler(this.SelectDirectory_btn_Click);
            // 
            // MaximumTime_input
            // 
            this.MaximumTime_input.CustomFormat = "HH:mm:ss";
            this.MaximumTime_input.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.MaximumTime_input.Location = new System.Drawing.Point(179, 48);
            this.MaximumTime_input.Name = "MaximumTime_input";
            this.MaximumTime_input.ShowUpDown = true;
            this.MaximumTime_input.Size = new System.Drawing.Size(116, 23);
            this.MaximumTime_input.TabIndex = 14;
            this.MaximumTime_input.Value = new System.DateTime(2022, 2, 19, 0, 0, 0, 0);
            // 
            // MaximumTime_lbl
            // 
            this.MaximumTime_lbl.AutoSize = true;
            this.MaximumTime_lbl.Location = new System.Drawing.Point(176, 28);
            this.MaximumTime_lbl.Name = "MaximumTime_lbl";
            this.MaximumTime_lbl.Size = new System.Drawing.Size(96, 17);
            this.MaximumTime_lbl.TabIndex = 13;
            this.MaximumTime_lbl.Text = "Maximum time";
            // 
            // MinimumTime_lbl
            // 
            this.MinimumTime_lbl.AutoSize = true;
            this.MinimumTime_lbl.Location = new System.Drawing.Point(25, 28);
            this.MinimumTime_lbl.Name = "MinimumTime_lbl";
            this.MinimumTime_lbl.Size = new System.Drawing.Size(93, 17);
            this.MinimumTime_lbl.TabIndex = 11;
            this.MinimumTime_lbl.Text = "Minimum time";
            // 
            // MinimumTime_input
            // 
            this.MinimumTime_input.CustomFormat = "HH:mm:ss";
            this.MinimumTime_input.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.MinimumTime_input.Location = new System.Drawing.Point(28, 48);
            this.MinimumTime_input.Name = "MinimumTime_input";
            this.MinimumTime_input.ShowUpDown = true;
            this.MinimumTime_input.Size = new System.Drawing.Size(116, 23);
            this.MinimumTime_input.TabIndex = 12;
            this.MinimumTime_input.Value = new System.DateTime(2022, 2, 19, 0, 0, 0, 0);
            // 
            // SoundsList
            // 
            this.SoundsList.AllowDrop = true;
            this.SoundsList.AllowUserToOrderColumns = true;
            this.SoundsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SoundsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SoundsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SoundsList.Location = new System.Drawing.Point(505, 12);
            this.SoundsList.Name = "SoundsList";
            this.SoundsList.Size = new System.Drawing.Size(561, 554);
            this.SoundsList.TabIndex = 19;
            // 
            // FileDialog
            // 
            this.FileDialog.Filter = "All files|*.*|pk3 files|*.pk3|zip files|*.zip";
            this.FileDialog.Multiselect = true;
            this.FileDialog.SupportMultiDottedExtensions = true;
            this.FileDialog.Title = "Select Files";
            // 
            // SaveDialog
            // 
            this.SaveDialog.DefaultExt = "mvchat";
            this.SaveDialog.FileName = "Untitled";
            this.SaveDialog.Filter = "mvchat|*.mvchat|All files|*.*";
            this.SaveDialog.Title = "Save File";
            // 
            // FolderDialog
            // 
            this.FolderDialog.Description = "Select a directory to temporary extract audio files for reading their time/length" +
    "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1078, 566);
            this.Controls.Add(this.SoundsList);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mvchat generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.AudioLimit_box.ResumeLayout(false);
            this.AudioLimit_box.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoundsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Generate_btn;
        private System.Windows.Forms.CheckBox SoundsText_check;
        private System.Windows.Forms.Label Offset_lbl;
        private System.Windows.Forms.Label LastOffset_lbl;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Reset_btn;
        private System.Windows.Forms.Button SelectFiles_btn;
        private System.Windows.Forms.Label TotalAdded_lbl;
        private System.Windows.Forms.TextBox Offset_input;
        private System.Windows.Forms.CheckBox NoDup_check;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView SoundsList;
        private System.Windows.Forms.Label MinimumTime_lbl;
        private System.Windows.Forms.Label MaximumTime_lbl;
        private System.Windows.Forms.DateTimePicker MinimumTime_input;
        private System.Windows.Forms.DateTimePicker MaximumTime_input;
        private System.Windows.Forms.GroupBox AudioLimit_box;
        private System.Windows.Forms.CheckBox AudioLimit_check;
        private System.Windows.Forms.OpenFileDialog FileDialog;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.FolderBrowserDialog FolderDialog;
        private System.Windows.Forms.Button SelectDirectory_btn;
    }
}