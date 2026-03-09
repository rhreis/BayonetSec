import React from 'react';
import {
  Box,
  Typography,
  Card,
  CardContent,
  Button,
} from '@mui/material';
import { Add as AddIcon } from '@mui/icons-material';

export const AssetsPage: React.FC = () => {
  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={4}>
        <div>
          <Typography variant="h4" component="h1" gutterBottom>
            Assets
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Manage and monitor your security assets
          </Typography>
        </div>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          size="large"
        >
          Add Asset
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
              Asset List
            </Typography>
            <Typography variant="body2" color="text.secondary">
              No assets found. Add your first asset to start monitoring.
            </Typography>
          </CardContent>
        </Card>
      </Box>
    </Box>
  );
};