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
    public class EmpresaDA : IEmpresaDA
    {
        private BDSaludOcupacionalDataContext model;

        public EmpresaDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public EmpresaBE.Response Listar_Empresa()
        {
            try
            {
                var ObjResult = from a in model.Company where a.Activo.Equals(true)
                                select new EmpresaBE.ResponseEmpresaBE()
                                {
                                    IDCompany            = a.IDCompany		
                                    ,RUC                 = a.RUC				
                                    ,Name                = a.Name			
                                    ,Tradename           = a.Tradename		
                                    ,Address             = a.Address			
                                    ,Contactname         = a.Contactname		
                                    ,ContactLastname     = a.ContactLastname	
                                    ,ContactLastname2    = a.ContactLastname2
                                    ,Cellphone           = a.Cellphone		
                                    ,Email               = a.Email			
                                    ,Activo              = Convert.ToBoolean(a.Activo)
                                };
                EmpresaBE.Response Result = new EmpresaBE.Response()
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
        public EmpresaBE.Response BuscarEmpresa(int IdEmpresa)
        {
            try
            {
                var ObjResult = from a in model.Company
                                where a.IDCompany.Equals(IdEmpresa)
                                && a.Activo.Equals(true)
                                select new EmpresaBE.ResponseEmpresaBE()
                                {
                                    IDCompany            = a.IDCompany		
                                    ,RUC                 = a.RUC				
                                    ,Name                = a.Name			
                                    ,Tradename           = a.Tradename		
                                    ,Address             = a.Address			
                                    ,Contactname         = a.Contactname		
                                    ,ContactLastname     = a.ContactLastname	
                                    ,ContactLastname2    = a.ContactLastname2
                                    ,Cellphone           = a.Cellphone		
                                    ,Email               = a.Email			
                                    ,Activo              = Convert.ToBoolean(a.Activo)
                                };
                EmpresaBE.Response Result = new EmpresaBE.Response()
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
        public Sp_Insert_EmpresaResult RegistrarEmpresa(EmpresaBE.ResponseEmpresaBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Empresa(ObjRequestBE.RUC             
                                                                  ,ObjRequestBE.Name            
                                                                  ,ObjRequestBE.Tradename       
                                                                  ,ObjRequestBE.Address         
                                                                  ,ObjRequestBE.Contactname     
                                                                  ,ObjRequestBE.ContactLastname 
                                                                  ,ObjRequestBE.ContactLastname2
                                                                  ,ObjRequestBE.Cellphone       
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
        public Sp_Delete_EmpresaResult EliminarEmpresa(EmpresaBE.ResponseEmpresaBE objEmpresaBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Empresa(objEmpresaBE.IDCompany, objEmpresaBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_EmpresaResult ModificarEmpresa(EmpresaBE.ResponseEmpresaBE objEmpresaBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Empresa(objEmpresaBE.IDCompany
                                                                  , objEmpresaBE.RUC
                                                                  , objEmpresaBE.Name
                                                                  , objEmpresaBE.Tradename
                                                                  , objEmpresaBE.Address
                                                                  , objEmpresaBE.Contactname
                                                                  , objEmpresaBE.ContactLastname
                                                                  , objEmpresaBE.ContactLastname2
                                                                  , objEmpresaBE.Cellphone
                                                                  , objEmpresaBE.Email
                                                                  , objEmpresaBE.Activo) 
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
