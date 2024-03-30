using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 5000;
UdpClient udpServer = null;

try
{
    udpServer = new UdpClient(port);
    Console.WriteLine($"Server started. IP Address: {Dns.GetHostAddresses(Dns.GetHostName())[3]}, Port: {port}");

    while (true)
    {
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] receiveBytes = udpServer.Receive(ref clientEndPoint);
        int first = BitConverter.ToInt32(receiveBytes, 0);
        int second = BitConverter.ToInt32(receiveBytes, 4);
        Console.WriteLine($"Received from client: {first} {second}");

        int answer = first + second;
        byte[] data = BitConverter.GetBytes(answer);
        udpServer.Send(data, data.Length, clientEndPoint);
        Console.WriteLine($"Sent to client: {answer}");
    }
}
catch (SocketException e)
{
    Console.WriteLine($"SocketException: {e}");
}
finally
{
    udpServer?.Close();
}