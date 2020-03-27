using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficioMVC.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Libraries.Arquivo
{
    public class UploadFile

    {
        IHostingEnvironment _appEnvironment;
        public UploadFile(IHostingEnvironment appEnviroment)
        {
            _appEnvironment = appEnviroment;
        }
        public string Upload(IFormFile file, string FileNewname)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);


           string ext =  Path.GetExtension(fileName);
            string Caminho_web = _appEnvironment.WebRootPath;

            string FileNameExt = FileNewname + ext;

            string Destino = Caminho_web + "\\Arquivos\\" + FileNameExt;





            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                File.Delete(fileName);
                 
            }


            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                
                uploadedFile.CopyTo(localFile);
            }
           
            
                Directory.Move(fileName, Destino);
                 return FileNameExt;


        }

        public bool FileExist(string file)
        {
            string Caminho_web = _appEnvironment.WebRootPath;
            string FileLocal = Caminho_web + "\\Arquivos\\" + file;

            if (System.IO.File.Exists(FileLocal))
            {
                return true;
            }
            return false;
        }
    }
}

