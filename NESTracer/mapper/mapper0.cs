using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESTracer
{
    internal class mapper0
    {
        public void initialize()
        {
        }
        public void prg_rom_setting()
        {
            if (nes_main.g_nes_mapper_control.g_prg_bank_num == 2)
            {
                nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(0, 0);
                nes_main.g_nes_mapper_control.set_prg_rom_bank_16k(2, 0);
            }
            else
            {
                nes_main.g_nes_mapper_control.set_prg_rom_bank_32k(0, 0);
            }

        }
        public void chr_rom_setting()
        {
            nes_main.g_nes_mapper_control.set_chr_rom_bank_8k(0, 0);
        }
        public void cpu_write1(int in_address, byte in_val)
        {
        }
        public void cpu_irq_clock(int in_clock)
        {
        }
        public void ppu_irq_line()
        {
        }
    }
}
