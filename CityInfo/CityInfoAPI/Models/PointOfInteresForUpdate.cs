using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Models
{
    public class PointOfInteresForCreationUpdate
    {
        [MaxLength(50,ErrorMessage ="Ragno maximo de 10 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage ="El campo descripcion es requerido, Ingresa informacion")]
        public string Description { get; set; }
    }
}
