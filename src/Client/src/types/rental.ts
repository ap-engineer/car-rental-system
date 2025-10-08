export type CarCategory = "SmallCar" | "Combi" | "Truck";

export interface PickupRequest {
    bookingNumber: string;
    registrationNumber: string;
    customerId: string;
    category: CarCategory;
    pickupDate: string; // ISO string
    pickupKm: number;
}

export interface ReturnRequest {
    bookingNumber: string;
    returnDate: string; // ISO string
    returnKm: number;
}

export interface RentalResult {
    bookingNumber: string;
    price: number;
    days: number;
    km: number;
    isReturned: boolean;
    pickupDate: string | null; // DateTime? → null or ISO string
}
