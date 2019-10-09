
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Core.Notifications
{
    public class EmailServer
    {
        public EmailServer(string host, int port = 25)
        {
            Guard.NotEmpty(host, nameof(host));
            Guard.IsPositive(port, nameof(port));

            Host = host;
            Port = port;
        }

        public EmailServer(string name, string username, string password, string host, int port = 25, bool useSsl = false)
        {
            Guard.NotEmpty(name, nameof(name));
            Guard.NotEmpty(username, nameof(username));
            Guard.NotEmpty(password, nameof(password));
            Guard.NotEmpty(host, nameof(host));
            Guard.IsPositive(port, nameof(port));

            Name = name;
            Host = host;
            Port = port;
            UseSsl = useSsl;
            Password = password;
            Username = username;
        }

        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }


    }
}
