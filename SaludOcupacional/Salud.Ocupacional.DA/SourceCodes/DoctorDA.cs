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
    public class DoctorDA : IDoctorDA
    {
        private BDSaludOcupacionalDataContext model;

        public DoctorDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public DoctorBE.Response Listar_Doctor()
        {
            try
            {
                var query = from p in model.Company
                            where p.Activo.Equals(true)
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany   =   p.IDCompany,
                                RUC         =   p.RUC,
                                Name        =   p.Name
                            };

                var query1 = from p in model.User
                             where p.Activo.Equals(true)
                             && p.Type.Equals(4)
                             && !(from d in model.Doctor
                                  where d.Activo.Equals(true)
                                  group d by d.DNIdoctor into g                                  
                                  select g.Key).Contains(p.DNI)
                             select new UserBE.ResponseUserBE()
                             {
                                 IdUsuario = p.IdUsuario,
                                 DNI = p.DNI,
                                 Name = p.Name
                             };

                var ObjResult = from a in model.Doctor where a.Activo.Equals(true)
                                select new DoctorBE.ResponseDoctoreBE()
                                {
                                    IdDoctor    =   a.IdDoctor,	
                                    DNIdoctor   =   a.DNIdoctor,	
                                    Specialism  =   a.Specialism,
                                    CMP		    =   a.CMP,			
                                    RENumber    =   a.RENumber,	
                                    Company	    =   a.Company,	
                                    lUsuario	=   query1,			
                                    lEmpresa	=   query,		
                                    Activo      =   Convert.ToBoolean(a.Activo)
                                };
                DoctorBE.Response Result = new DoctorBE.Response()
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
        public DoctorBE.Response BuscarDoctor(int IdDoctor)
        {
            try
            {
                var query = from p in model.Company
                            where p.Activo.Equals(true)
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany   =   p.IDCompany,
                                RUC         =   p.RUC,
                                Name        =   p.Name
                            };
                var query1 = (from p in model.User
                              where p.Activo.Equals(true)
                              && p.Type.Equals(4)
                              && !(from d in model.Doctor
                                   where d.Activo.Equals(true)
                                   group d by d.DNIdoctor into g
                                   select g.Key).Contains(p.DNI)
                              select new UserBE.ResponseUserBE()
                              {
                                  IdUsuario = p.IdUsuario,
                                  DNI = p.DNI,
                                  Name = p.Name
                              }).ToList();
                var query2 = (from p in model.Doctor
                              where p.Activo.Equals(true)
                              && p.IdDoctor.Equals(IdDoctor)
                              select new UserBE.ResponseUserBE()
                              {
                                  IdUsuario = p.IdDoctor,
                                  DNI = p.DNIdoctor,
                                  Name = p.Specialism
                              }).ToList();
                var ObjResult = from a in model.Doctor
                                where a.IdDoctor.Equals(IdDoctor)
                                && a.Activo.Equals(true)
                                select new DoctorBE.ResponseDoctoreBE()
                                {
                                    IdDoctor    =   a.IdDoctor,	
                                    DNIdoctor   =   a.DNIdoctor,	
                                    Specialism  =   a.Specialism,
                                    CMP		    =   a.CMP,			
                                    RENumber    =   a.RENumber,	
                                    Company	    =   a.Company,	
                                    lUsuario	=   query1.Union(query2),		
                                    lEmpresa	=   query,			
                                    Activo      =   Convert.ToBoolean(a.Activo)
                                };
                DoctorBE.Response Result = new DoctorBE.Response()
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
        public Sp_Insert_DoctorResult RegistrarDoctor(DoctorBE.ResponseDoctoreBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Doctor(ObjRequestBE.DNIdoctor
                                                                ,ObjRequestBE.Specialism
                                                                ,ObjRequestBE.CMP		  
                                                                ,ObjRequestBE.RENumber  
                                                                ,ObjRequestBE.Company	  
                                                                ,ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_DoctorResult EliminarDoctor(DoctorBE.ResponseDoctoreBE objDoctorBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Doctor(objDoctorBE.IdDoctor, objDoctorBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_DoctorResult ModificarDoctor(DoctorBE.ResponseDoctoreBE objDoctorBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Doctor(objDoctorBE.IdDoctor
                                                                , objDoctorBE.DNIdoctor
                                                                , objDoctorBE.Specialism
                                                                , objDoctorBE.CMP
                                                                , objDoctorBE.RENumber
                                                                , objDoctorBE.Company
                                                                , objDoctorBE.Activo)
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
