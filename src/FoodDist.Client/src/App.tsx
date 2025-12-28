import { useEffect, useState } from "react";
import { signalRService } from "./services/signalrService";

function App() {
    const [updates, setUpdates] = useState<string[]>([]);

    useEffect(() => {
        signalRService.start();
        signalRService.onParcelDelivered((msg) => {
            setUpdates((prev) => [...prev, `Parcel ${msg.Id} delivered: ${msg.Status}`]);
        });
    }, []);

    return (
        <div style={{ padding: "2rem" }}>
            <h1>📦 FoodDist Parcel Tracker</h1>
            <ul>
                {updates.map((u, i) => (
                    <li key={i}>{u}</li>
                ))}
            </ul>
        </div>
    );
}

export default App;
