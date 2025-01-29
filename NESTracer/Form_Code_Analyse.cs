using System.Diagnostics;
using static NESTracer.nes_6502;

namespace NESTracer
{
    public partial class Form_Code_Trace
    {
        public const int MEMSIZE = 0x8000 + 0x0800;

        public void update()
        {
            g_analyse_code_rom = new TRACECODE[nes_main.g_nes_mapper_control.g_prg_bank_num
                                                , nes_mapper_control.PRG_ROM_BANK_SIZE];
            g_analyse_code_ram = new TRACECODE[0x0800];
            for (
                int wbank = 0; wbank < nes_main.g_nes_mapper_control.g_prg_bank_num; wbank++)
            {
                for (int i = 0; i < nes_mapper_control.PRG_ROM_BANK_SIZE; i++)
                {
                    g_analyse_code_rom[wbank, i] = new TRACECODE();
                    g_analyse_code_rom[wbank, i].type = TRACECODE.TYPE.NON;
                    g_analyse_code_rom[wbank, i].leng = 1;
                    g_analyse_code_rom[wbank, i].front = 0;
                    g_analyse_code_rom[wbank, i].val = nes_main.g_nes_6502.g_rom[wbank, i];
                    g_analyse_code_rom[wbank, i].bank = wbank;
                    g_analyse_code_rom[wbank, i].s_addr = i;
                    //g_analyse_code_rom[wbank, i].stack = new List<STACK_LIST>();
                }
            }
            {
                int w_bank = nes_main.g_nes_mapper_control.g_prg_bank_num;
                for (int i = 0; i < 0x0800; i++)
                {
                    g_analyse_code_ram[i] = new TRACECODE();
                    g_analyse_code_ram[i].type = TRACECODE.TYPE.NON;
                    g_analyse_code_ram[i].leng = 1;
                    g_analyse_code_ram[i].front = 0;
                    g_analyse_code_ram[i].bank = w_bank;
                    g_analyse_code_ram[i].s_addr = i;
                    //g_analyse_code_ram[wbank, i].stack = new List<STACK_LIST>();
                }
            }
            {
                int wbank = nes_main.g_nes_mapper_control.g_prg_bank_num - 1;
                g_analyse_code_rom[wbank, 8186].type = TRACECODE.TYPE.UNIQUE;
                g_analyse_code_rom[wbank, 8186].leng = 2;
                g_analyse_code_rom[wbank, 8186].comment1 = "NMI vector";
                g_analyse_code_rom[wbank, 8187].leng = 2;
                g_analyse_code_rom[wbank, 8187].front = 1;
                g_analyse_code_rom[wbank, 8188].type = TRACECODE.TYPE.UNIQUE;
                g_analyse_code_rom[wbank, 8188].leng = 2;
                g_analyse_code_rom[wbank, 8188].comment1 = "RESET vector";
                g_analyse_code_rom[wbank, 8189].leng = 2;
                g_analyse_code_rom[wbank, 8189].front = 1;
                g_analyse_code_rom[wbank, 8190].type = TRACECODE.TYPE.UNIQUE;
                g_analyse_code_rom[wbank, 8190].leng = 2;
                g_analyse_code_rom[wbank, 8190].comment1 = "IRQ/BRK vector";
                g_analyse_code_rom[wbank, 8191].leng = 2;
                g_analyse_code_rom[wbank, 8191].front = 1;
            }
            int w_addr = read_analyse_code(0x87fc).val + (read_analyse_code(0x87fd).val << 8);
            int w_line = analyse_code_addrewss2line(w_addr);
            TRACECODE w_code = read_analyse_code(w_line);
            w_code.type = TRACECODE.TYPE.CHK;
            write_analyse_code(w_line, w_code);
            analyses();
        }

        public void analyses()
        {
            {
                for (int i = 0; i < 0x0800; i++)
                {
                    byte w_val = nes_main.g_nes_6502.g_ram[i];
                    if (g_analyse_code_ram[i].val != w_val)
                    {
                        g_analyse_code_ram[i].type = TRACECODE.TYPE.NON;
                        g_analyse_code_ram[i].leng = 1;
                        g_analyse_code_ram[i].front = 0;
                        g_analyse_code_ram[i].val = w_val;
                        g_analyse_code_ram[i].comment1 = "";
                        g_analyse_code_ram[i].ret_line = false;
                    }
                }
            }

            int wchange = 0;
            do
            {
                wchange = 0;
                int w_line = 0;
                do
                {
                    int w_leng = 0;
                    TRACECODE w_code1 = read_analyse_code(w_line);
                    TRACECODE w_code2 = default;
                    TRACECODE w_code3 = default;
                    if (w_code1.type == TRACECODE.TYPE.CHK)
                    {
                        OPLIST w_op1 = nes_main.g_nes_6502.g_oplist[w_code1.val];
                        w_leng = w_op1.size;
                        if (w_leng == 0)
                        {
                            w_code1.type = TRACECODE.TYPE.NON;
                            w_code1.leng = 1;
                            w_code1.front = 0;
                            write_analyse_code(w_line, w_code1);
                            w_leng = 1;
                        }
                        else
                        {
                            w_code1.type = TRACECODE.TYPE.OPC;
                            w_code1.leng = w_leng;
                            w_code1.front = 0;
                            int w_val = 0;
                            string w_val_string = "";
                            if (w_leng >= 2)
                            {
                                w_code2 = read_analyse_code(w_line + 1);
                                w_code2.type = TRACECODE.TYPE.OPR;
                                w_code2.leng = w_leng - 1;
                                w_code2.front = 1;
                                write_analyse_code(w_line + 1, w_code2);
                                w_val = w_code2.val;
                                w_val_string = w_val.ToString("x2");
                            }
                            if (w_leng >= 3)
                            {
                                w_code3 = read_analyse_code(w_line + 2);
                                w_code3.type = TRACECODE.TYPE.OPR;
                                w_code3.leng = w_leng - 2;
                                w_code3.front = 2;
                                write_analyse_code(w_line + 2, w_code3);
                                w_val = w_code2.val + (w_code3.val << 8);
                                w_val_string = w_val.ToString("x4");
                            }
                            //opcode
                            string w_opname_org = w_op1.opname_out;
                            w_code1.operand = w_opname_org.PadRight(14) + w_op1.format;
                            w_code1.operand = w_code1.operand.Replace("VAL", "$" + w_val_string);
                            //operand_jsr
                            if ((w_opname_org == "JSR") || (w_opname_org == "BSR"))
                            {
                                w_code1.operand_jsr = true;
                            }
                            //comment
                            if (w_op1.addr == ADDRESSING_TYPE.Absolute)
                            {
                                w_code1.comment1 = analyses_comment(w_val);
                            }
                            //next
                            analyses_next(
                                ref w_code1,
                                w_line,
                                w_line + w_leng,
                                w_opname_org,
                                w_op1.addr,
                                w_val);
                            write_analyse_code(w_line, w_code1);
                            wchange += 1;
                        }
                    }
                    else
                    if (w_code1.type == TRACECODE.TYPE.NON)
                    {
                        w_leng = 0;
                        for (int i = 0; i < 16; i++)
                        {
                            if (w_line + i >= MEMSIZE) break;
                            TRACECODE w_cur = read_analyse_code(w_line + i);
                            if (w_cur.type != TRACECODE.TYPE.NON) break;
                            w_leng = i + 1;
                        }
                        for (int i = 0; i < w_leng; i++)
                        {
                            TRACECODE w_cur = read_analyse_code(w_line + i);
                            w_cur.leng = w_leng - i;
                            w_cur.front = i;
                            write_analyse_code(w_line + i, w_cur);
                        }
                    }
                    else
                    {
                        w_leng = w_code1.leng;
                    }
                    w_line += w_leng;
                } while(w_line < MEMSIZE) ;
            } while (wchange > 0);
        }

        public void analyses_next(
              ref TRACECODE in_code1
            , int in_line
            , int in_next
            , string in_opname_org
            , ADDRESSING_TYPE in_op1_addr
            , int in_val )
        {
            if ((in_opname_org != "JMP")
                && (in_opname_org != "RTI")
                && (in_opname_org != "RTS"))
            {
                TRACECODE w_next_code = read_analyse_code(in_next);
                if ((analyse_code_line_bank_match(in_line, in_next) == true) && (w_next_code.type == TRACECODE.TYPE.NON))
                {
                    w_next_code.type = TRACECODE.TYPE.CHK;
                    write_analyse_code(in_next, w_next_code);
                }
            }
            else
            {
                in_code1.ret_line = true;
            }
            if ((in_opname_org == "JSR") || (in_opname_org == "JMP"))
            {
                if (in_op1_addr == ADDRESSING_TYPE.Absolute)
                {
                    int w_jmp = analyse_code_addrewss2line(in_val);
                    TRACECODE w_jmp_code = read_analyse_code(w_jmp);
                    in_code1.jmp_offset = in_val - analyse_code_line2addrewss(in_line);
                    if ((analyse_code_line_bank_match(in_line, w_jmp) == true) && (w_jmp_code.type == TRACECODE.TYPE.NON))
                    {
                        w_jmp_code.type = TRACECODE.TYPE.CHK;
                        write_analyse_code(w_jmp, w_jmp_code);
                    }
                }
            }
            else
            if ((in_opname_org == "BCC") || (in_opname_org == "BCS")
                || (in_opname_org == "BEQ") || (in_opname_org == "BMI")
                || (in_opname_org == "BNE") || (in_opname_org == "BPL")
                || (in_opname_org == "BVC") || (in_opname_org == "BVS"))
            {
                int w_bra = in_line + in_code1.leng + (sbyte)in_val;
                TRACECODE w_bra_code = read_analyse_code(w_bra);
                in_code1.jmp_offset = 2 + (sbyte)in_val;
                write_analyse_code(in_line, in_code1);
                if ((analyse_code_line_bank_match(in_line, w_bra) == true) && (w_bra_code.type == TRACECODE.TYPE.NON))
                {
                    w_bra_code.type = TRACECODE.TYPE.CHK;
                    write_analyse_code(w_bra, w_bra_code);
                }
            }
        }
        public string analyses_comment(int in_comaddr)
        {
            string out_comment = "";
            for (int i = 0; i < g_op_comment.Count - 1; i++)
            {
                if ((g_op_comment[i].address <= in_comaddr) && (in_comaddr < g_op_comment[i + 1].address))
                {
                    out_comment = g_op_comment[i].comment;
                    break;
                }
            }
            return out_comment;
        }

        public int analyse_code_addrewss2line(int in_addr)
        {
            if (in_addr >= 0x8000)
            {
                in_addr -= 0x7800;
            }
            return in_addr;
        }
        public int analyse_code_line2addrewss(int in_line)
        {
            if (in_line >= 0x0800)
            {
                in_line += 0x7800;
            }
            return in_line;
        }
        public bool analyse_code_line_bank_match(int in_line1, int in_line2)
        {
            bool w_out = false;
            int w_bank1 = (in_line1 >= 0x0800) ? ((in_line1 - 0x0800) >> 13) : -1;
            int w_bank2 = (in_line2 >= 0x0800) ? ((in_line2 - 0x0800) >> 13) : -1;
            if (w_bank1 == w_bank2) w_out = true;
            return w_out;
        }
        public TRACECODE read_analyse_code(int in_line)
        {
            TRACECODE w_code = default;
            if (in_line >= 0x0800)
            {
                
                int w_bank = nes_main.g_nes_bus.cpu_get_bank(in_line + 0x7800);
                int w_addr = (in_line - 0x0800) & 0x1fff;
                w_code = g_analyse_code_rom[w_bank, w_addr];
            }
            else
            {
                w_code = g_analyse_code_ram[in_line & 0x07ff];
            }
            return w_code;
        }
        public void write_analyse_code(int in_line, TRACECODE in_tracecode)
        {
            if (in_line >= 0x0800)
            {
                g_analyse_code_rom[in_tracecode.bank, in_tracecode.s_addr] = in_tracecode;
            }
            else
            {
                g_analyse_code_ram[in_tracecode.s_addr] = in_tracecode;
            }
        }
        public void write_analyse_code_set_chk(int in_addr)
        {
            if (in_addr >= 0x0800)
            {
                int w_bank = nes_main.g_nes_bus.cpu_get_bank(in_addr);
                int w_addr = in_addr & 0x1fff;

                if ((g_analyse_code_rom[w_bank, w_addr].type == TRACECODE.TYPE.NON))
                {
                    g_analyse_code_rom[w_bank, w_addr].type = TRACECODE.TYPE.CHK;
                }
            }
            else
            {
                g_analyse_code_ram[in_addr].type = TRACECODE.TYPE.CHK;
            }
        }
    }
}
