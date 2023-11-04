import {matchPath, useLinkClickHandler, useLocation} from "react-router-dom";
import {Navbar} from "flowbite-react";

export interface AppNavLinkProps {
    to: string;
    text: string;
}

export default function AppNavLink(props: AppNavLinkProps) {
    const { pathname } = useLocation();
    const clickHandler = useLinkClickHandler(props.to);
    
    return <span onClick={clickHandler}>
        <Navbar.Link href={props.to} active={matchPath(pathname, props.to) ? true : false}>
            {props.text}
        </Navbar.Link>
    </span>;
}