<template>
  <section class="hero-body">
    <div class="container">
      <div class="columns">
        <div class="column">
          <h1 class="title has-text-success">
            Check out the next 5 days forecast
          </h1>
          <h2 class="subtitle has-text-grey">
            Type the city name or the postal code and it will load automatically
          </h2>
          <TheSearch />
        </div>
      </div>
      <TheForecastList />
      <div class="columns has-padding-top-20">
        <div class="column">
          <h4 class="title is-6">
            Notes:
          </h4>
          <ul>
            <li>- This site uses <strong>metric unit</strong></li>
            <li>- Autocomplete field shows only <strong>german cities</strong>. However you can search for any city in the world by adding comma and the country you want. <i>e.g. "lisbon,pt"</i></li>
            <li>- The result is composed by <strong>Humidity</strong>, <strong>Temperature</strong> and <strong>Windspeed</strong> </li>
            <li>- <strong>MomentJs</strong> considers your current language for some fields</li>
          </ul>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  components: {
    TheForecastList: () => import('@/components/TheForecastList'),
    TheSearch: () => import('@/components/TheSearch')
  }
}
</script>

<style>
.has-padding-top-20 {
  padding-top: 20px;
}
</style>
