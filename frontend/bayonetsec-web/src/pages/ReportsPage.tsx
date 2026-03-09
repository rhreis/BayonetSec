import React from 'react';
import {
  Box,
  Typography,
  Card,
  CardContent,
  Button,
} from '@mui/material';
import { Add as AddIcon } from '@mui/icons-material';

export const ReportsPage: React.FC = () => {
  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={4}>
        <div>
          <Typography variant="h4" component="h1" gutterBottom>
            Reports
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Generate and manage security assessment reports
          </Typography>
        </div>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          size="large"
        >
          Generate Report
        </Button>
      </Box>

      <Box
        sx={{
          display: 'grid',
          gridTemplateColumns: '1fr',
          gap: 3,
        }}
      >
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Report List
            </Typography>
            <Typography variant="body2" color="text.secondary">
              No reports found. Generate your first security assessment report.
            </Typography>
          </CardContent>
        </Card>
      </Box>
    </Box>
  );
};