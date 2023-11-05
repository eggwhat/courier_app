import { Fragment, ReactNode } from "react";
import { getUserInfo } from "./storage";
import { Navigate } from "react-router-dom";



export function RolesAuthRoute({
  children,
  role,
}: {
  children: ReactNode;
  role: any;
}) {
  const userInfo = getUserInfo();
  const allowed =
    (role === null && userInfo === null) ||
    (role === "Courier" && userInfo?.courier != null) ||
    userInfo?.user?.role === role
      ? true
      : false;

  if (allowed) {
    return <Fragment>{children}</Fragment>;
  }

  return <Navigate to="/" />;
}
