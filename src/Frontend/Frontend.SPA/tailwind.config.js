module.exports = {
  purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    fontFamily: {
      sans: ['Inter', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Roboto', 'Helvetica', 'Arial', 'sans-serif'],
    },
    extend: {
      colors: {
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
    extend: {},
  },
  plugins: [],
};
