using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IDoctorDA
    {
        DoctorBE.Response Listar_Doctor();
        DoctorBE.Response BuscarDoctor(int IdDoctor); 
        Sp_Delete_DoctorResult EliminarDoctor(DoctorBE.ResponseDoctoreBE objDoctorBE);
        Sp_Update_DoctorResult ModificarDoctor(DoctorBE.ResponseDoctoreBE objDoctorBE);
        Sp_Insert_DoctorResult RegistrarDoctor(DoctorBE.ResponseDoctoreBE ObjRequestLoginBE);
    }
}
