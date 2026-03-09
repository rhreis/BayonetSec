import { Project, PaginatedResponse } from '../types';
import { httpClient } from '../api/httpClient';

class ProjectService {
  private readonly baseUrl = '/api/projects';

  async getProjects(pageNumber = 1, pageSize = 10): Promise<PaginatedResponse<Project>> {
    const response = await httpClient.get<PaginatedResponse<Project>>(
      `${this.baseUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
    return response.data;
  }

  async getProject(id: string): Promise<Project> {
    const response = await httpClient.get<Project>(`${this.baseUrl}/${id}`);
    return response.data;
  }

  async createProject(project: Omit<Project, 'id' | 'createdAt' | 'updatedAt'>): Promise<Project> {
    const response = await httpClient.post<Project>(this.baseUrl, project);
    return response.data;
  }

  async updateProject(id: string, project: Partial<Project>): Promise<Project> {
    const response = await httpClient.put<Project>(`${this.baseUrl}/${id}`, project);
    return response.data;
  }

  async deleteProject(id: string): Promise<void> {
    await httpClient.delete(`${this.baseUrl}/${id}`);
  }
}

export const projectService = new ProjectService();