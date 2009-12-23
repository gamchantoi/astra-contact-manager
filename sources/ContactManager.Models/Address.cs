namespace ContactManager.Models
{
    public partial class Address
    {
        public void LoadStreetReferences()
        {
            if (!StreetReference.IsLoaded)
                StreetReference.Load();
        }
    }
}
