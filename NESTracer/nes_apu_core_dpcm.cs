using System.Diagnostics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        public class Wave_Dpcm
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
