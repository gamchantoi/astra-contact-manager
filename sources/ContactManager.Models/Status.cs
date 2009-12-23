namespace ContactManager.Models
{
    public partial class Status
    {
        public bool IsActive
        {
            get
            {
                return Name.Equals(Statuses.Active.ToString());
            }
        }
    }
}
