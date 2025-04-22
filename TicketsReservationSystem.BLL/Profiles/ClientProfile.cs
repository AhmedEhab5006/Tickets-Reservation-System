using AutoMapper;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            // Client
            CreateMap<ClientAddDto, Client>();
            CreateMap<Client, ClientReadDto>();

            // Address
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressReadDto, Address>();


            // Ticket
            CreateMap<Ticket, TicketReadDto>();

            // Events
            CreateMap<Event, EventAddDto>();

            
            CreateMap<SportEvent, FullDetailSportEventReadDto>()
                .ForMember(dest => dest.team1, opt => opt.MapFrom(src => src.team1))
                .ForMember(dest => dest.team2, opt => opt.MapFrom(src => src.team2))
                .ForMember(dest => dest.sport, opt => opt.MapFrom(src => src.sport))
                .ForMember(dest => dest.tournament, opt => opt.MapFrom(src => src.tournament))
                .ForMember(dest => dest.tournamentStage, opt => opt.MapFrom(src => src.tournamentStage));

            CreateMap<EntertainmentEvent, FullDetailEntertainmentEventReadDto>()
                .ForMember(dest => dest.showCategory, opt => opt.MapFrom(src => src.showCategory))
                .ForMember(dest => dest.performerName, opt => opt.MapFrom(src => src.performerName))
                .ForMember(dest => dest.ageRestriction, opt => opt.MapFrom(src => src.ageRestriction))
                .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.duration))
                .ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.genre));
        }
    }
}
