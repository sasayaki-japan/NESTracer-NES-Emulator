using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        private int[] TRAIANGLE_LOOKUP = { 15,  14,  13,  12,  11,  10,  9,  8, 7, 6, 5, 4, 3, 2, 1, 0
                                          , 0, 1, 2, 3, 4, 5, 6, 7,  8,  9,  10,  11,  12,  13,  14,  15 }; 
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
            public bool c_vol = false;                   
            public int c_duty_cnt = 0;              
            public void clock_120()
            {
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
                c_vol = false;
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
                        c_vol = true;
                    }
                }
            }

            public void clock_cpu()
            {
                if (0 < c_counter)
                {
                    c_counter -= 1;
                }
                else
                {
                    c_counter = c_freq;
                    c_duty_cnt += 1;
                    c_duty_cnt &= 0x1f;
                }
            }
            public int clock_44100()
            {
                int w_out = 0;
                if (c_vol == true)
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
