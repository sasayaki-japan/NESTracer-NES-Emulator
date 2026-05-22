using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Net;

namespace NESTracer
{
    internal partial class nes_6502
    {
        public ushort g_reg_PC;
        public byte g_reg_A;
        public byte g_reg_X;
        public byte g_reg_Y;
        public byte g_reg_S;

        public bool g_flag_N;
        public bool g_flag_V;
        public bool g_flag_B;
        public bool g_flag_D;
        public bool g_flag_I;
        public bool g_flag_Z;
        public bool g_flag_C;
        public ushort g_initial_PC;
        public struct STACKLIST                 
        {
            public int bank;                    
            public string type;                 
            public int address;                 
        }
        public STACKLIST[] g_stack;      
        public const int STACK_SIZE = 256;      

        public bool interrupt_NMI;       
        public bool interrupt_RESET;  　 
        public bool interrupt_IRQ;
        public bool interrupt_NMI_act;
        public bool interrupt_IRQ_act;

        private OPLIST g_op;
        private byte g_opcode;
        public double g_clock_total;
        public int g_clock_opt;
        public int g_clock_now;
        //----------------------------------------------------------------
        public nes_6502()
        {
            initialize();
        }
        public void run(float in_clock)
        {
            int w_interrupt_clock = interrupt_chk();
            int w_sprite_hit_cnt = nes_main.g_nes_ppu.get_sprite_zero_hit();

            g_clock_total += in_clock;
            g_clock_now = w_interrupt_clock;
            while (g_clock_now < g_clock_total)
            {
                nes_main.g_form_code_trace.CPU_Trace(g_reg_PC);
	            g_opcode = nes_main.g_nes_bus.read1(g_reg_PC);
                g_op = g_oplist[g_opcode];
                if (g_clock_total < g_clock_now + g_op.clock) break;
                g_reg_PC += 1;
                g_clock_opt = 0;
                g_op.func();
                g_reg_PC += (ushort)(g_op.size - 1);
                g_clock_now += g_op.clock + g_clock_opt;
                if (w_sprite_hit_cnt < g_clock_now)
                {
                    nes_main.g_nes_ppu.set_sprite_zero_hit();
                    w_sprite_hit_cnt = 10000;
                }
            }
            g_clock_total -= g_clock_now;
        }
        private int interrupt_chk()
        {
            int w_clock = 0;
            if (interrupt_RESET == true)
            {
                interrupt_RESET = false;
                g_flag_I = true;
                g_reg_PC = nes_main.g_nes_bus.read2(0xfffc);
            }
            else
            if (interrupt_NMI == true)
            {
                interrupt_NMI = false;
                interrupt_NMI_act = true;
                push2(g_reg_PC, "NMI");
                g_flag_B = false;
                push_P();
                g_flag_I = true;
                g_reg_PC = nes_main.g_nes_bus.read2(0xfffa);
                w_clock = 7;
            }
            else
            if ((interrupt_IRQ == true) && (g_flag_I == false))
            {
                interrupt_IRQ = false;
                interrupt_IRQ_act = true;
                push2(g_reg_PC, "IRQ");
                g_flag_B = false;
                push_P();
                g_flag_I = true;
                g_reg_PC = nes_main.g_nes_bus.read2(0xfffe);
                w_clock = 7;
            }
            return w_clock;
        }
    }
}
