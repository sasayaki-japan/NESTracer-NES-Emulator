using System.Reflection.Emit;

namespace NESTracer
{
    internal partial class nes_6502
    {
        private void NZ_check(byte in_val)
        {
            if ((in_val & 0x80) == 0x80) g_flag_N = true; else g_flag_N = false;
            if (in_val == 0) g_flag_Z = true; else g_flag_Z = false;
        }
        private void C_check_add(uint in_val1)
        {
            if ((in_val1 & 0x100) == 0x100) g_flag_C = true; else g_flag_C = false;
        }
        private void C_check_sub(int in_val)
        {
            if ((~in_val & 0x0100) == 0x0100) g_flag_C = true; else g_flag_C = false;
        }
        private void V_check(byte in_val1, byte in_val2, byte in_val3)
        {
            if ((~(in_val1 ^ in_val2) & (in_val1 ^ in_val3) & 0x80) == 0x80) g_flag_V = true; else g_flag_V = false;
        }
        private void push1(byte in_val, string in_name)
        {
            g_stack[g_reg_S].bank = -1;
            g_stack[g_reg_S].type = in_name;
            g_stack[g_reg_S].address = in_val;
            nes_main.g_nes_bus.write1((ushort)(0x0100 + g_reg_S), in_val);
            g_reg_S -= 1;
        }
        private byte pop1()
        {
            g_reg_S += 1;
            byte w_val = nes_main.g_nes_bus.read1((ushort)(0x0100 + g_reg_S));
            g_stack[g_reg_S].bank = -1;
            g_stack[g_reg_S].type = "";
            g_stack[g_reg_S].address = -1;
            return w_val;
        }
        private void push2(ushort in_val, string in_name)
        {
            g_stack[g_reg_S].bank = nes_main.g_nes_bus.cpu_get_bank(in_val);
            g_stack[g_reg_S].type = in_name;
            g_stack[g_reg_S].address = in_val;
            g_reg_S -= 1;
            nes_main.g_nes_bus.write2((ushort)(0x0100 + g_reg_S), in_val);
            g_reg_S -= 1;
        }
        private (int bank, ushort offset, string type, ushort address) pop2_inf(int in_s)
        {
            return (g_stack[in_s].bank, (ushort)(g_stack[in_s].address % 0x2000), g_stack[in_s].type, (ushort)g_stack[in_s].address);
        }
        private ushort pop2()
        {
            g_reg_S += 1;
            ushort w_address = nes_main.g_nes_bus.read2((ushort)(0x0100 + g_reg_S));
            g_reg_S += 1;
            g_stack[g_reg_S].bank = -1;
            g_stack[g_reg_S].type = "";
            g_stack[g_reg_S].address = -1;
            return w_address;
        }
        private void push_P()
        {
            byte w_val = 0;
            if (g_flag_N == true) w_val = 0x80;
            if (g_flag_V == true) w_val |= 0x40;
            w_val |= 0x20;
            if (g_flag_B == true) w_val |= 0x10;
            if (g_flag_D == true) w_val |= 0x08;
            if (g_flag_I == true) w_val |= 0x04;
            if (g_flag_Z == true) w_val |= 0x02;
            if (g_flag_C == true) w_val |= 0x01;
            push1(w_val, "g_reg_P");
        }
        private void pop_P()
        {
            byte w_val = pop1();
            if ((w_val & 0x80) == 0x80) g_flag_N = true; else g_flag_N = false;
            if ((w_val & 0x40) == 0x40) g_flag_V = true; else g_flag_V = false;
            if ((w_val & 0x10) == 0x10) g_flag_B = true; else g_flag_B = false;
            if ((w_val & 0x08) == 0x08) g_flag_D = true; else g_flag_D = false;
            if ((w_val & 0x04) == 0x04) g_flag_I = true; else g_flag_I = false;
            if ((w_val & 0x02) == 0x02) g_flag_Z = true; else g_flag_Z = false;
            if ((w_val & 0x01) == 0x01) g_flag_C = true; else g_flag_C = false;
        }
    }
}
