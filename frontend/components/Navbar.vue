<template>
  <div>
    <v-navigation-drawer v-model="drawer" clipped app v-if="this.$store.state.auth">
      <v-list dense>
        <template v-for="item in items">
          <v-list-item :key="item.text">
            <v-list-item-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                <v-btn text small>
                  <nuxt-link :to="item.href">{{ item.text }}</nuxt-link>
                </v-btn>
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="primary" dark>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <v-app-bar-nav-icon @click.stop="drawer = !drawer" v-if="this.$store.state.auth"></v-app-bar-nav-icon>
        <nuxt-link to="/">
          <v-btn text large>App frontend</v-btn>
        </nuxt-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu bottom left offset-y open-on-hover v-if="this.$store.state.auth">
        <template v-slot:activator="{ on }">
          <v-btn dark icon v-on="on">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>

        <v-list>
          <v-list-item>
            <v-list-item-icon>
              <v-icon>mdi-logout-variant</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-btn text @click="logout()">Cerrar sesión</v-btn>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-menu>
      <div v-else>
        <v-btn text large to="/register">Regístrate</v-btn>
        <v-btn text large to="/login">Inicia Sesión</v-btn>
      </div>
    </v-app-bar>
  </div>
</template>


<script>
const Cookie = process.client ? require("js-cookie") : undefined;

export default {
  data: () => ({
    drawer: null,
    loading: false,
    items: [{ icon: "mdi-airplane", text: "Vuelos", href: "/vuelos" }],
  }),
  methods: {
    logout() {
      Cookie.remove("auth");
      this.$store.commit("setAuth", null);
      this.$router.push("/login");
    },
  },
};
</script>
