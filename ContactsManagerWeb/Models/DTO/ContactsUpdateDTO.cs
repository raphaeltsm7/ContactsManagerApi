﻿using System.ComponentModel.DataAnnotations;

namespace ContactsManagerWeb.Models.DTO {
    public class ContactsUpdateDTO {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O Nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        public string Email { get; set; }
        [Required]

        public string Phone { get; set; }
        [Required]

        public string Address { get; set; }
    }
}
