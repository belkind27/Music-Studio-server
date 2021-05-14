using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace amits_limon_server.Models
{
    public class Spotlight
    {
        public int SpotlightId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Youtube { get; set; }

        [Required]
        [MaxLength(100)]
        public string Spotify { get; set; }

        [Required]
        [MaxLength(100)]
        public string Amazone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apple { get; set; }

        [Required]
        [MaxLength(100)]
        public string Itunes { get; set; }

        [Required]
        [MaxLength(100)]
        public string Tidal { get; set; }

        [Required]
        [MaxLength(100)]
        public string Deezer { get; set; }

    }
}
