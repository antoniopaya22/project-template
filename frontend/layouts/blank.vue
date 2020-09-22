<template>
  <v-app dark class="trans">
    <v-main class="bgimg">
      <v-container class="fill-height">
        <nuxt />
      </v-container>
    </v-main>
    <Footer/>
  </v-app>
</template>

<script>
import Footer from '~/components/Footer';

export default {
  layout: "empty",
  components: {
    Footer
  }
};
</script>

<style scoped>
.trans {
  background-color: transparent;
}

.bgimg {
  background-image: url("~assets/login-background.jpg");
  background-repeat: no-repeat;
  background-size: 100% 100%;
}
</style>
