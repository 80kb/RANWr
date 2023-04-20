﻿using gablibela;
using gablibela.arc;
using gablibela.cmpr;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ranwr
{
    public class arc
    {
        public static readonly string Version = "v0.2";
        public static readonly string ArcVersion = "v0.1";

        public static readonly string Title = $"RANWr: RANWr Ain't NintendoWare {Version} || Archive CLI Tool {ArcVersion}";

        public static int CompressionLevel = 10;

        public static void Main(string[] args)
        {
            if (args.Length == 0) noCommand();
            if(args.Length > 0)
            {
                if(args.Length > 2)
                {
                    switch (args[0])
                    {
                        case "c":
                        case "compress":
                            compress(args[1], args[2]);
                            break;
                        case "d":
                        case "decompress":
                            decompress(args[1], args[2]);
                            break;
                    }
                }
                else if (args.Length > 1)
                {
                    switch (args[0])
                    {
                        case "c":
                        case "compress":
                            compress(args[1]);
                            break;
                        case "d":
                        case "decompress":
                            decompress(args[1]);
                            break;
                        case "list":
                            listFiles(args[1]);
                            break;
                    }
                    
                }
            }
        }

        public static void listFiles(string file)
        {
            byte[] original = File.ReadAllBytes(file);
            byte[] workFile = original;

            var signature = BitConverter.ToUInt32(workFile.Take(4).Reverse().ToArray());
            if (signature == YAZ0.SignatureHex) workFile = YAZ0.Decode(original);
            signature = BitConverter.ToUInt32(workFile.Take(4).Reverse().ToArray());
            if(signature == DARCH.Signature)
            {
                DARCH darch = new DARCH(workFile, Path.GetFileName(file));
                Console.WriteLine($"{Title}\n");
                darch.PrintArchive();
            }
        }

        public static void testing()
        {
            byte[] data = File.ReadAllBytes(@"C:\Users\Gabi\Pictures\RANWr\koopa_course.arc");
            DARCH darch = new DARCH(data);

            Console.WriteLine(Title);
            Console.WriteLine($"> Type 'ranwr-arc -h' for help.\n");
            darch.PrintArchive();
        }

        public static void noCommand()
        {
            Console.WriteLine(Title);
            Console.WriteLine($"> Type 'ranwr-arc -h' for help.");
        }
 
        public static void compress(string file, string output = null)
        {
            if (output == null) output = Path.GetFileNameWithoutExtension(file) + ".szs";

            Console.WriteLine(Title);
            Console.WriteLine($"Compressing: {Path.GetFileName(file)} to {output}");

            byte[] original = File.ReadAllBytes(file);
            var signature = BitConverter.ToUInt32(original.Take(4).Reverse().ToArray());
            if (signature == YAZ0.SignatureHex) return;
            byte[] compressed = YAZ0.Compress(original, (byte)CompressionLevel);
            
            File.WriteAllBytes(output, compressed);
        }

        public static void decompress(string file, string output = null)
        {
            if (output == null) output = Path.GetFileNameWithoutExtension(file) + ".arc";

            Console.WriteLine(Title);
            Console.WriteLine($"Decompressing: {Path.GetFileName(file)} to {output}");

            byte[] original = File.ReadAllBytes(file);
            var signature = BitConverter.ToUInt32(original.Take(4).Reverse().ToArray());
            if (signature != YAZ0.SignatureHex) return;
            byte[] decompressed = YAZ0.Decode(original);
            File.WriteAllBytes(output, decompressed);
        }
    }
    
}