namespace ContactManager.Web.Interfaces
{
    public interface IActiveConnections
    {
        string Name { get; set; }
        string Service { get; set; }
        string CallerId { get; set; }
        string Address { get; set; }
        string Uptime { get; set; }
        string Encoding { get; set; }
        string SessionId { get; set; }
        string LimitBytesIn { get; set; }
        string LimitBytesOut { get; set; }
    }
}