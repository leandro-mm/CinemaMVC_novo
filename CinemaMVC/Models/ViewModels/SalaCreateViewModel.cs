using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    public class SalaCreateViewModel
    {
        [Required]
        [Remote("CheckNomeSala", "Salas", ErrorMessage = "Sala já cadastrada com esse nome.")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Qtd. Assentos")]
        public int QtdAssentos { get; set; }

        [Required]
        [Display(Name = "Tipo Áudio")]
        public int TipoAudioID1 { get; set; }
        public SelectList TipoAudioID1selectList { get; set; }

        [Required]
        [Display(Name = "Tipo Animação")]
        public int TipoAnimacaoID1 { get; set; }
        public SelectList TipoAnimacaoID1selectList { get; set; }

        [Display(Name = "Tipo Áudio")]
        public int? TipoAudioID2 { get; set; }
        public SelectList TipoAudioID2selectList { get; set; }

        [Display(Name = "Tipo Animação")]
        public int? TipoAnimacaoID2 { get; set; }
        public SelectList TipoAnimacaoID2selectList { get; set; }

        [Display(Name = "Tipo Áudio")]
        public int? TipoAudioID3 { get; set; }
        public SelectList TipoAudioID3selectList { get; set; }

        [Display(Name = "Tipo Animação")]
        public int? TipoAnimacaoID3 { get; set; }
        public SelectList TipoAnimacaoID3selectList { get; set; }
    }
}