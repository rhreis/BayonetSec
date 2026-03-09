import { Severity, VulnerabilityStatus, ProjectStatus, ReportStatus } from '../types';

export const formatDate = (date: Date | string): string => {
  const d = new Date(date);
  return d.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
  });
};

export const formatDateTime = (date: Date | string): string => {
  const d = new Date(date);
  return d.toLocaleString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });
};

export const getSeverityColor = (severity: Severity): string => {
  switch (severity) {
    case Severity.Critical:
      return '#d32f2f';
    case Severity.High:
      return '#f57c00';
    case Severity.Medium:
      return '#fbc02d';
    case Severity.Low:
      return '#388e3c';
    case Severity.Info:
      return '#1976d2';
    default:
      return '#757575';
  }
};

export const getStatusColor = (status: VulnerabilityStatus | ProjectStatus | ReportStatus): string => {
  switch (status) {
    case VulnerabilityStatus.Open:
    case ProjectStatus.Active:
      return '#388e3c';
    case VulnerabilityStatus.InProgress:
    case ProjectStatus.Planning:
    case ReportStatus.Generating:
      return '#f57c00';
    case VulnerabilityStatus.Fixed:
    case ProjectStatus.Completed:
    case ReportStatus.Completed:
      return '#1976d2';
    case VulnerabilityStatus.Accepted:
    case ProjectStatus.OnHold:
      return '#fbc02d';
    case VulnerabilityStatus.FalsePositive:
    case ProjectStatus.Cancelled:
    case ReportStatus.Failed:
      return '#d32f2f';
    default:
      return '#757575';
  }
};

export const truncateText = (text: string, maxLength: number): string => {
  if (text.length <= maxLength) return text;
  return text.substring(0, maxLength) + '...';
};

export const generateId = (): string => {
  return Math.random().toString(36).substring(2) + Date.now().toString(36);
};

export const sleep = (ms: number): Promise<void> => {
  return new Promise(resolve => setTimeout(resolve, ms));
};