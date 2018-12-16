<template>
  <div class="columns info-text is-mobile">
    <div class="column">
      <img src="../assets/img/humidity.png" alt="Humidity" title="Humidity" class="img-icon-weather" />
      <span>{{ forecast.humidity.toFixed(2) }}%</span>
    </div>
    <div class="column">
      <img src="../assets/img/temperature.png" alt="Temperature" title="Temperature" class="img-icon-weather" />
      <span>{{ forecast.temp.toFixed(2) }}&ordm;C</span>
    </div>
    <div class="column">
      <img src="../assets/img/wind.png" alt="Windspeed" title="Windspeed" class="img-icon-weather" />
      <span>{{ forecast.speed.toFixed(2) }} m/s</span>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    forecast: {
      type: Object,
      default () {
        return {
          humidity: 0,
          speed: 0,
          temp: 0
        }
      }
    }
  }
}
</script>
