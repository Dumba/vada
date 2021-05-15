<template>
  <div id="nav">
    <router-link to="/">{{ localize("members") }}</router-link>
    <router-link to="/families">{{ localize("families") }}</router-link>
  </div>
  <router-view/>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { useStore } from "./store/index";

export default class AppComp extends Vue {
  localize(key: string): string {
    const store = useStore();
    return store.state.localization[key] ?? key;
  }

  mounted() {
    var store = useStore();
    store.dispatch("load", {});
  }
}
</script>

<style lang="sass">
#app
  display: grid
  margin: 2rem
  grid-template-columns: 800px
  justify-content: center

  #nav
    display: grid
    grid-auto-flow: column
    grid-gap: 1rem
    justify-content: center

    a
      text-decoration: none
      color: #123
      font-weight: bold
    
  h1
    justify-self: center
  button
    margin-top: -20px
    margin-bottom: 20px
    padding: 0px 15px
    justify-self: center
    width: max-content
  
  .detail
    display: grid
    grid-template-columns: 1fr 2fr
    grid-gap: 1rem

    h1
      grid-column: 1 / 3
    button
      grid-column: 1 / 3

    label
      grid-column: 1
      justify-self: end
      font-weight: bold
    input, select
      grid-column: 2

    &:not(.edit)
      input, select
        border-color: transparent
        opacity: 1
        color: #000
        background-color: #fff
        appearance: none

  .list
    display: grid
    justify-content: center

    ul, li
      display: grid
      grid-gap: 5px
      padding: 0

      a
        display: grid
        padding-left: 3px
        grid-auto-flow: column
        grid-gap: 5px
        text-decoration: none
        color: inherit

        border-left: 3px solid #000
</style>