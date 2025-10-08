import {type FormEvent, useState} from "react";
import {Box, TextField, Button, Stack} from "@mui/material";
import toast from "react-hot-toast";
import type {RentalResult} from "../types/rentalResult.ts";
import {registerReturn} from "../api/rentalApi";
import type {ReturnRequest} from "../types/returnRequest.ts";

type Props = {
    onSuccess: (result: RentalResult) => void;
};

const ReturnForm = ({onSuccess}: Props) => {
    const [form, setForm] = useState({
        bookingNumber: "",
        returnDate: "",
        returnKm: 0,
    });
    const [loading, setLoading] = useState(false);

    const update = (k: string, v: unknown) => setForm((s) => ({...s, [k]: v}));

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
        <Box component="form" onSubmit={submit} sx={{height: '100%', display: 'flex', flexDirection: 'column'}}>
            <Stack spacing={2} sx={{flex: 1, overflow: 'visible', pt: 0.5}}>
                <TextField
                    label="Booking Number"
                    value={form.bookingNumber}
                    onChange={(e) => update("bookingNumber", e.target.value)}
                    required
                    sx={{ mt: 1 }}
                />
                <TextField
                    label="Return Date"
                    type="datetime-local"
                    value={form.returnDate}
                    onChange={(e) => update("returnDate", e.target.value)}
                    InputLabelProps={{shrink: true}}
                    required
                />
                <TextField
                    label="Return Km"
                    type="number"
                    value={form.returnKm}
                    onChange={(e) => update("returnKm", e.target.value)}
                    inputProps={{min: 0}}
                />
                <Button
                    type="submit"
                    variant="contained"
                    disabled={loading}
                    sx={{mt: 'auto'}}
                >
                    {loading ? "Submitting..." : "Submit Return"}
                </Button>
            </Stack>
        </Box>
    );
}

export default ReturnForm;