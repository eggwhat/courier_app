import { FaShippingFast } from "react-icons/fa";

export function Loader() {
    return (
        <div className="flex items-center justify-center w-screen h-screen absolute bg-white z-50">
            <div className="flex flex-col items-center justify-center space-y-4">
                <FaShippingFast className="h-12 w-12 text-blue-700 animate-ping" />
            </div>
        </div>
    );
}