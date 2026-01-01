using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        private short[] NOISE_CYCLES = { 4, 8, 16, 32, 64, 96, 128, 160, 202, 254, 380, 508, 762, 1016, 2034, 4068 };
        public class Wave_Noise
        {
            public bool c_enable = false;             
            public bool c_len_count_enable = false;        
            public bool c_envelope_enable =false;          
            public int c_volume = 0;                
            public int c_freq = 0;                  
            public int c_noisetype = 0;                          
            public int c_len_count = 0;             
            public int c_freq_real = 0;             
            public int c_envelope_vol = 0;          
            public int c_envelope_count = 0;        
            public int c_shift_reg = 1;             
            public int c_shift_bit0 = 1;            
            public int c_counter = 0;               
            public int c_vol = 0;                   
            public void clock_120()
            {
                if ((c_enable == true) && (0 < c_len_count))
                {
                    if ((c_len_count_enable == false) && (0 < c_len_count))
                    {
                        c_len_count -= 1;
                    }
                }
            }
            public void clock_240()
            {
                c_vol = 0;
                if ((c_enable == true) && (0 < c_len_count))
                {
                    if (c_envelope_enable == false)
                    {
                        if (0 < c_envelope_count)
                        {
                            c_envelope_count -= 1;
                        }
                        if (c_envelope_count == 0)
                        {
                            c_envelope_count = c_volume + 1;
                            if (c_len_count_enable == true)
                            {
                                c_envelope_vol = (c_envelope_vol - 1) & 0x0f;
                            }
                            else
                            if (0 < c_envelope_vol)
                            {
                                c_envelope_vol -= 1;
                            }
                        }
                        c_vol = c_envelope_vol;
                    }
                    else
                    {
                        c_vol = c_volume;
                    }
                    if (c_len_count <= 0)
                    {
                        c_vol = 0;
                    }
                }
            }
            public void clock_apu()
            {
                if (c_counter <= 0)
                {
                    c_counter = c_freq;
                    int w_bit1 = c_shift_reg & 1;
                    int w_bit2 = 0;
                    if (c_noisetype == 0) w_bit2 = (c_shift_reg >> 1) & 1;
                    else w_bit2 = (c_shift_reg >> 6) & 1;
                    c_shift_reg >>= 1;
                    c_shift_reg = (ushort)((c_shift_reg | ((w_bit1 ^ w_bit2) << 14)));
                }
                else
                {
                    c_counter -= 1;
                }
            }
            public int clock_44100()
            {
                int w_out = 0;
                if (c_vol > 0)
                {
                    w_out = ((c_shift_reg & 1) == 0) ? c_vol: 0;
                    nes_main.g_nes_apu.g_freq_out[3] = c_freq_real;
                }
                else
                {
                    nes_main.g_nes_apu.g_freq_out[3] = 0;
                }
                return w_out;
            }
        }
    }
}
