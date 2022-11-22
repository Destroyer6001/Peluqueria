using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public interface IRepositorioEstilista
    {
        Task Actualizar(EstilistaViewModel estilista);
        Task Borrar(int id);
        Task Crear(EstilistaViewModel estilista);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<EstilistaViewModel>> Obtener();
        Task<EstilistaViewModel> ObtenerId(int id);
    }
}
