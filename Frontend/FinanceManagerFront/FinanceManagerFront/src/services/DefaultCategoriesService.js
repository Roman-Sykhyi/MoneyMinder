import { apiClient } from "../client";

export default class DefaultCategoriesService {
    static async loadDefaultCategories() {
        const response = apiClient.get('categories/defaultCategories')
        
        return response
    }
}