using NAudio.Gui;
using System.Xml.Linq;

namespace NESTracer
{
    internal partial class nes_ppu
    {
        public uint[] g_game_screen;
        public uint[] g_game_screen_name;
        private uint[] g_game_cmap;
        private uint[] g_game_cmap_name1;
        private uint[] g_game_cmap_name2;
        bool g_color_high_mode;
        bool g_color_high_b;
        bool g_color_high_g;
        bool g_color_high_r;
        public uint[] g_color;

        public bool g_renderer_action = false;
        public ManualResetEvent g_waitHandle;
        //----------------------------------------------------------------
        public void rendering_line(int in_line)
        {
            int w_bgmem = g_io_2000_4_BGMEM == false ? 0 : 1;
            //BG
            if (g_io_2001_3_BGSHOW == true)
            {
                int w_view_x = (((g_ppu_reg_v & 0x001f) << 3) | g_ppu_reg_x);
                int w_view_y = (((g_ppu_reg_v & 0x03e0) >> 2) | ((g_ppu_reg_v & 0x7000) >> 12));
                int w_base_x = ((g_ppu_reg_v & 0x0400) >> 10);
                int w_base_y = ((g_ppu_reg_v & 0x0800) >> 11);
                int w_name = (w_base_y << 1) | w_base_x;
                int w_view_dx = 8;
                int w_attr = 0;
                int w_offset = 0;
                int w_view_cy = w_view_y >> 3;
                int wx_top = 0;
                int w_base = ((g_io_2000_4_BGMEM == false) ? 0x0000 : 0x1000) + (w_view_y & 0x07); 
                int w_chr = 0;

                if (g_io_2001_1_BGleftmost == false)
                {
                    w_view_x += 8;
                    wx_top = 8;
                }
                for (int wx = wx_top; wx < 256; wx++)
                {
                    if (w_view_dx == 8)
                    {
                        if (256 <= w_view_x)
                        {
                            w_name ^= 1;
                            w_view_x -= 256;
                        }
                        int w_view_cx = w_view_x >> 3;
                        w_view_dx = w_view_x & 7;
                        int w_name_addr = get_nametable_address(0x2000 + (0x400 * w_name) + (w_view_cy << 5) + w_view_cx);
                        int w_attr_addr = get_nametable_address(0x23c0 + (0x400 * w_name) + ((w_view_cy << 1) & 0xf8) + (w_view_cx >> 2));
                        w_chr = g_ram[w_name_addr];
                        w_attr = g_ram[w_attr_addr];
                        if((w_view_cy & 0x03) < 2)
                        {
                            if((w_view_cx & 0x03) < 2)
                            {
                                w_attr = (w_attr & 0x03);
                            }
                            else
                            {
                                w_attr = (w_attr >> 2) & 0x03;
                            }
                        }
                        else
                        {
                            if ((w_view_cx & 0x03) < 2)
                            {
                                w_attr = (w_attr >> 4) & 0x03;
                            }
                            else
                            {
                                w_attr = (w_attr >> 6) & 0x03;
                            }
                        }
                        w_attr <<= 2;



                        w_offset = w_base + (w_chr * 16);
                    }
                    int w_shift = 7 - ((w_view_dx & 0x07));
                    int w_bit1 = (int)((g_ram[w_offset] >> w_shift) & 0x01);
                    int w_bit2 = (int)((g_ram[w_offset + 8] >> w_shift) & 0x01);
                    g_game_cmap[wx] = (uint)(((w_bit2<<1)+w_bit1) + w_attr);
                    w_view_x += 1;
                    w_view_dx += 1;
                }
            }
            //SP
            if (g_io_2001_4_SPSHOW == true)
            {
                int w_spmem = w_spmem = (g_io_2000_3_SPMEM == false) ? 0 : 1;
                for (int w_spnum = 63; w_spnum >= 0; w_spnum--)
                {
                    int w_y = g_memory_oam[w_spnum * 4] + 1;
                    int w_chr = g_memory_oam[w_spnum * 4 + 1];
                    int w_attr = g_memory_oam[w_spnum * 4 + 2];
                    int w_x = g_memory_oam[w_spnum * 4 + 3];
                    int w_attr_vertical = (w_attr & 0x80) != 0 ? 1 : 0;
                    int w_attr_horizon = (w_attr & 0x40) != 0 ? 1 : 0;
                    int w_attr_depth = (w_attr & 0x20) >> 5;
                    int w_attr_pallet = (w_attr & 0x03) << 2;
                    int cy = in_line - w_y;
                    if (g_io_2000_5_SPSIZE == false)
                    {
                        if ((cy < 0) || (7 < cy))
                        {
                            continue;
                        }
                        if (w_attr_vertical == 1) cy = 7 - cy;
                    }
                    else
                    {
                        if ((cy < 0) || (15 < cy))
                        {
                            continue;
                        }
                        w_spmem = (w_chr & 1);
                        w_chr &= 0xfe;
                        if (cy <= 7)
                        {
                            if (w_attr_vertical == 1)
                            {
                                w_chr += 1;
                                cy = 7 - cy;
                            }
                        }
                        else
                        {
                            cy -= 8;
                            if (w_attr_vertical == 0)
                            {
                                w_chr += 1;
                            }else
                            {
                                cy = 7 - cy;
                            }
                        }
                    }
                    int w_offset = cy << 3;
                    int w_base = (w_spmem * 0x1000) + cy + (w_chr * 16);
                    for (int j = 0; j <= 7; j++)
                    {
                        int cx = j;
                        if (w_attr_horizon == 1) cx = 7 - j;
                        int kx = w_x + j;
                        if (255 < kx) break;
                        if ((g_io_2001_2_SPleftmost == false) && (kx < 8)) continue;
                        int w_shift = 7 - cx;
                        int w_bit1 = (int)((g_ram[w_base] >> w_shift) & 0x01);
                        int w_bit2 = (int)((g_ram[w_base + 8] >> w_shift) & 0x01);
                        byte w_cor = (byte)(((w_bit2 << 1) + w_bit1));
                        if (w_cor != 0)
                        {
                            bool w_bg_opaque = (g_game_cmap[kx] & 3) != 0;
                            if (w_spnum == 0 && w_bg_opaque == true && kx != 255)
                            {
                                g_sprite_zero_hit = true;
                                g_sprite_zero_hit_cnt = kx;
                            }

                            if (w_attr_depth == 1)
                            {
                                if ((g_game_cmap[kx] & 3) == 0)
                                {
                                    g_game_cmap[kx] = (uint)(w_cor + w_attr_pallet + 0x10);
                                }
                            }
                            else
                            {
                                g_game_cmap[kx] = (uint)(w_cor + w_attr_pallet + 0x10);
                            }
                        }
                    }
                }
            }
            //name table
            {
                for (int i = 0; i < 2; i++)
                {
                    int w_name = i;
                    int w_view_dx = 8;
                    int w_attr = 0;
                    int w_bank = 0;
                    int w_bank_chr = 0;
                    int w_offset = (in_line & 0x07) << 3;
                    int w_view_cy = in_line >> 3;
                    int w_addr = (256 * 240 * i) + (in_line * 256);
                    for (int wx = 0; wx < 256; wx++)
                    {
                        if (w_view_dx == 8)
                        {
                            int w_view_cx = wx >> 3;
                            w_view_dx = 0;
                            int w_chr = g_ram[0x2000 + (0x400 * w_name) + (w_view_cy << 5) + w_view_cx];
                            w_attr = g_attrtable[w_name, w_view_cx, w_view_cy] << 2;
                            w_bank = get_bank(w_bgmem, w_chr);
                            w_bank_chr = get_bank_chr(w_bgmem, w_chr);
                        }
                        uint w_col = (uint)(g_chr_data[w_bank, w_bank_chr, w_offset + w_view_dx] + w_attr);
                        g_game_screen_name[w_addr] = g_color[g_ram[0x3f00 + w_col]];
                        w_view_dx += 1;
                        w_addr += 1;
                    }
                }
            }
            //mix
            {
                int w_addr = in_line * 256;
                uint w_bkcolor = g_color[g_ram[0x3f00]];
                for (int wx = 0; wx < 256; wx++)
                {
                    uint w_col = g_game_cmap[wx];
                    if ((w_col & 0x03) == 0)
                    {
                        g_game_screen[w_addr] = w_bkcolor;
                    }
                    else
                    {
                        uint wcot = g_color[g_ram[0x3f00 + w_col]];
                        g_game_screen[w_addr] = wcot;
                    }
                    g_game_cmap[wx] = 0;
                    w_addr += 1;
                }
            }
        }
        //----------------------------------------------------------------
        public void rendering()
        {
            if(g_renderer_action == false)
            {
                g_waitHandle.Set();
            }
        }
        public void run()
        {
            while (true)
            {
                g_renderer_action = false;
                g_waitHandle.WaitOne(Timeout.Infinite);
                g_waitHandle.Reset();
                nes_main.Nes_Screen_Update();
                g_renderer_action = true;
            }
        }
    }
}
