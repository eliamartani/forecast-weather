<template>
  <div
    id="app"
    class="hero is-fullheight"
  >
    <Header />
    <router-view />
    <Footer />
  </div>
</template>

<script>
export default {
  components: {
    Header: () => import('@/components/HeaderCard'),
    Footer: () => import('@/components/FooterCard')
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
