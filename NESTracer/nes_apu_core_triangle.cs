using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        public class Wave_Triangle
        {
            public bool c_enable = false;             
            public int c_len_count_enable = 0;        
            public int c_linear_value = 0;          
            public int c_freq = 0;                  
            public int c_len_count = 0;             
            public int c_freq_real = 0;             
            public bool c_linear_count_reset = false;    
            public int c_linear_count = 0;          
            public int c_counter = 0;               
            public int c_vol = 0;                   
            public int c_duty_cnt = 0;              

            public Wave_Triangle()
            {
            }
            public void clock_120()
            {
                c_vol = 0;
                if (c_enable == true)
                {
                    if ((c_len_count_enable == 0) && (0 < c_len_count))
                    {
                        c_len_count -= 1;
                    }
                }
            }
            public void clock_240()
            {
                c_vol = 0;
                if (c_enable == true)
                {
                    if (c_linear_count_reset == true)
                    {
                        c_linear_count = c_linear_value;
                    }
                    else if (c_linear_count != 0)
                    {
                        c_linear_count--;
                    }
                    if ((c_len_count_enable == 0) && (c_linear_count != 0))
                    {
                        c_linear_count_reset = false;
                    }

                    if ((0 < c_len_count) && (0 < c_linear_count))
                    {
                        c_vol = 1;
                    }
                }
            }

            public void clock_cpu()
            {
                if (c_counter <= 0)
                {
                    c_counter = c_freq;
                    if (c_vol == 1)
                    {
                        c_duty_cnt += 1;
                        c_duty_cnt &= 0x1f;
                    }
                }
                else
                {
                    c_counter -= 1;
                }
            }
            public short clock_44100()
            {
                short w_out = 0;
                if (c_vol == 1)
                {
                    w_out = nes_main.g_nes_apu.TRAIANGLE_LOOKUP[c_duty_cnt];
                    nes_main.g_nes_apu.g_freq_out[2] = c_freq_real;
                }
                else
                {
                    nes_main.g_nes_apu.g_freq_out[2] = 0;
                }

                return w_out;
            }
        }
    }
}
