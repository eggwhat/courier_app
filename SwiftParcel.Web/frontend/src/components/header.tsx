import { Button, Dropdown, Navbar } from "flowbite-react";
import { Link, useNavigate } from "react-router-dom";

import { FiLogIn } from "react-icons/fi";
import { FaShippingFast } from "react-icons/fa";
import { CgProfile } from "react-icons/cg";
import { getUserInfo, saveUserInfo } from "../utils/storage";
import { LoginModal } from "./modals/loginModal";
import React from "react";
import { getProfile, logout } from "../utils/api";
import AppNavLink from "./appNavLink";

export function Header(props: {
  loading: boolean;
  setLoading: (loading: boolean) => void;
}) {
  const [showLoginModal, setShowLoginModal] = React.useState(false);

  const navigate = useNavigate();

  const [userToken, setUserToken] = React.useState<any>(false);
  const [isCourier, setIsCourier] = React.useState<any>(false);

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
          if (res?.status === 200) {
            const newUserInfo = { ...getUserInfo(), courier: res.data.courier };
            saveUserInfo(newUserInfo);
            if (res.data.courier) {
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
            Parcel delivery
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
                  {userToken?.user?.email}
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
          {isCourier ? (
            <>
              <AppNavLink to="/deliveries" text="Deliveries" />
              <AppNavLink to="/parcels" text="All Parcels" />
            </>
          ) : null}
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
