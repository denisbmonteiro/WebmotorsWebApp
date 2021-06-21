using System.ComponentModel.DataAnnotations;

namespace WebmotorsWebApp.Models
{
    public class Anuncio
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(45)]
        [Required]
        public string Marca { get; set; }

        [MaxLength(45)]
        [Required]
        public string Modelo { get; set; }

        [MaxLength(45)]
        [Required]
        public string Versao { get; set; }

        public int Ano { get; set; }

        public int Quilometragem { get; set; }

        [Required]
        public string Observacao { get; set; }
    }
}