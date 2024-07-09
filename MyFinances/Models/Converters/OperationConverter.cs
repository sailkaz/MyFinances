using MyFinances.Models.Domains;
using MyFinances.Models.Dtos;

namespace MyFinances.Models.Converters
{
    
    public static class OperationConverter
    {
        
        public static OperationDto ToDto(this Operation operationDao)
        {
            return new OperationDto()
            {
                CategoryId = operationDao.CategoryId,
                Date = operationDao.Date,
                Description = operationDao.Description,
                Id = operationDao.Id,
                Value = operationDao.Value,
                Name = operationDao.Name,
            };
        }


        public static IEnumerable<OperationDto> ToDtos(this IEnumerable<Operation> operationDao)
        {
            if(operationDao == null)
                return Enumerable.Empty<OperationDto>();

            return operationDao.Select(x => ToDto(x));
        }


        public static Operation ToDao(this OperationDto operationDto)
        {
            return new Operation()
            {
                CategoryId = operationDto.CategoryId,
                Date = operationDto.Date,
                Description = operationDto.Description,
                Id = operationDto.Id,
                Value = operationDto.Value,
                Name = operationDto.Name,
            };
        }
    }
}
