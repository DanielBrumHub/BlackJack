using AutoMapper;
using BlackJack.DataTransfer.Jogos.Responses;
using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Aplicacao.Jogos.Profiles
{
    public class JogosProfile : Profile
    {
        public JogosProfile()
        {
            CreateMap<Jogo, JogoResponse>();
            CreateMap<Carta, CartaResponse>();
            CreateMap<Nipe, NipeResponse>();
        }
    }
}
