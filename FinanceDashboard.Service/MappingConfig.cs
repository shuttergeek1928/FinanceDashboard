using AutoMapper;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Models.Account;
using FinanceDashboard.Models.Subscription;

namespace FinanceDashboard.Service
{
    public class MappingConfig : Profile
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
            //CreateMap<Subscription, Task<Subscription>>().ReverseMap();
        }
    }
}
