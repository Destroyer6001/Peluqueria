using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public interface IRepositorioCita
    {
        Task<IEnumerable<CitaViewModel>> Obtener();
        Task Crear(CitaViewModel cita);
        Task<bool> Existe(int IdEstilista, DateTime hora);
        Task Actualizar(CitaCreacionViewModel cita);
        Task<CitaViewModel> ObtenerPorId(int id);
        Task Borrar(int id);
    }
}
