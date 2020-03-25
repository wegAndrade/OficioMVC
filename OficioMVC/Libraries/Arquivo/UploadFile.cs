using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public String Upload(IFormFile file)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);


            string Caminho_web = _appEnvironment.WebRootPath;

            string Destino = Caminho_web + "\\Arquivos\\" + fileName;





            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {

                return "Arquivo já existe";
            }


            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }
            try
            {
                Directory.Move(fileName, Destino);
            }
            catch (IOException e)
            {
                
                return "Arquivo já existe";
            }

            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            

            return Destino;
        }
    }
}

