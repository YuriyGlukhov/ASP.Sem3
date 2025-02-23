using ASP.Seminar3.Models;
using ASP.Seminar3.Models.DTO;
using AutoMapper;

namespace ASP.Seminar3.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryModel>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageModel>(MemberList.Destination).ReverseMap();

        }
    }
}
