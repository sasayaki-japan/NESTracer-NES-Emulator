using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    public partial class Form_MUSIC : Form
    {
        public static float[] KEY_SCALE_LIST = {
            34, 36, 38, 40, 42, 45, 48, 50, 53, 57,
            60, 64, 67, 71, 76, 80, 85, 90, 95, 101,
            107, 113, 120, 127, 135, 143, 151, 160, 170, 180,
            190, 202, 214, 227, 240, 254, 269, 285, 302, 320,
            339, 360, 381, 404, 428, 453, 480, 509, 539, 571,
            605, 641, 679, 719, 762, 807, 855, 906, 960, 1017,
            1078, 1142, 1210, 1282, 1358, 1438, 1524, 1615, 1711, 1812,
            1920, 2034, 2155, 2283, 2419, 2563, 2715, 2877, 3048, 3229,
            3421, 3625, 3840, 4069, 4310, 4567, 4838, 5126, 5431, 5754,
            6096, 6458, 6842, 7249, 7680 };
        public static int[] KEY_WIDTH = { 9, 6, 6, 6, 9, 9, 6, 6, 6, 6, 6, 9 };
        public static int[] KEY_POS = { 9, 15, 21, 27, 36, 45, 51, 57, 63, 69, 75, 84 };
        public static int[] KEY_COLOR = { 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0 };
        public static Bitmap g_orgBitmap;
        public static Bitmap g_cpyBitmap;
        public static int[] g_freq_out;
        public int g_screen_xpos;
        public int g_screen_ypos;
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_MUSIC()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            g_orgBitmap = new Bitmap(240, 674);
            g_cpyBitmap = new Bitmap(210, 674);
            g_freq_out = new int[11];
            pictureBox_view.Image = g_orgBitmap;


            //Keyboard display
            using (Graphics g = Graphics.FromImage(g_orgBitmap))
            {
                Brush brush = new SolidBrush(Color.White);
                Rectangle rect = new Rectangle(0, 0, 29, 672);
                g.FillRectangle(brush, rect);
                Pen pen = new Pen(Color.Black);
                for (int i = 0; i < 56; i++)
                {
                    rect = new Rectangle(0, i * 12, 28, 12);
                    g.DrawRectangle(pen, rect);
                }
                brush = new SolidBrush(Color.Black);
                for (int i = 0; i < 8; i++)
                {
                    rect = new Rectangle(0, i * 84 + 8, 15, 8);
                    g.FillRectangle(brush, rect);
                    rect = new Rectangle(0, i * 84 + 20, 15, 8);
                    g.FillRectangle(brush, rect);
                    rect = new Rectangle(0, i * 84 + 32, 15, 8);
                    g.FillRectangle(brush, rect);
                    rect = new Rectangle(0, i * 84 + 56, 15, 8);
                    g.FillRectangle(brush, rect);
                    rect = new Rectangle(0, i * 84 + 68, 15, 8);
                    g.FillRectangle(brush, rect);
                }
                Font font = new Font("Arial", 7);
                for (int i = 0; i < 8; i++)
                {
                    brush = Brushes.Black;
                    PointF point = new PointF(10, 661 - 84 * i);
                    g.DrawString("C" + i, font, brush, point);
                }
                //background display
                int dy = 672;
                for (int i = 0; i < 96; i++)
                {
                    if ((i % 2) == 0)
                    {
                        brush = new SolidBrush(Color.Azure);
                    }
                    else
                    {
                        brush = new SolidBrush(Color.Ivory);
                    }
                    dy -= KEY_WIDTH[i % 12];
                    rect = new Rectangle(29, dy, 211, KEY_WIDTH[i % 12]);
                    g.FillRectangle(brush, rect);
                }
                pictureBox_view.Image = (Bitmap)g_orgBitmap;
            }
        }
        //----------------------------------------------------------------
        //initialize
        //----------------------------------------------------------------
        public void initialize()
        {
            hScrollBar_Master.Value = nes_main.g_nes_apu.g_master_vol[0];
            hScrollBar_Square1.Value = nes_main.g_nes_apu.g_master_vol[1];
            hScrollBar_Square2.Value = nes_main.g_nes_apu.g_master_vol[2];
            hScrollBar_Triangle.Value = nes_main.g_nes_apu.g_master_vol[3];
            hScrollBar_Noise.Value = nes_main.g_nes_apu.g_master_vol[4];
            hScrollBar_Dmc.Value = nes_main.g_nes_apu.g_master_vol[5];
            checkBox_Master.Checked = nes_main.g_nes_apu.g_master_chk[0];
            checkBox_Square1.Checked = nes_main.g_nes_apu.g_master_chk[1];
            checkBox_Square2.Checked = nes_main.g_nes_apu.g_master_chk[2];
            checkBox_Triangle.Checked = nes_main.g_nes_apu.g_master_chk[3];
            checkBox_Noise.Checked = nes_main.g_nes_apu.g_master_chk[4];
            checkBox_Dmc.Checked = nes_main.g_nes_apu.g_master_chk[5];
            radio_stereo.Checked = nes_main.g_nes_apu.g_master_stereo;
        }
        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private void Form_MUSIC_Paint(object sender, PaintEventArgs e)
        {
            pictureBox_view.Invalidate();
        }
        private void pictureBox_view_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g_cpy = Graphics.FromImage(g_cpyBitmap))
            {
                Rectangle srcRect = new Rectangle(29, 0, 210, 672);
                Rectangle desRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);
                g_cpy.DrawImage(g_orgBitmap, desRect, srcRect, GraphicsUnit.Pixel);
            }
            using (Graphics g_org = Graphics.FromImage(g_orgBitmap))
            {
                Rectangle srcRect = new Rectangle(0, 0, 210, 672);
                Rectangle desRect = new Rectangle(30, 0, srcRect.Width, srcRect.Height);
                g_org.DrawImage(g_cpyBitmap, desRect, srcRect, GraphicsUnit.Pixel);
            }
            using (Graphics g_org = Graphics.FromImage(g_orgBitmap))
            {
                for (int i = 0; i < 5; i++)
                {
                    Brush brush;
                    switch (i)
                    {
                        case 0:
                            brush = new SolidBrush(Color.Salmon);
                            break;
                        case 1:
                            brush = new SolidBrush(Color.Gold);
                            break;
                        case 2:
                            brush = new SolidBrush(Color.Lime);
                            break;
                        case 3:
                            brush = new SolidBrush(Color.Aqua);
                            break;
                        default:
                            brush = new SolidBrush(Color.SteelBlue);
                            break;
                    }
                    int w_freq = g_freq_out[i];
                    if (0 < w_freq)
                    {
                        int w_ley_number = key_number_chk(w_freq);
                        int dy = 672 - ((int)(w_ley_number / 12) * 84) - KEY_POS[w_ley_number % 12];
                        Rectangle rect = new Rectangle(30, dy, 2, KEY_WIDTH[w_ley_number % 12]);
                        g_org.FillRectangle(brush, rect);
                    }
                }
            }
        }
        private int key_number_chk(float freq)
        {
            int w_out = KEY_SCALE_LIST.Length;
            for (int i = 0; i < KEY_SCALE_LIST.Length; i++)
            {
                if (freq <= KEY_SCALE_LIST[i])
                {
                    w_out = i;
                    break;
                }
            }
            return w_out;
        }
        //----------------------------------------------------------------
        ///Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void hScrollBar_Master_Scroll(object sender, ScrollEventArgs e)
        {
            nes_main.g_nes_apu.g_master_vol[0] = hScrollBar_Master.Value;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void hScrollBar_Square1_Scroll(object sender, ScrollEventArgs e)
        {
            nes_main.g_nes_apu.g_master_vol[1] = hScrollBar_Square1.Value;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void hScrollBar_Square2_Scroll(object sender, ScrollEventArgs e)
        {
            nes_main.g_nes_apu.g_master_vol[2] = hScrollBar_Square2.Value;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void hScrollBar_Triangle_Scroll(object sender, ScrollEventArgs e)
        {
            nes_main.g_nes_apu.g_master_vol[3] = hScrollBar_Triangle.Value;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void hScrollBar_Noise_Scroll(object sender, ScrollEventArgs e)
        {
            nes_main.g_nes_apu.g_master_vol[4] = hScrollBar_Noise.Value;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void hScrollBar_Dmc_Scroll(object sender, ScrollEventArgs e)
        {
            nes_main.g_nes_apu.g_master_vol[5] = hScrollBar_Dmc.Value;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void checkBox_Master_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_chk[0] = checkBox_Master.Checked;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void checkBox_Square1_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_chk[1] = checkBox_Square1.Checked;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void checkBox_Square2_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_chk[2] = checkBox_Square2.Checked;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void checkBox_Triangle_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_chk[3] = checkBox_Triangle.Checked;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void checkBox_Noise_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_chk[4] = checkBox_Noise.Checked;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void checkBox_Dmc_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_chk[5] = checkBox_Dmc.Checked;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void radio_stereo_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_stereo = true;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void radio_mono_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_nes_apu.g_master_stereo = false;
            nes_main.g_nes_apu.setting();
            nes_main.write_setting();
        }

        private void Form_MUSIC_FormClosing(object sender, FormClosingEventArgs e)
        {
            nes_main.g_music_enable = false;
            nes_main.g_form_setting.update();
            nes_main.write_setting();
            e.Cancel = true;
        }

        private void Form_MUSIC_ResizeEnd(object sender, EventArgs e)
        {
            var currentPosition = this.Location;
            g_screen_xpos = currentPosition.X;
            g_screen_ypos = currentPosition.Y;
            nes_main.write_setting();
        }

        private void Form_MUSIC_Shown(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
        }
    }
}
