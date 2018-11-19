<template>
  <div>
    <b-loading :is-full-page="false" :active.sync="isLoading" :can-cancel="true"></b-loading>
    <div class="forecast" v-if="!isLoading && result">
      <div class="columns">
        <div class="column is-block-tablet is-block-mobile is-half-tablet-only is-3-desktop">
          <iframe
            class="google_maps"
            width="303"
            height="277"
            v-bind:src="[`https://maps.google.com/maps?q=${result.city.name}%2C${result.city.country}&t=&z=13&ie=UTF8&iwloc=&output=embed`]"
            frameborder="0" scrolling="no" marginheight="0" marginwidth="0">
          </iframe>
        </div>
        <div class="column is-half-tablet-only is-9-desktop is-tomorrow is-today">
          <template v-bind="tomorrow">
            <div class="box">
              <div class="columns">
                <div class="column">
                  <h4 class="subtitle is-4 has-text-info">
                    <span v-bind:class="['flag-icon', 'flag-icon-' + result.city.country.toLowerCase()]"></span>
                    &nbsp;{{ result ? `${ result.city.name }, ${ result.city.country }` : '' }}
                  </h4>
                  <p class="day-week">
                    <img v-bind:src="[urlIcon + tomorrow.list[0].icon + '.png']" class="img-weather" alt="">
                  </p>
                  <p>
                    <span class="day-week">{{ moment(tomorrow.date).format('dddd') }}</span>
                    <span class="day">{{ moment(tomorrow.date).format('Do') }}
                      <span class="month">{{ moment(tomorrow.date).format('MMM') }}</span>
                    </span>
                  </p>
                </div>
                <div class="column">
                  <div class="table-days">
                    <div v-for="(weather, indexTime) in tomorrow.list" :key="indexTime">
                      <div class="columns info-text is-mobile">
                        <div class="column time">
                          {{ moment(weather.date).format('LT') }}
                        </div>
                        <div class="column">
                          {{ weather.humidity }}%
                        </div>
                        <div class="column">
                          {{ weather.temp }}&ordm;C
                        </div>
                        <div class="column">
                          {{ weather.speed }} m/s
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </template>
        </div>
      </div>
      <h3 class="subtitle menu-weather">
        <a href="javascript:;" v-bind:class="{ 'is-active': isForecastSelected }" v-on:click="isForecastSelected = true">5 days weather forecast</a>&nbsp;|&nbsp;
        <a href="javascript:;" v-bind:class="{ 'is-active': !isForecastSelected }" v-on:click="isForecastSelected = false">History</a>
      </h3>
      <div v-if="isForecastSelected" class="columns other-days is-desktop">
        <div class="column" v-for="(item, indexDay) in nextDays" v-bind:key="indexDay">
          <div class="box">
            <p>
              <span class="day-week">
                <img v-bind:src="[urlIcon + item.list[0].icon + '.png']" class="img-weather" alt="">
                <br>
                {{ moment(item.date).format('dddd') }}</span>
              <span class="day">
                {{ moment(item.date).format('Do') }}
                <span class="month">{{ moment(item.date).format('MMM') }}</span>
              </span>
            </p>
            <div class="table-days">
              <div v-for="(weather, indexTime) in item.list" :key="indexTime">
                <div class="columns info-text is-mobile">
                  <div class="column time">
                    {{ moment(weather.date).format('LT') }}
                  </div>
                  <div class="column">
                    {{ weather.humidity }}%
                  </div>
                  <div class="column">
                    {{ weather.temp }}&ordm;C
                  </div>
                  <div class="column">
                    {{ weather.speed }} m/s
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="columns is-history-list">
        <div class="column" v-for="(historyItem, index) in resultHistory" :key="index">
          <div class="box">
            <span class="day-week">
              <img v-bind:src="[urlIcon + historyItem.icon + '.png']" class="img-weather" alt="">
              <br>
              {{ historyItem.date }}
            </span>
            <div class="columns info-text is-mobile">
              <div class="column">
                {{ historyItem.humidity }}%
              </div>
              <div class="column">
                {{ historyItem.temp }}&ordm;C
              </div>
              <div class="column">
                {{ historyItem.speed }} m/s
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import api from '../data/api.json'
import axios from 'axios'
import moment from 'moment'
import {
  serverBus
} from '../store'
import 'flag-icon-css/css/flag-icon.min.css'

export default {
  data () {
    return {
      isForecastSelected: true,
      isLoading: false,
      result: null,
      resultHistory: null,
      searchValue: '',
      urlIcon: api.urlIcon
    }
  },
  created () {
    // receives value when event $emit is triggered
    serverBus.$on('change', (query, value, country) => {
      if (!value) return

      this.searchValue = value
      this.isLoading = true

      if (!country) country = api.default_country

      axios
        .get(`${api.url}?${query}=${value}&countryCode=${country}`)
        .then(response => {
          this.isLoading = false

          if (response.status === 200) {
            this.result = response.data.data

            if (this.result) {
              this.processToday()
            }
          }
        })
        .catch(error => {
          this.isLoading = false
          this.error(error)
        })
    })
  },
  computed: {
    nextDays () {
      return this.result.listByDay.filter((item, index) => {
        return index > 0
      })
    },
    tomorrow () {
      return this.result.listByDay[0]
    }
  },
  methods: {
    error (data) {
      let message = ''

      if (typeof data === 'object') {
        message = data.message
      } else {
        message = data
      }

      this.$toast.open({
        duration: 5000,
        message,
        position: 'is-bottom',
        type: 'is-danger'
      })
    },
    moment (value) {
      return moment(value)
    },
    processToday () {
      const storeKey = this.result.city.name + this.result.city.country
      const today = moment(this.result.today.date).format('L')
      const weatherKey = 'weather'
      const weatherData = JSON.parse(localStorage.getItem(weatherKey) || '{}')

      // get city data from local storage
      let store = weatherData[storeKey]

      if (!store) {
        store = {
          list: []
        }
      }

      console.log(today, this.result.today.date)

      // look if already there is a register for today's weather
      if (store.list.filter(item => item.date === today).length === 0) {
        store.list.push({ ...this.result.today, date: today })

        // add today for the list
        weatherData[storeKey] = store

        // save data locally
        localStorage.setItem(weatherKey, JSON.stringify(weatherData))
      }

      // shows history list from user
      this.resultHistory = store.list
    }
  }
}
</script>

<style lang="less">
#app {
  .forecast {
    .google_maps {
      width: 100%;
      height: 100%;
    }
    .day {
      display: block;
      font-weight: bold;
      margin-top: -10px;
      color: hsl(217, 71%, 53%);
      font-size: 40px;
      .month {
        color: hsl(204, 86%, 53%);
        font-size: 23px;
      }
    }
    .day-week {
      display: block;
      font-size: 20px;
      color: hsl(141, 71%, 48%);
    }
    .day, .day-week {
      text-align: center;
    }
    .is-tomorrow, .is-today {
      @media screen and (min-width: 1088px) {
        .day, .day-week {
          text-align: left;
        }
      }
    }
    .other-days {
      @media screen and (min-width: 1088px) and (max-width: 1279px) {
        .box {
          padding: 0.5rem;
        }
      }
    }
    .table-days {
      .time {
        color: hsl(171, 100%, 41%);
        font-weight: bold;
      }
      .info-text {
        font-size: 14px;
      }
    }
    .img-weather {
      width: 40px;
      height: 40px;
      margin-bottom: -10px;
    }
    .menu-weather {
      text-align: center;
      a {
        @link: hsl(348, 100%, 61%);
        color: @link;
        transition: color 0.2s;
        &:hover {
          color: lighten(@link, 10%);
        }
        &.is-active {
          text-decoration: underline;
        }
      }
    }
    .is-history-list {
      flex-wrap: wrap;
      .column {
        flex: 1 0 25%;
      }
    }
  }
}
</style>
