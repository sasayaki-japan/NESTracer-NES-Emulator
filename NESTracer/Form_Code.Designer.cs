namespace NESTracer
{
    partial class Form_Code
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
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            fileOpenToolStripMenuItem = new ToolStripMenuItem();
            runMenuItem = new ToolStripMenuItem();
            startToolStripMenuItem = new ToolStripMenuItem();
            stopMenuItem = new ToolStripMenuItem();
            stepOverMenuItem = new ToolStripMenuItem();
            stepInMenuItem1 = new ToolStripMenuItem();
            breakPointMenuItem = new ToolStripMenuItem();
            codeRefleshMenuItem = new ToolStripMenuItem();
            skipnextframeMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            hScrollBar_code = new HScrollBar();
            pictureBox_code = new PictureBox();
            vScrollBar_code = new VScrollBar();
            dataGridView_memory = new DataGridView();
            mode = new DataGridViewComboBoxColumn();
            address = new DataGridViewTextBoxColumn();
            val = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label3 = new Label();
            listBox_search = new ListBox();
            textBoxAddr = new TextBox();
            label2 = new Label();
            comboBox_comment2 = new ComboBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_code).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_memory).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem, runMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(935, 24);
            menuStrip1.TabIndex = 26;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fileOpenToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(49, 20);
            menuToolStripMenuItem.Text = "menu";
            // 
            // fileOpenToolStripMenuItem
            // 
            fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";
            fileOpenToolStripMenuItem.Size = new Size(119, 22);
            fileOpenToolStripMenuItem.Text = "file open";
            // 
            // runMenuItem
            // 
            runMenuItem.DropDownItems.AddRange(new ToolStripItem[] { startToolStripMenuItem, stopMenuItem, stepOverMenuItem, stepInMenuItem1, breakPointMenuItem, codeRefleshMenuItem, skipnextframeMenuItem });
            runMenuItem.Name = "runMenuItem";
            runMenuItem.Size = new Size(45, 20);
            runMenuItem.Text = "trace";
            runMenuItem.Click += runMenuItem_Click;
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.ShortcutKeys = Keys.F5;
            startToolStripMenuItem.Size = new Size(295, 22);
            startToolStripMenuItem.Text = "run";
            startToolStripMenuItem.Click += runMenuItem_Click;
            // 
            // stopMenuItem
            // 
            stopMenuItem.Name = "stopMenuItem";
            stopMenuItem.ShortcutKeys = Keys.F6;
            stopMenuItem.Size = new Size(295, 22);
            stopMenuItem.Text = "stop";
            stopMenuItem.Click += stopMenuItem_Click;
            // 
            // stepOverMenuItem
            // 
            stepOverMenuItem.Name = "stepOverMenuItem";
            stepOverMenuItem.ShortcutKeys = Keys.F7;
            stepOverMenuItem.Size = new Size(295, 22);
            stepOverMenuItem.Text = "step over";
            stepOverMenuItem.Click += stepOverMenuItem_Click;
            // 
            // stepInMenuItem1
            // 
            stepInMenuItem1.Name = "stepInMenuItem1";
            stepInMenuItem1.ShortcutKeys = Keys.F8;
            stepInMenuItem1.Size = new Size(295, 22);
            stepInMenuItem1.Text = "step in";
            stepInMenuItem1.Click += stepInMenuItem_Click;
            // 
            // breakPointMenuItem
            // 
            breakPointMenuItem.Name = "breakPointMenuItem";
            breakPointMenuItem.ShortcutKeys = Keys.F9;
            breakPointMenuItem.Size = new Size(295, 22);
            breakPointMenuItem.Text = "break point";
            breakPointMenuItem.Click += breakPointMenuItem_Click;
            // 
            // codeRefleshMenuItem
            // 
            codeRefleshMenuItem.Name = "codeRefleshMenuItem";
            codeRefleshMenuItem.ShortcutKeys = Keys.F10;
            codeRefleshMenuItem.Size = new Size(295, 22);
            codeRefleshMenuItem.Text = "code reflesh";
            codeRefleshMenuItem.Click += codeRefleshMenuItem_Click;
            // 
            // skipnextframeMenuItem
            // 
            skipnextframeMenuItem.Name = "skipnextframeMenuItem";
            skipnextframeMenuItem.ShortcutKeys = Keys.Control | Keys.F5;
            skipnextframeMenuItem.Size = new Size(295, 22);
            skipnextframeMenuItem.Text = "skip(next beginning of the frame)";
            skipnextframeMenuItem.Click += skipnextframeMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(hScrollBar_code);
            splitContainer1.Panel1.Controls.Add(pictureBox_code);
            splitContainer1.Panel1.Controls.Add(vScrollBar_code);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView_memory);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(listBox_search);
            splitContainer1.Panel2.Controls.Add(textBoxAddr);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(comboBox_comment2);
            splitContainer1.Size = new Size(935, 599);
            splitContainer1.SplitterDistance = 694;
            splitContainer1.TabIndex = 27;
            // 
            // hScrollBar_code
            // 
            hScrollBar_code.Dock = DockStyle.Bottom;
            hScrollBar_code.Location = new Point(0, 582);
            hScrollBar_code.Name = "hScrollBar_code";
            hScrollBar_code.Size = new Size(677, 17);
            hScrollBar_code.TabIndex = 9;
            hScrollBar_code.Scroll += hScrollBar_code_Scroll;
            // 
            // pictureBox_code
            // 
            pictureBox_code.BorderStyle = BorderStyle.FixedSingle;
            pictureBox_code.Dock = DockStyle.Fill;
            pictureBox_code.Location = new Point(0, 0);
            pictureBox_code.Name = "pictureBox_code";
            pictureBox_code.Size = new Size(677, 599);
            pictureBox_code.TabIndex = 8;
            pictureBox_code.TabStop = false;
            pictureBox_code.Paint += pictureBox_code_paint;
            pictureBox_code.MouseClick += pictureBox_code_MouseClick;
            pictureBox_code.MouseDoubleClick += pictureBox_code_MouseDoubleClick;
            // 
            // vScrollBar_code
            // 
            vScrollBar_code.Dock = DockStyle.Right;
            vScrollBar_code.Location = new Point(677, 0);
            vScrollBar_code.Maximum = 32767;
            vScrollBar_code.Name = "vScrollBar_code";
            vScrollBar_code.Size = new Size(17, 599);
            vScrollBar_code.TabIndex = 7;
            vScrollBar_code.Scroll += vScrollBar_code_Scroll;
            // 
            // dataGridView_memory
            // 
            dataGridView_memory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_memory.Columns.AddRange(new DataGridViewColumn[] { mode, address, val });
            dataGridView_memory.Location = new Point(7, 290);
            dataGridView_memory.MultiSelect = false;
            dataGridView_memory.Name = "dataGridView_memory";
            dataGridView_memory.RowHeadersVisible = false;
            dataGridView_memory.RowTemplate.Height = 25;
            dataGridView_memory.ScrollBars = ScrollBars.None;
            dataGridView_memory.ShowCellErrors = false;
            dataGridView_memory.ShowCellToolTips = false;
            dataGridView_memory.ShowEditingIcon = false;
            dataGridView_memory.ShowRowErrors = false;
            dataGridView_memory.Size = new Size(221, 225);
            dataGridView_memory.TabIndex = 35;
            dataGridView_memory.CellEndEdit += dataGridView_memory_CellEndEdit;
            // 
            // mode
            // 
            mode.HeaderText = "mode";
            mode.Items.AddRange(new object[] { "-", "read/write", "read", "write" });
            mode.Name = "mode";
            mode.Resizable = DataGridViewTriState.False;
            // 
            // address
            // 
            address.HeaderText = "address";
            address.Name = "address";
            address.Resizable = DataGridViewTriState.False;
            address.Width = 60;
            // 
            // val
            // 
            val.HeaderText = "val";
            val.Name = "val";
            val.ReadOnly = true;
            val.Resizable = DataGridViewTriState.False;
            val.Width = 60;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 272);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 34;
            label1.Text = "■memory  monitoring ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 33);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 24;
            label3.Text = "■comment1";
            // 
            // listBox_search
            // 
            listBox_search.FormattingEnabled = true;
            listBox_search.ItemHeight = 15;
            listBox_search.Location = new Point(7, 80);
            listBox_search.Name = "listBox_search";
            listBox_search.Size = new Size(120, 184);
            listBox_search.TabIndex = 23;
            listBox_search.Click += listBox_search_Click;
            listBox_search.DoubleClick += listBox_search_DoubleClick;
            // 
            // textBoxAddr
            // 
            textBoxAddr.Location = new Point(72, 3);
            textBoxAddr.MaxLength = 4;
            textBoxAddr.Name = "textBoxAddr";
            textBoxAddr.Size = new Size(36, 23);
            textBoxAddr.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 6);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 21;
            label2.Text = "■address";
            // 
            // comboBox_comment2
            // 
            comboBox_comment2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_comment2.FormattingEnabled = true;
            comboBox_comment2.Items.AddRange(new object[] { "", "PPU:PPU control($2000)", "PPU:PPU mask($2001)", "PPU:PPU status($2002)", "PPU:OAM address port($2003)", "PPU:OAM data port($2004)", "PPU:PPU scrolling position($2005)", "PPU:PPU address($2006)", "PPU:PPU data port($2007)", "APU:channe1:Pulse1($4000-3)", "APU:channe2:Pulse2($4004-7)", "APU:channe3:Triangle($4008-B)", "APU:channe4:Noise($400C～F)", "APU:channe5:DMC($4010～3)", "PPU: OAM DMA($4014)", "APU:status($4015)", "I/O:Joystick1/Joystick strobe($4016)", "I/O:Joystick2/Frame counter($4017)" });
            comboBox_comment2.Location = new Point(7, 51);
            comboBox_comment2.Name = "comboBox_comment2";
            comboBox_comment2.Size = new Size(227, 23);
            comboBox_comment2.TabIndex = 21;
            comboBox_comment2.SelectedIndexChanged += comboBox_comment2_SelectedIndexChanged;
            // 
            // Form_Code
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(935, 623);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            Name = "Form_Code";
            Text = "Code View";
            FormClosing += Form_Code_FormClosing;
            Shown += Form_Code_Shown;
            ResizeEnd += Form_Code_ResizeEnd;
            KeyDown += Form_Code_KeyDown;
            Resize += Form_Code_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_code).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_memory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listBox1;
        private DataGridView dataGridView_ppu;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem fileOpenToolStripMenuItem;
        private ToolStripMenuItem runMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopMenuItem;
        private ToolStripMenuItem stepOverMenuItem;
        private SplitContainer splitContainer1;
        private PictureBox pictureBox_code;
        private VScrollBar vScrollBar_code;
        private Label label3;
        private ListBox listBox_search;
        private TextBox textBoxAddr;
        private Label label2;
        private ComboBox comboBox_comment2;
        private ToolStripMenuItem codeRefleshMenuItem;
        private HScrollBar hScrollBar_code;
        private ToolStripMenuItem stepInMenuItem1;
        private ToolStripMenuItem breakPointMenuItem;
        private ToolStripMenuItem skipnextframeMenuItem;
        private DataGridView dataGridView_memory;
        private DataGridViewComboBoxColumn mode;
        private DataGridViewTextBoxColumn address;
        private DataGridViewTextBoxColumn val;
        private Label label1;
    }
}