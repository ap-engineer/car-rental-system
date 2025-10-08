import {type FormEvent, useState} from "react";
import { Box, Paper, TextField, Button, Stack, Typography } from "@mui/material";
import toast from "react-hot-toast";
import type {ReturnRequest, RentalResult} from "../types/rental";
import { registerReturn } from "../api/rentalApi";

type Props = {
    onSuccess: (result: RentalResult) => void;
};

export default function ReturnForm({ onSuccess }: Props) {
    const [form, setForm] = useState({
        bookingNumber: "",
        returnDate: "",
        returnKm: 0,
    });
    const [loading, setLoading] = useState(false);

    const update = (k: string, v: unknown) => setForm((s) => ({ ...s, [k]: v }));

    const submit = async (e: FormEvent) => {
        e.preventDefault();
        if (!form.bookingNumber || !form.returnDate) {
            toast.error("Booking number and return date are required.");
            return;
        }
        try {
            setLoading(true);
            const payload: ReturnRequest = {
                bookingNumber: form.bookingNumber.trim(),
                returnDate: new Date(form.returnDate).toISOString(),
                returnKm: Number(form.returnKm) || 0,
            };
            const res = await registerReturn(payload);
            toast.success(`Return registered for ${res.bookingNumber}`);
            onSuccess(res);
        } catch (err: any) {
            const msg = err?.response?.data?.title || err?.message || "Return failed";
            toast.error(msg);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Paper sx={{ p: 3, backgroundColor: "background.paper" }} component="form" onSubmit={submit}>
            <Typography variant="h6" gutterBottom>Register Return</Typography>
            <Stack spacing={2}>
                <TextField
                    label="Booking Number"
                    value={form.bookingNumber}
                    onChange={(e) => update("bookingNumber", e.target.value)}
                    required
                />
                <TextField
                    label="Return Date"
                    type="datetime-local"
                    value={form.returnDate}
                    onChange={(e) => update("returnDate", e.target.value)}
                    InputLabelProps={{ shrink: true }}
                    required
                />
                <TextField
                    label="Return Km"
                    type="number"
                    value={form.returnKm}
                    onChange={(e) => update("returnKm", e.target.value)}
                    inputProps={{ min: 0 }}
                />
                <Box>
                    <Button type="submit" variant="contained" disabled={loading}>
                        {loading ? "Submitting..." : "Submit Return"}
                    </Button>
                </Box>
            </Stack>
        </Paper>
    );
}
