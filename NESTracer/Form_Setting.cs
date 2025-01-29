using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    public partial class Form_Setting : Form
    {
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_Setting()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        //----------------------------------------------------------------
        //Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void Form_Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void comboBox_videoformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            nes_main.g_tvmode_req = comboBox_videoformat.SelectedIndex;
            nes_main.write_setting();
        }
        private void comboBox_rendering_SelectedIndexChanged(object sender, EventArgs e)
        {
            nes_main.g_gpu_req = comboBox_rendering.SelectedIndex;
            nes_main.write_setting();
        }
        private void checkBox_ppu_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_ppu_enable = checkBox_ppu.Checked;
            show_window();
            nes_main.write_setting();
        }

        private void checkBox_apu_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_music_enable = checkBox_apu.Checked;
            show_window();
            nes_main.write_setting();
        }

        private void checkBox_io_CheckedChanged_1(object sender, EventArgs e)
        {
            nes_main.g_io_enable = checkBox_io.Checked;
            show_window();
            nes_main.write_setting();
        }

        private void checkBox_pattern_CheckedChanged_1(object sender, EventArgs e)
        {
            nes_main.g_pattern_enable = checkBox_pattern.Checked;
            show_window();
            nes_main.write_setting();
        }

        private void checkBox_code_CheckedChanged_1(object sender, EventArgs e)
        {
            nes_main.g_code_enable = checkBox_code.Checked;
            show_window();
            nes_main.write_setting();
        }

        private void checkBox_bank_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_code_bank_enable = checkBox_bank.Checked;
            show_window();
            nes_main.write_setting();
        }

        private void checkBox_register_CheckedChanged_1(object sender, EventArgs e)
        {
            nes_main.g_registry_enable = checkBox_register.Checked;
            show_window();
            nes_main.write_setting();
        }
        private void checkBox_fsb_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_trace_fsb = checkBox_fsb.Checked;
            nes_main.write_setting();
        }

        private void checkBox_sip_CheckedChanged(object sender, EventArgs e)
        {
            nes_main.g_trace_sip = checkBox_sip.Checked;
            nes_main.write_setting();
        }

        //----------------------------------------------------------------
        //sub function
        //----------------------------------------------------------------
        public void update()
        {
            checkBox_ppu.Checked = nes_main.g_ppu_enable;
            checkBox_apu.Checked = nes_main.g_music_enable;
            checkBox_io.Checked = nes_main.g_io_enable;
            checkBox_pattern.Checked = nes_main.g_pattern_enable;
            checkBox_code.Checked = nes_main.g_code_enable;
            checkBox_bank.Checked = nes_main.g_code_bank_enable;
            checkBox_register.Checked = nes_main.g_registry_enable;
            checkBox_fsb.Checked = nes_main.g_trace_fsb;
            checkBox_sip.Checked = nes_main.g_trace_sip;
//            comboBox_videoformat.SelectedIndex = nes_main.g_d_vdp.g_vdp_status_0_tvmode;
//            comboBox_rendering.SelectedIndex = (nes_main.g_md_vdp.rendering_gpu == false) ? 0 : 1;
            show_window();
        }
        public void show_window()
        {
            if (nes_main.g_music_enable == true) { nes_main.g_form_music.Show(); } else { nes_main.g_form_music.Hide(); }
            if (nes_main.g_code_bank_enable == true) { nes_main.g_form_code_bank.Show(); } else { nes_main.g_form_code_bank.Hide(); }
            if (nes_main.g_code_enable == true) { nes_main.g_form_code.Show(); } else { nes_main.g_form_code.Hide(); }
            if (nes_main.g_ppu_enable == true) { nes_main.g_form_ppu_screen.Show(); } else { nes_main.g_form_ppu_screen.Hide(); }
            if (nes_main.g_pattern_enable == true) { nes_main.g_form_pattern.Show(); } else { nes_main.g_form_pattern.Hide(); }
            if (nes_main.g_io_enable == true) { nes_main.g_form_io.Show(); } else { nes_main.g_form_io.Hide(); }
            if (nes_main.g_registry_enable == true) { nes_main.g_form_registry.Show(); } else { nes_main.g_form_registry.Hide(); }
        }

    }
}
