namespace NetCoreLinqToSqlInjection.Models
{
    public interface ICoche
    {
        string Marca { get; }
        string Modelo { get; }
        string Imagen { get; }
        int Velocidad { get; }
        int VelocidadMaxima { get; }

        void Acelerar();
        void Frenar();
    }
}
