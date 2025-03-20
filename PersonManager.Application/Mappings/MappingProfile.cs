using AutoMapper;
using PersonManager.Application.DTOs;
using PersonManager.Application.Enums;
using PersonManager.Domain.Entities;

namespace PersonManager.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Criação
            CreateMap<CreateNaturalPersonDto, NaturalPerson>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Address, opt => opt.Ignore());

            CreateMap<CreateLegalPersonDto, LegalPerson>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Address, opt => opt.Ignore());

            // Atualização parcial – NaturalPerson
            CreateMap<UpdateNaturalPersonDto, NaturalPerson>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Name));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.DocumentNumber, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.DocumentNumber));
                    opt.MapFrom(src => src.DocumentNumber);
                })
                .ForMember(dest => dest.BirthDate, opt =>
                {
                    opt.PreCondition(src => src.BirthDate.HasValue);
                    opt.MapFrom(src => src.BirthDate.Value);
                })
                .ForMember(dest => dest.Address, opt => opt.Ignore());

            // Atualização parcial – LegalPerson
            CreateMap<UpdateLegalPersonDto, LegalPerson>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Name));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.DocumentNumber, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.DocumentNumber));
                    opt.MapFrom(src => src.DocumentNumber);
                })
                .ForMember(dest => dest.CompanyName, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.CompanyName));
                    opt.MapFrom(src => src.CompanyName);
                })
                .ForMember(dest => dest.Address, opt => opt.Ignore());

            // Mapeamento de retorno (Person → PersonDto)
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .AfterMap((src, dest) =>
                {
                    dest.PersonType = src is NaturalPerson ? PersonType.Natural : PersonType.Legal;

                    if (src is NaturalPerson np)
                    {
                        dest.BirthDate = np.BirthDate;
                        dest.CompanyName = null;
                    }
                    else if (src is LegalPerson lp)
                    {
                        dest.BirthDate = null;
                        dest.CompanyName = lp.CompanyName;
                    }
                });

            CreateMap<Address, AddressDto>();
            
            // Mapeamentos para operações
            CreateMap<Person, OperationResponseDto>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => 
                    src is NaturalPerson ? "Operação com Pessoa Física concluída com sucesso" : 
                    "Operação com Pessoa Jurídica concluída com sucesso"));
        }
    }
}
