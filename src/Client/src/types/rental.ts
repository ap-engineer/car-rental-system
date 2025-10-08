export type CarCategory = "SmallCar" | "Combi" | "Truck";

// Safe type for handling unknown values instead of 'any'
export type SafeValue = string | number | boolean | null | undefined | Record<string, unknown> | unknown[];

export type PickupRequest = {
    bookingNumber: string;
    registrationNumber: string;
    customerId: string;
    category: CarCategory;
    pickupDate: string; // ISO string
    pickupKm: number;
};

export type ReturnRequest = {
    bookingNumber: string;
    returnDate: string; // ISO string
    returnKm: number;
};

export type RentalResult = {
    bookingNumber: string;
    price: number;
    days: number;
    km: number;
    isReturned: boolean;
    pickupDate: string | null; // DateTime? → null or ISO string
};
