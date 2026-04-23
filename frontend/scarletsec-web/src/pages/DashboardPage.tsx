import React from 'react';
import {
  Card,
  CardContent,
  Typography,
  Box,
  Chip,
} from '@mui/material';
import {
  Assessment as AssessmentIcon,
  BugReport as BugIcon,
  Folder as FolderIcon,
  Devices as DevicesIcon,
} from '@mui/icons-material';
import { useAuth } from '../auth/AuthContext';

interface StatCardProps {
  title: string;
  value: string;
  subtitle: string;
  icon: React.ReactNode;
  color: string;
}

const StatCard: React.FC<StatCardProps> = ({ title, value, subtitle, icon, color }) => (
  <Card sx={{ height: '100%' }}>
    <CardContent>
      <Box display="flex" alignItems="center" mb={2}>
        <Box
          sx={{
            backgroundColor: color,
            borderRadius: 2,
            p: 1,
            mr: 2,
            color: 'white',
          }}
        >
          {icon}
        </Box>
        <Box>
          <Typography variant="h4" component="div" fontWeight="bold">
            {value}
          </Typography>
          <Typography variant="h6" color="text.secondary">
            {title}
          </Typography>
        </Box>
      </Box>
      <Typography variant="body2" color="text.secondary">
        {subtitle}
      </Typography>
    </CardContent>
  </Card>
);

export const DashboardPage: React.FC = () => {
  const { user } = useAuth();

  // Placeholder data - in a real app, this would come from API calls
  const stats = [
    {
      title: 'Active Projects',
      value: '12',
      subtitle: 'Projects currently in progress',
      icon: <FolderIcon />,
      color: '#1976d2',
    },
    {
      title: 'Open Vulnerabilities',
      value: '47',
      subtitle: 'Vulnerabilities requiring attention',
      icon: <BugIcon />,
      color: '#d32f2f',
    },
    {
      title: 'Assets Monitored',
      value: '156',
      subtitle: 'Total assets under monitoring',
      icon: <DevicesIcon />,
      color: '#388e3c',
    },
    {
      title: 'Reports Generated',
      value: '23',
      subtitle: 'Reports created this month',
      icon: <AssessmentIcon />,
      color: '#f57c00',
    },
  ];

  return (
    <Box>
      <Box mb={4}>
        <Typography variant="h4" component="h1" gutterBottom>
          Welcome back, {user?.firstName || user?.username}!
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Here's an overview of your security assessment activities.
        </Typography>
      </Box>

      <Box
        sx={{
          display: 'grid',
          gridTemplateColumns: { xs: '1fr', sm: '1fr 1fr', md: '1fr 1fr 1fr 1fr' },
          gap: 3,
          mb: 4,
        }}
      >
        {stats.map((stat, index) => (
          <Box key={index}>
            <StatCard {...stat} />
          </Box>
        ))}
      </Box>

      <Box
        sx={{
          display: 'grid',
          gridTemplateColumns: { xs: '1fr', md: '2fr 1fr' },
          gap: 3,
        }}
      >
        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Recent Activity
            </Typography>
            <Box sx={{ mt: 2 }}>
              <Typography variant="body2" color="text.secondary" paragraph>
                No recent activities to display. Start by creating your first project or adding assets to monitor.
              </Typography>
            </Box>
          </CardContent>
        </Card>

        <Card>
          <CardContent>
            <Typography variant="h6" gutterBottom>
              Quick Actions
            </Typography>
            <Box sx={{ mt: 2, display: 'flex', flexDirection: 'column', gap: 1 }}>
              <Chip
                label="Create New Project"
                variant="outlined"
                clickable
                sx={{ justifyContent: 'flex-start' }}
              />
              <Chip
                label="Add Asset"
                variant="outlined"
                clickable
                sx={{ justifyContent: 'flex-start' }}
              />
              <Chip
                label="Generate Report"
                variant="outlined"
                clickable
                sx={{ justifyContent: 'flex-start' }}
              />
            </Box>
          </CardContent>
        </Card>
      </Box>
    </Box>
  );
};