<template>
  <main class="detail" :class="{ edit }">
    <h1>{{ localize("family") }}</h1>
    <button @click="toggleEdit">{{ localize(edit ? "stop" : "edit") }}</button>
    <button @click="remove">{{ localize("remove") }}</button>

    <label for="name">{{ localize("name") }}</label>
    <input
      id="name"
      :value="family.name"
      @keyup="change('name', $event.target.value)"
      :disabled="!edit"
    />
    <label for="street">{{ localize("street") }}</label>
    <input
      id="street"
      :value="family.street"
      @keyup="change('street', $event.target.value)"
      :disabled="!edit"
    />
    <label for="houseNumber">{{ localize("houseNumber") }}</label>
    <input
      id="houseNumber"
      :value="family.houseNumber"
      @keyup="change('houseNumber', $event.target.value)"
      :disabled="!edit"
    />
    <label for="city">{{ localize("city") }}</label>
    <input
      id="city"
      :value="family.city"
      @keyup="change('city', $event.target.value)"
      :disabled="!edit"
    />
    <label for="zip">{{ localize("zip") }}</label>
    <input
      id="zip"
      :value="family.zip"
      @keyup="change('zip', $event.target.value)"
      :disabled="!edit"
    />
  </main>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { useStore } from "../store/index";
import { Component, Family } from "../store/types";

export default class FamilyComp extends Vue {
  edit: boolean = false;

  get id(): string {
    return this.$route.params.id as string;
  }

  get family(): Family {
    const store = useStore();
    const id = this.id;

    const family = store.state.families.find(f => f.id == id) ?? store.state.families[store.state.families.length - 1];
    
    return family;
  }

  toggleEdit() {
    this.edit = !this.edit;
  }

  localize(key: string): string {
    const store = useStore();
    return store.state.localization[key] ?? key;
  }

  change(property: string, value: string): void {
    ((this as unknown) as Component).$store.commit("updateFamily", {
      id: this.family.id,
      property,
      value,
    });
  }

  remove() {
    ((this as unknown) as Component).$store.commit("removeFamily", { id: this.family.id });
    this.$router.push("/families");
  }

  mounted() {
    this.edit = this.$route.params.id == "detail";
  }
}
</script>
