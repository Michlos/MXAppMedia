using AutoMapper;

using MXAppMedia.Api.Models;
namespace MXAppMedia.Api.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Initialize mappings here, e.g.:
        // CreateMap<SourceModel, DestinationModel>();
        // CreateMap<DestinationModel, SourceModel>();
        CreateMap<Client, ClientDTO>()
            .ReverseMap();

        CreateMap<Media, MediaDTO>()
            .ForMember(med => med.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(med => med.ClientName, opt => opt.MapFrom(src => src.Client.Name))
            .ReverseMap();
    }
}
