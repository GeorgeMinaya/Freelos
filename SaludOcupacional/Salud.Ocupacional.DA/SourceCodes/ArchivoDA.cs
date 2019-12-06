using Salud.Ocupacional.BE;
using Salud.Ocupacional.DA.Interfaces;
using Salud.Ocupacional.DM;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Salud.Ocupacional.DA.SourceCodes
{
    public class ArchivoDA : IArchivoDA
    {
        private readonly BDSaludOcupacionalDataContext model;

        public ArchivoDA() => model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);

        public ArchivoBE.ResponseArchivoBE DatosRegistrar()
        {
            // obtener compañias
            var companias = new List<EmpresaBE.ResponseEmpresaBE> { new EmpresaBE.ResponseEmpresaBE { IDCompany = 0, Name = "--- Empresa ---" } };
            companias.AddRange(from p in model.Company
                               select new EmpresaBE.ResponseEmpresaBE()
                               {
                                   IDCompany = p.IDCompany,
                                   Name = p.Name
                               });

            // obtener contratos
            var contratos = new List<ContratoBE.ResponseContratoBE> { new ContratoBE.ResponseContratoBE { IDContract = 0, DraegerUser = "--- Contrato ---" } };
            contratos.AddRange(from p in model.Contract
                               select new ContratoBE.ResponseContratoBE()
                               {
                                   IDContract = p.IDContract,
                                   DraegerUser = p.DraegerUser
                               });

            // obtener trabajadores
            var trabajadores = new List<TrabajadorBE.ResponseTrabajadorBE> { new TrabajadorBE.ResponseTrabajadorBE { IdTrabajador = 0, Name = "--- Trabajador ---" } };
            trabajadores.AddRange(from r in model.Employee
                                  select new TrabajadorBE.ResponseTrabajadorBE
                                  {
                                      IdTrabajador = r.IdTrabajador,
                                      Name = r.Name
                                  });

            return new ArchivoBE.ResponseArchivoBE
            {
                LCompany = companias,
                LContract = contratos,
                LTrabajador = trabajadores
            };
        }

        public int RegistrarArchivo(ArchivoBE.ResponseArchivoBE archivoBE)
        {
            // obtener el IdActividad basado en estructura de base de datos
            var actividad = model.Activity.FirstOrDefault(x => x.IDCompany.Equals(archivoBE.IDCompany)
                                                            && x.Contract.Equals(archivoBE.IdContract));

            if (actividad != null)
            {
                var nuevo = new IndicatorSingle
                {
                    IDactivity = actividad.IDactivity,
                    IDCompany = archivoBE.IDCompany,
                    IDContract = archivoBE.IdContract,
                    EmployeeID = $" { archivoBE.IdTrabajador }",
                    Date = archivoBE.FechaRegistro,
                    IdUsuario = archivoBE.IdUsuarioRegistro,
                    Filepath = archivoBE.UrlArchivo,
                    SerialNumber = string.Empty
                };

                model.IndicatorSingle.InsertOnSubmit(nuevo);
                model.SubmitChanges();

                return nuevo.IDindicator;
            }

            return 0;
        }
    }
}
