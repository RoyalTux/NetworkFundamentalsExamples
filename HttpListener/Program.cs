using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Listen();
        }

        private static async Task Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Waiting for requests...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                Console.WriteLine("Received: Method: " + request.HttpMethod + 
                    "\nProtocol Version: " + request.ProtocolVersion + 
                    "\nAt: " + DateTime.Now.ToShortTimeString() + "\n\n");
                HttpListenerResponse response = context.Response;

                string responseString = 
                    "<html>" +
                        "<head>" +
                            "<meta charset='utf8'>" +
                        "</head>" +
                        "<body>" +
                            "It works!" +
                        "</body>" +
                    "</html>";

                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
