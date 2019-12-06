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
    public class FactorRiesgoDA : IFactorRiesgoDA
    {
        private BDSaludOcupacionalDataContext model;

        public FactorRiesgoDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public FactorRiesgoBE.Response Listar_FactorRiesgo()
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
                var ObjResult = from a in model.RiskFactor where a.Activo.Equals(true)
                                select new FactorRiesgoBE.ResponseFactorRiesgoBE()
                                {
                                    ID              =   a.ID
                                    ,RiskFactor     =	a.RiskFactor1	
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                FactorRiesgoBE.Response Result = new FactorRiesgoBE.Response()
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
        public FactorRiesgoBE.Response BuscarFactorRiesgo(int IdFactorRiesgo)
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
                var ObjResult = from a in model.RiskFactor
                                where a.ID.Equals(IdFactorRiesgo)
                                && a.Activo.Equals(true)
                                select new FactorRiesgoBE.ResponseFactorRiesgoBE()
                                {
                                    ID              =   a.ID
                                    ,RiskFactor     =	a.RiskFactor1	
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                FactorRiesgoBE.Response Result = new FactorRiesgoBE.Response()
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
        public Sp_Insert_FactorRiesgoResult RegistrarFactorRiesgo(FactorRiesgoBE.ResponseFactorRiesgoBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_FactorRiesgo(ObjRequestBE.RiskFactor
                                                                   ,ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_FactorRiesgoResult EliminarFactorRiesgo(FactorRiesgoBE.ResponseFactorRiesgoBE objFactorRiesgoBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_FactorRiesgo(objFactorRiesgoBE.ID, objFactorRiesgoBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_FactorRiesgoResult ModificarFactorRiesgo(FactorRiesgoBE.ResponseFactorRiesgoBE objFactorRiesgoBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_FactorRiesgo(objFactorRiesgoBE.ID
                                                                   , objFactorRiesgoBE.RiskFactor
                                                                   , objFactorRiesgoBE.Activo)
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
