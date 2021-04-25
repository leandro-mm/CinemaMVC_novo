using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    public class SessaoCreateViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Data { get; set; }

        [Required]
        [Display(Name = "Hora Início")]
        public TimeSpan? HorarioInicio { get; set; }

        [Display(Name = "Hora Fim")]
        public TimeSpan? HorarioFim { get; set; }

        [Required]
        [Display(Name = "Valor Ingresso")]        
        public int? ValorIngresso { get; set; }

        [Required]
        [Display(Name = "Sala")]
        public int? SalaAudioAnimacaoID { get; set; }
        public SelectList SalaAudioAnimacaoSelectList { get; set; }

        [Required]
        [Display(Name = "Filme")]
        public int? FilmeID { get; set; }
        public SelectList FilmeSelectList { get; set; }


    }
}