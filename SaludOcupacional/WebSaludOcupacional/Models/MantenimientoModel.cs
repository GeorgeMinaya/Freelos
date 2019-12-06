using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSaludOcupacional.DTO;

namespace WebSaludOcupacional.Models
{
    public class MantenimientoModel
    {
        #region Usuarios        
        public class MantenimientoUsuarioModel : BaseModel
        {
            public IEnumerable<ResponseLoginBE> lUsuarios { get; set; }

        }     
        public class DatosUsuarioModel
        {
            public int IdUsuario { get; set; }
            [Required]
            [Display(Name = "Código de usuario")]
            public string NombreLogin { get; set; }
            [Required]
            [Display(Name = "Nombre de usuario")]
            public string NombreCompleto { get; set; }
            public List<PerfilDTO> lPerfiles { get; set; }
            public int IdPerfil { get; set; }
            public List<CentroDTO> lCentros { get; set; }
            public bool Activo { get; set; }
            [Required]
            [Display(Name = "Clave de usuario")]
            public string Clave { get; set; }
        }
        #endregion Usuarios

        #region Doctores     
        public class MantenimientoDoctorModel : BaseModel
        {
            public IEnumerable<DoctorBE.ResponseDoctoreBE> lDoctores { get; set; }

        }
        #endregion Doctores

        #region Empresas     
        public class MantenimientoEmpresaModel : BaseModel
        {
            public IEnumerable<EmpresaBE.ResponseEmpresaBE> lEmpresas { get; set; }

        }
        #endregion Empresas

        #region Menus     
        public class MantenimientoMenuModel : BaseModel
        {
            public IEnumerable<MenuBE.ResponseMenuBE> lMenus { get; set; }

        }
        #endregion Menus

        #region Guardia     
        public class MantenimientoGuardiaModel : BaseModel
        {
            public IEnumerable<GuardiaBE.ResponseGuardiaBE> lGuardia { get; set; }

        }
        #endregion Guardia

        #region Ocupacion     
        public class MantenimientoOcupacionModel : BaseModel
        {
            public IEnumerable<OcupacionBE.ResponseOcupacionBE> lOcupacion { get; set; }

        }
        #endregion Ocupacion

        #region Condicion     
        public class MantenimientoCondicionModel : BaseModel
        {
            public IEnumerable<CondicionBE.ResponseCondicionBE> lCondicions { get; set; }

        }
        #endregion Condicion

        #region Marca     
        public class MantenimientoMarcasModelosModel : BaseModel
        {
            public IEnumerable<MarcaModeloBE.ResponseMarcaModeloBE> lMarcaModelo { get; set; }

        }
        #endregion Marca

        #region Trabajador     
        public class MantenimientoTrabajadorModel : BaseModel
        {
            public IEnumerable<TrabajadorBE.ResponseTrabajadorBE> lTrabajador { get; set; }

        }
        #endregion Trabajador

        #region Contratos     
        public class MantenimientoContratosModel : BaseModel
        {
            public IEnumerable<ContratoBE.ResponseContratoBE> lContratos { get; set; }

        }
        #endregion Contratos

        #region Actividades     
        public class MantenimientoActividadModel : BaseModel
        {
            public IEnumerable<ActividadBE.ResponseActividadBE> lActividad { get; set; }

        }
        #endregion Actividades

        #region Archivos     
        public class MantenimientoArchivosModel : BaseModel
        {
            public IEnumerable<ArchivoBE.ResponseArchivoBE> lMenus { get; set; }

        }
        #endregion Archivos
        
        public class DatosPerfilModel
        {
            public int IdPerfil { get; set; }
            [Required]
            [Display(Name ="Perfil")]
            public string Nombre { get; set; }
            public bool Activo { get; set; }
            public List<MenuAppDTO> lOpciones { get; set; }
        }
    }
}