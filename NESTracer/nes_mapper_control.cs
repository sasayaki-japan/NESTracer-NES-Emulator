namespace NESTracer
{
    internal class nes_mapper_control
    {
        public object[] g_mapper;
        public int g_mapper_num;

        public int[] g_prg_bank_map;
        public int g_prg_bank_num;
        public int[] g_chr_bank_map;
        public int g_chr_bank_num;
        public byte[] g_swap_buffer;

        public const int PRG_ROM_BANK_SIZE = 8192;
        public const int PRG_ROM_MAP_NUM = 4;
        public const int CHR_ROM_BANK_SIZE = 1024;
        public const int CHR_ROM_MAP_NUM = 8;
        //----------------------------------------------------------------
        public nes_mapper_control()
        {
            initialize();
        }
        public void initialize()
        {
            g_mapper = new object[] {
                new mapper0(), new mapper1(), new mapper2(), new mapper3(), new mapper4(),
                new mapper0(), new mapper0(), new mapper7(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper21(), new mapper21(), new mapper21(), new mapper0(),
                new mapper21(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper0(), new mapper0(),
                new mapper0(), new mapper0(), new mapper0(), new mapper68(), new mapper0(),
            };
            g_prg_bank_map = new int[PRG_ROM_MAP_NUM];
            g_chr_bank_map = new int[CHR_ROM_MAP_NUM];
            g_swap_buffer = new byte[PRG_ROM_BANK_SIZE];
        }
        public void setting()
        {
            g_mapper_num = nes_main.g_nes_cartridge.g_mapper_num;
            ((dynamic)g_mapper[g_mapper_num]).initialize();
            g_prg_bank_num = nes_main.g_nes_cartridge.g_prg_rom_size / PRG_ROM_BANK_SIZE;
            g_chr_bank_num = nes_main.g_nes_cartridge.g_chr_rom_size / CHR_ROM_BANK_SIZE;
            if (g_chr_bank_num < 8) g_chr_bank_num = 8;        //8KB未満なら不足はCHR-RAM扱いとする
        }
        public void prg_rom_setting()
        {
            ((dynamic)g_mapper[g_mapper_num]).prg_rom_setting();
        }
        public void chr_rom_setting()
        {
            ((dynamic)g_mapper[g_mapper_num]).chr_rom_setting();
        }
        //----------------------------------------------------------------
        //mapping
        //----------------------------------------------------------------
        public void cpu_write1(int in_address, byte in_val)
        {
            ((dynamic)g_mapper[g_mapper_num]).cpu_write1(in_address, in_val);
        }
        //----------------------------------------------------------------
        //IRQ
        //----------------------------------------------------------------
        public void cpu_irq_clock(int in_clock)
        {
            ((dynamic)g_mapper[g_mapper_num]).cpu_irq_clock(in_clock);
        }
        public void ppu_irq_line()
        {
            ((dynamic)g_mapper[g_mapper_num]).ppu_irq_line();
        }
        //----------------------------------------------------------------
        //sub
        //----------------------------------------------------------------
        public void set_prg_rom_bank_8k(int in_map_num, int in_bank_num)
        {
            int w_bank_num = in_bank_num % g_prg_bank_num;
            g_prg_bank_map[in_map_num] = w_bank_num;
            Buffer.BlockCopy(nes_main.g_nes_cartridge.g_file
                , nes_main.g_nes_cartridge.g_prg_rom_offset + (w_bank_num * PRG_ROM_BANK_SIZE)
                , nes_main.g_nes_6502.g_ram
                , 0x8000 + (in_map_num * PRG_ROM_BANK_SIZE)
                , PRG_ROM_BANK_SIZE);
        }
        public void swap_prg_rom_bank_8k(int in_map_num1, int in_map_num2)
        {
            (g_prg_bank_map[in_map_num1], g_prg_bank_map[in_map_num2]) = (g_prg_bank_map[in_map_num2], g_prg_bank_map[in_map_num1]);
            Buffer.BlockCopy(nes_main.g_nes_6502.g_ram
                , 0x8000 + (in_map_num1 * PRG_ROM_BANK_SIZE)
                , g_swap_buffer
                , 0
                , PRG_ROM_BANK_SIZE);
            Buffer.BlockCopy(nes_main.g_nes_6502.g_ram
                , 0x8000 + (in_map_num2 * PRG_ROM_BANK_SIZE)
                , nes_main.g_nes_6502.g_ram
                , 0x8000 + (in_map_num1 * PRG_ROM_BANK_SIZE)
                , PRG_ROM_BANK_SIZE);
            Buffer.BlockCopy(g_swap_buffer
                , 0
                , nes_main.g_nes_6502.g_ram
                , 0x8000 + (in_map_num2 * PRG_ROM_BANK_SIZE)
                , PRG_ROM_BANK_SIZE);
        }
        public void set_prg_rom_bank_16k(int in_map_num, int in_bank_num)
        {
            int w_bank_num = in_bank_num % g_prg_bank_num;
            set_prg_rom_bank_8k(in_map_num, w_bank_num);
            set_prg_rom_bank_8k(in_map_num + 1, w_bank_num + 1);
        }
        public void set_prg_rom_bank_32k(int in_map_num, int in_bank_num)
        {
            int w_bank_num = in_bank_num % g_prg_bank_num;
            set_prg_rom_bank_8k(in_map_num, w_bank_num);
            set_prg_rom_bank_8k(in_map_num + 1, w_bank_num + 1);
            set_prg_rom_bank_8k(in_map_num + 2, w_bank_num + 2);
            set_prg_rom_bank_8k(in_map_num + 3, w_bank_num + 3);
        }
        public void set_chr_rom_bank_1k(int in_map_num, int in_bank_num)
        {
            int w_bank_num = in_bank_num % g_chr_bank_num;
            g_chr_bank_map[in_map_num] = w_bank_num;

            if (nes_main.g_nes_cartridge.g_chr_rom_size != 0)
            {

                Buffer.BlockCopy(nes_main.g_nes_cartridge.g_file
                    , nes_main.g_nes_cartridge.g_chr_rom_offset
                         + (w_bank_num * CHR_ROM_BANK_SIZE)
                    , nes_main.g_nes_ppu.g_ram
                    , (in_map_num * CHR_ROM_BANK_SIZE)
                    , 1024);
            }
        }
        public void set_chr_rom_bank_4k(int in_map_num, int in_bank_num)
        {
            int w_bank_num = in_bank_num % g_chr_bank_num;
            set_chr_rom_bank_1k(in_map_num, w_bank_num);
            set_chr_rom_bank_1k(in_map_num + 1, w_bank_num + 1);
            set_chr_rom_bank_1k(in_map_num + 2, w_bank_num + 2);
            set_chr_rom_bank_1k(in_map_num + 3, w_bank_num + 3);
        }
        public void set_chr_rom_bank_8k(int in_map_num, int in_bank_num)
        {
            int w_bank_num = in_bank_num % g_chr_bank_num;
            set_chr_rom_bank_1k(in_map_num, w_bank_num);
            set_chr_rom_bank_1k(in_map_num + 1, w_bank_num + 1);
            set_chr_rom_bank_1k(in_map_num + 2, w_bank_num + 2);
            set_chr_rom_bank_1k(in_map_num + 3, w_bank_num + 3);
            set_chr_rom_bank_1k(in_map_num + 4, w_bank_num + 4);
            set_chr_rom_bank_1k(in_map_num + 5, w_bank_num + 5);
            set_chr_rom_bank_1k(in_map_num + 6, w_bank_num + 6);
            set_chr_rom_bank_1k(in_map_num + 7, w_bank_num + 7);
        }
    }
}
