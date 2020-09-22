<template>
  <v-row align="center" justify="center">
    <v-col cols="12" lg="6" md="8" sm="12" xs="6">
      <v-card class="elevation-12">
        <v-alert type="error" :message="error" v-if="error" dismissible>{{error}}</v-alert>
        <v-form @submit.prevent="addUser()">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Regístrate</v-toolbar-title>
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
              label="Nombre"
              v-model="name"
              prepend-icon="mdi-account-box"
              outlined
              type="text"
            />
            <v-text-field
              label="Apellidos"
              v-model="lastname"
              prepend-icon="mdi-account-box"
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
            <v-text-field
              label="Repite la Contraseña"
              v-model="repassword"
              prepend-icon="mdi-textbox-password"
              outlined
              type="password"
            />
          </v-card-text>
          <v-card-actions>
            <v-container>
              <v-row>
                <v-col md="6" sm="12" xs="12">
                  <v-btn text color="deep-orange accent-4" to="/login">¿Ya tienes cuenta? Inicia sesión</v-btn>
                </v-col>
                <v-col md="6" sm="12" xs="12" align="right">
                  <v-btn color="primary" type="submit">Regístrate</v-btn>
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
      name: "",
      lastname: "",
      password: "",
      repassword: "",
      error: null
    };
  },
  methods: {
    addUser() {
      if(this.password == this.repassword){
        this.$axios
        .post(process.env.BASE_URL + "/api/users", {
          username: this.username,
          lastname: this.lastname,
          password: this.password
        })
        .then(response => {
          this.$swal("¡Registrado!", "Te has registrado correctamente, prueba a iniciar sesión", "success");
          this.$router.push("/login");
        })
        .catch(e => {
          this.error = e.response.data.Error;
        });
      } else{
        this.error = "Las contraseñas no coinciden";
      }
    }
  }
};
</script>
