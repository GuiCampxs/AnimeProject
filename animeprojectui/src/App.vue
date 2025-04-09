<template>
  <div id="app">
    <AnimeList />
  </div>
</template>

<script>
import AnimeList from './components/AnimeList.vue';

export default {
  components: {
    AnimeList,
  },
};
</script>

<style>
html, body {
  margin: 0;
  padding: 0;
  height: 100%;
  overflow-x: hidden; /* Evita rolagem horizontal */
}

#app {
  background-image: url('./assets/anime-background.jpg');
  background-size: cover; /* Faz o background cobrir a tela */
  background-position: center; /* Centraliza a imagem */
  background-attachment: fixed; /* Faz o background ficar fixo ao rolar */
  background-repeat: no-repeat; /* Evita repetição */
  min-height: 100vh; /* Garante que o app ocupe a altura total da tela */
  display: flex;
  justify-content: center;
  align-items: flex-start; /* Alinha o conteúdo no topo */
  padding: 20px;
  box-sizing: border-box;
}

/* Responsividade */
@media (max-width: 768px) {
  #app {
    padding: 10px;
    background-size: contain; /* Ajusta o background em telas menores */
  }
}
</style>