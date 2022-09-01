using AutoMapper;
using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserModel>().ReverseMap();
            //CreateMap<VillaUpdateModel, VillaModel>().ForMember(destinationModel => destinationModel.Id, options => options.Ignore());
        }
    }
}
