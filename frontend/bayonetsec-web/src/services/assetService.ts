import { Asset, PaginatedResponse } from '../types';
import { httpClient } from '../api/httpClient';

class AssetService {
  private readonly baseUrl = '/api/assets';

  async getAssets(
    pageNumber = 1,
    pageSize = 10,
    filters?: {
      projectId?: string;
      type?: string;
      search?: string;
    }
  ): Promise<PaginatedResponse<Asset>> {
    const params = new URLSearchParams({
      pageNumber: pageNumber.toString(),
      pageSize: pageSize.toString(),
      ...filters,
    });

    const response = await httpClient.get<PaginatedResponse<Asset>>(
      `${this.baseUrl}?${params}`
    );
    return response.data;
  }

  async getAsset(id: string): Promise<Asset> {
    const response = await httpClient.get<Asset>(`${this.baseUrl}/${id}`);
    return response.data;
  }

  async createAsset(asset: Omit<Asset, 'id' | 'createdAt' | 'updatedAt'>): Promise<Asset> {
    const response = await httpClient.post<Asset>(this.baseUrl, asset);
    return response.data;
  }

  async updateAsset(id: string, asset: Partial<Asset>): Promise<Asset> {
    const response = await httpClient.put<Asset>(`${this.baseUrl}/${id}`, asset);
    return response.data;
  }

  async deleteAsset(id: string): Promise<void> {
    await httpClient.delete(`${this.baseUrl}/${id}`);
  }
}

export const assetService = new AssetService();