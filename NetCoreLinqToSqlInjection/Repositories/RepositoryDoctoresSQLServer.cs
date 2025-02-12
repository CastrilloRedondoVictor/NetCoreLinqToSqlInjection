using Microsoft.Data.SqlClient;
using NetCoreLinqToSqlinjection.Repositories;
using NetCoreLinqToSqlInjection.Models;
using System.Data;

public class RepositoryDoctoresSQLServer: IRepositoryDoctores
{
    private DataTable tableDoctores;
    private SqlConnection cn;
    private SqlCommand com;

    public RepositoryDoctoresSQLServer()
    {
        string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Trust Server Certificate=True";
        this.cn = new SqlConnection(connectionString);
        this.com = new SqlCommand();
        this.com.Connection = this.cn;
        this.tableDoctores = new DataTable();
        SqlDataAdapter ad = new SqlDataAdapter
        ("select * from DOCTOR", connectionString);
        ad.Fill(this.tableDoctores);
    }

    public List<Doctor> GetDoctores()
    {
        var consulta = from datos in this.tableDoctores.AsEnumerable()
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

    public void InsertarDoctor
    (int idDoctor, string apellido, string especialidad
    , int salario, int idHospital)
    {
        string sql = "insert into DOCTOR values (@idhospital, @iddoctor , @apellido, @especialidad, @salario)";
        this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
        this.com.Parameters.AddWithValue("@apellido", apellido);
        this.com.Parameters.AddWithValue("@especialidad", especialidad);
        this.com.Parameters.AddWithValue("@salario", salario);
        this.com.Parameters.AddWithValue("@idhospital", idHospital);
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

        this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
        this.com.CommandType = CommandType.StoredProcedure;
        this.com.CommandText = sql;
        this.cn.Open();
        this.com.ExecuteNonQuery();
        this.cn.Close();
        this.com.Parameters.Clear();
    }


    public Doctor FindDoctor (int iddoctor)
    {
        var consulta = from datos in this.tableDoctores.AsEnumerable() where datos.Field<int>("DOCTOR_NO")==iddoctor
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

        this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
        this.com.Parameters.AddWithValue("@apellido", apellido);
        this.com.Parameters.AddWithValue("@especialidad", especialidad);
        this.com.Parameters.AddWithValue("@salario", salario);
        this.com.Parameters.AddWithValue("@idhospital", hospitalcod);
        this.com.CommandType = CommandType.StoredProcedure;
        this.com.CommandText = sql;
        this.cn.Open();
        this.com.ExecuteNonQuery();
        this.cn.Close();
        this.com.Parameters.Clear();
    }

    public List<Doctor> FindDoctoresEspecialidad(string especialidad)
    {
        var consulta = from datos in this.tableDoctores.AsEnumerable() where datos.Field<string>("ESPECIALIDAD")==especialidad
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