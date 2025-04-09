<template>
  <div class="container">
    <h2 class="text-center mb-4">Anime List</h2>

    <div class="mb-3 text-center">
      <button class="btn btn-primary" @click="startCreate">Add New Anime</button>
    </div>

    <div class="mb-3">
      <input
        v-model="searchTitle"
        type="text"
        class="form-control"
        placeholder="Search by title"
        @input="fetchAnimes"
      />
    </div>

    <table class="table table-striped table-bordered">
      <thead class="thead-dark">
        <tr>
          <th scope="col">ID</th>
          <th scope="col">Title</th>
          <th scope="col">Description</th>
          <th scope="col">Genre</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="anime in animes" :key="anime.id">
          <td>{{ anime.id }}</td>
          <td>{{ anime.title }}</td>
          <td>{{ anime.description }}</td>
          <td>{{ anime.genre }}</td>
          <td>
            <button class="btn btn-primary btn-sm mr-2" @click="editAnime(anime)">Edit</button>
            <button class="btn btn-danger btn-sm" @click="deleteAnime(anime.id)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <div
      class="modal fade"
      id="animeModal"
      tabindex="-1"
      aria-labelledby="animeModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="animeModalLabel">{{ isEditing ? 'Edit Anime' : 'Create Anime' }}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="cancelEdit"></button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="saveAnime">
              <div class="mb-3">
                <label for="title" class="form-label">Title</label>
                <input
                  v-model="currentAnime.title"
                  type="text"
                  class="form-control"
                  id="title"
                  placeholder="Title"
                  required
                />
              </div>
              <div class="mb-3">
                <label for="description" class="form-label">Description</label>
                <input
                  v-model="currentAnime.description"
                  type="text"
                  class="form-control"
                  id="description"
                  placeholder="Description"
                  required
                />
              </div>
              <div class="mb-3">
                <label for="genre" class="form-label">Genre</label>
                <input
                  v-model="currentAnime.genre"
                  type="text"
                  class="form-control"
                  id="genre"
                  placeholder="Genre"
                  required
                />
              </div>
              <button type="submit" class="btn btn-success mr-2">Save</button>
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" @click="cancelEdit">Cancel</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AnimeService from '../services/AnimeService';
import { Modal } from 'bootstrap';

export default {
  data() {
    return {
      animes: [],
      searchTitle: '',
      isEditing: false,
      isCreating: false,
      currentAnime: { id: null, title: '', description: '', genre: '' },
      modal: null,
    };
  },
  async created() {
    await this.fetchAnimes();
  },
  mounted() {
    this.modal = new Modal(document.getElementById('animeModal'));
  },
  methods: {
    async fetchAnimes() {
      try {
        if (this.searchTitle) {
          this.animes = await AnimeService.searchAnimesByTitle(this.searchTitle);
        } else {
          this.animes = await AnimeService.getAllAnimes();
        }
      } catch (error) {
        console.error('Error fetching animes:', error);
      }
    },
    async deleteAnime(id) {
      if (confirm('Are you sure you want to delete this anime?')) {
        try {
          await AnimeService.deleteAnime(id);
          await this.fetchAnimes();
        } catch (error) {
          console.error('Error deleting anime:', error);
        }
      }
    },
    editAnime(anime) {
      this.isEditing = true;
      this.currentAnime = { ...anime };
      this.modal.show();
    },
    startCreate() {
      this.isCreating = true;
      this.currentAnime = { id: null, title: '', description: '', genre: '' };
      this.modal.show();
    },
    async saveAnime() {
      try {
        if (this.isEditing) {
          await AnimeService.updateAnime(this.currentAnime.id, this.currentAnime);
        } else {
          await AnimeService.createAnime(this.currentAnime);
        }
        this.isEditing = false;
        this.isCreating = false;
        this.modal.hide();
        await this.fetchAnimes();
      } catch (error) {
        console.error('Error saving anime:', error);
      }
    },
    cancelEdit() {
      this.isEditing = false;
      this.isCreating = false;
      this.currentAnime = { id: null, title: '', description: '', genre: '' };
      this.modal.hide();
    },
  },
};
</script>

<style>
.container {
  max-width: 100%;
  margin: 0 auto;
  background: rgba(255, 255, 255, 0.8);
  padding: 20px;
  border-radius: 10px;
}
.btn-sm {
  margin-right: 5px;
}
</style>