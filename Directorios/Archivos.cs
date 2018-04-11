using Directorios.Eventos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using static Directorios.Eventos.ErroresDatos;

namespace Directorios
{
    public class Archivos
    {
        private static ErroresDatos erroresDatos = new ErroresDatos();
        public Archivos()
        {
            erroresDatos.ThresholdReached += c_ThresholdReached;
        }
        static void c_ThresholdReached(object sender, EventArgs e)
        {

        }
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            if (file1 == file2)
            {
                return true;
            }

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                return false;
            }

            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
        public Boolean comparar(string f1, string f2)
        {
            if (!File.Exists(f2))
            {
                return false;
            }
            // Creación de un objeto HashAlgorithm:
            using (HashAlgorithm hash = HashAlgorithm.Create())
            {
                using (FileStream fs1 = new FileStream(f1, FileMode.Open),
                                  fs2 = new FileStream(f2, FileMode.Open))
                {
                    if (fs2 == null || fs2.Length < 1)

                    {
                        return false;
                    }
                    // Cálculo de hash para los dos archivos:
                    byte[] hashBytes1 = hash.ComputeHash(fs1);
                    byte[] hashBytes2 = hash.ComputeHash(fs2);

                    // Comparación de los dos códigos hash:
                    if (BitConverter.ToString(hashBytes1) == BitConverter.ToString(hashBytes2))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        #region Archivos duplicados
        static void QueryDuplicates(string startFolder)
        {
            if (string.IsNullOrEmpty(startFolder))
            {
                erroresDatos.AgregarDatos(startFolder, string.Empty);
                throw new ArgumentException("No ha especificado las rutabase");
            }

            // Take a snapshot of the file system.  
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            // used in WriteLine to keep the lines shorter  
            int charsToSkip = startFolder.Length;

            // var can be used for convenience with groups.  
            var queryDupNames =
                from file in fileList
                group file.FullName.Substring(charsToSkip) by file.Name into fileGroup
                where fileGroup.Count() > 1
                select fileGroup;

            // Pass the query to a method that will  
            // output one page at a time.  
            PageOutput<string, string>(queryDupNames);
        }
        private static void PageOutput<K, V>(IEnumerable<System.Linq.IGrouping<K, V>> groupByExtList)
        {
            // Flag to break out of paging loop.  
            bool goAgain = true;

            // "3" = 1 line for extension + 1 for "Press any key" + 1 for input cursor.  
            int numLines = Console.WindowHeight - 3;

            // Iterate through the outer collection of groups.  
            foreach (var filegroup in groupByExtList)
            {
                // Start a new extension at the top of a page.  
                int currentLine = 0;

                // Output only as many lines of the current group as will fit in the window.  
                do
                {
                    Console.Clear();
                    Console.WriteLine("Filename = {0}", filegroup.Key.ToString() == String.Empty ? "[none]" : filegroup.Key.ToString());

                    // Get 'numLines' number of items starting at number 'currentLine'.  
                    var resultPage = filegroup.Skip(currentLine).Take(numLines);

                    //Execute the resultPage query  
                    foreach (var fileName in resultPage)
                    {
                        Console.WriteLine("\t{0}", fileName);
                    }

                    // Increment the line counter.  
                    currentLine += numLines;

                    // Give the user a chance to escape.  
                    Console.WriteLine("Press any key to continue or the 'End' key to break...");
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.End)
                    {
                        goAgain = false;
                        break;
                    }
                } while (currentLine < filegroup.Count());

                if (goAgain == false)
                    break;
            }

        }
        #endregion
    }
}
