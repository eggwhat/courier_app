require('dotenv').config();

const express = require("express");
const cors = require("cors");

const app = express();

var corsOptions = {
  origin: "https://nojusgat.github.io" // frontend url
};

app.use(cors(corsOptions));
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

const db = require("./models");

db.sequelize.sync()
  .then(() => {
    console.log("Synced db.");
  })
  .catch((err) => {
    console.log("Failed to sync db: " + err.message);
  });

app.get("/", (req, res) => {
  res.json({ message: "Welcome to the application." });
});

require("./routes/parcel.routes")(app);
require("./routes/courier.routes")(app);
require("./routes/car.routes")(app);
require("./routes/user.routes")(app);


const port = process.env.PORT || 8080;
app.listen(port, () => {
  console.log(`Backend server is running on port ${process.env.port}.`);
});