using OficioMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Models
{
    public class Documento
    {
     
        [Key]
        public int Id { get; set; }
        public int Numeracao { get; set; }
        public int Ano { get; set; }
        [Required]
        public string Assunto { get; set; }
        public string Observacoes { get; set; }
        public TipoDoc Tipo { get; set; }
        public string CaminhoArq { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataAlteracao { get; set; }
        [Required]
        public Siga_profs Usuario { get; set; }
        public int UsuarioId { get; set; }

        public Documento()
        {

        }
        public Documento(int id, int numeracao, int ano, string assunto, Siga_profs usuario, TipoDoc tipo)
        {
            Id = id;
            Numeracao = numeracao;
            Ano = ano;
            Assunto = assunto ;
            Usuario = usuario ;
            Tipo = tipo;
        }
    }
}

