using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
public class ChatBot : MonoBehaviour 
{
	public string server = "127.0.0.1", port = "1024";
	public string user, bot;

	public DictationScript dictation;
	public InputField text;
	public networkSocket netSock;

	// Use this for initialization
	private Process proc;
	void Start () 
	{
		//proc = new Process();
		//proc.StartInfo.FileName = Application.streamingAssetsPath + "/ChatScript/ChatScript/BINARIES/ChatScript";
		//proc.StartInfo.Arguments = "port=1024";
		//proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		//proc.Start();
		//Process.Start(Application.streamingAssetsPath + "/ChatScript/ChatScript/SERVER BATCH FILES/LocalServer.bat");
		//print("ChatSript Server started");

		SendChat("Hello");

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SendChat(string text)
	{
		//print(GetSocket.SocketSendReceive(server,Convert.ToInt32(port),user + Char.MinValue + bot + Char.MinValue +text.text+ Char.MinValue));
		//netSock.SendMsg("\0"+"\0"+"\0");
		string msg = user + " " + bot +  " " + text;
		print("Message to send: " + msg);
		WindowsVoice.Speak(netSock.SendMsg(user, bot, text));
	}

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
	{
		//SendChat(":quit");
		//proc.Kill();
	}
	
}
