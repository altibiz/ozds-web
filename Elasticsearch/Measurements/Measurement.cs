using System;

// NOTE: these have be a nullable types for NEST
// TODO: mapping?

namespace Elasticsearch {
public class Measurement : Common.Measurement {};

public class MeasurementGeoCoordinates : Common.MeasurementGeoCoordinates {};

public class MeasurementData : Common.MeasurementData {};
}
