using Estudiante.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiante.Application.Interfaces
{
    public interface IEstudianteService
    {
        Task<ResponseDto<EstudianteResponseDto>> AgregarEstudiante(EstudianteRequestDto estudiante);
        Task<ResponseDto<bool?>> AgregarMateriasEstudiante(EstudianteMateriasRequestDto estudianteMaterias);
        Task<ResponseDto<List<EstudianteQueryResponseDto>>> ObtenerEstudiantesPorMateria(int IdMateria);
        Task<ResponseDto<List<MateriasResponseDto>>> ObtenerMaterias();
        Task<ResponseDto<List<ProfesorQueryResponseDto>>> ObtenerProfesoresPorMateria(int idMateria);
        Task<ResponseDto<bool?>> VerificarMateriasEstudiante(decimal idEstudiante);
        Task<ResponseDto<List<MateriasResponseDto>>> ObtenerMateriasEstudiante(decimal IdEstudiante);
    }
}
