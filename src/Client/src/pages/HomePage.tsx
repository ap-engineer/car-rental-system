import {useState} from 'react';
import {Container, Grid, Paper, Typography, Box} from '@mui/material';
import {Toaster} from 'react-hot-toast';
import PickupForm from '../components/PickupForm';
import ReturnForm from '../components/ReturnForm';
import RentalsTable from '../components/RentalsTable';

const FormSection = ({children, title}: { children: React.ReactNode; title: string }) => (
    <Paper
        elevation={2}
        sx={{
            p: 3,
            height: '100%',
            display: 'flex',
            flexDirection: 'column'
        }}
    >
        <Typography variant="h6" gutterBottom fontWeight="medium">
            {title}
        </Typography>
        <Box sx={{mt: 2, flexGrow: 1, display: 'flex', flexDirection: 'column'}}>
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
        <Container maxWidth="xl" sx={{py: 4}}>
            <Toaster position="top-right"/>

            <Box mb={4}>
                <Typography variant="h4" component="h1" gutterBottom>
                    Car Rental System
                </Typography>
                <Typography color="textSecondary">
                    Manage vehicle pickups and returns
                </Typography>
            </Box>

            <Grid container spacing={3}>
                <Grid>
                    <FormSection title="Pickup Vehicle">
                        <PickupForm onSuccess={handleSuccess}/>
                    </FormSection>
                </Grid>
                
                <Grid >
                    <FormSection title="Return Vehicle">
                        <ReturnForm onSuccess={handleSuccess}/>
                    </FormSection>
                </Grid>

                <Grid>
                    <RentalsTable refreshTrigger={refreshKey}/>
                </Grid>
            </Grid>
        </Container>
    );
}
