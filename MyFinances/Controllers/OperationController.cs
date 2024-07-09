using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using MyFinances.Models;
using MyFinances.Models.Converters;
using MyFinances.Models.Domains;
using MyFinances.Models.Dtos;
using MyFinances.Models.Response;
using System.Runtime.Serialization;

namespace MyFinances.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;


        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> GetOperations()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.GetOperations().ToDtos();
            }
            catch (Exception ex)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }           

            return response;

        }

        [HttpGet("{page}/{itemNumbers}")]
        public DataResponse<IEnumerable<OperationDto>> GetOperations(int page, int itemNumbers)
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();          
               

            try
            {

                if(itemNumbers <1)
                throw new ArgumentException(nameof(itemNumbers), "The value must be greater than zero.");

                if (page < 1)
                    throw new ArgumentException(nameof(page), "The value must be greater than zero.");


                response.Data = _unitOfWork.Operation.GetOperations(page, itemNumbers).ToDtos();
            }
            catch (Exception ex)
            {
                // logowanie do pliku
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;

        }


        [HttpGet("{id}")]
        public DataResponse<OperationDto> GetOperation(int id) 
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                response.Data = _unitOfWork.Operation.GetOperation(id)?.ToDto();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpPost]
        public DataResponse<int> AddOperation(OperationDto operationDto) 
        {
            var response = new DataResponse<int>();
            
            try
            {
                var operation = operationDto.ToDao();
                _unitOfWork.Operation.AddOperation(operation);
                _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }

        
        [HttpPut]
        public Response UpdateOperation(OperationDto operation) 
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Updade(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpDelete("{id}")]
        public Response DeleteOperation(int id) 
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.RemoveOperation(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }
    }
}

