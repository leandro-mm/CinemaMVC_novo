//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwSessao
    {
        public int SessaoID { get; set; }
        public System.DateTime Data { get; set; }
        public int SalaID { get; set; }
        public string Sala { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFim { get; set; }
        public decimal ValorIngresso { get; set; }
        public int FilmeID { get; set; }
        public string NomeFilme { get; set; }
        public int TipoAnimacaoID { get; set; }
        public int TipoAudioID { get; set; }
    }
}
