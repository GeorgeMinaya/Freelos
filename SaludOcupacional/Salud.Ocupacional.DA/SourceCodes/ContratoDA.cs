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
    public class ContratoDA : IContratoDA
    {
        private BDSaludOcupacionalDataContext model;

        public ContratoDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public ContratoBE.Response Listar_Contrato()
        {
            try
            {
                var queryus = from p in model.User
                             where p.Activo.Equals(true)
                            select new UserBE.ResponseUserBE()
                            {
                                IdUsuario   =   p.IdUsuario,
                                DNI         =   p.DNI,
                                Name        =   p.Name
                            };
                var query = from p in model.Company
                            where p.Activo.Equals(true)
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany   =   p.IDCompany,
                                RUC         =   p.RUC,
                                Name        =   p.Name
                            };
                var ObjResult = from a in model.Contract where a.Activo.Equals(true)
                                select new ContratoBE.ResponseContratoBE()
                                {
                                    IDContract      =   a.IDContract
                                    ,Dascription    =	a.Dascription	
                                    ,DraegerUser    =	a.DraegerUser	
		                            ,Amount		    =	a.Amount.ToString()		
		                            ,Company	    =	a.Company	
		                            ,lCompany	    =	query	
                                    ,lUsuario	    =   queryus
		                            ,InitialDate    =	Convert.ToString(a.InitialDate)
		                            ,FinalDate	    =	Convert.ToString(a.FinalDate)
		                            ,Quantity	    =	a.Quantity	.ToString()	
		                            ,Celular        =	a.Celular.ToString()
		                            ,Email          =	a.Email	
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                ContratoBE.Response Result = new ContratoBE.Response()
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
        public ContratoBE.Response BuscarContrato(int IdContrato)
        {
            try
            {
                var queryus = from p in model.User
                             where p.Activo.Equals(true)
                            select new UserBE.ResponseUserBE()
                            {
                                IdUsuario   =   p.IdUsuario,
                                DNI         =   p.DNI,
                                Name        =   p.Name
                            };
                var query = from p in model.Company
                            where p.Activo.Equals(true)
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany   =   p.IDCompany,
                                RUC         =   p.RUC,
                                Name        =   p.Name
                            };
                var ObjResult = from a in model.Contract
                                where a.IDContract.Equals(IdContrato)
                                && a.Activo.Equals(true)
                                select new ContratoBE.ResponseContratoBE()
                                {
                                    IDContract      =   a.IDContract
                                    ,Dascription    =	a.Dascription	
                                    ,DraegerUser    =	a.DraegerUser	
		                            ,Amount		    =	a.Amount.ToString()		
		                            ,Company	    =	a.Company
		                            ,lCompany	    =	query	
                                    ,lUsuario	    =   queryus
		                            ,InitialDate    =	Convert.ToString(a.InitialDate)
		                            ,FinalDate	    =	Convert.ToString(a.FinalDate)	
		                            ,Quantity	    =	a.Quantity.ToString()		
		                            ,Celular        =	a.Celular.ToString()
		                            ,Email          =	a.Email	
                                    ,Activo         =   Convert.ToBoolean(a.Activo)
                                };
                ContratoBE.Response Result = new ContratoBE.Response()
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
        public Sp_Insert_ContratoResult RegistrarContrato(ContratoBE.ResponseContratoBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Contrato(ObjRequestBE.DraegerUser
                                                                   ,Convert.ToInt32(ObjRequestBE.Amount)		
                                                                   ,Convert.ToInt32(ObjRequestBE.Company)	
                                                                   ,ObjRequestBE.InitialDate
                                                                   ,ObjRequestBE.FinalDate	
                                                                   ,Convert.ToInt32(ObjRequestBE.Quantity)	
                                                                   ,ObjRequestBE.Dascription
                                                                   ,Convert.ToInt32(ObjRequestBE.Celular)
                                                                   ,ObjRequestBE.Email
                                                                   ,ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_ContratoResult EliminarContrato(ContratoBE.ResponseContratoBE objContratoBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Contrato(objContratoBE.IDContract, objContratoBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_ContratoResult ModificarContrato(ContratoBE.ResponseContratoBE objContratoBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Contrato(objContratoBE.IDContract
                                                                   , objContratoBE.DraegerUser
                                                                   , Convert.ToInt32(objContratoBE.Amount)
                                                                   , Convert.ToInt32(objContratoBE.Company)
                                                                   , objContratoBE.InitialDate
                                                                   , objContratoBE.FinalDate
                                                                   , Convert.ToInt32(objContratoBE.Quantity)
                                                                   , objContratoBE.Dascription
                                                                   , Convert.ToInt32(objContratoBE.Celular)
                                                                   , objContratoBE.Email
                                                                   , objContratoBE.Activo)
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
