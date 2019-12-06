using Salud.Ocupacional.BE;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IArchivoDA
    {
        ArchivoBE.ResponseArchivoBE DatosRegistrar();

        int RegistrarArchivo(ArchivoBE.ResponseArchivoBE archivoBE);
    }
}
