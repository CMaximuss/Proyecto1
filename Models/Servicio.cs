using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(4, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Range(1, 100000, ErrorMessage = ErrorViewModel.PrecioInvalido)]
        public double Precio { get; set; }
    }
}
