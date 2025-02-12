using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlinjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();
        void InsertarDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital);
        void DeleteDoctor(int idDoctor);
        Doctor FindDoctor(int iddoctor);
        void UpdateDoctor(int hospitalcod, int idDoctor, string apellido, string especialidad, int salario);
        List<Doctor> FindDoctoresEspecialidad(string especialidad);

    }
}