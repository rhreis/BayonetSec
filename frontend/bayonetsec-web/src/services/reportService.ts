import { Report, PaginatedResponse } from '../types';
import { httpClient } from '../api/httpClient';

class ReportService {
  private readonly baseUrl = '/api/reports';

  async getReports(
    pageNumber = 1,
    pageSize = 10,
    filters?: {
      projectId?: string;
      type?: string;
      status?: string;
    }
  ): Promise<PaginatedResponse<Report>> {
    const params = new URLSearchParams({
      pageNumber: pageNumber.toString(),
      pageSize: pageSize.toString(),
      ...filters,
    });

    const response = await httpClient.get<PaginatedResponse<Report>>(
      `${this.baseUrl}?${params}`
    );
    return response.data;
  }

  async getReport(id: string): Promise<Report> {
    const response = await httpClient.get<Report>(`${this.baseUrl}/${id}`);
    return response.data;
  }

  async generateReport(reportRequest: {
    projectId: string;
    type: string;
    parameters?: Record<string, unknown>;
  }): Promise<Report> {
    const response = await httpClient.post<Report>(`${this.baseUrl}/generate`, reportRequest);
    return response.data;
  }

  async downloadReport(id: string): Promise<Blob> {
    const response = await httpClient.get(`${this.baseUrl}/${id}/download`, {
      responseType: 'blob',
    });
    return response.data as Blob;
  }

  async deleteReport(id: string): Promise<void> {
    await httpClient.delete(`${this.baseUrl}/${id}`);
  }
}

export const reportService = new ReportService();