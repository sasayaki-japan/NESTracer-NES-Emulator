using System.DirectoryServices.ActiveDirectory;

namespace NESTracer
{
    public partial class Form_IO : Form
    {
        public class ParamView
        {
            public string name { get; set; }
            public string joystick { get; set; }
            public string keyboard { get; set; }
        }
        public static List<ParamView> g_paramview;
        public static string[] JOYSTICKS_NAME = {
                 "Button 0"  ,"Button 1"  ,"Button 2"  ,"Button 3"
                ,"Button 4"  ,"Button 5"  ,"Button 6"  ,"Button 7"
                ,"Button 8"  ,"Button 9"  ,"Button 10" ,"Button 11"
                ,"Button 12" ,"Button 13" ,"Button 14" ,"Button 15"
                ,"Button 16" ,"Button 17" ,"Button 18" ,"Button 19"
                ,"Button 20" ,"Button 21" ,"Button 22" ,"Button 23"
                ,"Button 24" ,"Button 25" ,"Button 26" ,"Button 27"
                ,"Button 28" ,"Button 29" ,"Button 30" ,"Button 31"
                ,"Point 0 Up"  ,"Point 0 Down"  ,"Point 0 Left"  ,"Point 0 Right"
                ,"Point 1 Up"  ,"Point 1 Down"  ,"Point 1 Left"  ,"Point 1 Right"
                ,"Rot 0"     ,"Rot 1"     ,"Rot 2"     ,"Rot 3"
                ,"XYZ 0"     ,"XYZ 1"     ,"XYZ 2"     ,"XYZ 3"
                ,"XYZ 4"     ,"XYZ 5"
        };
        public static string[] KEYS_NAME = {
            "","Escape","D1","D2", "D3","D4","D5","D6",
            "D7","D8","D9","D0", "Minus","Equals","Back","Tab",
            "Q","W","E","R", "T","Y","U","I",
            "O","P","LeftBracket","RightBracket", "Return","LeftControl","A","S",
            "D","F","G","H", "J","K","L","Semicolon",
            "Apostrophe","Grave","LeftShift","Backslash", "Z","X","C","V",
            "B","N","M","Comma", "Period","Slash","RightShift","Multiply",
            "LeftAlt","Space","Capital","F1", "F2","F3","F4","F5",
            "F6","F7","F8","F9", "F10","NumberLock","ScrollLock","NumberPad7",
            "NumberPad8","NumberPad9","Subtract","NumberPad4", "NumberPad5","NumberPad6","Add","NumberPad1",
            "NumberPad2","NumberPad3","NumberPad0","Decimal", "","","Oem102","F11",
            "F12","","","", "","","","",
            "","","","", "F13","F14","F15","",
            "","","","", "","","","",
            "Kana","","","AbntC1", "","","","",
            "","Convert","NoConvert", "","Yen","AbntC2","",
            "","","","", "","","","",
            "","","","", "","NumberPadEquals","","",
            "PreviousTrack","AT","Colon","Underline", "Kanji","Stop","AX","Unlabeled",
            "","NextTrack","","", "NumberPadEnter","RightControl","","",
            "Mute","Calculator","PlayPause","", "MediaStop","","","",
            "","","","", "","","VolumeDown","",
            "VolumeUp","","WebHome","NumberPadComma", "","Divide","","PrintScreen",
            "RightAlt","","","", "","","","",
            "","","","", "","Pause","","Home",
            "Up","PageUp","","Left", "","Right","","End",
            "Down","PageDown","Insert","Delete", "","","","",
            "","","","LeftWindowsKey", "RightWindowsKey","Applications","Power","Sleep",
            "","","","Wake", "","WebSearch","WebFavorites","WebRefresh",
            "WebStop","WebForward","WebBack","MyComputer", "Mail","MediaSelect","","",
            "","","","", "","","","",
            "","","","", "","","",""
        };
        public int g_screen_xpos;
        public int g_screen_ypos;
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_IO()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            dataGridView_io.Font = new Font("Yu Gothic UI", 8);

            g_paramview = new List<ParamView>()
            {
                new ParamView{name="A",joystick="",keyboard="" },
                new ParamView{name="B",joystick="",keyboard="" },
                new ParamView{name="Select",joystick="",keyboard="" },
                new ParamView{name="Start",joystick="",keyboard="" },
                new ParamView{name="Up",joystick="",keyboard="" },
                new ParamView{name="Down",joystick="",keyboard="" },
                new ParamView{name="left",joystick="",keyboard="" },
                new ParamView{name="Right",joystick="",keyboard="" },
            };
            dataGridView_io.DataSource = g_paramview;
            DataGridViewButtonColumn column = new DataGridViewButtonColumn();
            column.DataPropertyName = "joystick";
            dataGridView_io.Columns.Insert(dataGridView_io.Columns["joystick"].Index, column);
            dataGridView_io.Columns.Remove("joystick");
            column.Name = "joystick";
            DataGridViewButtonColumn column2 = new DataGridViewButtonColumn();
            column2.DataPropertyName = "keyboard";
            dataGridView_io.Columns.Insert(dataGridView_io.Columns["keyboard"].Index, column2);
            dataGridView_io.Columns.Remove("keyboard");
            column2.Name = "keyboard";
        }

        //----------------------------------------------------------------
        //Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void dataGridView_io_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "joystick")
            {
                var form1 = new Form_IO_Setting();
                form1.g_mode = 0;
                form1.ShowDialog();
                int w_result = form1.g_result;
                form1.Dispose();

                if (w_result != -1)
                {
                    if (w_result == -2)
                    {
                        dataGridView_io[e.ColumnIndex, e.RowIndex].Value = "";
                        w_result = 0;
                    }
                    if (w_result != -1)
                    {
                        dataGridView_io[e.ColumnIndex, e.RowIndex].Value = JOYSTICKS_NAME[w_result];
                    }
                    nes_main.g_nes_io.g_joy_allocation[e.RowIndex] = w_result;
                }
                nes_main.write_setting();
            }
            if (dgv.Columns[e.ColumnIndex].Name == "keyboard")
            {
                var form1 = new Form_IO_Setting();
                form1.g_mode = 1;
                form1.ShowDialog();
                int w_result = form1.g_result;
                form1.Dispose();

                if (w_result != -1)
                {
                    if (w_result == -2)
                    {
                        dataGridView_io[e.ColumnIndex, e.RowIndex].Value = "";
                        w_result = 0;
                    }
                    if (w_result != -1)
                    {
                        dataGridView_io[e.ColumnIndex, e.RowIndex].Value = KEYS_NAME[w_result];
                    }
                    nes_main.g_nes_io.g_key_allocation[e.RowIndex] = w_result;
                }
                nes_main.write_setting();
            }
        }

        private void Form_IO_FormClosing(object sender, FormClosingEventArgs e)
        {
            nes_main.g_io_enable = false;
            nes_main.g_form_setting.update();
            nes_main.write_setting();
            e.Cancel = true;
        }
        private void Form_IO_ResizeEnd(object sender, EventArgs e)
        {
            var currentPosition = this.Location;
            g_screen_xpos = currentPosition.X;
            g_screen_ypos = currentPosition.Y;
            nes_main.write_setting();
        }
        private void Form_IO_Shown(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(g_screen_xpos, g_screen_ypos);
        }

        private void button_rescan_Click(object sender, EventArgs e)
        {
            rescan();
        }
        public void rescan()
        {
            nes_main.g_nes_io.rescan();
            comboBox1.Items.Clear();
            int w_index = -1;
            for (int i = 0; i < nes_main.g_nes_io.g_joy_name_list.Count; i++)
            {
                w_index = 0;
                comboBox1.Items.Add(nes_main.g_nes_io.g_joy_name_list[i]);
                if (nes_main.g_nes_io.g_joy_name == nes_main.g_nes_io.g_joy_name_list[i])
                {
                    w_index = i;
                }
            }
            if (w_index == 0)
            {
                nes_main.g_nes_io.g_joy_name = nes_main.g_nes_io.g_joy_name_list[0];
                w_index = 0;
            }
            nes_main.g_nes_io.g_joy_device_cur = w_index;
            comboBox1.SelectedIndex = w_index;
            nes_main.write_setting();
        }
        //----------------------------------------------------------------
        //initialize
        //----------------------------------------------------------------
        public void initialize()
        {
            if (nes_main.g_nes_io.g_joy_name_list.Count > 0)
            {
                for (int i = 0; i < nes_main.g_nes_io.g_joy_name_list.Count; i++)
                {
                    comboBox1.Items.Add(nes_main.g_nes_io.g_joy_name_list[i]);
                }
                comboBox1.SelectedIndex = 0;
            }
            nes_main.g_nes_io.g_joy_device_cur = 0;

            for (int i = 0; i < nes_io.KEY_ALLCATION_NUM; i++)
            {
                dataGridView_io[1, i].Value = JOYSTICKS_NAME[nes_main.g_nes_io.g_joy_allocation[i]];
                dataGridView_io[2, i].Value = KEYS_NAME[nes_main.g_nes_io.g_key_allocation[i]];
            }
        }
    }
}
