using System;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class TestUdpClient : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public InputField inputField_IpHost;
    public InputField inputField_msg;

    private UdpSession m_udpSession;

    public void Send()
    {
        try
        {
            this.m_udpSession.Send(inputField_msg.text);
        }
        catch (Exception ex)
        {
            UnityLog.Logger.Exception(ex);
        }
    }

    public void InitUdp()
    {
        try
        {
            m_udpSession.SafeDispose();//��ǰ�ͷ�
            m_udpSession = new UdpSession();
            m_udpSession.Received += (remote, e) =>
            {
                UnityLog.Logger.Info($"Udp�յ���{e.ByteBlock.Span.ToString(Encoding.UTF8)}");
                return Task.CompletedTask;
            };

            IPHost iPHost = new IPHost(this.inputField_IpHost.text);//����Ĭ��ԭ����ַ��������ʱ����Ҫ��sendʱָ��
            m_udpSession.Setup(new TouchSocketConfig()
                .SetUdpDataHandlingAdapter(() => new NormalUdpDataHandlingAdapter())//����udp
                                                                                    //.SetUdpDataHandlingAdapter(() => new UdpPackageAdapter())//Udp��ģʽ��֧�ֳ���64k���ݡ�
                .SetBindIPHost(new IPHost(0))//0�˿ڼ�Ϊ����˿�
                .SetRemoteIPHost(iPHost));

            m_udpSession.Start();
            UnityLog.Logger.Info($"Udp��ʼ�����");
        }
        catch (Exception ex)
        {
            UnityLog.Logger.Exception(ex);
        }
    }
}