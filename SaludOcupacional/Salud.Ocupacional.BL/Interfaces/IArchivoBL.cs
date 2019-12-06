using ACL.Community.Response;
using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IArchivoBL
    {
        Result DatosRegistrar();

        Result RegistrarArchivo(ArchivoBE.ResponseArchivoBE archivoBE);
    }
}
