namespace NESTracer
{
    internal partial class nes_6502
    {
        public byte[] g_ram;
        public byte[,] g_rom;

        public byte read1(ushort in_address)
        {
            byte w_out = 0;
            if (in_address >= 0x8000)
            {
                w_out = nes_main.g_nes_mapper_control.prg_read1(in_address);
            }
            else
            if (in_address <= 0x17ff)
            {
                in_address &= 0x07ff;
                w_out = g_ram[in_address];
            }
            else
            if (in_address <= 0x7fff)
            {
                w_out = g_ram[in_address];
            }
            return w_out;
        }
        public ushort read2(ushort in_address)
        {
            ushort w_out = 0;
            if (in_address >= 0x8000)
            {
                w_out = nes_main.g_nes_mapper_control.prg_read1(in_address);
                in_address += 1;
                w_out += (ushort)(nes_main.g_nes_mapper_control.prg_read1(in_address) << 8);
            }
            else
            if (in_address <= 0x17ff)
            {
                in_address &= 0x07ff;
                w_out = (ushort)(g_ram[in_address]
                        + (g_ram[in_address + 1] << 8));
            }
            else
            if (in_address <= 0x7fff)
            {
                w_out = g_ram[in_address];
                in_address += 1;
                w_out += (ushort)(g_ram[in_address] << 8);
            }
            return w_out;
        }
        //----------------------------------------------------------------
        //メモリ書き込み
        //----------------------------------------------------------------
        public void write1(ushort in_address, byte in_val)
        {
            if (in_address <= 0x17ff)
            {
                in_address &= 0x07ff;
                g_ram[in_address] = in_val;
            }
            else
            if (in_address <= 0x7fff)
            {
                g_ram[in_address] = in_val;
            }
            else
            if (0x8000 <= in_address)
            {
                nes_main.g_nes_mapper_control.cpu_write1(in_address, in_val);
            }
        }
        public void write2(ushort in_address, ushort in_val)
        {
            in_address &= 0x07ff;
            g_ram[in_address] = (byte)(in_val % 256);
            g_ram[in_address + 1] = (byte)(in_val / 256);
        }
    }
}
