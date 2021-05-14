using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        [ForeignKey("Song")]
        public int SongId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Author { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(80)]
        public string Content { get; set; }

        [Required]
        public uint TimeStamp { get; set; }
    }
}
