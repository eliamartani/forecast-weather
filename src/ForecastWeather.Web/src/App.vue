<template>
  <div id="app" class="hero is-fullheight">
    <Header />
    <router-view />
    <Footer />
  </div>
</template>

<script>
import Header from '@/components/HeaderCard'
import Footer from '@/components/FooterCard'

export default {
  components: {
    Header,
    Footer
  }
}
</script>

<style lang="less">
#app {
  .hero-body {
    align-items: normal;
  }
}
</style>
