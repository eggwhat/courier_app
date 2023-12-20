import { Button, Dropdown, Navbar } from "flowbite-react";
import { Link, useNavigate } from "react-router-dom";

import { FiLogIn } from "react-icons/fi";
import { FaShippingFast } from "react-icons/fa";
import { CgProfile } from "react-icons/cg";
import { getUserInfo, saveUserInfo } from "../utils/storage";
import { LoginModal } from "./modals/loginModal";
import React, { useState } from "react";
import { getProfile, logout } from "../utils/api";
import AppNavLink from "./appNavLink";

export function Header(props: {
  loading: boolean;
  setLoading: (loading: boolean) => void;
}) {
  const [showLoginModal, setShowLoginModal] = React.useState(false);

  const navigate = useNavigate();

  const [userInfo, setUserInfo] = React.useState<any>(null);
  const [userEmail, setUserEmail] = useState('');
  const [isCourier, setIsCourier] = useState(false);
  
  const [userToken, setUserToken] = React.useState<any>(false);

  React.useEffect(() => {
    setUserToken(getUserInfo());
  }, [showLoginModal]);

  React.useEffect(() => {
    console.log('User token updated:', userToken);
  }, [userToken]);

  React.useMemo(() => {
    if (userToken) {
      getProfile()
        .then((res) => {
          console.log("res", res);
          setUserEmail(res.email);
          setIsCourier(res.role === "courier");

          if (res?.status === 200) {
           
            setIsCourier(res.role === "courier");

            const newUserInfo = { ...getUserInfo(), courier: res.courier };
            saveUserInfo(newUserInfo);
          

            if (res.courier) {
              setIsCourier(true);
            }
          } else {
            throw new Error();
          }
        })
        .catch(() => {
          // saveUserInfo(null);
          // setUserToken(null);
          // setIsCourier(false);
        })
        .finally(() => {
          props.setLoading(false);
        });
    } else if (userToken === null) {
      props.setLoading(false);
      setIsCourier(false);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userToken]);

  const logoutUser = async () => {
    logout();
    setUserToken(null);
    localStorage.removeItem("parcelId");
    navigate("/");
  };

  return (
    <>
      <Navbar fluid={false} rounded={true} className="mb-5">
        <Link to="/" className="flex items-center">
          <FaShippingFast className="mr-2 h-10 w-10 text-blue-700" />
          <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">
            Swift Parcel
          </span>
        </Link>
        <div className="flex md:order-2">
          {userToken ? (
            <Dropdown
              arrowIcon={false}
              inline={true}
              label={<CgProfile className="text-blue-700 w-8 h-8 mr-2" />}
            >
              <Dropdown.Header>
                <span className="block text-sm">
                  {userToken?.user?.username}
                </span>
                <span className="block truncate text-sm font-medium">
                  {userEmail || 'Email not available'}
                </span>
              </Dropdown.Header>
              <Dropdown.Item onClick={logoutUser}>Sign out</Dropdown.Item>
            </Dropdown>
          ) : (
            <Button className="mr-2" onClick={() => setShowLoginModal(true)}>
              <FiLogIn className="sm:mr-2 h-4 w-4" />
              <span className="hidden sm:flex">Sign in</span>
            </Button>
          )}
          {userToken?.user?.role === "admin" || userToken?.courier !== null ? (
            <Navbar.Toggle />
          ) : null}
        </div>
        <Navbar.Collapse>
          <AppNavLink to="/" text="Track Parcel" />
          {userToken?.user?.role === "User" ? <></> : null}
          {isCourier && (
            <>
              <AppNavLink to="/inquiries" text="Inquiries" />
              <AppNavLink to="/offers" text="Offer Requests" />
              <AppNavLink to="/sent-data" text="Sent Data" />
              <AppNavLink to="/pending-offers" text="Pending Offers" />
            </>
          )}
          {userToken?.user?.role === "admin" ? (
            <>
              <AppNavLink to="/couriers/manage" text="Couriers" />
              <AppNavLink to="/parcels/manage" text="Parcels" />
              <AppNavLink to="/cars/manage" text="Cars" />
            </>
          ) : null}
        </Navbar.Collapse>
      </Navbar>
      <LoginModal show={showLoginModal} setShow={setShowLoginModal} />
    </>
  );
}
