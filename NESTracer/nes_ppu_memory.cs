using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_ppu
    {
        public bool g_io_2000_7_VBLANK;
        public bool g_io_2000_6_PPUMODE;
        public bool g_io_2000_5_SPSIZE;
        public bool g_io_2000_4_BGMEM;
        public bool g_io_2000_3_SPMEM;
        public bool g_io_2000_2_OFFSET;
        public int g_io_2000_1_SCREEN;

        public bool g_io_2001_7_BEmpasize;
        public bool g_io_2001_6_GEmpasize;
        public bool g_io_2001_5_REmpasize;
        public bool g_io_2001_4_SPSHOW;
        public bool g_io_2001_3_BGSHOW;
        public bool g_io_2001_2_SPleftmost;
        public bool g_io_2001_1_BGleftmost;
        public bool g_io_2001_0_COLORMODE;

        public bool g_io_2002_7_VBLANK;
        public bool g_io_2002_6_SPRITE;
        public bool g_io_2002_5_SPOVER;
        public bool g_io_2002_4_ACTIVE;

        public byte g_io_2003_oam_offset;

        public byte[] g_ram;                 
        public byte[,] g_rom;
        public int g_nametable_arrangement;   
        public int[] g_nametable_bank;
        public byte[] g_memory_oam;
        public byte[,,] g_chr_data;
        public int[,,] g_attrtable;

        public bool g_bk_BEmpasize;
        public bool g_bk_GEmpasize;
        public bool g_bk_REmpasize;
        public int g_event_wbuf_write;
        public int g_event_wbuf_renderer;

        public byte g_openbus;

        //----------------------------------------------------------------
        //read
        //----------------------------------------------------------------
        public byte read1(int in_address)
        {
            byte w_out = 0;
            switch (in_address)
            {
                case 0x2002:
                    if (g_io_2002_7_VBLANK == true) w_out += 0x80;
                    if (g_io_2002_6_SPRITE == true) w_out += 0x40;
                    if (g_io_2002_5_SPOVER == true) w_out += 0x20;
                    g_io_2002_7_VBLANK = false;
                    g_reg_reg_w = 0;
                    break;
                case 0x2004:
                    w_out = g_memory_oam[g_io_2003_oam_offset];
                    g_io_2003_oam_offset += 1;
                    break;
                case 0x2007:
                    w_out = g_openbus;
                    int w_addr = g_ppu_reg_v & 0x3FFF;
                    if ((w_addr <= 0x1fff)||((0x3000 <= w_addr) && (w_addr <= 0x3eff)))
                    {
                        w_addr &= 0x1fff;
                        int w_bank = nes_main.g_nes_mapper_control.g_chr_bank_map[w_addr / nes_mapper_control.CHR_ROM_BANK_SIZE];
                        int w_offset = w_addr % nes_mapper_control.CHR_ROM_BANK_SIZE;
                        g_openbus = g_rom[w_bank, w_offset];
                    }
                    else
                    if (w_addr <= 0x2fff)
                    {
                        g_openbus = g_ram[get_nametable_address(w_addr)];
                    }
                    else
                    {
                        w_addr = 0x3f00 + (w_addr & 0x001f);
                        g_openbus = g_ram[w_addr];
                    }

                    if (((g_scanline < 240) || (g_scanline == 261)) && ((g_io_2001_3_BGSHOW == true) || (g_io_2001_4_SPSHOW == true)))
                    {
                        if (g_io_2000_2_OFFSET == false)
                        {
                            wrapping_around_x();
                        }
                        else
                        {
                            wrapping_around_y();
                        }
                    }
                    else
                    {
                        if (g_io_2000_2_OFFSET == false)
                        {
                            g_ppu_reg_v += 0x0001;
                        }
                        else
                        {
                            g_ppu_reg_v += 0x0020;
                        }
                    }
                    break;

                case 0x2000:
                case 0x2001:
                case 0x2003:
                case 0x2005:
                case 0x2006:
                    w_out = g_openbus;
                    break;
                default:
                    Console.WriteLine("Unsupported Mapper");
                    break;
            }
            return w_out;
        }
        //----------------------------------------------------------------
        //write
        //----------------------------------------------------------------
        public void write1(int in_address, byte in_val)
        {
            g_openbus = in_val;
            switch (in_address)
            {
                case 0x2000:
                    if (((in_val & 0x80) == 0x80) && (g_io_2000_7_VBLANK == false) && (g_io_2002_7_VBLANK == true))
                    {
                        nes_main.g_nes_6502.interrupt_NMI = true;
                    }
                    if ((in_val & 0x80) == 0x80) g_io_2000_7_VBLANK = true; else g_io_2000_7_VBLANK = false;
                    if ((in_val & 0x40) == 0x40) g_io_2000_6_PPUMODE = true; else g_io_2000_6_PPUMODE = false;
                    if ((in_val & 0x20) == 0x20) g_io_2000_5_SPSIZE = true; else g_io_2000_5_SPSIZE = false;
                    if ((in_val & 0x10) == 0x10) g_io_2000_4_BGMEM = true; else g_io_2000_4_BGMEM = false;
                    if ((in_val & 0x08) == 0x08) g_io_2000_3_SPMEM = true; else g_io_2000_3_SPMEM = false;
                    if ((in_val & 0x04) == 0x04) g_io_2000_2_OFFSET = true; else g_io_2000_2_OFFSET = false;
                    g_io_2000_1_SCREEN = in_val & 0x03;
                    g_ppu_reg_t = (g_ppu_reg_t & 0xf3ff) | ((in_val & 0x03) << 10);
                    break;
                case 0x2001:
                    if ((in_val & 0x80) == 0x80) g_io_2001_7_BEmpasize = true; else g_io_2001_7_BEmpasize = false;
                    if ((in_val & 0x40) == 0x40) g_io_2001_6_GEmpasize = true; else g_io_2001_6_GEmpasize = false;
                    if ((in_val & 0x20) == 0x20) g_io_2001_5_REmpasize = true; else g_io_2001_5_REmpasize = false;
                    if ((in_val & 0x10) == 0x10) g_io_2001_4_SPSHOW = true; else g_io_2001_4_SPSHOW = false;
                    if ((in_val & 0x08) == 0x08) g_io_2001_3_BGSHOW = true; else g_io_2001_3_BGSHOW = false;
                    if ((in_val & 0x04) == 0x04) g_io_2001_2_SPleftmost = true; else g_io_2001_2_SPleftmost = false;
                    if ((in_val & 0x02) == 0x02) g_io_2001_1_BGleftmost = true; else g_io_2001_1_BGleftmost = false;
                    if ((in_val & 0x01) == 0x01) g_io_2001_0_COLORMODE = true; else g_io_2001_0_COLORMODE = false;

                    if((g_io_2001_7_BEmpasize != g_bk_BEmpasize)
                        || (g_io_2001_6_GEmpasize != g_bk_GEmpasize)
                        || (g_io_2001_5_REmpasize != g_bk_REmpasize))
                    {
                        make_palette();
                    }
                    g_bk_BEmpasize = g_io_2001_7_BEmpasize;
                    g_bk_GEmpasize = g_io_2001_6_GEmpasize;
                    g_bk_REmpasize = g_io_2001_5_REmpasize;
                    break;
                case 0x2003:
                    g_io_2003_oam_offset = in_val;
                    break;
                case 0x2004:
                    g_memory_oam[g_io_2003_oam_offset] = in_val;
                    g_io_2003_oam_offset += 1;
                    break;
                case 0x2005:
                    if (g_reg_reg_w == 0)
                    {
                        g_ppu_reg_t = (g_ppu_reg_t & 0xffe0) | (in_val >> 3);
                        g_ppu_reg_x = in_val & 0x07;
                        g_reg_reg_w = 1;
                    }
                    else
                    {
                        g_ppu_reg_t = (g_ppu_reg_t & 0x8c1f) | ((in_val & 0xf8) << 2)
                                                                             | ((in_val & 0x07) << 12);
                        g_reg_reg_w = 0;
                    }
                    break;
                case 0x2006:
                    if (g_reg_reg_w == 0)
                    {
                        g_ppu_reg_t = (g_ppu_reg_t & 0x00ff) | ((in_val & 0x3f) << 8);
                        g_reg_reg_w = 1;
                    }
                    else
                    {
                        g_ppu_reg_t = (g_ppu_reg_t & 0xff00) | in_val;
                        g_ppu_reg_v = g_ppu_reg_t;
                        g_reg_reg_w = 0;
                    }
                    break;
                case 0x2007:
                    int w_addr = g_ppu_reg_v & 0x3FFF;
                    if ((w_addr <= 0x1fff) || ((0x3000 <= w_addr) && (w_addr <= 0x3eff)))
                    {
                        w_addr &= 0x1fff;
                        g_rom[(w_addr / nes_mapper_control.CHR_ROM_BANK_SIZE), (w_addr % nes_mapper_control.CHR_ROM_BANK_SIZE)] = in_val;
                        g_ram[w_addr] = in_val;
                        int w_bank = w_addr >> 10;
                        int w_chr = (w_addr & 0x03f0) >> 4;
                        int w_dy = (w_addr & 0x0007);

                        int w_read_addr = (w_addr & 0x1ff0) + w_dy;

                        int wchr1 = g_ram[w_read_addr];
                        int wchr2 = g_ram[w_read_addr + 8];

                        int wcheck_bit = 0x80;
                        for (int dx = 0; dx < 8; dx++)
                        {
                            int wd1 = (wchr1 & wcheck_bit) != 0 ? 1 : 0;
                            int wd2 = (wchr2 & wcheck_bit) != 0 ? 1 : 0;
                            g_chr_data[w_bank, w_chr, dx + w_dy * 8] = (byte)(wd2 * 2 + wd1);
                            wcheck_bit = wcheck_bit >> 1;
                        }
                    }
                    else
                    if (w_addr <= 0x2fff)
                    {
                        w_addr = get_nametable_address(w_addr);
                        g_ram[w_addr] = in_val;
                        if(((0x23c0 <= w_addr)&&(w_addr <= 0x23ff))||
                            ((0x27c0 <= w_addr) && (w_addr <= 0x27ff)))
                        {
                            int w_name = 0;
                            int w_x = 0;
                            int w_y = 0;
                            if (w_addr <= 0x23ff)
                            {
                                w_x = (w_addr - 0x23c0) & 0x07;
                                w_y = ((w_addr - 0x23c0) >> 3) & 0x07;
                            }
                            else
                            {
                                w_name = 1;
                                w_x = (w_addr - 0x27c0) & 0x07;
                                w_y = ((w_addr - 0x27c0) >> 3) & 0x07;
                            }
                            set_attrtable(w_name, w_x, w_y, in_val);
                        }
                    }
                    else
                    {
                        if((w_addr & 0x03) == 0)
                        {
                            g_ram[w_addr & 0xff0f] = (byte)(in_val & 0x3f);
                        }
                        else
                        {
                            g_ram[w_addr] = (byte)(in_val & 0x3f);
                        }
                    }

                    if (((g_scanline < 240) || (g_scanline == 261)) && ((g_io_2001_3_BGSHOW == true) || (g_io_2001_4_SPSHOW == true)))
                    {
                        if (g_io_2000_2_OFFSET == false)
                        {
                            wrapping_around_x();
                        }
                        else
                        {
                            wrapping_around_y();
                        }
                    }
                    else
                    {
                        if (g_io_2000_2_OFFSET == false)
                        {
                            g_ppu_reg_v += 0x0001;
                        }
                        else
                        {
                            g_ppu_reg_v += 0x0020;
                        }
                    }
                    break;
                case 0x4014:
                    for (int i = 0; i < 256; i++)
                    {
                        g_memory_oam[i] = nes_main.g_nes_6502.g_ram[in_val * 256 + i];
                    }
                    nes_main.g_nes_6502.g_clock_opt += 513;
                    break;
                default:
                    Console.WriteLine("Unsupported Mapper");
                    break;
            }
        }
        //----------------------------------------------------------------
        //sub
        //----------------------------------------------------------------
        public int get_bank(int in_pattern, int in_chr_num)
        {
            return nes_main.g_nes_bus.ppu_get_bank(in_pattern, in_chr_num);
        }
        public int get_bank_chr(int in_pattern, int in_chr_num)
        {
            return nes_main.g_nes_bus.ppu_get_bank_chr(in_pattern, in_chr_num);
        }
        public int get_nametable_address(int in_address)
        {
            int w_offset = in_address & 0x03ff;
            int w_num = (in_address & 0x0c00) >> 10;
            return g_nametable_bank[w_num] + w_offset;
        }
        public void set_attrtable(int in_name, int in_x, int in_y, byte in_val)
        {
            int wpal0 = in_val & 0x03;
            int wpal1 = (in_val >> 2) & 0x03;
            int wpal2 = (in_val >> 4) & 0x03;
            int wpal3 = (in_val >> 6) & 0x03;
            g_attrtable[in_name, in_x * 4 + 0, in_y * 4 + 0] = wpal0;
            g_attrtable[in_name, in_x * 4 + 1, in_y * 4 + 0] = wpal0;
            g_attrtable[in_name, in_x * 4 + 0, in_y * 4 + 1] = wpal0;
            g_attrtable[in_name, in_x * 4 + 1, in_y * 4 + 1] = wpal0;

            g_attrtable[in_name, in_x * 4 + 2, in_y * 4 + 0] = wpal1;
            g_attrtable[in_name, in_x * 4 + 3, in_y * 4 + 0] = wpal1;
            g_attrtable[in_name, in_x * 4 + 2, in_y * 4 + 1] = wpal1;
            g_attrtable[in_name, in_x * 4 + 3, in_y * 4 + 1] = wpal1;

            g_attrtable[in_name, in_x * 4 + 0, in_y * 4 + 2] = wpal2;
            g_attrtable[in_name, in_x * 4 + 1, in_y * 4 + 2] = wpal2;
            g_attrtable[in_name, in_x * 4 + 0, in_y * 4 + 3] = wpal2;
            g_attrtable[in_name, in_x * 4 + 1, in_y * 4 + 3] = wpal2;

            g_attrtable[in_name, in_x * 4 + 2, in_y * 4 + 2] = wpal3;
            g_attrtable[in_name, in_x * 4 + 3, in_y * 4 + 2] = wpal3;
            g_attrtable[in_name, in_x * 4 + 2, in_y * 4 + 3] = wpal3;
            g_attrtable[in_name, in_x * 4 + 3, in_y * 4 + 3] = wpal3;
        }
        public void make_palette()
        {
            bool w_high_mode = false;
            bool w_high_b = g_io_2001_7_BEmpasize;
            bool w_high_g = g_io_2001_6_GEmpasize;
            bool w_high_r = g_io_2001_5_REmpasize;
            if ((w_high_b == true) ||
                (w_high_g == true) ||
                (w_high_r == true))
            {
                w_high_mode = true;
                if ((w_high_b == true) &&
                    (w_high_g == true) &&
                    (w_high_r == true))
                {
                    w_high_b = false;
                    w_high_g = false;
                    w_high_r = false;
                }
            }
            if (g_io_2001_0_COLORMODE == false)
            {
                for (int i = 0; i < 64; i++)
                {
                    int w_b = PALLET_BASE[i].B;
                    int w_g = PALLET_BASE[i].G;
                    int w_r = PALLET_BASE[i].R;
                    if (w_high_mode == true)
                    {
                        if (w_high_b == false) w_b = (int)(w_b * 0.7);
                        if (w_high_g == false) w_g = (int)(w_g * 0.7);
                        if (w_high_r == false) w_r = (int)(w_r * 0.7);
                    }
                    g_color[i] = (uint)(0xff000000
                            | (uint)(w_r << 16)
                            | (uint)(w_g << 8)
                            | (uint)w_b);
                }
            }
            else
            {
                for (int i = 0; i < 64; i++)
                {
                    int w_b = PALLET_MONO[i].B;
                    int w_g = PALLET_MONO[i].G;
                    int w_r = PALLET_MONO[i].R;
                    if (w_high_mode == true)
                    {
                        if (g_io_2001_7_BEmpasize == false) w_b = (int)(w_b * 0.9);
                        if (g_io_2001_6_GEmpasize == false) w_g = (int)(w_g * 0.9);
                        if (g_io_2001_5_REmpasize == false) w_r = (int)(w_r * 0.9);
                    }
                    g_color[i] = (uint)(0xff000000
                            | (uint)(w_r << 16)
                            | (uint)(w_g << 8)
                            | (uint)w_b);
                }
            }
        }
    }
}

