using AutoMapper;
using PicpayChallenge.Application.DTOs;
using PicpayChallenge.Domain.Entities;

namespace PicpayChallenge.Application.ObjectMapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Sender, opt => opt.MapFrom(src => src.FromUser.Name))
                .ForMember(dto => dto.Receiver, opt => opt.MapFrom(src => src.ToUser.Name))
                .ForMember(dto => dto.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
