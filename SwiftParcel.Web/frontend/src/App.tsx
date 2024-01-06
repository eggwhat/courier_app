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
import CreateInquiry from "./pages/inquiries/createInquiry";
import Inquiries from "./pages/inquiries/inquiries";
import Offers from "./pages/offers/offers";


export function App() {

  
  return (
    <Router>
      <Suspense fallback={<Loader />}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route
            path="/create-inquiry"
            element={
              <RolesAuthRoute roles={['user', null]}>
                <CreateInquiry/>
              </RolesAuthRoute>
            }
          />
          <Route
            path="/inquiries"
            element={
              <RolesAuthRoute roles={['officeworker', 'courier']}>
                <Inquiries />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/offers"
            element={
              <RolesAuthRoute roles={['user']}>
                <Offers />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/register"
            element={
              <RolesAuthRoute roles={['user', null]}>
                <Register />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/deliveries"
            element={
              <RolesAuthRoute roles={["courier"]}>
                <Deliveries />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/parcels"
            element={
              <RolesAuthRoute roles={['officeworker', 'courier']}>
                <Parcels />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/couriers/manage"
            element={
              <RolesAuthRoute roles={["courier"]}>
                <ManageCouriers />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/parcels/manage"
            element={
              <RolesAuthRoute roles={["courier"]}>
                <ManageParcels />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/couriers/:courierId/parcels/manage"
            element={
              <RolesAuthRoute roles={["courier"]}>
                <ManageParcelsCourier />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/cars/:carId/parcels/manage"
            element={
              <RolesAuthRoute roles={["courier"]}>
                <ManageParcelsCar />
              </RolesAuthRoute>
            }
          />
          <Route
            path="/cars/manage"
            element={
              <RolesAuthRoute roles={["courier"]}>
                <ManageCars />
              </RolesAuthRoute>
            }
          />
        </Routes>
      </Suspense>
    </Router>
  );
}
