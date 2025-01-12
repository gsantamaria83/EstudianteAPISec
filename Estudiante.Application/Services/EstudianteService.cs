using Estudiante.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estudiante.Repositories.Interfaces;
using Estudiante.Models.Dtos;

namespace Estudiante.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _repository;
        public EstudianteService(IEstudianteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<EstudianteResponseDto>> AgregarEstudiante(EstudianteRequestDto estudiante)
        {
            ResponseDto<EstudianteResponseDto> response = await _repository.InsertarEstudiante(estudiante);
            return response;
        }

        public async Task<ResponseDto<bool?>> AgregarMateriasEstudiante(EstudianteMateriasRequestDto estudianteMaterias)
        {
            ResponseDto<bool?> response = await _repository.InsertarMateriasEstudiante(estudianteMaterias);
            return response;
        }

        public async Task<ResponseDto<List<EstudianteQueryResponseDto>>> ObtenerEstudiantesPorMateria(int IdMateria)
        {
            ResponseDto<List<EstudianteQueryResponseDto>> response = await _repository.SeleccionarEstudiantesPorMateria(IdMateria);
            return response;
        }
        public async Task<ResponseDto<List<MateriasResponseDto>>> ObtenerMaterias()
        {
            ResponseDto<List<MateriasResponseDto>> response = await _repository.SeleccionarMaterias();
            return response;
        }
        public async Task<ResponseDto<List<ProfesorQueryResponseDto>>> ObtenerProfesoresPorMateria(int idMateria)
        {
            ResponseDto<List<ProfesorQueryResponseDto>> response = await _repository.SeleccionarProfesoresPorMateria(idMateria);
            return response;
        }

        public async Task<ResponseDto<bool?>> VerificarMateriasEstudiante(decimal idEstudiante)
        {
            ResponseDto<bool?> response = await _repository.ValidarIngresoMateriasEstudiante(idEstudiante);
            return response;
        }

        public async Task<ResponseDto<List<MateriasResponseDto>>> ObtenerMateriasEstudiante(decimal IdEstudiante)
        {
            ResponseDto<List<MateriasResponseDto>> response = await _repository.SeleccionarMateriasEstudiante(IdEstudiante);
            return response;
        }
    }
}
