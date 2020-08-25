using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace LocalWindowService
{
   class Server
    {       
        //static void Main(string[] args)
        //{            
        //    StartServer();
        //}
        public static void StartServer()
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.1.101");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            Console.WriteLine(" ipAddress:" + ipAddress);
            Console.WriteLine("localEndPoint :" + localEndPoint);
            try
            {

                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();
                Console.WriteLine("connected to client");

                // Incoming data from the client.    
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
