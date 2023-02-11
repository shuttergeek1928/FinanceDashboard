using AutoMapper;
using FinanceDashboard.Models.Account;
using FinanceDashboard.Models.Subscription;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Models.PreLogon;
using FinanceDashboard.Models.Models.Income;
using FinanceDashboard.Models.Models.SegmentLimits;

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
            CreateMap<Account, RegisterAccountModel>().ReverseMap();
            CreateMap<AccountDetailModel, AccountCreateModel>().ReverseMap();
            //CreateMap<VillaUpdateModel, VillaModel>().ForMember(destinationModel => destinationModel.Id, options => options.Ignore());

            //Subscription mappers
            CreateMap<Subscription, SubscriptionListModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionCreateModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionUpdateModel>().ReverseMap();

            //Income Mappers
            CreateMap<Income, IncomeCreateModel>().ReverseMap();
            CreateMap<Income, IncomeListModel>().ReverseMap();
            CreateMap<Income, IncomeDetailModel>().ReverseMap();
            CreateMap<IncomeDetailModel, IncomeListModel>().ReverseMap();

            //Limits Mappers
            CreateMap<SegmentLimits, SegmentLimitsCreateModel>().ReverseMap();
            CreateMap<SegmentLimits, SegmentLimitsListModel>().ReverseMap();
        }
    }
}