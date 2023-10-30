const bcrypt = require("bcrypt");

module.exports = (sequelize, Sequelize) => {
    const User = sequelize.define("user", {
        username: {
            type: Sequelize.STRING,
            unique: true,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Username cannot be empty"
                },
                len: {
                    args: [3, 20],
                    msg: "Username must be between 3 and 20 characters"
                },
                isAlphanumeric: {
                    msg: "Username must contain only letters and numbers"
                },
                notNull: {
                    msg: 'Username is required'
                }
            },
        },
        password: {
            type: Sequelize.STRING,
            allowNull: false
        },
        email: {
            type: Sequelize.STRING,
            unique: true,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Email cannot be empty"
                },
                isEmail: {
                    msg: "Email must be a valid email address"
                },
                notNull: {
                    msg: 'Email is required'
                }
            }
        },
        role: {
            type: Sequelize.ENUM('User', 'Admin'),
            defaultValue: 'User'
        },
        iat: {
            type: Sequelize.INTEGER,
            allowNull: true,
            default: null
        }
    }, {
        defaultScope: {
            attributes: { exclude: ['password'] },
        },
        scopes: {
            withPassword: {
                attributes: {},
            }
        },
        hooks: {
            beforeCreate: (user) => {
                const salt = bcrypt.genSaltSync();
                user.password = bcrypt.hashSync(user.password, salt);
            }
        }
    });

    User.prototype.validPassword = function (password) {
        return bcrypt.compareSync(password, this.password);
    };

    return User;
};