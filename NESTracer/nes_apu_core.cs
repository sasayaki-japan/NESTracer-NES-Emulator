using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        private int[] LOOP_SYNC_LIST = { 183 * 2, 184 * 2, 184 * 2, 184 * 2 };
        private int[,] DUTY_LIST = {{0, 1, 0, 0, 0, 0, 0, 0},
                                    {0, 1, 1, 0, 0, 0, 0, 0},
                                    {0, 1, 1, 1, 1, 0, 0, 0},
                                    {1, 0, 0, 1, 1, 1, 1, 1}};
        private byte[] KEYOFF = {   0x05, 0x7f, 0x0a, 0x01
                                        , 0x14, 0x02, 0x28, 0x03
                                        , 0x50, 0x04, 0x1e, 0x05
                                        , 0x07, 0x06, 0x0d, 0x07
                                        , 0x06, 0x08, 0x0c, 0x09
                                        , 0x18, 0x0a, 0x30, 0x0b
                                        , 0x60, 0x0c, 0x24, 0x0d
                                        , 0x08, 0x0e, 0x10, 0x0f };
        private short[] SWEEP_LIMIT = { 0x03ff, 0x0555, 0x0666, 0x071c, 0x0787, 0x07c1, 0x07e0, 0x07f0 };
        private short[] TRAIANGLE_LOOKUP = { 0x7000,  0x6000,  0x5000,  0x4000
                                                ,  0x3000,  0x2000,  0x1000,  0x0000
                                                , -0x1000, -0x2000, -0x3000, -0x4000
                                                , -0x5000, -0x6000, -0x7000, -0x8000
                                                , -0x8000, -0x7000, -0x6000, -0x5000
                                                , -0x4000, -0x3000, -0x2000, -0x1000
                                                ,  0x0000,  0x1000,  0x2000,  0x3000
                                                ,  0x4000,  0x5000,  0x6000,  0x7000 };
        private short[] NOISE_CYCLES = { 4, 8, 16, 32, 64, 96, 128, 160, 202, 254, 380, 508, 762, 1016, 2034, 4068 };
        private short[] DMC_CYCLES = { 428, 380, 340, 320, 286, 254, 226, 214, 190, 160, 142, 128, 106,  85,  72,  54 };

        private int g_clock_cnt;
        private int g_cycle_bk = -1;
        private byte[] g_buffer;
        private int g_buffer_cur = 0;
        private float g_clock_44100 = 0;

        public void run(int in_clock)
        {
            for (int i = 0; i < in_clock; i++)
            {
                int w_sycle = (g_clock_cnt / 7445) % 4;
                if (w_sycle != g_cycle_bk)
                {
                    g_cycle_bk = w_sycle;
                    g_wave_square1.clock_240(w_sycle);
                    g_wave_square2.clock_240(w_sycle);
                    g_wave_triangle.clock_240(w_sycle);
                    g_wave_noise.clock_240(w_sycle);
                    g_wave_dmc.clock_240(w_sycle);

                    if ((w_sycle == 3) && (g_4017_6_frame_Interrupt == 0))
                    {
                        g_apu_reg[0x15] |= 0x40;     
                    }
                }
                g_clock_cnt += 1;
                g_clock_cnt %= (7445 * 4);

                if ((g_clock_cnt & 1) == 1)
                {
                    g_wave_square1.clock_apu();
                    g_wave_square2.clock_apu();
                    g_wave_noise.clock_apu();
                }
                g_wave_triangle.clock_cpu();
                g_wave_dmc.clock_apu();

                if (40.584f <= g_clock_44100)
                {
                    g_clock_44100 -= 40.584f;
                    float w_mix1 = g_wave_square1.clock_44100() * g_out_vol[1];
                    float w_mix2 = g_wave_square2.clock_44100() * g_out_vol[2];
                    float w_mix3 = g_wave_triangle.clock_44100() * g_out_vol[3];
                    float w_mix4 = g_wave_noise.clock_44100() * g_out_vol[4];
                    float w_mix5 = g_wave_dmc.clock_44100() * g_out_vol[5];

                    if(g_master_stereo==false)
                    {
                        float w_mix_total = w_mix1 + w_mix2 + w_mix3 + w_mix4 + w_mix5;
                        w_mix_total = (short)Math.Max((short)-32768, (short)Math.Min((short)32767, w_mix_total));
                        g_buffer[g_buffer_cur + 1] = (byte)((short)w_mix_total >> 8);    
                        g_buffer[g_buffer_cur + 0] = (byte)((short)w_mix_total & 0xff);
                        g_buffer[g_buffer_cur + 3] = (byte)((short)w_mix_total >> 8);
                        g_buffer[g_buffer_cur + 2] = (byte)((short)w_mix_total & 0xff);
                    }
                    else
                    {
                        float w_mix_left = (float)(w_mix1 + w_mix2 * 0.5 + w_mix3 + w_mix4 * 0.8 + w_mix5);
                        float w_mix_right = (float)(w_mix1 * 0.5 + w_mix2 + w_mix3 * 0.8 + w_mix4 + w_mix5);
                        w_mix_left = (short)Math.Max((short)-32768, (short)Math.Min((short)32767, w_mix_left));
                        w_mix_right = (short)Math.Max((short)-32768, (short)Math.Min((short)32767, w_mix_right));
                        g_buffer[g_buffer_cur + 1] = (byte)((short)w_mix_right >> 8);    
                        g_buffer[g_buffer_cur + 0] = (byte)((short)w_mix_right & 0xff);
                        g_buffer[g_buffer_cur + 3] = (byte)((short)w_mix_left >> 8);
                        g_buffer[g_buffer_cur + 2] = (byte)((short)w_mix_left & 0xff);
                    }

                    g_buffer_cur += 4;
                    if (BUFSIZE <= g_buffer_cur)
                    {
                        g_bufferedwaveprovider.AddSamples(g_buffer, 0, BUFSIZE);
                        g_buffer_cur = 0;
                    }
                }
                g_clock_44100 += 1;
            }
        }
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
            public Wave_Square()
            {
            }
            public void clock_240(int in_cycle)
            {
                c_vol = 0;
                if ((c_enable == false) || (c_len_count <= 0)) return;
                if (in_cycle % 2 == 1)
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
            public short clock_44100()
            {
                short w_out = 0;
                if ((c_vol > 0) && (c_freq >= 8))
                {
                    int w_vol = (short)(c_vol << 11);
                    w_out = (short)((nes_main.g_nes_apu.DUTY_LIST[c_duty, c_duty_cnt] == 1) ? w_vol : -w_vol);
                    nes_main.g_nes_apu.g_freq_out[c_mode] = c_freq_real;
                }
                else
                {
                    nes_main.g_nes_apu.g_freq_out[c_mode] = 0;
                }
                return w_out;
            }
        }
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
            public void clock_240(int in_cycle)
            {
                c_vol = 0;
                if (c_enable == true)
                {
                    if (in_cycle % 2 == 1)
                    {
                        if ((c_len_count_enable == 0) && (0 < c_len_count))
                        {
                            c_len_count -= 1;
                        }
                    }
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

            public Wave_Noise()
            {
            }
            public void clock_240(int in_cycle)
            {
                c_vol = 0;
                if ((c_enable == false) || (c_len_count <= 0)) return;
                if (in_cycle % 2 == 0)
                {
                    if ((c_len_count_enable == false) && (0 < c_len_count))
                    {
                        c_len_count -= 1;
                    }
                }
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
            public short clock_44100()
            {
                short w_out = 0;
                if (c_vol > 0)
                {
                    int w_vol = c_vol;
                    if ((c_shift_reg & 1) == 1)
                    {
                        w_vol = (short)-w_vol;
                    }
                    w_out = (short)(w_vol << 11);
                    nes_main.g_nes_apu.g_freq_out[3] = c_freq_real;
                }
                else
                {
                    nes_main.g_nes_apu.g_freq_out[3] = 0;
                }
                return w_out;
            }
        }
        public class Wave_Dmc
        {
            public bool c_enable = true;              
            public int c_freq = 0;                  
            public int c_loop = 0;                  
            public int c_irq = 0;                   
            public int c_value = 0;                 
            public ushort c_address = 0;            
            public int c_length = 0;                
            public int c_freq_real = 0;             
            public ushort c_cur_address = 0;        
            public int c_cur_count = 0;             
            public byte c_cur_byte = 0;             
            public int c_counter = 0;               
            public Wave_Dmc()
            {
            }
            public void clock_240(int in_cycle)
            {
            }
            public void clock_apu()
            {
                if (c_counter <= 0)
                {
                    c_counter = c_freq;
                    if ((c_enable == true)&&(c_cur_count > 0))
                    {
                        if ((c_cur_count & 0x07) == 0)
                        {
                            c_cur_byte = nes_main.g_nes_bus.read1(c_cur_address);
                            if (c_cur_address == 0xffff)
                            {
                                c_cur_address = 0x8000;
                            }
                            else
                            {
                                c_cur_address += 1;
                            }
                        }
                        c_cur_count -= 1;
                        if (c_cur_count == 0)
                        {
                            if (c_loop == 1)
                            {
                                c_cur_address = c_address;
                                c_cur_count = c_length;
                            }
                            else
                            {
                                c_value = 0;
                                if (c_irq == 1)
                                {
                                    nes_main.g_nes_apu.g_apu_reg[0x15] |= 0x80;
                                }
                            }
                        }
                        if (c_cur_count > 0)
                        {
                            if ((c_cur_byte & 1) == 1)
                            {
                                if (c_value < 127) c_value += 2;
                            }
                            else
                            {
                                if (0 < c_value) c_value -= 2;
                            }
                            c_cur_byte >>= 1;
                        }
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
                if ((c_enable == true)&& (c_cur_count > 0))
                {
                    w_out = (short)((c_value - 64) << 9);
                    nes_main.g_nes_apu.g_freq_out[4] = c_freq_real;
                }
                else
                {
                    nes_main.g_nes_apu.g_freq_out[4] = 0;
                }
                return w_out;
            }
        }
    }
}
