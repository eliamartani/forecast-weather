<template>
  <div class="columns info-text is-mobile">
    <div class="column">
      <img
        src="../assets/img/humidity.png"
        alt="Humidity"
        title="Humidity"
        class="img-icon-weather"
      >
      <span>{{ humidity }}</span>
    </div>
    <div class="column">
      <img
        src="../assets/img/temperature.png"
        alt="Temperature"
        title="Temperature"
        class="img-icon-weather"
      >
      <span>{{ temp }}</span>
    </div>
    <div class="column">
      <img
        src="../assets/img/wind.png"
        alt="Windspeed"
        title="Windspeed"
        class="img-icon-weather"
      >
      <span>{{ speed }}</span>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    forecast: {
      default: () => ({
        humidity: 0,
        speed: 0,
        temp: 0
      }),
      required: false,
      type: Object
    }
  },
  data () {
    return {
      assetsUrl: ''
    }
  },
  computed: {
    humidity () {
      return `${ this.forecast.humidity.toFixed(2) }%`
    },
    temp () {
      return `${ this.forecast.temp.toFixed(2) }ºC`
    },
    speed () {
      return `${ this.forecast.speed.toFixed(2) } m/s`
    }
  }
}
</script>
