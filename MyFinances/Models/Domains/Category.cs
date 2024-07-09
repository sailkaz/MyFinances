using System;
using System.Collections.Generic;

namespace MyFinances.Models.Domains
{
    
    public partial class Category
    {

        public Category()
        {
            Operations = new HashSet<Operation>();
        }

        
        public int Id { get; set; }


        public string Name { get; set; } = null!;

        
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
