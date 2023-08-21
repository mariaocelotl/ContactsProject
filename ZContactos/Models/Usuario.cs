using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace ZContactos.Models
{
 //Aqui se crea el modelo Usuario del formulario con los campos obligatorios requeridos.
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        //Required para marcar como campo obligatorio
        [Required (ErrorMessage = "Nombre obligatorio")]
        [StringLength(50, ErrorMessage = "Nombre no puede tener mas de  50 caracteres.")]
        public string nombre { get; set; }



        [StringLength(50, ErrorMessage = "Apellido Paterno puede tener mas de  50 caracteres.")]
        public string apellidoPaterno { get; set; }



        [StringLength(50, ErrorMessage = "Apellido Materno no puede tener mas de  50 caracteres.")]
        public string apellidoMaterno { get; set; }




        //[DataType(DataType.PhoneNumber, ErrorMessage = "Telefono is not valid")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(10, ErrorMessage = "Telefono no valido")]
        public string telefono { get; set; }



        //Required para marcar como campo obligatorio
        [Required(ErrorMessage = "Celular obligatorio")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(10, ErrorMessage = "Celular no valido")]
        public string celular { get; set; }




        //Required para marcar como campo obligatorio
        [Required(ErrorMessage = "Correo electronico obligatorio")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string correo { get; set; }
    }
}
