const { v4: uuidv4 } = require('uuid');

module.exports = (sequelize, Sequelize) => {
    const Parcel = sequelize.define("parcel", {
        parcelNumber: {
            type: Sequelize.STRING,
            allowNull: false,
            unique: true,
            primaryKey: true,
            defaultValue: function() {
                return "LT" + uuidv4().toString().replace(/-/gi, '').toUpperCase().slice(0, 13);
            }
        },
        senderName: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Sender name cannot be empty"
                },
                notNull: {
                    msg: 'Sender name is required'
                }
            }
        },
        senderAddress: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Sender address cannot be empty"
                },
                notNull: {
                    msg: 'Sender address is required'
                }
            }
        },
        senderPhone: {
            type: Sequelize.STRING,
            allowNull: true,
            validate: {
                notEmpty: {
                    msg: "Sender phone number cannot be empty"
                },
            }
        },
        receiverName: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Receiver name cannot be empty"
                },
                notNull: {
                    msg: 'Receiver name is required'
                }
            }
        },
        receiverAddress: {
            type: Sequelize.STRING,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: "Receiver address cannot be empty"
                },
                notNull: {
                    msg: 'Receiver address is required'
                }
            }
        },
        receiverPhone: {
            type: Sequelize.STRING,
            allowNull: true,
            validate: {
                notEmpty: {
                    msg: "Receiver phone number cannot be empty"
                }
            }
        },
        weight: {
            type: Sequelize.DOUBLE,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: 'Parcel weight cannot be empty'
                },
                isNumeric: {
                    msg: 'Parcel weight must be a number'
                },
                min: {
                    args: 0.1,
                    msg: 'Parcel weight must be greater than 0kg'
                },
                max: {
                    args: 100,
                    msg: 'Parcel weight must be less than 100kg'
                },
                notNull: {
                    msg: 'Parcel weight is required'
                }
            }
        },
        price: {
            type: Sequelize.DOUBLE,
            allowNull: false,
            validate: {
                notEmpty: {
                    msg: 'Parcel price cannot be empty'
                },
                isNumeric: {
                    msg: 'Parcel price must be a number'
                },
                min: {
                    args: 0.1,
                    msg: 'Parcel price must be greater than 0'
                },
                max: {
                    args: 100000,
                    msg: 'Parcel price must be less than 100000'
                },
                notNull: {
                    msg: 'Parcel price is required'
                }
            }
        },
        status: {
            type: Sequelize.ENUM('Pending', 'In progress', 'Delivered'),
            defaultValue: 'Pending',
            validate: {
                isIn: {
                    args: [['Pending', 'In progress', 'Delivered']],
                    msg: "Status must be either 'Pending', 'In progress' or 'Delivered'"
                }
            }
        }
    });

    return Parcel;
};