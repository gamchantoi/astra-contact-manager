using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IBaseModel
    {
        bool Validate(out List<ValidatorStatus> ex);
    }
}