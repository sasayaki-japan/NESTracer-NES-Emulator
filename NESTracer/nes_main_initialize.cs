using MDTracer;
using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_main
    {
        public static void initialize()
        {
            Process currentProcess = Process.GetCurrentProcess();
            int processId = currentProcess.Id;
            currentProcess.PriorityClass = ProcessPriorityClass.High;

            g_nes_cartridge = new nes_cartridge();
            g_nes_mapper_control = new nes_mapper_control();
            g_nes_bus = new nes_bus();
            g_nes_6502 = new nes_6502();
            g_nes_ppu = new nes_ppu();
            g_nes_apu = new nes_apu();
            g_nes_io = new nes_io();

            g_form_setting = new Form_Setting();
            g_form_code_trace = new Form_Code_Trace();
            g_form_code = new Form_Code();
            g_form_code_bank = new Form_Code_Bank();

            g_form_music = new Form_MUSIC();
            g_form_ppu_screen = new Form_PPU_Screen();
            g_form_pattern = new Form_Pattern();
            g_form_io = new Form_IO();
            g_form_registry = new Form_Registry();

            g_form_music.initialize();
            g_form_ppu_screen.initialize();
            g_form_io.initialize();
            
            g_setting_name = new List<string>();
            g_setting_val = new List<string>();
            g_task_usage = 0;
        }
    }
}
