using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OficioMVC.Models
{
    public class Siga_profs
    {

        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("user_login")]
        public string user_login { get; set; }
        [JsonProperty("user_pass")]
        public string user_pass { get; set; }
        [JsonProperty("user_nicename")]
        public string user_nicename { get; set; }
        [JsonProperty("ativo")]
        public string ativo { get; set; }
        [JsonProperty("dpto")]
        public string dpto { get; set; }
    }
    }



