using static NESTracer.Form_Code_Trace;

namespace NESTracer
{
    public partial class Form_Code_Bank : Form
    {
        public int g_top_line;
        public int g_screen_xpos;
        public int g_screen_ypos;
        //----------------------------------------------------------------
        //form
        //----------------------------------------------------------------
        public Form_Code_Bank()
        {
            InitializeComponent();
            pictureBox_code.MouseWheel += PictureBox_code_MouseWheel;
            scrollbar_set();
        }
        //----------------------------------------------------------------
        //initialize
        //----------------------------------------------------------------
        public void initialize()
        {
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = nes_main.g_nes_mapper_control.g_prg_bank_num - 1;
        }
        //----------------------------------------------------------------
        //Event Handling: Painting
        //----------------------------------------------------------------
        private void pictureBox_code_paint(object sender, PaintEventArgs e)
        {
            vScrollBar_code.Value = g_top_line;
            Code_Paint_Code(e, pictureBox_code.Width
                            , pictureBox_code.Height
                            , pictureBox_Code_line_num()
                            , g_top_line
                            , hScrollBar_code.Value);
        }
        //----------------------------------------------------------------               
        //Event Handling: Screen Operations
        //----------------------------------------------------------------
        private void Form_Bank_Code_Resize(object sender, EventArgs e)
        {
            scrollbar_set();
            pictureBox_code.Invalidate();
        }
        private void Form_Bank_Code_FormClosing(object sender, FormClosingEventArgs e)
        {
            nes_main.g_code_enable = false;
            nes_main.g_form_setting.update();
            nes_main.write_setting();
            e.Cancel = true;
        }
        private void Form_Bank_Code_ResizeEnd(object sender, EventArgs e)
        {
            var currentPosition = this.Location;
            g_screen_xpos = currentPosition.X;
            g_screen_ypos = currentPosition.Y;
            nes_main.write_setting();
        }

        private void Form_Bank_Code_Shown(object sender, EventArgs e)
        {
            Location = new Point(g_screen_xpos, g_screen_ypos);
        }
        //----------------------------------------------------------------
        //Event Handling: mouse operations
        //----------------------------------------------------------------
        private void vScrollBar_code_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll)
            {
                picturebox_scroll(e.NewValue, 0);
            }
        }
        private void hScrollBar_code_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll)
            {
                scrollbar_set();
                pictureBox_code.Invalidate();
            }
        }
        private void PictureBox_code_MouseWheel(object sender, MouseEventArgs e)
        {
            int w_cur = 0;
            if (e.Delta > 0) w_cur = -1;
            if (e.Delta < 0) w_cur = 1;
            picturebox_scroll(g_top_line, w_cur);
        }
        //----------------------------------------------------------------
        //Event Handling: key operations
        //----------------------------------------------------------------
        private void Form_Code_Bank_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    picturebox_scroll(g_top_line, 1);
                    break;
                case Keys.Up:
                    picturebox_scroll(g_top_line, -1);
                    break;
                case Keys.PageDown: picturebox_scroll(g_top_line, pictureBox_Code_line_num() - 1); break;
                case Keys.PageUp: picturebox_scroll(g_top_line, -(pictureBox_Code_line_num() - 1)); break;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox_code.Invalidate();
        }
        //----------------------------------------------------------------
        //Event Handling: menu
        //----------------------------------------------------------------
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           nes_main.g_form_code_trace.analyses();
           pictureBox_code.Invalidate();
        }
        //----------------------------------------------------------------
        //sub function
        //----------------------------------------------------------------
        public int pictureBox_Code_line_num()
        {
            return (pictureBox_code.Height - 4) / 16;
        }
        private void scrollbar_set()
        {
            int w_leng = pictureBox_Code_line_num();
            vScrollBar_code.Maximum = 0x2000;
            vScrollBar_code.SmallChange = 1;
            vScrollBar_code.LargeChange = w_leng;
            hScrollBar_code.Maximum = 1500;
            hScrollBar_code.SmallChange = 1;
            hScrollBar_code.LargeChange = pictureBox_code.Width;
        }
        public void picturebox_scroll(int in_line, int in_line_offset)
        {
            int w_line = in_line;
            if (in_line_offset != 0)
            {
                if (0 < in_line_offset)
                {
                    for (int i = 0; i < in_line_offset; i++)
                    {
                        TRACECODE w_code = nes_main.g_form_code_trace.read_analyse_code(w_line);
                        w_line += w_code.leng;
                        if (w_line >= 0x2000)
                        {
                            w_line = in_line;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < -in_line_offset; i++)
                    {
                        if (w_line <= 0)
                        {
                            w_line = in_line;
                            break;
                        }
                        TRACECODE w_code = nes_main.g_form_code_trace.read_analyse_code(w_line - 1);
                        w_line -= w_code.front + 1;
                    }
                }
            }
            else
            {
                TRACECODE w_code = nes_main.g_form_code_trace.read_analyse_code(w_line);
                TRACECODE w_code_prev = nes_main.g_form_code_trace.read_analyse_code(w_line + w_code.front);
                TRACECODE w_code_next = nes_main.g_form_code_trace.read_analyse_code(w_line - w_code.leng);
                if (g_top_line < w_line)
                {
                    if (w_line - w_code.front > 0)
                    {
                        w_line += w_code_prev.leng;
                    }
                    else
                    {
                        w_line -= w_code_next.front;
                    }
                }
                else
                if (g_top_line > w_line)
                {
                    if (w_line + w_code.leng < 0x2000)
                    {
                        w_line -= w_code_next.front;
                    }
                    else
                    {
                        w_line += w_code_prev.leng;
                    }
                }
            }
            if ((in_line - w_line > 100) || (in_line - w_line < -100)) w_line = w_line;


            g_top_line = w_line;
            pictureBox_code.Invalidate();
        }
        //----------------------------------------------------------------
        public void Code_Paint_Code(PaintEventArgs e, int in_width, int in_height
            , int in_line_num, int in_top_line, int in_hScrollBar)
        {
            e.Graphics.FillRectangle(Brushes.White, 0, 0, in_width, in_height);
            e.Graphics.FillRectangle(Brushes.DarkBlue, 0, 0, in_width, 20);
            Font wfont = new Font("ＭＳ ゴシック", 10);
            Font wfont_data = new Font("ＭＳ ゴシック", 9);
            Brush wbrush = Brushes.White;
            Brush wbrush_red = Brushes.Red;
            e.Graphics.DrawString("addr", wfont, wbrush, new PointF(20 - in_hScrollBar, 1));
            e.Graphics.DrawString("dump", wfont, wbrush, new PointF(70 - in_hScrollBar, 1));
            e.Graphics.DrawString("mnemonic", wfont, wbrush, new PointF(200 - in_hScrollBar, 1));
            e.Graphics.DrawString("comment1", wfont, wbrush, new PointF(400 - in_hScrollBar, 1));
            //-----------------------------------------------------------
            string w_text;
            int w_cur_line = in_top_line;
            int w_bank = (int)numericUpDown1.Value;
            for (int w_cur = 0; w_cur < in_line_num; w_cur++)
            {
                if (0x2000 <= w_cur_line) break;
                TRACECODE w_code = nes_main.g_form_code_trace.g_analyse_code_rom[w_bank, w_cur_line];
                if (w_code.type == TRACECODE.TYPE.NON)
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, 0, 20 + w_cur * 16, in_width, 16);
                }
                if (w_code.ret_line == true)
                {
                    e.Graphics.DrawLine(Pens.LightGray, 0, 20 + w_cur * 16 + 15, in_width, 20 + w_cur * 16 + 15);
                }
                //address
                w_text = w_cur_line.ToString("X4");
                e.Graphics.DrawString(w_text, wfont, Brushes.Black, new PointF(20 - in_hScrollBar, 20 + w_cur * 16));
                //dump
                int w_dump_len = 0;
                if (w_code.type == TRACECODE.TYPE.NON)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (0x2000 <= w_cur_line + i) break;
                        if (nes_main.g_form_code_trace.g_analyse_code_rom[w_bank, w_cur_line + i].type != TRACECODE.TYPE.NON) break;
                        w_dump_len += 1;
                    }
                }
                else
                {
                    w_dump_len = w_code.leng;
                }
                for (int i = 0; i < w_dump_len; i++)
                {
                    w_text = nes_main.g_form_code_trace.g_analyse_code_rom[w_bank, w_cur_line + i].val.ToString("X2");
                    e.Graphics.DrawString(w_text, wfont_data, Brushes.Blue, new PointF(70 + i * 15 - in_hScrollBar, 22 + w_cur * 16));
                }
                //View details
                if (w_code.type == TRACECODE.TYPE.OPC)
                {
                    //mnemonic
                    w_text = w_code.operand;
                    e.Graphics.DrawString(w_text, wfont, Brushes.Black, new PointF(200 - in_hScrollBar, 20 + w_cur * 16));
                }
                //comment1
                e.Graphics.DrawString(w_code.comment1, wfont, Brushes.Black, new PointF(400 - in_hScrollBar, 20 + w_cur * 16));
                w_cur_line += w_dump_len;
            }
            wfont.Dispose();
            wfont_data.Dispose();
        }


    }
}

