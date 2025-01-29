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
        private WaveOut g_waveOut;
        public Wave_Square g_wave_square1;
        public Wave_Square g_wave_square2;
        public Wave_Triangle g_wave_triangle;
        public Wave_Noise g_wave_noise;
        public Wave_Dmc g_wave_dmc;

        //----------------------------------------------------------------
        public nes_apu()
        {
            g_apu_reg = new byte[0x18];
            g_master_chk = new bool[6];
            g_master_vol = new int[6];
            g_out_vol = new float[6];
            g_freq_out = new int[5];

            g_bufferedwaveprovider = new BufferedWaveProvider(new WaveFormat(SAMPLING, BIT, CHANNELS));
            g_waveOut = new WaveOut();
            g_waveOut.Init(g_bufferedwaveprovider);
            g_waveOut.Play();

            g_wave_square1 = new Wave_Square();
            g_wave_square1.c_mode = 0;
            g_wave_square2 = new Wave_Square();
            g_wave_square2.c_mode = 1;
            g_wave_triangle = new Wave_Triangle();
            g_wave_noise = new Wave_Noise();
            g_wave_dmc = new Wave_Dmc();

            g_buffer = new byte[BUFSIZE];
        }
        public void setting()
        {
            g_out_vol[1] = 0;
            g_out_vol[2] = 0;
            g_out_vol[3] = 0;
            g_out_vol[4] = 0;
            g_out_vol[5] = 0;
            if (g_master_chk[0] == true)
            {
                float w_master = g_master_vol[0] / 100.0f;
                if (g_master_chk[1] == true) g_out_vol[1] = 0.12f * (g_master_vol[1] / 100.0f) / w_master;
                if (g_master_chk[2] == true) g_out_vol[2] = 0.12f * (g_master_vol[2] / 100.0f) / w_master;
                if (g_master_chk[3] == true) g_out_vol[3] = 0.14f * (g_master_vol[3] / 100.0f) / w_master;
                if (g_master_chk[4] == true) g_out_vol[4] = 0.08f * (g_master_vol[4] / 100.0f) / w_master;
                if (g_master_chk[5] == true) g_out_vol[5] = 0.43f * (g_master_vol[5] / 100.0f) / w_master;
            }
            nes_main.write_setting();
        }
    }
}
