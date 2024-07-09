using MyFinances.Models.Domains;
using System.Linq;

namespace MyFinances.Models.Repositories
{

    public class OperationRepository
    {
        private readonly MyFinancesContext _context;


        public OperationRepository(MyFinancesContext context) 
        { 
            _context = context;

        }

        public IEnumerable<Operation> GetOperations()
        {
            return _context.Operations.ToList();
        }

        public IEnumerable<Operation> GetOperations(int page, int itemNumbers)
        {
            if(itemNumbers >10)
                itemNumbers = 10;
            return _context.Operations.Skip((page-1)*itemNumbers).Take(itemNumbers).ToList();
        }


        public Operation GetOperation(int id) 
        {
            return _context.Operations.FirstOrDefault(o => o.Id == id);
        }


        public void AddOperation(Operation operation)
        {
            operation.Date = DateTime.Now;
            _context.Operations.Add(operation);
        }


        public void Updade(Operation operation) 
        {
            var operationToUpdate = _context.Operations.First(o => o.Id == operation.Id);

            
            operationToUpdate.Name = operation.Name;
            operationToUpdate.Description = operation.Description;
            operationToUpdate.Value = operation.Value;
            operationToUpdate.CategoryId = operation.CategoryId;
        }


        public void RemoveOperation(int id) 
        {
            var operationToRemove = _context.Operations.First(o => o.Id == id);
            _context.Operations.Remove(operationToRemove);
        }
    }
}
