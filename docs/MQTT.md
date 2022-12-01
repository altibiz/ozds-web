# MQTT

[MQTT](https://mqtt.org/) is the Standard for IoT Messaging.

It is designed as an extremely lightweight publish/subscribe messaging
transport that is ideal for connecting remote devices with a small code
footprint and minimal network bandwidth.

MQTT today is used in a wide variety of industries, such as automotive,
manufacturing, telecommunications, oil and gas, etc.

Features of MQTT are:
 - bidirectional communication
 - lightweight and efficient
 - scales to millions of things
 - reliable message delivery
 - support for unreliable networks
 - security enabled

This could be useful for a number of reasons:
 - there are no DLMS libraries that are not GPL licensed (Gurux.DLMS) which
   means that we cannot create closed source solutions using just DLMS
 - MQTT provides a more robust solution (proxying) to the overall problem of 
   unreliable networks for communicating with measurement devices
 - there is a lot more software support for MQTT solutions
 - MQTT could be used for proxying devices that don't use DLMS as
   a communication protocol

## MQTT Publish/Subscribe Architecture

[!MQTT Publish/Subscribe Architecture](docs/assets/mqtt-publish-subscribe.png)

## Libraries

### MQTTnet

[MQTTnet](https://github.com/dotnet/MQTTnet) is a high performance .NET library
for MQTT based communication. It provides a MQTT client and a MQTT server 
(broker) and supports the MQTT protocol up to version 5. It is compatible with
mostly any supported .NET Framework version and CPU architecture.

LICENSE: MIT

### Gurux.MQTT

[Gurux.MQTT](https://github.com/Gurux/Gurux.MQTT) is a library that allows
GXDLMSDirector to access meters where it's not possible to client establish
connection to the meter, example when dynamic IP addresses are used.

LICENSE: GPL v2

## Products

### MQTT Gateway

[MQTT Gateway](https://www.mqttgateway.net/) is a company that produces devices
acting as gateways between different protocols such as DLMS -> MQTT, DLMS ->
ModBus, ModBus -> MQTT, etc.

## ModBus

[ModBus](https://en.wikipedia.org/wiki/Modbus) is an older protocol that MQTT
wants to improve upon. ModBus was not designed with IoT and 5G in mind and
that's where MQTT steps in.
