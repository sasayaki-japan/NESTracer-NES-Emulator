namespace NESTracer
{
    partial class Form_Setting
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
            groupBox1 = new GroupBox();
            checkBox_register = new CheckBox();
            checkBox_ppu = new CheckBox();
            checkBox_pattern = new CheckBox();
            checkBox_apu = new CheckBox();
            checkBox_io = new CheckBox();
            checkBox_bank = new CheckBox();
            checkBox_code = new CheckBox();
            groupBox2 = new GroupBox();
            checkBox_sip = new CheckBox();
            checkBox_fsb = new CheckBox();
            groupBox3 = new GroupBox();
            comboBox_rendering = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            comboBox_videoformat = new ComboBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox_register);
            groupBox1.Controls.Add(checkBox_ppu);
            groupBox1.Controls.Add(checkBox_pattern);
            groupBox1.Controls.Add(checkBox_apu);
            groupBox1.Controls.Add(checkBox_io);
            groupBox1.Controls.Add(checkBox_bank);
            groupBox1.Controls.Add(checkBox_code);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(176, 180);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Window";
            // 
            // checkBox_register
            // 
            checkBox_register.AutoSize = true;
            checkBox_register.Location = new Point(97, 72);
            checkBox_register.Name = "checkBox_register";
            checkBox_register.Size = new Size(68, 19);
            checkBox_register.TabIndex = 9;
            checkBox_register.Text = "Register";
            checkBox_register.UseVisualStyleBackColor = true;
            checkBox_register.CheckedChanged += checkBox_register_CheckedChanged_1;
            // 
            // checkBox_ppu
            // 
            checkBox_ppu.AutoSize = true;
            checkBox_ppu.Location = new Point(24, 22);
            checkBox_ppu.Name = "checkBox_ppu";
            checkBox_ppu.Size = new Size(47, 19);
            checkBox_ppu.TabIndex = 10;
            checkBox_ppu.Text = "Ppu";
            checkBox_ppu.UseVisualStyleBackColor = true;
            checkBox_ppu.CheckedChanged += checkBox_ppu_CheckedChanged;
            // 
            // checkBox_pattern
            // 
            checkBox_pattern.AutoSize = true;
            checkBox_pattern.Location = new Point(23, 97);
            checkBox_pattern.Name = "checkBox_pattern";
            checkBox_pattern.Size = new Size(64, 19);
            checkBox_pattern.TabIndex = 4;
            checkBox_pattern.Text = "Pattern";
            checkBox_pattern.UseVisualStyleBackColor = true;
            checkBox_pattern.CheckedChanged += checkBox_pattern_CheckedChanged_1;
            // 
            // checkBox_apu
            // 
            checkBox_apu.AutoSize = true;
            checkBox_apu.Location = new Point(23, 47);
            checkBox_apu.Name = "checkBox_apu";
            checkBox_apu.Size = new Size(58, 19);
            checkBox_apu.TabIndex = 3;
            checkBox_apu.Text = "Music";
            checkBox_apu.UseVisualStyleBackColor = true;
            checkBox_apu.CheckedChanged += checkBox_apu_CheckedChanged;
            // 
            // checkBox_io
            // 
            checkBox_io.AutoSize = true;
            checkBox_io.Location = new Point(24, 72);
            checkBox_io.Name = "checkBox_io";
            checkBox_io.Size = new Size(43, 19);
            checkBox_io.TabIndex = 2;
            checkBox_io.Text = "I/O";
            checkBox_io.UseVisualStyleBackColor = true;
            checkBox_io.CheckedChanged += checkBox_io_CheckedChanged_1;
            // 
            // checkBox_bank
            // 
            checkBox_bank.AutoSize = true;
            checkBox_bank.Location = new Point(97, 47);
            checkBox_bank.Name = "checkBox_bank";
            checkBox_bank.Size = new Size(52, 19);
            checkBox_bank.TabIndex = 1;
            checkBox_bank.Text = "Bank";
            checkBox_bank.UseVisualStyleBackColor = true;
            checkBox_bank.CheckedChanged += checkBox_bank_CheckedChanged;
            // 
            // checkBox_code
            // 
            checkBox_code.AutoSize = true;
            checkBox_code.Location = new Point(97, 22);
            checkBox_code.Name = "checkBox_code";
            checkBox_code.Size = new Size(53, 19);
            checkBox_code.TabIndex = 0;
            checkBox_code.Text = "Code";
            checkBox_code.UseVisualStyleBackColor = true;
            checkBox_code.CheckedChanged += checkBox_code_CheckedChanged_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBox_sip);
            groupBox2.Controls.Add(checkBox_fsb);
            groupBox2.Location = new Point(209, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 74);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Trace";
            // 
            // checkBox_sip
            // 
            checkBox_sip.AutoSize = true;
            checkBox_sip.Location = new Point(6, 47);
            checkBox_sip.Name = "checkBox_sip";
            checkBox_sip.Size = new Size(157, 19);
            checkBox_sip.TabIndex = 2;
            checkBox_sip.Text = "Skip interrupt processing";
            checkBox_sip.UseVisualStyleBackColor = true;
            checkBox_sip.CheckedChanged += checkBox_sip_CheckedChanged;
            // 
            // checkBox_fsb
            // 
            checkBox_fsb.AutoSize = true;
            checkBox_fsb.Location = new Point(6, 22);
            checkBox_fsb.Name = "checkBox_fsb";
            checkBox_fsb.Size = new Size(106, 19);
            checkBox_fsb.TabIndex = 1;
            checkBox_fsb.Text = "First Step Break";
            checkBox_fsb.UseVisualStyleBackColor = true;
            checkBox_fsb.CheckedChanged += checkBox_fsb_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(comboBox_rendering);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(comboBox_videoformat);
            groupBox3.Location = new Point(209, 92);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 100);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "system";
            // 
            // comboBox_rendering
            // 
            comboBox_rendering.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_rendering.Enabled = false;
            comboBox_rendering.FormattingEnabled = true;
            comboBox_rendering.Items.AddRange(new object[] { "CPU", "GPU" });
            comboBox_rendering.Location = new Point(110, 44);
            comboBox_rendering.Name = "comboBox_rendering";
            comboBox_rendering.Size = new Size(85, 23);
            comboBox_rendering.TabIndex = 7;
            comboBox_rendering.SelectedIndexChanged += comboBox_rendering_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 47);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 6;
            label2.Text = "Rendering mode";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 21);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 5;
            label1.Text = "Video Format";
            // 
            // comboBox_videoformat
            // 
            comboBox_videoformat.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_videoformat.Enabled = false;
            comboBox_videoformat.FormattingEnabled = true;
            comboBox_videoformat.Items.AddRange(new object[] { "NTSC", "PAL" });
            comboBox_videoformat.Location = new Point(109, 18);
            comboBox_videoformat.Name = "comboBox_videoformat";
            comboBox_videoformat.Size = new Size(85, 23);
            comboBox_videoformat.TabIndex = 4;
            comboBox_videoformat.SelectedIndexChanged += comboBox_videoformat_SelectedIndexChanged;
            // 
            // Form_Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 200);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form_Setting";
            Text = "Setting View";
            FormClosing += Form_Setting_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox checkBox_ppu;
        private CheckBox checkBox_pattern;
        private CheckBox checkBox4;
        private CheckBox checkBox_bank;
        private CheckBox checkBox_register;
        private CheckBox checkBox_io;
        private CheckBox checkBox_code;
        private GroupBox groupBox2;
        private CheckBox checkBox_fsb;
        private GroupBox groupBox3;
        private ComboBox comboBox_rendering;
        private Label label1;
        private ComboBox comboBox_videoformat;
        private CheckBox checkBox_sip;
        private CheckBox checkBox_apu;
        private Label label2;
    }
}