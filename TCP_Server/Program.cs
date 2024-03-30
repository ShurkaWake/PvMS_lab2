using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = null;
const int port = 6000;
const string ip = "127.0.0.1";

try
{
    server = new TcpListener(IPAddress.Parse(ip), port);
    server.Start();
    var address = Dns.GetHostAddresses(Dns.GetHostName())[3].ToString();
    Console.WriteLine($"Server started. IP Address: {address}, Port: {port}");

    while (true)
    {
        Console.WriteLine("Waiting for a client to connect...");
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Client connected...");

        Thread clientThread = new Thread(HandleClient);
        clientThread.Start(client);
    }
}
catch (Exception e)
{
    Console.WriteLine("Error: " + e.Message);
    server?.Stop();
}

static void HandleClient(object obj)
{
    TcpClient client = (TcpClient)obj;
    try
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[8];
        stream.Read(buffer, 0, buffer.Length);
        int first = BitConverter.ToInt32(buffer, 0);
        int second = BitConverter.ToInt32(buffer, 4);
        Console.WriteLine($"Received from client: {first} {second}");

        int answer = first + second;
        byte[] data = BitConverter.GetBytes(answer);
        stream.Write(data, 0, data.Length);
        Console.WriteLine($"Sent to client: {answer}");

        stream.Close();
        client.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: " + e.Message);
        client.Close();
    }
}