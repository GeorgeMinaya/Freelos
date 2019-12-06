using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebSaludOcupacional.DTO;

namespace WebSaludOcupacional.Models
{
    public class TransportistaModel
    {
        public class InicioModel : BaseModel
        {
            [Required]
            [Display(Name ="Número de guía")]
            [MaxLength(16)]
            [MinLength(16)]            //[RegularExpression("^[0-9]*$", ErrorMessage = "Sólo debe ingresar números")]
            public string NumeroGuia { get; set; }
        }

        public class OrdenCargaModel : BaseModel
        {            
            public string CentroGuia { get; set; }
            public string NumeroGuia { get; set; }           
            public string FechaEntrega { get; set; }
            public string Vehiculo { get; set; }
            public string Transportista { get; set; }
            public string NombreTransportista { get; set; }
            public string NombreSupervisor { get; set; }
            public string Viaje { get; set; }
            public string Sistema { get; set; }
            public List<ItemModel> Items { get; set; }
          
        }

        public class ItemModel
        {
            public string IdRegistro { get; set; }            
            public string TipoProducto { get; set; }
            [Required]            
            public string Material { get; set; }            
            public string Descripcion { get; set; }
            public decimal NumeroSuu { get; set; }
            public decimal Unidad { get; set; }
            public decimal SubUnidad { get; set; }           
            [DisplayFormat(DataFormatString ="{0:N0}", ApplyFormatInEditMode = true)]
            [RegularExpression("^[0-9]*$", ErrorMessage = "Sólo debe ingresar números")]
            public int UnidadCambio { get; set; }
            [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
            [RegularExpression("^[0-9]*$", ErrorMessage = "Sólo debe ingresar números")]
            public int SubUnidadCambio { get; set; }            
            [Required]
            public int IdTipoMovimiento { get; set; }
            public bool Revision { get; set; }
            public string MaterialEnvase { get; set; }
            //Campos nuevos
            public int CantOriginal { get; set; }
            public int CantCompra { get; set; }
            public int CantConsignacion { get; set; }
            public int CantValidar { get; set; }
        }
        
    }
}