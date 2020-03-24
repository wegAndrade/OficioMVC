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
            // Extrai o nome do arquivo que veio do browser
            var fileName = System.IO.Path.GetFileName(file.FileName);

            // Se existe a imagem no arquivo temporario a mesma não pode ser enviada
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
                return "Imagem já existe";
            }


            // Cria a imagem na raiz do projeto.
            using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }

            //Armazena o caminho
            string Caminho_web = _appEnvironment.WebRootPath;
            string Destino = Caminho_web + "\\Arquivos\\" + fileName;
            try
            {
                //transfere o  arquivo para pasta destino
                Directory.Move(fileName, Destino);
            }
            catch (IOException e)
            {
               
                //Deleta o arquivo da pasta raiz 
                System.IO.File.Delete(fileName);
                //Caso já exista na pasta destino volta mensagem de arquivo existe
                return "Imagem já existe" + e.Message;
            }

            if (System.IO.File.Exists(fileName))
            {
                //Deleta o arquivo da pasta raiz mantendo apenas no destino
                System.IO.File.Delete(fileName);
            }

            
            return "Imagem enviada com sucesso";


        }
        public String CaminhoDoc(String name)
        {
            string Caminho_web = _appEnvironment.WebRootPath;
            string Local = Caminho_web + "\\Arquivos\\" + name;

            return Local;
        }
    }
}

