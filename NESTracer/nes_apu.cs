using NAudio.Wave;
using System.Diagnostics.Metrics;

namespace NESTracer
{
    internal partial class nes_apu
    {
        public const float CPU_CLOCK = 1789773.0f;
        public const int SAMPLING = 44100;
        public const int BIT = 16;
        public const int CHANNELS = 2;
        public const int BUFSIZE = 1024;

        public byte[] g_apu_reg;
        public int[] g_freq_out;

        public bool[] g_master_chk;
        public int[] g_master_vol;
        public float[] g_out_vol;
        public bool g_master_stereo;

        private int g_4017_6_frame_Interrupt;

        private BufferedWaveProvider g_bufferedwaveprovider;
        private WaveOutEvent g_waveOut;
        private byte[] g_buffer;
        private int g_buffer_cur = 0;
        public Wave_Square g_wave_square1;
        public Wave_Square g_wave_square2;
        public Wave_Triangle g_wave_triangle;
        public Wave_Noise g_wave_noise;
        public Wave_Dpcm g_wave_dpcm;

        //----------------------------------------------------------------
        public nes_apu()
        {
            g_apu_reg = new byte[0x18];
            g_master_chk = new bool[6];
            g_master_vol = new int[6];
            g_out_vol = new float[6];
            g_freq_out = new int[5];

            g_bufferedwaveprovider = new BufferedWaveProvider(new WaveFormat(SAMPLING, BIT, CHANNELS));
            g_bufferedwaveprovider.BufferDuration = TimeSpan.FromMilliseconds(200);
            g_bufferedwaveprovider.DiscardOnBufferOverflow = true;
            g_waveOut = new WaveOutEvent();
            g_waveOut.DesiredLatency = 100;
            g_waveOut.Init(g_bufferedwaveprovider);
            g_buffer = new byte[BUFSIZE];

            g_wave_square1 = new Wave_Square();
            g_wave_square1.c_mode = 0;
            g_wave_square2 = new Wave_Square();
            g_wave_square2.c_mode = 1;
            g_wave_triangle = new Wave_Triangle();
            g_wave_noise = new Wave_Noise();
            g_wave_dpcm = new Wave_Dpcm();
            g_waveOut.Play();
        }
        public void setting(bool in_write_setting = true)
        {
            g_out_vol[1] = 0;
            g_out_vol[2] = 0;
            g_out_vol[3] = 0;
            g_out_vol[4] = 0;
            g_out_vol[5] = 0;
            if (g_master_chk[0] == true)
            {
                float w_master = Math.Clamp(g_master_vol[0], 0, 100) / 100.0f;
                if (g_master_chk[1] == true) g_out_vol[1] = (Math.Clamp(g_master_vol[1], 0, 100) / 100.0f) * w_master;
                if (g_master_chk[2] == true) g_out_vol[2] = (Math.Clamp(g_master_vol[2], 0, 100) / 100.0f) * w_master;
                if (g_master_chk[3] == true) g_out_vol[3] = (Math.Clamp(g_master_vol[3], 0, 100) / 100.0f) * w_master;
                if (g_master_chk[4] == true) g_out_vol[4] = (Math.Clamp(g_master_vol[4], 0, 100) / 100.0f) * w_master;
                if (g_master_chk[5] == true) g_out_vol[5] = (Math.Clamp(g_master_vol[5], 0, 100) / 100.0f) * w_master;
            }
            if (in_write_setting == true)
            {
                nes_main.write_setting();
            }
        }
    }
}
