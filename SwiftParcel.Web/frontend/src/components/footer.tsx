import { Footer as Foot } from "flowbite-react";
import { FaShippingFast } from "react-icons/fa";
import { Link } from "react-router-dom";

export function Footer() {
  return (
    <footer className="mt-3 p-4 bg-white rounded-lg md:px-6 md:py-8 dark:bg-gray-900">
      <div className="sm:flex sm:items-center sm:justify-between">
        <Link to="/" className="flex items-center mb-4 sm:mb-0">
          <FaShippingFast className="mr-2 h-10 w-10 dark:text-white" />
          <span className="self-center text-xl font-semibold whitespace-nowrap text-blue-700">
            Parcel delivery
          </span>
        </Link>
        <ul className="flex flex-wrap items-center mb-6 text-sm text-gray-500 sm:mb-0 dark:text-gray-400">
          <li>
            <Link to="/" className="mr-4 hover:underline md:mr-6 ">
                Home
            </Link>
          </li>
          <li>
            <Link to="/" className="mr-4 hover:underline md:mr-6 ">
                Contacts
            </Link>
          </li>
        </ul>
      </div>
      <hr className="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
      <Foot.Copyright href="/" by="Parcel delivery" year={2022} />
    </footer>
  );
}
