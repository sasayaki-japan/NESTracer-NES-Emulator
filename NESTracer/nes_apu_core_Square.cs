using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        private int[,] DUTY_LIST = {{0, 1, 0, 0, 0, 0, 0, 0},
                                    {0, 1, 1, 0, 0, 0, 0, 0},
                                    {0, 1, 1, 1, 1, 0, 0, 0},
                                    {1, 0, 0, 1, 1, 1, 1, 1}};
        public class Wave_Square
        {
            public bool c_enable = false;             
            public int c_mode = 0;                    
            public int c_duty = 0;                  
            public bool c_len_count_enable = false;        
            public bool c_envelope_enable = false;          
            public int c_volume = 0;                
            public bool c_sweep_enable = false;            
            public int c_sweep_value = 0;    　      
            public bool c_sweep_negate = false;            
            public int c_sweep_shift = 0;           
            public int c_freq = 0;                  
            public int c_len_count = 0;             
            public int c_freq_real = 0;             
            public int c_envelope_vol = 0;          
            public int c_sweep_freqlimit = 0;
            public int c_envelope_count = 0;
            public int c_envelope_count_init = 0;
            public int c_sweep_count = 0;           
            public int c_counter = 0;               
            public int c_vol = 0;                   
            public int c_duty_cnt = 0;              

            public void clock_120()
            {
                if ((c_enable == true) && (0 < c_len_count))
                {
                    if ((c_len_count_enable == false) && (0 < c_len_count))
                    {
                        c_len_count -= 1;
                    }
                    if ((c_sweep_enable == true) && (c_sweep_shift != 0))
                    {
                        if (0 < c_sweep_count)
                        {
                            c_sweep_count--;
                        }
                        if (c_sweep_count == 0)
                        {
                            c_sweep_count = c_sweep_value;
                            if (c_sweep_negate == true)
                            {
                                if (c_mode == 0)
                                    c_freq -= ((c_freq >> c_sweep_shift) + 1);
                                else
                                    c_freq -= (c_freq >> c_sweep_shift);
                            }
                            else
                            {
                                c_freq += (c_freq >> c_sweep_shift);
                            }
                        }
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
                        else
                        if (c_envelope_count == 0)
                        {
                            c_envelope_count = c_envelope_count_init;
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
                    if ((c_sweep_enable == true) && (c_sweep_negate == false) && (c_sweep_freqlimit < c_freq))
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
                    c_duty_cnt += 1;
                    c_duty_cnt &= 0x7;
                }
                else
                {
                    c_counter -= 1;
                }
            }
            public int clock_44100()
            {
                int w_out = 0;
                if ((c_vol > 0) && (c_freq >= 8))
                {
                    w_out = ((nes_main.g_nes_apu.DUTY_LIST[c_duty, c_duty_cnt] == 1) ? c_vol : 0);
                    nes_main.g_nes_apu.g_freq_out[c_mode] = c_freq_real;
                }
                else
                {
                    nes_main.g_nes_apu.g_freq_out[c_mode] = 0;
                }
                return w_out;
            }
        }
    }
}
