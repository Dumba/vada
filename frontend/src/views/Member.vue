<template>
  <main class="detail" :class="{ edit }">
    <h1>{{ localize("member") }}</h1>
    <button @click="toggleEdit">{{ localize(edit ? "stop" : "edit") }}</button>
    <button @click="remove">{{ localize("delete") }}</button>

    <label for="firstName">{{ localize("firstName") }}</label>
    <input
      id="firstName"
      :value="member.firstName"
      @keyup="change('firstName', $event.target.value)"
      :disabled="!edit"
    />
    <label for="middleName">{{ localize("middleName") }}</label>
    <input
      id="middleName"
      :value="member.middleName"
      @keyup="change('middleName', $event.target.value)"
      :disabled="!edit"
    />
    <label for="lastName">{{ localize("lastName") }}</label>
    <input
      id="lastName"
      :value="member.lastName"
      @keyup="change('lastName', $event.target.value)"
      :disabled="!edit"
    />
    <label for="role">{{ localize("role") }}</label>
    <select
      :value="member.role"
      @change="change('role', $event.target.value)"
      :disabled="!edit"
    >
      <option value="child">{{ localize("child") }}</option>
      <option value="parent">{{ localize("parent") }}</option>
    </select>
    <label for="family">{{ localize("family") }}</label>
    <select
      :value="member.familyId"
      @change="change('familyId', $event.target.value)"
      :disabled="!edit"
    >
      <option v-for="family in families" :key="family.id" :value="family.id">
        {{ family.name }}
      </option>
    </select>
    <label for="street">{{ localize("street") }}</label>
    <input id="street" :value="family.street" disabled />
    <label for="houseNumber">{{ localize("houseNumber") }}</label>
    <input id="houseNumber" :value="family.houseNumber" disabled />
    <label for="city">{{ localize("city") }}</label>
    <input id="city" :value="family.city" disabled />
    <label for="zip">{{ localize("zip") }}</label>
    <input id="zip" :value="family.zip" disabled />
  </main>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { useStore } from "../store/index";
import { Member, Component, Family } from "../store/types";

export default class MemberComp extends Vue {
  edit: boolean = false;

  get id(): string {
    return this.$route.params.id as string;
  }

  get member(): Member {
    const store = useStore();
    const id = this.id;

    const member = store.state.members.find(m => m.id == id) ?? store.state.members[store.state.members.length - 1];

    return member;
  }

  get family(): Family {
    return this.families.find(f => f.id == this.member.familyId) ?? ({} as Family);
  }

  get families(): Family[] {
    const store = useStore();
    return store.state.families;
  }

  toggleEdit() {
    this.edit = !this.edit;
  }

  localize(key: string): string {
    const store = useStore();
    return store.state.localization[key] ?? key;
  }

  change(property: string, value: string): void {
    ((this as unknown) as Component).$store.commit("updateMember", {
      id: this.member.id,
      property,
      value,
    });
  }

  remove() {
    ((this as unknown) as Component).$store.commit("removeMember", { id: this.member.id });
    this.$router.push("/");
  }

  mounted() {
    this.edit = this.$route.params.id == "detail";
  }
}
</script>
