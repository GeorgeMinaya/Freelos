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
    public class TrabajadorDA : ITrabajadorDA
    {
        private BDSaludOcupacionalDataContext model;

        public TrabajadorDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public TrabajadorBE.Response Listar_Trabajador()
        {
            try
            {
                var QueryOC = new List<OcupacionBE.ResponseOcupacionBE> {new OcupacionBE.ResponseOcupacionBE{ID = 0, Occupation = "--- Ocupacion ---" } };
                QueryOC.AddRange(from p in model.OccupationType
                                 select new OcupacionBE.ResponseOcupacionBE()
                                 {
                                     ID = p.ID,
                                     Occupation = p.Occupation,
                                     Activo = Convert.ToBoolean(p.Activo)
                                 });

                var QueryCO = new List<EmpresaBE.ResponseEmpresaBE> { new EmpresaBE.ResponseEmpresaBE { IDCompany = 0, Name = "--- Empresa ---" } };
                QueryCO.AddRange(from p in model.Company
                                 where p.Activo.Equals(true)
                                 select new EmpresaBE.ResponseEmpresaBE()
                                {
                                    IDCompany       =   p.IDCompany,
                                    Name            =   p.Name
                                });

                var QUERYSH = new List<ScheduleBE.ResponseScheduleBE> { new ScheduleBE.ResponseScheduleBE { ID = 0, Schedule = "--- Turno ---" } };
                QUERYSH.AddRange(from p in model.Schedule
                                 where p.Activo.Equals(true)
                                 select new ScheduleBE.ResponseScheduleBE()
                                {
                                    ID              =   p.ID,
                                    Schedule        =   p.Schedule1
                                });

                var QueryWC = new List<CondicionBE.ResponseCondicionBE> { new CondicionBE.ResponseCondicionBE { ID = 0, WorkCondition = "--- Condicion ---" } };
                QueryWC.AddRange(from p in model.WorkCondition
                                 where p.Activo.Equals(true)
                                 select new CondicionBE.ResponseCondicionBE()
                                {
                                    ID              =   p.ID,
                                    WorkCondition   =   p.WorkCondition1
                                });

                var QueryRF = new List<RiskFactorBE.ResponseRiskFactorBE> { new RiskFactorBE.ResponseRiskFactorBE { ID = 0, RiskFactor = "--- Factor de Riesgo ---" } };
                QueryRF.AddRange(from p in model.RiskFactor
                                 where p.Activo.Equals(true)
                                 select new RiskFactorBE.ResponseRiskFactorBE()
                                {
                                    ID              =   p.ID,
                                    RiskFactor      =   p.RiskFactor1
                                });
                                
                var query1 = from p in model.User
                             where p.Activo.Equals(true)
                             && p.Type.Equals(2)
                             && !(from d in model.Employee
                                  where d.Activo.Equals(true)
                                  group d by d.DNIemployee into g
                                  select g.Key).Contains(p.DNI)
                             select new UserBE.ResponseUserBE()
                            {
                                IdUsuario   =   p.IdUsuario,
                                DNI         =   p.DNI,
                                Name        =   p.Name
                            };
                var ObjResult = from a in model.Employee where a.Activo.Equals(true)
                                select new TrabajadorBE.ResponseTrabajadorBE()
                                {
                                    IdTrabajador          =     a.IdTrabajador
                                    , DNIemployee         =     a.DNIemployee	
                                    , Birthdate           =     Convert.ToString(a.Birthdate)
                                    , Occupation          =     a.Occupation
                                    , lOccupation         =     QueryOC.ToList()			
                                    , Schedule            =     a.Schedule	
                                    , lSchedule           =     QUERYSH.ToList()
                                    , Company             =     a.Company
                                    , lCompany            =     QueryCO.ToList()		
                                    , WorkCondition       =     a.WorkCondition
                                    , lWorkCondition      =     QueryWC.ToList()
                                    , lUsuario	          =     query1		
                                    , DateEntry           =     Convert.ToString(a.DateEntry)
                                    , RiskFactor          =     a.RiskFactor
                                    , lRiskFactor         =     QueryRF.ToList()		
                                    , Name                =     a.Name	
                                    , Activo              =     Convert.ToBoolean(a.Activo)
                                };
                TrabajadorBE.Response Result = new TrabajadorBE.Response()
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
        public TrabajadorBE.Response BuscarTrabajador(int IdTrabajador)
        {
            try
            {
                var QueryOC = from p in model.OccupationType
                              where p.Activo.Equals(true)
                              select new OcupacionBE.ResponseOcupacionBE()
                                {
                                    ID          =   p.ID,
                                    Occupation  =   p.Occupation
                                };
                var QueryCO = from p in model.Company
                              where p.Activo.Equals(true)
                              select new EmpresaBE.ResponseEmpresaBE()
                                {
                                    IDCompany       =   p.IDCompany,
                                    Name            =   p.Name
                                };
                var QUERYSH = from p in model.Schedule
                              where p.Activo.Equals(true)
                              select new ScheduleBE.ResponseScheduleBE()
                                {
                                    ID              =   p.ID,
                                    Schedule        =   p.Schedule1
                                };
                var QueryWC = from p in model.WorkCondition
                              where p.Activo.Equals(true)
                              select new CondicionBE.ResponseCondicionBE()
                                {
                                    ID              =   p.ID,
                                    WorkCondition   =   p.WorkCondition1
                                };
                var QueryRF = from p in model.RiskFactor
                              where p.Activo.Equals(true)
                              select new RiskFactorBE.ResponseRiskFactorBE()
                                {
                                    ID              =   p.ID,
                                    RiskFactor      =   p.RiskFactor1
                                };

                List<UserBE.ResponseUserBE> query1 = new List<UserBE.ResponseUserBE>(from p in model.User
                             where p.Activo.Equals(true)
                             && p.Type.Equals(2)
                             && !(from d in model.Employee
                                  where d.Activo.Equals(true)
                                  group d by d.DNIemployee into g
                                  select g.Key).Contains(p.DNI)
                             select new UserBE.ResponseUserBE()
                            {
                                IdUsuario   =   p.IdUsuario,
                                DNI         =   p.DNI,
                                Name        =   p.Name
                            }).ToList();
                UserBE.ResponseUserBE UserModi = (from p in model.Employee
                                                        .Where(x => x.IdTrabajador.Equals(IdTrabajador))
                                                        select new UserBE.ResponseUserBE
                                                        {
                                                            IdUsuario = p.IdTrabajador,
                                                            DNI = p.DNIemployee,
                                                            Name = p.Name
                                                        }).Single();
                query1.Add(UserModi);
                var ObjResult = from a in model.Employee
                                where a.IdTrabajador.Equals(IdTrabajador)
                                && a.Activo.Equals(true)
                                select new TrabajadorBE.ResponseTrabajadorBE()
                                {
                                    IdTrabajador          =     a.IdTrabajador
                                    , DNIemployee         =     a.DNIemployee	
                                    , Birthdate           =     Convert.ToString(a.Birthdate)
                                    , Occupation          =     a.Occupation
                                    , lOccupation         =     QueryOC.ToList()			
                                    , Schedule            =     a.Schedule	
                                    , lSchedule           =     QUERYSH.ToList()
                                    , Company             =     a.Company
                                    , lCompany            =     QueryCO.ToList()		
                                    , WorkCondition       =     a.WorkCondition
                                    , lWorkCondition      =     QueryWC.ToList()
                                    , lUsuario	          =     query1			
                                    , DateEntry           =     Convert.ToString(a.DateEntry)
                                    , RiskFactor          =     a.RiskFactor
                                    , lRiskFactor         =     QueryRF.ToList()		
                                    , Name                =     a.Name	
                                    , Activo              =     Convert.ToBoolean(a.Activo)
                                };
                TrabajadorBE.Response Result = new TrabajadorBE.Response()
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
        public Sp_Insert_TrabajadorResult RegistrarTrabajador(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Trabajador(objTrabajadorBE.DNIemployee
                                                                  , objTrabajadorBE.Birthdate
                                                                  , objTrabajadorBE.Occupation
                                                                  , objTrabajadorBE.Schedule
                                                                  , objTrabajadorBE.Company
                                                                  , objTrabajadorBE.WorkCondition
                                                                  , objTrabajadorBE.RiskFactor
                                                                  , objTrabajadorBE.Name
                                                                  , objTrabajadorBE.Activo)
                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_TrabajadorResult EliminarTrabajador(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Trabajador(objTrabajadorBE.IdTrabajador, objTrabajadorBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_TrabajadorResult ModificarTrabajador(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Trabajador(objTrabajadorBE.IdTrabajador
                                                                  , objTrabajadorBE.DNIemployee
                                                                  , objTrabajadorBE.Birthdate
                                                                  , objTrabajadorBE.Occupation
                                                                  , objTrabajadorBE.Schedule
                                                                  , objTrabajadorBE.Company
                                                                  , objTrabajadorBE.WorkCondition
                                                                  , objTrabajadorBE.RiskFactor
                                                                  , objTrabajadorBE.Name
                                                                  , objTrabajadorBE.Activo)
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
