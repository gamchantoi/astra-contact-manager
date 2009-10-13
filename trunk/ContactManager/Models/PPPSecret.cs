namespace ContactManager.Models
{
    public partial class PPPSecret
    {
        public string Profile { get; set; }
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool SystemRealIP { get; set; }
        public bool SystemStayOnline { get; set; }
    }
}
