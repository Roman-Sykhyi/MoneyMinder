import { apiClient } from "../client";

export default class DefaultCategoriesService {
    static async loadDefaultCategories() {
        const response = apiClient.get('categories/defaultCategories')
        
        const test = apiClient.get('python/prediction')
        console.log(test);

        return response
    }
}