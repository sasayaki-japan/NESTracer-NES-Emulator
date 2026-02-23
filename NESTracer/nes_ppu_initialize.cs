using System.Drawing.Imaging;

namespace NESTracer
{
    internal partial class nes_ppu
    {
        public Color[] PALLET_MONO = {
            Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),
            Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),
            Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),
            Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),Color.FromArgb(0x75,0x75,0x75),

            Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),
            Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),
            Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),
            Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),Color.FromArgb(0xbc,0xbc,0xbc),

            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),

            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
            Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),Color.FromArgb(0xff,0xff,0xff),
        };
        public Color[] PALLET_BASE = {
            Color.FromArgb(0x75,0x75,0x75), Color.FromArgb(0x27,0x1b,0x8f), Color.FromArgb(0x00,0x00,0xab), Color.FromArgb(0x47,0x00,0x9f),
            Color.FromArgb(0x8f,0x00,0x77), Color.FromArgb(0xab,0x00,0x13), Color.FromArgb(0xa7,0x00,0x00), Color.FromArgb(0x7f,0x0b,0x00),
            Color.FromArgb(0x43,0x2f,0x00), Color.FromArgb(0x00,0x47,0x00), Color.FromArgb(0x00,0x51,0x00), Color.FromArgb(0x00,0x3f,0x17),
            Color.FromArgb(0x1b,0x3f,0x5f), Color.FromArgb(0x00,0x00,0x00), Color.FromArgb(0x00,0x00,0x00), Color.FromArgb(0x00,0x00,0x00),

            Color.FromArgb(0xbc,0xbc,0xbc), Color.FromArgb(0x00,0x73,0xef), Color.FromArgb(0x23,0x3b,0xef), Color.FromArgb(0x83,0x00,0xf3),
            Color.FromArgb(0xbf,0x00,0xbf), Color.FromArgb(0xe7,0x00,0x5b), Color.FromArgb(0xdb,0x2b,0x00), Color.FromArgb(0xcb,0x4f,0x0f),
            Color.FromArgb(0x8b,0x73,0x00), Color.FromArgb(0x00,0x97,0x00), Color.FromArgb(0x00,0xab,0x00), Color.FromArgb(0x00,0x93,0x3b),
            Color.FromArgb(0x00,0x83,0x8b), Color.FromArgb(0x00,0x00,0x00), Color.FromArgb(0x00,0x00,0x00), Color.FromArgb(0x00,0x00,0x00),

            Color.FromArgb(0xff,0xff,0xff), Color.FromArgb(0x3f,0xbf,0xff), Color.FromArgb(0x5f,0x73,0xff), Color.FromArgb(0xa7,0x8b,0xfd),
            Color.FromArgb(0xf7,0x7b,0xff), Color.FromArgb(0xff,0x77,0xb7), Color.FromArgb(0xff,0x77,0x63), Color.FromArgb(0xff,0x9b,0x3b),
            Color.FromArgb(0xf3,0xbf,0x3f), Color.FromArgb(0x83,0xd3,0x13), Color.FromArgb(0x4f,0xdf,0x4b), Color.FromArgb(0x58,0xf8,0x98),
            Color.FromArgb(0x00,0xeb,0xdb), Color.FromArgb(0x75,0x75,0x75), Color.FromArgb(0x00,0x00,0x00), Color.FromArgb(0x00,0x00,0x00),

            Color.FromArgb(0xff,0xff,0xff), Color.FromArgb(0xab,0xe7,0xff), Color.FromArgb(0xc7,0xd7,0xff), Color.FromArgb(0xd7,0xcb,0xff),
            Color.FromArgb(0xff,0xc7,0xff), Color.FromArgb(0xff,0xc7,0xdb), Color.FromArgb(0xff,0xdb,0xab), Color.FromArgb(0xff,0xdb,0xab),
            Color.FromArgb(0xff,0xe7,0xa3), Color.FromArgb(0xe3,0xff,0xa3), Color.FromArgb(0xab,0xf3,0xbf), Color.FromArgb(0xb3,0xff,0xcf),
            Color.FromArgb(0x9f,0xff,0xf3), Color.FromArgb(0xbc,0xbc,0xbc), Color.FromArgb(0x00,0x00,0x00), Color.FromArgb(0x00,0x00,0x00),
        };
        public nes_ppu()
        {
            g_ram = new byte[0x4000];
            g_nametable_bank = new int[4];
            g_memory_oam = new byte[256];
            g_attrtable = new int[2, 32, 32];
            g_color = new uint[64];

            g_game_cmap = new uint[256];
            g_game_cmap_name1 = new uint[256];
            g_game_cmap_name2 = new uint[256];
            g_game_screen = new uint[256 * 240];
            g_game_screen_name = new uint[2 * 256 * 240];

            make_palette();
        }
        public void setting()
        {
            g_rom = new byte[nes_main.g_nes_mapper_control.g_chr_bank_num, nes_mapper_control.CHR_ROM_BANK_SIZE];
            if (nes_main.g_nes_cartridge.g_chr_rom_size > 0)
            {
                for (int wbank = 0; wbank < nes_main.g_nes_mapper_control.g_chr_bank_num; wbank++)
                {
                    for (int i = 0; i < nes_mapper_control.CHR_ROM_BANK_SIZE; i++)
                    {
                        nes_main.g_nes_ppu.g_rom[wbank, i]
                            = nes_main.g_nes_cartridge.g_file[16 + nes_main.g_nes_cartridge.g_prg_rom_size
                                                        + (wbank * nes_mapper_control.CHR_ROM_BANK_SIZE) + i];
                    }
                }
            }
            nes_main.g_nes_mapper_control.chr_rom_setting();

            g_scanline = 0;
            g_event_wbuf_write = 0;
            g_event_wbuf_renderer = 1;
            g_io_2003_oam_offset = 0;

            g_nametable_arrangement = nes_main.g_nes_cartridge.g_nametable_arrangement + 2;
            if (nes_main.g_nes_cartridge.g_ignore_mirroring == 1)
            {
                g_nametable_bank[0] = 0x2000;
                g_nametable_bank[1] = 0x2000;
                g_nametable_bank[2] = 0x2000;
                g_nametable_bank[3] = 0x2000;
            }
            else
            {
                if (g_nametable_arrangement == 2)
                {
                    g_nametable_bank[0] = 0x2000;
                    g_nametable_bank[1] = 0x2000;
                    g_nametable_bank[2] = 0x2400;
                    g_nametable_bank[3] = 0x2400;
                }
                else
                {
                    g_nametable_bank[0] = 0x2000;
                    g_nametable_bank[1] = 0x2400;
                    g_nametable_bank[2] = 0x2000;
                    g_nametable_bank[3] = 0x2400;
                }
            }

            g_chr_data = new byte[nes_main.g_nes_mapper_control.g_chr_bank_num, 64, 64];
            if(nes_main.g_nes_cartridge.g_chr_rom_size != 0)
            {
                for (int wbank = 0; wbank < nes_main.g_nes_mapper_control.g_chr_bank_num; wbank++)
                {
                    for (int wchr = 0; wchr < 64; wchr++)
                    {
                        for (int dy = 0; dy < 8; dy++)
                        {
                            int wcur = wchr * 16 + dy;
                            int wchr1 = g_rom[wbank, wcur];
                            int wchr2 = g_rom[wbank, wcur + 8];
                            int wcheck_bit = 0x80;
                            for (int dx = 0; dx < 8; dx++)
                            {
                                int wd1 = (wchr1 & wcheck_bit) != 0 ? 1 : 0;
                                int wd2 = (wchr2 & wcheck_bit) != 0 ? 1 : 0;
                                g_chr_data[wbank, wchr, dx + dy * 8] = (byte)(wd2 * 2 + wd1);
                                wcheck_bit = wcheck_bit >> 1;
                            }
                        }
                    }
                }

            }
        }
    }
}
