using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    public partial class Form_Code_Trace
    {
        public bool g_cpu_pause;
        private ManualResetEvent g_waitHandle;
        //----------------------------------------------------------------
        //trace event
        //----------------------------------------------------------------
        public void Trace_Start()
        {
            if (g_cpu_pause == true)
            {
                g_cpu_pause = false;
                g_waitHandle.Set();
            }
        }

        public void Trace_Stop()
        {
            g_cpu_pause = true;
        }
        public void Trace_StepIn()
        {
            if (g_cpu_pause == false)
            {
                g_cpu_pause = true;
            }
            else
            {
                g_waitHandle.Set();
            }
        }
        public void Trace_StepOver()
        {
            if (g_cpu_pause == false)
            {
                g_cpu_pause = true;
            }
            else
            {
                int w_line = analyse_code_addrewss2line(nes_main.g_nes_6502.g_reg_PC);
                TRACECODE w_code = read_analyse_code(w_line);
                if (w_code.operand_jsr == true)
                {
                    int w_line2 = analyse_code_addrewss2line((nes_main.g_nes_6502.g_reg_PC + w_code.leng));
                    TRACECODE w_code2 = read_analyse_code(w_line2);
                    w_code2.break_flash = true;
                    write_analyse_code(w_line2, w_code2);
                    g_cpu_pause = false;
                }
                g_waitHandle.Set();
            }
        }
        public void Trace_FirstStepBreak()
        {
            int w_line = analyse_code_addrewss2line(nes_main.g_nes_6502.g_initial_PC);
            TRACECODE w_code = read_analyse_code(w_line);
            w_code.break_flash = true;
            write_analyse_code(w_line, w_code);
        }
        //----------------------------------------------------------------
        public void CPU_Trace(int in_addr)
        {
            int w_line = analyse_code_addrewss2line(in_addr);
            write_analyse_code_set_chk(in_addr);

            int w_hit = nes_main.g_form_code.g_memory_monitor_hit;
            if (w_hit != -1)
            {
                int w_line2 = analyse_code_addrewss2line(nes_main.g_nes_6502.g_reg_PC);
                TRACECODE w_code2 = read_analyse_code(w_line2);
                w_code2.break_flash = true;
                write_analyse_code(w_line2, w_code2);
                nes_main.g_form_code.g_memory_monitor_hit = -1;
            }
            TRACECODE w_code = read_analyse_code(w_line);
            if (((g_cpu_pause == true) &&
                 ((nes_main.g_trace_sip == false) ||
                 ((nes_main.g_nes_6502.interrupt_NMI_act == false)
                 && (nes_main.g_nes_6502.interrupt_IRQ_act == false))))
                || (w_code.break_static == true)
                || (w_code.break_flash == true))
            {
                g_cpu_pause = true;
                w_code.break_flash = false;
                nes_main.g_form_code.g_stop_line = w_line;
                write_analyse_code(w_line, w_code);
                int w_line_offset = nes_main.g_form_code.pictureBox_Code_line_num() >> 1;

                nes_main.g_form_code_trace.analyses();
                nes_main.g_form_code.picturebox_scroll(w_line, -w_line_offset);
                nes_main.g_form_registry.Invalidate();
                g_waitHandle.WaitOne(Timeout.Infinite);
                g_waitHandle.Reset();
            }
        }
    }
}
