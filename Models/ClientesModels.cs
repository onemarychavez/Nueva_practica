using System.ComponentModel.DataAnnotations;

namespace Nueva_practica.Models
{
    public class ClientesModels
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string? Nombre { get; set; }
        public string? Dui { get; set; }
        public string? Correo { get; set; }
        public string?  Direccion { get; set; }
        public int Telefono { get; set; }


}
}
