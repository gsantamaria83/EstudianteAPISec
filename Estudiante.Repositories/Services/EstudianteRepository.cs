using Estudiante.Models.Dtos;
using Estudiante.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Estudiante.Repositories.Services
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly IConfiguration _configuration;
        public EstudianteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseDto<EstudianteResponseDto>> InsertarEstudiante(EstudianteRequestDto estudiante)
        {
            if (estudiante == null)
            {
                EstudianteResponseDto estudianteResponse = new EstudianteResponseDto
                {
                    IdEstudiante = 0
                };
                object obj = new object();
                ResponseDto<EstudianteResponseDto> response = new ResponseDto<EstudianteResponseDto>(false, "Usuario se encuentra nulo. Por favor validar ", estudianteResponse);
                return response;
            }

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    string idEstudiante = "0";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@NombreEstudiante", value: estudiante.NombreEstudiante, DbType.String, ParameterDirection.Input, size: 100);
                    parameters.Add("@CorreoElectronico", value: estudiante.CorreoElectronico, DbType.String, ParameterDirection.Input, size: 100);
                    parameters.Add("@Contrasena", value: estudiante.Contrasena, DbType.String, ParameterDirection.Input, size: 50);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);
                    parameters.Add("@IdEstudiante", value: idEstudiante, DbType.String, ParameterDirection.Output);

                    var result = await connection.ExecuteAsync("EstInsertarEstudiante", parameters, commandType: CommandType.StoredProcedure);
                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    idEstudiante = parameters.Get<string>("@IdEstudiante");
                    EstudianteResponseDto estudianteResponse = new EstudianteResponseDto
                    {
                        IdEstudiante = Convert.ToDecimal(idEstudiante)
                    };

                    ResponseDto<EstudianteResponseDto> response = new ResponseDto<EstudianteResponseDto>(true, mensaje, estudianteResponse);
                    return response;

                }
            }
            catch (Exception ex)
            {
                EstudianteResponseDto estudianteResponse = new EstudianteResponseDto
                {
                    IdEstudiante = 0
                };
                ResponseDto<EstudianteResponseDto> response = new ResponseDto<EstudianteResponseDto>(false, ex.Message, estudianteResponse);
                return response;


            }
        }

        public async Task<ResponseDto<bool?>> InsertarMateriasEstudiante(EstudianteMateriasRequestDto estudianteMaterias)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    string idEstudiante = "0";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IdMateria", value: estudianteMaterias.IdMateria, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@IdEstudiante", value: estudianteMaterias.IdEstudiante, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.ExecuteAsync("EstInsertarMateriasEstudiante", parameters, commandType: CommandType.StoredProcedure);
                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    EstudianteResponseDto estudianteResponse = new EstudianteResponseDto
                    {
                        IdEstudiante = Convert.ToDecimal(idEstudiante)
                    };

                    ResponseDto<bool?> response = new ResponseDto<bool?>(true, mensaje);

                    return response;

                }
            }
            catch (Exception ex)
            {
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, ex.Message);
                return response;


            }
        }

        public async Task<ResponseDto<List<EstudianteQueryResponseDto>>> SeleccionarEstudiantesPorMateria(int IdMateria)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    List<EstudianteQueryResponseDto> estudiante = new List<EstudianteQueryResponseDto>();

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@IdMateria", value: IdMateria, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.QueryAsync<EstudianteQueryResponseDto>("EstSeleccionarEstudiantesPorMateria", parameters, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        estudiante = result.ToList();
                    }
                    else
                    {
                        mensaje = "No se encontraron resultados";
                        status = false;
                        ResponseDto<List<EstudianteQueryResponseDto>> response2 = new ResponseDto<List<EstudianteQueryResponseDto>>(true, mensaje);
                        return response2;
                    }


                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    ResponseDto<List<EstudianteQueryResponseDto>> response = new ResponseDto<List<EstudianteQueryResponseDto>>(true, mensaje, estudiante);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<List<EstudianteQueryResponseDto>> response = new ResponseDto<List<EstudianteQueryResponseDto>>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }

        public async Task<ResponseDto<bool?>> ValidarIngresoMateriasEstudiante(decimal IdEstudiante)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@IdEstudiante", value: IdEstudiante, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.ExecuteAsync("EstValidarIngresoMateriasEstudiante", parameters, commandType: CommandType.StoredProcedure);
                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");

                    ResponseDto<bool?> response = new ResponseDto<bool?>(status, mensaje);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<bool?> response = new ResponseDto<bool?>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }

        public async Task<ResponseDto<List<MateriasResponseDto>>> SeleccionarMaterias()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    List<MateriasResponseDto> materias = new List<MateriasResponseDto>();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.QueryAsync<MateriasResponseDto>("EstSeleccionarMaterias", parameters, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        materias = result.ToList();
                    }
                    else
                    {
                        mensaje = "No se encontraron resultados";
                        status = false;
                        ResponseDto<List<MateriasResponseDto>> response2 = new ResponseDto<List<MateriasResponseDto>>(true, mensaje);
                        return response2;
                    }


                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    ResponseDto<List<MateriasResponseDto>> response = new ResponseDto<List<MateriasResponseDto>>(true, mensaje, materias);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<List<MateriasResponseDto>> response = new ResponseDto<List<MateriasResponseDto>>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }
        //Revision de subida a github
        public async Task<ResponseDto<List<MateriasResponseDto>>> SeleccionarMateriasEstudiante(decimal IdEstudiante)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    List<MateriasResponseDto> materias = new List<MateriasResponseDto>();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IdEstudiante", value: IdEstudiante, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.QueryAsync<MateriasResponseDto>("EstSeleccionarMateriasEstudiante", parameters, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        materias = result.ToList();
                    }
                    else
                    {
                        mensaje = "No se encontraron resultados";
                        status = false;
                        ResponseDto<List<MateriasResponseDto>> response2 = new ResponseDto<List<MateriasResponseDto>>(true, mensaje);
                        return response2;
                    }


                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    ResponseDto<List<MateriasResponseDto>> response = new ResponseDto<List<MateriasResponseDto>>(true, mensaje, materias);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<List<MateriasResponseDto>> response = new ResponseDto<List<MateriasResponseDto>>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }

        public async Task<ResponseDto<List<ProfesorQueryResponseDto>>> SeleccionarProfesoresPorMateria(int IdMateria)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    string mensaje = "";
                    bool status = false;
                    List<ProfesorQueryResponseDto> profesor = new List<ProfesorQueryResponseDto>();

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@IdMateria", value: IdMateria, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Mensaje", value: mensaje, DbType.String, ParameterDirection.Output);
                    parameters.Add("@Status", value: status, DbType.Boolean, ParameterDirection.Output);

                    var result = await connection.QueryAsync<ProfesorQueryResponseDto>("EstSeleccionarProfesorPorMateria", parameters, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        profesor = result.ToList();
                    }
                    else
                    {
                        mensaje = "No se encontraron resultados";
                        status = false;
                        ResponseDto<List<ProfesorQueryResponseDto>> response2 = new ResponseDto<List<ProfesorQueryResponseDto>>(true, mensaje);
                        return response2;
                    }


                    mensaje = parameters.Get<string>("@Mensaje");
                    status = parameters.Get<bool>("@Status");
                    ResponseDto<List<ProfesorQueryResponseDto>> response = new ResponseDto<List<ProfesorQueryResponseDto>>(true, mensaje, profesor);
                    return response;

                }
            }
            catch (Exception)
            {
                ResponseDto<List<ProfesorQueryResponseDto>> response = new ResponseDto<List<ProfesorQueryResponseDto>>(false, "Ha ocurrido un error en seleccionar usuario por id");
                return response;
            }
        }

    }
}
