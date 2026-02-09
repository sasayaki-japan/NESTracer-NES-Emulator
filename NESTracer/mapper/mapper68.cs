namespace NESTracer
{
    internal class mapper68
    {
        public const int BANKg_reg_NUM = 4;
        public bool g_prg_ram_enable;
        public int g_bank_prg;
        public int[] g_bank_chr;
        public bool g_nametable_enable;

        public void initialize()
        {
            g_prg_ram_enable = false;
            g_nametable_enable = false;
            g_bank_prg = 0;
            g_bank_chr = new int[BANKg_reg_NUM];
        }
        public void prg_rom_setting()
        {
            nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(0, 0);
            nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(2, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
        }
        public void chr_rom_setting()
        {
        }
        public void cpu_write1(int in_address, byte in_val)
        {
            if (in_address >= 0xf000)
            {
                g_prg_ram_enable = ((in_val & 0x10) == 0x10);
                g_bank_prg = (in_val & 0x0f) * 2;
                nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(0, g_bank_prg);
                nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(1, g_bank_prg + 1);
            }
            else
            if (in_address >= 0xe000)
            {
                switch (in_val & 0x03)
                {
                    case 0:
                        nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_arrangement = 3;
                        break;
                    case 1:
                        nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_arrangement = 2;
                        break;
                    case 2:
                        nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2000;
                        nes_main.g_nes_ppu.g_nametable_arrangement = 0;
                        break;
                    case 3:
                        nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                        nes_main.g_nes_ppu.g_nametable_arrangement = 1;
                        break;
                }
                g_nametable_enable = ((in_val & 0x10) == 0x10);
            }
            else
            if (in_address >= 0xd000)
            {
                if (g_nametable_enable == true)
                {
                    int w_in_val = (in_val & 0x7f) + 0x80;
                    for (int i = 0; i < nes_mapper_control.CHR_ROM_BANK_SIZE; i++)
                    {
                        byte w_val = nes_main.g_nes_ppu.g_rom[w_in_val, i];
                        nes_main.g_nes_ppu.g_ram[0x2400 + i] = w_val;
                        nes_main.g_nes_ppu.set_attrtable(0x2400 + i, w_val);
                    }
                }
            }
            else
            if (in_address >= 0xc000)
            {
                if(g_nametable_enable == true)
                {
                    int w_in_val = (in_val & 0x7f) + 0x80;
                    for (int i = 0; i < nes_mapper_control.CHR_ROM_BANK_SIZE; i++)
                    {
                        byte w_val = nes_main.g_nes_ppu.g_rom[w_in_val, i];
                        nes_main.g_nes_ppu.g_ram[0x2000 + i] = w_val;
                        nes_main.g_nes_ppu.set_attrtable(0x2000 + i, w_val);
                    }
                }
            }
            else
            if (in_address >= 0xb000)
            {
                g_bank_chr[3] = (in_val & 0xff) * 2;
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(6, g_bank_chr[3]);
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(7, g_bank_chr[3] + 1);
            }
            else
            if (in_address >= 0xa000)
            {
                g_bank_chr[2] = (in_val & 0xff) * 2;
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(4, g_bank_chr[2]);
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(5, g_bank_chr[2] + 1);
            }
            else
            if (in_address >= 0x9000)
            {
                g_bank_chr[1] = (in_val & 0xff) * 2;
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(2, g_bank_chr[1]);
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(3, g_bank_chr[1] + 1);
            }
            else
            if (in_address >= 0x8000)
            {
                g_bank_chr[0] = (in_val & 0xff) * 2;
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(0, g_bank_chr[0]);
                nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(1, g_bank_chr[0] + 1);
            }
        }
        public void cpu_irq_clock(int in_clock)
        {
        }
        public void ppu_irq_line()
        {
        }
    }
}
