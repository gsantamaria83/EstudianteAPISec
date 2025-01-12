using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiante.Models.Dtos
{
    public class MateriasResponseDto
    {
        public int IdMateria { get; set; }
        public string? NombreMateria { get; set; }
        public int Creditos { get; set; }
    }
}
