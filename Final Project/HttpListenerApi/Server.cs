using ApiLogic.Logic;
using Microsoft.Azure.Cosmos;
using System.Net;
using System.Threading;

namespace HttpListenerApi
{
    public class Server
    {
        static void Main(string[] args)
        {
            HandleClientRequests();

        }
        public static  void HandleClientRequests()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:7071/");

            listener.Start();

            Console.WriteLine("http://127.0.0.1:7071/");

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                Thread th = new Thread(RequestHandler.RequestHandle);
                th.Start(ctx);
                
            }
        }
        
    }
}