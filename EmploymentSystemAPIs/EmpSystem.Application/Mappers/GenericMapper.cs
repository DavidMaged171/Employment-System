

using AutoMapper;

namespace EmpSystem.Application.Mappers
{
    public static class GenericMapper<TSource, TDestination>
    {
        public static TDestination Map(TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper=new Mapper(config);
            return mapper.Map(source, destination);
        }
    }
}
