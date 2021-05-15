import { createStore, useStore as VuexStore } from 'vuex';
import { Store, State, UpdateParam } from './types'

export default createStore<State>({
  state: {
    members: [
      {
        id: "32fa11ed-4608-4c02-ac77-bf802ea8bd52",
        firstName: "Samuel",
        middleName: "L.",
        lastName: "Jackson",
        role: "child",
        familyId: "98bdd017-51fe-4df6-84d1-07080870844c",
      },
      {
        id: "32fa11ed-4608-4c02-ac77-bf802ea8bd53",
        firstName: "Jiří",
        lastName: "Jackson",
        role: "child",
        familyId: "98bdd017-51fe-4df6-84d1-07080870844c",
      }
    ],
    families: [
      {
        id: "98bdd017-51fe-4df6-84d1-07080870844b",
        name: "Lachmani",
        street: "Dubějovická",
        houseNumber: "50",
        city: "Trhový Štěpánov",
        zip: "257 63",
      },
      {
        id: "98bdd017-51fe-4df6-84d1-07080870844c",
        name: "ee",
        street: "uu",
        houseNumber: "50",
        city: "Trhový",
        zip: "257 63",
      }
    ],
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
        role: "",
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
    removeMember(store: State, payload) {
      store.members = store.members.filter(m => m.id != payload.id);
      console.log(store);
      
    },
    removeFamily(store: State, payload) {
      store.families = store.families.filter(f => f.id != payload.id);
      store.members = store.members.filter(m => m.familyId != payload.id);
    }
  },
  actions: {
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