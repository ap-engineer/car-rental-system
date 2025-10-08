export type RentalResult = {
    bookingNumber: string;
    price: number;
    days: number;
    km: number;
    isReturned: boolean;
    pickupDate: string | null; // DateTime? → null or ISO string
};
