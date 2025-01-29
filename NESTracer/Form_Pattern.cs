using System;
using System.Drawing.Imaging;

namespace NESTracer
{
    public partial class Form_Pattern : Form
    {
        public int g_screen_xpos;
        public int g_screen_ypos;
        public Bitmap[] g_picture_list;
        private bool g_update_enable; 
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_Pattern()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            int wmap_num = nes_main.g_nes_mapper_control.g_chr_bank_num;
            hScrollBar_picturebox.Minimum = 0;
            hScrollBar_picturebox.Maximum = wmap_num - 1;
            hScrollBar_picturebox.LargeChange = 1;
        }
        //----------------------------------------------------------------
        //initialize
        //----------------------------------------------------------------
        public void update()
        {
            g_picture_list = new Bitmap[nes_main.g_nes_mapper_control.g_chr_bank_num];
            for (int i = 0; i < nes_main.g_nes_mapper_control.g_chr_bank_num; i++)
            {
                g_picture_list[i] = new Bitmap(256, 64);
                picture_make(g_picture_list[i], i);
            }
            picture_set(0);
            label_bankno.Text = "0";
            g_update_enable = true;
        }
        //----------------------------------------------------------------
        ///Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void hScrollBar_picturebox_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll)
            {
                int wmap_num = nes_main.g_nes_mapper_control.g_chr_bank_num;
                int w_cur = e.NewValue;
                if (w_cur < 0)
                {
                    w_cur = 0;
                }
                else
                if (w_cur >= wmap_num)
                {
                    w_cur = wmap_num - 1;
                }
                picture_set(w_cur);
                label_bankno.Text = w_cur.ToString();
                this.Invalidate();
            }
        }
        private void Form_Pattern_FormClosing(object sender, FormClosingEventArgs e)
        {
            nes_main.g_pattern_enable = false;
            nes_main.g_form_setting.update();
            nes_main.write_setting();
            e.Cancel = true;
        }
        private void Form_Pattern_ResizeEnd(object sender, EventArgs e)
        {
            var currentPosition = this.Location;
            g_screen_xpos = currentPosition.X;
            g_screen_ypos = currentPosition.Y;
            nes_main.write_setting();
        }
        private void Form_Pattern_Shown(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
        }
        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private void Form_Pattern_Paint(object sender, PaintEventArgs e)
        {
            if(g_update_enable==true)
            {
                picture_view();
                pictureBox.Invalidate();
            }
        }
        //----------------------------------------------------------------
        //sub
        //----------------------------------------------------------------
        public void picture_make(Bitmap in_bitmap, int in_bank)
        {
            Color[] w_color = new Color[4];
            w_color[0] = nes_main.g_nes_ppu.PALLET_BASE[0x0f];
            w_color[1] = nes_main.g_nes_ppu.PALLET_BASE[0x18];
            w_color[2] = nes_main.g_nes_ppu.PALLET_BASE[0x28];
            w_color[3] = nes_main.g_nes_ppu.PALLET_BASE[0x38];
            BitmapData bmpData = in_bitmap.LockBits(new Rectangle(0, 0, in_bitmap.Width, in_bitmap.Height),
                                                 ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            IntPtr ptr = bmpData.Scan0;
            int stride = bmpData.Stride;
            unsafe
            {
                byte* pixels = (byte*)ptr;
                for (int wy = 0; wy < 4; wy++)
                {
                    for (int wx = 0; wx < 16; wx++)
                    {
                        int wchr = wx + (wy * 16);
                        int woffset = 0;
                        for (int cy = 0; cy < 8; cy++)
                        {
                            int wcy = (wy * 8 + cy) * 2;
                            for (int cx = 0; cx < 8; cx++)
                            {
                                int wcx = (wx * 8 + cx) * 2;
                                Color color = w_color[nes_main.g_nes_ppu.g_chr_data[in_bank, wchr, woffset]];
                                woffset += 1;
                                for (int dy = 0; dy < 2; dy++)
                                {
                                    for (int dx = 0; dx < 2; dx++)
                                    {
                                        int pixelOffset =
                                            ((wcy + dy) * stride) + ((wcx + dx) << 2);
                                        *(pixels + pixelOffset) = color.B;
                                        *(pixels + pixelOffset + 1) = color.G;
                                        *(pixels + pixelOffset + 2) = color.R;
                                        *(pixels + pixelOffset + 3) = 255;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            in_bitmap.UnlockBits(bmpData);
        }
        public void picture_set(int in_bank)
        {
            pictureBox.BackColor = Color.Black;
            pictureBox.Image = g_picture_list[in_bank];
        }
        public void picture_view()
        {
            pictureBox_1_1.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[0]];
            pictureBox_1_2.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[1]];
            pictureBox_1_3.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[2]];
            pictureBox_1_4.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[3]];
            pictureBox_2_1.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[4]];
            pictureBox_2_2.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[5]];
            pictureBox_2_3.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[6]];
            pictureBox_2_4.Image = g_picture_list[nes_main.g_nes_mapper_control.g_chr_bank_map[7]];
        }

    }
}
