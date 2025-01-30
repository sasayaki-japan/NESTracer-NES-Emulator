using SharpDX.DirectInput;
using System.Diagnostics;
using System.Windows.Forms;

namespace NESTracer
{
    internal partial class nes_io
    {
        public void rescan()
        {
            DirectInput dinput = new DirectInput();
            if (dinput != null)
            {
                g_keyboard = new SharpDX.DirectInput.Keyboard(dinput);
                if (g_keyboard != null)
                {
                    g_keyboard.Properties.BufferSize = 128;
                }

                var w_device = dinput
                .GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices)
                .Concat(dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                .Concat(dinput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices))
                .ToList();
                g_joy_device_cur = -1;
                foreach (var deviceInstance in w_device)
                {
                    Joystick w_joy_obj = new Joystick(dinput, deviceInstance.InstanceGuid);
                    if (!g_joy_name_list.Contains(deviceInstance.ProductName))
                    {
                        g_joy_device.Add(w_joy_obj);
                        g_joy_name_list.Add(deviceInstance.ProductName);
                        if (deviceInstance.ProductName == g_joy_name)
                        {
                            g_joy_device_cur = g_joy_name_list.Count - 1;
                        }
                    }
                }
            }
        }
        public int read_device_joystick()
        {
            int w_out = -1;
            if (g_joy_device.Count > 0)
            {
                g_joy_device[g_joy_device_cur].Acquire();
                g_joy_device[g_joy_device_cur].Poll();
                var state = g_joy_device[g_joy_device_cur].GetCurrentState();
                if (state == null) { return -1; }

                for (int i = 0; i < JOY_STATUS_NUM; i++)
                {
                    g_joy_status[i] = 0;
                }
                for (int i = 0; i < 32; i++)
                {
                    if (state.Buttons[i] == true)
                    {
                        g_joy_status[i] = 1;
                        w_out = i;
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (state.PointOfViewControllers[i] == 0)
                    {
                        g_joy_status[32 + (i * 4)] = 1;
                        w_out = 32 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 4500)
                    {
                        g_joy_status[32 + (i * 4)] = 1;
                        g_joy_status[33 + (i * 4)] = 1;
                        w_out = 32 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 9000)
                    {
                        g_joy_status[33 + (i * 4)] = 1;
                        w_out = 33 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 13500)
                    {
                        g_joy_status[33 + (i * 4)] = 1;
                        g_joy_status[34 + (i * 4)] = 1;
                        w_out = 33 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 18000)
                    {
                        g_joy_status[34 + (i * 4)] = 1;
                        w_out = 34 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 22500)
                    {
                        g_joy_status[34 + (i * 4)] = 1;
                        g_joy_status[35 + (i * 4)] = 1;
                        w_out = 34 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 27000)
                    {
                        g_joy_status[35 + (i * 4)] = 1;
                        w_out = 35 + (i * 4);
                    }
                    else
                    if (state.PointOfViewControllers[i] == 31500)
                    {
                        g_joy_status[32 + (i * 4)] = 1;
                        g_joy_status[35 + (i * 4)] = 1;
                        w_out = 35 + (i * 4);
                    }
                }
                if (state.RotationX < 0x3000)
                {
                    g_joy_status[40] = 1;
                    w_out = 40;
                }
                else
                if (state.RotationX > 0xd000)
                {
                    g_joy_status[41] = 1;
                    w_out = 41;
                }
                if (state.RotationY < 0x3000)
                {
                    g_joy_status[42] = 1;
                    w_out = 42;
                }
                else
                if (state.RotationY > 0xd000)
                {
                    g_joy_status[43] = 1;
                    w_out = 43;
                }

                if (state.X < 0x3000)
                {
                    g_joy_status[44] = 1;
                    w_out = 44;
                }
                else
                if (state.X > 0xd000)
                {
                    g_joy_status[45] = 1;
                    w_out = 45;
                }
                if (state.Y < 0x3000)
                {
                    g_joy_status[46] = 1;
                    w_out = 46;
                }
                else
                if (state.Y > 0xd000)
                {
                    g_joy_status[47] = 1;
                    w_out = 47;
                }

                if (state.Z < 0x3000)
                {
                    g_joy_status[48] = 1;
                    w_out = 48;
                }
                else
                if (state.Z > 0xd000)
                {
                    g_joy_status[49] = 1;
                    w_out = 49;
                }
            }
            return w_out;
        }
        public int read_device_keyboard()
        {
            int w_out = -1;
            if (g_keyboard != null)
            {
                g_keyboard.Acquire();
                g_keyboard.Poll();
                var state = g_keyboard.GetBufferedData();
                if (state == null) { return -1; }
                var isPressedKeys = state.Where(n => n.IsPressed);
                foreach (var key in isPressedKeys)
                {
                    g_key_status[(int)key.Key] = 1;
                    w_out = (int)key.Key;
                }
                var issReleasedKeys = state.Where(n => n.IsReleased);
                foreach (var key in issReleasedKeys)
                {
                    g_key_status[(int)key.Key] = 0;
                    w_out = (int)key.Key;
                }
                for (int i = 0; i < KEY_STATUS_NUM; i++)
                {
                    if (g_key_status[i] == 1)
                    {
                        w_out = i;
                        break;
                    }
                }
            }
            return w_out;
        }

    }
}
