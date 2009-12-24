using ContactManager.Models.Enums;

namespace ContactManager.Models
{
    public partial class Status
    {
        public bool IsActive
        {
            get
            {
                return Name.Equals(STATUSES.Active.ToString());
            }
        }
    }
}
