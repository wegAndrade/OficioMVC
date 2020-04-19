using Newtonsoft.Json;
using OficioMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Models
{
    public class Documento
    {

        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:000}")]
        [JsonProperty("Numeracao")]
        public int Numeracao { get; set; }
        [JsonProperty("Ano")]
        public int Ano { get; set; }
        public StatusDoc Status { get; set; }
        [Required]
        public string Assunto { get; set; }
        public string Observacoes { get; set; }
        public TipoDoc Tipo { get; set; }
        public string CaminhoArq { get; set; }
        [Display(Name = "Data Envio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [JsonProperty("DataEnvio")]
        public DateTime DataEnvio { get; set; }
        [Display(Name = "Data Alteração")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAlteracao { get; set; }
        [JsonProperty("Usuario")]
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
            Status = StatusDoc.Aberto;
        }

        public Documento(int id, int numeracao, int ano, string assunto,string observacoes, Siga_profs usuario, TipoDoc tipo, DateTime dataEnvio)
        {
            Id = id;
            Numeracao = numeracao;
            Ano = ano;
            Assunto = assunto;
            Usuario = usuario;
            Tipo = tipo;
            Observacoes = observacoes;
            DataEnvio = dataEnvio;
            Status = StatusDoc.Aberto;
            UsuarioId = usuario.ID;
        }
    }
}

