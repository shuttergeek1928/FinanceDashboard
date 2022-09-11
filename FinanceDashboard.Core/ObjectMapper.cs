using AutoMapper;
using FinanceDashboard.Core.Models.Account;
using FinanceDashboard.Core.Models.Subscription;
using FinanceDashboard.Data.SqlServer.Entities;

namespace FinanceDashboard.Core
{
    public class ObjectMapper
    {
        private readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var MapperConfigurations = new MapperConfiguration(maps =>
            {
                // This line ensures that internal properties are also mapped over.
                maps.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                maps.AddProfile<MappingConfig>();
            });

            var mapper = MapperConfigurations.CreateMapper();
            return mapper;
        });

        public IMapper Mapper => Lazy.Value;
    }

    internal class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //Accoumt mappers
            CreateMap<Account, AccountDetailModel>().ReverseMap();
            CreateMap<Account, AccountListModel>().ReverseMap();
            CreateMap<Account, AccountCreateModel>().ReverseMap();
            CreateMap<Account, AccountSummaryModel>().ReverseMap();
            CreateMap<AccountDetailModel, AccountCreateModel>().ReverseMap();
            //CreateMap<VillaUpdateModel, VillaModel>().ForMember(destinationModel => destinationModel.Id, options => options.Ignore());

            //Subscription mappers
            CreateMap<Subscription, SubscriptionListModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionCreateModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionUpdateModel>().ReverseMap();
        }
    }
}