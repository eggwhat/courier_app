const { getPagination, getPagingData } = require('./shared');
const db = require("../models");
const auth = require("../middlewares/auth.middleware");
const Users = db.users;
const Couriers = db.couriers;

const { Op } = require("sequelize");

exports.register = (req, res) => {
    const user = {
        username: req.body.username,
        password: req.body.password,
        email: req.body.email
    };

    Users.create(user)
        .then(user => {
            user.dataValues.password = undefined;
            user.dataValues.iat = undefined;
            const token = auth.generateToken(user);
            res.status(201).send({ user, token });
        })
        .catch(err => {
            res.status(400).send({
                message: err.message || "Some error occurred while creating the User."
            });
        });
};

exports.login = (req, res) => {
    const { username, password } = req.body;
    Users.scope("withPassword").findOne({ where: { username }, attributes: { exclude: ['iat'] } })
        .then(user => {
            if (user == null) {
                res.status(400).send({
                    message: `Failed to login.`
                });
            } else {
                if (user.validPassword(password)) {
                    user.dataValues.password = undefined;
                    const token = auth.generateToken(user);
                    res.status(201).send({ user, token });
                } else {
                    res.status(400).send({
                        message: "Failed to login."
                    });
                }
            }
        })
        .catch(err => {
            res.status(500).send({
                message: err.message || "Some error occurred while retrieving users."
            });
        });
};

exports.logout = (req, res) => {
    Users.update({ iat: null }, { where: { id: req.user.id } }).then(() => {
        res.send({
            message: "User logged out successfully."
        });
    }).catch(err => {
        res.status(500).send({
            message: err.message || "Some error occurred while logging out."
        });
    });
};

exports.profile = async (req, res) => {
    const courier = await Couriers.findOne({ where: { userId: req.user.id } });
    res.send({ ...req.user.dataValues, courier });
};

exports.findAll = (req, res) => {
    if (req.user.role != "Admin") {
        res.status(403).send({
            message: "Page access is restricted."
        });
        return;
    }
    const { page, size, unassigned } = req.query;
    if (page != null && isNaN(page) || size != null && isNaN(size)) {
        res.status(400).send({
            message: "Page and size must be numbers."
        });
        return;
    }

    let where = {};
    if(unassigned != null && unassigned == 'true') {
        where.id = { [Op.notIn]: db.Sequelize.literal("(SELECT userId FROM couriers WHERE userId IS NOT NULL)") };
    }

    const { limit, offset } = getPagination(page, size);
    Users.findAndCountAll({
        limit,
        offset,
        where,
        attributes: { exclude: ['password', 'iat'] },
    })
        .then(data => {
            if (data.count == 0) {
                res.status(204).send({
                    message: "Users were not found."
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
                message: err.message || "Some error occurred while retrieving users."
            });
        });
};