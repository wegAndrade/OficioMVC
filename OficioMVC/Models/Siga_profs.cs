using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OficioMVC.Models
{
    public class Siga_profs
    {

        [JsonProperty("ID")]
        
        public int ID { get; set; }
        [JsonProperty("user_login")]
        [Required]
        [Display(Name ="Usuario de login é obrigatorio!")]
        public string user_login { get; set; }

        [Required]
        [Display(Name = "Senha está vazia!")]
        [JsonProperty("user_pass")]
        public string user_pass { get; set; }
        [JsonProperty("user_nicename")]
        public string user_nicename { get; set; }
        [JsonProperty("ativo")]
        public string ativo { get; set; }
        [JsonProperty("dpto")]
        public string dpto { get; set; }
        public bool master { get; set; }
        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();

        public Siga_profs(int iD, string user_login, string user_pass, string user_nicename, string ativo, string dpto)
        {
            ID = iD;
            this.user_login = user_login ;
            this.user_pass = user_pass ;
            this.user_nicename = user_nicename ;
            this.ativo = ativo;
            this.dpto = dpto;
            this.master = false;
        }
        public Siga_profs(int iD, string user_login, string user_pass, string user_nicename, string ativo, string dpto,bool Master)
        {
            ID = iD;
            this.user_login = user_login;
            this.user_pass = user_pass;
            this.user_nicename = user_nicename;
            this.ativo = ativo;
            this.dpto = dpto;
            this.master = Master;
        }
        public Siga_profs()
        {

        }
    }
    }



