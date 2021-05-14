using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace amits_limon_server.Models
{
    public class Song
    {
        public int SongId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ImgSource { get; set; }

        [Required]
        [MaxLength(1000)]
        public string AudioSource { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
    public class SongUpload:Song
    {
        public IFormFile Image { get; set; }

        public IFormFile Audio { get; set; }

    }
}

