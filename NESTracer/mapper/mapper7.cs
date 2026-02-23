using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTracer
{
    internal class mapper7
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
            nes_main.g_nes_mapper_control.set_prg_rom_bank_32k(0, 0);
        }
        public void chr_rom_setting()
        {
            nes_main.g_nes_mapper_control.set_chr_rom_bank_8k(0, 0);
        }
        public void cpu_write1(int in_address, byte in_val)
        {
            int bank = (in_val & 0x07);
            nes_main.g_nes_mapper_control.set_prg_rom_bank_32k(0, bank * 4);

            int baseAddr = 0;
            if ((in_val & 0x10) == 0)
            {
                baseAddr = 0x2000;
                nes_main.g_nes_ppu.g_nametable_arrangement = 0;
            }
            else
            {
                baseAddr = 0x2400;
                nes_main.g_nes_ppu.g_nametable_arrangement = 1;
            }

            nes_main.g_nes_ppu.g_nametable_bank[0] = baseAddr;
            nes_main.g_nes_ppu.g_nametable_bank[1] = baseAddr;
            nes_main.g_nes_ppu.g_nametable_bank[2] = baseAddr;
            nes_main.g_nes_ppu.g_nametable_bank[3] = baseAddr;
        }
        public void cpu_irq_clock(int in_clock)
        {
        }
        public void ppu_irq_line()
        {
        }
    }
}
