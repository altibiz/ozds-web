// TODO: move to Ozds.Modules.Ozds.Assets and use TypeScript

/**
 * @param {string} deviceId
 * @param {Period} period
 * @returns {Promise<DashboardMeasurement[]>}
 */
const getDashboardMeasurements = async (deviceId, period) =>
  query({
    query: gql`
      query ($deviceId: String, $period: PeriodInput) {
        getDashboardMeasurements(deviceId: $deviceId, period: $period)
      }
    `,
    variables: {
      deviceId,
      period: serializePeriod(period),
    },
  });

/**
 * @param {string} ownerId
 * @param {Period} period
 * @returns {Promise<MultiDashboardMeasurements>}
 */
const getDashboardMeasurementsByOwner = async (ownerId, period) =>
  query({
    query: gql`
      query ($ownerId: String, $period: PeriodInput) {
        getDashboardMeasurementsByOwner(ownerId: $ownerId, period: $period)
      }
    `,
    variables: {
      ownerId,
      period: serializePeriod(period),
    },
  });

/**
 * @param {string} ownerUserId
 * @param {Period} period
 * @returns {Promise<MultiDashboardMeasurements>}
 */
const getDashboardMeasurementsByOwnerUser = async (ownerUserId, period) =>
  query({
    query: gql`
      query ($ownerUserId: String, $period: PeriodInput) {
        getDashboardMeasurementsByOnwerUser(
          ownerUserId: $ownerUserId
          period: $period
        )
      }
    `,
    variables: {
      ownerUserId,
      period: serializePeriod(period),
    },
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
 * @property {Date} timestamp
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
 * @property {Date} timestamp
 * @property {DeviceDashboardMeasurementData[]} data
 */

/**
 * @typedef {Object} MultiDashboardMeasurements
 * @property {string[]} deviceIds
 * @property {MultiDashboardMeasurementData[]} data
 */

/**
 * @typedef {Object} Period
 * @property {Date} from
 * @property {Date} to
 */

// NOTE: just for intellisense
const gql = (strings) => strings[0];

const endpoint = "http://localhost:5001/graphql";

const query = async (body) =>
  await fetch(endpoint, {
    method: "POST",
    body: JSON.stringify(body),
    headers: {
      "content-type": "application/json",
    },
  }).then((data) => data.json());

const serializePeriod = (period) => ({
  from: period.from.toISOString(),
  to: period.from.toISOString(),
});

// TODO: document??
window.Dashboard = {
  getDashboardMeasurements,
  getDashboardMeasurementsByOwner,
  getDashboardMeasurementsByOwnerUser,
};
