﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class Comercio
    {   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(3, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Display(Name = "Correo Electronico")]
        [EmailAddress(ErrorMessage = ErrorViewModel.CorreoInvalido)]
        public string Mail { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(8, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(10, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(3, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Display(Name ="Contraseña")]
        [MinLength(5, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(20, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Contrasenia { get; set; }

        [Display(Name ="Lista de servicios")]
        public List<Servicio> Servicios { get; set; }

        [Display(Name ="Reservas")]
        public List<Reserva> ReservasComercio { get; set; }

        [Display(Name ="Foto de Comercio")]
        public string FotoComercio { get; set; }


    }
}
