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
    public class OcupacionDA : IOcupacionDA
    {
        private BDSaludOcupacionalDataContext model;

        public OcupacionDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public OcupacionBE.Response Listar_Ocupacion()
        {
            try
            {
                var query = from p in model.Company
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany   =   p.IDCompany,
                                RUC         =   p.RUC,
                                Name        =   p.Name
                            };
                var ObjResult = from a in model.OccupationType where a.Activo.Equals(true)
                                select new OcupacionBE.ResponseOcupacionBE()
                                {
                                    ID              =   a.ID
                                    ,Occupation    =	a.Occupation		
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                OcupacionBE.Response Result = new OcupacionBE.Response()
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
        public OcupacionBE.Response BuscarOcupacion(int IdOcupacion)
        {
            try
            {
                var query = from p in model.Company
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany   =   p.IDCompany,
                                RUC         =   p.RUC,
                                Name        =   p.Name
                            };
                var ObjResult = from a in model.OccupationType
                                where a.ID.Equals(IdOcupacion)
                                && a.Activo.Equals(true)
                                select new OcupacionBE.ResponseOcupacionBE()
                                {
                                    ID              =   a.ID
                                    ,Occupation     =	a.Occupation		
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                OcupacionBE.Response Result = new OcupacionBE.Response()
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
        public Sp_Insert_OcupacionResult RegistrarOcupacion(OcupacionBE.ResponseOcupacionBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Ocupacion(ObjRequestBE.Occupation
                                                                   ,ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_OcupacionResult EliminarOcupacion(OcupacionBE.ResponseOcupacionBE objOcupacionBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Ocupacion(objOcupacionBE.ID, objOcupacionBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_OcupacionResult ModificarOcupacion(OcupacionBE.ResponseOcupacionBE objOcupacionBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Ocupacion(objOcupacionBE.ID
                                                                   , objOcupacionBE.Occupation
                                                                   , objOcupacionBE.Activo)
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
