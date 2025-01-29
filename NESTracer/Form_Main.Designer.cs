namespace NESTracer
{
    partial class Form_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            settingToolStripMenuItem = new ToolStripMenuItem();
            SettingMenuItem1 = new ToolStripMenuItem();
            hardResetMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel_game = new Panel();
            pictureBox_game = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel_game.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_game).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(256, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingToolStripMenuItem
            // 
            settingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SettingMenuItem1, hardResetMenuItem });
            settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            settingToolStripMenuItem.Size = new Size(56, 20);
            settingToolStripMenuItem.Text = "Setting";
            // 
            // SettingMenuItem1
            // 
            SettingMenuItem1.Name = "SettingMenuItem1";
            SettingMenuItem1.ShortcutKeys = Keys.F4;
            SettingMenuItem1.Size = new Size(156, 22);
            SettingMenuItem1.Text = "Setting";
            SettingMenuItem1.Click += SettingMenuItem1_Click;
            // 
            // hardResetMenuItem
            // 
            hardResetMenuItem.Name = "hardResetMenuItem";
            hardResetMenuItem.ShortcutKeys = Keys.F12;
            hardResetMenuItem.Size = new Size(156, 22);
            hardResetMenuItem.Text = "Hard Reset";
            hardResetMenuItem.Click += hardResetMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(50, 20);
            aboutToolStripMenuItem.Text = "about";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 264);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(256, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel_game
            // 
            panel_game.Controls.Add(pictureBox_game);
            panel_game.Dock = DockStyle.Fill;
            panel_game.Location = new Point(0, 24);
            panel_game.Name = "panel_game";
            panel_game.Size = new Size(256, 240);
            panel_game.TabIndex = 2;
            // 
            // pictureBox_game
            // 
            pictureBox_game.Dock = DockStyle.Fill;
            pictureBox_game.Location = new Point(0, 0);
            pictureBox_game.Name = "pictureBox_game";
            pictureBox_game.Size = new Size(256, 240);
            pictureBox_game.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_game.TabIndex = 0;
            pictureBox_game.TabStop = false;
            pictureBox_game.Paint += pictureBox_game_Paint;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(256, 286);
            Controls.Add(panel_game);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Name = "Form_Main";
            Text = "NES Tracer";
            Load += Form1_Load;
            ResizeEnd += Form_Main_ResizeEnd;
            SizeChanged += Form_Main_SizeChanged;
            KeyDown += Form_Main_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel_game.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_game).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private StatusStrip statusStrip1;
        private Panel panel_game;
        private PictureBox pictureBox_game;
        private ToolStripMenuItem settingToolStripMenuItem;
        private ToolStripMenuItem SettingMenuItem1;
        private ToolStripMenuItem hardResetMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private OpenFileDialog openFileDialog1;
    }
}
