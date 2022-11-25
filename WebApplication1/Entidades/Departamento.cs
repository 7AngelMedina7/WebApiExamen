namespace WebApplication1.Entidades
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Disponibilidad { get; set; }
        public int Precio { get; set; }

        public List<Inquilino> Inquilino { get; set; }
    }
}
