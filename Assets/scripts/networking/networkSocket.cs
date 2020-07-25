using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Linq;

 

public class networkSocket : MonoBehaviour
{
    public String host = "localhost";
    public Int32 port = 1024;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;



    /*void Update()
    {
        string received_data = readSocket();
        string key_stroke = Input.inputString;

        // Collects keystrokes into a buffer
        if (key_stroke != ""){
            input_buffer += key_stroke;

            if (key_stroke == "\n"){
            	// Send the buffer, clean it
            	Debug.Log("Sending: " + input_buffer);
            	writeSocket(input_buffer);
            	input_buffer = "";
            }

        }


        if (received_data != "")
        {
        	// Do something with the received data,
        	// print it in the log for now
            Debug.Log(received_data);
        }
    } */


    void Awake()
    {
        //setupSocket();
    }

    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e)
        {
        	// Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string user, string bot, string msg)
    {
        if (!socket_ready)
            return;
            
        //line = line + "\r\n";
        Byte[] userdata = Encoding.ASCII.GetBytes(user + '\0');
        Byte[] botdata = Encoding.ASCII.GetBytes(bot + '\0');
        Byte[] msgdata = Encoding.ASCII.GetBytes(msg + '\0');
        Byte[] data = userdata.Concat(botdata).Concat(msgdata).ToArray();
        net_stream.Write(data,0, data.Length);
        //socket_writer.Flush();
       print("Message "+ data.ToString() + " Sent");
    }

    public String readSocket()
    {
        if (!socket_ready)
            return "Socket is not Ready";

		if(net_stream.CanRead)
		{
			byte[] myReadBuffer = new byte[1024];
          	StringBuilder myCompleteMessage = new StringBuilder();
          	int numberOfBytesRead = 0;

          	// Incoming message may be larger than the buffer size.
          	do{
               	numberOfBytesRead = net_stream.Read(myReadBuffer, 0, myReadBuffer.Length);
				myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
          	}
          	while(net_stream.DataAvailable);
			return myCompleteMessage.ToString();
		}      

        return "No Data Available";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }

	public string SendMsg(string user, string bot, string msg)
	{
		setupSocket();
		writeSocket(user, bot, msg);
		var result = readSocket();
        closeSocket();
        return result;
	}

}