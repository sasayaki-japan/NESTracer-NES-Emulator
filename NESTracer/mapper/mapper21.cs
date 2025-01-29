using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;

namespace NESTracer
{
    internal class mapper21
    {
        public static int g_mapper_type;                    
        public static int[,,] g_reg = {{{ 0, 2, 4, 6 },{ 0, 0x40, 0x80, 0xc0 }},    
                                        {{ 0, 2, 1, 3 },{ 0, 2, 1, 3 }},            
                                        {{ 0, 1, 2, 3 },{ 0, 4, 8, 12 }},           
                                        {{ 0, 2, 1, 3 },{ 0, 8, 4, 12 }}};          
        public const int BANKg_reg_NUM = 8;                   
        public int g_select_bank;                           
        public static int[] g_bankreg;                      

        public static int g_irq_enable;
        public static byte g_irq_counter;
        public static byte g_irq_latch;
        public static float g_irq_clock;
        public void initialize()
        {
            g_bankreg = new int[BANKg_reg_NUM];
            switch(nes_main.g_nes_mapper_control.g_mapper_num)
            {
                case 21:
                    g_mapper_type = 0;
                    break;
                case 22:
                    g_mapper_type = 1;
                    break;
                case 23:
                    g_mapper_type = 2;
                    break;
                case 25:
                    g_mapper_type = 3;
                    break;
            }
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
            nes_main.g_nes_mapper_control.set_chr_rom_bank_8k(0, 0);
        }
        public void cpu_write1(int in_address, byte in_val)
        {
            int w_addr_high = in_address & 0xf000;
            int w_addr_low = in_address & 0x00ff;
            for(int i = 0; i < 4; i++)
            {
                if ((g_reg[g_mapper_type, 0, i] == w_addr_low) ||
                    (g_reg[g_mapper_type, 1, i] == w_addr_low))
                {
                    w_addr_low = i;
                    break;
                }
            }
            switch (w_addr_high)
            {
                case 0x8000:
                    if (g_select_bank == 0)
                    {
                        nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(0, in_val);
                        nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(2, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
                    }
                    else
                    {
                        nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(0, nes_main.g_nes_mapper_control.g_prg_bank_num - 2);
                        nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(2, in_val);
                    }
                    break;
                case 0x9000:
                    if (w_addr_low == 2)
                    {
                        if(g_select_bank != (in_val & 0x02))
                        {
                            nes_main.g_nes_mapper_control.swap_prg_rom_bank_8k(0, 2);
                        }
                        g_select_bank = in_val & 0x02;
                    }
                    else
                    if (w_addr_low == 0)
                    {
                        switch (in_val)
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
                    }
                    break;
                case 0xa000:
                    nes_main.g_nes_mapper_control.set_prg_rom_bank_8k(1, in_val);
                    break;
                case 0xb000:
                case 0xc000:
                case 0xd000:
                case 0xe000:
                    int w_bank = ((in_address - 0xb000) >> 11) + (w_addr_low >> 1);
                    if ((w_addr_low & 1) == 0)
                    {
                        g_bankreg[w_bank] = (g_bankreg[w_bank] & 0xf0) | (in_val & 0x0f);
                        nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(w_bank, g_bankreg[w_bank]);
                    }
                    else
                    {
                        g_bankreg[w_bank] = (g_bankreg[w_bank] & 0x0f) | ((in_val & 0x0f) << 4);
                        nes_main.g_nes_mapper_control.set_chr_rom_bank_1k(w_bank, g_bankreg[w_bank]);
                    }
                    break;
                case 0xf000:
                    switch (w_addr_low)
                    {
                        case 0:
                            g_irq_latch = (byte)((g_irq_latch & 0xf0) | (in_val & 0x0f));
                            break;
                        case 1:
                            g_irq_latch = (byte)((g_irq_latch & 0x0f) | ((in_val & 0x0f) << 4));
                            break;
                        case 2:
                            g_irq_enable = in_val & 0x03;
                            g_irq_counter = g_irq_latch;
                            g_irq_clock = 0;
                            break;
                        case 3:
                            g_irq_enable = (g_irq_enable & 0x01) * 3;
                            break;
                    }
                    break;
            }
        }
        public void cpu_irq_clock(int in_clock)
        {
            for (int i = 0; i < in_clock; i++)
            {
                if ((g_irq_enable & 0x02) == 0x02)
                {
                    g_irq_clock += 1;
                    while (g_irq_clock >= 113.667f)
                    {
                        g_irq_clock -= 113.667f;
                        g_irq_counter++;
                        if (g_irq_counter == 0)
                        {
                            g_irq_counter = g_irq_latch;
                            nes_main.g_nes_6502.interrupt_IRQ = true;
                        }
                    }
                }
            }
        }
        public void ppu_irq_line()
        {
        }

    }
}
