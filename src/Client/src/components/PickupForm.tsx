import { useState } from "react";
import { Box, TextField, MenuItem, Button, Stack } from "@mui/material";
import toast from "react-hot-toast";
import type {CarCategory, PickupRequest, RentalResult, SafeValue} from "../types/rental";
import { registerPickup } from "../api/rentalApi";

type Props = {
    onSuccess: (result: RentalResult) => void;
};

const categories: CarCategory[] = ["SmallCar", "Combi", "Truck"];

export default function PickupForm({ onSuccess }: Props) {
    const [form, setForm] = useState({
        bookingNumber: "",
        registrationNumber: "",
        customerId: "",
        category: "SmallCar" as CarCategory,
        pickupDate: "",
        pickupKm: 0,
    });
    const [loading, setLoading] = useState(false);

    const update = (k: string, v: SafeValue) => setForm((s) => ({ ...s, [k]: v }));

    const submit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!form.bookingNumber || !form.registrationNumber || !form.customerId || !form.pickupDate) {
            toast.error("All required fields must be filled.");
            return;
        }
        try {
            setLoading(true);
            const payload: PickupRequest = {
                bookingNumber: form.bookingNumber.trim(),
                registrationNumber: form.registrationNumber.trim(),
                customerId: form.customerId.trim(),
                category: form.category,
                pickupDate: new Date(form.pickupDate).toISOString(),
                pickupKm: Number(form.pickupKm) || 0,
            };
            const res = await registerPickup(payload);
            toast.success(`Pickup registered for ${res.bookingNumber}`);
            onSuccess(res);
        } catch (err: any) {
            const msg = err?.response?.data?.title || err?.message || "Pickup failed";
            toast.error(msg);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box component="form" onSubmit={submit} sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
            <Stack spacing={1.5} sx={{ flex: 1, overflow: 'auto' }}>
                <TextField
                    label="Booking Number"
                    value={form.bookingNumber}
                    onChange={(e) => update("bookingNumber", e.target.value)}
                    required
                />
                <TextField
                    label="Registration Number"
                    value={form.registrationNumber}
                    onChange={(e) => update("registrationNumber", e.target.value)}
                    required
                />
                <TextField
                    label="Customer ID"
                    value={form.customerId}
                    onChange={(e) => update("customerId", e.target.value)}
                    required
                />
                <TextField
                    select
                    label="Category"
                    value={form.category}
                    onChange={(e) => update("category", e.target.value as CarCategory)}
                >
                    {categories.map((c) => (
                        <MenuItem key={c} value={c}>{c}</MenuItem>
                    ))}
                </TextField>
                <TextField
                    label="Pickup Date"
                    type="datetime-local"
                    value={form.pickupDate}
                    onChange={(e) => update("pickupDate", e.target.value)}
                    InputLabelProps={{ shrink: true }}
                    required
                />
                <TextField
                    label="Pickup Km"
                    type="number"
                    value={form.pickupKm}
                    onChange={(e) => update("pickupKm", e.target.value)}
                    inputProps={{ min: 0 }}
                />
                <Button 
                    type="submit" 
                    variant="contained" 
                    disabled={loading}
                    sx={{ mt: 'auto' }}
                >
                    {loading ? "Submitting..." : "Submit Pickup"}
                </Button>
            </Stack>
        </Box>
    );
}
