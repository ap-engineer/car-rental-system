import {useEffect, useState} from "react";
import {
    TableContainer,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    Chip,
    Typography,
    CircularProgress,
    Box
} from "@mui/material";
import {getRentals} from "../api/rentalApi";
import type {RentalResult} from "../types/rental";
import {toast} from "react-hot-toast";

interface RentalsTableProps {
    refreshTrigger?: number; // Can be used to trigger a refresh from parent
}

export default function RentalsTable({refreshTrigger = 0}: RentalsTableProps) {
    const [rows, setRows] = useState<RentalResult[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    const loadRentals = async () => {
        try {
            setIsLoading(true);
            setError(null);
            const data = await getRentals();
            setRows(data);
        } catch (err) {
            const errorMessage = err instanceof Error ? err.message : 'Failed to load rentals';
            setError(errorMessage);
            toast.error(errorMessage);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        loadRentals();
    }, [refreshTrigger]);

    const formatDate = (dateString: string | null): string => {
        if (!dateString) return 'N/A';
        try {
            return new Date(dateString).toLocaleString();
        } catch {
            return 'Invalid date';
        }
    };

    if (isLoading) {
        return (
            <Box sx={{height: '100%', display: 'flex', flexDirection: 'column', overflow: 'hidden'}}>
                <Typography variant="h6" sx={{p: 2, fontWeight: 'medium', flexShrink: 0}}>
                    Registered Rentals
                </Typography>
                <Box sx={{flex: 1, display: "flex", justifyContent: "center", alignItems: "center"}}>
                    <CircularProgress/>
                </Box>
            </Box>
        );
    }

    if (error) {
        return (
            <Box sx={{height: '100%', display: 'flex', flexDirection: 'column', overflow: 'hidden'}}>
                <Typography variant="h6" sx={{p: 2, fontWeight: 'medium', flexShrink: 0}}>
                    Registered Rentals
                </Typography>
                <Box sx={{flex: 1, display: "flex", justifyContent: "center", alignItems: "center", p: 2}}>
                    <Typography color="error">{error}</Typography>
                </Box>
            </Box>
        );
    }

    return (
        <Box sx={{height: '100%', display: 'flex', flexDirection: 'column', overflow: 'hidden'}}>
            <Typography variant="h6" sx={{p: 2, fontWeight: 'medium', flexShrink: 0}}>
                Registered Rentals
            </Typography>
            <TableContainer
                sx={{
                    flex: 1,
                    overflow: 'auto'
                }}
            >
                <Table size="small" aria-label="rentals table" stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell>Booking #</TableCell>
                            <TableCell align="right">Price</TableCell>
                            <TableCell align="right">Days</TableCell>
                            <TableCell align="right">Km</TableCell>
                            <TableCell align="center">Status</TableCell>
                            <TableCell>Pickup Date</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.length > 0 ? (
                            rows.map((rental) => (
                                <TableRow
                                    key={rental.bookingNumber}
                                    hover
                                    sx={{'&:last-child td, &:last-child th': {border: 0}}}
                                >
                                    <TableCell component="th" scope="row">
                                        {rental.bookingNumber}
                                    </TableCell>
                                    <TableCell align="right">
                                        {rental.price}
                                    </TableCell>
                                    <TableCell align="right">{rental.days}</TableCell>
                                    <TableCell align="right">
                                        {rental.km.toLocaleString('sv-SE')} km
                                    </TableCell>
                                    <TableCell align="center">
                                        <Chip
                                            size="small"
                                            label={rental.isReturned ? 'Returned' : 'Active'}
                                            color={rental.isReturned ? 'success' : 'warning'}
                                            variant={rental.isReturned ? 'filled' : 'outlined'}
                                        />
                                    </TableCell>
                                    <TableCell>
                                        {formatDate(rental.pickupDate)}
                                    </TableCell>
                                </TableRow>
                            ))
                        ) : (
                            <TableRow>
                                <TableCell colSpan={6} align="center" sx={{py: 3}}>
                                    <Typography color="textSecondary">
                                        No rental records found
                                    </Typography>
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
}
