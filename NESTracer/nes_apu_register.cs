using NAudio.Wave;
using System.Diagnostics.Metrics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        public byte read1(int in_address)
        {
            byte w_out = 0;
            switch (in_address)
            {
                case 0x4015:
                    g_apu_reg[0x15] &= 0xc0;
                    if (g_wave_square1.c_len_count > 0) w_out |= 0x01;
                    if (g_wave_square2.c_len_count > 0) w_out |= 0x02;
                    if (g_wave_triangle.c_len_count > 0) w_out |= 0x04;
                    if (g_wave_noise.c_len_count > 0) w_out |= 0x08;
                    if (g_wave_dpcm.c_enable == true) w_out |= 0x10;
                    w_out = g_apu_reg[0x15];
                    g_apu_reg[0x15] &= 0xbf;
                    break;
                default:
                    w_out = 0x40;
                    break;
            }
            return w_out;
        }

        public void write1(int in_address, byte in_val)
        {
            if(in_address == 0x4015)
            {
                g_apu_reg[0x15] &= 0xc0;
                in_val &= 0x1f;
                g_apu_reg[0x15] |= in_val;
            }
            else
            {
                g_apu_reg[in_address & 0x00ff] = in_val;
            }
            switch (in_address)
            {
                case 0x4000:
                    g_wave_square1.c_duty = in_val >> 6;
                    g_wave_square1.c_len_count_enable = ((in_val & 0x20) == 0) ? false : true;
                    g_wave_square1.c_envelope_enable = ((in_val & 0x10) == 0) ? false : true;
                    g_wave_square1.c_volume = (short)(in_val & 0x0f);
                    g_wave_square1.c_envelope_count_init = g_wave_square1.c_volume + 1;
                    break;
                case 0x4001:
                    g_wave_square1.c_sweep_enable = ((in_val & 0x80) == 0) ? false : true;
                    g_wave_square1.c_sweep_value = ((in_val & 0x70) >> 4) + 1;
                    g_wave_square1.c_sweep_negate = ((in_val & 0x08) == 0) ? false : true;
                    g_wave_square1.c_sweep_shift = in_val & 0x07;
                    if(g_wave_square1.c_sweep_negate == false)
                    {
                        g_wave_square1.c_sweep_freqlimit = SWEEP_LIMIT[g_wave_square1.c_sweep_shift];
                    }
                    break;
                case 0x4002:
                    g_wave_square1.c_freq = in_val | ((g_apu_reg[3] & 0x07) << 8);
                    g_wave_square1.c_freq_real = (int)(CPU_CLOCK / ((g_wave_square1.c_freq + 1) << 4));
                    break;
                case 0x4003:
                    g_wave_square1.c_freq = g_apu_reg[2] | ((in_val & 0x07) << 8);
                    g_wave_square1.c_freq_real = (int)(CPU_CLOCK / ((g_wave_square1.c_freq + 1) << 4));
                    g_wave_square1.c_len_count = KEYOFF[in_val >> 3] * 2;
                    g_wave_square1.c_counter = g_wave_square1.c_volume + 1;
                    g_wave_square1.c_envelope_vol = 15;
                    g_wave_square1.c_duty_cnt = 0;
                    g_wave_square1.c_envelope_count = g_wave_square1.c_envelope_count_init + 1;
                    break;
                case 0x4004:
                    g_wave_square2.c_duty = in_val >> 6;
                    g_wave_square2.c_len_count_enable = ((in_val & 0x20) == 0) ? false : true;
                    g_wave_square2.c_envelope_enable = ((in_val & 0x10) == 0) ? false : true;
                    g_wave_square2.c_volume = (short)(in_val & 0x0f);
                    break;
                case 0x4005:
                    g_wave_square2.c_sweep_enable = ((in_val & 0x80) == 0) ? false : true;
                    g_wave_square2.c_sweep_value = ((in_val & 0x70) >> 4) + 1;
                    g_wave_square2.c_sweep_negate = ((in_val & 0x08) == 0) ? false : true;
                    g_wave_square2.c_sweep_shift = in_val & 0x07;
                    if (g_wave_square2.c_sweep_negate == false)
                    {
                        g_wave_square2.c_sweep_freqlimit = SWEEP_LIMIT[g_wave_square2.c_sweep_shift];
                    }
                    break;
                case 0x4006:
                    g_wave_square2.c_freq = in_val | ((g_apu_reg[7] & 0x07) << 8);
                    g_wave_square2.c_freq_real = (int)(CPU_CLOCK / ((g_wave_square2.c_freq + 1) << 4));
                    break;
                case 0x4007:
                    g_wave_square2.c_freq = g_apu_reg[6] | ((in_val & 0x07) << 8);
                    g_wave_square2.c_freq_real = (int)(CPU_CLOCK / ((g_wave_square2.c_freq + 1) << 4));
                    g_wave_square2.c_len_count = KEYOFF[in_val >> 3] * 2;
                    g_wave_square2.c_counter = g_wave_square1.c_volume + 1;
                    g_wave_square2.c_envelope_vol = 15;
                    g_wave_square1.c_duty_cnt = 0;
                    break;
                case 0x4008:
                    g_wave_triangle.c_len_count_enable = in_val >> 7;
                    g_wave_triangle.c_linear_value = in_val & 0x7f;
                    break;
                case 0x400a:
                    g_wave_triangle.c_freq = in_val | ((g_apu_reg[0x0b] & 0x07) << 8);
                    g_wave_triangle.c_freq_real = (int)(CPU_CLOCK / ((g_wave_triangle.c_freq + 1) << 5));
                    g_wave_triangle.c_counter = 0;
                    break;
                case 0x400b:
                    g_wave_triangle.c_freq = g_apu_reg[0x0a] | ((in_val & 0x07) << 8);
                    g_wave_triangle.c_freq_real = (int)(CPU_CLOCK / ((g_wave_triangle.c_freq + 1) << 5));
                    g_wave_triangle.c_len_count = KEYOFF[in_val >> 3] * 2;
                    g_wave_triangle.c_linear_count_reset = true;
                    g_wave_triangle.c_linear_count = 0;
                    g_wave_triangle.c_counter = 0;
                    break;
                case 0x400c:
                    g_wave_noise.c_len_count_enable = ((in_val & 0x20) == 0) ? false : true;
                    g_wave_noise.c_envelope_enable = ((in_val & 0x10) == 0) ? false : true;
                    g_wave_noise.c_volume = (short)(in_val & 0x0f);
                    break;
                case 0x400e:
                    g_wave_noise.c_freq = NOISE_CYCLES[in_val & 0x0f];
                    g_wave_noise.c_freq_real = (int)(CPU_CLOCK / (g_wave_noise.c_freq + 1));
                    g_wave_noise.c_noisetype = in_val >> 7;
                    break;
                case 0x400f:
                    g_wave_noise.c_len_count = KEYOFF[in_val >> 3] * 2;
                    g_wave_noise.c_envelope_vol = 15;
                    //g_wave_noise.c_shift_reg = 1;
                    break;
                case 0x4010:
                    g_wave_dpcm.c_freq = DMC_CYCLES[in_val & 0x0f];
                    g_wave_dpcm.c_loop = (in_val & 0x40) >> 6;
                    g_wave_dpcm.c_irq = in_val >> 7;
                    if (g_wave_dpcm.c_irq == 0)
                    {
                        g_apu_reg[0x15] &= 0x7f;     
                    }
                    break;
                case 0x4011:
                    g_wave_dpcm.c_value = in_val & 0x7f;
                    break;
                case 0x4012:
                    g_wave_dpcm.c_address = (ushort)(0xc000 + (ushort)(in_val << 6));
                    break;
                case 0x4013:
                    g_wave_dpcm.c_length = in_val << 7;
                    break;
                case 0x4015:
                    if ((in_val & 0x01) == 0)
                    {
                        g_wave_square1.c_enable = false;
                    }
                    else
                    {
                        g_wave_square1.c_enable = true;
                    }
                    if ((in_val & 0x02) == 0)
                    {
                        g_wave_square2.c_enable = false;
                    }
                    else
                    {
                        g_wave_square2.c_enable = true;
                    }
                    if ((in_val & 0x04) == 0)
                    {
                        g_wave_triangle.c_enable = false;
                        g_wave_triangle.c_linear_count = 0;
                        g_wave_triangle.c_linear_count_reset = false;
                    }
                    else
                    {
                        g_wave_triangle.c_enable = true;
                    }
                    if ((in_val & 0x08) == 0)
                    {
                        g_wave_noise.c_enable = false;
                        g_wave_noise.c_len_count = 0;
                    }
                    else
                    {
                        g_wave_noise.c_enable = true;
                    }
                    if ((in_val & 0x10) == 0)
                    {
                        g_wave_dpcm.c_enable = false;
                    }
                    else
                    {
                        g_wave_dpcm.c_enable = true;
                        g_wave_dpcm.c_freq_real = g_wave_dpcm.c_freq;
                        g_wave_dpcm.c_cur_address = g_wave_dpcm.c_address;
                        g_wave_dpcm.c_cur_count = g_wave_dpcm.c_length;

                    }
                    g_wave_dpcm.c_irq = 0;
                    g_apu_reg[0x15] &= 0x7f;     
                    break;
                case 0x4017:
                    g_4017_6_frame_Interrupt = (in_val & 0x40) >> 6;

                    if (g_4017_6_frame_Interrupt == 1)
                    {
                        g_apu_reg[0x15] &= 0xbf;     
                    }
                    break;
            }
        }
    }
}
