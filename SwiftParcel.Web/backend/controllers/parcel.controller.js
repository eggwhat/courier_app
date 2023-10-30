const { getPagination, getPagingData } = require('./shared');
const db = require("../models");
const userModel = require('../models/user.model');
const Parcels = db.parcels;
const Couriers = db.couriers;

exports.create = (req, res) => {
    if (req.user.role != 'Admin') {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }

    const parcel = {
        senderName: req.body.senderName,
        senderAddress: req.body.senderAddress,
        senderPhone: req.body.senderPhone,
        receiverName: req.body.receiverName,
        receiverAddress: req.body.receiverAddress,
        receiverPhone: req.body.receiverPhone,
        weight: req.body.weight,
        price: req.body.price,
        status: req.body.status,
        courierId: req.body.courierId
    }

    Parcels.create(parcel)
        .then(data => {
            res.status(201).send(data);
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while creating the Parcel."
            });
        });
};

exports.findAll = async (req, res) => {
    const { page, size, unassigned } = req.query;
    if (page != null && isNaN(page) || size != null && isNaN(size)) {
        res.status(400).send({
            message: "Page and size must be numbers."
        });
        return;
    }

    const { limit, offset } = getPagination(page, size);

    let where = {};
    const courier = await Couriers.findOne({
        where: { userId: req.user.id },
        attributes: ['id']
    });

    if(unassigned != null && unassigned == 'true') {
        where.courierId = null;
    }

    if (courier != null) {
        where.courierId = courier.id;
    } else if (req.user.role != 'Admin') {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }

    Parcels.findAndCountAll({ limit, offset, where, include: {
        model: Couriers,
        as: "courier",
        attributes: ['id', 'firstname', 'lastname'],
    }, attributes: {
        exclude: ['courierId']
    }})
        .then(data => {
            if (data.count == 0) {
                res.status(204).send({
                    message: "Parcels were not found."
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
                message: err.message || "Some error occurred while retrieving parcels."
            });
        });
};

exports.findAllByCourier = async (req, res) => {
    const id = req.params.id;
    const { page, size } = req.query;
    if (page != null && isNaN(page) || size != null && isNaN(size)) {
        res.status(400).send({
            message: "Page and size must be numbers."
        });
        return;
    }

    const { limit, offset } = getPagination(page, size);

    const courier = await Couriers.findOne({
        where: { id: id, userId: req.user.id },
        attributes: ['id']
    });

    if (req.user.role != 'Admin' && courier == null) {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }

    Parcels.findAndCountAll({limit, offset, where: { courierId: id }})
        .then(data => {
            if (data.count == 0) {
                res.status(400).send({
                    message: `Parcels with courier id ${id} were not found.`
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
                message: err.message || "Some error occurred while retrieving parcels."
            });
        });
};

exports.findAllByCars = (req, res) => {
    if (req.user.role != 'Admin') {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }

    const id = req.params.id;
    const { page, size } = req.query;
    if (page != null && isNaN(page) || size != null && isNaN(size)) {
        res.status(400).send({
            message: "Page and size must be numbers."
        });
        return;
    }

    const { limit, offset } = getPagination(page, size);
    Parcels.findAndCountAll({
        limit, offset,
        include: [
            {
                model: Couriers,
                as: "courier",
                where: { carId: id },
                attributes: ['id', 'firstname', 'lastname']
            }
        ],
        attributes: {
            exclude: ['courierId']
        }
    })
        .then(data => {
            if (data.count == 0) {
                res.status(400).send({
                    message: `Parcels with car id ${id} were not found.`
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
                message: err.message || "Some error occurred while retrieving parcels."
            });
        });
};

exports.findOne = async (req, res) => {
    const id = req.params.id;

    const data = {
        where: { "parcelNumber": id },
    }

    if(req.user != null) {
        const courier = await Couriers.findOne({
            where: { userId: req.user.id },
            attributes: ['id']
        });
    
        if (req.user.role == 'Admin' || courier != null) {
            data.include = {
                model: Couriers,
                as: "courier",
                include: ["car", "user"],
                attributes: {
                    exclude: ['carId', 'userId']
                }
            }
            data.attributes = {
                exclude: ['courierId']
            }
        } else {
            data.attributes = [ 'parcelNumber', 'status', 'senderName', 'receiverName', 'weight', 'createdAt', 'updatedAt' ]
        }
    } else {
        data.attributes = [ 'parcelNumber', 'status', 'senderName', 'receiverName', 'weight', 'createdAt', 'updatedAt' ]
    }

    Parcels.findOne(data)
        .then(data => {
            if (data == null) {
                res.status(400).send({
                    message: `Parcel with id ${id} was not found.`
                });
            } else {
                res.send(data);
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving the Parcel."
            });
        });
};

exports.update = async (req, res) => {
    const id = req.params.id;

    let where = {};
    const courier = await Couriers.findOne({
        where: { userId: req.user.id },
        attributes: ['id']
    });

    if (courier != null) {
        where.courierId = courier.id;
    } else if (req.user.role != 'Admin') {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }

    const parcelExists = await Parcels.count({ where: { parcelNumber: id, ...where } }) > 0;
    if (!parcelExists) {
        res.status(400).send({
            message: `Parcel with id ${id} was not found.`
        });
        return;
    }

    if(req.user.role != 'Admin' && where.hasOwnProperty('courierId') && (!req.body.hasOwnProperty('status') || (req.body.hasOwnProperty('status') && Object.keys(req.body).length > 1))) {
        res.status(400).send({
            message: "Page access is restricted."
        });
        return;
    }

    if ("parcelNumber" in req.body) {
        res.status(400).send({
            message: "Parcel number cannot be changed."
        });
        return;
    }

    if ("courierId" in req.body && req.body.courierId != null) {
        const courierExists = await Couriers.count({ where: { id: req.body.courierId } }) > 0;;
        if (!courierExists) {
            res.status(400).send({
                message: `Courier with id ${req.body.courierId} was not found.`
            });
            return;
        }
    }

    Parcels.update(req.body, {
        where: { parcelNumber: id, ...where },
    })
        .then(num => {
            if (num == 1) {
                Parcels.findByPk(id, {
                    include: [
                        {
                            model: Couriers,
                            as: "courier",
                            include: ["car", "user"],
                            attributes: {
                                exclude: ['carId', 'userId']
                            }
                        }
                    ],
                    attributes: {
                        exclude: ['courierId']
                    }
                })
                    .then(data => {
                        res.send(data);
                    });
            } else {
                res.status(400).send({
                    message: `Failed to update Parcel with id ${id}.`
                });
            }
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while updating the Parcel."
            });
        });
};

exports.delete = (req, res) => {
    if (req.user.role != 'Admin') {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const id = req.params.id;

    Parcels.destroy({
        where: { parcelNumber: id }
    })
        .then(num => {
            if (num == 1) {
                res.status(204).send();
            } else {
                res.status(400).send({
                    message: `Parcel with id ${id} was not found.`
                });
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while deleting the Parcel."
            });
        });
};