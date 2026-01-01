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
        private float g_clock_cnt_2 = 0;
        private float g_clock_cnt_120 = 0;
        private float g_clock_cnt_240 = 0;
        private float g_clock_cnt_44100 = 0;
        private int g_clock_sycle = 0;
        public void run(float in_clock)
        {
            for (int i = 0; i < (int)in_clock; i++)
            {
                g_clock_cnt_2 = (g_clock_cnt_2 + 1) % 2;
                if (g_clock_cnt_2 == 0)
                {
                    g_wave_square1.clock_apu();
                    g_wave_square2.clock_apu();
                    g_wave_noise.clock_apu();
                }
                g_wave_triangle.clock_cpu();
                g_wave_dpcm.clock_apu();

                g_clock_cnt_120 = (g_clock_cnt_120 + 1) % 14914;
                if (g_clock_cnt_120 == 0)
                {
                    g_wave_square1.clock_120();
                    g_wave_square2.clock_120();
                    g_wave_triangle.clock_120();
                    g_wave_noise.clock_120();
                }
                g_clock_cnt_240 = (g_clock_cnt_240 + 1) % 7457;
                if (g_clock_cnt_240 == 0)
                {
                    g_wave_square1.clock_240();
                    g_wave_square2.clock_240();
                    g_wave_triangle.clock_240();
                    g_wave_noise.clock_240();

                    g_clock_sycle = (g_clock_sycle + 1) % 4;
                    if ((g_clock_sycle == 0) && (g_4017_6_frame_Interrupt == 0))
                    {
                        g_apu_reg[0x15] |= 0x40;
                        if (nes_main.g_nes_6502.g_flag_I == false)
                        {
                            nes_main.g_nes_6502.interrupt_IRQ = true;
                        }
                    }
                }
                g_clock_cnt_44100 = (g_clock_cnt_44100 + 1) % 40;
                if (g_clock_cnt_44100 == 0)
                {
                    float w_mix1 = g_wave_square1.clock_44100() * g_out_vol[1];
                    float w_mix2 = g_wave_square2.clock_44100() * g_out_vol[2];
                    float w_mix3 = g_wave_triangle.clock_44100() * g_out_vol[3];
                    float w_mix4 = g_wave_noise.clock_44100() * g_out_vol[4];
                    float w_mix5 = g_wave_dpcm.clock_44100() * g_out_vol[5];

                    if (g_master_stereo == false)
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
            }
        }
    }
}
