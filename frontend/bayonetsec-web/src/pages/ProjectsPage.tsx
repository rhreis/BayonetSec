import React from 'react';
import {
  Box,
  Typography,
  Card,
  CardContent,
  Button,
} from '@mui/material';
import { Add as AddIcon } from '@mui/icons-material';

export const ProjectsPage: React.FC = () => {
  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={4}>
        <div>
          <Typography variant="h4" component="h1" gutterBottom>
            Projects
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Manage your security assessment projects
          </Typography>
        </div>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          size="large"
        >
          New Project
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
              Project List
            </Typography>
            <Typography variant="body2" color="text.secondary">
              No projects found. Create your first project to get started.
            </Typography>
          </CardContent>
        </Card>
      </Box>
    </Box>
  );
};