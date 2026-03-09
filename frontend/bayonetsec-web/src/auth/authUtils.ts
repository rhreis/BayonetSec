import { UserRole } from '../types';

export const hasRequiredRole = (userRole: UserRole, requiredRole: UserRole): boolean => {
  const roleHierarchy: Record<UserRole, number> = {
    [UserRole.Client]: 1,
    [UserRole.Tester]: 2,
    [UserRole.Admin]: 3,
  };

  return roleHierarchy[userRole] >= roleHierarchy[requiredRole];
};