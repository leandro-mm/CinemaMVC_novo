using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    [MetadataType(typeof(TipoAnimacaoMetaData))]
    public partial class TipoAnimacao { }
        
    public class TipoAnimacaoMetaData
    {
        [Required]
        [Remote("CheckDescricao", "TiposAnimacao", ErrorMessage = "Tipo Animação já cadastrada com esse nome.")]
        public string Descricao { get; set; }

        public int TipoAnimacaoID { get; set; }
    }
}