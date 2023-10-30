const Sequelize = require("sequelize");

const sequelize = new Sequelize(process.env.db_database, process.env.db_user, process.env.db_password, {
  host: process.env.db_host,
  dialect: process.env.db_dialect,
  operatorsAliases: 0,
  pool: {
    max: Number(process.env.pool_max) || 5,
    min: Number(process.env.pool_min) || 0,
    acquire: Number(process.env.pool_acquire) || 30000,
    idle: Number(process.env.pool_idle) || 10000
  },
  logging: process.env.db_logging === "true"
});

const db = {};

db.Sequelize = Sequelize;
db.sequelize = sequelize;

db.parcels = require("./parcel.model.js")(sequelize, Sequelize);
db.couriers = require("./courier.model.js")(sequelize, Sequelize);
db.cars = require("./car.model.js")(sequelize, Sequelize);
db.users = require("./user.model.js")(sequelize, Sequelize);

db.couriers.hasMany(db.parcels, { as: "parcel" });
db.parcels.belongsTo(db.couriers, {
    foreignKey: "courierId",
    as: "courier",
});

db.cars.hasOne(db.couriers, { as: "car" });
db.couriers.belongsTo(db.cars, {
    foreignKey: {
        name: "carId",
        unique: true
    },
    as: "car",
});

db.users.hasOne(db.couriers, { as: "user" });
db.couriers.belongsTo(db.users, {
    foreignKey: {
        name: "userId",
        unique: true
    },
    as: "user",
});

module.exports = db;