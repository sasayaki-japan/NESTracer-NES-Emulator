namespace NESTracer
{
    partial class Form_MUSIC
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
            radio_stereo = new RadioButton();
            radio_mono = new RadioButton();
            hScrollBar_Dmc = new HScrollBar();
            checkBox_Dmc = new CheckBox();
            label5 = new Label();
            hScrollBar_Noise = new HScrollBar();
            checkBox_Noise = new CheckBox();
            label6 = new Label();
            hScrollBar_Triangle = new HScrollBar();
            checkBox_Triangle = new CheckBox();
            label3 = new Label();
            hScrollBar_Square2 = new HScrollBar();
            checkBox_Square2 = new CheckBox();
            label4 = new Label();
            hScrollBar_Square1 = new HScrollBar();
            checkBox_Square1 = new CheckBox();
            label2 = new Label();
            hScrollBar_Master = new HScrollBar();
            checkBox_Master = new CheckBox();
            label1 = new Label();
            pictureBox_view = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_view).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radio_stereo);
            groupBox1.Controls.Add(radio_mono);
            groupBox1.Controls.Add(hScrollBar_Dmc);
            groupBox1.Controls.Add(checkBox_Dmc);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(hScrollBar_Noise);
            groupBox1.Controls.Add(checkBox_Noise);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(hScrollBar_Triangle);
            groupBox1.Controls.Add(checkBox_Triangle);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(hScrollBar_Square2);
            groupBox1.Controls.Add(checkBox_Square2);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(hScrollBar_Square1);
            groupBox1.Controls.Add(checkBox_Square1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(hScrollBar_Master);
            groupBox1.Controls.Add(checkBox_Master);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(179, 191);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Channels";
            // 
            // radio_stereo
            // 
            radio_stereo.AutoSize = true;
            radio_stereo.Location = new Point(79, 166);
            radio_stereo.Name = "radio_stereo";
            radio_stereo.Size = new Size(57, 19);
            radio_stereo.TabIndex = 20;
            radio_stereo.Text = "stereo";
            radio_stereo.UseVisualStyleBackColor = true;
            radio_stereo.CheckedChanged += radio_stereo_CheckedChanged;
            // 
            // radio_mono
            // 
            radio_mono.AutoSize = true;
            radio_mono.Checked = true;
            radio_mono.Location = new Point(17, 166);
            radio_mono.Name = "radio_mono";
            radio_mono.Size = new Size(56, 19);
            radio_mono.TabIndex = 19;
            radio_mono.TabStop = true;
            radio_mono.Text = "mono";
            radio_mono.UseVisualStyleBackColor = true;
            radio_mono.CheckedChanged += radio_mono_CheckedChanged;
            // 
            // hScrollBar_Dmc
            // 
            hScrollBar_Dmc.Location = new Point(73, 133);
            hScrollBar_Dmc.Maximum = 109;
            hScrollBar_Dmc.Name = "hScrollBar_Dmc";
            hScrollBar_Dmc.Size = new Size(100, 15);
            hScrollBar_Dmc.TabIndex = 18;
            hScrollBar_Dmc.Scroll += hScrollBar_Dmc_Scroll;
            // 
            // checkBox_Dmc
            // 
            checkBox_Dmc.AutoSize = true;
            checkBox_Dmc.Location = new Point(55, 134);
            checkBox_Dmc.Name = "checkBox_Dmc";
            checkBox_Dmc.Size = new Size(15, 14);
            checkBox_Dmc.TabIndex = 17;
            checkBox_Dmc.UseVisualStyleBackColor = true;
            checkBox_Dmc.CheckedChanged += checkBox_Dmc_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.SteelBlue;
            label5.Location = new Point(6, 134);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 16;
            label5.Text = "Dmc";
            // 
            // hScrollBar_Noise
            // 
            hScrollBar_Noise.Location = new Point(73, 110);
            hScrollBar_Noise.Maximum = 109;
            hScrollBar_Noise.Name = "hScrollBar_Noise";
            hScrollBar_Noise.Size = new Size(100, 15);
            hScrollBar_Noise.TabIndex = 15;
            hScrollBar_Noise.Scroll += hScrollBar_Noise_Scroll;
            // 
            // checkBox_Noise
            // 
            checkBox_Noise.AutoSize = true;
            checkBox_Noise.Location = new Point(55, 111);
            checkBox_Noise.Name = "checkBox_Noise";
            checkBox_Noise.Size = new Size(15, 14);
            checkBox_Noise.TabIndex = 14;
            checkBox_Noise.UseVisualStyleBackColor = true;
            checkBox_Noise.CheckedChanged += checkBox_Noise_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Aqua;
            label6.Location = new Point(6, 111);
            label6.Name = "label6";
            label6.Size = new Size(37, 15);
            label6.TabIndex = 13;
            label6.Text = "Noise";
            // 
            // hScrollBar_Triangle
            // 
            hScrollBar_Triangle.Location = new Point(73, 88);
            hScrollBar_Triangle.Maximum = 109;
            hScrollBar_Triangle.Name = "hScrollBar_Triangle";
            hScrollBar_Triangle.Size = new Size(100, 15);
            hScrollBar_Triangle.TabIndex = 12;
            hScrollBar_Triangle.Scroll += hScrollBar_Triangle_Scroll;
            // 
            // checkBox_Triangle
            // 
            checkBox_Triangle.AutoSize = true;
            checkBox_Triangle.Location = new Point(55, 90);
            checkBox_Triangle.Name = "checkBox_Triangle";
            checkBox_Triangle.Size = new Size(15, 14);
            checkBox_Triangle.TabIndex = 11;
            checkBox_Triangle.UseVisualStyleBackColor = true;
            checkBox_Triangle.CheckedChanged += checkBox_Triangle_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Lime;
            label3.Location = new Point(6, 90);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 10;
            label3.Text = "Triangle";
            // 
            // hScrollBar_Square2
            // 
            hScrollBar_Square2.Location = new Point(73, 66);
            hScrollBar_Square2.Maximum = 109;
            hScrollBar_Square2.Name = "hScrollBar_Square2";
            hScrollBar_Square2.Size = new Size(100, 15);
            hScrollBar_Square2.TabIndex = 9;
            hScrollBar_Square2.Scroll += hScrollBar_Square2_Scroll;
            // 
            // checkBox_Square2
            // 
            checkBox_Square2.AutoSize = true;
            checkBox_Square2.Location = new Point(55, 66);
            checkBox_Square2.Name = "checkBox_Square2";
            checkBox_Square2.Size = new Size(15, 14);
            checkBox_Square2.TabIndex = 8;
            checkBox_Square2.UseVisualStyleBackColor = true;
            checkBox_Square2.CheckedChanged += checkBox_Square2_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Gold;
            label4.Location = new Point(6, 66);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 7;
            label4.Text = "Square2";
            // 
            // hScrollBar_Square1
            // 
            hScrollBar_Square1.Location = new Point(73, 43);
            hScrollBar_Square1.Maximum = 109;
            hScrollBar_Square1.Name = "hScrollBar_Square1";
            hScrollBar_Square1.Size = new Size(100, 15);
            hScrollBar_Square1.TabIndex = 6;
            hScrollBar_Square1.Scroll += hScrollBar_Square1_Scroll;
            // 
            // checkBox_Square1
            // 
            checkBox_Square1.AutoSize = true;
            checkBox_Square1.Location = new Point(55, 44);
            checkBox_Square1.Name = "checkBox_Square1";
            checkBox_Square1.Size = new Size(15, 14);
            checkBox_Square1.TabIndex = 5;
            checkBox_Square1.UseVisualStyleBackColor = true;
            checkBox_Square1.CheckedChanged += checkBox_Square1_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Salmon;
            label2.Location = new Point(6, 43);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 4;
            label2.Text = "Square1";
            // 
            // hScrollBar_Master
            // 
            hScrollBar_Master.Location = new Point(73, 19);
            hScrollBar_Master.Maximum = 109;
            hScrollBar_Master.Name = "hScrollBar_Master";
            hScrollBar_Master.Size = new Size(100, 15);
            hScrollBar_Master.TabIndex = 3;
            hScrollBar_Master.Scroll += hScrollBar_Master_Scroll;
            // 
            // checkBox_Master
            // 
            checkBox_Master.AutoSize = true;
            checkBox_Master.Location = new Point(55, 19);
            checkBox_Master.Name = "checkBox_Master";
            checkBox_Master.Size = new Size(15, 14);
            checkBox_Master.TabIndex = 1;
            checkBox_Master.UseVisualStyleBackColor = true;
            checkBox_Master.CheckedChanged += checkBox_Master_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "Master";
            // 
            // pictureBox_view
            // 
            pictureBox_view.BorderStyle = BorderStyle.FixedSingle;
            pictureBox_view.Location = new Point(227, 9);
            pictureBox_view.Margin = new Padding(0);
            pictureBox_view.Name = "pictureBox_view";
            pictureBox_view.Size = new Size(242, 674);
            pictureBox_view.TabIndex = 1;
            pictureBox_view.TabStop = false;
            pictureBox_view.Paint += pictureBox_view_Paint;
            // 
            // Form_MUSIC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 696);
            Controls.Add(pictureBox_view);
            Controls.Add(groupBox1);
            Name = "Form_MUSIC";
            Text = "Music View";
            FormClosing += Form_MUSIC_FormClosing;
            Shown += Form_MUSIC_Shown;
            ResizeEnd += Form_MUSIC_ResizeEnd;
            Paint += Form_MUSIC_Paint;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_view).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private PictureBox pictureBox_view;
        private CheckBox checkBox_Master;
        private Label label1;
        private HScrollBar hScrollBar_Master;
        private HScrollBar hScrollBar_Dmc;
        private CheckBox checkBox_Dmc;
        private Label label5;
        private HScrollBar hScrollBar_Noise;
        private CheckBox checkBox_Noise;
        private Label label6;
        private HScrollBar hScrollBar_Triangle;
        private CheckBox checkBox_Triangle;
        private Label label3;
        private HScrollBar hScrollBar_Square2;
        private CheckBox checkBox_Square2;
        private Label label4;
        private HScrollBar hScrollBar_Square1;
        private CheckBox checkBox_Square1;
        private Label label2;
        private RadioButton radio_stereo;
        private RadioButton radio_mono;
    }
}