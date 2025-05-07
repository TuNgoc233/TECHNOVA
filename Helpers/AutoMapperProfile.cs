using AutoMapper;
using TECHNOVA.Data;
using TECHNOVA.ViewModels;

namespace TECHNOVA.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, Customer>();
        }
    }
}
