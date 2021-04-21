using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    [MetadataType(typeof(TipoAudioMetaData))]
    public partial class TipoAudio { }
        
    public class TipoAudioMetaData
    {
        [Required]
        [Remote("CheckDescricao", "TiposAudio", ErrorMessage = "Tipo Áudio já cadastrado com esse nome.")]
        public string Descricao { get; set; }

        public int TipoAudioID { get; set; }
    }
}