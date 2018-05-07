using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPScript : MonoBehaviour
{
    static int NUM_NODES = 6;

    private static float[] yaw = new float[NUM_NODES];
    private static float[] pitch = new float[NUM_NODES];
    private static float[] roll = new float[NUM_NODES];
    private static bool[] dataReady = new bool[NUM_NODES];

    private Socket serverSocket = null;
    private List<EndPoint> clientList = new List<EndPoint>();
    private byte[] byteData = new byte[1024];
    private int PORT_NUM = 4210;

    public static bool IsDataReady(int id)
    {
        if (id < NUM_NODES)
            return dataReady[id];
        else
            return false;
    }

    public static float GetYaw(int id)
    {
        if (id < NUM_NODES)
            return yaw[id];
        else
            return 0;
    }
    public static float GetPitch(int id)
    {
        if (id < NUM_NODES)
            return pitch[id];
        else
            return 0;
    }
    public static float GetRoll(int id)
    {
        if (id < NUM_NODES)
            return roll[id];
        else
            return 0;
    }

    // Use this for initialization
    void Start()
    {
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NUM));
        EndPoint newClientEP = new IPEndPoint(IPAddress.Any, 0);
        serverSocket.BeginReceiveFrom(byteData, 0, this.byteData.Length, SocketFlags.None, ref newClientEP, DataReceiveCallback, newClientEP);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void DataReceiveCallback(IAsyncResult iar)
    {
        EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
        int dataLen = 0;
        byte[] data = null;
        try
        {
            dataLen = serverSocket.EndReceiveFrom(iar, ref clientEP);
            data = new byte[dataLen];
            Array.Copy(byteData, data, dataLen);

            String text = Encoding.UTF8.GetString(data);

            //Debug.Log(text);

            String[] list = text.Split('=');
            int id = Convert.ToInt32(list[0]);
            String[] floats = list[1].Split(',');
            floats[2] = floats[2].Substring(0, floats[2].Length - 1);

            if (id < NUM_NODES)
            {
                dataReady[id] = true;
                yaw[id] = Convert.ToSingle(floats[0]);
                pitch[id] = Convert.ToSingle(floats[1]);
                roll[id] = Convert.ToSingle(floats[2]);
            }
        }
        finally
        {
            EndPoint newClientEP = new IPEndPoint(IPAddress.Any, 0);
            serverSocket.BeginReceiveFrom(this.byteData, 0, this.byteData.Length, SocketFlags.None, ref newClientEP, DataReceiveCallback, newClientEP);
        }

        if (!clientList.Any(client => client.Equals(clientEP)))
            clientList.Add(clientEP);
    }

    private void OnDestroy()
    {
        serverSocket.Close();
        serverSocket = null;
        clientList.Clear();
    }
}