import { HashRouter as Router, Routes, Route } from "react-router-dom";
import { RolesAuthRoute } from "./utils/others";
import React, { Suspense, useEffect } from "react";
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
import CreateInquiry from "./pages/createInquiry";
import Inquiries from "./pages/inquiries";


export function App() {

  
  return (
    <Router>
      <Suspense fallback={<Loader />}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route
            path="/create-inquiry"
            element={
              <RolesAuthRoute role="user">
                <CreateInquiry/>
              </RolesAuthRoute>
            }
          />
          <Route
            path="/inquiries"
            element={
              <RolesAuthRoute role={null}>
                <Inquiries />
              </RolesAuthRoute>
            }
          />
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
              <RolesAuthRoute role="courier">
                <Deliveries />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/parcels"
            element={
              <RolesAuthRoute role={null}>
                <Parcels />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/couriers/manage"
            element={
              <RolesAuthRoute role="admin">
                <ManageCouriers />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/parcels/manage"
            element={
              <RolesAuthRoute role="admin">
                <ManageParcels />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/couriers/:courierId/parcels/manage"
            element={
              <RolesAuthRoute role="admin">
                <ManageParcelsCourier />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/cars/:carId/parcels/manage"
            element={
              <RolesAuthRoute role="admin">
                <ManageParcelsCar />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/cars/manage"
            element={
              <RolesAuthRoute role="admin">
                <ManageCars />
              </RolesAuthRoute>
            }
          />
        </Routes>
      </Suspense>
    </Router>
  );
}
