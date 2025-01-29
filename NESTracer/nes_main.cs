using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_main
    {
        public const float PPU_LINE_RENDER_CPU_CLOCK = 113.666f;

        public static Form_Setting g_form_setting;
        public static Form_PPU_Screen g_form_ppu_screen;
        public static Form_Pattern g_form_pattern;

        public static Form_Code g_form_code;
        public static Form_Code_Trace g_form_code_trace;
        public static Form_IO g_form_io;
        public static Form_MUSIC g_form_music;
        public static Form_Registry g_form_registry;
        public static Form_Code_Bank g_form_code_bank;

        public static nes_cartridge g_nes_cartridge;
        public static nes_mapper_control g_nes_mapper_control;
        public static nes_bus g_nes_bus;
        public static nes_6502 g_nes_6502;
        public static nes_ppu g_nes_ppu;
        public static nes_apu g_nes_apu;
        public static nes_io g_nes_io;

        public static bool g_ppu_enable;
        public static bool g_pattern_enable;
        public static bool g_code_enable;
        public static bool g_io_enable;
        public static bool g_music_enable;
        public static bool g_registry_enable;
        public static bool g_code_bank_enable;

        public static bool g_trace_fsb;
        public static bool g_trace_sip;
        public static bool g_hard_reset_req;
        public static bool g_trace_nextframe;

        private static int g_task_usage;
        //----------------------------------------------------------------
        public static bool run(string in_romname)
        {
            if (false == g_nes_cartridge.load(in_romname)) return false;
            g_nes_mapper_control.setting();
            nes_main.g_form_setting.update();
            nes_main.g_form_setting.show_window();
            g_nes_6502.setting();
            g_nes_ppu.setting();
            g_nes_6502.reset();
            g_form_code_trace.initialize();
            g_form_code_trace.update();
            //g_form_code_trace.CPU_Trace_push(Form_Code_Trace.STACK_LIST_TYPE.TOP, 0xfffc, g_nes_6502.g_reg_PC, 0, g_nes_6502.g_reg_S);
            g_form_code_bank.initialize();
            g_form_pattern.update();
            if (g_trace_fsb == true)
            {
                g_form_code_trace.Trace_FirstStepBreak();
                g_code_enable = true;
                g_form_setting.show_window();
                g_form_setting.update();
                write_setting();
            }

            Task<int> task_cpu = Task.Run<int>(() =>
            {
                md_run();
                return 0;
            });
            g_nes_ppu.g_waitHandle = new ManualResetEvent(false);
            Task<int> task_ppu = Task.Run<int>(() =>
            {
                g_nes_ppu.run();
                return 0;
            });
            return true;
        }
        public static void Nes_Screen_Update()
        {
            for (int i = 0; i < 5; i++)
            {
                Form_MUSIC.g_freq_out[i] = g_nes_apu.g_freq_out[i];
            }
            Form_Main.Instance.picture_update(g_task_usage);
            if (g_ppu_enable == true)
            {
                g_form_ppu_screen.picture_update();
                g_form_ppu_screen.Invalidate();
            }
            g_form_pattern.Invalidate();
            g_form_music.Invalidate();
        }

        public static void Trace_Screen_Update()
        {
            g_form_io.Invalidate();
            g_form_code.Invalidate();
            g_form_registry.Invalidate();
            g_form_code_bank.Invalidate();
        }

        private static void md_run()
        {
            int w_log_pef_sum = 0;
            int w_log_pef_cnt = 0;
            Stopwatch w_stopwatch = new Stopwatch();
            w_stopwatch.Start();

            while (true)
            {
                if (g_hard_reset_req == true)
                {
                    g_nes_6502.reset();
                    read_setting();
                    if (g_trace_fsb == true)
                    {
                        g_form_code_trace.Trace_FirstStepBreak();
                    }
                    g_hard_reset_req = false;
                }
                if (g_trace_nextframe == true)
                {
                    int w_line = g_form_code_trace.analyse_code_addrewss2line(g_nes_6502.g_reg_PC);
                    Form_Code_Trace.TRACECODE w_code = g_form_code_trace.read_analyse_code(w_line);
                    w_code.break_flash = true;
                    g_form_code_trace.write_analyse_code(w_line, w_code);
                    g_trace_nextframe = false;
                }
                for (int w_vline = 0; w_vline < nes_ppu.PPU_LINE_NUM; w_vline++)
                {
                    g_nes_ppu.run(w_vline);
                    g_nes_6502.run(PPU_LINE_RENDER_CPU_CLOCK);
                    g_nes_ppu.run2();
                    g_nes_apu.run((int)PPU_LINE_RENDER_CPU_CLOCK);
                }

                TimeSpan timeSpan2 = w_stopwatch.Elapsed;
                int wtime = 0;
                TimeSpan timeSpan;
                float w_wait = 16666.666f;

                timeSpan = w_stopwatch.Elapsed;
                wtime = (int)(timeSpan.TotalMilliseconds * 1000);
                int w_log_pef = (int)((wtime / w_wait) * 100);
                w_log_pef_sum += w_log_pef;
                w_log_pef_cnt += 1;
                if (w_log_pef_cnt % 60 == 0)
                {
                    g_task_usage = w_log_pef_sum / w_log_pef_cnt;
                    w_log_pef_cnt = 0;
                    w_log_pef_sum = 0;
                }
                do
                {
                    timeSpan = w_stopwatch.Elapsed;
                    wtime = (int)(timeSpan.TotalMilliseconds * 1000);
                } while ((w_wait > wtime) && (timeSpan.Seconds < 1));    //1,000,000 / 60
                w_stopwatch.Restart();
            }
        }
    }
}
