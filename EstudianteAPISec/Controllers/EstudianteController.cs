using Estudiante.Application.Interfaces;
using Estudiante.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EstudianteAPISec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _service;

        public EstudianteController(IEstudianteService service)
        {
            _service = service;
        }

        [HttpPost("CrearEstudiante")]
        public async Task<ActionResult<ResponseDto<EstudianteResponseDto>>> CrearEstudiante([FromBody] EstudianteRequestDto estudianteIngreso)
        {
            return Ok(await _service.AgregarEstudiante(estudianteIngreso));

        }

        [HttpPost("CrearMateriasEstudiante")]
        public async Task<ActionResult<ResponseDto<bool?>>> CrearMateriasEstudiante([FromBody] EstudianteMateriasRequestDto estudianteMateria)
        {
            return Ok(await _service.AgregarMateriasEstudiante(estudianteMateria));
        }

        [HttpGet("ObtenerEstudiantesPorMateria")]
        public async Task<ActionResult<ResponseDto<List<EstudianteQueryResponseDto>>>> ObtenerEstudiantesPorMateria([FromQuery] int IdMateria)
        {
            return Ok(await _service.ObtenerEstudiantesPorMateria(IdMateria));
        }

        [HttpGet("ObtenerMaterias")]
        public async Task<ActionResult<ResponseDto<List<MateriasResponseDto>>>> ObtenerMaterias()
        {
            return Ok(await _service.ObtenerMaterias());

        }

        [HttpGet("ObtenerMateriasPorEstudiante")]
        public async Task<ActionResult<ResponseDto<List<MateriasResponseDto>>>> ObtenerMateriasPorEstudiante(decimal IdEstudiante)
        {
            return Ok(await _service.ObtenerMateriasEstudiante(IdEstudiante));

        }

        [HttpGet("ObtenerProfesoresPorMateria")]
        public async Task<ActionResult<ResponseDto<List<ProfesorQueryResponseDto>>>> ObtenerProfesoresPorMateria([FromQuery] int IdMateria)
        {
            return Ok(await _service.ObtenerProfesoresPorMateria(IdMateria));

        }

        [HttpGet("VerificarMateriasEstudiante")]
        public async Task<ActionResult<ResponseDto<bool?>>> VerificarMateriasEstudiante([FromQuery] decimal IdEstudiante)
        {
            return Ok(await _service.VerificarMateriasEstudiante(IdEstudiante));

        }
    }
}
