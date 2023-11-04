const jwt = require('jsonwebtoken');
const db = require("../models");
const Users = db.users;

exports.generateToken = (user) => {
    const token = jwt.sign({
        id: user.id,
        email: user.email,
        role: user.role
    }, process.env.jwt_secret, {
        expiresIn: process.env.jwt_expiration
    });

    const decoded = jwt.verify(token, process.env.jwt_secret);

    Users.update({
        iat: decoded.iat
    }, {
        where: {
            id: user.id
        }
    });

    return token;
};

exports.authorization = async (req, res, next) => {
    try {
        const token = req.headers.authorization.split(" ")[1];
        const decoded = jwt.verify(token, process.env.jwt_secret);

        const user = await Users.findByPk(decoded.id);
        if (!user || user.iat != decoded.iat)
            throw new Error();

        user.dataValues.iat = undefined;

        req.user = user;
        next();
    } catch (error) {
        return res.status(401).json({
            message: "Authorization failed."
        });
    }
};

exports.authorization_check = async (req, res, next) => {
    try {
        const token = req.headers.authorization.split(" ")[1];
        const decoded = jwt.verify(token, process.env.jwt_secret);

        const user = await Users.findByPk(decoded.id);
        if (!user || user.iat != decoded.iat)
            throw new Error();

        user.dataValues.iat = undefined;

        req.user = user;
        next();
    } catch (error) {
        req.user = null;
        next();
    }
};