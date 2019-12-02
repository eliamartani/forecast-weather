<template>
  <div>
    <b-loading
      :is-full-page="false"
      :active.sync="isLoading"
      :can-cancel="true"
    />
    <div
      v-if="!isLoading && result && result.city"
      class="forecast"
    >
      <div class="columns">
        <div class="column is-block-tablet is-block-mobile is-half-tablet-only is-3-desktop">
          <iframe
            class="google_maps"
            width="303"
            height="277"
            :src="[`https://maps.google.com/maps?q=${result.city.name}%2C${result.city.country}&t=&z=13&ie=UTF8&iwloc=&output=embed`]"
            frameborder="0"
            scrolling="no"
            marginheight="0"
            marginwidth="0"
          />
        </div>
        <div class="column is-half-tablet-only is-9-desktop is-tomorrow is-today">
          <template v-bind="tomorrow">
            <div class="box">
              <div class="columns">
                <div class="column">
                  <h4 class="subtitle is-4 has-text-info">
                    <span :class="['flag-icon', 'flag-icon-' + result.city.country.toLowerCase()]" />
                    &nbsp;
                    {{ result ? `${ result.city.name }, ${ result.city.country }` : '' }}
                  </h4>
                  <p class="day-week">
                    <img
                      :src="[urlIcon + tomorrow.forecast.icon + '.png']"
                      class="img-weather"
                      alt=""
                    >
                  </p>
                  <p>
                    <span class="day-week">{{ moment(tomorrow.date).format('dddd') }}</span>
                    <span class="day">{{ moment(tomorrow.date).format('Do') }}
                      <span class="month">{{ moment(tomorrow.date).format('MMM') }}</span>
                    </span>
                  </p>
                </div>
                <div class="column column-middle">
                  <div class="table-days">
                    <TheWeather :forecast="tomorrow.forecast" />
                  </div>
                </div>
              </div>
            </div>
          </template>
        </div>
      </div>
      <section>
        <b-tabs
          position="is-centered"
          type="is-toggle-rounded"
          class="block"
        >
          <b-tab-item label="Next 5 days">
            <div class="columns other-days is-desktop">
              <div
                v-for="(item, indexDay) in nextDays"
                :key="indexDay"
                class="column"
              >
                <div class="box">
                  <p>
                    <span class="day-week">
                      <img
                        :src="[urlIcon + item.forecast.icon + '.png']"
                        class="img-weather"
                        alt=""
                      >
                      <br>
                      {{ moment(item.date).format('dddd') }}</span>
                    <span class="day">
                      {{ moment(item.date).format('Do') }}
                      <span class="month">{{ moment(item.date).format('MMM') }}</span>
                    </span>
                  </p>
                  <div class="table-days">
                    <TheWeather :forecast="item.forecast" />
                  </div>
                </div>
              </div>
            </div>
          </b-tab-item>
          <b-tab-item label="History">
            <div class="columns is-history-list">
              <div
                v-for="(historyItem, index) in resultHistory"
                :key="index"
                class="column"
              >
                <div class="box">
                  <span class="day-week">
                    <img
                      :src="[urlIcon + historyItem.icon + '.png']"
                      class="img-weather"
                      alt=""
                    >
                    <br>
                    {{ historyItem.date }}
                  </span>
                  <TheWeather :forecast="historyItem" />
                </div>
              </div>
            </div>
          </b-tab-item>
        </b-tabs>
      </section>
    </div>
  </div>
</template>

<script>
import api from '@/data/api.json'
import axios from 'axios'
import moment from 'moment'
import { eventBus } from '@/store'
import 'flag-icon-css/css/flag-icon.min.css'

const defaultSettings = {
  duration: 5000,
  position: 'is-bottom'
}

export default {
  components: {
    TheWeather: () => import('@/components/TheWeather')
  },
  data () {
    return {
      isLoading: false,
      result: null,
      resultHistory: null,
      searchValue: '',
      urlIcon: api.urlIcon
    }
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
  created () {
    // receives value when event $emit is triggered
    eventBus.$on('change', (query, value, country) => {
      if (!value) return

      this.searchValue = value
      this.isLoading = true

      if (!country) country = api.default_country

      axios
        .get(`${api.url}?${query}=${value}&countryCode=${country}`)
        .then(response => {
          this.isLoading = false

          if (response.status !== 200) {
            this.warning('Unexpected error')
            return
          }

          if (response.data.hasOwnProperty('Message')) {
            // clean what was defined
            this.result = null

            this.warning(response.data.Message)
          } else {
            this.result = response.data

            this.processToday()
          }
        })
        .catch(error => {
          this.result = null
          this.isLoading = false
          this.error(error)
        })
    })
  },
  methods: {
    error (data) {
      let message = ''

      if (typeof data === 'object') {
        message = data.message
      } else {
        message = data
      }

      this.danger(message)
    },
    moment (value) {
      return moment(value)
    },
    processToday () {
      if (!this.result && !this.result.city) return

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
    },
    danger (message) {
      this.$toast.open({
        ...defaultSettings,
        ...{
          message,
          type: 'is-danger'
        }
      })
    },
    warning (message) {
      this.$toast.open({
        ...defaultSettings,
        ...{
          message,
          type: 'is-warning'
        }
      })
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
    .other-days {
      @media screen and (min-width: 1088px) and (max-width: 1279px) {
        .box {
          padding: 0.5rem;
        }
      }
    }
    .table-days {
      .info-text {
        font-size: 14px;
      }
    }
    .img-weather {
      width: 40px;
      height: 40px;
      margin-bottom: -10px;
    }
    .column {
      text-align: center;
      .img-icon-weather {
        & + span {
          display: block;
        }
      }
    }
    .column-middle {
      align-self: center;
    }
    .other-days, .is-history-list {
      flex-wrap: wrap;
      & > .column {
        flex: 1 0 25%;
        flex-grow: 0;
      }
    }
    .tabs.is-toggle {
      .is-active {
        a {
          background-color: #66c2d0;
          border-color: #66c2d0;
        }
      }
    }
  }
}
</style>
