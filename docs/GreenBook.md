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

Each DLMS/COSEM client and each server – a COSEM logical device – is bound to a
Service Access Point (SAP). The SAPs reside in the supporting layer of the
DLMS/COSEM AL. Depending on the communication profile the SAP may be a
TCP-UDP/IP wrapper address, an upper HDLC address, an LLC address etc.

System titles are like MAC adresses for physical devices. Companion
specifications may use a different schema for these.
