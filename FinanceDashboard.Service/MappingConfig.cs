using AutoMapper;
using FinanceDashboard.Models.Data.Entities;
using FinanceDashboard.Service.Models;

namespace FinanceDashboard.Service
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDetailModel>().ReverseMap();
            CreateMap<User, UserListModel>().ReverseMap();
            CreateMap<User, UserCreateModel>().ReverseMap();
            //CreateMap<VillaUpdateModel, VillaModel>().ForMember(destinationModel => destinationModel.Id, options => options.Ignore());
        }
    }
}
