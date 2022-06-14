// TODO: move to Ozds.Modules.Ozds or something and use TypeScript
// TODO: better error management
// NOTE: requires luxon

/**
 * @typedef {Window & GraphQL} GraphQLWindow
 */

/**
 * @typedef {Object} GraphQL
 * @property {typeof getDashboardMeasurements}
 *   getDashboardMeasurements
 * @property {typeof getDashboardMeasurementsByOwner}
 *   getDashboardMeasurementsByOwner
 * @property {typeof getDashboardMeasurementsByOwnerUser}
 *   getDashboardMeasurementsByOwnerUser
 * @property {typeof normalizeDashboardMeasurements}
 *   normalizeDashboardMeasurements
 * @property {typeof normalizeMultiDashboardMeasurements}
 *   normalizeMultiDashboardMeasurements
 * @property {typeof deserializePeriod}
 *   deserializePeriod
 * @property {typeof serializePeriod}
 *   serializePeriod
 */

/**
 * @param {string} deviceId
 * @param {Period} period
 * @returns {Promise<DashboardMeasurement[]>}
 */
const getDashboardMeasurements = async (deviceId, period) => {
  const response = await query({
    query: gql`
      query ($deviceId: String!, $period: PeriodInput) {
        dashboardMeasurements(deviceId: $deviceId, period: $period) {
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
    throw new Error("Failed fetching");
  }

  return normalizeDashboardMeasurements(response.data.dashboardMeasurements);
};

/**
 * @param {string} ownerId
 * @param {Period} period
 * @returns {Promise<MultiDashboardMeasurements>}
 */
const getDashboardMeasurementsByOwner = async (ownerId, period) => {
  const response = await query({
    query: gql`
      query ($ownerId: String!, $period: PeriodInput) {
        dashboardMeasurementsByOwner(ownerId: $ownerId, period: $period) {
          deviceIds
          measurements {
            timestamp
            data {
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

/**
 * @param {string} ownerUserId
 * @param {Period} period
 * @returns {Promise<MultiDashboardMeasurements>}
 */
const getDashboardMeasurementsByOwnerUser = async (ownerUserId, period) => {
  const response = await query({
    query: gql`
      query ($ownerUserId: String!, $period: PeriodInput) {
        dashboardMeasurementsByOnwerUser(
          ownerUserId: $ownerUserId
          period: $period
        ) {
          deviceIds
          measurements {
            timestamp
            data {
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
    response.data.dashboardMeasurementsByOnwerUser,
  );
};

/**
 * @param {any} multi
 * @returns {MultiDashboardMeasurements}
 */
const normalizeMultiDashboardMeasurements = (multi) => ({
  ...multi,
  measurements: multi.measurements
    .map((measurement) => ({
      ...measurement,
      timestamp: new luxon.DateTime(measurement.timestamp),
    }))
    .sort((a, b) => a.timestamp - b.timestamp),
});

/**
 * @param {any} measurements
 * @returns {DashboardMeasurement[]}
 */
const normalizeDashboardMeasurements = (measurements) =>
  measurements
    .map((measurement) => ({
      ...measurement,
      timestamp: new luxon.DateTime(measurement.timestamp),
    }))
    .sort((a, b) => a.timestamp - b.timestamp);

/**
 * @param {SerializedPeriod} period
 * @returns {Period}
 */
const deserializePeriod = (period) => ({
  from: new luxon.DateTime(period.from),
  to: new luxon.DateTime(period.to),
});

/**
 * @param {Period} period
 * @returns {SerializedPeriod}
 */
const serializePeriod = (period) => ({
  from: period.from.toISO(),
  to: period.to.toISO(),
});

/**
 * @typedef {Object} DashboardMeasurementData
 * @property {number} energy
 * @property {number} highCostEnergy
 * @property {number} lowCostEnergy
 * @property {number} power
 * @property {number} powerL1
 * @property {number} powerL2
 * @property {number} powerL3
 * @property {number} currentL1
 * @property {number} currentL2
 * @property {number} currentL3
 * @property {number} voltageL1
 * @property {number} voltageL2
 * @property {number} voltageL3
 */

/**
 * @typedef {Object} DashboardMeasurement
 * @property {luxon.DateTime} timestamp
 * @property {string} deviceId
 * @property {DashboardMeasurementData} data
 */

/**
 * @typedef {Object} DeviceDashboardMeasurementData
 * @property {string} deviceId
 * @property {DashboardMeasurementData} data
 */

/**
 * @typedef {Object} MultiDashboardMeasurementData
 * @property {luxon.DateTime} timestamp
 * @property {DeviceDashboardMeasurementData[]} data
 */

/**
 * @typedef {Object} MultiDashboardMeasurements
 * @property {string[]} deviceIds
 * @property {MultiDashboardMeasurementData[]} data
 */

/**
 * @typedef {Object} Period
 * @property {luxon.DateTime} from
 * @property {luxon.DateTime} to
 */

/**
 * @typedef {Object} SerializedPeriod
 * @property {string} from
 * @property {string} to
 */

const query = async (body) => {
  try {
    // TODO: correct endpoint
    const response = await fetch("https://localhost:5001/graphql", {
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

window.GraphQL = {
  getDashboardMeasurements,
  getDashboardMeasurementsByOwner,
  getDashboardMeasurementsByOwnerUser,
  normalizeDashboardMeasurements,
  normalizeMultiDashboardMeasurements,
  deserializePeriod,
  serializePeriod,
};

// NOTE: just for intellisense
const gql = (strings) => strings[0];
