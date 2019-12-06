using ACL.MegaCentro.DALC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL.MegaCentro.BE;
using ACL.MegaCentro.DM;
using System.Configuration;

namespace ACL.MegaCentro.DALC.SourceCodes
{
    public class ImpresoraDALC : IImpresoraDALC
    {
        MegaCentroDataContext model;
        public ImpresoraDALC() {
            this.model = new MegaCentroDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }

        public int Actualizar(ImpresoraBE objImpresoraBE)
        {
            var qryper = from pim in model.Perfil_Impresoras.Where(x => x.IdImpresora.Equals(objImpresoraBE.IdImpresora))
                         select pim;

            foreach (Perfil_Impresora perfil in qryper) {
                perfil.Activo = false;
                perfil.IdUsuarioModifico = objImpresoraBE.IdUsuarioRegistro;
                perfil.FechaModifico = DateTime.Now;
            }
            model.SubmitChanges();

            var qryimp = from imp in model.Impresoras.Where(x => x.IdImpresora.Equals(objImpresoraBE.IdImpresora))
                         select imp;

            foreach (Impresora objImpresora in qryimp) {
                objImpresora.Descripcion = objImpresoraBE.Descripcion;
                objImpresora.IP = objImpresoraBE.IP;
                objImpresora.IdCentro = objImpresoraBE.IdCentro;
                objImpresora.IdUsuarioModifico = objImpresoraBE.IdUsuarioRegistro;
                objImpresora.Activo = objImpresoraBE.Activo;
            }

            foreach (PerfilBE objPerfilBE in objImpresoraBE.Perfiles) {
                var perfil = from per in model.Perfil_Impresoras.Where(x => x.IdPerfil.Equals(objPerfilBE.IdPerfil) && x.IdImpresora.Equals(objImpresoraBE.IdImpresora))
                             select per;
                 
                if (perfil.Count() == 0)
                {
                    Perfil_Impresora objPerfil = new Perfil_Impresora()
                    {
                        IdPerfil = objPerfilBE.IdPerfil,
                        IdImpresora = objImpresoraBE.IdImpresora,
                        Activo = true,
                        Eliminado = true,
                        IdUsuarioRegistro = objImpresoraBE.IdUsuarioRegistro,
                        FechaRegistro = DateTime.Now,
                        IdUsuarioModifico = objImpresoraBE.IdUsuarioRegistro,
                        FechaModifico = DateTime.Now
                    };

                    model.Perfil_Impresoras.InsertOnSubmit(objPerfil);
                }
                else {
                    foreach (Perfil_Impresora objPerfil in perfil) {
                        objPerfil.Activo = true;
                        objPerfil.IdUsuarioModifico = objImpresoraBE.IdUsuarioRegistro;
                        objPerfil.FechaModifico = DateTime.Now;
                    }
                }
            }

            model.SubmitChanges();

            return objImpresoraBE.IdImpresora;
        }
        public IEnumerable<ImpresoraBE> Listar(int IdCentro)
        {
            var query = from imp in model.Impresoras.Where(x => x.IdCentro.Equals(IdCentro) && x.Activo.Equals(true) || IdCentro.Equals(0))
                        join cen in model.Centros on imp.IdCentro equals cen.IdCentro
                        select new ImpresoraBE()
                        {                            
                            IdImpresora = imp.IdImpresora,
                            IP = imp.IP,
                            Descripcion = imp.Descripcion,
                            Centro = new CentroBE()
                            {
                                IdCentro = imp.IdCentro,
                                Nombre = cen.Nombre
                            },
                            Perfiles = new List<PerfilBE>(from a in model.Perfil_Impresoras
                                                          join b in model.Perfils
                                                           on a.IdPerfil equals b.IdPerfil
                                                          where a.IdImpresora.Equals(imp.IdImpresora) select new PerfilBE {
                                                            Activo = a.Activo,
                                                            IdPerfil = a.IdPerfil,
                                                            IdUsuarioRegistro = a.IdUsuarioRegistro,
                                                            Nombre = b.Nombre                                                          
                                                        }),
                            Activo = imp.Activo
                        };

            if (query.Count().Equals(0))
                throw new ArgumentException("No exiten impresoras con los datos ingresados");

            return query;
        }
        public int Registrar(ImpresoraBE objImpresoraBE)
        {
            Impresora objImpresora = new Impresora()
            {
                Descripcion = objImpresoraBE.Descripcion,
                IdCentro = objImpresoraBE.IdCentro,
                IP = objImpresoraBE.IP,
                Activo = objImpresoraBE.Activo,
                IdUsuarioRegistro = objImpresoraBE.IdUsuarioRegistro,
                FechaRegistro = DateTime.Now,
                IdUsuarioModifico = objImpresoraBE.IdUsuarioRegistro,
                FechaModifico = DateTime.Now,
                Eliminado = false
            };

            model.Impresoras.InsertOnSubmit(objImpresora);
            model.SubmitChanges();

            foreach (PerfilBE objPerfilBE in objImpresoraBE.Perfiles) {
                Perfil_Impresora objPerfil = new Perfil_Impresora()
                {
                    IdPerfil = objPerfilBE.IdPerfil,
                    IdImpresora = objImpresora.IdImpresora,
                    IdUsuarioRegistro = objImpresoraBE.IdUsuarioRegistro,
                    FechaRegistro = DateTime.Now,
                    IdUsuarioModifico = objImpresoraBE.IdUsuarioRegistro,
                    FechaModifico = DateTime.Now,
                    Activo = objImpresoraBE.Activo,
                    Eliminado = false
                };

                model.Perfil_Impresoras.InsertOnSubmit(objPerfil);
            }
            model.SubmitChanges();

            return objImpresora.IdImpresora;
        }
    }
}
