using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Models
{
    public class Note
    {
        public int NoteId { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
