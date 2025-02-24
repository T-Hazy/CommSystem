using System;
using TouchSocket.Core;
using TouchSocket.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class TestTcpClient : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    public InputField inputField_Iphost;
    public InputField inputField_Msg;

    private TcpClient m_tcpClient;

    public void Connect()
    {
        try
        {
            m_tcpClient.SafeDispose();
            m_tcpClient = new TcpClient();
            //��������
            TouchSocketConfig config = new TouchSocketConfig();
            config.ConfigureContainer(a =>
                {
                    a.AddLogger(UnityLog.Logger);
                })
                .ConfigurePlugins(a =>
                {
                    a.Add<MyTcpPlugin>();
                })
                .SetRemoteIPHost(new IPHost(inputField_Iphost.text))
                .SetTcpDataHandlingAdapter(() => new FixedHeaderPackageAdapter());

            //��������
            m_tcpClient.Setup(config);

            m_tcpClient.Connect();

            UnityLog.Logger.Info("Connected");
        }
        catch (Exception ex)
        {
            UnityLog.Logger.Exception(ex);
        }
    }

    public void Send()
    {
        try
        {
            m_tcpClient.Send(this.inputField_Msg.text);
        }
        catch (Exception ex)
        {
            m_tcpClient.Logger.Exception(ex);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}