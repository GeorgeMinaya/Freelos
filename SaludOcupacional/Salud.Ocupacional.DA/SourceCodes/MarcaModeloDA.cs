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
    public class MarcaModeloDA : IMarcaModeloDA
    {
        private BDSaludOcupacionalDataContext model;

        public MarcaModeloDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public MarcaModeloBE.Response Listar_MarcaModelo()
        {
            try
            {
                var ObjResult = from a in model.Respirator
                                where a.Activo.Equals(true)
                                select new MarcaModeloBE.ResponseMarcaModeloBE()
                                {
                                    ID          =   a.ID
                                    ,Brand      =	a.Brand
                                    ,Model      =	a.Model
                                    ,Activo     =   Convert.ToBoolean(a.Activo)
                                };
                MarcaModeloBE.Response Result = new MarcaModeloBE.Response()
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
        public MarcaModeloBE.Response BuscarMarcaModelo(int IdMarcaModelo)
        {
            try
            {
                var ObjResult = from a in model.Respirator
                                where a.ID.Equals(IdMarcaModelo)
                                && a.Activo.Equals(true)
                                select new MarcaModeloBE.ResponseMarcaModeloBE()
                                {
                                    ID          =   a.ID
                                    ,Brand      =	a.Brand
                                    ,Model      =	a.Model
                                    ,Activo     =   Convert.ToBoolean(a.Activo)
                                };
                MarcaModeloBE.Response Result = new MarcaModeloBE.Response()
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
        public Sp_Insert_MarcaModeloResult RegistrarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Insert_MarcaModelo(ObjRequestBE.Brand
                                                                   , ObjRequestBE.Model
                                                                   , ObjRequestBE.Activo)
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_MarcaModeloResult EliminarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE objMarcaModeloBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_MarcaModelo(objMarcaModeloBE.ID, objMarcaModeloBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_MarcaModeloResult ModificarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE ObjRequestBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Update_MarcaModelo(ObjRequestBE.ID
                                                                   , ObjRequestBE.Brand
                                                                   , ObjRequestBE.Model
                                                                   , ObjRequestBE.Activo)
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
