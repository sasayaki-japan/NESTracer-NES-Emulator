using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    public partial class Form_Registry : Form
    {
        public int g_screen_xpos;
        public int g_screen_ypos;
        public class ParamView
        {
            public string name { get; set; }
            public string value { get; set; }
        }
        private static List<ParamView> g_paramview_cpu;
        private static List<ParamView> g_paramview_ppu;
        private static List<ParamView> g_paramview_apu;
        private static List<ParamView> g_paramview_cartrig;

        public static string[] CPU_NAME = {
            "ProgramCounter",
            "register_S",
            "register_A",
            "register_X",
            "register_Y",
            "status_N",
            "status_V",
            "status_Z",
            "status_C",
            "cpu memory bank0",
            "cpu memory bank1",
            "cpu memory bank2",
            "cpu memory bank3"
        };
        public static string[] PPU_NAME = {
            "2000_7_VBLANK",
            "2000_6_PPUMODE",
            "2000_5_SPSIZE",
            "2000_4_BGMEM",
            "2000_3_SPMEM",
            "2000_2_OFFSET",
            "2000_1_SCREEN",
            "2001_7_BEmpasize",
            "2001_6_GEmpasize",
            "2001_5_REmpasize",
            "2001_4_SPSHOW",
            "2001_3_BGSHOW",
            "2001_2_SPleftmost",
            "2001_1_BGleftmost",
            "2001_0_COLORMODE",
            "2002_7_VBLANK",
            "2002_6_SPRITE",
            "2002_5_SPOVER",
            "2002_4_ACTIVE",
            "2003_oam_offset",
            "2005_scroll_x",
            "2005_scroll_y",
            "scanline",
            "scanline address",
            "nametable arrangement",
            "ppu memory bank0",
            "ppu memory bank1",
            "ppu memory bank2",
            "ppu memory bank3",
            "ppu memory bank4",
            "ppu memory bank5",
            "ppu memory bank6",
            "ppu memory bank7"
        };
        public static string[] APU_NAME = {
            "4000_duty",
            "4000_len_count_enable",
            "4000_envelope_enable",
            "4000_volume",
            "4001_sweep_enable",
            "4001_sweep_value",
            "4001_sweep_negate",
            "4001_sweep_shift",
            "4002_freq",
            "4003_len_count",
            "4004_duty",
            "4004_len_count_enable",
            "4004_envelope_enable",
            "4004_volume",
            "4005_sweep_enable",
            "4005_sweep_value",
            "4005_sweep_negate",
            "4005_sweep_shift",
            "4006_freq",
            "4007_len_count",
            "4008_len_count_enable",
            "4008_linear_value",
            "400a_freq",
            "400b_len_count",
            "400c_len_count_enable",
            "400c_envelope_enable",
            "400c_volume",
            "400e_freq",
            "400e_noisetype ",
            "400f_len_count",
            "4010_freq",
            "4010_loop",
            "4010_irq",
            "4011_value",
            "4012_address",
            "4013_length",
            "pulse1 enable",
            "pulse1 freq(Hz)",
            "pulse1 envelope_vol",
            "pulse1 envelope_count",
            "pulse1 sweep_freqlimit",
            "pulse1 sweep_count",
            "pulse1 counter",
            "pulse1 vol",
            "pulse1 duty_cnt",
            "pulse2 enable",
            "pulse2 freq(Hz)",
            "pulse2 envelope_vol",
            "pulse2 envelope_count",
            "pulse2 sweep_freqlimit",
            "pulse2 sweep_count",
            "pulse2 counter",
            "pulse2 vol",
            "pulse2 duty_cnt",
            "triangle  enable",
            "triangle freq(Hz)",
            "triangle linear_count_reset",
            "triangle linear_count",
            "triangle counter",
            "triangle vol",
            "triangle duty_cnt",
            "noize enable",
            "noize freq(Hz)",
            "noize envelope_vol",
            "noize envelope_count",
            "noize c_shift_reg",
            "noize c_shift_bit0",
            "noize counter",
            "noize vol",
            "dmc enable",
            "dmc freq(Hz)",
            "dmc cur_address",
            "dmc cur_count",
            "dmc cur_byte",
            "dmc counter"
        };
        public static string[] CARTRIG_NAME = {
            "prg-rom size",
            "chrrom size",
            "mapper no",
            "prg-bank max",
            "chr-bank max"
        };

        public class CallView
        {
            public string stack { get; set; }
            public string val { get; set; }
            public string bank { get; set; }
        }
        public static List<CallView> g_paramview_call;
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_Registry()
        {
            InitializeComponent();
            dataGridView_cpu.Font = new Font("Yu Gothic UI", 8);
            dataGridView_ppu.Font = new Font("Yu Gothic UI", 8);
            dataGridView_apu.Font = new Font("Yu Gothic UI", 8);
            dataGridView_cartrig.Font = new Font("Yu Gothic UI", 8);

            g_paramview_cpu = new List<ParamView>();
            for (int i = 0; i < CPU_NAME.Length; i++)
            {
                ParamView w_addval = new ParamView();
                w_addval.name = CPU_NAME[i];
                w_addval.value = "";
                g_paramview_cpu.Add(w_addval);
            }
            dataGridView_cpu.DataSource = g_paramview_cpu;

            g_paramview_ppu = new List<ParamView>();
            for (int i = 0; i < PPU_NAME.Length; i++)
            {
                ParamView w_addval = new ParamView();
                w_addval.name = PPU_NAME[i];
                w_addval.value = "";
                g_paramview_ppu.Add(w_addval);
            }
            dataGridView_ppu.DataSource = g_paramview_ppu;

            g_paramview_apu = new List<ParamView>();
            for (int i = 0; i < APU_NAME.Length; i++)
            {
                ParamView w_addval = new ParamView();
                w_addval.name = APU_NAME[i];
                w_addval.value = "";
                g_paramview_apu.Add(w_addval);
            }
            dataGridView_apu.DataSource = g_paramview_apu;

            g_paramview_cartrig = new List<ParamView>();
            for (int i = 0; i < CARTRIG_NAME.Length; i++)
            {
                ParamView w_addval = new ParamView();
                w_addval.name = CARTRIG_NAME[i];
                w_addval.value = "";
                g_paramview_cartrig.Add(w_addval);
            }
            dataGridView_cartrig.DataSource = g_paramview_cartrig;

            g_paramview_call = new List<CallView>(nes_6502.STACK_SIZE);
            for (int i = 0; i < nes_6502.STACK_SIZE; i++)
            {
                g_paramview_call.Add(new CallView { stack = "" });
            }
            dataGridView_call_stack.DataSource = g_paramview_call;
        }
        //----------------------------------------------------------------
        //Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void Form_Registry_FormClosing(object sender, FormClosingEventArgs e)
        {
            nes_main.g_registry_enable = false;
            nes_main.g_form_setting.update();
            nes_main.write_setting();
            e.Cancel = true;
        }

        private void Form_Registry_ResizeEnd(object sender, EventArgs e)
        {
            var currentPosition = this.Location;
            g_screen_xpos = currentPosition.X;
            g_screen_ypos = currentPosition.Y;
            nes_main.write_setting();
        }

        private void Form_Registry_Shown(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
        }
        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private void Form_Registry_Paint(object sender, PaintEventArgs e)
        {
            if (nes_main.g_form_code_trace.g_cpu_pause == true)
            {
                g_paramview_cpu[0].value = nes_main.g_nes_6502.g_reg_PC.ToString("x4");
                g_paramview_cpu[1].value = nes_main.g_nes_6502.g_reg_S.ToString();
                g_paramview_cpu[2].value = nes_main.g_nes_6502.g_reg_A.ToString();
                g_paramview_cpu[3].value = nes_main.g_nes_6502.g_reg_X.ToString();
                g_paramview_cpu[4].value = nes_main.g_nes_6502.g_reg_Y.ToString();
                g_paramview_cpu[5].value = nes_main.g_nes_6502.g_reg_N.ToString();
                g_paramview_cpu[6].value = nes_main.g_nes_6502.g_reg_V.ToString();
                g_paramview_cpu[7].value = nes_main.g_nes_6502.g_reg_Z.ToString();
                g_paramview_cpu[8].value = nes_main.g_nes_6502.g_reg_C.ToString();
                g_paramview_cpu[9].value = nes_main.g_nes_mapper_control.g_prg_bank_map[0].ToString();
                g_paramview_cpu[10].value = nes_main.g_nes_mapper_control.g_prg_bank_map[1].ToString();
                g_paramview_cpu[11].value = nes_main.g_nes_mapper_control.g_prg_bank_map[2].ToString();
                g_paramview_cpu[12].value = nes_main.g_nes_mapper_control.g_prg_bank_map[3].ToString();

                g_paramview_ppu[0].value = nes_main.g_nes_ppu.g_io_2000_7_VBLANK.ToString();
                g_paramview_ppu[1].value = nes_main.g_nes_ppu.g_io_2000_6_PPUMODE.ToString();
                g_paramview_ppu[2].value = nes_main.g_nes_ppu.g_io_2000_5_SPSIZE.ToString();
                g_paramview_ppu[3].value = nes_main.g_nes_ppu.g_io_2000_4_BGMEM.ToString();
                g_paramview_ppu[4].value = nes_main.g_nes_ppu.g_io_2000_3_SPMEM.ToString();
                g_paramview_ppu[5].value = nes_main.g_nes_ppu.g_io_2000_2_OFFSET.ToString();
                g_paramview_ppu[6].value = "$" + (2000 + (nes_main.g_nes_ppu.g_io_2000_1_SCREEN * 400)).ToString();
                g_paramview_ppu[7].value = nes_main.g_nes_ppu.g_io_2001_7_BEmpasize.ToString();
                g_paramview_ppu[8].value = nes_main.g_nes_ppu.g_io_2001_6_GEmpasize.ToString();
                g_paramview_ppu[9].value = nes_main.g_nes_ppu.g_io_2001_5_REmpasize.ToString();
                g_paramview_ppu[10].value = nes_main.g_nes_ppu.g_io_2001_4_SPSHOW.ToString();
                g_paramview_ppu[11].value = nes_main.g_nes_ppu.g_io_2001_3_BGSHOW.ToString();
                g_paramview_ppu[12].value = nes_main.g_nes_ppu.g_io_2001_2_SPleftmost.ToString();
                g_paramview_ppu[13].value = nes_main.g_nes_ppu.g_io_2001_1_BGleftmost.ToString();
                g_paramview_ppu[14].value = nes_main.g_nes_ppu.g_io_2001_0_COLORMODE.ToString();
                g_paramview_ppu[15].value = nes_main.g_nes_ppu.g_io_2002_7_VBLANK.ToString();
                g_paramview_ppu[16].value = nes_main.g_nes_ppu.g_io_2002_6_SPRITE.ToString();
                g_paramview_ppu[17].value = nes_main.g_nes_ppu.g_io_2002_5_SPOVER.ToString();
                g_paramview_ppu[18].value = nes_main.g_nes_ppu.g_io_2002_4_ACTIVE.ToString();
                g_paramview_ppu[19].value = nes_main.g_nes_ppu.g_io_2003_oam_offset.ToString();
//                g_paramview_ppu[20].value = nes_main.g_nes_ppu.g_io_2005_scroll_x.ToString();
//                g_paramview_ppu[21].value = nes_main.g_nes_ppu.g_io_2005_scroll_y.ToString();
                g_paramview_ppu[22].value = nes_main.g_nes_ppu.g_scanline.ToString();
//                g_paramview_ppu[23].value = nes_main.g_nes_ppu.g_ppu_reg_v.ToString("x4");
                string w_str = "";
                switch (nes_main.g_nes_ppu.g_nametable_arrangement)
                {
                    case 0: w_str = "one screen low"; break;
                    case 1: w_str = "one screen high"; break;
                    case 2: w_str = "vertical"; break;
                    case 3: w_str = "horizontal"; break;
                }
                g_paramview_ppu[24].value = w_str;
                g_paramview_ppu[25].value = nes_main.g_nes_mapper_control.g_chr_bank_map[0].ToString();
                g_paramview_ppu[26].value = nes_main.g_nes_mapper_control.g_chr_bank_map[1].ToString();
                g_paramview_ppu[27].value = nes_main.g_nes_mapper_control.g_chr_bank_map[2].ToString();
                g_paramview_ppu[28].value = nes_main.g_nes_mapper_control.g_chr_bank_map[3].ToString();
                g_paramview_ppu[29].value = nes_main.g_nes_mapper_control.g_chr_bank_map[4].ToString();
                g_paramview_ppu[30].value = nes_main.g_nes_mapper_control.g_chr_bank_map[5].ToString();
                g_paramview_ppu[31].value = nes_main.g_nes_mapper_control.g_chr_bank_map[6].ToString();
                g_paramview_ppu[32].value = nes_main.g_nes_mapper_control.g_chr_bank_map[7].ToString();

                g_paramview_apu[0].value = nes_main.g_nes_apu.g_wave_square1.c_duty.ToString();
                g_paramview_apu[1].value = nes_main.g_nes_apu.g_wave_square1.c_len_count_enable.ToString();
                g_paramview_apu[2].value = nes_main.g_nes_apu.g_wave_square1.c_envelope_enable.ToString();
                g_paramview_apu[3].value = nes_main.g_nes_apu.g_wave_square1.c_volume.ToString();
                g_paramview_apu[4].value = nes_main.g_nes_apu.g_wave_square1.c_sweep_enable.ToString();
                g_paramview_apu[5].value = nes_main.g_nes_apu.g_wave_square1.c_sweep_value.ToString();
                g_paramview_apu[6].value = nes_main.g_nes_apu.g_wave_square1.c_sweep_negate.ToString();
                g_paramview_apu[7].value = nes_main.g_nes_apu.g_wave_square1.c_sweep_shift.ToString();
                g_paramview_apu[8].value = nes_main.g_nes_apu.g_wave_square1.c_freq.ToString();
                g_paramview_apu[9].value = nes_main.g_nes_apu.g_wave_square1.c_len_count.ToString();
                g_paramview_apu[10].value = nes_main.g_nes_apu.g_wave_square2.c_duty.ToString();
                g_paramview_apu[11].value = nes_main.g_nes_apu.g_wave_square2.c_len_count_enable.ToString();
                g_paramview_apu[12].value = nes_main.g_nes_apu.g_wave_square2.c_envelope_enable.ToString();
                g_paramview_apu[13].value = nes_main.g_nes_apu.g_wave_square2.c_volume.ToString();
                g_paramview_apu[14].value = nes_main.g_nes_apu.g_wave_square2.c_sweep_enable.ToString();
                g_paramview_apu[15].value = nes_main.g_nes_apu.g_wave_square2.c_sweep_value.ToString();
                g_paramview_apu[16].value = nes_main.g_nes_apu.g_wave_square2.c_sweep_negate.ToString();
                g_paramview_apu[17].value = nes_main.g_nes_apu.g_wave_square2.c_sweep_shift.ToString();
                g_paramview_apu[18].value = nes_main.g_nes_apu.g_wave_square2.c_freq.ToString();
                g_paramview_apu[19].value = nes_main.g_nes_apu.g_wave_square2.c_len_count.ToString();
                g_paramview_apu[20].value = nes_main.g_nes_apu.g_wave_triangle.c_len_count_enable.ToString();
                g_paramview_apu[21].value = nes_main.g_nes_apu.g_wave_triangle.c_linear_value.ToString();
                g_paramview_apu[22].value = nes_main.g_nes_apu.g_wave_triangle.c_freq.ToString();
                g_paramview_apu[23].value = nes_main.g_nes_apu.g_wave_triangle.c_len_count.ToString();
                g_paramview_apu[24].value = nes_main.g_nes_apu.g_wave_noise.c_len_count_enable.ToString();
                g_paramview_apu[25].value = nes_main.g_nes_apu.g_wave_noise.c_envelope_enable.ToString();
                g_paramview_apu[26].value = nes_main.g_nes_apu.g_wave_noise.c_volume.ToString();
                g_paramview_apu[27].value = nes_main.g_nes_apu.g_wave_noise.c_freq.ToString();
                g_paramview_apu[28].value = nes_main.g_nes_apu.g_wave_noise.c_noisetype.ToString();
                g_paramview_apu[29].value = nes_main.g_nes_apu.g_wave_noise.c_len_count.ToString();
                g_paramview_apu[30].value = nes_main.g_nes_apu.g_wave_dmc.c_freq.ToString();
                g_paramview_apu[31].value = nes_main.g_nes_apu.g_wave_dmc.c_loop.ToString();
                g_paramview_apu[32].value = nes_main.g_nes_apu.g_wave_dmc.c_irq.ToString();
                g_paramview_apu[33].value = nes_main.g_nes_apu.g_wave_dmc.c_value.ToString();
                g_paramview_apu[34].value = nes_main.g_nes_apu.g_wave_dmc.c_address.ToString();
                g_paramview_apu[35].value = nes_main.g_nes_apu.g_wave_dmc.c_length.ToString();
                g_paramview_apu[36].value = nes_main.g_nes_apu.g_wave_square1.c_enable.ToString();
                g_paramview_apu[37].value = nes_main.g_nes_apu.g_wave_square1.c_freq_real.ToString();
                g_paramview_apu[38].value = nes_main.g_nes_apu.g_wave_square1.c_envelope_vol.ToString();
                g_paramview_apu[39].value = nes_main.g_nes_apu.g_wave_square1.c_envelope_count.ToString();
                g_paramview_apu[40].value = nes_main.g_nes_apu.g_wave_square1.c_sweep_freqlimit.ToString();
                g_paramview_apu[41].value = nes_main.g_nes_apu.g_wave_square1.c_sweep_count.ToString();
                g_paramview_apu[42].value = nes_main.g_nes_apu.g_wave_square1.c_counter.ToString();
                g_paramview_apu[43].value = nes_main.g_nes_apu.g_wave_square1.c_vol.ToString();
                g_paramview_apu[44].value = nes_main.g_nes_apu.g_wave_square1.c_duty_cnt.ToString();
                g_paramview_apu[45].value = nes_main.g_nes_apu.g_wave_square2.c_enable.ToString();
                g_paramview_apu[46].value = nes_main.g_nes_apu.g_wave_square2.c_freq_real.ToString();
                g_paramview_apu[47].value = nes_main.g_nes_apu.g_wave_square2.c_envelope_vol.ToString();
                g_paramview_apu[48].value = nes_main.g_nes_apu.g_wave_square2.c_envelope_count.ToString();
                g_paramview_apu[49].value = nes_main.g_nes_apu.g_wave_square2.c_sweep_freqlimit.ToString();
                g_paramview_apu[50].value = nes_main.g_nes_apu.g_wave_square2.c_sweep_count.ToString();
                g_paramview_apu[51].value = nes_main.g_nes_apu.g_wave_square2.c_counter.ToString();
                g_paramview_apu[52].value = nes_main.g_nes_apu.g_wave_square2.c_vol.ToString();
                g_paramview_apu[53].value = nes_main.g_nes_apu.g_wave_square2.c_duty_cnt.ToString();

                g_paramview_apu[54].value = nes_main.g_nes_apu.g_wave_triangle.c_enable.ToString();
                g_paramview_apu[55].value = nes_main.g_nes_apu.g_wave_triangle.c_freq_real.ToString();
                g_paramview_apu[56].value = nes_main.g_nes_apu.g_wave_triangle.c_linear_count_reset.ToString();
                g_paramview_apu[57].value = nes_main.g_nes_apu.g_wave_triangle.c_linear_count.ToString();
                g_paramview_apu[58].value = nes_main.g_nes_apu.g_wave_triangle.c_counter.ToString();

                g_paramview_apu[59].value = nes_main.g_nes_apu.g_wave_triangle.c_vol.ToString();
                g_paramview_apu[60].value = nes_main.g_nes_apu.g_wave_triangle.c_duty_cnt.ToString();
                g_paramview_apu[61].value = nes_main.g_nes_apu.g_wave_noise.c_enable.ToString();
                g_paramview_apu[62].value = nes_main.g_nes_apu.g_wave_noise.c_freq_real.ToString();
                g_paramview_apu[63].value = nes_main.g_nes_apu.g_wave_noise.c_envelope_vol.ToString();
                g_paramview_apu[64].value = nes_main.g_nes_apu.g_wave_noise.c_envelope_count.ToString();
                g_paramview_apu[65].value = nes_main.g_nes_apu.g_wave_noise.c_shift_reg.ToString();
                g_paramview_apu[66].value = nes_main.g_nes_apu.g_wave_noise.c_shift_bit0.ToString();
                g_paramview_apu[67].value = nes_main.g_nes_apu.g_wave_noise.c_counter.ToString();
                g_paramview_apu[68].value = nes_main.g_nes_apu.g_wave_noise.c_vol.ToString();
                g_paramview_apu[69].value = nes_main.g_nes_apu.g_wave_dmc.c_enable.ToString();
                g_paramview_apu[70].value = nes_main.g_nes_apu.g_wave_dmc.c_freq_real.ToString();
                g_paramview_apu[71].value = nes_main.g_nes_apu.g_wave_dmc.c_cur_address.ToString();
                g_paramview_apu[72].value = nes_main.g_nes_apu.g_wave_dmc.c_cur_count.ToString();
                g_paramview_apu[73].value = nes_main.g_nes_apu.g_wave_dmc.c_cur_byte.ToString();
                g_paramview_apu[74].value = nes_main.g_nes_apu.g_wave_dmc.c_counter.ToString();

                g_paramview_cartrig[0].value = (nes_main.g_nes_cartridge.g_prg_rom_size / 1024) + " KB";
                g_paramview_cartrig[1].value = (nes_main.g_nes_cartridge.g_chr_rom_size / 1024) + " KB"; ;
                g_paramview_cartrig[2].value = nes_main.g_nes_cartridge.g_mapper_num.ToString();
                g_paramview_cartrig[3].value = nes_main.g_nes_mapper_control.g_prg_bank_num.ToString();
                g_paramview_cartrig[4].value = nes_main.g_nes_mapper_control.g_chr_bank_num.ToString();

                for (int i = 0; i < nes_6502.STACK_SIZE; i++)
                {
                    g_paramview_call[i].stack = "";
                    g_paramview_call[i].val = "";
                    g_paramview_call[i].bank = "";
                }
                int w_offset = 0;
                for (int i = 0; i < nes_6502.STACK_SIZE; i++)
                {
                    switch (nes_main.g_nes_6502.g_stack[i].type)
                    {
                        case "JSR":
                        case "BRK":
                        case "NMI":
                        case "IRQ":
                            g_paramview_call[w_offset].stack = nes_main.g_nes_6502.g_stack[i].type;
                            g_paramview_call[w_offset].val = nes_main.g_nes_6502.g_stack[i].address.ToString("X4");
                            g_paramview_call[w_offset].bank = nes_main.g_nes_6502.g_stack[i].bank.ToString();
                            w_offset += 1;
                            break;
                    }
                }
                dataGridView_cpu.Invalidate();
                dataGridView_ppu.Invalidate();
                dataGridView_apu.Invalidate();
                dataGridView_cartrig.Invalidate();
                dataGridView_call_stack.Invalidate();
            }
        }
    }
}
