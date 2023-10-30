const { getPagination, getPagingData } = require('./shared');
const db = require("../models");
const Cars = db.cars;
const Couriers = db.couriers;

exports.create = (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const car = {
        make: req.body.make,
        model: req.body.model,
        licensePlate: req.body.licensePlate
    }

    Cars.create(car)
        .then(data => {
            res.status(201).send(data);
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while creating the Car."
            });
        });
};

exports.findAll = (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const { page, size } = req.query;
    if (page != null && isNaN(page) || size != null && isNaN(size)) {
        res.status(400).send({
            message: "Page and size must be numbers."
        });
        return;
    }

    const { limit, offset } = getPagination(page, size);

    Cars.findAndCountAll({ limit, offset })
        .then(data => {
            if (data.count == 0) {
                res.status(204).send({
                    message: "Cars were not found."
                });
            } else {
                const response = getPagingData(data, page, limit);
                if (response.page > response.total_pages) {
                    res.status(400).send({
                        message: `Page ${page} was not found.`
                    });
                } else {
                    res.send(response);
                }
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving cars."
            });
        });
};

exports.findOne = async (req, res) => {
    const id = req.params.id;

    const courier = await Couriers.findOne({ where: { userId: req.user.id } });
    if (req.user.role != "Admin" && ((!courier || !courier.carId) || (courier && id != courier.carId))) {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }

    Cars.findByPk(id)
        .then(data => {
            if (data == null) {
                res.status(400).send({
                    message: `Car with id ${id} was not found.`
                });
            } else {
                res.send(data);
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving the Car."
            });
        });
};

exports.update = async (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const id = req.params.id;

    const carExists = await Cars.count({ where: { id } }) > 0;
    if (!carExists) {
        res.status(400).send({
            message: `Car with id ${id} was not found.`
        });
        return;
    }

    if ("id" in req.body) {
        res.status(400).send({
            message: "Id cannot be changed."
        });
        return;
    }

    Cars.update(req.body, {
        where: { id },
    })
        .then(num => {
            if (num == 1) {
                Cars.findByPk(id)
                    .then(data => {
                        res.send(data);
                    });
            } else {
                res.status(400).send({
                    message: `Failed to update Car with id ${id}.`
                });
            }
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while updating the Car."
            });
        });
};

exports.delete = (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const id = req.params.id;

    Cars.destroy({
        where: { id }
    })
        .then(num => {
            if (num == 1) {
                res.status(204).send();
            } else {
                res.status(400).send({
                    message: `Car with id ${id} was not found.`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while deleting the Car."
            });
        });
};