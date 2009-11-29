namespace ContactManager.Models
{
    public partial class Address
    {
        public Street Street { get; set; }

        public Address()
        {
            if (EntityKey == null) return;
            
            astra_StreetsReference.Load();
            Street = astra_StreetsReference.Value;
        }
    }
}
