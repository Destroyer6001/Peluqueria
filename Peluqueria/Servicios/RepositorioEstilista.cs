using Dapper;
using Microsoft.Data.SqlClient;
using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public class RepositorioEstilista : IRepositorioEstilista
    {
        private readonly string ConnectionString;
        public RepositorioEstilista(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(EstilistaViewModel estilista)
        { 
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO ESTILISTA (Nombre,Edad,DocumentoDeIdentidad) VALUES
                                                            (@NOMBRE,@EDAD,@DOCUMENTODEIDENTIDAD);
                                                            SELECT SCOPE_IDENTITY();", estilista);

            estilista.Id = id;

        }

        public async Task<bool> Existe(string nombre)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM ESTILISTA WHERE Nombre = @NOMBRE;", new {nombre});

            return existe == 1;
        }

        public async Task<IEnumerable<EstilistaViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<EstilistaViewModel>(@"SELECT Id, Nombre, Edad, DocumentoDeIdentidad FROM ESTILISTA");
        }

        public async Task Actualizar(EstilistaViewModel estilista)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@" UPDATE ESTILISTA SET Nombre = @NOMBRE, Edad = @Edad, DocumentoDeIdentidad = @DocumentoDeIdentidad WHERE ID = @ID", estilista);

        }

        public async Task<EstilistaViewModel> ObtenerId(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<EstilistaViewModel>(@"SELECT ID, NOMBRE, Edad, DocumentoDeIdentidad FROM ESTILISTA
                                                                        WHERE ID = @ID", new { id});
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync("DELETE ESTILISTA WHERE ID = @iD", new { id });

        }
    }
}
