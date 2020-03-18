

using OficioMVC.Models.Enums;

namespace OficioMVC.Models
{
    public class Edital : Documento
    {
        public TipoEdital Tipo { get; set; }

        public Edital(int id,int numeracao,int ano,string assunto,Siga_profs user, TipoEdital tipo): base(id, numeracao, ano, assunto,user )
        {
            Tipo = tipo;
        }
    }
}
