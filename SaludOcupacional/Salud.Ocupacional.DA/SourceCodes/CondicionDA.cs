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
    public class CondicionDA : ICondicionDA
    {
        private BDSaludOcupacionalDataContext model;

        public CondicionDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public CondicionBE.Response Listar_Condicion()
        {
            try
            {
                var ObjResult = from a in model.WorkCondition
                                where a.Activo.Equals(true)
                                select new CondicionBE.ResponseCondicionBE()
                                {
                                    ID                  =   a.ID
                                    ,WorkCondition      =	a.WorkCondition1
                                    ,Activo             =   Convert.ToBoolean(a.Activo)
                                };
                CondicionBE.Response Result = new CondicionBE.Response()
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
        public CondicionBE.Response BuscarCondicion(int IdCondicion)
        {
            try
            {
                var ObjResult = from a in model.WorkCondition
                                where a.ID.Equals(IdCondicion)
                                && a.Activo.Equals(true)
                                select new CondicionBE.ResponseCondicionBE()
                                {
                                    ID                  =   a.ID
                                    ,WorkCondition      =	a.WorkCondition1
                                    ,Activo             =   Convert.ToBoolean(a.Activo)
                                };
                CondicionBE.Response Result = new CondicionBE.Response()
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
        public Sp_Insert_CondicionResult RegistrarCondicion(CondicionBE.ResponseCondicionBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Condicion(ObjRequestBE.WorkCondition
                                                                   , ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_CondicionResult EliminarCondicion(CondicionBE.ResponseCondicionBE objCondicionBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Condicion(objCondicionBE.ID, objCondicionBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_CondicionResult ModificarCondicion(CondicionBE.ResponseCondicionBE objCondicionBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Condicion(objCondicionBE.ID
                                                                   , objCondicionBE.WorkCondition
                                                                   , objCondicionBE.Activo)
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
