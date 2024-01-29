using AutoMapper;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Presentation.DTOs;

namespace PicpayChallenge.Presentation.ObjectMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                 .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dto => dto.UserType, opt => opt.MapFrom(src => src.UserType))
                 .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dto => dto.Document, opt => opt.MapFrom(src => src.CPF_CNPJ))
                 .ForMember(dto => dto.Amount, opt => opt.MapFrom(src => src.Balance))
                 .ForMember(dto => dto.SentTxs, opt => opt.MapFrom(src => src.FromTransactions))
                 .ForMember(dto => dto.ReceivedTxs, opt => opt.MapFrom(src => src.ToTransactions));
        }
    }
}
