namespace NESTracer
{
    public partial class Form_Code_Trace
    {
        public int g_arrow_start_line;
        public int g_arrow_end_line;
        //----------------------------------------------------------------
        public void Code_Paint_Code(PaintEventArgs e, int in_width, int in_height
            , int in_line_num, int in_top_line, int in_stop_line, int in_cursole_line, int in_hScrollBar)
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
            int w_arrow_start_line = -1;
            int w_arrow_end_line = -1;
            for (int w_cur = 0; w_cur < in_line_num; w_cur++)
            {
                if (MEMSIZE <= w_cur_line) break;
                if (g_arrow_start_line == w_cur_line) w_arrow_start_line = w_cur;
                if (g_arrow_end_line == w_cur_line) w_arrow_end_line = w_cur;
                if (128 < Math.Abs(g_arrow_start_line - g_arrow_end_line))
                {
                    w_arrow_start_line = -1;
                    w_arrow_end_line = -1;
                }
                TRACECODE w_code = read_analyse_code(w_cur_line);
                if (w_cur_line == in_stop_line)
                {
                    e.Graphics.FillRectangle(Brushes.LightBlue, 0, 20 + w_cur * 16, in_width, 16);
                }
                else
                if (w_code.type == TRACECODE.TYPE.NON)
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, 0, 20 + w_cur * 16, in_width, 16);
                }
                if (w_code.ret_line == true)
                {
                    e.Graphics.DrawLine(Pens.LightGray, 0, 20 + w_cur * 16 + 15, in_width, 20 + w_cur * 16 + 15);
                }
                if (w_cur_line == in_cursole_line)
                {
                    e.Graphics.DrawRectangle(Pens.Gray, 0, 20 + w_cur * 16, in_width, 15);
                }
                //break mark
                if (w_code.break_static == true)
                {
                    w_text = "BK";
                    e.Graphics.DrawString(w_text, wfont, wbrush_red, new PointF(-in_hScrollBar, 20 + w_cur * 16));
                }
                //address
                w_text = analyse_code_line2addrewss(w_cur_line).ToString("X4");
                e.Graphics.DrawString(w_text, wfont, Brushes.Black, new PointF(20 - in_hScrollBar, 20 + w_cur * 16));

                //dump
                int w_dump_len = w_code.leng;
                if (w_code.type == TRACECODE.TYPE.NON)
                {
                    w_dump_len = 0;
                    for (int i = 0; i < 16; i++)
                    {
                        if (Form_Code_Trace.MEMSIZE <= w_cur_line + i) break;
                        if (read_analyse_code(w_cur_line + i).type != TRACECODE.TYPE.NON) break;
                        w_dump_len += 1;
                    }
                }
                for (int i = 0; i < w_dump_len; i++)
                {
                    w_text = read_analyse_code(w_cur_line + i).val.ToString("X2");
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

            //arrow
            if ((w_arrow_start_line != -1) || (w_arrow_end_line != -1))
            {
                if (w_arrow_start_line != -1)
                {
                    e.Graphics.DrawRectangle(Pens.Red, 228 - in_hScrollBar, (28 + w_arrow_start_line * 16), 10, 1);
                }
                if (w_arrow_end_line != -1)
                {
                    e.Graphics.DrawString("∠", wfont, Brushes.Red, new PointF(225 - in_hScrollBar, 19 + w_arrow_end_line * 16));
                }
                int w_top = 0;
                int w_bottom = 0;
                if (g_arrow_start_line > g_arrow_end_line)
                {
                    int w_work = w_arrow_start_line;
                    w_arrow_start_line = w_arrow_end_line;
                    w_arrow_end_line = w_work;
                }
                if (w_arrow_start_line == -1)
                {
                    w_top = 20;
                }
                else
                {
                    w_top = 28 + w_arrow_start_line * 16;
                }
                if (w_arrow_end_line == -1)
                {
                    w_bottom = 40 + in_line_num * 16;
                }
                else
                {
                    w_bottom = 28 + w_arrow_end_line * 16;
                }
                int w_haba = w_bottom - w_top;
                e.Graphics.DrawRectangle(Pens.Red, 238 - in_hScrollBar, w_top, 1, w_haba);
            }
            else
            {
                if ((g_arrow_start_line < g_arrow_end_line)
                    && (g_arrow_start_line < in_top_line)
                    && (g_arrow_end_line > in_top_line))
                {
                    int w_bottom = 40 + in_line_num * 16;
                    e.Graphics.DrawRectangle(Pens.Red, 238 - in_hScrollBar, 20, 1, w_bottom);
                }
            }
            wfont.Dispose();
            wfont_data.Dispose();
        }
    }
}
