<template>
  <v-row align="center" justify="center">
    <v-col cols="12" lg="6" md="8" sm="12" xs="6">
      <v-card class="elevation-12">
        <v-alert type="error" :message="error" v-if="error">{{error}}</v-alert>
        <v-form @submit.prevent="login()">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Iniciar sesión</v-toolbar-title>
            <v-spacer />
          </v-toolbar>
          <v-card-text>
            <v-text-field
              label="Usuario"
              v-model="username"
              prepend-icon="mdi-account"
              outlined
              type="text"
            />
            <v-text-field
              label="Contraseña"
              v-model="password"
              prepend-icon="mdi-lock"
              outlined
              type="password"
            />
          </v-card-text>
          <v-card-actions>
            <v-container>
              <v-row>
                <v-col md="6" sm="12" xs="12">
                  <v-btn text color="deep-orange accent-4" to="/register">¿No tienes cuenta? Regístrate</v-btn>
                </v-col>
                <v-col md="6" sm="12" xs="12" align="right">
                  <v-btn color="primary" type="submit">Iniciar sesión</v-btn>
                </v-col>
              </v-row>
            </v-container>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
const Cookie = process.client ? require("js-cookie") : undefined;

export default {
  layout: "blank",
  data() {
    return {
      username: "",
      password: "",
      error: null
    };
  },
  methods: {
    login() {
      this.$axios
        .post(process.env.BASE_URL + "/api/login", {
          username: this.username,
          password: this.password
        })
        .then(response => {
          const auth = response.data;
          this.$store.commit("setAuth", auth);
          Cookie.set("auth", auth);
          this.$router.push("/");
        })
        .catch(e => {
          console.log(e.response.data)
          this.error = e.response.data.error;
        });
    }
  }
};
</script>
