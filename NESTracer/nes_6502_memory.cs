namespace NESTracer
{
    internal partial class nes_6502
    {
        public byte[] g_ram;
        public byte[,] g_rom;

        public byte read1(ushort in_address)
        {
            return g_ram[in_address];
        }
        public ushort read2(ushort in_address)
        {
            return (ushort)(g_ram[in_address] + (g_ram[in_address + 1] << 8));
        }
        //----------------------------------------------------------------
        //メモリ書き込み
        //----------------------------------------------------------------
        public void write1(ushort in_address, byte in_val)
        {
            if (in_address < 0x8000)
            {
                g_ram[in_address] = in_val;
            }
            else
            {
                nes_main.g_nes_mapper_control.cpu_write1(in_address, in_val);
            }
        }
        public void write2(ushort in_address, ushort in_val)
        {
            g_ram[in_address] = (byte)(in_val % 256);
            g_ram[in_address + 1] = (byte)(in_val >> 8);
        }
    }
}
