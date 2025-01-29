using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTracer
{
    internal class mapper1
    {
        public byte g_shift_val;                            
        public int g_shift_cur;                             
        public byte[] g_control_reg;                        
        public void initialize()
        {
            g_control_reg = new byte[4];
            g_control_reg[0] = 0x0c;
            g_shift_cur = 0;
            g_shift_val = 0;
        }
        public void prg_rom_setting()
        {
            nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(0, 0);
            nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(2, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
        }
        public void chr_rom_setting()
        {
            nes_main.g_nes_mapper_control.set_chr_rom_bank_8k(0, 0);
        }
        public void cpu_write1(int in_address, byte in_val)
        {
            if((in_val & 0x80) == 0x80)
            {
                g_control_reg[0] |= 0x0c;
                g_control_reg[1] = 0;
                g_control_reg[2] = 0;
                g_control_reg[3] = 0;
                g_shift_cur = 0;
                g_shift_val = 0;
            }
            else
            {
                if ((in_val & 0x01) == 0x01)
                {
                    g_shift_val = (byte)(g_shift_val | (1 << g_shift_cur));
                }
                g_shift_cur += 1;
                if (g_shift_cur == 5)
                {
                    int w_g_reg_num = (in_address & 0x7FFF) >> 13;
                    g_control_reg[w_g_reg_num] = g_shift_val;
                    g_shift_cur = 0;
                    g_shift_val = 0;
                    switch (w_g_reg_num)
                    {
                        case 0:
                            switch (g_control_reg[0] & 0x03)
                            {
                                case 0:
                                    nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_arrangement = 0;
                                    break;
                                case 1:
                                    nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_arrangement = 1;
                                    break;
                                case 2:
                                    nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_arrangement = 3;
                                    break;
                                case 3:
                                    nes_main.g_nes_ppu.g_nametable_bank[0] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[1] = 0x2000;
                                    nes_main.g_nes_ppu.g_nametable_bank[2] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_bank[3] = 0x2400;
                                    nes_main.g_nes_ppu.g_nametable_arrangement = 2;
                                    break;
                            }
                            break;
                        case 1:
                            if ((g_control_reg[0] & 0x10) == 0x10)
                            {
                                nes_main.g_nes_mapper_control.set_chr_rom_bank_4k(0, g_control_reg[1] << 2);
                            }
                            else
                            {
                                nes_main.g_nes_mapper_control.set_chr_rom_bank_8k(0, (g_control_reg[1] & 0xfe) << 2);
                            }
                            break;
                        case 2:
                            if ((g_control_reg[0] & 0x10) == 0x10)
                            {
                                nes_main.g_nes_mapper_control.set_chr_rom_bank_4k(4, g_control_reg[2] << 2);
                            }
                            else
                            {
                                nes_main.g_nes_mapper_control.set_chr_rom_bank_8k(0, (g_control_reg[2] & 0xfe) << 2);
                            }
                            break;
                        case 3:
                            switch ((g_control_reg[0] & 0x0c) >> 2)
                            {
                                case 0:
                                case 1:
                                    nes_main.g_nes_mapper_control.set_prg_rom_bank_32k(0, (g_control_reg[3] & 0xfe));
                                    break;
                                case 2:
                                    nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(0, 0);
                                    nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(2, g_control_reg[3] << 1);
                                    break;
                                case 3:
                                    nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(0, g_control_reg[3] << 1);
                                    nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(2, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
                                    break;
                            }
                            break;
                    }
                }
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
