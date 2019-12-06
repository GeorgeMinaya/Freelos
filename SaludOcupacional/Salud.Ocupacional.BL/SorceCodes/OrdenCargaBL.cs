using ACL.MegaCentro.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL.MegaCentro.BE;
using ACL.MegaCentro.DALC.SourceCodes;
using ACL.MegaCentro.DALC.Interfaces;
using Lindley.General.Communication;
using System.Data.SqlClient;

namespace ACL.MegaCentro.BL.SorceCodes
{
    public class OrdenCargaBL : IOrdenCargaBL
    {
        private IOrdenCargaDALC objOrdenCargaDALC;
        private IErrorDALC objErrorDALC;

        public OrdenCargaBL() {
            objOrdenCargaDALC = new OrdenCargaDALC();
        }

        public IEnumerable<OrdenCargaBE> ListarFinalizados(int IdCentro, string FechaInicio, string FechaFinal, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.ListarFinalizados(IdCentro, FechaInicio, FechaFinal);                
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ListarFinalizados", 1, null, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.ListarFinalizados", 1, null, null);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public OrdenCargaBE BuscarDiferencias(int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.BuscarDiferencias(IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.BuscarDiferencias", 1, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.BuscarDiferencias", 1, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public object ConsultarPI(string NumeroGuia, ref int code, ref string message)
        {
            try
            {
                var centro = NumeroGuia.Substring(0, 4);
                var guia = NumeroGuia.Substring(4, NumeroGuia.Length - 4);

                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.ConsultarPI(centro, guia);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ConsultarPI", 1, NumeroGuia, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(3, ex.Message, "OrdenCargaBL.ConsultarPI", 1, NumeroGuia, null);

                message = Notification.Titulo.ErrorInterface;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public OrdenCargaBE CargoTransportista(int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.CargoTransportista(IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.CargoTransportista", 1, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.CargoTransportista", 1, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public bool TransportistaCancelar(int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.TransportistaCancelar(IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.TransportistaCancelar", 1, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.TransportistaCancelar", 1, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        
        public OrdenCargaBE ConsultarGuiaAS400(string NumeroGuia, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                var centro = NumeroGuia.Substring(0, 4);
                var guia = NumeroGuia.Substring(4, NumeroGuia.Length - 4);
                
                    var orden = objOrdenCargaDALC.ConsultarGuia_AS400(NumeroGuia);
                    var t1 = orden.Items.Where(x => x.IdTipoMovimiento.Equals(1)).OrderBy(x => x.TipoProducto + x.TipoVenta + (x.Orden + 1000).ToString()).ToList();
                    var t2 = orden.Items.Where(x => x.IdTipoMovimiento.Equals(2)).ToList();

                    int correlativo = 0;
                    var t1final = new List<ItemBE>();
                    var tipo = string.Empty;
                    foreach (var item in t1)
                    {
                        bool considerar = true;
                        if (!tipo.Equals(item.TipoProducto))
                            correlativo = 0;

                        if (item.TipoVenta.Equals("01"))
                            item.CantOriginal = Convert.ToInt32(item.Unidad * item.NumeroSuu + item.SubUnidad);
                        else
                            item.CantOriginal = 0;
                        //Compra
                        var compra = t1.Find(x => x.TipoVenta.Equals("03") && x.Material.Equals(item.Material));
                        if (compra != null && !item.TipoVenta.Equals(compra.TipoVenta))
                            item.CantCompra = Convert.ToInt32(compra.Unidad * compra.NumeroSuu + compra.SubUnidad);
                        else
                        {
                            if (item.TipoVenta.Equals("01"))
                                item.CantCompra = 0;

                            if (item.TipoVenta.Equals("03"))
                            {
                                if (t1final.Find(x => x.Material.Equals(compra.Material)) == null)
                                    item.CantCompra = Convert.ToInt32(compra.Unidad * compra.NumeroSuu + compra.SubUnidad);
                                else
                                {
                                    considerar = false;
                                }
                            }
                        }
                        //Prestamos
                        var prestamo = t1.Find(x => x.TipoVenta.Equals("04") && x.Material.Equals(item.Material));
                        if (prestamo != null && !item.TipoVenta.Equals(prestamo.TipoVenta))
                            item.CantPrestamo = Convert.ToInt32(prestamo.Unidad * prestamo.NumeroSuu + prestamo.SubUnidad);
                        else
                        {
                            if (item.TipoVenta.Equals("01"))
                                item.CantPrestamo = 0;

                            if (item.TipoVenta.Equals("04"))
                            {
                                if (t1final.Find(x => x.Material.Equals(prestamo.Material)) == null)
                                    item.CantPrestamo = Convert.ToInt32(prestamo.Unidad * prestamo.NumeroSuu + prestamo.SubUnidad);
                                else
                                {
                                    considerar = false;
                                }
                            }
                        }
                        //Consignacion
                        var consignacion = t1.Find(x => x.TipoVenta.Equals("05") && x.Material.Equals(item.Material));
                        if (consignacion != null && !item.TipoVenta.Equals(consignacion.TipoVenta))
                            item.CantConsignacion = Convert.ToInt32(consignacion.Unidad * consignacion.NumeroSuu + consignacion.SubUnidad);
                        else
                        {
                            if (item.TipoVenta.Equals("01"))
                                item.CantConsignacion = 0;

                            if (item.TipoVenta.Equals("05"))
                            {
                                if (t1final.Find(x => x.Material.Equals(consignacion.Material)) == null)
                                    item.CantConsignacion = Convert.ToInt32(consignacion.Unidad * consignacion.NumeroSuu + consignacion.SubUnidad);
                                else
                                {
                                    considerar = false;
                                }
                            }
                        }

                        item.CantValidar = item.CantOriginal + item.CantCompra + item.CantConsignacion;
                        if (item.TipoProducto.Equals("EN"))
                        {
                            if (!item.NumeroSuu.Equals(1))
                            {
                                item.UnidadCambio = Math.Floor((item.CantOriginal + item.CantCompra + item.CantConsignacion - item.CantPrestamo) / item.NumeroSuu);
                                item.SubUnidadCambio = (item.CantOriginal + item.CantCompra + item.CantConsignacion - item.CantPrestamo) % item.NumeroSuu;
                            }
                            else
                            {
                                item.UnidadCambio = 0;
                                item.SubUnidadCambio = item.CantOriginal + item.CantCompra + item.CantConsignacion - item.CantPrestamo;
                            }
                        }
                        else
                        {
                            item.UnidadCambio = 0;
                            item.SubUnidadCambio = 0;
                        }

                        if (considerar)
                        {
                            item.IdRegistro = ++correlativo;
                            //item.IdTipoMovimiento = 1;
                            t1final.Add(item);
                            tipo = item.TipoProducto;
                        }
                    }

                    var t2final = new List<ItemBE>();
                    correlativo = 0;
                    foreach (var item in t2)
                    {
                        item.IdRegistro = ++correlativo;
                        //item.IdTipoMovimiento = 2;
                        item.CantValidar = Convert.ToInt32(item.Unidad * item.NumeroSuu + item.SubUnidad);
                        item.CantOriginal = Convert.ToInt32(item.Unidad * item.NumeroSuu + item.SubUnidad);
                        //item.Unidad = item.UnidadCambio;
                        //item.SubUnidad = item.SubUnidadCambio;
                        t2final.Add(item);
                    }

                    orden.Items = t1final.Union(t2final).ToList();
                    //orden.IdUsuarioTransportista = IdUsuarioTransportista;
                    //orden.IdUsuarioRegistro = IdUsuarioTransportista;
                    orden.NumeroGuia = guia;
                    orden.Centro = centro;
                    //orden.IdOrdenCarga = objOrdenCargaDALC.TransportistaIniciar(IdUsuarioTransportista, centro, guia);
                    return objOrdenCargaDALC.Insertar(orden);
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorInterface;
                
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public OrdenCargaBE ConsultarGuia(int IdCentroLogin, int IdUsuarioTransportista, string NumeroGuia, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                var centro = NumeroGuia.Substring(0, 4);
                var guia = NumeroGuia.Substring(4, NumeroGuia.Length - 4);

                if (!objOrdenCargaDALC.ValidarCentro(IdCentroLogin, centro))
                    throw new ArgumentException("CUSTOM", new Exception("La guía no pertenece al centro seleccionado por el usuario."));

                var IdOrdenCarga = objOrdenCargaDALC.BuscarPorNumeroGuia(Convert.ToDecimal(guia).ToString());
                if (IdOrdenCarga.Equals(0))
                {
                    //var orden = objOrdenCargaDALC.ConsultarGuia_PI(centro, guia);
                    var orden = objOrdenCargaDALC.ConsultarGuia_AS400(NumeroGuia);
                    var t1 = orden.Items.Where(x => x.IdTipoMovimiento.Equals(1)).OrderBy(x => x.TipoProducto + x.TipoVenta + (x.Orden + 1000).ToString()).ToList();
                    var t2 = orden.Items.Where(x => x.IdTipoMovimiento.Equals(2)).ToList();

                    int correlativo = 0;
                    var t1final = new List<ItemBE>();
                    var tipo = string.Empty;
                    foreach (var item in t1)
                    {
                        bool considerar = true;
                        if (!tipo.Equals(item.TipoProducto))
                            correlativo = 0;

                        if (item.TipoVenta.Equals("01"))
                            item.CantOriginal = Convert.ToInt32(item.Unidad * item.NumeroSuu + item.SubUnidad);
                        else
                            item.CantOriginal = 0;
                        //Compra
                        var compra = t1.Find(x => x.TipoVenta.Equals("03") && x.Material.Equals(item.Material));
                        if (compra != null && !item.TipoVenta.Equals(compra.TipoVenta))
                            item.CantCompra = Convert.ToInt32(compra.Unidad * compra.NumeroSuu + compra.SubUnidad);
                        else
                        {
                            if (item.TipoVenta.Equals("01"))
                                item.CantCompra = 0;

                            if (item.TipoVenta.Equals("03"))
                            {
                                if (t1final.Find(x => x.Material.Equals(compra.Material)) == null)
                                    item.CantCompra = Convert.ToInt32(compra.Unidad * compra.NumeroSuu + compra.SubUnidad);
                                else
                                {
                                    considerar = false;
                                }
                            }
                        }
                        //Prestamos
                        var prestamo = t1.Find(x => x.TipoVenta.Equals("04") && x.Material.Equals(item.Material));
                        if (prestamo != null && !item.TipoVenta.Equals(prestamo.TipoVenta))
                            item.CantPrestamo = Convert.ToInt32(prestamo.Unidad * prestamo.NumeroSuu + prestamo.SubUnidad);
                        else
                        {
                            if (item.TipoVenta.Equals("01"))
                                item.CantPrestamo = 0;

                            if (item.TipoVenta.Equals("04"))
                            {
                                if (t1final.Find(x => x.Material.Equals(prestamo.Material)) == null)
                                    item.CantPrestamo = Convert.ToInt32(prestamo.Unidad * prestamo.NumeroSuu + prestamo.SubUnidad);
                                else
                                {
                                    considerar = false;
                                }
                            }
                        }
                        //Consignacion
                        var consignacion = t1.Find(x => x.TipoVenta.Equals("05") && x.Material.Equals(item.Material));
                        if (consignacion != null && !item.TipoVenta.Equals(consignacion.TipoVenta))
                            item.CantConsignacion = Convert.ToInt32(consignacion.Unidad * consignacion.NumeroSuu + consignacion.SubUnidad);
                        else
                        {
                            if (item.TipoVenta.Equals("01"))
                                item.CantConsignacion = 0;

                            if (item.TipoVenta.Equals("05"))
                            {
                                if (t1final.Find(x => x.Material.Equals(consignacion.Material)) == null)
                                    item.CantConsignacion = Convert.ToInt32(consignacion.Unidad * consignacion.NumeroSuu + consignacion.SubUnidad);
                                else
                                {
                                    considerar = false;
                                }
                            }
                        }

                        item.CantValidar = item.CantOriginal + item.CantCompra + item.CantConsignacion;
                        if (item.TipoProducto.Equals("EN"))
                        {
                            if (!item.NumeroSuu.Equals(1))
                            {
                                item.UnidadCambio = Math.Floor((item.CantOriginal + item.CantCompra + item.CantConsignacion - item.CantPrestamo) / item.NumeroSuu);
                                item.SubUnidadCambio = (item.CantOriginal + item.CantCompra + item.CantConsignacion - item.CantPrestamo) % item.NumeroSuu;
                            }
                            else
                            {
                                item.UnidadCambio = 0;
                                item.SubUnidadCambio = item.CantOriginal + item.CantCompra + item.CantConsignacion - item.CantPrestamo;
                            }
                        }
                        else
                        {
                            item.UnidadCambio = 0;
                            item.SubUnidadCambio = 0;
                        }

                        if (considerar)
                        {
                            item.IdRegistro = ++correlativo;
                            //item.IdTipoMovimiento = 1;
                            t1final.Add(item);
                            tipo = item.TipoProducto;
                        }
                    }

                    var t2final = new List<ItemBE>();
                    correlativo = 0;
                    foreach (var item in t2)
                    {
                        item.IdRegistro = ++correlativo;
                        //item.IdTipoMovimiento = 2;
                        item.CantValidar = Convert.ToInt32(item.Unidad * item.NumeroSuu + item.SubUnidad);
                        item.CantOriginal = Convert.ToInt32(item.Unidad * item.NumeroSuu + item.SubUnidad);
                        //item.Unidad = item.UnidadCambio;
                        //item.SubUnidad = item.SubUnidadCambio;
                        t2final.Add(item);
                    }

                    orden.Items = t1final.Union(t2final).ToList();
                    orden.IdUsuarioTransportista = IdUsuarioTransportista;
                    orden.IdUsuarioRegistro = IdUsuarioTransportista;
                    orden.NumeroGuia = guia;
                    orden.Centro = centro;
                    orden.IdOrdenCarga = objOrdenCargaDALC.TransportistaIniciar(IdUsuarioTransportista, centro, guia);
                    return objOrdenCargaDALC.Insertar(orden);
                }
                else
                {
                    var orden = objOrdenCargaDALC.ConsultarGuia_SQL(IdOrdenCarga);
                    if (orden.Items.Count.Equals(0))
                    {
                        objOrdenCargaDALC.Eliminar(IdOrdenCarga, IdUsuarioTransportista);
                        this.ConsultarGuia(IdCentroLogin, IdUsuarioTransportista, NumeroGuia, ref code, ref message);
                        //message = "Error SQL: la orden " + NumeroGuia + " no contiene detalle (" + IdOrdenCarga.ToString() + "). Comunicarse con TI (APP)";
                        //code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                        //return null;
                    }
                    orden.IdUsuarioTransportista = IdUsuarioTransportista;
                    orden.IdUsuarioRegistro = IdUsuarioTransportista;
                    objOrdenCargaDALC.LiquidoIniciar(IdUsuarioTransportista, IdOrdenCarga);
                    return orden;
                }
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ConsultarGuia", IdUsuarioTransportista, NumeroGuia, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorInterface;

                objErrorDALC.Registrar(3, message, "OrdenCargaBL.ConsultarGuia", IdUsuarioTransportista, NumeroGuia, null);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public bool EnvaseIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.EnvaseIniciar(IdUsuarioTransportista, IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.EnvaseIniciar", IdUsuarioTransportista, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorNoControlado;
                objErrorDALC.Registrar(1, message, "OrdenCargaBL.EnvaseIniciar", IdUsuarioTransportista, null, IdOrdenCarga);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public bool OtrosIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.OtrosIniciar(IdUsuarioTransportista, IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.OtrosIniciar", IdUsuarioTransportista, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.OtrosIniciar", IdUsuarioTransportista, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public bool CambiosIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.CambioIniciar(IdUsuarioTransportista, IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.CambiosIniciar", IdUsuarioTransportista, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorNoControlado;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.CambiosIniciar", IdUsuarioTransportista, null, IdOrdenCarga);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public bool NoPlanIniciar(int IdUsuarioTransportista, int IdOrdenCarga, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.NoPlanIniciar(IdUsuarioTransportista, IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.NoPlanIniciar", IdUsuarioTransportista, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorNoControlado;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.NoPlanIniciar", IdUsuarioTransportista, null, IdOrdenCarga);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public bool TransportistaFinalizar(OrdenCargaBE objOrdenCargaBE, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                objOrdenCargaDALC.TransportistaActualizar(objOrdenCargaBE);
                return objOrdenCargaDALC.TransportistaFinalizar((int)objOrdenCargaBE.IdUsuarioTransportista, (int)objOrdenCargaBE.IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.TransportistaFinalizar", (int)objOrdenCargaBE.IdUsuarioTransportista, null, objOrdenCargaBE.IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorNoControlado;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.TransportistaFinalizar", (int)objOrdenCargaBE.IdUsuarioTransportista, null, objOrdenCargaBE.IdOrdenCarga);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public IEnumerable<OrdenCargaBE> ListarTodosPendientes(ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.ListarTodosPendientes();
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ListarTodosPendientes", 0, null, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.ListarTodosPendientes", 0, null, null);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public IEnumerable<OrdenCargaBE> ListarPendientes(string centro, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.ListarPendientes(centro);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ListarPendientes", 0, null, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.ListarPendientes", 0, null, null);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public OrdenCargaBE InspectorIniciar(int IdOrdenCarga, int IdUsuarioSupervisor, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                var orden = objOrdenCargaDALC.ConsultarGuia_SQL(IdOrdenCarga);
                orden.Items = orden.Items.OrderBy(x => x.Orden).ToList();
                objOrdenCargaDALC.InspectorIniciar(IdUsuarioSupervisor, IdOrdenCarga);

                return orden;
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.InspectorIniciar", IdUsuarioSupervisor, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorNoControlado;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.InspectorIniciar", IdUsuarioSupervisor, null, IdOrdenCarga);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public bool InspectorFinalizar(OrdenCargaBE objOrdenCargaBE, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                if (!objOrdenCargaDALC.ComercialEnviar(objOrdenCargaBE))
                    throw new ArgumentException("No se puedo enviar guardar la orden de carga");

                objOrdenCargaDALC.InspectorActualizar(objOrdenCargaBE);

                return objOrdenCargaDALC.InspectorFinalizar((int)objOrdenCargaBE.IdUsuarioSupervisor,
                    (int)objOrdenCargaBE.IdOrdenCarga);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.InspectorFinalizar", (int)objOrdenCargaBE.IdUsuarioSupervisor, null, objOrdenCargaBE.IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorInterface;

                objErrorDALC.Registrar(3, message, "OrdenCargaBL.InspectorFinalizar", (int)objOrdenCargaBE.IdUsuarioSupervisor, null, objOrdenCargaBE.IdOrdenCarga);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public bool BorealEnviar(OrdenCargaBE objOrdenCargaBE, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                objOrdenCargaDALC.BorealEnviar(objOrdenCargaBE);
                return true;
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.BorealEnviar", (int)objOrdenCargaBE.IdUsuarioSupervisor, null, objOrdenCargaBE.IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(3, ex.Message, "OrdenCargaBL.BorealEnviar", (int)objOrdenCargaBE.IdUsuarioSupervisor, null, objOrdenCargaBE.IdOrdenCarga);

                message = Notification.Titulo.ErrorInterface;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public int? BuscarIdOrdenCarga(string NumeroGuia, ref int code, ref string message) {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                var centro = NumeroGuia.Substring(0, 4);
                var guia = Convert.ToDecimal(NumeroGuia.Substring(4, NumeroGuia.Length - 4)).ToString();

                return objOrdenCargaDALC.BuscarIdOrdenCarga(centro, guia);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.BuscarIdOrdenCarga", 0, NumeroGuia, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorInterface;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.BuscarIdOrdenCarga", 0, NumeroGuia, null);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();

                return null;
            }
        }
        public bool DevolucionTotal(int IdCentroLogin, int IdUsuarioInspector, string NumeroGuia, ref int code, ref string message) {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                var centro = NumeroGuia.Substring(0, 4);
                var guia = Convert.ToDecimal(NumeroGuia.Substring(4, NumeroGuia.Length - 4)).ToString();

                var orden = ConsultarGuia(IdCentroLogin, IdUsuarioInspector, NumeroGuia, ref code, ref message);
                if (!code.Equals(200))
                    throw new ArgumentException(message);

                foreach (ItemBE item in orden.Items) {
                    if (item.IdTipoMovimiento.Equals(1))
                    {
                        if (item.TipoProducto.Equals("LQ")) {
                            item.UnidadCambio = item.Unidad;
                            item.SubUnidadCambio = item.SubUnidad;
                            item.UnidadCambioTrans = item.Unidad;
                            item.SubUnidadCambioTrans = item.SubUnidad;
                        }

                        if (item.TipoProducto.Equals("EN"))
                        {
                            if (orden.Sistema.Equals("B") || orden.Sistema.Equals("M"))
                            {
                                item.UnidadCambio = 0;
                                item.SubUnidadCambio = 0;
                                item.UnidadCambioTrans = 0;
                                item.SubUnidadCambioTrans = 0;
                            }

                            if (orden.Sistema.Equals("S"))
                            {
                                item.UnidadCambio = item.Unidad;
                                item.SubUnidadCambio = item.SubUnidad;
                                item.UnidadCambioTrans = item.Unidad;
                                item.SubUnidadCambioTrans = item.SubUnidad;
                            }
                        }
                    }
                    else {
                        item.UnidadCambio = 0;
                        item.SubUnidadCambio = 0;
                        item.UnidadCambioTrans = 0;
                        item.SubUnidadCambioTrans = 0;
                    }
                }
                orden.IdUsuarioSupervisor = IdUsuarioInspector;
                InspectorFinalizar(orden, ref code, ref message);
                BorealEnviar(orden, ref code, ref message);

                return true;
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.DevolucionTotal", IdUsuarioInspector, NumeroGuia, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(3, ex.Message, "OrdenCargaBL.DevolucionTotal", IdUsuarioInspector, NumeroGuia, null);

                message = Notification.Titulo.ErrorInterface;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public bool Eliminar(int IdOrdenCarga, int IdUsuarioRegistro, ref int code, ref string message) {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;

                return objOrdenCargaDALC.Eliminar(IdOrdenCarga, IdUsuarioRegistro);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.Eliminar", IdUsuarioRegistro, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return false;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(1, ex.Message, "OrdenCargaBL.Eliminar", IdUsuarioRegistro, null, IdOrdenCarga);

                message = Notification.Titulo.ErrorNoControlado;
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return false;
            }
        }
        public IEnumerable<OrdenCargaBE.ReporteUtilizacion> ReporteUtilizacion(int IdCentro, DateTime FechaInicio, DateTime FechaFinal, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.ReporteUtilizacion(IdCentro, FechaInicio, FechaFinal);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ReporteUtilizacion", 0, null, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorInterface;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.ReporteUtilizacion", 0, null, null);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
        public IEnumerable<OrdenCargaBE.ReporteDevolucion> ReporteDevolucion(int IdCentro, string FechaInicio, string FechaFinal, ref int code, ref string message)
        {
            try
            {
                code = Notification.OperationCode.Exito.GetHashCode();
                message = Notification.Titulo.Exito;
                return objOrdenCargaDALC.ReporteDevolucion(IdCentro, FechaInicio, FechaFinal);
            }
            catch (SqlException exSql)
            {
                objErrorDALC = new ErrorDALC();
                objErrorDALC.Registrar(2, exSql.Message, "OrdenCargaBL.ReporteDevolucion", 0, null, null);

                message = Notification.Titulo.ErrorSQLServer;
                code = Notification.OperationCode.ErrorDataBase.GetHashCode();
                return null;
            }
            catch (Exception ex)
            {
                objErrorDALC = new ErrorDALC();

                if (ex.Message.Equals("CUSTOM"))
                    message = ex.InnerException.Message;
                else
                    message = Notification.Titulo.ErrorInterface;

                objErrorDALC.Registrar(1, message, "OrdenCargaBL.ReporteDevolucion", 0, null, null);
                code = Notification.OperationCode.ErrorNotControl.GetHashCode();
                return null;
            }
        }
    }
}
