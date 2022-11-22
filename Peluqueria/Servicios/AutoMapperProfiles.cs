using AutoMapper;
using Peluqueria.Models;

namespace Peluqueria.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CitaViewModel, CitaCreacionViewModel>();
        }
    }
}
