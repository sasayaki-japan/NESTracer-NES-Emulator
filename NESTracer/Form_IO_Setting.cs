using System.ComponentModel;

namespace NESTracer
{
    public partial class Form_IO_Setting : Form
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        public int g_mode;
        public int g_result;
        private volatile bool g_closingByUser;
        public Form_IO_Setting()
        {
            g_result = -1;
            InitializeComponent();
            backgroundWorker1.WorkerSupportsCancellation = true;
            FormClosing += Form_IO_Setting_FormClosing;
        }

        private void Form_IO_Setting_Shown(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }

            this.ActiveControl = null;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int w_ret = -1;
            WaitForInputRelease();
            do
            {
                if (backgroundWorker1.CancellationPending == true || g_closingByUser == true)
                {
                    return;
                }
                if(g_mode == 0)
                {
                    w_ret = nes_main.g_nes_io.read_device_joystick_for_setting();
                    if (w_ret != -1)
                    {
                        break;
                    }
                }
                if (g_mode == 1)
                {
                    w_ret = nes_main.g_nes_io.read_device_keyboard_for_setting();
                    if (w_ret != -1)
                    {
                        break;
                    }
                }
                Thread.Sleep(10);
            } while (true);
            if (backgroundWorker1.CancellationPending == true || g_closingByUser == true) return;

            g_result = w_ret;
            if (!IsDisposed && IsHandleCreated)
            {
                try
                {
                    BeginInvoke(new Action(() =>
                    {
                        Close();
                    }));
                }
                catch (ObjectDisposedException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private void WaitForInputRelease()
        {
            while (backgroundWorker1.CancellationPending == false && g_closingByUser == false)
            {
                int w_ret = g_mode == 0
                    ? nes_main.g_nes_io.read_device_joystick_for_setting()
                    : nes_main.g_nes_io.read_device_keyboard_for_setting();

                if (w_ret == -1) return;
                Thread.Sleep(10);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            g_closingByUser = true;
            if (backgroundWorker1.IsBusy == true) backgroundWorker1.CancelAsync();
            Close();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            g_closingByUser = true;
            g_result = -2;
            if (backgroundWorker1.IsBusy == true) backgroundWorker1.CancelAsync();
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (g_mode == 1 && TryGetDirectInputKeyNo(msg, out int w_keyNo) == true)
            {
                SetKeyboardResult(w_keyNo);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetKeyboardResult(int in_keyNo)
        {
            if (in_keyNo <= 0 || in_keyNo >= nes_io.KEY_STATUS_NUM) return;

            g_result = in_keyNo;
            g_closingByUser = true;
            if (backgroundWorker1.IsBusy == true) backgroundWorker1.CancelAsync();
            Close();
        }

        private static bool TryGetDirectInputKeyNo(Message in_msg, out int out_keyNo)
        {
            out_keyNo = -1;
            if (in_msg.Msg != WM_KEYDOWN && in_msg.Msg != WM_SYSKEYDOWN) return false;

            Keys w_virtualKey = (Keys)(int)in_msg.WParam;
            if (w_virtualKey == Keys.Pause)
            {
                out_keyNo = (int)SharpDX.DirectInput.Key.Pause;
                return true;
            }

            long w_lParam = in_msg.LParam.ToInt64();
            int w_scanCode = (int)((w_lParam >> 16) & 0xff);
            if (w_scanCode == 0) return false;

            bool w_extended = (w_lParam & (1L << 24)) != 0;
            out_keyNo = w_extended == true ? w_scanCode | 0x80 : w_scanCode;
            return true;
        }

        private void Form_IO_Setting_FormClosing(object? sender, FormClosingEventArgs e)
        {
            g_closingByUser = true;
            if (backgroundWorker1.IsBusy == true) backgroundWorker1.CancelAsync();
        }
    }

}
