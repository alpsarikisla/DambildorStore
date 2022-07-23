using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DambildorStore.Models.ViewModels
{
    public class PayViewModel
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(maximumLength: 75, ErrorMessage = "En Fazla 75 Karakter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(maximumLength: 16, MinimumLength = 16, ErrorMessage = "Kart Numarası 16 Karakter OLmalıdır")]
        public string CardNumber { get; set; }

        public string ExpM { get; set; }

        public string ExpY { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(maximumLength: 3, MinimumLength = 3, ErrorMessage = "Cvv Numarası 3 Karakter OLmalıdır")]
        public string Cvv { get; set; }
    }
}