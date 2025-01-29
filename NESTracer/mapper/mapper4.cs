namespace NESTracer
{
    internal class mapper4
    {
        public const int BANKg_reg_NUM = 8;                   

        public bool g_prg_mode;                             
        public bool g_chr_mode;                             
        public int g_select_bank;                           
        public static int[] g_bankreg;                      

        public static int g_irq_latch;
        public static int g_irq_counter;
        public static bool g_irq_enable;
        public static bool g_irq_reload;

        public void initialize()
        {
            g_bankreg = new int[BANKg_reg_NUM];
        }
        public void prg_rom_setting()
        {
            nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(0, 0);
            nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(1, 1);
            nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(2, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
            nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(3, nes_main.g_nes_mapper_control.g_prg_bank_num - 1);
        }
        public void chr_rom_setting()
        {
        }
        public void cpu_write1(int in_address, byte in_val)
        {
            if (in_address >= 0xe000)
            {
                if ((in_address & 0x01) == 0)
                {
                    g_irq_enable = false;
                }
                else
                {
                    g_irq_enable = true;
                }
            }
            else
            if (in_address >= 0xc000)
            {
                if ((in_address & 0x01) == 0)
                {
                    g_irq_latch = in_val;
                }
                else
                {
                    g_irq_counter = 0;
                    g_irq_reload = true;
                }
            }
            else
            if (in_address >= 0xa000)
            {
                if ((in_address & 0x01) == 0)
                {
                    if (in_val == 1)
                    {
                        nes_main.g_nes_ppu.g_nametable_arrangement = 2;
                        nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                    }
                    else
                    {
                        nes_main.g_nes_ppu.g_nametable_arrangement = 3;
                        nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                    }
                }
                else
                {
                }
            }
            else
            if (in_address >= 0x8000)
            {
                if((in_address & 0x0001) == 0)
                {
                    if ((in_val & 0x80) == 0) g_chr_mode = false; else g_chr_mode = true;
                    if ((in_val & 0x40) == 0) g_prg_mode = false; else g_prg_mode = true;
                    g_select_bank = in_val & 0x07;
                }
                else
                {
                    g_bankreg[g_select_bank] = in_val;
                }
                if (g_prg_mode == false)
                {
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(0, g_bankreg[6]);
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(1, g_bankreg[7]);
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(2, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(3, nes_main.g_nes_mapper_control.g_prg_bank_num - 1);
                }
                else
                {
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(0, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(1, g_bankreg[7]);
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(2, g_bankreg[6]);
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(3, nes_main.g_nes_mapper_control.g_prg_bank_num - 1);
                }
                if (g_chr_mode == false)
                {
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(0, (g_bankreg[0] & 0xfe));
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(1, (g_bankreg[0] & 0xfe) + 1);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(2, (g_bankreg[1] & 0xfe));
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(3, (g_bankreg[1] & 0xfe) + 1);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(4, g_bankreg[2]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(5, g_bankreg[3]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(6, g_bankreg[4]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(7, g_bankreg[5]);
                }
                else
                {
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(0, g_bankreg[2]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(1, g_bankreg[3]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(2, g_bankreg[4]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(3, g_bankreg[5]);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(4, (g_bankreg[0] & 0xfe));
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(5, (g_bankreg[0] & 0xfe) + 1);
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(6, (g_bankreg[1] & 0xfe));
                    nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(7, (g_bankreg[1] & 0xfe) + 1);
                }
            }
        }
        public void cpu_irq_clock(int in_clock)
        {
        }
        public void ppu_irq_line()
        {
            if ((nes_main.g_nes_ppu.g_io_2001_4_SPSHOW == true) || (nes_main.g_nes_ppu.g_io_2001_3_BGSHOW == true))
            {
                if ((g_irq_counter == 0) || (g_irq_reload == true))
                {
                    int wcnt = g_irq_counter;
                    g_irq_counter = g_irq_latch;
                    g_irq_reload = false;
                }
                else
                {
                    g_irq_counter -= 1;
                    if ((g_irq_enable == true) && (g_irq_counter == 0 && nes_main.g_nes_ppu.g_scanline != 0))
                    {
                        nes_main.g_nes_6502.interrupt_IRQ = true;
                    }
                    else
                    {
                        nes_main.g_nes_6502.interrupt_IRQ = false;
                    }
                }
            }
        }
    }
}
