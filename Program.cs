using System.Net.NetworkInformation;

using CancellationTokenSource cancellationTokenSource = new();
CancellationToken cancellationToken = cancellationTokenSource.Token;

Ping pingSender = new();

using CancellationTokenRegistration cancellationTokenRegistration =
                                    cancellationToken.Register(() => pingSender.SendAsyncCancel());

int i = 0;
while (true)
{
    cancellationToken.ThrowIfCancellationRequested();

    PingReply pingReply = await pingSender.SendPingAsync("google.com");
    Console.Write($"Status: {pingReply.Status}\t");
    Console.Write($"RoundtripTime: {pingReply.RoundtripTime}\t");
    Console.Write($"IP Address: {pingReply.Address}\t");
    Console.Write($"TTL: {pingReply.Options.Ttl}\t");
    Console.WriteLine($"DontFragment: {pingReply.Options.DontFragment}\t");
    if (i == 0)
    {
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(10)); // This is not blocking call
    }
    i++;
}
