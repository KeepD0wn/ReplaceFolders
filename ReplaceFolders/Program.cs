using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ReplaceFolders
{
    class Program
    {
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName,
                    file.Name), true);
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic"))
                {
                    string key = "";
                    using (StreamReader sr = new StreamReader($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic"))
                    {
                        key = sr.ReadToEnd();
                    }
                    key = key.Replace("\r\n", "");

                    if (PcInfo.GetCurrentPCInfo() == key)
                    {
                        string mainPath = @"C:\Program Files (x86)\Steam\userdata";
                        DirectoryInfo dir = new DirectoryInfo(mainPath);
                        int g = 0;
                        foreach (var item in dir.GetDirectories())
                        {
                            DirectoryInfo sourceDir = new DirectoryInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\reference");
                            DirectoryInfo destinationDir = new DirectoryInfo($@"{mainPath}\{item.Name}");
                            CopyDirectory(sourceDir, destinationDir);

                            g += 1;
                            Console.WriteLine("Folders replaced: " + g);
                        }
                        Console.WriteLine("Done");
                    }
                    else
                    {
                        Console.WriteLine("[SYSTEM] License not found");
                        Thread.Sleep(5000);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("[SYSTEM] License not found");
                    Thread.Sleep(5000);
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
            Console.ReadLine();
        }
    }
}
