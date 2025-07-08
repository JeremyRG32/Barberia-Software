namespace Barberia.Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public EstadoUsuario Estado { get; set; } = EstadoUsuario.Default;
        public string? Direccion { get; set; }

        public List<Local>? LocalesFavoritos { get; set; }

        public enum EstadoUsuario
        {
            Default,
            EnLocal,
            EnCola,
            EnEspera,
            SiendoAtendido,
            Atendido,
            Pago
        }

        public class Local
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }
    }
}
