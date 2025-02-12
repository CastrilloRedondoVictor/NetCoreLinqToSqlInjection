using System.Data;
using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;

namespace NetCoreLinqToSqlinjection.Repositories
{
    public class RepositoryDoctoresOracle : IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private OracleConnection cn;
        private OracleCommand com;
        public RepositoryDoctoresOracle()
        {
            string connectionString =
                @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.tablaDoctores = new DataTable();
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            OracleDataAdapter ad =
                new OracleDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tablaDoctores);
        }
        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor
                {
                    HospitalCod = row.Field<int>("HOSPITAL_COD"),
                    DoctorNo = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                };
                doctores.Add(doctor);
            }
            return doctores;
        }

        public void InsertarDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (:idhospital, :iddoctor, :apellido, :especialidad, :salario)";

            this.com.Parameters.Add(new OracleParameter(":idhospital", idHospital));
            this.com.Parameters.Add(new OracleParameter(":iddoctor", idDoctor));
            this.com.Parameters.Add(new OracleParameter(":apellido", apellido));
            this.com.Parameters.Add(new OracleParameter(":especialidad", especialidad));
            this.com.Parameters.Add(new OracleParameter(":salario", salario));

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void DeleteDoctor(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";

            this.com.Parameters.Add(new OracleParameter(":iddoctor", idDoctor));
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Doctor FindDoctor(int iddoctor)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == iddoctor
                           select datos;
            var row = consulta.First();

            Doctor doc = new Doctor();
            doc.HospitalCod = row.Field<int>("HOSPITAL_COD");
            doc.DoctorNo = row.Field<int>("DOCTOR_NO");
            doc.Apellido = row.Field<string>("APELLIDO");
            doc.Especialidad = row.Field<string>("ESPECIALIDAD");
            doc.Salario = row.Field<int>("SALARIO");

            return doc;
        }

        public void UpdateDoctor(int hospitalcod, int idDoctor, string apellido, string especialidad, int salario)
        {
            string sql = "SP_UPDATE_DOCTOR";

            this.com.Parameters.Add(new OracleParameter(":p_iddoctor", idDoctor));
            this.com.Parameters.Add(new OracleParameter(":p_apellido", apellido));
            this.com.Parameters.Add(new OracleParameter(":p_especialidad", especialidad));
            this.com.Parameters.Add(new OracleParameter(":p_salario", salario));
            this.com.Parameters.Add(new OracleParameter(":p_idhospital", hospitalcod));
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public List<Doctor> FindDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD") == especialidad
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.HospitalCod = row.Field<int>("HOSPITAL_COD");
                doc.DoctorNo = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doctores.Add(doc);
            }
            return doctores;
        }
    }
}
