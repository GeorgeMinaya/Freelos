using Salud.Ocupacional.BE;
using Salud.Ocupacional.DA.Interfaces;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.SourceCodes
{
    public class PerfilDA: IPerfilDA
    {
        private BDSaludOcupacionalDataContext model;

        public PerfilDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }

        public IEnumerable<PerfilBE> Listar() {
            var query = from p in model.UserType
                        select new PerfilBE()
                        {
                            IdPerfil = p.ID,
                            Nombre = p.UserType1
                        };

            return query;
        }

        public int Registrar(PerfilBE objPerfilBE) {
            /*
            var objPerfil = new Perfil() {
                Nombre = objPerfilBE.Nombre,
                IdUsuarioRegistro = objPerfilBE.IdUsuarioRegistro,
                FechaRegistro = DateTime.Now,
                IdUsuarioModifico = objPerfilBE.IdUsuarioRegistro,
                FechaModifico = DateTime.Now,
                Activo = true,
                Eliminado = false
            };

            model.UserType.InsertOnSubmit(objPerfil);
            model.SubmitChanges();            

            foreach (var menu in objPerfilBE.Opciones) {
                var objMenuApp = new Perfil_MenuApp()
                {
                    IdMenuApp = menu.IdMenu,
                    IdPerfil = objPerfil.IdPerfil,
                    IdUsuarioRegistro = objPerfil.IdUsuarioRegistro,
                    FechaRegistro = DateTime.Now,
                    IdUsuarioModifico = objPerfil.IdUsuarioModifico,
                    FechaModifico = DateTime.Now,
                    Activo = true,
                    Eliminado = false
                };
                model.Perfil_MenuApps.InsertOnSubmit(objMenuApp);
            }
            model.SubmitChanges();
            return objPerfil.IdPerfil;
            */
            return 0;
        }

        public int Actualizar(PerfilBE objPerfilBE) {
            /*
            var query = from per in model.Perfils
                        where per.IdPerfil.Equals(objPerfilBE.IdPerfil)
                        select per;

            if (query == null)
                return 0;

            foreach (Perfil objPerfil in query) {
                objPerfil.Nombre = objPerfilBE.Nombre;
                objPerfil.Activo = objPerfilBE.Activo;
                objPerfil.FechaModifico = DateTime.Now;
                objPerfil.IdUsuarioModifico = objPerfilBE.IdUsuarioRegistro;
            }
            model.SubmitChanges();


            var opciones = from men in model.Perfil_MenuApps
                           where men.IdPerfil.Equals(objPerfilBE.IdPerfil)
                           select men;

            foreach (Perfil_MenuApp objMenu in opciones) {
                objMenu.Activo = false;
                model.SubmitChanges();
            }

            foreach (MenuAppBE objMenuAppBE in objPerfilBE.Opciones) {
                var qry = opciones.Where(x => x.IdMenuApp.Equals(objMenuAppBE.IdMenu));
                if (!qry.Any()) {
                    var objMenu = new Perfil_MenuApp();
                    objMenu.IdPerfil = objPerfilBE.IdPerfil;
                    objMenu.IdMenuApp = objMenuAppBE.IdMenu;
                    objMenu.IdUsuarioRegistro = objPerfilBE.IdUsuarioRegistro;
                    objMenu.FechaRegistro = DateTime.Now;
                    objMenu.IdUsuarioModifico = objPerfilBE.IdUsuarioRegistro;
                    objMenu.FechaModifico = DateTime.Now;
                    objMenu.Activo = true;
                    objMenu.Eliminado = false;

                    model.Perfil_MenuApps.InsertOnSubmit(objMenu);
                }
                else {
                    var objMenu = qry.Single();
                    objMenu.IdUsuarioModifico = objPerfilBE.IdUsuarioRegistro;
                    objMenu.FechaModifico = DateTime.Now;
                    objMenu.Activo = true;
                    objMenu.Eliminado = false;
                }
            }
            model.SubmitChanges();

            return objPerfilBE.IdPerfil;
            */
            return 0;
        }

        public PerfilBE Buscar(int IdPerfil)
        {
            var query = from per in model.UserType
                        where per.ID.Equals(IdPerfil)
                        select new PerfilBE()
                        {
                            IdPerfil = per.ID,
                            Nombre = per.UserType1,
                            /*
                            Opciones = (from men in model.Menu
                                        join pme in model.Perfil_MenuApps.Where(x => x.IdPerfil.Equals(IdPerfil) && x.Activo) on men.IdMenuApp equals pme.IdMenuApp into ps
                                        from pme in ps.DefaultIfEmpty()
                                        select new MenuAppBE()
                                        {
                                            IdMenu = men.IdMenuApp,
                                            Nombre = men.Nombre,
                                            Activo = pme == null ? false : true
                                        })
                            */
                        };

            return query.Single();
        }
    }
}
