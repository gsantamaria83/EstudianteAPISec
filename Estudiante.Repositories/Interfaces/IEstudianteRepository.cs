using Estudiante.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiante.Repositories.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<ResponseDto<List<MateriasResponseDto>>> SeleccionarMaterias();
        Task<ResponseDto<EstudianteResponseDto>> InsertarEstudiante(EstudianteRequestDto estudiante);
        Task<ResponseDto<bool?>> InsertarMateriasEstudiante(EstudianteMateriasRequestDto estudianteMaterias);
        Task<ResponseDto<List<EstudianteQueryResponseDto>>> SeleccionarEstudiantesPorMateria(int IdMateria);
        Task<ResponseDto<List<ProfesorQueryResponseDto>>> SeleccionarProfesoresPorMateria(int IdMateria);
        Task<ResponseDto<bool?>> ValidarIngresoMateriasEstudiante(decimal IdEstudiante);
        Task<ResponseDto<List<MateriasResponseDto>>> SeleccionarMateriasEstudiante(decimal IdEstudiante);
    }
}

