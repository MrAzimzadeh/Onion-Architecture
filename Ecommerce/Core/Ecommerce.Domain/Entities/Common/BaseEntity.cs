using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        // Bunu etmemizin Sebebi eger her hansisa bir class da bu nu isletkmek istemirikse bu zaman virtual etmek lazimdir ve onu da override etmek lazimdir ki Ona [NotMapped] yazma bilek 
        virtual public DateTime UpdateDate { get; set; }
        

    }
}
