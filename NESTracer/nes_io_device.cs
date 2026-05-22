using SharpDX.DirectInput;

namespace NESTracer
{
    internal partial class nes_io
    {
        public void rescan()
        {
            rescan(true);
        }

        private void rescan(bool in_updateKeyboard)
        {
            lock (g_joy_rescan_lock)
            {
                g_joy_last_rescan_time = Environment.TickCount64;
                string w_target_name;
                string w_old_name;
                string[] w_old_name_list;
                Guid[] w_old_guid_list;
                int w_old_device_cur;
                lock (g_joy_device_lock)
                {
                    w_target_name = g_joy_name;
                    w_old_name = g_joy_name;
                    w_old_name_list = g_joy_name_list.ToArray();
                    w_old_guid_list = g_joy_instance_guid_list.ToArray();
                    w_old_device_cur = g_joy_device_cur;
                }

                DirectInput dinput = new DirectInput();
                if (dinput != null)
                {
                    List<Joystick> w_new_joy_device = new List<Joystick>();
                    List<string> w_new_joy_name_list = new List<string>();
                    List<Guid> w_new_guid_list = new List<Guid>();
                    int w_new_joy_device_cur = -1;
                    string w_new_joy_name = w_target_name;

                    SharpDX.DirectInput.Keyboard w_new_keyboard = null;
                    if (in_updateKeyboard)
                    {
                        w_new_keyboard = new SharpDX.DirectInput.Keyboard(dinput);
                        if (w_new_keyboard != null)
                        {
                            w_new_keyboard.Properties.BufferSize = 128;
                        }
                    }

                    var w_device = dinput
                    .GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices)
                    .Concat(dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    .Concat(dinput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices))
                    .ToList();
                    foreach (var deviceInstance in w_device)
                    {
                        Joystick w_joy_obj = new Joystick(dinput, deviceInstance.InstanceGuid);
                        if (!w_new_joy_name_list.Contains(deviceInstance.ProductName))
                        {
                            w_new_joy_device.Add(w_joy_obj);
                            w_new_joy_name_list.Add(deviceInstance.ProductName);
                            w_new_guid_list.Add(deviceInstance.InstanceGuid);
                            if(deviceInstance.ProductName == w_target_name)
                            {
                                w_new_joy_device_cur = w_new_joy_name_list.Count - 1;
                            }
                        }
                        else
                        {
                            w_joy_obj.Dispose();
                        }
                    }
                    if (w_new_joy_device_cur < 0 && w_new_joy_device.Count > 0)
                    {
                        w_new_joy_device_cur = 0;
                        w_new_joy_name = w_new_joy_name_list[0];
                    }

                    bool w_changed = w_old_device_cur != w_new_joy_device_cur
                        || w_old_name != w_new_joy_name
                        || !w_old_name_list.SequenceEqual(w_new_joy_name_list)
                        || !w_old_guid_list.SequenceEqual(w_new_guid_list);

                    if (in_updateKeyboard == false && w_changed == false)
                    {
                        foreach (Joystick w_joy in w_new_joy_device)
                        {
                            w_joy.Dispose();
                        }
                        return;
                    }

                    List<Joystick> w_old_joy_device;
                    SharpDX.DirectInput.Keyboard w_old_keyboard;
                    lock (g_joy_device_lock)
                    {
                        w_old_joy_device = g_joy_device;
                        w_old_keyboard = in_updateKeyboard ? g_keyboard : null;
                        g_joy_device = w_new_joy_device;
                        g_joy_name_list = w_new_joy_name_list;
                        g_joy_instance_guid_list = w_new_guid_list;
                        g_joy_device_cur = w_new_joy_device_cur;
                        g_joy_name = w_new_joy_name;
                        if (in_updateKeyboard)
                        {
                            g_keyboard = w_new_keyboard;
                        }
                    }

                    foreach (Joystick w_joy in w_old_joy_device)
                    {
                        w_joy.Dispose();
                    }
                    w_old_keyboard?.Dispose();

                    if (w_changed && w_new_joy_device.Count > 0 && nes_main.g_form_io != null)
                    {
                        nes_main.g_form_io.update_joystick_combo_from_device_scan();
                    }
                }
            }
        }
        public void start_periodic_joystick_rescan()
        {
            if (g_joy_rescan_timer != null) return;
            g_joy_rescan_timer = new System.Threading.Timer(_ => rescan_joystick_if_needed(), null, JOY_RESCAN_INTERVAL_MS, JOY_RESCAN_INTERVAL_MS);
        }
        
        private void rescan_joystick_if_needed()
        {
            long w_now = Environment.TickCount64;
            if (w_now - g_joy_last_rescan_time < JOY_RESCAN_INTERVAL_MS) return;
            g_joy_last_rescan_time = w_now;
            if (Interlocked.CompareExchange(ref g_joy_rescan_in_progress, 1, 0) != 0) return;

            Task.Run(() =>
            {
                try
                {
                    rescan(false);
                }
                finally
                {
                    Interlocked.Exchange(ref g_joy_rescan_in_progress, 0);
                }
            });
        }
        public int read_device_joystick()
        {
            return read_device_joystick_core(g_joy_status);
        }

        public int read_device_joystick_for_setting()
        {
            return read_device_joystick_core(null);
        }

        private int read_device_joystick_core(byte[]? in_status)
        {
            int w_out = -1;
            if (in_status != null)
            {
                Array.Clear(in_status, 0, in_status.Length);
            }

            lock (g_joy_device_lock)
            {
                if (g_joy_device.Count <= 0 || g_joy_device_cur < 0 || g_joy_device_cur >= g_joy_device.Count)
                {
                    rescan_joystick_if_needed();
                    return -1;
                }

                try
                {
                    g_joy_device[g_joy_device_cur].Acquire();
                    g_joy_device[g_joy_device_cur].Poll();
                }
                catch
                {
                    rescan_joystick_if_needed();
                    return -1;
                }

                JoystickState state = g_joy_device[g_joy_device_cur].GetCurrentState();
                if (state == null)
                {
                    rescan_joystick_if_needed();
                    return -1;
                }

                bool[] w_buttons = state.Buttons;
                for (int i = 0; i < 32 && i < w_buttons.Length; i++)
                {
                    if (w_buttons[i] == true)
                    {
                        set_joystick_status(in_status, i, ref w_out);
                    }
                }

                int[] w_povs = state.PointOfViewControllers;
                for (int i = 0; i < 2 && i < w_povs.Length; i++)
                {
                    update_joystick_pov_status(in_status, 32 + (i * 4), w_povs[i], ref w_out);
                }

                update_joystick_axis_status(in_status, state.RotationX, 40, 41, ref w_out);
                update_joystick_axis_status(in_status, state.RotationY, 42, 43, ref w_out);
                update_joystick_axis_status(in_status, state.X, 44, 45, ref w_out);
                update_joystick_axis_status(in_status, state.Y, 46, 47, ref w_out);
                update_joystick_axis_status(in_status, state.Z, 48, 49, ref w_out);
            }

            return w_out;
        }

        private static void update_joystick_pov_status(byte[]? in_status, int in_baseIndex, int in_value, ref int out_pressedIndex)
        {
            switch (in_value)
            {
                case 0:
                    set_joystick_status(in_status, in_baseIndex, ref out_pressedIndex);
                    break;
                case 4500:
                    set_joystick_status(in_status, in_baseIndex, ref out_pressedIndex);
                    set_joystick_status(in_status, in_baseIndex + 1, ref out_pressedIndex);
                    break;
                case 9000:
                    set_joystick_status(in_status, in_baseIndex + 1, ref out_pressedIndex);
                    break;
                case 13500:
                    set_joystick_status(in_status, in_baseIndex + 1, ref out_pressedIndex);
                    set_joystick_status(in_status, in_baseIndex + 2, ref out_pressedIndex);
                    break;
                case 18000:
                    set_joystick_status(in_status, in_baseIndex + 2, ref out_pressedIndex);
                    break;
                case 22500:
                    set_joystick_status(in_status, in_baseIndex + 2, ref out_pressedIndex);
                    set_joystick_status(in_status, in_baseIndex + 3, ref out_pressedIndex);
                    break;
                case 27000:
                    set_joystick_status(in_status, in_baseIndex + 3, ref out_pressedIndex);
                    break;
                case 31500:
                    set_joystick_status(in_status, in_baseIndex, ref out_pressedIndex);
                    set_joystick_status(in_status, in_baseIndex + 3, ref out_pressedIndex);
                    break;
            }
        }

        private static void update_joystick_axis_status(byte[]? in_status, int in_value, int in_lowIndex, int in_highIndex, ref int out_pressedIndex)
        {
            if (in_value < 0x3000)
            {
                set_joystick_status(in_status, in_lowIndex, ref out_pressedIndex);
            }
            else if (in_value > 0xd000)
            {
                set_joystick_status(in_status, in_highIndex, ref out_pressedIndex);
            }
        }

        private static void set_joystick_status(byte[]? in_status, int in_index, ref int out_pressedIndex)
        {
            if (in_index < 0 || in_index >= JOY_STATUS_NUM) return;
            if (in_status != null && in_index < in_status.Length)
            {
                in_status[in_index] = 1;
            }
            out_pressedIndex = in_index;
        }

        public int read_device_keyboard()
        {
            return read_device_keyboard_core(g_key_status);
        }

        public int read_device_keyboard_for_setting()
        {
            return read_device_keyboard_core(null);
        }

        private int read_device_keyboard_core(byte[]? in_status)
        {
            int w_out = -1;
            lock (g_joy_device_lock)
            {
                if (in_status != null)
                {
                    Array.Clear(in_status, 0, in_status.Length);
                }

                if (g_keyboard != null)
                {
                    try
                    {
                        g_keyboard.Acquire();
                        g_keyboard.Poll();
                        KeyboardState state = g_keyboard.GetCurrentState();
                        if (state == null) { return -1; }

                        foreach (Key w_key in state.PressedKeys)
                        {
                            int w_keyNo = (int)w_key;
                            if (0 <= w_keyNo && w_keyNo < KEY_STATUS_NUM)
                            {
                                if (in_status != null && w_keyNo < in_status.Length)
                                {
                                    in_status[w_keyNo] = 1;
                                }
                                if (w_out == -1)
                                {
                                    w_out = w_keyNo;
                                }
                            }
                        }
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
            return w_out;
        }

    }
}
