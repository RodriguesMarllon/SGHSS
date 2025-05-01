using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Paciente
    {
        public Guid Id { get; private set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must have a maximum of 100 characters")]
        public string Name { get; private set; }
        
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must have 11 digits")]
        public string CPF { get; private set; }
        
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; private set; }
        
        [StringLength(20, ErrorMessage = "Phone must have a maximum of 20 characters")]
        public string Phone { get; private set; }
        
        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100, ErrorMessage = "Email must have a maximum of 100 characters")]
        public string Email { get; private set; }
        
        [StringLength(200, ErrorMessage = "Address must have a maximum of 200 characters")]
        public string Address { get; private set; }
        
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        // Constructor for Entity Framework
        protected Paciente() { }

        public Paciente(string name, string cpf, DateTime birthDate, string phone, string email, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            CPF = cpf;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void Update(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
} 