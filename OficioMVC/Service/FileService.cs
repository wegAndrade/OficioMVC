using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficioMVC.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service
{
    public class FileService

    {
        private string Caminho { get; }
        IHostingEnvironment _appEnvironment;
        public FileService(IHostingEnvironment appEnviroment)
        {
            _appEnvironment = appEnviroment;
            Caminho = _appEnvironment.WebRootPath + "\\Arquivos\\";
        }
        public string Upload(IFormFile file, string FileNewname)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);


            string ext = Path.GetExtension(fileName);



            string FileNameExt = FileNewname + ext;

            string Destino = Caminho + FileNameExt;





            // If file with same name exists delete it
            if (System.IO.File.Exists(Destino))
            {
                File.Delete(Destino);

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

        public ActionResult Download(string Arq)
        {
            string caminho = Caminho + Arq;

            FileStream arquivo = new FileStream(caminho, FileMode.Open);
            FileStreamResult download = new FileStreamResult(arquivo, "application/PNG"); // O segundo parâmetro é o Mime type
            download.FileDownloadName = Arq;
            return download;
        }
        public bool FileExist(string file)
        {

            string FileLocal = Caminho + file;

            if (System.IO.File.Exists(FileLocal))
            {
                return true;
            }
            return false;
        }

        public bool DeleteFile(string file)
        {
            if (FileExist(Caminho + file))
            {
                try
                {
                    File.Delete(Caminho + file);
                    return true;
                }

                catch (IOException )
                {
                    return false;
                }
            }
            return false;

        }
        public String ReplaceFile(IFormFile file, string FileNewname, string FileOld)
        {

            DeleteFile(FileOld);
            try
            {
                return Upload(file, FileNewname);
            }
            catch (IOException )
            {
                return null;
            }



        }
    }
}

