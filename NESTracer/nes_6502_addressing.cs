namespace NESTracer
{
    public enum ADDRESSING_TYPE : int
    {
        Non,
        Immediate,
        Zeropage,
        ZeropageX,
        ZeropageY,
        Absolute,
        AbsoluteX,
        AbsoluteY,
        IndirectX,
        IndirectY,
        Accumulator,
        Indirect,
        Relative,
        Implied
    }
    internal partial class nes_6502
    {

        public struct OPLIST
        {
            public Action func;
            public ADDRESSING_TYPE addr;
            public string opname_out;
            public string format;
            public ushort size;
            public int clock;
            public bool page_boundary;
        }
        public OPLIST[] g_oplist;

        private ushort addressing_address()
        {
            ushort w_address = 0;
            switch (g_op.addr)
            {
                case ADDRESSING_TYPE.Immediate:
                    w_address = g_reg_PC;
                    break;
                case ADDRESSING_TYPE.Zeropage:
                    w_address = nes_main.g_nes_bus.read1(g_reg_PC);
                    break;
                case ADDRESSING_TYPE.ZeropageX:
                    w_address = (ushort)((nes_main.g_nes_bus.read1(g_reg_PC) + g_reg_X) & 0xff);
                    break;
                case ADDRESSING_TYPE.ZeropageY:
                    w_address = (ushort)((nes_main.g_nes_bus.read1(g_reg_PC) + g_reg_Y) & 0xff);
                    break;
                case ADDRESSING_TYPE.Absolute:
                    w_address = nes_main.g_nes_bus.read2(g_reg_PC);
                    break;
                case ADDRESSING_TYPE.AbsoluteX:
                    w_address = (ushort)(nes_main.g_nes_bus.read2(g_reg_PC) + g_reg_X);
                    if(g_op.page_boundary == true)
                    {
                        if ((g_reg_PC & 0xff00) != (w_address & 0xff00))
                        {
                            g_clock_opt += 1;
                        }
                    }
                    break;
                case ADDRESSING_TYPE.AbsoluteY:
                    w_address = (ushort)(nes_main.g_nes_bus.read2(g_reg_PC) + g_reg_Y);
                    if (g_op.page_boundary == true)
                    {
                        if ((g_reg_PC & 0xff00) != (w_address & 0xff00))
                        {
                            g_clock_opt += 1;
                        }
                    }
                    break;
                case ADDRESSING_TYPE.Indirect:
                    w_address = nes_main.g_nes_bus.read2(g_reg_PC);
                    byte w_low = nes_main.g_nes_bus.read1(w_address);
                    byte w_high = nes_main.g_nes_bus.read1((ushort)((w_address & 0xff00) | (w_address + 1) & 0x00ff));
                    w_address = (ushort)(w_low | (w_high << 8));
                    break;
                case ADDRESSING_TYPE.IndirectX:
                    w_address = (ushort)(nes_main.g_nes_bus.read1(g_reg_PC) + g_reg_X);
                    w_address = (ushort)(nes_main.g_nes_bus.read1((ushort)(w_address & 0xff))
                        | (nes_main.g_nes_bus.read1((ushort)((w_address + 1) & 0xff)) << 8));
                    break;
                case ADDRESSING_TYPE.IndirectY:
                    w_address = nes_main.g_nes_bus.read1(g_reg_PC);
                    w_address = (ushort)(nes_main.g_nes_bus.read1(w_address)
                        | (nes_main.g_nes_bus.read1((ushort)((w_address + 1) & 0xff)) << 8));
                    w_address = (ushort)(w_address + g_reg_Y);
                    if (g_op.page_boundary == true)
                    {
                        if ((g_reg_PC & 0xff00) != (w_address & 0xff00))
                        {
                            g_clock_opt += 1;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("addressing_read", "error");
                    break;
            }
            return w_address;
        }
        private byte addressing_read()
        {
            byte w_out = 0;
            if (g_op.addr == ADDRESSING_TYPE.Accumulator)
            {
                w_out = g_reg_A;
            }
            else
            {
                w_out = nes_main.g_nes_bus.read1(addressing_address());
            }
            return w_out;
        }

        private void addressing_write(byte in_data)
        {
            if (g_op.addr == ADDRESSING_TYPE.Accumulator)
            {
                g_reg_A = in_data;
            }
            else
            {
                nes_main.g_nes_bus.write1(addressing_address(), in_data);
            }
        }
    }
}
