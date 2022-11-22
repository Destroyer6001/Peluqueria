using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public interface IRepositorioTipoDeServicio
    {
        Task Actualizar(TipoDeServicioViewModel tipodeservicio);
        Task Borrar(int id);
        Task Crear(TipoDeServicioViewModel servicio);

        Task<bool> Existe(string nombre);

        Task<IEnumerable<TipoDeServicioViewModel>> Obtener();
        Task<TipoDeServicioViewModel> ObtenerId(int id);
    }
}
