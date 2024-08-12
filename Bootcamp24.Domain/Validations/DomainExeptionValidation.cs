using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp24.Domain.Validations;
public class DomainExeptionValidation : Exception
{
    public DomainExeptionValidation(string error) : base(error) { }

    public static void ExceptionHandler(bool hasError, string error)
    {
        if (hasError) throw new DomainExeptionValidation(error);
    }
}
