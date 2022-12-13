# DLMS Green Book

## Scope

The DLMS/COSEM specification specifies an interface model and communication
protocols for data exchange with metering equipment.

The DLMS/COSEM specification follows a three-step approach:

- Step 1, Modelling: This covers the interface model of metering equipment and
  rules for data identification;
  - Step 2, Messaging: This covers the services for mapping the interface model
    to protocol data units (APDU) and the encoding of this APDUs.
  - Step 3, Transporting: This covers the transportation of the messages through
    the communication channel.

Step 1 is specified in the Blue Book, while the Green Book covers steps 2 and 3.

## Information exchange in DLMS/COSEM

The objective of DLMS/COSEM is to specify a standard for a business domain
oriented interface object model for metering devices and systems, as well as
services to access the objects. Communication profiles to transport the messages
through various communication media are also specified.

The term "metering devices" is an abstraction; consequently "metering device"
may be any type of device for which this abstraction is suitable.

### Communication model

DLMS/COSEM uses the concepts of the Open Systems Interconnection (OSI) model
to model information exchange between meters and data collection systems.

### Naming and addressing

Each DLMS/COSEM client and each server – a COSEM logical device – is bound to a
Service Access Point (SAP). The SAPs reside in the supporting layer of the
DLMS/COSEM AL. Depending on the communication profile the SAP may be a
TCP-UDP/IP wrapper address, an upper HDLC address, an LLC address etc.

System titles are like MAC adresses for physical devices. Companion
specifications may use a different schema for these.

You have to exchange system titles before any cryptography is used. There are
a couple of ways of doing this specified in the book.

### Connection oriented operation

A communication session consists of three phases:

- first, an application level connection, called AA (Application Association),
  is established between a client and a server AE (Application Entity).
  Before initiating the establishment of an AA, the peer PhLs of the client
  and server side protocol stacks have to be connected. The intermediate
  layers may have to be connected or not. Each layer, which needs to be
  connected, may support one or more connections simultaneously;
- once the AA is established, message exchange can take place;
- at the end of the data exchange, the AA is released.

For the purposes of very simple devices, one-way communicating devices,
and for multicasting and broadcasting pre-established AAs are also allowed.
Pre-established AAs cannot be released. See also the complete Green Book.

### Application associations

Only client requests can establish an AA.

A confirmed AA is proposed by the client and accepted by the server provided
that:

- the user of the client is known by the server;
- the application context proposed by the client is acceptable for the server;
- the authentication mechanism proposed by the client is acceptable for
  the server and the authentication is successful;
- the elements of the xDLMS context can be successfully negotiated between
  the client and the server.

Unconfirmed AAs are useful for sending broadcast messages.

AAs are modelled by COSEM "Association SN / LN" objects that hold the SAPs
identifying the associated partners, the name of the application context,
the name of the authentication mechanism, and the xDLMS context.

The "Association SN / LN" objects also determine a specific set of access
rights to COSEM object attributes and methods and they point to (reference)
a “Security setup” object that hold the elements of the security context.
The access rights and the security context may be different in each AA. These
objects are specified in DLMS UA 1000-1.

#### Application context

The application context determines:

- the set of Application Service Elements (ASEs) present in the AL;
- the referencing style of COSEM object attributes and methods: short name
  (SN) referencing or logical name (LN) referencing;
- the transfer syntax;
- whether ciphering is used or not.

Application contexts are identified by names.

#### Authentication

In DLMS/COSEM, authentication takes place during AA establishment.

In confirmed AAs either the client (unilateral authentication) or both the
client and the server (mutual authentication) can authenticate itself.

In an unconfirmed AA, only the client can authenticate itself.

In pre-established AAs, authentication of the communicating partners is not
available.

#### xDLMS context

The xDLMS context determines the set of xDLMS services and capabilities that
can be used in a given AA.

#### Security context

The security context is relevant when the application context stipulates
ciphering. It comprises the security suite, the security policy, the security
keys and other security material. See also the complete Green Book. It is
managed by “Security setup” objects.

#### Access rights

Access rights determine the rights of the client(s) to access COSEM object
attributes and methods within an AA. The set of access rights depend on the
role of the client and is pre-configured in the server. See also the complete
Green Book.

#### Messaging patterns

In confirmed AAs:

- the client can send confirmed service requests and the server responds:
  pull operation;
- the client can send unconfirmed service requests. The server does not
  respond;
- the server can send unsolicited service requests to the client:
  push operation. NOTE: The unsolicited services may be InformationReport
  (with SN referencing), EventNotification (with LN referencing) or
  DataNotification (used both with SN and LN referencing).

In unconfirmed AAs:

- only the client can initiate service requests and only unconfirmed ones.
  The server cannot respond and it cannot initiate service requests.

#### Data exchange between third parties and DLMS/COSEM servers

Third parties – that are outside the DLMS/COSEM client-server relationship –
may also exchange information with servers, using a client as a broker. To
support end-to-end security, such third parties shall be "DLMS/COSEM aware"
meaning that they shall be able to send messages to the client that contain
properly formatted xDLMS APDUs carrying properly formatted COSEM data, and
that they shall be able to process messages received from the server via the
client. See also the complete Green Book.

Messages from the server to the third party may be solicited or unsolicited.

#### Communication profiles

This chapter specifies how DLMS/COSEM interacts with OSI.

Communication profiles specify how the DLMS/COSEM AL and the COSEM data model
modelling the Application Process (AP) are supported by the lower,
communication media specific protocol layers.

#### Model of a DLMS/COSEM metering system

Metering equipment is modelled as a set of logical devices, hosted in a single
physical device. Each logical device represents a server AP and models a subset
of the functionality of the metering equipment as these are seen through its
communication interfaces. The various functions are modelled using COSEM
objects.

#### Model of DLMS/COSEM servers

The following uses the
[HDLC](https://en.wikipedia.org/wiki/High-Level_Data_Link_Control) protocol as
an example.

The DLMS/COSEM AL is supported by the HDLC based data link layer. Its main
role is to provide a reliable data transfer between the peer layers. It also
provides addressing of the logical devices in such a way, that each logical
device is bound to a single HDLC address. The Management Logical Device is
always bound to the address 0x01. To allow creating a local network so that
several metering devices at a given metering site can be reached through a
single access point, another address, the physical address is also provided by
the data link layer. The logical device addresses are referred to as upper HDLC
addresses, while the physical device address is referred to as a lower HDLC
address.

The DLMS/COSEM AL is supported by the DLMS/COSEM TL, comprising the internet
TCP or UDP layer and a wrapper. The main role of the wrapper is to adapt the
OSI-style service set, provided by the DLMS/COSEM TL to and from TCP and UDP
function calls. It also provides addressing for the logical devices, binding
them to a SAP called wrapper port. The Management Logical Device is always
bound to wrapper port 0x01. Finally, the wrapper provides information about the
length of the APDUs transmitted, to help the peer to recognise the end of
the APDU. This is necessary due the streaming nature of TCP.

#### Model of a DLMS/COSEM client

The model of the client – obviously – is very similar to the model of the
servers:

- in this particular model, the DLMS/COSEM AL is supported either by the HDLC
  based data link layer or the DLMS/COSEM TL, meaning that the AL uses the
  services of one or the other as determined by the APs. In other words, the
  APDUs are received from or sent through the appropriate supporting layer,
  which in turn use the services of its supporting layer respectively;
- unlike on the server side, the addressing provided by the HDLC layer has a
  single level only, that of the Service Access Points (SAP) of each
  Application Process (AP).

As explained, client APs and server APs are identified by their SAPs.
Therefore, an AA between a client and a server side AP can be identified by a
pair of client and server SAPs.

#### Interoperability and interconnectivity in DLMS/COSEM

In the DLMS/COSEM environment, interoperability and interconnectivity is
defined between client and server AEs. A client and a server AE must be
interoperable and interconnectable to ensure data exchange between the two
systems.

Interoperability is when the client and server are in sync with the syntax and
the semantics of AEs. Interconnectivity is when all protocols of the server and
client are connected.

#### Ensuring interconnectivity: the protocol identification service

In DLMS/COSEM, AA establishment is always initiated by the client AE. However,
in some cases, it may not have knowledge about the protocol stack used by an
unknown server device (for example when the server has initiated the physical
connection establishment). In such cases, the client AE needs to obtain
information about the protocol stack implemented in the server.

A specific, application level service is available for this purpose: the
protocol identification service. It is an optional application level service,
allowing the client AE to obtain information – after establishing a physical
connection – about the protocol stack implemented in the server. The protocol
identification service, specified in the complete Green Book, uses directly the
data transfer services (PHDATA.request /.indication) of the PhL; it bypasses
the other protocol layers. It is recommended to support it in all communication
profiles that have access to the PhL.

#### System integration and meter installation

System integration is supported by DLMS/COSEM in a number of ways. A possible
process is described here.

The presence of a Public Client (bound to address 0x10 in any profile) is
mandatory in each client system. Its main role is to reveal the structure of
an unknown – for example newly installed –metering equipment. This takes place
within a mandatory AA between the Public Client and the Management Logical
Device, with no security precautions. Once the structure is known, data can be
accessed with using the proper authentication mechanisms and cryptographic
protection of the xDLMS messages and COSEM data.

When a new meter is installed in the system, it may generate an event report to
the client. Once this is detected, the client can retrieve the internal
structure of the meter, and then send the necessary configuration information
(for example tariff schedules and installation specific parameters) to the
meter. With this, the meter is ready to use. System integration is also
facilitated by the availability of the DLMS/COSEM conformance testing,
described in the Yellow Book, DLMS UA 1001-1. With this, correct implementation
of the specification in metering equipment can be tested and certified.
