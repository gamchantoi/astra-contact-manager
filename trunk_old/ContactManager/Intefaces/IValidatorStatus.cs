using System;

namespace ContactManager.Intefaces
{
    public interface IValidatorStatus
    {
        String Key { get; set; }
        String Message { get; set; }
    }
}