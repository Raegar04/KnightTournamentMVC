using AutoMapper;

namespace KnightTournament.Extensions
{
    public static class AutoMapperExtension
    {
        public static void MapTo<TFromEntity, TToEntity>(this TFromEntity fromEntity, ref TToEntity toEntity) 
        {
            var config = new MapperConfiguration(expression => 
            {
                expression.CreateMap<TFromEntity, TToEntity>();
            });
            var mapper = new Mapper(config);
            toEntity = mapper.Map<TFromEntity, TToEntity>(fromEntity);
        }
    }
}
