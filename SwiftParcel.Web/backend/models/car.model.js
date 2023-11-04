module.exports = (sequelize, Sequelize) => {
    const Car = sequelize.define("car", {
        make: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Car make cannot be empty"
                },
                notNull: {
                    msg: 'Car make is required'
                }
            }
        },
        model: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Car model cannot be empty"
                },
                notNull: {
                    msg: 'Car model is required'
                }
            }
        },
        licensePlate: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "License plate cannot be empty"
                },
                notNull: {
                    msg: 'License plate is required'
                }
            }
        }
    }, { timestamps: false });

    return Car;
};