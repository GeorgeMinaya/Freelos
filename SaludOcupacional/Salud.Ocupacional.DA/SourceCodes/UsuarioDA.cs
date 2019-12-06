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
    public class UsuarioDA : IUsuarioDA
    {
        private BDSaludOcupacionalDataContext model;

        public UsuarioDA()
        {
            this.model = new BDSaludOcupacionalDataContext(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Enviroment"]].ConnectionString);
        }
        public Response Usuario_Autenticar(RequestLoginBE ObjRequestBE)
        {
            try
            {
                String EncripPass = Base64Encode(ObjRequestBE.Password);
                var ObjMenu = from a in model.Menu
                              where a.Activo.Equals(true)
                              select new MenuBE.ResponseMenuBE()
                                {
                                    IdMenu      =   a.TypeUser,
                                    Nombre      =   a.Menu1,
                                    Icono       =   a.Icono,
                                    URL         =   a.URL,
                                };
                
                var query = from p in model.UserType
                            select new PerfilBE()
                            {
                                IdPerfil    =   p.ID,
                                Nombre      =   p.UserType1
                            };
                var login = model.Sp_LoginUsuer(ObjRequestBE.DNI, EncripPass).Single();
                ResponseLoginBE ObjResult = new ResponseLoginBE {
                                    IdUsuario       =   login.IdUsuario,
                                    DNI             =   login.DNI,
                                    Name            =   login.Name,
                                    LastName1       =   login.LastName1,
                                    LastName2       =   login.LastName2,
                                    Type            =   login.Type,
                                    Password        =   login.Password,
                                    Mobile          =   login.Mobile,
                                    Email           =   login.Email,
                                    RegisterDate    =   login.RegisterDate,
                                    perfilList      =   query.ToList(),
                                    LastAccesDate   =   Convert.ToDateTime(login.LastAccesDate)
                                };
                ObjResult.menuList = ObjMenu.ToList();
                Response Result = new Response() {
                    response = ObjResult
                };
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response Listar_Usuario()
        {
            try
            {
                var query = from p in model.UserType
                            select new PerfilBE()
                            {
                                IdPerfil    =   p.ID,
                                Nombre      =   p.UserType1
                            };
                var ObjResult = from a in model.User where a.Activo.Equals(true)
                                select new ResponseLoginBE()
                                {
                                    IdUsuario       =   a.IdUsuario,
                                    DNI             =   a.DNI,
                                    Name            =   a.Name,
                                    LastName1       =   a.LastName1,
                                    LastName2       =   a.LastName2,
                                    Type            =   a.Type,
                                    Password        =   a.Password,
                                    Mobile          =   a.Mobile,
                                    Email           =   a.Email,
                                    perfilList      =   query.ToList(),
                                };
                Response Result = new Response()
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
        public int NotificaUsuario(RequestLoginBE objLoginBE)
        {
            var ObjResult = (from a in model.User
                            where a.Email.Equals(objLoginBE.Email)
                            select a).Single();
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress("postmaster@sealcheckchip.com");
            m.To.Add(objLoginBE.Email);
            m.Subject = "Notificación de Recuperacion de Contraseña";
            m.Body = "<html><head><meta charset='UTF-8'></head><body><table width='600' cellspacing='0' cellpadding='0' border='0'  align='center' style='background-color: #f7f7f7;'><tbody><tr><td align='center' height='8' style='border-bottom: 1px solid #9494" +
                        "94; background - color:#a00d0d;border-top:medium none #716f63;padding:5px;'><span style='font-family:tahoma;color: #ffffff;padding:5px 0 5px 5px ;'>ACM RESTORE</span></td></tr><tr><td align='center' style='border-width: 1px; border-style: solid;   border-co" +
                        "lor: #dbdbdb #e7e7e7 #e7e7e7; background-color: white; padding-left: 10px;'><img src='http://sealcheckchip.com/Content/Imagenes/descarga.png' width='150' height='100' /><span style='font-family: tahoma; color: #6f6f6f; font-size: 12px;'></span></td></tr><tr><td height='11'  style='padding:5px;background-color: #e8e8e8;'>Estimado " + ObjResult.Name + ",<br /> Usted ha solicitado que se recuerde la contraseña de la web www.sealcheckchip.com <br />Su contraseña es " + Base64Decode(ObjResult.Password) + "<br />Atentamente, <br />El equipo de Draesger Perú SAC</td></tr><tr><td height='20' align='center' style='background-color: #a00d0d; font-family:" +
                        "tahoma; color: white; font - size: 10px; border - top: 1px solid #595959;padding:5px;'>Draeger Perú SAC - GESTIÓN DE SALUD OCUPACIONAL </td></tr></tbody></table></body></html>";
            m.IsBodyHtml = true;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            sc.Host = "mail.sealcheckchip.com";
            string str1 = "gmail.com";
            string str2 = "postmaster@sealcheckchip.com".ToLower();
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential("postmaster@sealcheckchip.com", "italo1511+");
                    sc.EnableSsl = true;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential("postmaster@sealcheckchip.com", "italo1511+");
                    sc.EnableSsl = false;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 1;
        }
        public Response BuscarUsuario(int IdUsuario)
        {
            try
            {
                var query = from p in model.UserType
                            select new PerfilBE()
                            {
                                IdPerfil    =   p.ID,
                                Nombre      =   p.UserType1
                            };
                var ObjMenu = from a in model.Menu
                              where a.Activo.Equals(true)
                              select new MenuBE.ResponseMenuBE()
                                {
                                    IdMenu      =   a.TypeUser,
                                    Nombre      =   a.Menu1,
                                    Icono       =   a.Icono,
                                    URL         =   a.URL,
                                };
                var ObjResult = from a in model.User
                                where a.IdUsuario.Equals(IdUsuario)
                                && a.Activo.Equals(true)
                                select new ResponseLoginBE()
                                {
                                    IdUsuario       =   a.IdUsuario,
                                    DNI             =   a.DNI,
                                    Name            =   a.Name,
                                    LastName1       =   a.LastName1,
                                    LastName2       =   a.LastName2,
                                    Type            =   a.Type,
                                    Password        =   Base64Decode(a.Password),
                                    Mobile          =   a.Mobile,
                                    Email           =   a.Email,
                                    Activo          =   Convert.ToBoolean(a.Activo),
                                    menuList        =   ObjMenu.ToList(),
                                    perfilList      =   query.ToList(),
                                };
                Response Result = new Response()
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
        public Sp_Insert_UsuerResult RegistrarUsuario(ResponseLoginBE ObjRequestBE)
        {
            try
            {
                String EncripPass = Base64Encode(ObjRequestBE.Password);
                var OnjResult = from a in model.Sp_Insert_Usuer(ObjRequestBE.DNI,
                                                                ObjRequestBE.Name,
                                                                ObjRequestBE.LastName1,
                                                                ObjRequestBE.LastName2,
                                                                ObjRequestBE.Type,
                                                                EncripPass,
                                                                ObjRequestBE.Mobile,
                                                                ObjRequestBE.Email).ToList()
                                                                select a;

                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Delete_UsuerResult EliminarUsuario(ResponseLoginBE objUsuarioBE)
        {
            try
            {
                var OnjResult = from a in model.Sp_Delete_Usuer(objUsuarioBE.IdUsuario, objUsuarioBE.Activo)
                                select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Sp_Update_UsuerResult ModificarUsuario(ResponseLoginBE objUsuarioBE)
        {
            try
            {
                String EncripPass = Base64Encode(objUsuarioBE.Password);
                var OnjResult = from a in model.Sp_Update_Usuer(objUsuarioBE.DNI,
                                                            objUsuarioBE.Name,
                                                            objUsuarioBE.LastName1,
                                                            objUsuarioBE.LastName2,
                                                            objUsuarioBE.Type,
                                                            EncripPass,
                                                            objUsuarioBE.Mobile,
                                                            objUsuarioBE.Activo,
                                                            objUsuarioBE.Email).ToList()
                                                            select a;
                return OnjResult.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }    
}
