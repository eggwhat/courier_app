import { HashRouter as Router, Routes, Route } from "react-router-dom";
import { RolesAuthRoute } from "./utils/others";
import React, { Suspense } from "react";
import { Loader } from "./components/loader";
import Parcels from "./pages/parcels/parcels";
import ManageParcels from "./pages/parcels/manage";
import Register from "./pages/register";
import ManageParcelsCourier from "./pages/parcels/courier";
import Home from "./pages/home";
import Deliveries from "./pages/deliveries";
import ManageCouriers from "./pages/couriers/manage";
import ManageCars from "./pages/cars/manage";
import ManageParcelsCar from "./pages/cars/parcels";

export function App() {
  return (
    <Router>
      <Suspense fallback={<Loader />}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route
            path="/register"
            element={
              <RolesAuthRoute role={null}>
                <Register />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/deliveries"
            element={
              <RolesAuthRoute role="Courier">
                <Deliveries />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/parcels"
            element={
              <RolesAuthRoute role="Courier">
                <Parcels />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/couriers/manage"
            element={
              <RolesAuthRoute role="Admin">
                <ManageCouriers />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/parcels/manage"
            element={
              <RolesAuthRoute role="Admin">
                <ManageParcels />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/couriers/:courierId/parcels/manage"
            element={
              <RolesAuthRoute role="Admin">
                <ManageParcelsCourier />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/cars/:carId/parcels/manage"
            element={
              <RolesAuthRoute role="Admin">
                <ManageParcelsCar />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/cars/manage"
            element={
              <RolesAuthRoute role="Admin">
                <ManageCars />
              </RolesAuthRoute>
            }
          />
        </Routes>
      </Suspense>
    </Router>
  );
}
