using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace NESTracer
{
    public partial class Form_PPU_Screen : Form
    {
        public int g_screen_xpos;
        public int g_screen_ypos;
        public Bitmap g_hexcode_bmp;
        Brush[] s_brush = {  Brushes.LightYellow,
                            Brushes.LightPink,
                            Brushes.LightGreen,
                            Brushes.LightBlue
        };
        public int g_layout_enable = -1;
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_PPU_Screen()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            pictureBox_Nametable12.Image = new Bitmap(256, 240);
            pictureBox_Nametable12.BackColor = Color.Black;
        }
        //----------------------------------------------------------------
        //initialize
        //----------------------------------------------------------------
        public void initialize()
        {
            pictureBox_Nametable1.Image = new Bitmap(384, 240);
            pictureBox_Nametable2.Image = new Bitmap(384, 240);

            g_hexcode_bmp = new Bitmap(12 * 256, 8 * 4);
            var graphics = Graphics.FromImage(g_hexcode_bmp);
            for (int wattr = 0; wattr < 4; wattr++)
            {
                graphics.FillRectangle(s_brush[wattr], 0, wattr * 8, 12 * 256, wattr * 8 + 8);
            }

            Font font = new Font("Fira Code", 6);
            SolidBrush brush = new SolidBrush(Color.Black);
            for (int wval = 0; wval < 256; wval++)
            {
                string wmoji = wval.ToString("X2");
                for (int wattr = 0; wattr < 4; wattr++)
                {
                    graphics.DrawString(wmoji, font, brush, new PointF(wval * 12, wattr * 8));
                }
            }
            font.Dispose();
            brush.Dispose();

            pictureBox_PalleteBG.Image = new Bitmap(100, 100);
            pictureBox_PalleteSP.Image = new Bitmap(100, 100);
        }
        //----------------------------------------------------------------
        //Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void Form_PPU_Memory_FormClosing(object sender, FormClosingEventArgs e)
        {
            nes_main.g_ppu_enable = false;
            nes_main.g_form_setting.update();
            nes_main.write_setting();
            e.Cancel = true;
        }

        private void Form_PPU_Memory_ResizeEnd(object sender, EventArgs e)
        {
            var currentPosition = this.Location;
            g_screen_xpos = currentPosition.X;
            g_screen_ypos = currentPosition.Y;
            nes_main.write_setting();
        }

        private void Form_PPU_Memory_Shown(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
        }
        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private void Form_PPU_Memory_Paint(object sender, PaintEventArgs e)
        {
            if (g_layout_enable == -1)
            {
                if (nes_main.g_nes_ppu.g_nametable_arrangement == 2)
                {
                    label_screen2.Text = "nametable 2(0x2800～0x2bff)";
                    label_screen2.Location = new Point(12, 269);
                    pictureBox_Nametable2.Location = new Point(9, 284);
                    pictureBox_Nametable12.Location = new Point(406, 24);
                    pictureBox_Nametable22.Location = new Point(406, 284);
                }
                else
                {
                    label_screen2.Text = "nametable 2(0x2400～0x27ff)";
                    label_screen2.Location = new Point(398, 9);
                    pictureBox_Nametable2.Location = new Point(398, 24);
                    pictureBox_Nametable12.Location = new Point(9, 284);
                    pictureBox_Nametable22.Location = new Point(398, 284);
                }
                g_layout_enable = nes_main.g_nes_ppu.g_nametable_arrangement;
            }
            pictureBox_Nametable1.Invalidate();
            pictureBox_Nametable2.Invalidate();
            pictureBox_PalleteBG.Invalidate();
            pictureBox_PalleteSP.Invalidate();
        }

        private void pictureBox_Nametable1_Paint(object sender, PaintEventArgs e)
        {
            nametable_paint(sender, 0, 0x2000);
        }

        private void pictureBox_Nametable2_Paint(object sender, PaintEventArgs e)
        {

            nametable_paint(sender, 1, 0x2400);
        }
        public void nametable_paint(object sender, int in_bank, int in_address)
        {
            Bitmap src_bitmap = g_hexcode_bmp;
            BitmapData src_bmpData = src_bitmap.LockBits(new Rectangle(0, 0, src_bitmap.Width, src_bitmap.Height),
                                                 ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            IntPtr src_ptr = src_bmpData.Scan0;
            int src_stride = src_bmpData.Stride;
            Bitmap dest_bitmap = (Bitmap)((PictureBox)sender).Image;
            BitmapData dest_bmpData = dest_bitmap.LockBits(new Rectangle(0, 0, dest_bitmap.Width, dest_bitmap.Height),
                                                 ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            IntPtr dest_ptr = dest_bmpData.Scan0;
            int dest_stride = dest_bmpData.Stride;

            int bytesPerPixel = 4;
            unsafe
            {
                for (int wy = 0; wy < 30; wy++)
                {
                    for (int wx = 0; wx < 32; wx++)
                    {
                        int wval = nes_main.g_nes_ppu.g_ram[in_address];
                        in_address += 1;
                        int wattr = nes_main.g_nes_ppu.g_attrtable[in_bank, wx, wy];
                        unsafe
                        {
                            byte* src_pixels = (byte*)src_ptr;
                            byte* dest_pixels = (byte*)dest_ptr;
                            for (int cy = 0; cy < 8; cy++)
                            {
                                for (int cx = 0; cx < 12; cx++)
                                {
                                    int src_pixelOffset = (wattr * 8 + cy) * src_stride + (wval * 12 + cx) * bytesPerPixel;
                                    int dest_pixelOffset = (wy * 8 + cy) * dest_stride + (wx * 12 + cx) * bytesPerPixel;
                                    *(dest_pixels + dest_pixelOffset) = *(src_pixels + src_pixelOffset);
                                    *(dest_pixels + dest_pixelOffset + 1) = *(src_pixels + src_pixelOffset + 1);
                                    *(dest_pixels + dest_pixelOffset + 2) = *(src_pixels + src_pixelOffset + 2);
                                    *(dest_pixels + dest_pixelOffset + 3) = *(src_pixels + src_pixelOffset + 3);
                                }
                            }
                        }
                    }
                }
            }
            src_bitmap.UnlockBits(src_bmpData);
            dest_bitmap.UnlockBits(dest_bmpData);
        }

        private void pictureBox_PalleteBG_Paint(object sender, PaintEventArgs e)
        {
            nes_setup_Pallete(e, 0x3f00);
        }

        private void pictureBox_PalleteSP_Paint(object sender, PaintEventArgs e)
        {
            nes_setup_Pallete(e, 0x3f10);
        }
        public void nes_setup_Pallete(PaintEventArgs e, int in_address)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Color wcolor = nes_main.g_nes_ppu.PALLET_BASE[nes_main.g_nes_ppu.g_ram[in_address + i * 4 + j]];
                    SolidBrush brush = new SolidBrush(wcolor);
                    e.Graphics.FillRectangle(brush, 25 * j, 25 * i, 25, 24);
                }
            }
        }
        public delegate void UpdatePictureBoxDelegate(PictureBox in_pic, Bitmap in_bitmap);
        private void UpdatePictureBox(PictureBox in_pic, Bitmap in_bitmap)
        {
            if (in_pic.InvokeRequired)
            {
                in_pic.Invoke(new UpdatePictureBoxDelegate(UpdatePictureBox), new object[] { in_bitmap.Clone() });
            }
            else
            {
                in_pic.Image = (Bitmap)in_bitmap.Clone();
            }
        }
        public void picture_update(Bitmap in_bitmap1, Bitmap in_bitmap2)
        {
            this.Invoke(new UpdatePictureBoxDelegate(this.UpdatePictureBox), new object[] { pictureBox_Nametable12, in_bitmap1 });
            this.Invoke(new UpdatePictureBoxDelegate(this.UpdatePictureBox), new object[] { pictureBox_Nametable22, in_bitmap2 });
        }
        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private readonly object g_bitmapLock = new object();
        public void picture_update()
        {
            lock (g_bitmapLock)
            {
                Bitmap g_work_bitmap = new Bitmap(256, 240);
                for (int i = 0; i < 2; i++)
                {
                    BitmapData game_bmpData = g_work_bitmap.LockBits(new Rectangle(0, 0, g_work_bitmap.Width, g_work_bitmap.Height),
                                                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    IntPtr dest_ptr = game_bmpData.Scan0;
                    int dest_stride = game_bmpData.Stride;
                    const int bytesPerPixel = 4;
                    unsafe
                    {
                        int w_base = 256 * 240 * i;
                        for (int wy = 0; wy < 240; wy++)
                        {
                            uint* pixel = (uint*)dest_ptr;
                            for (int wx = 0; wx < 256; wx++)
                            {
                                *pixel = nes_main.g_nes_ppu.g_game_screen_name[w_base];
                                pixel = (uint*)((IntPtr)pixel + bytesPerPixel);
                                w_base += 1;
                            }
                            dest_ptr += dest_stride;
                        }
                    }
                    g_work_bitmap.UnlockBits(game_bmpData);
                    try
                    {
                        if(i == 0)
                        {
                            this.Invoke((Action)(() =>
                            {
                                pictureBox_Nametable12.Image?.Dispose();
                                pictureBox_Nametable12.Image = new Bitmap(g_work_bitmap);
                                pictureBox_Nametable12.Width = 256;
                                pictureBox_Nametable12.Height = 240;
                            }));
                        }
                        else
                        {
                            this.Invoke((Action)(() =>
                            {
                                pictureBox_Nametable22.Image?.Dispose();
                                pictureBox_Nametable22.Image = new Bitmap(g_work_bitmap);
                                pictureBox_Nametable22.Width = 256;
                                pictureBox_Nametable22.Height = 240;
                            }));
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
