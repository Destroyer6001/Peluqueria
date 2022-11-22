using Dapper;
using Microsoft.Data.SqlClient;
using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public class RepositorioCita : IRepositorioCita
    {
        private readonly string ConnectionString;
        public RepositorioCita(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(CitaViewModel cita)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO CITA (Nombre,Apellidos,Telefono,HoraCita,
                                                            IdTipoDeServicio, IdEstilista) VALUES
                                                            (@NOMBRE,@APELLIDOS,@TELEFONO,@HoraCita,@IDTIPODESERVICIO,
                                                            @IDESTILISTA);SELECT SCOPE_IDENTITY();", cita);

            cita.Id = id;

        }

        public async Task<bool> Existe(int IdEstilista, DateTime HoraCita)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM CITA
                    WHERE IdEstilista = @IDESTILISTA AND HoraCita = @HoraCita;", new {IdEstilista,HoraCita});

            return existe == 1;
        }

        public async Task<IEnumerable<CitaViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<CitaViewModel>(@"SELECT CITA.ID, CITA.NOMBRE, CITA.APELLIDOS, CITA.HoraCita, CITA.TELEFONO, ES.NOMBRE AS NombreEstilista, TP.NOMBRE AS NombreTipoDECuenta
                                                                FROM CITA 
                                                                INNER JOIN ESTILISTA ES
                                                                ON ES.ID = CITA.IDESTILISTA
                                                                INNER JOIN TIPODESERVICIO TP
                                                                ON TP.Id = CITA.IdTipoDeServicio");

        }

        public async Task<CitaViewModel> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<CitaViewModel>(
                @"SELECT CITA.ID, CITA.NOMBRE, CITA.APELLIDOS, CITA.HoraCita, CITA.TELEFONO, 
                CITA.IdEstilista, Cita.IdTipoDeServicio
                FROM CITA INNER JOIN ESTILISTA ES ON ES.ID = CITA.IDESTILISTA
                INNER JOIN TIPODESERVICIO TP ON TP.Id = CITA.IdTipoDeServicio
                WHERE CITA.ID = @Id", new {id});
        }

        public async Task Actualizar(CitaCreacionViewModel cita)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"UPDATE CITA SET
            Nombre = @NOMBRE,Apellidos = @APELLIDOS, TELEFONO = @TELEFONO, HORACITA = @HORACITA, 
            IdTipoDeServicio = @IDTIPODESERVICIO, IdEstilista = @IDESTILISTA
            WHERE Id = @ID;", cita);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"DELETE CITA WHERE ID = @ID", new { id });
        }
    }
}
