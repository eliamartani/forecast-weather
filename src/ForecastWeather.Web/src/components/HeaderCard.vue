<template>
  <header id="header">
    <nav class="navbar is-spaced">
      <div class="container">
        <div class="navbar-brand">
          <router-link to="/" class="navbar-item">
            <img src="../assets/img/logo.png" alt="Forecast Weather">
          </router-link>
          <span class="navbar-burger burger" v-bind:class="{ 'is-active': isMenuOpened }" v-on:click="isMenuOpened = !isMenuOpened">
            <span></span>
            <span></span>
            <span></span>
          </span>
        </div>
        <div id="navbarMenu" class="navbar-menu" v-bind:class="{ 'is-active': isMenuOpened }">
          <div class="navbar-end">
            <router-link class="navbar-item" to="/">Home</router-link>
            <router-link class="navbar-item" to="/about">About</router-link>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<script>
export default {
  data () {
    return {
      isMenuOpened: false
    }
  }
}
</script>

<style lang="less">
#header {
  .router-link-exact-active {
    color: hsl(141, 71%, 48%);
  }
  .navbar-item {
    background-color: transparent;
    transition: all 0.2s linear;
  }
}
</style>
