using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    internal class nes_bus
    {
        //----------------------------------------------------------------
        //read
        //----------------------------------------------------------------
        public byte read1(ushort in_address)
        {
            byte w_out = 0;
            if (in_address >= 0x8000) w_out = nes_main.g_nes_6502.read1(in_address);
            else
            if (in_address <= 0x17ff) w_out = nes_main.g_nes_6502.read1(in_address);
            else
            if (in_address <= 0x3fff) w_out = nes_main.g_nes_ppu.read1(in_address);
            else
            if (in_address <= 0x4015) w_out = nes_main.g_nes_apu.read1(in_address);
            else
            if (in_address <= 0x4017) w_out = nes_main.g_nes_io.read1(in_address);
            else
            if (in_address <= 0x7fff) w_out = nes_main.g_nes_6502.read1(in_address);
            else MessageBox.Show("g_nes_bus.read1", "error");

            nes_main.g_form_code.memory_monitor_check(in_address, w_out, false);
            return w_out;
        }
        public ushort read2(ushort in_address)
        {
            ushort w_out = 0;
            if (in_address >= 0x8000) w_out = nes_main.g_nes_6502.read2(in_address);
            else
            if (in_address <= 0x17ff) w_out = nes_main.g_nes_6502.read2(in_address);
            else
            if (in_address <= 0x7fff) w_out = nes_main.g_nes_6502.read2(in_address);
            else MessageBox.Show("g_nes_bus.read2", "error");

            nes_main.g_form_code.memory_monitor_check(in_address, w_out, false);
            return w_out;
        }
        //----------------------------------------------------------------
        //write
        //----------------------------------------------------------------
        public void write1(ushort in_address, byte in_val)
        {
            nes_main.g_form_code.memory_monitor_check(in_address, in_val, true);

            if (in_address <= 0x17ff) nes_main.g_nes_6502.write1(in_address, in_val);
            else
            if (in_address <= 0x3fff) nes_main.g_nes_ppu.write1(in_address, in_val);
            else
            if (in_address == 0x4014) nes_main.g_nes_ppu.write1(in_address, in_val);
            else
            if (in_address <= 0x4015) nes_main.g_nes_apu.write1(in_address, in_val);
            else
            if (in_address == 0x4016) nes_main.g_nes_io.write1(in_address, in_val);
            else
            if (in_address == 0x4017)
            {
                nes_main.g_nes_io.write1(in_address, in_val);
                nes_main.g_nes_apu.write1(in_address, in_val);
            }
            else
            if (in_address <= 0x7fff) nes_main.g_nes_6502.write1(in_address, in_val);
            else
            if (0x8000 <= in_address) nes_main.g_nes_mapper_control.cpu_write1(in_address, in_val);
            else MessageBox.Show("g_nes_bus.write1", "error");
        }
        public void write2(ushort in_address, ushort in_val)
        {
            nes_main.g_form_code.memory_monitor_check(in_address, in_val, true);
            if (in_address <= 0x17ff) nes_main.g_nes_6502.write2(in_address, in_val);
            else MessageBox.Show("g_nes_bus.write2", "error");
        }
        //----------------------------------------------------------------
        //bank
        //----------------------------------------------------------------
        public int cpu_get_bank(int in_address)
        {
            if (in_address < 0x8000)
            {
                return -1;
            }
            else
            {
                return nes_main.g_nes_mapper_control.g_prg_bank_map[(in_address - 0x8000) >> 13];
            }
        }
        public int cpu_get_bank_offset(int in_address)
        {
            return in_address & 0x1fff;
        }
        public int ppu_get_bank(int in_pattern, int in_chr_num)
        {
            return nes_main.g_nes_mapper_control.g_chr_bank_map[(in_pattern * 4) + (in_chr_num / 64)];
        }
        public int ppu_get_bank_chr(int in_pattern, int in_chr_num)
        {
            return in_chr_num & 0x3f;
        }
    }
}
