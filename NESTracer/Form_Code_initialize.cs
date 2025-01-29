using System;
using static NESTracer.nes_6502;

namespace NESTracer
{
    public partial class Form_Code_Trace
    {
        public struct TRACECODE
        {
            public enum TYPE : int
            {
                NON,
                OPC,
                OPR,
                UNIQUE,
                CHK
            }
            public TYPE type;
            public int bank;
            public int s_addr;
            public byte val;
            public int leng;
            public int front;            
            public string operand;
            public string comment1;
            public bool break_static;
            public bool break_flash;
            public int jmp_offset;
            public bool ret_line;
            public bool operand_jsr;
        }
        public TRACECODE[,] g_analyse_code_rom;
        public TRACECODE[] g_analyse_code_ram;

        public struct OP_COMMENT1
        {
            public int address;
            public string comment;
            public OP_COMMENT1(int in_address, string in_comment)
            {
                address = in_address;
                comment = in_comment;
            }
        };
        private List<OP_COMMENT1> g_op_comment;

        public void initialize()
        {
            g_cpu_pause = false;
            g_waitHandle = new ManualResetEvent(false);
            g_arrow_start_line = -1;
            g_arrow_end_line = -1;

            g_op_comment = new List<OP_COMMENT1>();
            g_op_comment.Add(new OP_COMMENT1(0x000000, ""));
            g_op_comment.Add(new OP_COMMENT1(0x2000, "PPU:PPU control($2000)"));
            g_op_comment.Add(new OP_COMMENT1(0x2001, "PPU:PPU mask($2001)"));
            g_op_comment.Add(new OP_COMMENT1(0x2002, "PPU:PPU status($2002)"));
            g_op_comment.Add(new OP_COMMENT1(0x2003, "PPU:OAM address port($2003)"));
            g_op_comment.Add(new OP_COMMENT1(0x2004, "PPU:OAM data port($2004)"));
            g_op_comment.Add(new OP_COMMENT1(0x2005, "PPU:PPU scrolling position($2005)"));
            g_op_comment.Add(new OP_COMMENT1(0x2006, "PPU:PPU address($2006)"));
            g_op_comment.Add(new OP_COMMENT1(0x2007, "PPU:PPU data port($2007)"));
            g_op_comment.Add(new OP_COMMENT1(0x4000, "APU:channe1:Pulse1($4000～3)"));
            g_op_comment.Add(new OP_COMMENT1(0x4004, "APU:channe2:Pulse2($4004～7)"));
            g_op_comment.Add(new OP_COMMENT1(0x4008, "APU:channe3:Triangle($4008～b)"));
            g_op_comment.Add(new OP_COMMENT1(0x400c, "APU:channe4:Noise($400c～f)"));
            g_op_comment.Add(new OP_COMMENT1(0x4010, "APU:channe5:DMC($4010～3)"));
            g_op_comment.Add(new OP_COMMENT1(0x4014, "PPU:OAM DMA($4014)"));
            g_op_comment.Add(new OP_COMMENT1(0x4015, "APU:status($4015)"));
            g_op_comment.Add(new OP_COMMENT1(0x4016, "I/O:Joystick1/Joystick strobe($4016)"));
            g_op_comment.Add(new OP_COMMENT1(0x4017, "I/O:Joystick2/Frame counter($4017)"));
            g_op_comment.Add(new OP_COMMENT1(0x4018, ""));
        }
    }
}
