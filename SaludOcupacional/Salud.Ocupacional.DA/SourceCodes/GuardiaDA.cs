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
    public class GuardiaDA : IGuardiaDA
    {
        private BDSaludOcupacionalDataContext model;

        public GuardiaDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public GuardiaBE.Response Listar_Guardia()
        {
            try
            {
                var ObjResult = from a in model.Schedule
                                where a.Activo.Equals(true)
                                select new GuardiaBE.ResponseGuardiaBE()
                                {
                                    ID              =   a.ID
                                    ,Schedule       =	a.Schedule1
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                GuardiaBE.Response Result = new GuardiaBE.Response()
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
        public GuardiaBE.Response BuscarGuardia(int IdGuardia)
        {
            try
            {
                var ObjResult = from a in model.Schedule
                                where a.ID.Equals(IdGuardia)
                                && a.Activo.Equals(true)
                                select new GuardiaBE.ResponseGuardiaBE()
                                {
                                    ID              =   a.ID
                                    ,Schedule       =	a.Schedule1
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                GuardiaBE.Response Result = new GuardiaBE.Response()
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
        public Sp_Insert_GuardiaResult RegistrarGuardia(GuardiaBE.ResponseGuardiaBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Guardia(ObjRequestBE.Schedule
                                                                   ,ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_GuardiaResult EliminarGuardia(GuardiaBE.ResponseGuardiaBE objGuardiaBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Guardia(objGuardiaBE.ID, objGuardiaBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_GuardiaResult ModificarGuardia(GuardiaBE.ResponseGuardiaBE objGuardiaBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Guardia(objGuardiaBE.ID
                                                                   , objGuardiaBE.Schedule
                                                                   , objGuardiaBE.Activo)
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
