using Dapper;
using Microsoft.Data.SqlClient;
using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public class RepositorioTipoDeServicio : IRepositorioTipoDeServicio
    {
        private readonly string ConnectionString;
        public RepositorioTipoDeServicio(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoDeServicioViewModel servicio)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO TIPODESERVICIO (Nombre) VALUES
                                                            (@NOMBRE);
                                                            SELECT SCOPE_IDENTITY();", servicio);

            servicio.Id = id;

        }

        public async Task<bool> Existe(string nombre)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM TIPODESERVICIO WHERE Nombre = @NOMBRE;", new { nombre });

            return existe == 1;
        }

        public async Task<IEnumerable<TipoDeServicioViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<TipoDeServicioViewModel>(@"SELECT Id, Nombre FROM TIPODESERVICIO");
        }


        public async Task Actualizar(TipoDeServicioViewModel tipodeservicio)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@" UPDATE TIPODESERVICIO SET Nombre = @NOMBRE WHERE ID = @ID", tipodeservicio);

        }

        public async Task<TipoDeServicioViewModel> ObtenerId(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoDeServicioViewModel>(@"SELECT ID, NOMBRE FROM TIPODESERVICIO
                                                                        WHERE ID = @ID", new { id });
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync("DELETE TIPODESERVICIO WHERE ID = @iD", new { id });

        }

    }
}
