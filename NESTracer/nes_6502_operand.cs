using System.DirectoryServices.ActiveDirectory;
using System.Reflection.Emit;

namespace NESTracer
{
    internal partial class nes_6502
    {
        private void op_LDA() => NZ_check(g_reg_A = addressing_read());
        private void op_LDX() => NZ_check(g_reg_X = addressing_read());
        private void op_LDY() => NZ_check(g_reg_Y = addressing_read());
        private void op_STA() => addressing_write(g_reg_A);
        private void op_STX() => addressing_write(g_reg_X);
        private void op_STY() => addressing_write(g_reg_Y);
        private void op_TAX() => NZ_check(g_reg_X = g_reg_A);
        private void op_TAY() => NZ_check(g_reg_Y = g_reg_A);
        private void op_TSX() => NZ_check(g_reg_X = g_reg_S);
        private void op_TXA() => NZ_check(g_reg_A = g_reg_X);
        private void op_TXS() => g_reg_S = g_reg_X;
        private void op_TYA() => NZ_check(g_reg_A = g_reg_Y);
        private void op_ADC_SUB(byte w_val1)
        {
            uint w_val = (uint)g_reg_A + (uint)w_val1 + (uint)((g_reg_C == true) ? 1 : 0);
            C_check_add(w_val);
            V_check(g_reg_A, w_val1, (byte)w_val);
            g_reg_A = (byte)w_val;
            NZ_check(g_reg_A);
        }
        private void op_ADC() => op_ADC_SUB(addressing_read());
        private void op_AND() => NZ_check(g_reg_A &= addressing_read());
        private void op_ASL()
        {
            byte w_val = addressing_read();
            if ((w_val & 0x80) == 0x80) g_reg_C = true; else g_reg_C = false;
            w_val <<= 1;
            addressing_write(w_val);
            NZ_check(w_val);
        }
        private void op_BIT()
        {
            byte w_val = addressing_read();
            if ((w_val & 0x80) == 0x80) g_reg_N = true; else g_reg_N = false;
            if ((w_val & 0x40) == 0x40) g_reg_V = true; else g_reg_V = false;
            if ((w_val & g_reg_A) == 0) g_reg_Z = true; else g_reg_Z = false;
        }
        private void op_CMP()
        {
            byte w_val = addressing_read();
            int w_val2 = (int)g_reg_A - (int)w_val;
            byte w_val3 = (byte)w_val2;
            C_check_sub(w_val2);
            NZ_check(w_val3);
        }
        private void op_CPX()
        {
            byte w_val = addressing_read();
            int w_val2 = (int)g_reg_X - (int)w_val;
            byte w_val3 = (byte)w_val2;
            C_check_sub(w_val2);
            NZ_check(w_val3);
        }
        private void op_CPY()
        {
            byte w_val = addressing_read();
            int w_val2 = (int)g_reg_Y - (int)w_val;
            byte w_val3 = (byte)w_val2;
            C_check_sub(w_val2);
            NZ_check(w_val3);
        }
        private void op_DEC()
        {
            byte w_val = addressing_read();
            w_val -= 1;
            addressing_write(w_val);
            NZ_check(w_val);
        }
        private void op_DEX() => NZ_check(g_reg_X -= 1);
        private void op_DEY() => NZ_check(g_reg_Y -= 1);
        private void op_EOR() => NZ_check(g_reg_A ^= addressing_read());

        private void op_INC()
        {
            byte w_val = addressing_read();
            w_val += 1;
            addressing_write(w_val);
            NZ_check(w_val);
        }
        private void op_INX() => NZ_check(g_reg_X += 1);
        private void op_INY() => NZ_check(g_reg_Y += 1);
        private void op_LSR()
        {
            byte w_val = addressing_read();
            if ((w_val & 0x01) == 0x01) g_reg_C = true; else g_reg_C = false;
            w_val >>= 1;
            addressing_write(w_val);
            NZ_check(w_val);
        }
        private void op_ORA()
        {
            byte w_val = addressing_read();
            g_reg_A |= w_val;
            NZ_check(g_reg_A);
        }
        private void op_ROL()
        {
            byte w_val = addressing_read();
            bool old_c = g_reg_C;
            if ((w_val & 0x80) == 0x80) g_reg_C = true; else g_reg_C = false;
            w_val = (byte)(w_val << 1);
            if (old_c == true) w_val |= 0x01;
            addressing_write(w_val);
            NZ_check(w_val);
        }
        private void op_ROR()
        {
            byte w_val = addressing_read();
            bool old_c = g_reg_C;
            if ((w_val & 0x01) == 0x01) g_reg_C = true; else g_reg_C = false;
            w_val = (byte)(w_val >> 1);
            if (old_c == true) w_val |= 0x80;
            addressing_write(w_val);
            NZ_check(w_val);
        }
        private void op_SBC()
        {
            byte w_val = addressing_read();
            op_ADC_SUB((byte)(w_val ^ 0xff));
        }
        private void op_PHA() => push1(g_reg_A, "PHA");
        private void op_PHP()
        {
            byte w_val = 0;
            if (g_reg_N == true) w_val = 0x80;
            if (g_reg_V == true) w_val |= 0x40;
            w_val |= 0x20;
            w_val |= 0x10;
            if (g_reg_D == true) w_val |= 0x08;
            if (g_reg_I == true) w_val |= 0x04;
            if (g_reg_Z == true) w_val |= 0x02;
            if (g_reg_C == true) w_val |= 0x01;
            push1(w_val, "PHP");
        }
        private void op_PLA() => NZ_check(g_reg_A = pop1());
        private void op_PLP()
        {
            byte w_val = pop1();
            if ((w_val & 0x80) == 0x80) g_reg_N = true; else g_reg_N = false;
            if ((w_val & 0x40) == 0x40) g_reg_V = true; else g_reg_V = false;
            g_reg_R = true;
            g_reg_B = false;
            if ((w_val & 0x08) == 0x08) g_reg_D = true; else g_reg_D = false;
            if ((w_val & 0x04) == 0x04) g_reg_I = true; else g_reg_I = false;
            if ((w_val & 0x02) == 0x02) g_reg_Z = true; else g_reg_Z = false;
            if ((w_val & 0x01) == 0x01) g_reg_C = true; else g_reg_C = false;
        }
        private void op_JMP()
        {
            ushort w_addr_bk = g_reg_PC;
            g_reg_PC = (ushort)(addressing_address() - 2);
        }
        private void op_JSR()
        {
            push2((ushort)(g_reg_PC + 1), "JSR");
            ushort w_address = addressing_address();
            //nes_main.g_form_code_trace.CPU_Trace_push(Form_Code_Trace.STACK_LIST_TYPE.JSR, 0xfffe, w_address, (uint)(g_reg_PC - 1), g_reg_S);
            g_reg_PC = (ushort)(w_address - 2);
        }
        private void op_RTS()
        {
            var w_ret = pop2_inf(g_reg_S + 2);
            uint w_pc = g_reg_PC;
            g_reg_PC = (ushort)(pop2() + 1);
            //nes_main.g_form_code_trace.CPU_Trace_pop(g_reg_PC, w_pc, g_reg_S);
        }
        private void op_RTI()
        {
            interrupt_NMI_act = false;
            interrupt_IRQ_act = false;
            
            pop_P();

            ushort w_addr = 0;
            var w_ret = pop2_inf(g_reg_S + 2);
            if(w_ret.type == "IRQ")
            {
                w_addr = nes_main.g_nes_bus.read2(0xFFFE);
            }
            else
            if (w_ret.type == "NMI")
            {
                w_addr = nes_main.g_nes_bus.read2(0xFFFA);
            }
            if (w_addr == 0)
            {
                w_addr = w_addr;
            }
            //Form_Code_Analyse.gcode_write_func_address(g_reg_PC - 1, nes_main.g_nes_mapper_control.g_prg_bank_num - 1, w_addr);

            g_reg_PC = pop2();
        }
        private void op_BCS() => Branch_SUB(g_reg_C == true);
        private void op_BCC() => Branch_SUB(g_reg_C == false);
        private void op_BEQ() => Branch_SUB(g_reg_Z == true);
        private void op_BNE() => Branch_SUB(g_reg_Z == false);
        private void op_BMI() => Branch_SUB(g_reg_N == true);
        private void op_BPL() => Branch_SUB(g_reg_N == false);
        private void op_BVS() => Branch_SUB(g_reg_V == true);
        private void op_BVC() => Branch_SUB(g_reg_V == false);
        private void Branch_SUB(bool evaluation)
        {
            if (evaluation == true)
            {
                ushort pc_back = g_reg_PC;
                sbyte w_offset = (sbyte)nes_main.g_nes_bus.read1(g_reg_PC);
                g_reg_PC = (ushort)(g_reg_PC + w_offset);
                if ((g_reg_PC & 0xff00) != (pc_back & 0xff00))
                {
                    g_clock_opt += 2;
                }
                else
                {
                    g_clock_opt += 1;
                }
            }
        }
        private void op_CLC() => g_reg_C = false;
        private void op_CLD() => g_reg_D = false;
        private void op_CLI() => g_reg_I = false;
        private void op_CLV() => g_reg_V = false;
        private void op_SEC() => g_reg_C = true;
        private void op_SED() => g_reg_D = true;
        private void op_SEI() => g_reg_I = true;
        private void op_BRK()
        {
            push_P();
            push2(g_reg_PC, "BRK");
            g_reg_B = true;
            g_reg_PC = nes_main.g_nes_bus.read2(0xFFFE);
        }
        private void op_NOP()
        {
        }
    }
}
