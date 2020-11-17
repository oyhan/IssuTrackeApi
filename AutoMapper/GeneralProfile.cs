//using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Dto.Identity;
using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;
using AutoMapper;
using Humanizer;
using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BaseDto = Asanobat.IssueTracker.Models.Dto.Issue.BaseDto;

namespace Asanobat.IssueTracker.AutoMapper
{
    public class GeneralProfile : Profile
    {

        public GeneralProfile()
        {
            CreateMap<BaseModel<Guid>, BaseDto>().IncludeAllDerived().
                ForMember(s => s.CreatedDate, s =>
                {
                    s.MapFrom(d => d.CreatedDate);
                    s.ConvertUsing(new DateTimeValueConvertor(), d => d.CreatedDate);
                }).
                ForMember(s => s.LastModifiedDate, s => s.ConvertUsing(new DateTimeValueConvertor()))
                //                .AfterMap((dbModel, dto) =>
                //            {
                //
                //                dto.CreatedDate = dbModel.CreatedDate.ToUniversalTime().Humanize(culture: new CultureInfo("fa-IR"));
                //                dto.LastModifiedDate = dbModel.LastModifiedDate.ToUniversalTime().Humanize(culture: new CultureInfo("fa-IR"));
                //            })B
                ;

            CreateMap<BaseDto, BaseModel<Guid>>().IncludeAllDerived()
                .ForMember(d => d.LastModifiedDate, opt => opt.Ignore());


            CreateMap<IssueTypeModel, IssueTypeDto>().ForMember(s => s.UsersList, o =>
            {
                o.MapFrom(s => s.IssueUsers.Select(u => u.User.Id));
            });


            CreateMap<IssueTypeDto, IssueTypeModel>().ForMember(s => s.IssueUsers, o =>
         {
             o.MapFrom(s => s.UsersList.Select(u => new IssueTypeModelUserModel() { UserId = u, IssueTypeId = s.Id }));
         });

            CreateMap<ApplicationUser, PersonnelDto>().ForMember(s => s.RolesList, o =>
            {
                o.MapFrom<PersonnelDtoValueResolver>();
            });


            CreateMap<IssueTypeDto, IssueTypeModel>().ForMember(s => s.IssueUsers, o =>
            {
                o.MapFrom(s => s.UsersList.Select(u => new IssueTypeModelUserModel() { UserId = u, IssueTypeId = s.Id }));
            });



            //      .ForMember(d => d.Propertys, m =>
            //{
            //    m.MapFrom(s => s.Propertys.Select(p => new IssueTypePropertyTypeDto()
            //    {
            //        IssueTypeId = s.Id,
            //        PropertyId = p.Property.Id,
            //        Property = p.Property.Id==0?p.Property:null

            //    }));
            //});
            //                .ForMember(d => d.CreatedDate, opt => opt.Ignore());


        }



    }

    public class DateTimeValueConvertor : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime sourceMember, ResolutionContext context)
        {
            return sourceMember.ToUniversalTime().Humanize(culture: new CultureInfo("en-US"));
        }
    }
}
