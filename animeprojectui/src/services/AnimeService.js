import axios from 'axios';

const API_URL = 'https://localhost:7260/api/Animes';

export default {
  async getAllAnimes() {
    const response = await axios.get(API_URL);
    return response.data;
  },

  async getAnimeById(id) {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  },

  async createAnime(anime) {
    const response = await axios.post(API_URL, anime);
    return response.data;
  },

  async updateAnime(id, anime) {
    const response = await axios.put(`${API_URL}/${id}`, anime);
    return response.data;
  },

  async deleteAnime(id) {
    await axios.delete(`${API_URL}/${id}`);
  },

  async searchAnimesByTitle(title) {
    const response = await axios.get(`${API_URL}/search?title=${title}`);
    return response.data;
  }
};