using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;

public class MyTcpPlugin : PluginBase, ITcpConnectedPlugin, ITcpClosedPlugin, ITcpReceivedPlugin
{
    private readonly ILog m_logger;

    public MyTcpPlugin(ILog logger)
    {
        this.m_logger = logger;
    }

    public async Task OnTcpClosed(ITcpSession client, ClosedEventArgs e)
    {
        this.m_logger.Info($"�ͻ��ˣ�{client.GetIPPort()}�ѶϿ�����");
        await e.InvokeNext();
    }

    public async Task OnTcpConnected(ITcpSession client, ConnectedEventArgs e)
    {
        this.m_logger.Info($"�ͻ��ˣ�{client.GetIPPort()}������");
        await e.InvokeNext();
    }

    public async Task OnTcpReceived(ITcpSession client, ReceivedDataEventArgs e)
    {
        this.m_logger.Info(e.ByteBlock.ToString());
        await e.InvokeNext();
    }
}