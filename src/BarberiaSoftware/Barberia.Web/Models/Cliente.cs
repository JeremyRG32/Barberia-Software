namespace Barberia.Web.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public EstadoCliente Estado { get; set; } = EstadoCliente.Default;
        public string? Direccion { get; set; }

        public List<Local>? LocalesFavoritos { get; set; }

        public enum EstadoCliente
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
