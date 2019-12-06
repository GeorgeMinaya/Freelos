using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebSaludOcupacional.DTO;

namespace WebSaludOcupacional.Models
{
    public class ReportesModel
    {
        public class Liquidacion : BaseModel
        {
            [Required]
            public string FchRangos { get; set; }            
            [Required]
            public int IdCentro { get; set; }
            public IEnumerable<CentroDTO> Centros { get; set; }
        }

        public class Utilizacion : BaseModel
        {
            [Required]
            public string FchRangos { get; set; }
            [Required]
            public int IdCentro { get; set; }
            public IEnumerable<CentroDTO> Centros { get; set; }
        }

        public class ReImpresion : BaseModel
        {
            [Required]
            [Display(Name = "Número de guía")]
            [MaxLength(16)]
            [MinLength(16)]            //[RegularExpression("^[0-9]*$", ErrorMessage = "Sólo debe ingresar números")]
            public string NumeroGuia { get; set; }
            public string IpImpresora { get; set; }
            public int IdOrdenCarga { get; set; }
        }
    }
}