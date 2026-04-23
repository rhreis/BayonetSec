// Base types
export interface BaseEntity {
  id: string;
  createdAt: Date;
  updatedAt: Date;
}

// User and Authentication types
export interface User {
  id: string;
  username: string;
  email: string;
  firstName?: string;
  lastName?: string;
  role: UserRole;
  tenantId: string;
  isActive: boolean;
  lastLoginAt?: Date;
}

export enum UserRole {
  Admin = 'Admin',
  Tester = 'Tester',
  Client = 'Client'
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  user: User;
  token: string;
  refreshToken: string;
  expiresAt: Date;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  refreshToken: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
}

// Tenant types
export interface Tenant extends BaseEntity {
  name: string;
  domain?: string;
  isActive: boolean;
}

// Project types
export interface Project extends BaseEntity {
  name: string;
  description?: string;
  status: ProjectStatus;
  tenantId: string;
  startDate?: Date;
  endDate?: Date;
  ownerId: string;
}

export enum ProjectStatus {
  Planning = 'Planning',
  Active = 'Active',
  OnHold = 'OnHold',
  Completed = 'Completed',
  Cancelled = 'Cancelled'
}

// Asset types
export interface Asset extends BaseEntity {
  name: string;
  type: AssetType;
  description?: string;
  ipAddress?: string;
  hostname?: string;
  projectId: string;
  tenantId: string;
  tags: string[];
  metadata: Record<string, unknown>;
}

export enum AssetType {
  Server = 'Server',
  Workstation = 'Workstation',
  NetworkDevice = 'NetworkDevice',
  WebApplication = 'WebApplication',
  MobileApplication = 'MobileApplication',
  Database = 'Database',
  Other = 'Other'
}

// Vulnerability types
export interface Vulnerability extends BaseEntity {
  title: string;
  description: string;
  severity: Severity;
  status: VulnerabilityStatus;
  cvssScore?: number;
  cveId?: string;
  assetId: string;
  projectId: string;
  tenantId: string;
  discoveredAt: Date;
  reportedById: string;
  assignedToId?: string;
  remediationPlan?: string;
  tags: string[];
}

export enum Severity {
  Critical = 'Critical',
  High = 'High',
  Medium = 'Medium',
  Low = 'Low',
  Info = 'Info'
}

export enum VulnerabilityStatus {
  Open = 'Open',
  InProgress = 'InProgress',
  Fixed = 'Fixed',
  Accepted = 'Accepted',
  FalsePositive = 'FalsePositive'
}

// Report types
export interface Report extends BaseEntity {
  title: string;
  description?: string;
  type: ReportType;
  status: ReportStatus;
  projectId: string;
  tenantId: string;
  generatedById: string;
  generatedAt: Date;
  fileUrl?: string;
  parameters: Record<string, unknown>;
}

export enum ReportType {
  VulnerabilityAssessment = 'VulnerabilityAssessment',
  PenetrationTest = 'PenetrationTest',
  Compliance = 'Compliance',
  ExecutiveSummary = 'ExecutiveSummary'
}

export enum ReportStatus {
  Generating = 'Generating',
  Completed = 'Completed',
  Failed = 'Failed'
}

// API Response types
export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message?: string;
  errors?: string[];
}

export interface PaginatedResponse<T> {
  data: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

// Form types
export interface FormFieldError {
  field: string;
  message: string;
}

export interface FormState<T> {
  data: T;
  errors: FormFieldError[];
  isSubmitting: boolean;
  isValid: boolean;
}

// Navigation types
export interface NavItem {
  id: string;
  label: string;
  path: string;
  icon?: string;
  children?: NavItem[];
  requiredRole?: UserRole;
}

// Theme types
export interface ThemeConfig {
  mode: 'light' | 'dark';
  primaryColor: string;
  secondaryColor: string;
}