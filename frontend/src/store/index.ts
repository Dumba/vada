import axios from "axios";
import { createStore, useStore as VuexStore } from 'vuex';
import { Store, State, UpdateParam, DeleteParam } from './types'

export default createStore<State>({
  state: {
    members: [],
    families: [],
    localization: {
      members: "Členové",
      member: "Člen",
      families: "Rodiny",
      family: "Rodina",
      id: "Id",
      firstName: "Jméno",
      middleName: "2. jméno",
      lastName: "Příjmení",
      role: "Role",
      name: "Název",
      street: "Ulice",
      houseNumber: "č.p.",
      city: "Obec",
      zip: "PSČ",
      child: "Dítě",
      parent: "Rodič",
      create: "Vytvořit",
      edit: "Upravit",
      stop: "Ukončit úpravu"
    }
  },
  mutations: {
    load(store, payload) {
      store.members = payload.members;
      store.families = payload.families;
    },
    updateMember(store: State, payload: UpdateParam) {
      const member = store.members.find(m => m.id == payload.id);
      (member as any)[payload.property] = payload.value;
    },
    updateFamily(store: State, payload: UpdateParam) {
      const family = store.families.find(m => m.id == payload.id);
      (family as any)[payload.property] = payload.value;
    },
    createMember(store: State) {
      store.members.push({
        id: newGuid(),
        firstName: "",
        lastName: "",
        role: "child",
        familyId: store.families[0].id,
      });
    },
    createFamily(store: State) {
      store.families.push({
        id: newGuid(),
        name: "",
        street: "",
        houseNumber: "",
        city: "",
        zip: ""
      });
    },
    removeMember(store: State, payload: DeleteParam) {
      store.members = store.members.filter(m => m.id != payload.id);
      console.log(store);
      
    },
    removeFamily(store: State, payload: DeleteParam) {
      store.families = store.families.filter(f => f.id != payload.id);
      store.members = store.members.filter(m => m.familyId != payload.id);
    }
  },
  actions: {
    async load(context) {
      var membersResp = await axios.get("/api/member/list");
      var familiesResp = await axios.get("/api/family/list");

      context.commit("load", { members: membersResp.data, families: familiesResp.data });
    },
    async updateMember(context, payload: UpdateParam) {
      var updatedMember = context.state.members.find(m => m.id == payload.id);
      (updatedMember as any)[payload.property] = payload.value;

      await axios.post('/api/member/update', updatedMember);

      context.commit("updateMember", payload);
    },
    async updateFamily(context, payload: UpdateParam) {
      var updatedFamily = context.state.families.find(m => m.id == payload.id);
      (updatedFamily as any)[payload.property] = payload.value;

      await axios.post('/api/family/update', updatedFamily);

      context.commit("updateFamily", payload);
    },
    async removeMember(context, payload: DeleteParam) {
      await axios.delete(`/api/member/delete?id=${payload.id}`);

      context.commit("removeMember", payload);
    },
    async removeFamily(context, payload: DeleteParam) {
      await axios.delete(`/api/family/delete?id=${payload.id}`);

      context.commit("removeFamily", payload);
    },
  },
  modules: {
  }
})

function newGuid(): string {
  return `${randomHex()}${randomHex()}-${randomHex()}-4${randomHex().substr(0, 3)}-${randomHex()}-${randomHex()}${randomHex()}${randomHex()}`.toLowerCase();
}
function randomHex(): string {
  return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

export function useStore(): Store {
  return VuexStore() as unknown as Store;
}