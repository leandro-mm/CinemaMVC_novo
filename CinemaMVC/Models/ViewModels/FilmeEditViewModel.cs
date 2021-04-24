using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    public class FilmeEditViewModel
    {        
        public int FilmeID { get; set; }

        [Required]
        [Remote("CheckNomeFilme", "Filmes", AdditionalFields = "FilmeID", ErrorMessage = "Filme já cadastrado com esse nome.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Duração")]
        public TimeSpan? Duracao { get; set; }

        [Display(Name = "Alterar Imagem")]
        public HttpPostedFileBase UploadedFile { get; set; }

        public int ImagemID { get; set; }
        public byte[] Imagem { get; set; }

    }
}