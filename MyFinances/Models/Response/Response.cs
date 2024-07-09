using System.ComponentModel;

namespace MyFinances.Models.Response
{

    public class Response
    {

        public Response() 
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get;set; }

        public bool Success => Errors == null || !Errors.Any();
    }
}
