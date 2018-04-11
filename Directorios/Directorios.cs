using Directorios.Eventos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Directorios.Eventos.ErroresDatos;

namespace Directorios
{
    /// <summary>
    /// directorios
    /// </summary>
    /// <seealso cref="System.IObserver{Directorios.Errores}" />
    public class Directorios: IObserver<Errores>
    {
        private static List<IObserver<Errores>> observers;
        public static string Ruta { get; set; }
        public static object objlock = new object();       
        static Errores errores = new Errores();
        static int _value;
        private const int MAX_RECURSIVE_CALLS = 1000;
        static int ctr = 0;
        private static ErroresDatos erroresDatos = new ErroresDatos();
        static Directorios()
        {
            observers = new List<IObserver<Errores>>();
            erroresDatos.ThresholdReached += c_ThresholdReached;
        }
        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            using (StreamWriter sw = File.CreateText(Ruta))
            {
                sw.WriteLine(e.Archivo);
                sw.WriteLine(e.ErrorDescription);
            }
        }
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
            {
                if (String.IsNullOrEmpty(sourceDirName))
                {
                    throw new ArgumentException("directoriovacio");
                }
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    lock (objlock)
                    {
                        Directory.CreateDirectory(destDirName);
                    }
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                var eve = new UtilEvents();
                if (files != null && files.Any())
                {
                    Archivos Archivos = new Archivos();
                    //File.Exists(destDirName + @"\" + file.Name) 
                    Parallel.ForEach(files, file =>
                    {
                        if (!Archivos.comparar(file.FullName, destDirName + @"\" + file.Name))
                        {
                            try
                            {
                                FrmCopy._tool.Progres(Interlocked.Add(ref _value, 1));
                                CopyFile(file.FullName, destDirName);
                                FrmCopy._tool.Progres(Interlocked.Increment(ref _value));
                            }
                            catch (Exception ex)
                            {
                                errores.ErroresData.Add(ex.GetBaseException().Message);
                            }

                        }
                    });
                }
                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    if (dirs != null && dirs.Any())
                    {
                        Parallel.ForEach(dirs, subdir =>
                        {
                            try
                            {
                                string temppath = Path.Combine(destDirName, subdir.Name);
                                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                            }
                            catch (Exception ex)
                            {
                                erroresDatos.AgregarDatos(subdir.FullName, ex.GetBaseException().Message);
                                errores.ErroresData.Add(ex.GetBaseException().Message);
                            }

                        });
                    }
                }

            }
            catch (Exception ex)
            {

                erroresDatos.AgregarDatos("", ex.GetBaseException().Message);
            }

        }
        private async static void CopyFile(string filename, string EndDirectory)
        {
            using (FileStream SourceStream = File.Open(filename, FileMode.Open))
            using (FileStream DestinationStream = File.Create(EndDirectory + filename.Substring(filename.LastIndexOf('\\'))))
            {
                await SourceStream.CopyToAsync(DestinationStream);
            }
        }
        public static ConcurrentBag<string> getError()
        {
            return errores.ErroresData;
        }
        private List<string> GetFiles(string path, string pattern)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                    files.AddRange(GetFiles(directory, pattern));
            }
            catch (UnauthorizedAccessException) { }

            return files;
        }
        public static List<string> GetAllFilesFromFolder(string root, bool searchSubfolders)
        {
            Queue<string> folders = new Queue<string>();
            List<string> files = new List<string>();
            folders.Enqueue(root);
            while (folders.Count != 0)
            {
                string currentFolder = folders.Dequeue();
                try
                {
                    string[] filesInCurrent = System.IO.Directory.GetFiles(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                    files.AddRange(filesInCurrent);
                }
                catch
                {
                    // Do Nothing
                }
                try
                {
                    if (searchSubfolders)
                    {
                        string[] foldersInCurrent = System.IO.Directory.GetDirectories(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string _current in foldersInCurrent)
                        {
                            folders.Enqueue(_current);
                        }
                    }
                }
                catch
                {
                    // Do Nothing
                }
            }
            return files;
        }

        public void OnNext(Errores value)
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
