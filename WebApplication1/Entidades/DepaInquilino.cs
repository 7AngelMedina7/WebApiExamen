namespace WebApplication1.Entidades
{
    public class DepaInquilino
    {
        public int DepaId { get; set; }
        public int InquiId { get; set; }
        public Departamento Departamento { get; set; }
        public Inquilino Inquilino { get; set; }
    }
}
