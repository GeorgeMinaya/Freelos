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
    public class ActividadDA : IActividadDA
    {
        private BDSaludOcupacionalDataContext model;

        public ActividadDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public ActividadBE.Response Listar_Actividad(int IdUsuario)
        {
            try
            {
                var QueryCO = new List<EmpresaBE.ResponseEmpresaBE> { new EmpresaBE.ResponseEmpresaBE { IDCompany = 0, Name = "--- Empresa ---" } };
                QueryCO.AddRange(from p in model.Company
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany       =   p.IDCompany,
                                Name            =   p.Name
                            });

                var QueryCT = new List<ContratoBE.ResponseContratoBE> { new ContratoBE.ResponseContratoBE { IDContract = 0, DraegerUser = "--- Contrato ---" } };
                QueryCT.AddRange(from p in model.Contract
                            select new ContratoBE.ResponseContratoBE()
                            {
                                IDContract       =   p.IDContract,
                                DraegerUser      =   p.DraegerUser
                            });

                var QueryMM = new List<MarcaModeloBE.ResponseMarcaModeloBE> { new MarcaModeloBE.ResponseMarcaModeloBE { ID = 0, Model = "--- Marca & Modelo ---" } };
                QueryMM.AddRange(from p in model.Respirator
                            select new MarcaModeloBE.ResponseMarcaModeloBE()
                            {
                                ID      =   p.ID,
                                Model   =   p.Model
                            });

                var QueryFT = new List<FiltroBE.ResponseFiltroBE> { new FiltroBE.ResponseFiltroBE { IDFilter = 0, Filter = "--- Filtro ---" } };
                QueryFT.AddRange(from p in model.Filter
                            select new FiltroBE.ResponseFiltroBE()
                            {
                                IDFilter =   p.IDFilter,
                                Filter   =   p.Filter1
                            });
                var ObjResult = from a in model.Activity
                                join d in model.Doctor
                                 on a.IDCompany equals d.Company
                                where d.IdDoctor.Equals(IdUsuario)
                                && a.Activo.Equals(true)
                                select new ActividadBE.ResponseActividadBE()
                                {
                                    IDactivity          =   a.IDactivity 
                                    , IDCompany         =   a.IDCompany  
                                    , lCompany          =   QueryCO 
                                    , Date              =   Convert.ToString(a.Date)
                                    , Contract          =   a.Contract   
                                    , lContract         =   QueryCT 
                                    , StartHour         =   Convert.ToString(a.StartHour)
                                    , FinishHour        =   Convert.ToString(a.FinishHour)
                                    , Respirator        =   a.Respirator 
                                    , lRespirator       =   QueryMM 
                                    , Filter            =   a.Filter  
                                    , lFilter           =   QueryFT
                                    , Quantity          =   a.Quantity   
                                    , Supervisor        =   a.Supervisor 
                                    , Description       =   a.Description
                                    , Activo            =   Convert.ToBoolean(a.Activo)
                                };
                ActividadBE.Response Result = new ActividadBE.Response()
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
        public ActividadBE.Response BuscarActividad(int IdActividad)
        {
            try
            {
                var QueryCO = new List<EmpresaBE.ResponseEmpresaBE> { new EmpresaBE.ResponseEmpresaBE { IDCompany = 0, Name = "--- Empresa ---" } };
                QueryCO.AddRange(from p in model.Company
                            select new EmpresaBE.ResponseEmpresaBE()
                            {
                                IDCompany       =   p.IDCompany,
                                Name            =   p.Name
                            });

                var QueryCT = new List<ContratoBE.ResponseContratoBE> { new ContratoBE.ResponseContratoBE { IDContract = 0, DraegerUser = "--- Contrato ---" } };
                QueryCT.AddRange(from p in model.Contract
                            select new ContratoBE.ResponseContratoBE()
                            {
                                IDContract       =   p.IDContract,
                                DraegerUser      =   p.DraegerUser
                            });

                var QueryMM = new List<MarcaModeloBE.ResponseMarcaModeloBE> { new MarcaModeloBE.ResponseMarcaModeloBE { ID = 0, Model = "--- Marca & Modelo ---" } };
                QueryMM.AddRange(from p in model.Respirator
                            select new MarcaModeloBE.ResponseMarcaModeloBE()
                            {
                                ID      =   p.ID,
                                Model   =   p.Model
                            });

                var QueryFT = new List<FiltroBE.ResponseFiltroBE> { new FiltroBE.ResponseFiltroBE { IDFilter = 0, Filter = "--- Filtro ---" } };
                QueryFT.AddRange(from p in model.Filter
                            select new FiltroBE.ResponseFiltroBE()
                            {
                                IDFilter =   p.IDFilter,
                                Filter   =   p.Filter1
                            });
                var ObjResult = from a in model.Activity
                                where a.IDactivity.Equals(IdActividad)
                                && a.Activo.Equals(true)
                                select new ActividadBE.ResponseActividadBE()
                                {
                                    IDactivity          =   a.IDactivity 
                                    , IDCompany         =   a.IDCompany  
                                    , lCompany          =   QueryCO 
                                    , Date              =   Convert.ToString(a.Date)
                                    , Contract          =   a.Contract   
                                    , lContract         =   QueryCT 
                                    //, StartHour         =   a.StartHour.ToString("hh\\:mm\\:ss")
                                    //, FinishHour        =   a.FinishHour.ToString("hh\\:mm\\:ss")
                                    , StartHour1         =   a.StartHour
                                    , FinishHour1        =   a.FinishHour
                                    , Respirator        =   a.Respirator 
                                    , lRespirator       =   QueryMM 
                                    , Filter            =   a.Filter  
                                    , lFilter           =   QueryFT
                                    , Quantity          =   a.Quantity   
                                    , Supervisor        =   a.Supervisor 
                                    , Description       =   a.Description
                                    , Activo            =   Convert.ToBoolean(a.Activo)
                                };

                var Objres = new List<ActividadBE.ResponseActividadBE>();

                foreach (var item in ObjResult)
                {
                    item.IDactivity = item.IDactivity;
                    item.StartHour = item.StartHour1.ToString("hh\\:mm\\:ss");
                    item.FinishHour = item.FinishHour1.ToString("hh\\:mm\\:ss");
                    Objres.Add(item);
                }
                ObjResult.Union(Objres);
                ActividadBE.Response Result = new ActividadBE.Response()
                {
                    response = Objres.Single()
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Insert_ActividadResult RegistrarActividad(ActividadBE.ResponseActividadBE objActividadBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_Actividad(objActividadBE.IDCompany
                                                                    , objActividadBE.Date       
                                                                    , objActividadBE.Contract   
                                                                    , objActividadBE.StartHour  
                                                                    , objActividadBE.FinishHour 
                                                                    , objActividadBE.Respirator 
                                                                    , objActividadBE.Filter     
                                                                    , objActividadBE.Quantity   
                                                                    , objActividadBE.Supervisor 
                                                                    , objActividadBE.Description
                                                                    , objActividadBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_ActividadResult EliminarActividad(ActividadBE.ResponseActividadBE objActividadBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Actividad(objActividadBE.IDactivity, objActividadBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_ActividadResult ModificarActividad(ActividadBE.ResponseActividadBE objActividadBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_Actividad(objActividadBE.IDactivity
                                                                    , objActividadBE.IDCompany
                                                                    , objActividadBE.Date
                                                                    , objActividadBE.Contract
                                                                    , objActividadBE.StartHour
                                                                    , objActividadBE.FinishHour
                                                                    , objActividadBE.Respirator
                                                                    , objActividadBE.Filter
                                                                    , objActividadBE.Quantity
                                                                    , objActividadBE.Supervisor
                                                                    , objActividadBE.Description
                                                                    , objActividadBE.Activo)
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
