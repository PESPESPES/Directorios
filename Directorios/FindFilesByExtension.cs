using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorios
{

    /// <summary>
    /// Obtener archivos
    /// </summary>
    public class FindFilesByExtension
    {
        public string Extension { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FindFilesByExtension" /> class.
        /// </summary>
        /// <param name="extension">The extension.</param>
        public FindFilesByExtension(string extension)
        {
            Extension = extension;
        }
        public void GetData(string rutaBase)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(rutaBase);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            //Create the query  
            IEnumerable<System.IO.FileInfo> fileQuery =
                from file in fileList
                where file.Extension == Extension
                orderby file.Name
                select file;

            //Execute the query. This might write out a lot of files!  
            foreach (System.IO.FileInfo fi in fileQuery)
            {
                Console.WriteLine(fi.FullName);
            }

            // Create and execute a new query by using the previous   
            // query as a starting point. fileQuery is not   
            // executed again until the call to Last()  
            var newestFile =
                (from file in fileQuery
                 orderby file.CreationTime
                 select new { file.FullName, file.CreationTime })
                .Last();
        }
    }
}
