const { getPagination, getPagingData } = require('./shared');
const db = require("../models");
const Couriers = db.couriers;
const Cars = db.cars;
const Users = db.users;

exports.create = (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const courier = {
        firstname: req.body.firstname,
        lastname: req.body.lastname,
        phone: req.body.phone,
        status: req.body.status,
        carId: req.body.carId,
        userId: req.body.userId
    };

    Couriers.create(courier)
        .then(data => {
            res.status(201).send(data);
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while creating the Courier."
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

    Couriers.findAndCountAll({
        limit,
        offset,
        include: [{ model: Cars, as: "car", attributes: ['id', 'licensePlate'] }, { model: Users, as: "user", attributes: ['id', 'username', 'email'] }],
        attributes: { exclude: ['carId', 'userId'] },
    })
        .then(data => {
            if (data.count == 0) {
                res.status(204).send({
                    message: "Couriers were not found."
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
                message: err.message || "Some error occurred while retrieving couriers."
            });
        });
};

exports.findOne = (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const id = req.params.id;

    Couriers.findByPk(id, { include: ["car", "user"], attributes: { exclude: ['carId', 'userId'] } })
        .then(data => {
            if (data == null) {
                res.status(400).send({
                    message: `Courier with id ${id} was not found.`
                });
            } else {
                res.send(data);
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving the Courier."
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

    const courierExists = await Couriers.count({ where: { id } }) > 0;
    if (!courierExists) {
        res.status(400).send({
            message: `Courier with id ${id} was not found.`
        });
        return;
    }

    if ("id" in req.body) {
        res.status(400).send({
            message: "Id cannot be changed."
        });
        return;
    }

    if ("carId" in req.body && req.body.carId != null) {
        const carExists = await Cars.count({ where: { id: req.body.carId } }) > 0;
        if (!carExists) {
            res.status(400).send({
                message: `Car with id ${req.body.carId} was not found.`
            });
            return;
        }
    }

    if ("userId" in req.body && req.body.userId != null) {
        const userExists = await Users.count({ where: { id: req.body.userId } }) > 0;
        if (!userExists) {
            res.status(400).send({
                message: `User with id ${req.body.userId} was not found.`
            });
            return;
        }
    }

    Couriers.update(req.body, {
        where: { id },
    })
        .then(num => {
            if (num == 1) {
                Couriers.findByPk(id, { include: ["car", "user"], attributes: { exclude: ['carId', 'userId'] } })
                    .then(data => {
                        res.send(data);
                    });
            } else {
                res.status(400).send({
                    message: `Failed to update Courier with id ${id}.`
                });
            }
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while updating the Courier."
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

    Couriers.destroy({
        where: { id }
    })
        .then(num => {
            if (num == 1) {
                res.status(204).send();
            } else {
                res.status(400).send({
                    message: `Courier with id ${id} was not found.`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while deleting the Courier."
            });
        });
};