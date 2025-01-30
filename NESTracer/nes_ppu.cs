using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_ppu
    {
        public const int PPU_LINE_NUM = 262;
        public int g_scanline;
        public int g_ppu_reg_t;
        public int g_ppu_reg_v;
        public int g_ppu_reg_x;
        public int g_reg_reg_w;
        public bool g_sprite_zero_hit;
        //----------------------------------------------------------------
        public nes_ppu()
        {
            initialize();
        }
        public void run(int in_vline)
        {
            g_scanline = in_vline;
            if (g_scanline <= 239)
            {
                rendering_line(g_scanline);
                if (g_sprite_zero_hit == true)
                {
                    g_io_2002_6_SPRITE = true;
                }
                nes_main.g_nes_mapper_control.ppu_irq_line();
            }
            else
            if (g_scanline == 240)
            {
                rendering();
            }
            else
            if (g_scanline == 241)   
            {
                g_io_2002_7_VBLANK = true;
                if (g_io_2000_7_VBLANK == true)
                {
                    nes_main.g_nes_6502.interrupt_NMI = true;
                }
            }
            else
            if (g_scanline == 261)   
            {
                g_io_2002_7_VBLANK = false;
                g_io_2002_6_SPRITE = false;
                g_io_2002_5_SPOVER = false;
                g_sprite_zero_hit = false;
            }
        }
        public void run2()
        {
        	            if (g_scanline <= 239)
            {
            if ((g_io_2001_3_BGSHOW == true) || (g_io_2001_4_SPSHOW == true))
            {
                wrapping_around_y();
                g_ppu_reg_v = (g_ppu_reg_v & 0xfbe0)
                            | (g_ppu_reg_t & 0x041f);
            }
        }
            if (g_scanline == 261)
            {
                if ((g_io_2001_3_BGSHOW == true) || (g_io_2001_4_SPSHOW == true))
                {
                    g_ppu_reg_v = g_ppu_reg_t;
                }
            }
        }
        public void wrapping_around_x()
        {
            if ((g_ppu_reg_v & 0x001f) == 0x001f)
            {
                g_ppu_reg_v &= ~0x001f;
                g_ppu_reg_v ^= 0x0400;
            }
            else
            {
                g_ppu_reg_v += 0x0001;
            }
        }
        public void wrapping_around_y()
        {
            if ((g_ppu_reg_v & 0x7000) != 0x7000)
            {
                g_ppu_reg_v += 0x1000;
            }
            else
            {
                int w_y = g_ppu_reg_v & 0x03e0;
                if (w_y == 0x03a0)
                {
                    g_ppu_reg_v &= 0x0c1f;
                    g_ppu_reg_v ^= 0x0800;
                }
                else
                if (w_y == 0x03e0)
                {
                    g_ppu_reg_v &= 0x0c1f;
                }
                else
                {
                    g_ppu_reg_v &= 0x0fff;
                    g_ppu_reg_v += 0x0020;
                }
            }
        }
    }
}
