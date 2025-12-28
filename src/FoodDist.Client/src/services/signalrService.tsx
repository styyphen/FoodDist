import * as signalR from "@microsoft/signalr";

class SignalRService {
    private connection: signalR.HubConnection;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5000/hubs/parcels")
            .withAutomaticReconnect()
            .build();
    }

    async start() {
        try {
            await this.connection.start();
            console.log("SignalR Connected");
        } catch (err) {
            console.error("SignalR Connection Error:", err);
            setTimeout(() => this.start(), 5000);
        }
    }

    onParcelDelivered(callback: (data: any) => void) {
        this.connection.on("ParcelDelivered", callback);
    }
}

export const signalRService = new SignalRService();
