namespace NESTracer
{
    partial class Form_Code_Bank
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
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            codeRefleshToolStripMenuItem = new ToolStripMenuItem();
            pictureBox_code = new PictureBox();
            hScrollBar_code = new HScrollBar();
            vScrollBar_code = new VScrollBar();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_code).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(numericUpDown1);
            splitContainer1.Panel1.Controls.Add(menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pictureBox_code);
            splitContainer1.Panel2.Controls.Add(hScrollBar_code);
            splitContainer1.Panel2.Controls.Add(vScrollBar_code);
            splitContainer1.Size = new Size(739, 532);
            splitContainer1.SplitterDistance = 25;
            splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(625, 5);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 1;
            label1.Text = "bank";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(663, 2);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(59, 23);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(739, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { codeRefleshToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(45, 20);
            toolStripMenuItem1.Text = "trace";
            // 
            // codeRefleshToolStripMenuItem
            // 
            codeRefleshToolStripMenuItem.Name = "codeRefleshToolStripMenuItem";
            codeRefleshToolStripMenuItem.ShortcutKeys = Keys.F10;
            codeRefleshToolStripMenuItem.Size = new Size(180, 22);
            codeRefleshToolStripMenuItem.Text = "code reflesh";
            codeRefleshToolStripMenuItem.Click += toolStripMenuItem1_Click;
            // 
            // pictureBox_code
            // 
            pictureBox_code.BorderStyle = BorderStyle.FixedSingle;
            pictureBox_code.Dock = DockStyle.Fill;
            pictureBox_code.Location = new Point(0, 0);
            pictureBox_code.Name = "pictureBox_code";
            pictureBox_code.Size = new Size(722, 486);
            pictureBox_code.TabIndex = 10;
            pictureBox_code.TabStop = false;
            pictureBox_code.Paint += pictureBox_code_paint;
            // 
            // hScrollBar_code
            // 
            hScrollBar_code.Dock = DockStyle.Bottom;
            hScrollBar_code.Location = new Point(0, 486);
            hScrollBar_code.Name = "hScrollBar_code";
            hScrollBar_code.Size = new Size(722, 17);
            hScrollBar_code.TabIndex = 9;
            hScrollBar_code.Scroll += hScrollBar_code_Scroll;
            // 
            // vScrollBar_code
            // 
            vScrollBar_code.Dock = DockStyle.Right;
            vScrollBar_code.Location = new Point(722, 0);
            vScrollBar_code.Maximum = 32767;
            vScrollBar_code.Name = "vScrollBar_code";
            vScrollBar_code.Size = new Size(17, 503);
            vScrollBar_code.TabIndex = 7;
            vScrollBar_code.Scroll += vScrollBar_code_Scroll;
            // 
            // Form_Code_Bank
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(739, 532);
            Controls.Add(splitContainer1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form_Code_Bank";
            Text = "Code Bank View";
            FormClosing += Form_Bank_Code_FormClosing;
            Shown += Form_Bank_Code_Shown;
            ResizeEnd += Form_Bank_Code_ResizeEnd;
            KeyDown += Form_Code_Bank_KeyDown;
            Resize += Form_Bank_Code_Resize;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_code).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private VScrollBar vScrollBar_code;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private PictureBox pictureBox_code;
        private HScrollBar hScrollBar_code;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem codeRefleshToolStripMenuItem;
    }
}