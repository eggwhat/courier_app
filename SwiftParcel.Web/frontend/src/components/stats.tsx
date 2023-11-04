import { Card } from "flowbite-react";

export default function Stats(props: { parcels: any }) {
  return (
    <Card className="mb-2">
      <h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
        Statistics
      </h5>
      <div className="flex flex-wrap gap-2">
        <b>Parcels:</b> {props.parcels?.total_results || "0"}
      </div>
    </Card>
  );
}
