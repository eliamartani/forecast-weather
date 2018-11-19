export const removeDiacritics = (value) => {
  if (!value) return ''

  let returnValue = value.toLowerCase()
  const nonAsciis = {
    'a': '[àáâãäå]',
    'ae': 'æ',
    'c': 'ç',
    'e': '[èéêë]',
    'i': '[ìíîï]',
    'n': 'ñ',
    'o': '[òóôõö]',
    'oe': 'œ',
    'u': '[ùúûűü]',
    'y': '[ýÿ]'
  }

  for (const i in nonAsciis) {
    returnValue = returnValue.replace(new RegExp(nonAsciis[i], 'g'), i)
  }

  return returnValue
}
