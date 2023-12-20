import { Fragment, ReactNode } from "react";
import { getUserInfo } from "./storage";
import { Navigate } from "react-router-dom";



export function RolesAuthRoute({children, role,}: {
  children: ReactNode;
  role: string;
}) {
  const userInfo = getUserInfo();
  console.log("UserInfo: ", userInfo)

  console.log("Required role: ", role); // Log the required role
  console.log("User's role: ", userInfo?.role); // Log the user's actual role


  const allowed = role === null ? 
                    !userInfo : // If no role is required, allow only if no user is logged in
                    userInfo?.role === role; // If a role is specified, check if the user has this role

  if (allowed) {
    return <Fragment>{children}</Fragment>;
  }

  return <Navigate to="/" />;
}
