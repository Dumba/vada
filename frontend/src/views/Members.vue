<template>
  <main class="list">
    <h1>{{ localize("members") }}</h1>
    <button @click="create">{{ localize("create") }}</button>
    <ul>
      <li v-for="member in members" :key="member.id">
        <router-link :to="`/members/${member.id}`">
          <div class="firstName">{{ member.firstName }}</div>
          <div class="middleName">{{ member.middleName }}</div>
          <div class="lastName">{{ member.lastName }}</div>
        </router-link>
      </li>
    </ul>
  </main>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { useStore } from "../store/index";
import { Component, Member } from "../store/types";

export default class Members extends Vue {
  get members(): Member[] {
    const store = useStore();
    return store.state.members;
  }

  localize(key: string): string {
    const store = useStore();
    return store.state.localization[key] ?? key;
  }

  create() {
    (this as unknown as Component).$store.commit("createMember", {});
    this.$router.push("/members/detail");
  }
}
</script>