using System.Net.Sockets;
using System.Text;

const string ip = "127.0.0.1";
const int port = 6000;

try
{
    TcpClient client = new TcpClient(ip, port);
    Console.WriteLine("Connected to server...");

    NetworkStream stream = client.GetStream();

    int a = 15;
    int b = 17;
    byte[] data = BitConverter.GetBytes(a).Concat(BitConverter.GetBytes(b)).ToArray();
    Console.WriteLine($"Sended to server: {a} {b}");
    stream.Write(data, 0, data.Length);

    byte[] response = new byte[4];
    stream.Read(response, 0, response.Length);
    int sum = BitConverter.ToInt32(response, 0);
    Console.WriteLine($"Received from server: {sum}");

    stream.Close();
    client.Close();
}
catch (Exception e)
{
    Console.WriteLine("Error: " + e.Message);
}

Console.ReadLine();