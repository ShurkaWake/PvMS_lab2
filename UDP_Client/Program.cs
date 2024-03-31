using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 5112;
const string ip = "127.0.0.1";

try
{
    UdpClient udpClient = new UdpClient();
    udpClient.Connect(ip, port);

    int a = 30;
    int b = 54;
    byte[] data = BitConverter.GetBytes(a).Concat(BitConverter.GetBytes(b)).ToArray();
    Console.WriteLine($"Sended to server: {a} {b}");
    udpClient.Send(data, data.Length);

    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
    byte[] response = udpClient.Receive(ref serverEndPoint);
    int sum = BitConverter.ToInt32(response, 0);
    Console.WriteLine($"Received from server: {sum}");

    udpClient.Close();
}
catch (SocketException e)
{
    Console.WriteLine($"SocketException: {e}");
}
Console.ReadLine();