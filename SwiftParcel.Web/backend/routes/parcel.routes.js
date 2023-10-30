module.exports = app => {
    const parcels = require("../controllers/parcel.controller.js");
    const auth = require("../middlewares/auth.middleware");

    var router = require("express").Router();

    router.post("/", auth.authorization, parcels.create);
    router.get("/", auth.authorization, parcels.findAll);
    router.get("/:id", auth.authorization_check, parcels.findOne);
    router.put("/:id", auth.authorization, parcels.update);
    router.delete("/:id", auth.authorization, parcels.delete);

    app.use('/api/parcels', router);
};