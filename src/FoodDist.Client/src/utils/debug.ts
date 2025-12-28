export function debugDeliveryStatus(status: string) {
    if (status == "delivered") {
        console.log("Delivered parcel");
    }
    return status;
}
