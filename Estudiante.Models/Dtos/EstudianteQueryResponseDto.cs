using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiante.Models.Dtos
{
    public class EstudianteQueryResponseDto
    {
        public decimal IdEstudiante { get; set; }
        public string? NombreEstudiante { get; set; }
        public string? CorreoElectronico { get; set; }
    }

}
