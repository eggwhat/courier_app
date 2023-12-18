import { Badge, Button, Spinner, TextInput } from "flowbite-react";
import React from "react";
import { BsBoxSeam } from "react-icons/bs";
import { HiExclamation } from "react-icons/hi";
import { Footer } from "../components/footer";
import { Header } from "../components/header";
import { Loader } from "../components/loader";
import { getParcel } from "../utils/api";
import { Link } from "react-router-dom";

export default function Home() {
  const [loading, setLoading] = React.useState(true);

  const [trackingNumber, setTrackingNumber] = React.useState("");
  const [error, setError] = React.useState(false);
  const [errorText, setErrorText] = React.useState("");

  const [parcel, setParcel] = React.useState<any>(null);
  const [loadingParcel, setLoadingParcel] = React.useState(false);

  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError(false);
    setLoadingParcel(true);
    if (trackingNumber.startsWith("LT")) {
      getParcel(trackingNumber)
        .then((res) => {
          if (res?.data) {
            setParcel(res?.data);
          } else {
            setParcel(null);
            setError(true);
            setErrorText("Parcel not found");
          }
        })
        .catch((err) => {
          setParcel(null);
          setError(true);
          setErrorText("Parcel not found");
        })
        .finally(() => {
          setLoadingParcel(false);
        });
      return;
    }
    setTimeout(() => {
      setTrackingNumber("");
      setError(true);
      setErrorText("Invalid tracking number");
      setLoadingParcel(false);
    }, 1000);
  };

  return (
    <>
      {loading ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loading} setLoading={setLoading} />

        <div className="flex flex-col items-center justify-center my-20">
          <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
              Post Your Parcel
          </h1>
          <p className="mb-4 text-gray-600 dark:text-gray-400">
              Give your requirements and get many options to choose the best one.
          </p>
          <Link
            to="/inquiry" 
            className="mt-4 w-full md:w-1/2">
            <Button
              gradientDuoTone="cyanToBlue"
              size="xl"
              className="mt-4 w-full"
              type="submit">
              Create an inquiry
            </Button>
          </Link>
        </div>

        <form onSubmit={onSubmit}>
          <div className="flex flex-col items-center justify-center my-20">
            <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
              Track Your Parcel
            </h1>
            <p className="mb-4 text-gray-600 dark:text-gray-400">
              Track your parcel with the parcel tracking number.
            </p>

            <div
              className={`mb-4 flex flex-row gap-2 items-center transition-all duration-1000 ease-in-out ${
                error ? "opacity-100" : "opacity-0"
              }`}
            >
              <div className="inline-flex h-8 w-8 shrink-0 items-center justify-center rounded-lg bg-orange-100 text-orange-500 dark:bg-orange-700 dark:text-orange-200">
                <HiExclamation className="h-5 w-5" />
              </div>
              <div className="ml-3 text-sm font-normal text-gray-500">
                {errorText}
              </div>
            </div>

            <TextInput
              placeholder="Parcel Tracking Number"
              className="w-full md:w-1/2"
              type="text"
              sizing="lg"
              required={true}
              icon={BsBoxSeam}
              value={trackingNumber}
              onChange={(e) => setTrackingNumber(e.target.value)}
            />
            <Button
              gradientDuoTone="cyanToBlue"
              size="xl"
              className="mt-4 w-full md:w-1/2"
              type="submit"
            >
              {loadingParcel ? (
                <>
                  <div className="mr-3">
                    <Spinner size="sm" light={true} />
                  </div>
                  Loading ...
                </>
              ) : (
                "Track Parcel"
              )}
            </Button>
          </div>
        </form>
        {parcel ? (
          <div className="flex flex-col items-center justify-center my-20">
            <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
              Parcel Details
            </h1>
            <p className="mb-4 text-gray-600 dark:text-gray-400">
              Here are the details of your parcel.
            </p>
            <div className="flex flex-col gap-4 w-full md:w-1/2">
              <div className="flex flex-row gap-4 items-center">
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">
                    Tracking Number
                  </p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    {parcel.parcelNumber}
                  </p>
                </div>
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">Status</p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    <span className="flex flex-row justify-center">
                      <Badge
                        size="sm"
                        color={
                          parcel.status === "Pending"
                            ? "warning"
                            : parcel.status === "In progress"
                            ? "pink"
                            : "success"
                        }
                        className="text-center"
                      >
                        {parcel.status}
                      </Badge>
                    </span>
                  </p>
                </div>
              </div>
              <div className="flex flex-row gap-4 items-center">
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">Sender</p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    {parcel.senderName}
                  </p>
                </div>
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">Receiver</p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    {parcel.receiverName}
                  </p>
                </div>
              </div>
              <div className="flex flex-row gap-4 items-center">
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">Weight</p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    {parcel.weight} kg
                  </p>
                </div>
              </div>
              <div className="flex flex-row gap-4 items-center">
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">Started</p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    {new Date(parcel.createdAt).toLocaleString("lt-LT")}
                  </p>
                </div>
                <div className="w-1/2 text-center">
                  <p className="text-gray-600 dark:text-gray-400">
                    Last Update
                  </p>
                  <p className="text-gray-900 dark:text-white font-bold">
                    {new Date(parcel.updatedAt).toLocaleString("lt-LT")}
                  </p>
                </div>
              </div>
            </div>
          </div>
        ) : null}
        <Footer />
      </div>
    </>
  );
}
