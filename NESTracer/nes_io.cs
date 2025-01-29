using Microsoft.VisualBasic.Devices;
using SharpDX.DirectInput;

namespace NESTracer
{
    internal partial class nes_io
    {
        public const int JOY_STATUS_NUM = 50;
        public const int KEY_STATUS_NUM = 256;
        public const int KEY_ALLCATION_NUM = 8;

        public byte[] g_joy_status;
        public byte[] g_key_status;

        private SharpDX.DirectInput.Keyboard g_keyboard;
        public List<SharpDX.DirectInput.Joystick> g_joy_device;
        public List<string> g_joy_name_list;
        public int g_joy_device_cur;
        public string g_joy_name;
        public int[] g_key_allocation;
        public int[] g_joy_allocation;

        public int g_key_cur;
        //----------------------------------------------------------------
        public nes_io()
        {
            g_joy_name_list = new List<string>();
            g_joy_device = new List<Joystick>();
            g_joy_status = new byte[JOY_STATUS_NUM];
            g_key_status = new byte[KEY_STATUS_NUM];
            g_joy_allocation = new int[KEY_ALLCATION_NUM];
            g_key_allocation = new int[KEY_ALLCATION_NUM];
        }
        //----------------------------------------------------------------
        //read
        //----------------------------------------------------------------
        public byte read1(int in_address)
        {
            byte w_out = 0;
            switch (in_address)
            {
                case 0x4016:
                    if (g_key_cur < 8)
                    {
                        w_out = (byte)(g_key_status[g_key_allocation[g_key_cur]]);
                        //w_out = (byte)(g_key_status[g_key_allocation[g_key_cur]] | g_joy_status[g_joy_allocation[g_key_cur]]);
                        g_key_cur += 1;
                    }
                    break;
                default:
                    break;
            }
            return w_out;
        }
        //----------------------------------------------------------------
        //write
        //----------------------------------------------------------------
        public void write1(int in_address, byte in_val)
        {
            switch (in_address)
            {
                case 0x4016:
                    if((in_val & 0x01) == 0)
                    {
                        read_device_keyboard();
                        read_device_joystick();
                        g_key_cur = 0;
                    }
                    break;
                case 0x4017:
                    break;
                default:
                    break;
            }
        }
    }
}
