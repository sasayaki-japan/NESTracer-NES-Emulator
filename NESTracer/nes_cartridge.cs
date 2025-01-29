using System.IO.Compression;

namespace NESTracer
{
    public class nes_cartridge
    {
        public byte[] g_file;                 
        public int g_file_size;               
        public int g_prg_rom_size;           
        public int g_chr_rom_size;           
        public int g_ignore_mirroring;         
        public int g_trainer;                
        public int g_batry;                  
        public int g_nametable_arrangement;      
        public int g_mapper_num;   
        
        public bool load(string in_romname)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(in_romname, FileMode.Open)))
                {
                    FileInfo fileInfo = new FileInfo(in_romname);
                    g_file_size = (int)fileInfo.Length;
                    g_file = new byte[g_file_size];
                    reader.Read(g_file, 0, g_file_size);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The file cannot be found", "error");
                return false;
            }
            if ((g_file[0] == 0x50) && (g_file[1] == 0x4b) && (g_file[2] == 0x03) && (g_file[3] == 0x04))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (FileStream fileStream = new FileStream(in_romname, FileMode.Open))
                    {
                        using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                using (Stream zipEntryStream = entry.Open())
                                {
                                    zipEntryStream.CopyTo(memoryStream);
                                }
                            }
                        }
                    }
                    byte[] uncompressedData = memoryStream.ToArray();


                    g_file_size = (int)memoryStream.Length;
                    g_file = new byte[g_file_size];
                    for(int i = 0; i < g_file_size; i++)
                    {
                        g_file[i] = uncompressedData[i];
                    }
                }
            }
            if ((g_file[0] != 'N') || (g_file[1] != 'E') || (g_file[2] != 'S') || (g_file[3] != 0x1A))
            {
                return false;
            }
            g_prg_rom_size = g_file[4] * 16384;
            g_chr_rom_size = g_file[5] * 8192;
            g_ignore_mirroring = (g_file[6] & 0x08) >> 3;
            g_trainer = (g_file[6] & 0x04) >> 2;
            g_trainer = (g_file[6] & 0x02) >> 1;
            g_nametable_arrangement = g_file[6] & 0x01;
            g_mapper_num = (g_file[7] & 0xf0) + (g_file[6] >> 4);
            return true;
        }
    }
}
