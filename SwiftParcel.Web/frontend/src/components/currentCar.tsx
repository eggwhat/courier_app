import { Badge, Card } from "flowbite-react";
import React from "react";
import { getCarPersonal } from "../utils/api";

export default function CurrentCar(props: {
  loading: boolean;
  setLoading: (loading: boolean) => void;
}) {
  const [currentCar, setCurrentCar] = React.useState<any>(null);

  React.useEffect(() => {
    getCarPersonal()
      .then((res) => {
        setCurrentCar(res?.data);
      })
      .catch((err) => {
        setCurrentCar(null);
      })
      .finally(() => {
        props.setLoading(false);
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Card className="mb-2">
      <h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
        Your Car
      </h5>
      <div className="flex flex-wrap gap-2">
        {currentCar === null ? (
          <p>You don't have a car yet.</p>
        ) : (
          <>
            <b>Make:</b> {currentCar?.make} <b>Model:</b> {currentCar?.model}
            <Badge color="warning" size="sm">
              {currentCar?.licensePlate}
            </Badge>
          </>
        )}
      </div>
    </Card>
  );
}
