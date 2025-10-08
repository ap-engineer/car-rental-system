import type {CarCategory} from "./rentalResult.ts";

export type PickupRequest = {
    bookingNumber: string;
    registrationNumber: string;
    customerId: string;
    category: CarCategory;
    pickupDate: string;
    pickupKm: number;
};