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
        public bool g_reg_N;
        public bool g_reg_V;
        public bool g_reg_R;
        public bool g_reg_B;
        public bool g_reg_D;
        public bool g_reg_I;
        public bool g_reg_Z;
        public bool g_reg_C;
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
        public Int64 g_clock_now;
        public int g_clock_opt;
        //----------------------------------------------------------------
        public nes_6502()
        {
            initialize();
        }
        public void run(float in_clock)
        {
            interrupt_chk();
            g_clock_total += in_clock;
            while (g_clock_now < g_clock_total)
            {
                nes_main.g_form_code_trace.CPU_Trace(g_reg_PC);

                g_clock_opt = 0;
	            g_opcode = nes_main.g_nes_bus.read1(g_reg_PC);
	            g_op = g_oplist[g_opcode];
                g_reg_PC += 1;
                g_op.func();
                g_reg_PC += (ushort)(g_op.size - 1);
                g_clock_now += g_op.clock + g_clock_opt;
            }
        }
        private void interrupt_chk()
        {
            if (((nes_main.g_nes_apu.g_apu_reg[0x15] & 0x40) == 0x40) && (g_reg_I == false))
            {
                interrupt_IRQ = true;
            }
            if (interrupt_RESET == true)
            {
                interrupt_RESET = false;
                g_reg_I = true;
                g_reg_PC = nes_main.g_nes_bus.read2(0xFFFC);
            }
            else
            if ((interrupt_IRQ == true) && (g_reg_I == false))
            {
                interrupt_IRQ = false;
                interrupt_IRQ_act = true;
                push2(g_reg_PC, "IRQ");
                g_reg_B = false;
                push_P();
                g_reg_I = true;
                g_reg_PC = nes_main.g_nes_bus.read2(0xFFFE);
                //nes_main.g_form_code_trace.CPU_Trace_push(Form_Code_Trace.STACK_LIST_TYPE.IRQ, 0xfffe, g_reg_PC, g_reg_PC, g_reg_S);
                g_clock_opt += 7;
            }
            else
            if (interrupt_NMI == true)
            {
                interrupt_NMI = false;
                interrupt_NMI_act = true;
                push2(g_reg_PC, "NMI");
                g_reg_B = false;
                push_P();
                g_reg_I = true;
                g_reg_PC = nes_main.g_nes_bus.read2(0xFFFA);
                //nes_main.g_form_code_trace.CPU_Trace_push(Form_Code_Trace.STACK_LIST_TYPE.NMI, 0xfffa, g_reg_PC, g_reg_PC, g_reg_S);
                g_clock_opt += 7;
            }
        }
    }
}
