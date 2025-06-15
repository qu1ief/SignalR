﻿using System.ComponentModel.DataAnnotations;

namespace PustokTask.Models
{
    public class Genre :BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
