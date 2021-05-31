// eslint-disable-next-line @typescript-eslint/no-var-requires
const defaultTheme = require('tailwindcss/defaultTheme');

module.exports = {
  purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: 'class', // or 'media' or 'class'
  theme: {
    fontFamily: {
      sans: ['Inter', ...defaultTheme.fontFamily.sans],
    },
    extend: {
      colors: {
        red: '#FF3146',
        darkred: '#E51A2E',
        lightred: '#FFDADE',
        success: '#17BF63',
        danger: '#E0245E',
        'gray-100': '#DEE3EA',
        'gray-200': '#B2BDCD',
        'gray-300': '#5D7290',
        'gray-700': '#242C37',
        'gray-800': '#151A21',
        'gray-900': '#0B0E11',
      },
    },
  },
  variants: {
    extend: {
      display: ['group-hover'],
    },
  },
  plugins: [],
};
