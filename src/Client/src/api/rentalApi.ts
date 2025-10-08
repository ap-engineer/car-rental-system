import axios from "axios";
import type {PickupRequest, ReturnRequest, RentalResult} from "../types/rental";

const API_BASE = import.meta.env.VITE_API_BASE ?? "/api";

// Endpoints:
// POST /api/rentals/pickup
// POST /api/rentals/return
// GET  /api/rentals
export async function registerPickup(req: PickupRequest): Promise<RentalResult> {
    const { data } = await axios.post<RentalResult>(`${API_BASE}/rentals/pickup`, req);
    return data;
}

export async function registerReturn(req: ReturnRequest): Promise<RentalResult> {
    const { data } = await axios.post<RentalResult>(`${API_BASE}/rentals/return`, req);
    return data;
}

export async function getRentals(): Promise<RentalResult[]> {
    const { data } = await axios.get<RentalResult[]>(`${API_BASE}/rentals`);
    return data;
}
