using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface IEntityCustomResourcesRepository
    {
        bool CreateResource (string key , string value );
        CustomResource EditResource(CustomResource resource);
        CustomResource GetResource(string key );
        bool DeleteResource(string key );
    }
}
