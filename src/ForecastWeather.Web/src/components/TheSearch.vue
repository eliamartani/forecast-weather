<template>
  <div>
    <b-autocomplete
    v-model="city"
    :data="filterAutocomplete"
    :keep-first="true"
    :loading="isLoading"
    @input="getWrittenData"
    icon="magnify"
    ref="autocomplete"
    placeholder="Type a City name or Postal code"
    field="city">
      <template slot-scope="props">
        <span class="text-city">
          {{ props.option.city }}
        </span>
        <span class="text-admin has-text-grey-light">
          {{ props.option.admin }}
        </span>
      </template>
    </b-autocomplete>
  </div>
</template>

<script>
import dataCities from '../data/cities-de.json'
import { removeDiacritics } from '../mixins/string'
import { serverBus } from '../store'

export default {
  data () {
    return {
      city: '',
      data: dataCities,
      isLoading: false,
      timerId: null
    }
  },
  computed: {
    filterAutocomplete () {
      if (!this.city) return

      // compares normalized string
      const city = removeDiacritics(this.city)

      return this.data.filter(option => option.city_normalized.indexOf(city) > -1)
    }
  },
  methods: {
    getWrittenData (data) {
      // clear timer before calling API
      window.clearTimeout(this.timerId)

      // accept only more than three characters to search
      if (!data || data.length < 3) return

      // shows loading icon
      this.isLoading = true

      this.timerId = window.setTimeout(() => {
        // get country code if provided
        let [ value, countryCode ] = data.split(',')

        // if true, considers zip code
        const isNumeric = !isNaN(Number(value))

        // differs how API will search
        const query = isNumeric ? 'zipcode' : 'city'

        if (!countryCode) countryCode = ''

        // triggers the API
        serverBus.$emit('change', query, value, countryCode.trim())

        // hide loading icon
        this.isLoading = false
      }, 1000)
    }
  }
}
</script>

<style lang="less">
  #app {
    .autocomplete {
      .control {
        .input {
          border: 1px solid #EAEBEE;
          box-shadow: none;
          font-size: 20px;
          padding-left: 2em;
          padding-top: 0.3rem;
          transition: all .2s linear;
          &:focus {
            border-color: transparent;
            box-shadow: 0 2px 30px rgba(55, 72, 93, 0.3);
          }
        }
        &.is-loading {
          &::after {
            height: 1.5em;
            width: 1.5rem;
          }
        }
        &.has-icons-left {
          .icon.is-left {
            left: 0.2rem;
            height: 2.8rem;
          }
        }
      }
    }
    .dropdown-menu {
      .dropdown-item {
        .text-admin {
          margin-left: 10px;
        }
      }
    }
  }
</style>
