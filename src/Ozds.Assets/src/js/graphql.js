const getDashboardMeasurementsByDevice = async (deviceId, period) => {
  const response = await query({
    query: gql`
      query ($deviceId: String!, $period: PeriodInput) {
        dashboardMeasurementsByDevice(deviceId: $deviceId, period: $period) {
          timestamp
          deviceId
          data {
            energy
            highCostEnergy
            lowCostEnergy
            power
            powerL1
            powerL2
            powerL3
            currentL1
            currentL2
            currentL3
            voltageL1
            voltageL2
            voltageL3
          }
        }
      }
    `,
    variables: {
      deviceId,
      period: serializePeriod(period),
    },
  });

  if (!response) {
    throw new Error("Failed fetching ");
  }

  return normalizeDashboardMeasurements(
    response.data.dashboardMeasurementsByDevice,
  );
};

const getDashboardMeasurementsByOwner = async (ownerId, period) => {
  const response = await query({
    query: gql`
      query ($ownerId: String!, $period: PeriodInput) {
        dashboardMeasurementsByOwner(ownerId: $ownerId, period: $period) {
          timestamp
          deviceId
          data {
            energy
            highCostEnergy
            lowCostEnergy
            power
            powerL1
            powerL2
            powerL3
            currentL1
            currentL2
            currentL3
            voltageL1
            voltageL2
            voltageL3
          }
        }
      }
    `,
    variables: {
      ownerId,
      period: serializePeriod(period),
    },
  });

  if (!response) {
    throw new Error("Failed fetching");
  }

  return normalizeMultiDashboardMeasurements(
    response.data.dashboardMeasurementsByOwner,
  );
};

const getDashboardMeasurementsByOwnerUser = async (ownerUserId, period) => {
  const response = await query({
    query: gql`
      query ($ownerUserId: String!, $period: PeriodInput) {
        dashboardMeasurementsByOwnerUser(
          ownerUserId: $ownerUserId
          period: $period
        ) {
          timestamp
          deviceId
          data {
            energy
            highCostEnergy
            lowCostEnergy
            power
            powerL1
            powerL2
            powerL3
            currentL1
            currentL2
            currentL3
            voltageL1
            voltageL2
            voltageL3
          }
        }
      }
    `,
    variables: {
      ownerUserId,
      period: serializePeriod(period),
    },
  });

  if (!response) {
    throw new Error("Failed fetching");
  }

  return normalizeMultiDashboardMeasurements(
    response.data.dashboardMeasurementsByOwnerUser,
  );
};

const normalizeDashboardMeasurements = (measurements) =>
  measurements
    .map((measurement) => ({
      ...measurement,
      timestamp: deserializeDateTime(measurement.timestamp),
    }))
    .sort((a, b) => compareDateTime(a.timestamp, b.timestamp));

const normalizeMultiDashboardMeasurements = (measurements) => {
  measurements = measurements.reduce((current, next) => {
    next = {
      ...next,
      timestamp: deserializeDateTime(next.timestamp),
    };

    if (!current[next.deviceId]) {
      current[next.deviceId] = [];
    }

    current[next.deviceId].push(next);
    return current;
  }, {});

  Object.values(measurements).forEach((deviceMeasurements) =>
    deviceMeasurements.sort((a, b) =>
      compareDateTime(a.timestamp, b.timestamp),
    ),
  );

  return measurements;
};

const deserializePeriod = (period) => ({
  from: deserializeDateTime(period.from),
  to: deserializeDateTime(period.to),
});

const serializePeriod = (period) => ({
  from: serializeDateTime(period.from),
  to: serializeDateTime(period.to),
});

const deserializeDateTime = (dateTime) =>
  luxon.DateTime.fromISO(dateTime, { zone: "utc" });

const serializeDateTime = (dateTime) => dateTime.toISO();

const query = async (body) => {
  try {
    const response = await fetch("/graphql", {
      method: "POST",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    const result = await response.json();
    return result;
  } catch (error) {
    console.log(error);
  }
};

// NOTE: https://stackoverflow.com/a/64855525/4348107
const compareDateTime = (a, b) => a.toMillis() - b.toMillis();

// NOTE: just for intellisense
const gql = (strings) => strings[0];

window.GraphQL = {
  getDashboardMeasurementsByDevice,
  getDashboardMeasurementsByOwner,
  getDashboardMeasurementsByOwnerUser,
  normalizeDashboardMeasurements,
  normalizeMultiDashboardMeasurements,
  deserializePeriod,
  serializePeriod,
  deserializeDateTime,
  serializeDateTime,
};
