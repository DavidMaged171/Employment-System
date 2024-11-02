

using AutoMapper;

namespace EmpSystem.Application.Mappers
{
    public static class GenericMapper<TSource, TDestination> where TDestination: class
    {
        public static TDestination Map(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper=new Mapper(config);
            return mapper.Map<TDestination>(source);
        }
        public static List<TDestination> Map(List<TSource> sourceList, List<TDestination> destinationList)
        {
            var destList= new List<TDestination>(capacity:sourceList.Count);
            for (int i = 0; i < sourceList.Count; i++) 
            {
                destList.Add(Map(sourceList[i]));
            }
            return destList;
        }
    }
}
