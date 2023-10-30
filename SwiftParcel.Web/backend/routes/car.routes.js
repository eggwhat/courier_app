module.exports = app => {
    const cars = require("../controllers/car.controller.js");
    const parcels = require("../controllers/parcel.controller.js");
    const auth = require("../middlewares/auth.middleware");

    var router = require("express").Router();

    router.post("/", auth.authorization, cars.create);
    router.get("/", auth.authorization, cars.findAll);
    router.get("/:id", auth.authorization, cars.findOne);
    router.put("/:id", auth.authorization, cars.update);
    router.delete("/:id", auth.authorization, cars.delete);
    router.get("/:id/parcels", auth.authorization, parcels.findAllByCars);

    app.use('/api/cars', router);
};