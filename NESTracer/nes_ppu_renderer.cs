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
            int w_spmem = g_io_2000_3_SPMEM == false ? 0 : 1;

            //BG
            if (g_io_2001_3_BGSHOW == true)
            {
                int w_view_x = (((g_ppu_reg_v & 0x001f) << 3) | g_ppu_reg_x);
                int w_view_y = (((g_ppu_reg_v & 0x03e0) >> 2) | ((g_ppu_reg_v & 0x7000) >> 12));
                int w_base_x = ((g_ppu_reg_v & 0x0400) >> 10);
                int w_base_y = ((g_ppu_reg_v & 0x0800) >> 11);
                int w_name = 0;
                switch (g_nametable_arrangement)
                {
                    case 0: w_name = 0; break;
                    case 1: w_name = 1; break;
                    case 2: w_name = w_base_y; break;
                    case 3: w_name = w_base_x; break;
                }
                int w_view_dx = 8;
                int w_attr = 0;
                int w_bank = 0;
                int w_bank_chr = 0;
                int w_offset = (w_view_y & 0x07) << 3;
                int w_view_cy = w_view_y >> 3;
                int wx_top = 0;
                if (g_io_2001_1_BGleftmost == true)
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
                            if(g_nametable_arrangement == 3) w_name ^= 1;
                            w_view_x -= 256;
                        }
                        int w_view_cx = w_view_x >> 3;
                        w_view_dx = w_view_x & 7;
                        int w_chr = g_ram[0x2000 + (0x400 * w_name) + (w_view_cy << 5) + w_view_cx];
                        w_attr = g_attrtable[w_name, w_view_cx, w_view_cy] << 2;
                        w_bank = get_bank(w_bgmem, w_chr);
                        w_bank_chr = get_bank_chr(w_bgmem, w_chr);
                    }
                    g_game_cmap[wx] = (uint)(g_chr_data[w_bank, w_bank_chr, w_offset + w_view_dx] + w_attr);
                    w_view_x += 1;
                    w_view_dx += 1;
                }
            }
            //SP
            if (g_io_2001_4_SPSHOW == true)
            {
                if (g_io_2000_5_SPSIZE == false)
                {
                    //sprite size 8 * 8
                    for (int w_spnum = 63; w_spnum >= 0; w_spnum--)
                    {
                        int wy = g_memory_oam[w_spnum * 4] + 1;
                        if ((wy <= in_line) && (in_line <= wy + 7))
                        {
                            int wchr = g_memory_oam[w_spnum * 4 + 1];
                            int wattr = g_memory_oam[w_spnum * 4 + 2];
                            int wx = g_memory_oam[w_spnum * 4 + 3];
                            int wattr_vertical = (wattr & 0x80) != 0 ? 1 : 0;
                            int wattr_horizon = (wattr & 0x40) != 0 ? 1 : 0;
                            int wattr_depth = (wattr & 0x20) >> 5;
                            int wattr_pallet = wattr & 0x03;
                            int w_bank = get_bank(w_spmem, wchr);
                            int w_bank_chr = get_bank_chr(w_spmem, wchr);
                            int wpallete_base = wattr_pallet * 4;
                            int cy = in_line - wy;
                            if (wattr_vertical == 1) cy = 7 - cy;
                            int w_offset = cy << 3;
                            for (int j = 0; j <= 7; j++)
                            {
                                int cx = j;
                                if (wattr_horizon == 1) cx = 7 - j;
                                int kx = wx + j;
                                if (255 < kx) break;
                                if((g_io_2001_2_SPleftmost == true)&& (kx < 8)) continue;
                                byte w_cor = g_chr_data[w_bank, w_bank_chr, w_offset + cx];
                                sp_sub(w_spnum, w_cor, kx, wattr_depth, wpallete_base);
                            }
                        }
                    }
                }
                else
                {
                    //sprite size 8 * 16
                    for (int w_depth = 1; w_depth >=0; w_depth--)
                    {
                        for (int w_spnum = 63; w_spnum >= 0; w_spnum--)
                        {
                            int wy = g_memory_oam[w_spnum * 4];
                            if ((wy <= in_line) && (in_line <= wy + 15))
                            {
                                int wchr = g_memory_oam[w_spnum * 4 + 1];
                                int wattr = g_memory_oam[w_spnum * 4 + 2];
                                int wx = g_memory_oam[w_spnum * 4 + 3];
                                int wattr_vertical = (wattr & 0x80) != 0 ? 1 : 0;
                                int wattr_horizon = (wattr & 0x40) != 0 ? 1 : 0;
                                int wattr_depth = (wattr & 0x20) >> 5;
                                int wattr_pallet = wattr & 0x03;
                                int w_spmem2 = wchr & 1;
                                wchr &= 0xfe;
                                int w_bank = get_bank(w_spmem2, wchr);
                                int w_bank_chr = get_bank_chr(w_spmem2, wchr);
                                int w_bank2 = get_bank(w_spmem2, wchr + 1);
                                int w_bank_chr2 = get_bank_chr(w_spmem2, wchr + 1);
                                int wpallete_base = wattr_pallet * 4;
                                if (wattr_depth != w_depth) continue;
                                for (int cy = 0; cy < 8; cy++)
                                {
                                    int ky = 0;
                                    if (wattr_vertical == 0)
                                    {
                                        ky = wy + cy;
                                    }
                                    else
                                    {
                                        ky = wy + (15 - cy);
                                    }
                                    if (in_line == ky)
                                    {
                                        int w_offset = cy << 3;
                                        for (int cx = 0; cx < 8; cx++)
                                        {
                                            int kx = 0;
                                            if (wattr_horizon == 0)
                                            {
                                                kx = wx + cx;
                                            }
                                            else
                                            {
                                                kx = wx + (7 - cx);
                                            }
                                            if (255 < kx) break;
                                            if ((g_io_2001_2_SPleftmost == true) && (kx < 8)) continue;
                                            byte w_cor = g_chr_data[w_bank, w_bank_chr, w_offset];
                                            sp_sub(w_spnum, w_cor, kx, wattr_depth, wpallete_base);
                                            w_offset += 1;
                                        }
                                    }
                                }
                                for (int cy = 0; cy < 8; cy++)
                                {
                                    int ky = 0;
                                    if (wattr_vertical == 0)
                                    {
                                        ky = wy + cy + 8;
                                    }
                                    else
                                    {
                                        ky = wy + (7 - cy);
                                    }
                                    if (in_line == ky)
                                    {
                                        int w_offset = cy << 3;
                                        for (int cx = 0; cx < 8; cx++)
                                        {
                                            int kx = 0;
                                            if (wattr_horizon == 0)
                                            {
                                                kx = wx + cx;
                                            }
                                            else
                                            {
                                                kx = wx + (7 - cx);
                                            }
                                            if (255 < kx) break;
                                            if ((g_io_2001_2_SPleftmost == true) && (kx < 8)) continue;
                                            byte w_cor = g_chr_data[w_bank2, w_bank_chr2, w_offset];
                                            sp_sub(w_spnum, w_cor, kx, wattr_depth, wpallete_base);
                                            w_offset += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //name table
            {
                for(int i = 0; i < 2; i++)
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
        public void sp_sub(int in_spnum, uint in_cor, int in_kx, int in_wattr_depth, int in_wpallete_base)
        {
            if (in_cor != 0)
            {
                if (in_wattr_depth == 1)
                {
                    if ((g_game_cmap[in_kx] & 3) == 0)
                    {
                        g_game_cmap[in_kx] = (uint)(in_cor + in_wpallete_base + 0x10);
                    }
                    if (in_spnum == 0)
                    {
                        g_sprite_zero_hit = true;
                    }
                }
                else
                {
                    g_game_cmap[in_kx] = (uint)(in_cor + in_wpallete_base + 0x10);
                    if (in_spnum == 0)
                    {
                        g_sprite_zero_hit = true;
                    }
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
                g_renderer_action = true;
                nes_main.Nes_Screen_Update();
            }
        }
    }
}
