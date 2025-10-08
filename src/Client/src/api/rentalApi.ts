import axios from "axios";
import type {RentalResult} from "../types/rentalResult.ts";
import type {PickupRequest} from "../types/pickupRequest.ts";
import type {ReturnRequest} from "../types/returnRequest.ts";

const API_BASE = import.meta.env.VITE_API_BASE ?? "/api";

// Endpoints:
// POST /api/rentals/pickup
// POST /api/rentals/return
// GET  /api/rentals
export const registerPickup = async (req: PickupRequest): Promise<RentalResult> => {
    const {data} = await axios.post<RentalResult>(`${API_BASE}/rentals/pickup`, req);
    return data;
};

export const registerReturn = async (req: ReturnRequest): Promise<RentalResult> => {
    const {data} = await axios.post<RentalResult>(`${API_BASE}/rentals/return`, req);
    return data;
};

export const getRentals = async (): Promise<RentalResult[]> => {
    const {data} = await axios.get<RentalResult[]>(`${API_BASE}/rentals`);
    return data;
};
