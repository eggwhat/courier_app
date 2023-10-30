module.exports = (sequelize, Sequelize) => {
    const Courier = sequelize.define("courier", {
        firstname: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "First name cannot be empty"
                },
                notNull: {
                    msg: 'First name is required'
                }
            }
        },
        lastname: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Last name cannot be empty"
                },
                notNull: {
                    msg: 'Last name is required'
                }
            }
        },
        phone: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Phone number cannot be empty"
                },
                notNull: {
                    msg: 'Phone number is required'
                }
            }
        },
        status: {
            type: Sequelize.ENUM('Available', 'Busy'),
            defaultValue: 'Available',
            validate: {
                isIn: {
                    args: [['Available', 'Busy']],
                    msg: "Status must be either 'Available' or 'Busy'"
                }
            }
        }
    });

    return Courier;
};