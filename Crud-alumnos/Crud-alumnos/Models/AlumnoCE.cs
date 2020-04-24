using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crud_alumnos.Models
{
    public class AlumnoCE
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Ingerese Nombres")]
        public string Nombres { get; set; }
        [Required]
        [Display(Name = "Ingerese Apellidos")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Edad del Alumno")]
        public int Edad { get; set; }
        [Required]
        [Display(Name = "Ingerese Sexo")]
        public string Sexo { get; set; }
        [Required]
        [Display(Name = "ciudad")]
        public int CodCiudad { get; set; }

        public string NombreCiudad { get; set; }

        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }

        public System.DateTime FechaRegistro { get; set; }

    }

    [MetadataType(typeof(AlumnoCE))]

    public partial class Alumno {
    
        
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }

        public string NombreCiudad { get; set; }
    
    }
}