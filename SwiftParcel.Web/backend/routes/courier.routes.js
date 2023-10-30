module.exports = app => {
    const couriers = require("../controllers/courier.controller.js");
    const parcels = require("../controllers/parcel.controller.js");
    const auth = require("../middlewares/auth.middleware");

    var router = require("express").Router();

    router.post("/", auth.authorization, couriers.create);
    router.get("/", auth.authorization, couriers.findAll);
    router.get("/:id", auth.authorization, couriers.findOne);
    router.put("/:id", auth.authorization, couriers.update);
    router.delete("/:id", auth.authorization, couriers.delete);

    router.get("/:id/parcels", auth.authorization, parcels.findAllByCourier);

    app.use('/api/couriers', router);
};