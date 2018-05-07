#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

const char* ssid = "wsn_s18";
const char* password = "tusharg1993";

WiFiUDP Udp;
unsigned int localUdpPort = 4210;  // local port to listen on
char incomingPacket[255];  // buffer for incoming packets
char  replyPacket[] = "Hi there! Got the message :-)";  // a reply string to send back
char customReply[255];

void setup()
{
  Serial.begin(115200);
  Serial.println();

  Serial.printf("Connecting to %s ", ssid);
  WiFi.begin(ssid,password);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println(" connected");

  Udp.begin(localUdpPort);
  Serial.printf("Now listening at IP %s, UDP port %d\n", WiFi.localIP().toString().c_str(), localUdpPort);
  Serial.printf("Writing to IP %d.%d.%d.%d\n",Udp.destinationIP()[0],Udp.destinationIP()[1],Udp.destinationIP()[2],Udp.destinationIP()[3]);
}


void loop()
{ 
  if (Serial.available() > 0){
    int counter = 0;
    customReply[counter] = Serial.read();
    while(customReply[counter] != '!'){
      counter ++;
      customReply[counter] = Serial.read();
    }
    counter ++;
    customReply[counter] = 0;
    Serial.printf("%s\n",customReply);
    Udp.beginPacket("192.168.137.1",localUdpPort);
    Udp.write(customReply);
    Udp.endPacket();
  }
  int packetSize = Udp.parsePacket();
  if (packetSize)
  {
    // receive incoming UDP packets
    Serial.printf("Received %d bytes from %s, port %d\n", packetSize, Udp.remoteIP().toString().c_str(), Udp.remotePort());
    int len = Udp.read(incomingPacket, 255);
    if (len > 0)
    {
      incomingPacket[len] = 0;
    }
    Serial.printf("UDP packet contents: %s\n", incomingPacket);

    // send back a reply, to the IP address and port we got the packet from
    Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
    Udp.write(replyPacket);
    Udp.endPacket();
  }
}
