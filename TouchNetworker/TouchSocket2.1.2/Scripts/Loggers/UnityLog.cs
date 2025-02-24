using System.Text;
using TouchSocket.Core;
using UnityEngine;
using UnityEngine.UI;

public class UnityLog : MonoBehaviour
{
    public Text text_Log;
    public static EasyLogger Logger { get; private set; }

    private void Awake()
    {
        Logger = new EasyLogger(log =>
        {
            Loom.QueueOnMainThread(() =>
            {
                text_Log.text = new StringBuilder(log).Append(text_Log.text).ToString();
            });
        });
    }
}