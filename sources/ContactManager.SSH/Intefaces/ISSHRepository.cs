using System.Collections.Generic;

namespace ContactManager.SSH.Intefaces
{
    public interface ISSHRepository
    {
        bool AutoMode { get; set; }

        string Connect(string host, string username, string password);
        void Disconnect();
        string RunCommand(string command);
        string BuildCommand(string key, string value);
        Dictionary<string, string> BuildProperties(string str);
        string GetComment(string str, IDictionary<string, string> kvp);
        string GetValue(IDictionary<string, string> properties, string property);
    }
}