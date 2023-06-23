using System;

namespace MVCBasico.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        public const string CampoRequerido = "{0} el campo es obligatorio.";
        public const string CaracteresMinimos = "{0} debe superar los {1} caracteres.";
        public const string CaracteresMaximos = "{0} no debe superar los {1} caracteres.";
        public const string CorreoInvalido = "El {0} es invalido.";
        public const string PrecioInvalido = "El {0} debe ser mayor a  ${1}.";



    }
}
