using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFolders
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
                    file.Name),true);
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
            // System.IO.File.Copy(@"C:\Users\gvozd\Desktop\1188453866\730", @"C:\Users\gvozd\Desktop\1158606011", true);
            //File.Copy(@"C:\Users\gvozd\Desktop\1188453866\730", Path.Combine(@"C:\Users\gvozd\Desktop\1158606011", Path.GetFileName(@"C:\Users\gvozd\Desktop\1188453866\730")), false);

            DirectoryInfo sourceDir = new DirectoryInfo(@"C:\Users\gvozd\Desktop\1188453866");
            DirectoryInfo destinationDir = new DirectoryInfo(@"C:\Users\gvozd\Desktop\1158606011");

            CopyDirectory(sourceDir, destinationDir);

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
