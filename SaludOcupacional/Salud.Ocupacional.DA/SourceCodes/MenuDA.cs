using Salud.Ocupacional.BE;
using Salud.Ocupacional.DA.Interfaces;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.SourceCodes
{
    public class MenuDA : IMenuDA
    {
        private BDSaludOcupacionalDataContext model;

        public MenuDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public MenuBE.Response Listar_Menu()
        {
            try
            {
                var query = from p in model.UserType
                            select new MenuTypeBE.ResponseMenuTypeBE()
                            {
                                Id          =   p.ID,
                                UserType    =   p.UserType1,
                            };
                var ObjResult = from a in model.Menu where a.Activo.Equals(true)
                                select new MenuBE.ResponseMenuBE()
                                {
                                    IdMenu      =   a.ID,  
                                    TypeUser    =   a.TypeUser,       
                                    Nombre      =   a.Menu1, 
                                    Icono       =   a.Icono,  
                                    URL         =   a.URL,
                                    lTipoMenu   =   query,
                                    Activo      =   Convert.ToBoolean(a.Activo)
                                };
                MenuBE.Response Result = new MenuBE.Response()
                {
                    lresponse = ObjResult
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MenuTypeBE.Response Listar_Type_Menu()
        {
            try
            {
                var ObjResult = from p in model.UserType
                            select new MenuTypeBE.ResponseMenuTypeBE()
                            {
                                Id          =   p.ID,
                                UserType    =   p.UserType1,
                            };
                MenuTypeBE.Response Result = new MenuTypeBE.Response()
                {
                    lresponse = ObjResult
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MenuBE.Response BuscarMenu(int IdMenu)
        {
            try
            {
                var query = from p in model.UserType
                            select new MenuTypeBE.ResponseMenuTypeBE()
                            {
                                Id          =   p.ID,
                                UserType    =   p.UserType1,
                            };
                var ObjResult = from a in model.Menu
                                where a.ID.Equals(IdMenu)
                                && a.Activo.Equals(true)
                                select new MenuBE.ResponseMenuBE()
                                {
                                    IdMenu      =   a.ID,
                                    TypeUser    =   a.TypeUser,     
                                    Nombre      =   a.Menu1, 
                                    Icono       =   a.Icono,  
                                    URL         =   a.URL,
                                    lTipoMenu   =   query,
                                    Activo      =   Convert.ToBoolean(a.Activo)
                                };
                MenuBE.Response Result = new MenuBE.Response()
                {
                    response = ObjResult.Single()
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Insert_MenuResult RegistrarMenu(MenuBE.ResponseMenuBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Menu(ObjRequestBE.TypeUser
                                                               , ObjRequestBE.Nombre
                                                               , ObjRequestBE.Icono
                                                               , ObjRequestBE.URL	  
                                                               , ObjRequestBE.Activo)
                                                               select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_MenuResult EliminarMenu(MenuBE.ResponseMenuBE objMenuBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Menu(objMenuBE.IdMenu, objMenuBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_MenuResult ModificarMenu(MenuBE.ResponseMenuBE objMenuBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Menu(objMenuBE.IdMenu
                                                               , objMenuBE.TypeUser
                                                               , objMenuBE.Nombre
                                                               , objMenuBE.URL
                                                               , objMenuBE.Icono
                                                               , objMenuBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }    
}
