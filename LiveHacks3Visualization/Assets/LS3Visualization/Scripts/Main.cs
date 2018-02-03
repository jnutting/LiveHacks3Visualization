using UnityEngine;
using WebSocketSharp;

using System.Threading;
using System;

public class Main : MonoBehaviour {
    private WebSocket ws;
    public UserCollection userCollection;

    void Start() {
        //ws = new WebSocket("ws://echo.websocket.org");
        ws = new WebSocket("ws://stagecast.se/api/events/team_phogg/ws?x-user-listener=1");

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;

        ws.ConnectAsync();        
    }

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
        //Thread.Sleep(3000);
        //ws.SendAsync("This WebSockets stuff is a breeze!", OnSendComplete);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        //Debug.Log("WebSocket server said: " + e.Data);
        //Debug.Log("WebSocket server rawData: " + e.RawData);
        //Thread.Sleep(3000);
        //ws.CloseAsync();

        UnityMainThreadDispatcher.Instance().Enqueue(() => Parse(e.Data));
        //Parse(e.Data);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
    }

    private void OnSendComplete(bool success) {
        Debug.Log("Message sent successfully? " + success);
    }

    private void Parse(string json) {
        UnknownMessageWrapper umw = JsonUtility.FromJson<UnknownMessageWrapper>(json);

        if (umw == null) { 
            Debug.Log("Failed to unpack UnknownType");
            return; 
            }
        //Debug.Log("unpacked UnknownType");
        if (umw.msg.type == "tick") {
            TickMessageWrapper tmw = JsonUtility.FromJson<TickMessageWrapper>(json);
            if (tmw == null) { 
                Debug.Log("Failed to unpack Tick");
                return;
            }
            //Debug.Log("unpacked Tick");
            userCollection.HandleTick(tmw.msg);
        }
        else if (umw.msg.type == "name") {
            NameMessageWrapper wrapper = JsonUtility.FromJson<NameMessageWrapper>(json);
            if (wrapper == null) { 
                Debug.Log("Failed to unpack Name");
                return;
            }
            //Debug.Log("unpacked Tick");
            userCollection.HandleName(wrapper.msg);
        }
        else if (umw.msg.type == "namedColor") {
            NamedColorMessageWrapper wrapper = JsonUtility.FromJson<NamedColorMessageWrapper>(json);
            if (wrapper == null) { 
                Debug.Log("Failed to unpack NamedColor");
                return;
            }
            //Debug.Log("unpacked Tick");
            userCollection.HandleNamedColor(wrapper.msg);
        } else {
            Debug.Log("Unknown event type " + umw.msg.type);
        }
    }
}

[Serializable]
public class UnknownMessageWrapper {
    public UnknownType msg;
}

[Serializable]
public class UnknownType {
    public string type;
}

[Serializable]
public class TickMessageWrapper {
    public Tick msg;
}

[Serializable]
public class Tick {
    public string uuid;
    public float upness;
    public DeviceVelocity velocity;
}

[Serializable]
public class DeviceVelocity {
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class NameMessageWrapper {
    public Name msg;
}

[Serializable]
public class Name {
    public string uuid;
    public string name;
}

[Serializable]
public class NamedColorMessageWrapper {
    public NamedColor msg;
}

[Serializable]
public class NamedColor {
    public string uuid;
    public string colorName;
}