using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace IpDnsExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Get host entry using dns example\n");

            await GetHostEntryUsingDns("www.youtube.com");

            Console.WriteLine("\nGet local ip address example\n");

            await GetLocalIPAddress();
        }

        public static async Task GetHostEntryUsingDns(string host)
        {
            IPHostEntry hostEntry = await Dns.GetHostEntryAsync(host);

            Console.WriteLine(hostEntry.HostName);

            foreach (IPAddress ip in hostEntry.AddressList)
            {
                Console.WriteLine(ip.MapToIPv4());
                Console.WriteLine(ip.MapToIPv6());
            }
        }

        public static async Task GetLocalIPAddress()
        {
            IPHostEntry host = await Dns.GetHostEntryAsync(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(ip);
                }
            }
        }
    }
}
