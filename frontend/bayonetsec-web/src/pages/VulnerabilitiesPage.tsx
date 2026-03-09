import React from 'react';
import {
  Box,
  Typography,
  Card,
  CardContent,
  Button,
} from '@mui/material';
import { Add as AddIcon } from '@mui/icons-material';

export const VulnerabilitiesPage: React.FC = () => {
  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={4}>
        <div>
          <Typography variant="h4" component="h1" gutterBottom>
            Vulnerabilities
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Track and manage discovered vulnerabilities
          </Typography>
        </div>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          size="large"
        >
          Report Vulnerability
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
              Vulnerability List
            </Typography>
            <Typography variant="body2" color="text.secondary">
              No vulnerabilities found. Start by running security assessments on your assets.
            </Typography>
          </CardContent>
        </Card>
      </Box>
    </Box>
  );
};