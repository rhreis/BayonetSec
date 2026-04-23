import { LoginRequest, LoginResponse, User } from '../types';
import { httpClient } from '../api/httpClient';

class AuthService {
  private readonly baseUrl = '/api/auth';

  async login(credentials: LoginRequest): Promise<LoginResponse> {
    const response = await httpClient.post<LoginResponse>(`${this.baseUrl}/login`, credentials);
    return response.data;
  }

  async logout(): Promise<void> {
    await httpClient.post(`${this.baseUrl}/logout`);
  }

  async refreshToken(): Promise<{ token: string; expiresAt: Date }> {
    const refreshToken = localStorage.getItem('refresh_token');
    if (!refreshToken) {
      throw new Error('No refresh token available');
    }

    const response = await httpClient.post<{ token: string; expiresAt: Date }>(
      `${this.baseUrl}/refresh`,
      { refreshToken }
    );
    return response.data;
  }

  async getCurrentUser(): Promise<User> {
    const response = await httpClient.get<User>(`${this.baseUrl}/me`);
    return response.data;
  }

  async changePassword(currentPassword: string, newPassword: string): Promise<void> {
    await httpClient.post(`${this.baseUrl}/change-password`, {
      currentPassword,
      newPassword,
    });
  }

  async forgotPassword(email: string): Promise<void> {
    await httpClient.post(`${this.baseUrl}/forgot-password`, { email });
  }

  async resetPassword(token: string, newPassword: string): Promise<void> {
    await httpClient.post(`${this.baseUrl}/reset-password`, {
      token,
      newPassword,
    });
  }
}

export const authService = new AuthService();