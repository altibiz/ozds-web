# DLMS

[DLMS/COSEM](https://www.dlms.com/) is the standard language for smart devices.

Smart metering - and more generally, smart devices - need a globally accepted
standard language that ensures interoperability, efficiency and security.

DLMS/COSEM provides just that.

[Webinar](https://www.youtube.com/watch?v=ku0Cvu9OzT0).

[Tester](https://kalkitech.com/products/dlms-software/dlms-million-meter-simulator/).

## Libraries

The main problem is that there is not a lot of software support for DLMS and the
existing support is either not robust enough or is GPL licensed. We should
probably create an in-house implementation of the protocol based on existing
implementations and connect bridge it to MQTT which is more supported and
robust.

### Gurux.DLMS.Net

[Gurux.DLMS.Net](https://github.com/Gurux/Gurux.DLMS.Net) library is a
high-performance .NET component that helps you to read you DLMS/COSEM
compatible electricity, gas or water meters.

LICENSE: GPL v2

## Applications

### GXDLMSDirector

With [GXDLMSDirector](https://github.com/Gurux/GXDLMSDirector) application you
can read DLMS/COSEM compatible electricity, gas or water meters. You can control
your meters with easy to use user interface.

LICENSE: GPL v2

## IEC 62056

[IEC 62056](https://en.wikipedia.org/wiki/IEC_62056) is a set of standards for
electricity metering data exchange by International Electrotechnical Commission.

The IEC 62056 standards are the international standard versions of the
DLMS/COSEM specification.
