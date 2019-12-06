using ACL.MegaCentro.DALC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL.MegaCentro.BE;
using ACL.MegaCentro.DALC.MG27Proxy;
using ACL.MegaCentro.DALC.MG39Proxy;
using ACL.MegaCentro.DALC.MG40Proxy;
using ACL.MegaCentro.DM;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using IBM.Data.DB2.iSeries;
using System.Data;
using System.IO;

namespace ACL.MegaCentro.DALC.SourceCodes
{
    public class OrdenCargaDALC : IOrdenCargaDALC
    {
        private MegaCentroDataContext model;

        public OrdenCargaDALC() {
            this.model = new MegaCentroDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }

        public bool ValidarCentro(int IdCentro, string CentroGuia)
        {
            var query = from cs in model.Centro_Sistemas.Where(x => x.IdCentro.Equals(IdCentro))
                        where cs.Codigo.Equals(CentroGuia)
                        select cs;

            if (query.Count().Equals(0))
                return false;

            return true;
        }
        public bool TransportistaCancelar(int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaModifico = DateTime.Now;
                orden.Eliminado = true;
            }

            model.SubmitChanges();

            return true;
        }
        public bool Eliminar(int IdOrdenCarga, int IdUsuarioRegistro) {
            var query = from oca in model.OrdenCargas
                        where oca.IdOrdenCarga.Equals(IdOrdenCarga)
                        select oca;

            if (!query.Any())
                throw new ArgumentException("CUSTOM", new Exception("Error: No se puede eliminar la orden seleccionada"));

            var orden = query.Single();
            orden.Eliminado = true;
            orden.IdUsuarioModifico = IdUsuarioRegistro;
            orden.FechaModifico = DateTime.Now;
            model.SubmitChanges();

            return true;
        }

        #region Consultar Guia Transportista
        public OrdenCargaBE ConsultarGuia_PI(string CodigoCentro, string NumeroGuia)
        {
            using (var client = new MG39Proxy.SI_MG39_OrdenCargaT2_Sync_OutClient(Helpers.AppConfigHelper.GetBinding(),
               Helpers.AppConfigHelper.GetEndPoint(Resources.PI.ResourceManager.GetString("MG39_HTTP_" + ConfigurationManager.AppSettings["Enviroment"]))))
            {

                var orden = new DT_OrdenCargaT2_Req_Out()
                {
                    Header = new MG39Proxy.DT_HEADER()
                    {
                        Sender = "MOBILE",
                        Receiver = "BASIS",
                        SendDate = DateTime.Now,
                        Plant = CodigoCentro,
                        MessageType = "INT_MG_39",
                        MessageId = Resources.PI.MessageId
                    },
                    Body = new DT_OrdenCargaT2_Req_OutBody()
                    {
                        CentroGuia = CodigoCentro,
                        NumGuia = decimal.Parse(NumeroGuia),
                        NumGuiaSpecified = true
                    }
                };

                client.ClientCredentials.UserName.UserName = Resources.PI.Usuario;
                client.ClientCredentials.UserName.Password = Resources.PI.Contrasena;
                var response = client.SI_MG39_OrdenCargaT2_Sync_Out(orden);
                client.Close();

                if (!string.IsNullOrEmpty(response.Body.Error))
                    throw new ArgumentException("CUSTOM", new Exception(response.Body.Error));

                if(response.Body.Items.Count().Equals(0))
                    throw new ArgumentException("CUSTOM", new Exception("Error: La guía no contiene detalle. Por favor, lea otra página o comuniquese con soporte."));

                var correlativo = 0;
                var result = new OrdenCargaBE()
                {
                    //Body
                    Sender = response.Header.Sender,
                    Receiver = response.Header.Receiver,
                    Centro = response.Header.Plant,
                    MessageID = response.Header.MessageId,
                    MessageType = response.Header.MessageType,
                    //Header
                    Locacion = response.Body.Locacion,
                    NumeroCargaBasis = response.Body.NumCargaBasis,
                    NumeroCargaSipan = response.Body.NumCargaSipan,
                    NumeroCargaMixta = response.Body.NumCargaMixta,
                    CentroGuia = response.Body.CentroGuia,
                    NumeroGuia = response.Body.NumGuia.ToString(),
                    Sistema = response.Body.Sistema,
                    Error = response.Body.Error,
                    RutaEntrega = response.Body.DeliveryRoute,
                    FechaEntrega = response.Body.DeliveryDate,
                    Vehiculo = response.Body.Vehiculo,
                    Transportista = response.Body.Transportista,
                    NombreTransportista = response.Body.NombreTransp,
                    Viaje = response.Body.Viaje,
                    FromPI = true,
                    //Items
                    Items = (from i in response.Body.Items.Where(x => (x.Unidades > 0 || x.Subunidades > 0))
                             select new ItemBE()
                             {
                                 Orden = ++correlativo,
                                 TipoProducto = i.TipoProd,
                                 TipoVenta = string.IsNullOrEmpty(i.TipoVenta) ? "01" : i.TipoVenta,

                                 Material = response.Body.Sistema.Equals("B") ? i.SKUBasis.ToString() : response.Body.Sistema.Equals("S") ? i.SKUComercial : i.SKU,
                                 SKU = i.SKU,
                                 SKUBasis = i.SKUBasis,
                                 SKUComercial = i.SKUComercial,

                                 Descripcion = i.Descripcion,

                                 MaterialEnvase = response.Body.Sistema.Equals("B") ? i.SKUBasisEnvase.ToString() : response.Body.Sistema.Equals("S") ? i.SKUComercialEnvase : i.MaterialEnvase,
                                 SKUEnvase = i.MaterialEnvase,
                                 SKUComercialEnvase = i.SKUComercialEnvase,
                                 SKUBasisEnvase = i.SKUBasisEnvase,

                                 Secuencia = i.Secuencia,
                                 NumeroSuu = i.Numsuu,

                                 Unidad = i.Unidades,
                                 SubUnidad = i.Subunidades,
                                 UnidadOriginal = i.Unidades,
                                 SubUnidadOriginal = i.Subunidades,
                                 UnidadCambio = i.UnidCambios,
                                 SubUnidadCambio = i.SubuCambios,
                                 IdTipoMovimiento = 1
                             }).ToList()
                };

                var t2 = response.Body.Items.Where(x => (x.UnidCambios > 0 || x.SubuCambios > 0));
                var items = (from i in response.Body.Items.Where(x => x.UnidCambios > 0 || x.SubuCambios > 0)
                             select new ItemBE()
                             {
                                 Orden = ++correlativo,
                                 TipoProducto = i.TipoProd,
                                 TipoVenta = string.IsNullOrEmpty(i.TipoVenta) ? "01" : i.TipoVenta,

                                 Material = response.Body.Sistema.Equals("B") ? i.SKUBasis.ToString() : response.Body.Sistema.Equals("S") ? i.SKUComercial : i.SKU,
                                 SKU = i.SKU,
                                 SKUBasis = i.SKUBasis,
                                 SKUComercial = i.SKUComercial,

                                 Descripcion = i.Descripcion,


                                 MaterialEnvase = response.Body.Sistema.Equals("B") ? i.SKUBasisEnvase.ToString() : response.Body.Sistema.Equals("S") ? i.SKUComercialEnvase : i.MaterialEnvase,
                                 SKUEnvase = i.MaterialEnvase,
                                 SKUComercialEnvase = i.SKUComercialEnvase,
                                 SKUBasisEnvase = i.SKUBasisEnvase,

                                 Secuencia = i.Secuencia,
                                 NumeroSuu = i.Numsuu,

                                 Unidad = i.UnidCambios,
                                 SubUnidad = i.SubuCambios,
                                 UnidadOriginal = i.UnidCambios,
                                 SubUnidadOriginal = i.SubuCambios,
                                 UnidadCambio = i.UnidCambios,
                                 SubUnidadCambio = i.SubuCambios,
                                 IdTipoMovimiento = 2
                             }).ToList();

                result.Items = result.Items.Union(items).ToList();

                return result;
            }
        }
        public OrdenCargaBE ConsultarGuia_SQL(int IdOrdenCarga)
        {
            var datos = from ord in model.OrdenCargas
                        where ord.IdOrdenCarga.Equals(IdOrdenCarga) && 
                        ord.Eliminado.Equals(false) //&& !ord.Estado.Equals(7)
                        select ord;

            if(!datos.Any())
                throw new ArgumentException("CUSTOM", new Exception("No se encontro orden de carga"));

            if (datos.Single().Estado.Equals(7))
                throw new ArgumentException("CUSTOM", new Exception("La orden ya ha sido procesada"));

            var query = from o in model.OrdenCargaCabs
                        where o.Activo && o.IdOrdenCarga.Equals(IdOrdenCarga) && !o.Elimino
                        select new OrdenCargaBE()
                        {
                            IdOrdenCarga = o.IdOrdenCarga,
                            IdOrdenCargaCab = o.IdOrdenCargaCab,
                            Interface = o.Interface,
                            Centro = o.Centro,
                            MessageID = o.MessageID,
                            MessageType = o.MessageType,
                            Locacion = o.Location,
                            NumeroCargaBasis = o.NumeroCargaBasis,
                            NumeroCargaSipan = o.NumeroCargaSipan,
                            NumeroCargaMixta = o.NumeroCargaMixta,
                            CentroGuia = o.CentroGuia,
                            NumeroGuia = o.NumeroGuia.ToString(),
                            Sistema = o.Sistema,
                            Error = o.Error,
                            RutaEntrega = o.RutaEntrega,
                            FechaEntrega = o.FechaEntrega,
                            Vehiculo = o.Vehiculo,
                            Transportista = o.Transportista,
                            NombreTransportista = o.NombreTransportista,
                            Viaje = o.Viaje,
                            Receiver = o.Receiver,
                            Sender = o.Sender,
                            FromPI = false
                        };

            var orden = query.Single();
            var item = from i in model.OrdenCargaDets
                       where i.IdOrdenCargaCab.Equals(orden.IdOrdenCargaCab)
                       select new ItemBE()
                       {
                           IdRegistro = (int)i.IdRegistro,
                           Orden = (int)i.Orden,
                           IdOrdenCargaDet = i.IdOrdenCargaDet,
                           TipoProducto = i.TipoProducto,
                           TipoVenta = i.TipoVenta,
                           Material = orden.Sistema.Equals("B") ? i.SKUBasis.ToString() : orden.Sistema.Equals("S") ? i.SKUComercial : i.SKU,
                           SKU = i.SKU,
                           SKUBasis = (decimal)i.SKUBasis,
                           SKUComercial = i.SKUComercial,
                           Secuencia = (decimal)i.Secuencia,
                           Descripcion = i.Descripcion,
                           NumeroSuu = (decimal)i.NumeroSuu,
                           Unidad = (decimal)i.Unidad,
                           SubUnidad = (decimal)i.SubUnidad,
                           UnidadOriginal = (decimal)i.UnidadOriginal,
                           SubUnidadOriginal = (decimal)i.SubUnidadOriginal,
                           UnidadCambio = (decimal)i.UnidadCambio,
                           SubUnidadCambio = (decimal)i.SubUnidadCambio,
                           IdTipoMovimiento = i.IdTipoMovimiento,
                           MaterialEnvase = i.MaterialEnvase,
                           SKUEnvase = i.SKUEnvase,
                           SKUBasisEnvase = (decimal)i.SKUBasisEnvase,
                           SKUComercialEnvase = i.SKUComercialEnvase,
                           Revision = ((decimal)i.UnidadCambio > 0 || (decimal)i.SubUnidadCambio > 0) ? false : true,
                           //IdBackColor = (int)i.IdBackColor
                           CantOriginal = (int)i.CantOriginal,
                           CantCompra = (int)i.CantCompra,
                           CantPrestamo = (int)i.CantPrestamo,
                           CantConsignacion = (int)i.CantConsignacion,
                           CantValidar = (int)i.CantValidar
                       };
            try
            {
                orden.Items = item.ToList();
            }
            catch (Exception)
            {

                orden.Items = new List<ItemBE>();
            }
            

            return orden;
        }
        public OrdenCargaBE ConsultarGuia_AS400(string NumeroGuia)
        {
            StringReader cadena = new StringReader(string.Empty);
            iDB2Command command = new iDB2Command();
            string linea = string.Empty;
            int cantt1 = 1, cantt2 = 1;
            try
            {
                string CadenaConexion = ConfigurationManager.ConnectionStrings[string.Format("AS400Basis{0}", ConfigurationManager.AppSettings["Enviroment"])].ConnectionString;
                var conn = new iDB2Connection(CadenaConexion);
                conn.Open();
                command = new iDB2Command("IOEA404C", conn);
                command.CommandType = CommandType.StoredProcedure;

                iDB2Parameter request = new iDB2Parameter();
                request.ParameterName = "@MSJ";
                request.Size = 16;
                request.Value = NumeroGuia;
                request.DbType = DbType.String;
                request.Direction = ParameterDirection.Input;
                command.Parameters.Add(request);

                iDB2Parameter error = new iDB2Parameter();
                error.ParameterName = "@ERR";
                error.Size = 30;
                error.DbType = DbType.StringFixedLength;
                error.Direction = ParameterDirection.Output;
                command.Parameters.Add(error);

                iDB2Parameter result = new iDB2Parameter();
                result.ParameterName = "@MSJR";
                result.Size = 320000;
                result.Direction = ParameterDirection.Output;
                command.Parameters.Add(result);

                command.ExecuteNonQuery();

                if (!command.Parameters["@ERR"].Value.ToString().Trim().Equals(string.Empty))
                    throw new ArgumentException("CUSTOM", new Exception(command.Parameters["@ERR"].Value.ToString().Trim()));

                var primero = true;
                var objOrdenCargaBE = new OrdenCargaBE();
                cadena = new StringReader(command.Parameters["@MSJR"].Value.ToString());

                //cadena = new StringReader("INT_MG_39  IA000001222222000012222220IA  2907A             00043262                  3113019300093573S                      20180913F7A90102907HUAYTA MENDOZA JUAN 1" + Environment.NewLine +
                //"==LQ0125007001011800000000118IK 2250PET PQX6               000000000000000000000000083000600000000101000000000000000000606 " + Environment.NewLine +
                //"==LQ0125163701011501000000158IK 3 LT PFM PQ*4              000000000000000000000000597000400000000060000000000000000000000 " + Environment.NewLine +
                //"==OT01      01011304000000167                              00000000000000000000000200160000000000006}000000000000000000000 " + Environment.NewLine +
                //"==OT0140051698210101000007806PARIH.MADERA 1.25 X 1.05      000000000000000000000002346900100000000000000300000000000000000 ");

                while (null != (linea = cadena.ReadLine()))
                {
                    if (linea != null)
                    {
                        if (primero)
                        {
                            objOrdenCargaBE.Interface = linea.Substring(0, 11);
                            objOrdenCargaBE.Centro = linea.Substring(11, 4);
                            objOrdenCargaBE.MessageID = linea.Substring(15, 11);
                            objOrdenCargaBE.Locacion = linea.Substring(37, 4);
                            objOrdenCargaBE.NumeroCargaBasis = linea.Substring(41, 18);
                            objOrdenCargaBE.NumeroCargaSipan = string.IsNullOrEmpty(linea.Substring(59, 8).Trim()) ? 0 : decimal.Parse(linea.Substring(59, 8));
                            objOrdenCargaBE.NumeroCargaMixta = linea.Substring(67, 18);
                            objOrdenCargaBE.CentroGuia = linea.Substring(85, 4);
                            objOrdenCargaBE.NumeroGuia = linea.Substring(89, 12);
                            objOrdenCargaBE.Sistema = linea.Substring(101, 1);
                            objOrdenCargaBE.Error = linea.Substring(102, 50);
                            objOrdenCargaBE.RutaEntrega = linea.Substring(152, 5);
                            objOrdenCargaBE.FechaEntrega = linea.Substring(157, 8);
                            objOrdenCargaBE.Vehiculo = linea.Substring(165, 6);
                            objOrdenCargaBE.Transportista = linea.Substring(171, 5);
                            objOrdenCargaBE.NombreTransportista = linea.Substring(176, 20);
                            objOrdenCargaBE.Viaje = linea.Substring(196, 1);
                            primero = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(linea.Trim()))
                            {
                                var Unidad = decimal.Parse(linea.Substring(90, 11));
                                var SubUnidad = decimal.Parse(linea.Substring(101, 5));
                                var UnidadCambio = decimal.Parse(linea.Substring(106, 11));
                                var SubUnidadCambio = decimal.Parse(linea.Substring(117, 5));

                                if (Unidad > 0 || SubUnidad > 0)
                                {
                                    var material = objOrdenCargaBE.Sistema.Equals("B") ? linea.Substring(20, 9) : objOrdenCargaBE.Sistema.Equals("S") ? linea.Substring(12, 8) : linea.Substring(6, 6);

                                    var item1 = new ItemBE()
                                    {
                                        TipoProducto = linea.Substring(2, 2),
                                        TipoVenta = string.IsNullOrEmpty(linea.Substring(4, 2)) ? "01" : linea.Substring(4, 2),
                                        Material = $"{decimal.Parse(material)}",
                                        SKU = linea.Substring(6, 6),
                                        SKUComercial = linea.Substring(12, 8),
                                        SKUBasis = decimal.Parse(linea.Substring(20, 9)),
                                        Descripcion = linea.Substring(29, 30),
                                        MaterialEnvase = objOrdenCargaBE.Sistema.Equals("B") ? linea.Substring(73, 9) : objOrdenCargaBE.Sistema.Equals("S") ? linea.Substring(65, 8) : linea.Substring(59, 6),
                                        SKUEnvase = linea.Substring(59, 6),
                                        SKUComercialEnvase = linea.Substring(65, 8),
                                        SKUBasisEnvase = decimal.Parse(linea.Substring(73, 9)),
                                        Secuencia = decimal.Parse(linea.Substring(82, 5)),
                                        NumeroSuu = decimal.Parse(linea.Substring(87, 3))
                                    };
                                    item1.Unidad = Unidad;
                                    item1.SubUnidad = SubUnidad;
                                    item1.UnidadCambio = 0;
                                    item1.SubUnidadCambio = 0;
                                    item1.Orden = cantt1;
                                    item1.IdTipoMovimiento = 1;
                                    objOrdenCargaBE.Items.Add(item1);
                                    cantt1++;
                                }

                                if (UnidadCambio > 0 || SubUnidadCambio > 0)
                                {
                                    var material2 = objOrdenCargaBE.Sistema.Equals("B") ? linea.Substring(20, 9) : objOrdenCargaBE.Sistema.Equals("S") ? linea.Substring(12, 8) : linea.Substring(6, 6);

                                    var item2 = new ItemBE()
                                    {
                                        TipoProducto = linea.Substring(2, 2),
                                        TipoVenta = string.IsNullOrEmpty(linea.Substring(4, 2)) ? "01" : linea.Substring(4, 2),
                                        Material = $"{decimal.Parse(material2)}",
                                        SKU = linea.Substring(6, 6),
                                        SKUComercial = linea.Substring(12, 8),
                                        SKUBasis = decimal.Parse(linea.Substring(20, 9)),
                                        Descripcion = linea.Substring(29, 30),
                                        MaterialEnvase = objOrdenCargaBE.Sistema.Equals("B") ? linea.Substring(73, 9) : objOrdenCargaBE.Sistema.Equals("S") ? linea.Substring(65, 8) : linea.Substring(59, 6),
                                        SKUEnvase = linea.Substring(59, 6),
                                        SKUComercialEnvase = linea.Substring(65, 8),
                                        SKUBasisEnvase = decimal.Parse(linea.Substring(73, 9)),
                                        Secuencia = decimal.Parse(linea.Substring(82, 5)),
                                        NumeroSuu = decimal.Parse(linea.Substring(87, 3))
                                    };
                                    item2.Unidad = UnidadCambio;
                                    item2.SubUnidad = SubUnidadCambio;
                                    item2.UnidadCambio = UnidadCambio;
                                    item2.SubUnidadCambio = SubUnidadCambio;
                                    item2.Orden = cantt2;
                                    item2.IdTipoMovimiento = 2;
                                    objOrdenCargaBE.Items.Add(item2);
                                    cantt2++;
                                }
                            }
                        }
                    }
                    else
                        break;
                }

                if (objOrdenCargaBE.Items.Count.Equals(0))
                    throw new ArgumentException("CUSTOM", new Exception("ERROR en la guía " + NumeroGuia + ": " + command.Parameters["@MSJR"].Value.ToString()));

                objOrdenCargaBE.Items = objOrdenCargaBE.Items.OrderBy(x => x.Secuencia).OrderBy(x => x.IdTipoMovimiento).ToList();
                return objOrdenCargaBE;

            }
            catch (iDB2Exception exiseries)
            {
                //var modelmail = new MailMegaCentroDataContext(ConfigurationManager.ConnectionStrings["MAIL" + ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
                //modelmail.SEND_MAIL_MEGA(exiseries.Message, NumeroGuia.Substring(0, 4), cantt1, NumeroGuia, cadena.ToString());
                throw exiseries;
            }
            catch (Exception ex)
            {
                if (!ex.Message.Equals("CUSTOM"))
                {
                    var modelmail = new MailMegaCentroDataContext(ConfigurationManager.ConnectionStrings["MAIL" + ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
                    string NombreCentro = (from cs in model.Centro_Sistemas
                                           join c in model.Centros
                                           on cs.IdCentro equals c.IdCentro
                                           where cs.Codigo.Equals(NumeroGuia.Substring(0, 4))
                                           select c.Nombre).Single();
                    modelmail.SEND_MAIL_MEGA(ex.Message, NombreCentro, cantt1, NumeroGuia, linea.Trim());
                }
                throw ex;
            }
        }
        public OrdenCargaBE Insertar(OrdenCargaBE objOrdenCargaBE)
        {
            var orden = new OrdenCargaCab();

            orden.IdOrdenCarga = (int)objOrdenCargaBE.IdOrdenCarga;
            orden.Receiver = objOrdenCargaBE.Receiver;
            orden.Sender = objOrdenCargaBE.Sender;
            orden.Centro = objOrdenCargaBE.Centro;
            orden.MessageID = objOrdenCargaBE.MessageID;
            orden.MessageType = objOrdenCargaBE.MessageType;
            orden.Location = objOrdenCargaBE.Locacion;
            orden.NumeroCargaBasis = objOrdenCargaBE.NumeroCargaBasis;
            orden.NumeroCargaSipan = objOrdenCargaBE.NumeroCargaSipan;
            orden.NumeroCargaMixta = objOrdenCargaBE.NumeroCargaMixta;
            orden.CentroGuia = objOrdenCargaBE.CentroGuia;
            orden.NumeroGuia = decimal.Parse(objOrdenCargaBE.NumeroGuia);
            orden.Sistema = objOrdenCargaBE.Sistema;
            orden.Error = objOrdenCargaBE.Error;
            orden.RutaEntrega = objOrdenCargaBE.RutaEntrega;
            orden.FechaEntrega = objOrdenCargaBE.FechaEntrega;
            orden.Vehiculo = objOrdenCargaBE.Vehiculo;
            orden.Transportista = objOrdenCargaBE.Transportista;
            orden.NombreTransportista = objOrdenCargaBE.NombreTransportista;
            orden.Viaje = objOrdenCargaBE.Viaje;
            orden.IdUsuarioRegistro = (int)objOrdenCargaBE.IdUsuarioRegistro;
            orden.FechaRegistro = DateTime.Now;
            orden.IdUsuarioModifico = (int)objOrdenCargaBE.IdUsuarioRegistro;
            orden.FechaModifico = DateTime.Now;
            orden.Activo = true;
            orden.Elimino = false;

            model.OrdenCargaCabs.InsertOnSubmit(orden);
            model.SubmitChanges();

            //int x = 0;
            //var items = objOrdenCargaBE.Items;
            foreach (ItemBE objItemBE in objOrdenCargaBE.Items)
            {
                var item = new OrdenCargaDet()
                {
                    Orden = objItemBE.Orden,
                    IdRegistro = objItemBE.IdRegistro,
                    IdOrdenCargaCab = orden.IdOrdenCargaCab,
                    TipoProducto = objItemBE.TipoProducto,
                    TipoVenta = objItemBE.TipoVenta,
                    SKU = objItemBE.SKU,
                    SKUBasis = objItemBE.SKUBasis,
                    SKUComercial = objItemBE.SKUComercial,
                    Secuencia = objItemBE.Secuencia,
                    Descripcion = objItemBE.Descripcion,
                    NumeroSuu = objItemBE.NumeroSuu,
                    Unidad = objItemBE.Unidad,
                    SubUnidad = objItemBE.SubUnidad,
                    UnidadOriginal = objItemBE.UnidadOriginal,
                    SubUnidadOriginal = objItemBE.SubUnidadOriginal,
                    UnidadCambio = objItemBE.UnidadCambio,
                    SubUnidadCambio = objItemBE.SubUnidadCambio,
                    UnidadCambioTrans = objItemBE.UnidadCambio,
                    SubUnidadCambioTrans = objItemBE.SubUnidadCambio,
                    IdTipoMovimiento = objItemBE.IdTipoMovimiento,
                    MaterialEnvase = objItemBE.MaterialEnvase,
                    SKUEnvase = objItemBE.SKUEnvase,
                    SKUComercialEnvase = objItemBE.SKUComercialEnvase,
                    SKUBasisEnvase = objItemBE.SKUBasisEnvase,
                    //IdBackColor = objItemBE.IdBackColor,
                    CantOriginal = objItemBE.CantOriginal,
                    CantCompra = objItemBE.CantCompra,
                    CantPrestamo = objItemBE.CantPrestamo,
                    CantConsignacion = objItemBE.CantConsignacion,
                    CantValidar = objItemBE.CantValidar,
                    IdUsuarioRegistro = (int)objOrdenCargaBE.IdUsuarioRegistro,
                    FechaRegistro = DateTime.Now,
                    IdUsuarioModifico = (int)objOrdenCargaBE.IdUsuarioRegistro,
                    FechaModifico = DateTime.Now,
                    Activo = true,
                    Elimino = false
                };

                model.OrdenCargaDets.InsertOnSubmit(item);
                model.SubmitChanges();

                objItemBE.IdOrdenCargaDet = item.IdOrdenCargaDet;
            }
            objOrdenCargaBE.IdOrdenCargaCab = orden.IdOrdenCargaCab;
            return objOrdenCargaBE;
        }
        public int TransportistaIniciar(int IdUsuarioTransportista, string CodigoCentro, string NumeroGuia)
        {
            NumeroGuia = decimal.Parse(NumeroGuia).ToString();
            int IdOrdenCarga = 0;

            var query = from o in model.OrdenCargas
                        where o.NumeroGuia.Equals(NumeroGuia) && o.Eliminado.Equals(false)
                        select o;

            if (!query.Count().Equals(0))
            {
                foreach (OrdenCarga orden in query)
                {
                    IdOrdenCarga = orden.IdOrdenCarga;
                    orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioLiquidos);
                    orden.IdUsuarioTransportista = IdUsuarioTransportista;
                    orden.FechaInicioLiquido = DateTime.Now;
                    orden.IdUsuarioModifico = IdUsuarioTransportista;
                    orden.FechaModifico = DateTime.Now;
                }
                model.SubmitChanges();
            }
            else
            {
                var orden = new OrdenCarga()
                {
                    NumeroGuia = NumeroGuia,
                    CodigoCentro = CodigoCentro,
                    IdUsuarioTransportista = IdUsuarioTransportista,
                    FechaInicioLiquido = DateTime.Now,
                    IdUsuarioRegistro = IdUsuarioTransportista,
                    FechaRegistro = DateTime.Now,
                    IdUsuarioModifico = IdUsuarioTransportista,
                    FechaModifico = DateTime.Now,
                    Estado = int.Parse(Resources.OrdenCargaEstado.InicioLiquidos),
                    Eliminado = false
                };

                model.OrdenCargas.InsertOnSubmit(orden);
                model.SubmitChanges();

                IdOrdenCarga = orden.IdOrdenCarga;
            }

            return IdOrdenCarga;
        }
        public int BuscarPorNumeroGuia(string NumeroGuia) {
            var query = from ord in model.OrdenCargas
                        where ord.NumeroGuia.Equals(NumeroGuia) && ord.Eliminado.Equals(false)
                        select ord.IdOrdenCarga;

            if (query.Count() > 1)
                throw new ArgumentException("CUSTOM", new Exception("La orden contiene errores en la base de datos SQL"));
            else {
                if (query.Count().Equals(1))
                    return query.Single();
                else
                    return 0;
            }
        }
        #endregion

        #region Registro de la guia Transportista
        public bool EnvaseIniciar(int IdUsuarioTransportista, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaInicioEnvase = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioTransportista;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioEnvaces);
            }

            model.SubmitChanges();

            return true;
        }
        public bool OtrosIniciar(int IdUsuarioTransportista, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaInicioOtros = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioTransportista;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioOtros);
            }

            model.SubmitChanges();

            return true;
        }
        public bool CambioIniciar(int IdUsuarioTransportista, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaInicioCambios = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioTransportista;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioCambios);
            }

            model.SubmitChanges();

            return true;
        }
        public bool NoPlanIniciar(int IdUsuarioTransportista, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaInicioNoPlan = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioTransportista;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioNoPlanificados);
            }

            model.SubmitChanges();

            return true;
        }
        public void TransportistaActualizar(OrdenCargaBE objOrdenCargaBE)
        {
            var query = from c in model.OrdenCargaCabs.Where(x => x.IdOrdenCargaCab.Equals(objOrdenCargaBE.IdOrdenCargaCab))
                        select c;

            foreach (OrdenCargaCab cab in query)
            {
                cab.IdUsuarioModifico = (int)objOrdenCargaBE.IdUsuarioRegistro;
                cab.FechaModifico = DateTime.Now;
            }

            var items = from i in model.OrdenCargaDets.Where(x => x.IdOrdenCargaCab.Equals(objOrdenCargaBE.IdOrdenCargaCab))
                        select i;

            foreach (OrdenCargaDet det in items)
            {
                var item = objOrdenCargaBE.Items.Find(x => x.IdOrdenCargaDet.Equals(det.IdOrdenCargaDet));
                det.UnidadCambio = item.UnidadCambio;
                det.SubUnidadCambio = item.SubUnidadCambio;
                det.UnidadCambioTrans = item.UnidadCambio;
                det.SubUnidadCambioTrans = item.SubUnidadCambio;
                det.IdUsuarioModifico = (int)objOrdenCargaBE.IdUsuarioRegistro;
                det.FechaModifico = DateTime.Now;
            }

            model.SubmitChanges();
        }
        public bool TransportistaFinalizar(int IdUsuarioTransportista, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaFinalRegistro = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioTransportista;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.FinalizoTransporte);
            }

            model.SubmitChanges();

            return true;
        }
        public bool LiquidoIniciar(int IdUsuarioTransportista, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaInicioEnvase = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioTransportista;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioLiquidos);
            }

            model.SubmitChanges();

            return true;
        }
        #endregion

        #region Registro de la guia Inspector
        public IEnumerable<OrdenCargaBE> ListarTodosPendientes()
        {
            var query = from o in model.OrdenCargas.Where(x => !(bool)x.Eliminado)
                        join c in model.OrdenCargaCabs on o.IdOrdenCarga equals c.IdOrdenCarga
                        where o.Estado.Equals(Resources.OrdenCargaEstado.FinalizoTransporte) || o.Estado.Equals(Resources.OrdenCargaEstado.InicioSupervisor)
                        orderby o.FechaFinalRegistro descending
                        select new OrdenCargaBE()
                        {
                            IdOrdenCarga = o.IdOrdenCarga,
                            IdOrdenCargaCab = c.IdOrdenCargaCab,
                            NumeroGuia = o.NumeroGuia,
                            Transportista = c.Transportista,
                            NombreTransportista = c.NombreTransportista,
                            Vehiculo = c.Vehiculo,
                            Estado = (int)o.Estado,
                            EstadoDescripcion = o.Estado.Equals(int.Parse(Resources.OrdenCargaEstado.FinalizoTransporte)) ? "Pendiente" : "En revisión",
                            FechaFinalRegistro = o.FechaFinalRegistro
                        };

            return query;
        }

        public IEnumerable<OrdenCargaBE> ListarPendientes(string centro)
        {
            var query = from o in model.OrdenCargas.Where(x => !(bool)x.Eliminado)
                        join c in model.OrdenCargaCabs.Where(x => x.CentroGuia.Equals(centro)) on o.IdOrdenCarga equals c.IdOrdenCarga
                        where o.Estado.Equals(Resources.OrdenCargaEstado.FinalizoTransporte) || o.Estado.Equals(Resources.OrdenCargaEstado.InicioSupervisor)
                        orderby o.FechaFinalRegistro descending
                        select new OrdenCargaBE()
                        {
                            IdOrdenCarga = o.IdOrdenCarga,
                            IdOrdenCargaCab = c.IdOrdenCargaCab,
                            NumeroGuia = o.NumeroGuia,
                            Transportista = c.Transportista,
                            NombreTransportista = c.NombreTransportista,
                            Vehiculo = c.Vehiculo,
                            Estado = (int)o.Estado,
                            EstadoDescripcion = o.Estado.Equals(int.Parse(Resources.OrdenCargaEstado.FinalizoTransporte)) ? "Pendiente" : "En revisión",
                            FechaFinalRegistro = o.FechaFinalRegistro
                        };

            return query;
        }

        public bool InspectorIniciar(int IdUsuarioSupervisor, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.InicioSupervisor);
                orden.FechaInicioSupervision = DateTime.Now;
                orden.IdUsuarioSupervisor = IdUsuarioSupervisor;
                orden.IdUsuarioModifico = IdUsuarioSupervisor;
                orden.FechaModifico = DateTime.Now;
            }

            model.SubmitChanges();

            return true;
        }
        public bool InspectorFinalizar(int IdUsuarioSupervisor, int IdOrdenCarga)
        {
            var query = from o in model.OrdenCargas
                        where o.IdOrdenCarga.Equals(IdOrdenCarga)
                        select o;

            if (query.Count().Equals(0))
                throw new ArgumentException("CUSTOM", new Exception("No se encontró Orden de Carga"));

            foreach (OrdenCarga orden in query)
            {
                orden.FechaFinalSupervision = DateTime.Now;
                orden.IdUsuarioModifico = IdUsuarioSupervisor;
                orden.FechaModifico = DateTime.Now;
                orden.Estado = int.Parse(Resources.OrdenCargaEstado.FinalizoSupervisor);
                if (orden.IdUsuarioSupervisor.Equals(null))
                    orden.IdUsuarioSupervisor = IdUsuarioSupervisor;
            }

            model.SubmitChanges();

            return true;
        }
        public void InspectorActualizar(OrdenCargaBE objOrdenCargaBE)
        {
            var query = from c in model.OrdenCargaCabs.Where(x => x.IdOrdenCargaCab.Equals(objOrdenCargaBE.IdOrdenCargaCab))
                        select c;

            foreach (OrdenCargaCab cab in query)
            {
                cab.IdUsuarioModifico = (int)objOrdenCargaBE.IdUsuarioRegistro;
                cab.FechaModifico = DateTime.Now;
            }

            var items = from i in model.OrdenCargaDets.Where(x => x.IdOrdenCargaCab.Equals(objOrdenCargaBE.IdOrdenCargaCab))
                        select i;

            foreach (OrdenCargaDet det in items)
            {
                var item = objOrdenCargaBE.Items.Find(x => x.IdOrdenCargaDet.Equals(det.IdOrdenCargaDet));
                det.Unidad = item.Unidad;
                det.SubUnidad = item.SubUnidad;
                det.UnidadCambio = item.UnidadCambio;
                det.SubUnidadCambio = item.SubUnidadCambio;
                det.IdUsuarioModifico = (int)objOrdenCargaBE.IdUsuarioRegistro;
                det.FechaModifico = DateTime.Now;
            }

            model.SubmitChanges();
        }
        public void BorealEnviar(OrdenCargaBE objOrdenCargaBE)
        {
            using (var client = new SI_MG27_DevolucionT2_Req_Async_OutClient(Helpers.AppConfigHelper.GetBinding(),
                Helpers.AppConfigHelper.GetEndPoint(Resources.PI.ResourceManager.GetString("MG27_HTTP_" + ConfigurationManager.AppSettings["Enviroment"]))))
            {
                client.ClientCredentials.UserName.UserName = Resources.PI.Usuario;
                client.ClientCredentials.UserName.Password = Resources.PI.Contrasena;

                var items = from i in objOrdenCargaBE.Items.Where(x => (x.UnidadCambio > 0 || x.SubUnidadCambio > 0) && !x.TipoProducto.Equals("OT"))
                            group i by new
                            {
                                i.SKU,
                                i.IdTipoMovimiento
                            } into g
                            select new DT_DevolucionT2_ReqBodyDetail()
                            {
                                SKU = g.Key.SKU,
                                StockType = g.Key.IdTipoMovimiento.Equals(1) ? "PTER" : "PNC",
                                PacksQty = g.Where(i => i.IdTipoMovimiento.Equals(g.Key.IdTipoMovimiento) && i.SKU.Equals(g.Key.SKU)).Sum(i => Convert.ToInt32(i.UnidadCambio)).ToString(),
                                RemainingUnitsQty = g.Where(i => i.IdTipoMovimiento.Equals(g.Key.IdTipoMovimiento) && i.SKU.Equals(g.Key.SKU)).Sum(i => Convert.ToInt32(i.SubUnidadCambio)).ToString()
                            };

                var orden = new DT_DevolucionT2_Req()
                {
                    Header = new MG27Proxy.DT_HEADER()
                    {
                        MessageId = objOrdenCargaBE.IdOrdenCarga.ToString(),
                        MessageType = "INT_MG_27",
                        Plant = objOrdenCargaBE.CentroGuia,
                        Sender = "MOBIL",
                        Receiver = "BOREAL",
                        SendDate = DateTime.Now
                    },
                    Body = new DT_DevolucionT2_ReqBody()
                    {
                        OperationCode = "RETURNS_FROM_DISTRIBUTION",
                        ReferenceId = objOrdenCargaBE.Vehiculo,
                        Details = items.ToArray()
                    },
                };

                try
                {
                    client.SI_MG27_DevolucionT2_Req_Async_Out(orden);
                }
                catch (Exception ex) { }
            }
        }
        public bool ComercialEnviar(OrdenCargaBE objOrdenCargaBE)
        {
            using (var client = new SI_MG40_DescargaOrdenCargaT2_Req_Async_OutClient(Helpers.AppConfigHelper.GetBinding(),
                Helpers.AppConfigHelper.GetEndPoint(Resources.PI.ResourceManager.GetString("MG40_HTTP_" + ConfigurationManager.AppSettings["Enviroment"]))))
            {
                var items = from i in objOrdenCargaBE.Items.Where(x => x.UnidadCambio > 0 || x.SubUnidadCambio > 0)
                            group i by new
                            {
                                i.SKU
                            } into g
                            select new DT_DescargaOrdenCargaT2_Req_OutBodyItem()
                            {
                                SKU = g.Key.SKU,
                                Cajas = Convert.ToInt32(g.Where(i => i.IdTipoMovimiento.Equals(1)).Sum(i => i.UnidadCambio)),
                                Botellas = Convert.ToInt32(g.Where(i => i.IdTipoMovimiento.Equals(1)).Sum(i => i.SubUnidadCambio)),
                                CajasCambios = Convert.ToInt32(g.Where(i => i.IdTipoMovimiento.Equals(2) && objOrdenCargaBE.Sistema.Equals("S")).Sum(i => i.UnidadCambio)),
                                BotellasCambios = Convert.ToInt32(g.Where(i => i.IdTipoMovimiento.Equals(2) && objOrdenCargaBE.Sistema.Equals("S")).Sum(i => i.SubUnidadCambio)),
                                CajasSpecified = true,
                                BotellasSpecified = true,
                                CajasCambiosSpecified = true,
                                BotellasCambiosSpecified = true
                            };

                var obj = new DT_DescargaOrdenCargaT2_Req_Out()
                {
                    Header = new MG40Proxy.DT_HEADER()
                    {
                        Sender = "MOBILE",
                        Receiver = "COMERCIAL",
                        MessageType = "INT_MG_40",
                        MessageId = objOrdenCargaBE.IdOrdenCarga.ToString(),
                        Plant = objOrdenCargaBE.Centro,
                        SendDate = DateTime.Now
                    },
                    Body = new DT_DescargaOrdenCargaT2_Req_OutBody()
                    {
                        //Locacion = objOrdenCargaBE.Locacion,
                        CentroGuia = objOrdenCargaBE.CentroGuia,
                        NumGuia = decimal.Parse(objOrdenCargaBE.NumeroGuia),
                        //NumGuiaSpecified = true,
                        Items = items.Where(x => x.Cajas > 0 || x.Botellas > 0 || x.CajasCambios > 0 || x.BotellasCambios > 0).ToArray(),
                        Sistema = objOrdenCargaBE.Sistema,
                        NumCargaBasis = string.IsNullOrEmpty(objOrdenCargaBE.NumeroCargaBasis) ? string.Empty : objOrdenCargaBE.NumeroCargaBasis,
                        NumCargaMixta = string.IsNullOrEmpty(objOrdenCargaBE.NumeroCargaMixta) ? string.Empty : objOrdenCargaBE.NumeroCargaMixta,
                        NumCargaSipan = objOrdenCargaBE.NumeroCargaSipan == null ? 0 : Convert.ToInt32((decimal)objOrdenCargaBE.NumeroCargaSipan)
                    }
                };

                client.ClientCredentials.UserName.UserName = Resources.PI.Usuario;
                client.ClientCredentials.UserName.Password = Resources.PI.Contrasena;

                try
                {
                    client.SI_MG40_DescargaOrdenCargaT2_Req_Async_Out(obj);
                }
                catch (Exception ex)
                {

                }

                return true;
            }
        }
        #endregion

        #region Reporte de liquidacion
        public IEnumerable<OrdenCargaBE> ListarFinalizados(int IdCentro, string FechaInicio, string FechaFinal)
        {
            var query = from oc in model.OrdenCargas
                        join occ in model.OrdenCargaCabs.Where(x => x.FechaEntrega.CompareTo(FechaInicio) >= 0 && FechaFinal.CompareTo(x.FechaEntrega) >= 0) on oc.IdOrdenCarga equals occ.IdOrdenCarga
                        join usu in model.Usuarios on oc.IdUsuarioSupervisor equals usu.IdUsuario
                        join csi in model.Centro_Sistemas on oc.CodigoCentro equals csi.Codigo
                        join cen in model.Centros on csi.IdCentro equals cen.IdCentro
                        where oc.Estado.Equals(7) && !(bool)oc.Eliminado && cen.IdCentro.Equals(IdCentro)
                        orderby occ.FechaEntrega descending
                        select new OrdenCargaBE()
                        {
                            Centro = oc.CodigoCentro,
                            NombreCentro = cen.Nombre,
                            NumeroGuia = oc.NumeroGuia,
                            IdOrdenCarga = oc.IdOrdenCarga,
                            IdOrdenCargaCab = occ.IdOrdenCargaCab,
                            FechaEntrega = occ.FechaEntrega,
                            FechaInicioLiquido = oc.FechaInicioLiquido,
                            Vehiculo = occ.Vehiculo,
                            Transportista = occ.Transportista,
                            NombreTransportista = occ.NombreTransportista,
                            FechaInicioSupervision = oc.FechaInicioSupervision,
                            UsuarioLoginSupervisor = usu.NombreLogin,
                            NombreSupervisor = usu.NombreCompleto
                        };

            return query;
        }
        public OrdenCargaBE BuscarDiferencias(int IdOrdenCarga)
        {
            var query = from oc in model.OrdenCargas.Where(x => x.IdOrdenCarga.Equals(IdOrdenCarga) && x.Eliminado.Equals(false))
                        join occ in model.OrdenCargaCabs on oc.IdOrdenCarga equals occ.IdOrdenCarga
                        join use in model.Usuarios on oc.IdUsuarioSupervisor equals use.IdUsuario
                        join csi in model.Centro_Sistemas on oc.CodigoCentro equals csi.Codigo
                        join cen in model.Centros on csi.IdCentro equals cen.IdCentro
                        select new OrdenCargaBE()
                        {
                            NumeroGuia = oc.NumeroGuia,
                            Transportista = occ.Transportista,
                            NombreTransportista = occ.NombreTransportista,
                            FechaEntrega = occ.FechaEntrega,
                            NombreSupervisor = use.NombreCompleto,
                            Centro = csi.Codigo,
                            NombreCentro = cen.Nombre,
                            IdOrdenCargaCab = occ.IdOrdenCargaCab,
                            IdOrdenCarga = oc.IdOrdenCarga,
                            Sistema = occ.Sistema
                        };

            var orden = new OrdenCargaBE();

            if (query.Any())
            {
                orden = query.Single();

                var items = from it in model.OrdenCargaDets
                            where it.IdOrdenCargaCab.Equals(orden.IdOrdenCargaCab) && (it.UnidadCambio > 0 || it.SubUnidadCambio > 0)
                            select new ItemBE()
                            {
                                Material = orden.Sistema.Equals("B") ? it.SKUBasis.ToString() : orden.Sistema.Equals("S") ? it.SKUComercial : it.SKU,
                                Descripcion = it.Descripcion,
                                Unidad = (decimal)it.Unidad,
                                SubUnidad = (decimal)it.SubUnidad,
                                UnidadCambio = (decimal)it.UnidadCambio,
                                SubUnidadCambio = (decimal)it.SubUnidadCambio,
                                UnidadCambioTrans = (decimal)it.UnidadCambioTrans,
                                SubUnidadCambioTrans = (decimal)it.SubUnidadCambioTrans,
                                IdTipoMovimiento = it.IdTipoMovimiento,
                                TipoProducto = it.TipoProducto,
                                TipoVenta = it.TipoVenta
                            };

                orden.Items = items.ToList();
            }
            else
            {
                var querybak = from oc in model.OrdenCarga1s.Where(x => x.IdOrdenCarga.Equals(IdOrdenCarga) && x.Eliminado.Equals(false))
                            join occ in model.OrdenCargaCab1s on oc.IdOrdenCarga equals occ.IdOrdenCarga
                            join use in model.Usuarios on oc.IdUsuarioSupervisor equals use.IdUsuario
                            join csi in model.Centro_Sistemas on oc.CodigoCentro equals csi.Codigo
                            join cen in model.Centros on csi.IdCentro equals cen.IdCentro
                            select new OrdenCargaBE()
                            {
                                NumeroGuia = oc.NumeroGuia,
                                Transportista = occ.Transportista,
                                NombreTransportista = occ.NombreTransportista,
                                FechaEntrega = occ.FechaEntrega,
                                NombreSupervisor = use.NombreCompleto,
                                Centro = csi.Codigo,
                                NombreCentro = cen.Nombre,
                                IdOrdenCargaCab = occ.IdOrdenCargaCab,
                                IdOrdenCarga = oc.IdOrdenCarga,
                                Sistema = occ.Sistema
                            };

                orden = querybak.Single();

                var items = from it in model.OrdenCargaDet1s
                            where it.IdOrdenCargaCab.Equals(orden.IdOrdenCargaCab) && (it.UnidadCambio > 0 || it.SubUnidadCambio > 0)
                            select new ItemBE()
                            {
                                Material = orden.Sistema.Equals("B") ? it.SKUBasis.ToString() : orden.Sistema.Equals("S") ? it.SKUComercial : it.SKU,
                                Descripcion = it.Descripcion,
                                Unidad = (decimal)it.Unidad,
                                SubUnidad = (decimal)it.SubUnidad,
                                UnidadCambio = (decimal)it.UnidadCambio,
                                SubUnidadCambio = (decimal)it.SubUnidadCambio,
                                UnidadCambioTrans = (decimal)it.UnidadCambioTrans,
                                SubUnidadCambioTrans = (decimal)it.SubUnidadCambioTrans,
                                IdTipoMovimiento = it.IdTipoMovimiento,
                                TipoProducto = it.TipoProducto,
                                TipoVenta = it.TipoVenta
                            };

                orden.Items = items.ToList();
            }            

            return orden;
        }
        public OrdenCargaBE CargoTransportista(int IdOrdenCarga)
        {
            var query = from oc in model.OrdenCargas.Where(x => x.IdOrdenCarga.Equals(IdOrdenCarga))
                        join occ in model.OrdenCargaCabs on oc.IdOrdenCarga equals occ.IdOrdenCarga
                        join usu in model.Usuarios on oc.IdUsuarioSupervisor equals usu.IdUsuario
                        select new OrdenCargaBE()
                        {
                            IdOrdenCarga = oc.IdOrdenCarga,
                            IdOrdenCargaCab = occ.IdOrdenCargaCab,
                            NombreSupervisor = usu.NombreCompleto,
                            FechaFinalSupervision = oc.FechaFinalSupervision,
                            Transportista = occ.NombreTransportista,
                            Vehiculo = occ.Vehiculo,
                            NumeroGuia = oc.NumeroGuia,
                            Sistema = occ.Sistema
                        };

            var orden = query.Single();

            var items = from ocd in model.OrdenCargaDets.Where(x => x.IdOrdenCargaCab.Equals(orden.IdOrdenCargaCab))
                        where ocd.UnidadCambio > 0 || ocd.SubUnidadCambio > 0
                        select new ItemBE()
                        {
                            Material = orden.Sistema.Equals("B") ? ocd.SKUBasis.ToString() : orden.Sistema.Equals("S") ? ocd.SKUComercial : ocd.SKU,
                            Descripcion = ocd.Descripcion,
                            Unidad = (decimal)ocd.Unidad,
                            SubUnidad = (decimal)ocd.SubUnidad,
                            UnidadCambio = (decimal)ocd.UnidadCambio,
                            SubUnidadCambio = (decimal)ocd.SubUnidadCambio,
                            TipoProducto = ocd.TipoProducto
                        };

            orden.Items = items.ToList();

            return orden;
        }
        public int BuscarIdOrdenCarga(string CodigoCentro, string NumeroGuia) {
            var query = from oc in model.OrdenCargas
                        where oc.NumeroGuia.Equals(NumeroGuia) && oc.CodigoCentro.Equals(CodigoCentro) && oc.Estado.Equals(7) && !(bool)oc.Eliminado
                        select oc.IdOrdenCarga;
            if (!query.Any())
                throw new ArgumentException("CUSTOM", new Exception(string.Format("La orden {0} no se ha terminado de procesar.", NumeroGuia)));

            return query.Single();
        }
        public IEnumerable<OrdenCargaBE.ReporteUtilizacion> ReporteUtilizacion(int IdCentro, DateTime FechaInicio, DateTime FechaFinal) {

            var query = from oca in model.OrdenCargas.Where(x => x.FechaRegistro.Date >= FechaInicio.Date && x.FechaRegistro.Date <= FechaFinal.Date)
                        join csi in model.Centro_Sistemas.Where(x => x.IdCentro.Equals(IdCentro)) on oca.CodigoCentro equals csi.Codigo
                        join ins in model.Usuarios on oca.IdUsuarioSupervisor equals ins.IdUsuario
                        join cab in model.OrdenCargaCabs on oca.IdOrdenCarga equals cab.IdOrdenCarga
                        join det in model.OrdenCargaDets on cab.IdOrdenCargaCab equals det.IdOrdenCargaCab
                        where oca.Estado.Equals(7) && oca.Eliminado.Equals(false)
                        orderby oca.FechaRegistro descending
                        select new OrdenCargaBE.ReporteUtilizacion
                        {
                            CodigoCentro = oca.CodigoCentro,
                            NumeroGuia = oca.NumeroGuia,
                            FechaEntrega = cab.FechaEntrega,
                            PlacaVehiculo = cab.Vehiculo,
                            Transportista = cab.NombreTransportista,
                            Inspector = ins.NombreCompleto.ToUpper(),
                            SKU = det.SKU,
                            Producto = det.Descripcion,
                            TipoProducto = det.TipoProducto,
                            UnidadTransporte = Convert.ToInt32(det.UnidadCambioTrans),
                            SubUnidadTransporte = Convert.ToInt32(det.SubUnidadCambioTrans),
                            UnidadInspector = Convert.ToInt32(det.UnidadCambio),
                            SubUnidadInspector = Convert.ToInt32(det.SubUnidadCambio),
                            TipoMovimiento = det.IdTipoMovimiento.Equals(1) ? "DEV" : "CAMB",
                            Diferencia = (det.UnidadCambio * det.NumeroSuu + det.SubUnidadCambio).Equals((det.UnidadCambioTrans * det.NumeroSuu + det.SubUnidadCambioTrans)) ? false : true,
                            IdTipoMovimiento = det.IdTipoMovimiento,
                            FechaRegistro = oca.FechaRegistro,
                            Orden = (int)det.Orden
                        };

            // Consultar en las tablas de backup
            var querybak = from oca in model.OrdenCarga1s.Where(x => x.FechaRegistro.Date >= FechaInicio.Date && x.FechaRegistro.Date <= FechaFinal.Date)
                           join csi in model.Centro_Sistemas.Where(x => x.IdCentro.Equals(IdCentro)) on oca.CodigoCentro equals csi.Codigo
                           join ins in model.Usuarios on oca.IdUsuarioSupervisor equals ins.IdUsuario
                           join cab in model.OrdenCargaCab1s on oca.IdOrdenCarga equals cab.IdOrdenCarga
                           join det in model.OrdenCargaDet1s on cab.IdOrdenCargaCab equals det.IdOrdenCargaCab
                           where oca.Estado.Equals(7) && oca.Eliminado.Equals(false)
                           orderby oca.FechaRegistro descending
                           select new OrdenCargaBE.ReporteUtilizacion
                           {
                               CodigoCentro = oca.CodigoCentro,
                               NumeroGuia = oca.NumeroGuia,
                               FechaEntrega = cab.FechaEntrega,
                               PlacaVehiculo = cab.Vehiculo,
                               Transportista = cab.NombreTransportista,
                               Inspector = ins.NombreCompleto.ToUpper(),
                               SKU = det.SKU,
                               Producto = det.Descripcion,
                               TipoProducto = det.TipoProducto,
                               UnidadTransporte = Convert.ToInt32(det.UnidadCambioTrans),
                               SubUnidadTransporte = Convert.ToInt32(det.SubUnidadCambioTrans),
                               UnidadInspector = Convert.ToInt32(det.UnidadCambio),
                               SubUnidadInspector = Convert.ToInt32(det.SubUnidadCambio),
                               TipoMovimiento = det.IdTipoMovimiento.Equals(1) ? "DEV" : "CAM",
                               Diferencia = (det.UnidadCambio * det.NumeroSuu + det.SubUnidadCambio).Equals((det.UnidadCambioTrans * det.NumeroSuu + det.SubUnidadCambioTrans)) ? false : true,
                               IdTipoMovimiento = det.IdTipoMovimiento,
                               FechaRegistro = oca.FechaRegistro,
                               Orden = (int)det.Orden
                           };

            var union = query.Union(querybak).OrderBy(x => (x.FechaRegistro.ToString() + x.Orden.ToString() + x.IdTipoMovimiento.ToString()));

            if (!union.Any())
                throw new ArgumentException("CUSTOM", new Exception("ErrorDA: No se encontro datos disponibles."));

            return union;
        }
        public IEnumerable<OrdenCargaBE.ReporteDevolucion> ReporteDevolucion(int IdCentro, string FechaInicio, string FechaFinal)
        {
            var query = from oc in model.OrdenCargas
                        join occ in model.OrdenCargaCabs.Where(x => x.FechaEntrega.CompareTo(FechaInicio) >= 0 && FechaFinal.CompareTo(x.FechaEntrega) >= 0) on oc.IdOrdenCarga equals occ.IdOrdenCarga
                        join usu in model.Usuarios on oc.IdUsuarioSupervisor equals usu.IdUsuario
                        join csi in model.Centro_Sistemas on oc.CodigoCentro equals csi.Codigo
                        join cen in model.Centros on csi.IdCentro equals cen.IdCentro
                        where oc.Estado.Equals(7) && !(bool)oc.Eliminado && cen.IdCentro.Equals(IdCentro)
                        orderby occ.FechaEntrega descending
                        select new OrdenCargaBE.ReporteDevolucion()
                        {
                            Centro = oc.CodigoCentro,
                            NumeroGuia = oc.NumeroGuia,
                            IdOrdenCarga = oc.IdOrdenCarga,
                            FechaEntrega = occ.FechaEntrega,
                            Vehiculo = occ.Vehiculo,
                            Transportista = occ.NombreTransportista,
                            Inspector = usu.NombreCompleto,
                            FechaInicio = oc.FechaInicioLiquido,
                            FechaFinal = ((DateTime)oc.FechaFinalSupervision),
                            TiempoEspera = oc.FechaInicioSupervision == null ? 0 : ((DateTime)oc.FechaInicioSupervision - (DateTime)oc.FechaFinalRegistro).TotalSeconds / 60,
                            TiempoInspeccion = oc.FechaInicioSupervision == null ? 0 : ((DateTime)oc.FechaFinalSupervision - (DateTime)oc.FechaInicioSupervision).TotalSeconds / 60,
                            TiempoRegistro = oc.FechaInicioSupervision == null ? 0 : ((DateTime)oc.FechaFinalRegistro - oc.FechaInicioLiquido).TotalSeconds / 60,
                        };

            // Consultar las tablas bak
            var querybak = from oc in model.OrdenCarga1s
                        join occ in model.OrdenCargaCab1s.Where(x => x.FechaEntrega.CompareTo(FechaInicio) >= 0 && FechaFinal.CompareTo(x.FechaEntrega) >= 0) on oc.IdOrdenCarga equals occ.IdOrdenCarga
                        join usu in model.Usuarios on oc.IdUsuarioSupervisor equals usu.IdUsuario
                        join csi in model.Centro_Sistemas on oc.CodigoCentro equals csi.Codigo
                        join cen in model.Centros on csi.IdCentro equals cen.IdCentro
                        where oc.Estado.Equals(7) && !(bool)oc.Eliminado && cen.IdCentro.Equals(IdCentro)
                        orderby occ.FechaEntrega descending
                        select new OrdenCargaBE.ReporteDevolucion()
                        {
                            Centro = oc.CodigoCentro,
                            NumeroGuia = oc.NumeroGuia,
                            IdOrdenCarga = oc.IdOrdenCarga,
                            FechaEntrega = occ.FechaEntrega,
                            Vehiculo = occ.Vehiculo,
                            Transportista = occ.NombreTransportista,
                            Inspector = usu.NombreCompleto,
                            FechaInicio = oc.FechaInicioLiquido,
                            FechaFinal = ((DateTime)oc.FechaFinalSupervision),
                            TiempoEspera = oc.FechaInicioSupervision == null ? 0 : ((DateTime)oc.FechaInicioSupervision - (DateTime)oc.FechaFinalRegistro).TotalSeconds / 60,
                            TiempoInspeccion = oc.FechaInicioSupervision == null ? 0 : ((DateTime)oc.FechaFinalSupervision - (DateTime)oc.FechaInicioSupervision).TotalSeconds / 60,
                            TiempoRegistro = oc.FechaInicioSupervision == null ? 0 : ((DateTime)oc.FechaFinalRegistro - oc.FechaInicioLiquido).TotalSeconds / 60,
                        };

            var union = query.Union(querybak);

            if (!union.Any())
                throw new ArgumentException("CUSTOM", new Exception("ErrorDA: No se encontro datos disponibles."));

            return union;
        }
        #endregion

        #region Desarrollo
        public object ConsultarPI(string CodigoCentro, string NumeroGuia)
        {
            using (var client = new MG39Proxy.SI_MG39_OrdenCargaT2_Sync_OutClient(Helpers.AppConfigHelper.GetBinding(),
                Helpers.AppConfigHelper.GetEndPoint(Resources.PI.ResourceManager.GetString("MG39_HTTP_" + ConfigurationManager.AppSettings["Enviroment"]))))
            {

                var orden = new DT_OrdenCargaT2_Req_Out()
                {
                    Header = new MG39Proxy.DT_HEADER()
                    {
                        Sender = "MOBILE",
                        Receiver = "BASIS",
                        SendDate = DateTime.Now,
                        Plant = CodigoCentro,
                        MessageType = "INT_MG_39",
                        MessageId = Resources.PI.MessageId
                    },
                    Body = new DT_OrdenCargaT2_Req_OutBody()
                    {
                        CentroGuia = CodigoCentro,
                        NumGuia = decimal.Parse(NumeroGuia),
                        NumGuiaSpecified = true
                    }
                };

                client.ClientCredentials.UserName.UserName = Resources.PI.Usuario;
                client.ClientCredentials.UserName.Password = Resources.PI.Contrasena;
                var response = client.SI_MG39_OrdenCargaT2_Sync_Out(orden);
                client.Close();

                return response;
            }
        }
        #endregion
    }
}
