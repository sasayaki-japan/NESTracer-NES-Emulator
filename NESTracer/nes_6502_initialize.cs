using System.Diagnostics;
using static NESTracer.nes_6502;

namespace NESTracer
{
    internal partial class nes_6502
    {
        public void initialize()
        {
            g_ram = new byte[0x8000];
            g_stack = new STACKLIST[STACK_SIZE];

            g_oplist = new OPLIST[256];
            g_oplist[0xA9] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xA5] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xB5] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0xAD] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0xBD] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0xB9] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0xA1] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0xB1] = new OPLIST() { func = op_LDA, opname_out = "LDA", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0xA2] = new OPLIST() { func = op_LDX, opname_out = "LDX", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xA6] = new OPLIST() { func = op_LDX, opname_out = "LDX", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xB6] = new OPLIST() { func = op_LDX, opname_out = "LDX", format = "VAL,Y", addr = ADDRESSING_TYPE.ZeropageY, size = 2, clock = 4 };
            g_oplist[0xAE] = new OPLIST() { func = op_LDX, opname_out = "LDX", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0xBE] = new OPLIST() { func = op_LDX, opname_out = "LDX", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };

            g_oplist[0xA0] = new OPLIST() { func = op_LDY, opname_out = "LDY", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xA4] = new OPLIST() { func = op_LDY, opname_out = "LDY", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xB4] = new OPLIST() { func = op_LDY, opname_out = "LDY", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0xAC] = new OPLIST() { func = op_LDY, opname_out = "LDY", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0xBC] = new OPLIST() { func = op_LDY, opname_out = "LDY", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };

            g_oplist[0x85] = new OPLIST() { func = op_STA, opname_out = "STA", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x95] = new OPLIST() { func = op_STA, opname_out = "STA", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0x8D] = new OPLIST() { func = op_STA, opname_out = "STA", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0x9D] = new OPLIST() { func = op_STA, opname_out = "STA", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 5 };
            g_oplist[0x99] = new OPLIST() { func = op_STA, opname_out = "STA", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 5 };
            g_oplist[0x81] = new OPLIST() { func = op_STA, opname_out = "STA", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0x91] = new OPLIST() { func = op_STA, opname_out = "STA", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5 };

            g_oplist[0x86] = new OPLIST() { func = op_STX, opname_out = "STX", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x96] = new OPLIST() { func = op_STX, opname_out = "STX", format = "VAL,Y", addr = ADDRESSING_TYPE.ZeropageY, size = 2, clock = 4 };
            g_oplist[0x8E] = new OPLIST() { func = op_STX, opname_out = "STX", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };

            g_oplist[0x84] = new OPLIST() { func = op_STY, opname_out = "STY", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x94] = new OPLIST() { func = op_STY, opname_out = "STY", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0x8C] = new OPLIST() { func = op_STY, opname_out = "STY", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };

            g_oplist[0xAA] = new OPLIST() { func = op_TAX, opname_out = "TAX", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0xA8] = new OPLIST() { func = op_TAY, opname_out = "TAY", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0xBA] = new OPLIST() { func = op_TSX, opname_out = "TSX", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x8A] = new OPLIST() { func = op_TXA, opname_out = "TXA", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x9A] = new OPLIST() { func = op_TXS, opname_out = "TXS", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x98] = new OPLIST() { func = op_TYA, opname_out = "TYA", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };

            g_oplist[0x69] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0x65] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x75] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0x6D] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0x7D] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x79] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x61] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0x71] = new OPLIST() { func = op_ADC, opname_out = "ADC", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0x29] = new OPLIST() { func = op_AND, opname_out = "AND", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0x25] = new OPLIST() { func = op_AND, opname_out = "AND", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x35] = new OPLIST() { func = op_AND, opname_out = "AND", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0x2D] = new OPLIST() { func = op_AND, opname_out = "AND", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0x3D] = new OPLIST() { func = op_AND, opname_out = "AND", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x39] = new OPLIST() { func = op_AND, opname_out = "AND", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x21] = new OPLIST() { func = op_AND, opname_out = "AND", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0x31] = new OPLIST() { func = op_AND, opname_out = "AND", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0x0A] = new OPLIST() { func = op_ASL, opname_out = "ASL", format = "A", addr = ADDRESSING_TYPE.Accumulator, size = 1, clock = 2 };
            g_oplist[0x06] = new OPLIST() { func = op_ASL, opname_out = "ASL", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 5 };
            g_oplist[0x16] = new OPLIST() { func = op_ASL, opname_out = "ASL", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 6 };
            g_oplist[0x0E] = new OPLIST() { func = op_ASL, opname_out = "ASL", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0x1E] = new OPLIST() { func = op_ASL, opname_out = "ASL", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 7 };

            g_oplist[0x24] = new OPLIST() { func = op_BIT, opname_out = "BIT", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x2C] = new OPLIST() { func = op_BIT, opname_out = "BIT", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };

            g_oplist[0xC9] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xC5] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xD5] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0xCD] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0xDD] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0xD9] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0xC1] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0xD1] = new OPLIST() { func = op_CMP, opname_out = "CMP", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0xE0] = new OPLIST() { func = op_CPX, opname_out = "CPX", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xE4] = new OPLIST() { func = op_CPX, opname_out = "CPX", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xEC] = new OPLIST() { func = op_CPX, opname_out = "CPX", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };

            g_oplist[0xC0] = new OPLIST() { func = op_CPY, opname_out = "CPY", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xC4] = new OPLIST() { func = op_CPY, opname_out = "CPY", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xCC] = new OPLIST() { func = op_CPY, opname_out = "CPY", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };

            g_oplist[0xC6] = new OPLIST() { func = op_DEC, opname_out = "DEC", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 5 };
            g_oplist[0xD6] = new OPLIST() { func = op_DEC, opname_out = "DEC", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 6 };
            g_oplist[0xCE] = new OPLIST() { func = op_DEC, opname_out = "DEC", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0xDE] = new OPLIST() { func = op_DEC, opname_out = "DEC", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 7 };

            g_oplist[0xCA] = new OPLIST() { func = op_DEX, opname_out = "DEX", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x88] = new OPLIST() { func = op_DEY, opname_out = "DEY", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };

            g_oplist[0x49] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0x45] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x55] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0x4D] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0x5D] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x59] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x41] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0x51] = new OPLIST() { func = op_EOR, opname_out = "EOR", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0xE6] = new OPLIST() { func = op_INC, opname_out = "INC", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 5 };
            g_oplist[0xF6] = new OPLIST() { func = op_INC, opname_out = "INC", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 6 };
            g_oplist[0xEE] = new OPLIST() { func = op_INC, opname_out = "INC", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0xFE] = new OPLIST() { func = op_INC, opname_out = "INC", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 7 };

            g_oplist[0xE8] = new OPLIST() { func = op_INX, opname_out = "INX", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0xC8] = new OPLIST() { func = op_INY, opname_out = "INY", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };

            g_oplist[0x4A] = new OPLIST() { func = op_LSR, opname_out = "LSR", format = "A", addr = ADDRESSING_TYPE.Accumulator, size = 1, clock = 2 };
            g_oplist[0x46] = new OPLIST() { func = op_LSR, opname_out = "LSR", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 5 };
            g_oplist[0x56] = new OPLIST() { func = op_LSR, opname_out = "LSR", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 6 };
            g_oplist[0x4E] = new OPLIST() { func = op_LSR, opname_out = "LSR", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0x5E] = new OPLIST() { func = op_LSR, opname_out = "LSR", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 7 };

            g_oplist[0x09] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0x05] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0x15] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0x0D] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0x1D] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x19] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0x01] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0x11] = new OPLIST() { func = op_ORA, opname_out = "ORA", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0x2A] = new OPLIST() { func = op_ROL, opname_out = "ROL", format = "A", addr = ADDRESSING_TYPE.Accumulator, size = 1, clock = 2 };
            g_oplist[0x26] = new OPLIST() { func = op_ROL, opname_out = "ROL", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 5 };
            g_oplist[0x36] = new OPLIST() { func = op_ROL, opname_out = "ROL", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 6 };
            g_oplist[0x2E] = new OPLIST() { func = op_ROL, opname_out = "ROL", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0x3E] = new OPLIST() { func = op_ROL, opname_out = "ROL", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 7 };

            g_oplist[0x6A] = new OPLIST() { func = op_ROR, opname_out = "ROR", format = "A", addr = ADDRESSING_TYPE.Accumulator, size = 1, clock = 2 };
            g_oplist[0x66] = new OPLIST() { func = op_ROR, opname_out = "ROR", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 5 };
            g_oplist[0x76] = new OPLIST() { func = op_ROR, opname_out = "ROR", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 6 };
            g_oplist[0x6E] = new OPLIST() { func = op_ROR, opname_out = "ROR", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0x7E] = new OPLIST() { func = op_ROR, opname_out = "ROR", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 7 };

            g_oplist[0xE9] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "#VAL", addr = ADDRESSING_TYPE.Immediate, size = 2, clock = 2 };
            g_oplist[0xE5] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "VAL", addr = ADDRESSING_TYPE.Zeropage, size = 2, clock = 3 };
            g_oplist[0xF5] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "VAL,X", addr = ADDRESSING_TYPE.ZeropageX, size = 2, clock = 4 };
            g_oplist[0xED] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 4 };
            g_oplist[0xFD] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "VAL,X", addr = ADDRESSING_TYPE.AbsoluteX, size = 3, clock = 4, page_boundary = true };
            g_oplist[0xF9] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "VAL,Y", addr = ADDRESSING_TYPE.AbsoluteY, size = 3, clock = 4, page_boundary = true };
            g_oplist[0xE1] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "(VAL,X)", addr = ADDRESSING_TYPE.IndirectX, size = 2, clock = 6 };
            g_oplist[0xF1] = new OPLIST() { func = op_SBC, opname_out = "SBC", format = "(VAL),Y", addr = ADDRESSING_TYPE.IndirectY, size = 2, clock = 5, page_boundary = true };

            g_oplist[0x48] = new OPLIST() { func = op_PHA, opname_out = "PHA", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 3 };
            g_oplist[0x08] = new OPLIST() { func = op_PHP, opname_out = "PHP", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 3 };
            g_oplist[0x68] = new OPLIST() { func = op_PLA, opname_out = "PLA", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 4 };
            g_oplist[0x28] = new OPLIST() { func = op_PLP, opname_out = "PLP", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 4 };

            g_oplist[0x4C] = new OPLIST() { func = op_JMP, opname_out = "JMP", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 3 };
            g_oplist[0x6C] = new OPLIST() { func = op_JMP, opname_out = "JMP", format = "(VAL)", addr = ADDRESSING_TYPE.Indirect, size = 3, clock = 5 };

            g_oplist[0x20] = new OPLIST() { func = op_JSR, opname_out = "JSR", format = "VAL", addr = ADDRESSING_TYPE.Absolute, size = 3, clock = 6 };
            g_oplist[0x60] = new OPLIST() { func = op_RTS, opname_out = "RTS", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 6 };
            g_oplist[0x40] = new OPLIST() { func = op_RTI, opname_out = "RTI", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 6 };

            g_oplist[0x90] = new OPLIST() { func = op_BCC, opname_out = "BCC", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0xB0] = new OPLIST() { func = op_BCS, opname_out = "BCS", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0xF0] = new OPLIST() { func = op_BEQ, opname_out = "BEQ", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0x30] = new OPLIST() { func = op_BMI, opname_out = "BMI", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0xD0] = new OPLIST() { func = op_BNE, opname_out = "BNE", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0x10] = new OPLIST() { func = op_BPL, opname_out = "BPL", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0x50] = new OPLIST() { func = op_BVC, opname_out = "BVC", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };
            g_oplist[0x70] = new OPLIST() { func = op_BVS, opname_out = "BVS", format = "VAL", addr = ADDRESSING_TYPE.Relative, size = 2, clock = 2 };

            g_oplist[0x18] = new OPLIST() { func = op_CLC, opname_out = "CLC", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0xD8] = new OPLIST() { func = op_CLD, opname_out = "CLD", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x58] = new OPLIST() { func = op_CLI, opname_out = "CLI", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0xB8] = new OPLIST() { func = op_CLV, opname_out = "CLV", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x38] = new OPLIST() { func = op_SEC, opname_out = "SEC", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0xF8] = new OPLIST() { func = op_SED, opname_out = "SED", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
            g_oplist[0x78] = new OPLIST() { func = op_SEI, opname_out = "SEI", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };

            g_oplist[0x00] = new OPLIST() { func = op_BRK, opname_out = "BRK", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 7 };
            g_oplist[0xEA] = new OPLIST() { func = op_NOP, opname_out = "NOP", format = "", addr = ADDRESSING_TYPE.Implied, size = 1, clock = 2 };
        }
        public void setting()
        {
            g_rom = new byte[nes_main.g_nes_mapper_control.g_prg_bank_num, nes_mapper_control.PRG_ROM_BANK_SIZE];

            for (int wbank = 0; wbank < nes_main.g_nes_mapper_control.g_prg_bank_num; wbank++)
            {
                for (int i = 0; i < nes_mapper_control.PRG_ROM_BANK_SIZE; i++)
                {
                    nes_main.g_nes_6502.g_rom[wbank, i]
                        = nes_main.g_nes_cartridge.g_file[16 + (wbank * nes_mapper_control.PRG_ROM_BANK_SIZE) + i];
                }
            }
            nes_main.g_nes_mapper_control.prg_rom_setting();
        }
        public void reset()
        {
            g_initial_PC = nes_main.g_nes_bus.read2(0xFFFC);
            g_reg_PC = g_initial_PC;
            g_reg_A = 0;
            g_reg_X = 0;
            g_reg_Y = 0;
            g_reg_S = 253;
            g_reg_N = false;
            g_reg_V = false;
            g_reg_R = true;
            g_reg_B = false;
            g_reg_D = false;
            g_reg_I = true;
            g_reg_Z = false;
            g_reg_C = false;

            interrupt_NMI = false;
            interrupt_RESET = false;
            interrupt_IRQ = false;
            interrupt_NMI_act = false;
            interrupt_IRQ_act = false;

            g_clock_total = 0;
            g_clock_now = 0;

        }
    }
}
