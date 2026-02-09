using MDTracer;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace NESTracer
{
    public partial class Form_Main : Form
    {
        public static Form_Main Instance { get; private set; }
        public static int g_screen_size_x;
        public static int g_screen_size_y;
        public static int g_screen_xpos;
        public static int g_screen_ypos;
        public static string[] g_file_name;
        private static bool g_filelist_view;
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_Main()
        {
            InitializeComponent();
            Instance = this;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
        //----------------------------------------------------------------
        //Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            g_file_name = new string[9];
            pictureBox_game.Image = new Bitmap(320, 240);
            pictureBox_game.BackColor = Color.Black;
            nes_main.initialize();
            nes_main.read_setting();
            nes_main.g_form_io.initialize();
            nes_main.g_form_io.rescan();
            nes_main.g_form_music.initialize();
            g_filelist_view = true;
            this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
            BringToFront();
        }

        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
            if (g_filelist_view == false)
            {
                int w_x = this.Size.Width;
                int w_y = this.Size.Height;
                if ((w_x - 16) != g_screen_size_x)
                {
                    w_y = (int)(240 * ((w_x - 16) / 256.0f)) + 85;
                }
                else
                if ((w_y - 85) != g_screen_size_y)
                {
                    w_x = (int)(256 * ((w_y - 85) / 240.0f)) + 16;
                }
                this.Size = new Size(w_x, w_y);
            }
        }
        private void Form_Main_ResizeEnd(object sender, EventArgs e)
        {
            if (g_filelist_view == false)
            {
                g_screen_xpos = this.Location.X;
                g_screen_ypos = this.Location.Y;
                g_screen_size_x = this.Width - 16;
                g_screen_size_y = this.Height - 85;
                nes_main.write_setting();
            }
        }
        private void pictureBox_game_Paint(object sender, PaintEventArgs e)
        {
            if (g_filelist_view == true)
            {
                Font wfont = new Font("ÇlÇr ÉSÉVÉbÉN", 10);
                Brush wbrush = Brushes.White;
                e.Graphics.DrawString("file select", wfont, wbrush, new PointF(20, 20));
                e.Graphics.DrawString("(F key: Select in File Explorer)", wfont, wbrush, new PointF(30, 40));
                for (int i = 0; i < 9; i++)
                {
                    string w_filename = Path.GetFileName(g_file_name[i]);
                    e.Graphics.DrawString((i + 1) + ": " + w_filename, wfont, wbrush, new PointF(30, 60 + i * 15));
                }
            }
        }
        //----------------------------------------------------------------
        //Event Handling: key operations
        //----------------------------------------------------------------
        private void Form_Main_KeyDown(object sender, KeyEventArgs e)
        {
            string w_filename = "";
            switch (e.KeyCode)
            {
                case Keys.F:
                    if (g_filelist_view == true)
                    {
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            w_filename = openFileDialog1.FileName;
                        }
                    }
                    break;
                case Keys.D1: w_filename = g_file_name[0]; break;
                case Keys.D2: w_filename = g_file_name[1]; break;
                case Keys.D3: w_filename = g_file_name[2]; break;
                case Keys.D4: w_filename = g_file_name[3]; break;
                case Keys.D5: w_filename = g_file_name[4]; break;
                case Keys.D6: w_filename = g_file_name[5]; break;
                case Keys.D7: w_filename = g_file_name[6]; break;
                case Keys.D8: w_filename = g_file_name[7]; break;
                case Keys.D9: w_filename = g_file_name[8]; break;
            }
            if ((g_filelist_view == true) && (w_filename != null) && (w_filename != ""))
            {
                if (true == nes_main.run(w_filename))
                {
                    file_list_update(w_filename);
                    this.MaximumSize = new Size(0, 0);
                    this.MinimumSize = new Size(0, 0);
                    this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
                    this.Width = g_screen_size_x + 16;
                    this.Height = g_screen_size_y + 85;
                    g_filelist_view = false;
                }
            }
        }
        private void file_list_update(string in_file)
        {
            for (int i = 0; i < 8; i++)
            {
                if (g_file_name[i] == in_file)
                {
                    for (int m = i; m < 8; m++)
                    {
                        g_file_name[m] = g_file_name[m + 1];
                    }
                    break;
                }
            }
            for (int i = 8; i >= 1; i--)
            {
                g_file_name[i] = g_file_name[i - 1];
            }
            g_file_name[0] = in_file;
            nes_main.write_setting();
        }
        //----------------------------------------------------------------
        //Event Handling: menu
        //----------------------------------------------------------------
        private void SettingMenuItem1_Click(object sender, EventArgs e)
        {
            nes_main.g_form_setting.Show();
        }

        private void hardResetMenuItem_Click(object sender, EventArgs e)
        {
            nes_main.g_hard_reset_req = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newForm = new Form_About();
            newForm.ShowDialog();
        }

        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private readonly object g_bitmapLock = new object();
        public void picture_update(int in_cpu)
        {
            g_filelist_view = false;
            toolStripStatusLabel1.Text = "task usage:" + in_cpu + "%";
            lock (g_bitmapLock)
            {
                int w_bitmap_x = panel_game.ClientSize.Width;
                int w_bitmap_y = panel_game.ClientSize.Height;
                float w_cx = (float)256 / (float)w_bitmap_x;
                float w_cy = (float)240 / (float)w_bitmap_y;
                Bitmap g_work_bitmap = new Bitmap(w_bitmap_x, w_bitmap_y);
                BitmapData game_bmpData = g_work_bitmap.LockBits(new Rectangle(0, 0, g_work_bitmap.Width, g_work_bitmap.Height),
                                            ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                IntPtr dest_ptr = game_bmpData.Scan0;
                int dest_stride = game_bmpData.Stride;
                const int bytesPerPixel = 4;
                unsafe
                {
                    float w_dy = 0;
                    for (int wy = 0; wy < w_bitmap_y; wy++)
                    {
                        uint* pixel = (uint*)dest_ptr;
                        float w_dx = 0;
                        int w_base = (int)w_dy * 256;
                        for (int wx = 0; wx < w_bitmap_x; wx++)
                        {
                            *pixel = nes_main.g_nes_ppu.g_game_screen[w_base + (int)w_dx];
                            w_dx += w_cx;
                            pixel = (uint*)((IntPtr)pixel + bytesPerPixel);
                        }
                        dest_ptr += dest_stride;
                        w_dy += w_cy;
                    }
                }
                g_work_bitmap.UnlockBits(game_bmpData);
                try
                {
                    this.Invoke((Action)(() =>
                    {
                        pictureBox_game.Image?.Dispose();
                        pictureBox_game.Image = new Bitmap(g_work_bitmap);
                        pictureBox_game.Width = w_bitmap_x;
                        pictureBox_game.Height = w_bitmap_y;
                    }));
                }
                catch { }
            }
        }
    }
}
