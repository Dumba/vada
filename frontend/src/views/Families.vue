<template>
  <main class="list">
    <h1>{{ localize("families") }}</h1>
    <button @click="create">{{ localize("create") }}</button>
    <ul>
      <li v-for="family in families" :key="family.id">
        <router-link :to="`/families/${family.id}`">
          <div class="name">{{ family.name }}</div>
        </router-link>
      </li>
    </ul>
  </main>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { useStore } from "../store/index";
import { Component, Family } from "../store/types";

export default class FamiliesComp extends Vue {
  get families(): Family[] {
    const store = useStore();
    return store.state.families;
  }

  localize(key: string): string {
    const store = useStore();
    return store.state.localization[key] ?? key;
  }

  create() {
    (this as unknown as Component).$store.commit("createFamily", {});
    this.$router.push("/families/detail");
  }
}
</script>