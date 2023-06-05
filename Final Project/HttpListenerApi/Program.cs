using ApiLogic.Logic;
using System.Net;
namespace HttpListenerApi
{
    public class Program
    {
        static void Main(string[] args)
        {
            HandleClientRequests().Wait();

        }
        public static async Task<IActionResult> HandleClientRequests()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:7071/");

            listener.Start();

            Console.WriteLine("http://localhost:7071/");

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                HttpListenerRequest request = ctx.Request;
                using HttpListenerResponse resp = ctx.Response;

                string HttpMethod = request.HttpMethod;
                string RawUrl = request.RawUrl;
                System.IO.Stream body = request.InputStream;
                System.Text.Encoding encoding = request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string stringbody = reader.ReadToEnd();
                if (HttpMethod == "post" && RawUrl == "user/{username}/login")
                {
                    bool response = await UserLogic.Login(stringbody);
                    if (response)
                    {
                       
                    }
                    else
                    {
                        
                    }
                     

                }
                
                body.Close();
                reader.Close();


                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("hello");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();

            }
        }
    }
}