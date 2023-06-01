using AutoMapper;
using PR_103_2019.Dtos;
using PR_103_2019.Models;

namespace PR_103_2019.MapProfile
{
    public class Map:Profile
    {
        public Map()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
