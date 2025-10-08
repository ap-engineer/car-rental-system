import {type ReactNode, useState} from 'react';
import {Container, Grid, Paper, Typography, Box} from '@mui/material';
import {Toaster} from 'react-hot-toast';
import PickupForm from '../components/PickupForm';
import ReturnForm from '../components/ReturnForm';
import RentalsTable from '../components/RentalsTable';

const FormSection = ({children, title}: { children: ReactNode; title: string }) => (
    <Paper
        elevation={2}
        sx={{
            p: 2,
            height: '70vh',
            display: 'flex',
            flexDirection: 'column',
            overflow: 'hidden'
        }}
    >
        <Typography variant="h6" gutterBottom fontWeight="medium" sx={{ mb: 1 }}>
            {title}
        </Typography>
        <Box sx={{flexGrow: 1, display: 'flex', flexDirection: 'column', overflow: 'hidden'}}>
            {children}
        </Box>
    </Paper>
);

export default function HomePage() {
    const [refreshKey, setRefreshKey] = useState(0);

    const handleSuccess = () => {
        // Increment refreshKey to trigger table refresh
        setRefreshKey(prev => prev + 1);
    };

    return (
        <Box sx={{ height: '100vh', overflow: 'hidden', display: 'flex', flexDirection: 'column' }}>
            <Container maxWidth="xl" sx={{ py: 2, flex: 1, display: 'flex', flexDirection: 'column', overflow: 'hidden' }}>
                <Toaster position="top-right"/>

                <Box mb={2}>
                    <Typography variant="h4" component="h1" gutterBottom sx={{ mb: 1 }}>
                        Car Rental System
                    </Typography>
                    <Typography color="textSecondary" variant="body2">
                        Manage vehicle pickups and returns
                    </Typography>
                </Box>

                <Grid container spacing={2} sx={{ flex: 1, overflow: 'hidden' }}>
                    <Grid size={{ xs: 12, md: 3 }}>
                        <FormSection title="Pickup Vehicle">
                            <PickupForm onSuccess={handleSuccess}/>
                        </FormSection>
                    </Grid>
                    
                    <Grid size={{ xs: 12, md: 3 }}>
                        <FormSection title="Return Vehicle">
                            <ReturnForm onSuccess={handleSuccess}/>
                        </FormSection>
                    </Grid>

                    <Grid size={{ xs: 12, md: 6 }}>
                        <Paper
                            elevation={2}
                            sx={{
                                height: '70vh',
                                display: 'flex',
                                flexDirection: 'column',
                                overflow: 'hidden'
                            }}
                        >
                            <RentalsTable refreshTrigger={refreshKey}/>
                        </Paper>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
}
