using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    public partial class Form_IO_Setting : Form
    {
        public int g_mode;
        public int g_result;
        public Form_IO_Setting()
        {
            g_result = -1;
            InitializeComponent();
        }

        private void Form_IO_Setting_Shown(object sender, EventArgs e)
        {
            do
            {
                int w_ret = nes_main.g_nes_io.read_device_keyboard();
                if (w_ret == -1)
                {
                    break;
                }
            } while (true);
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int w_ret = -1;
            do
            {
                if (g_mode == 0)
                {
                    w_ret = nes_main.g_nes_io.read_device_joystick();
                    if (w_ret != -1)
                    {
                        break;
                    }
                }
                if (g_mode == 1)
                {
                    w_ret = nes_main.g_nes_io.read_device_keyboard();
                    if (w_ret != -1)
                    {
                        break;
                    }
                }
                Thread.Sleep(10);
            } while (true);
            g_result = w_ret;
            Invoke(new Action(() =>
            {
                Close();
            }));
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            g_result = -2;
            Close();
        }
    }

}
