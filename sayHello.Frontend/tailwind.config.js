/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        sans: ["Titillium Web", "sans-serif"],
        regular: ["Titillium Web", "sans-serif"],
        semibold: ["Titillium Web", "sans-serif"],
      },
      colors: {
        basic: "#091d2f",
        secondary: "#513c5a",
        lightText: "#818388",
        purple: "#560D49",
        gray: "#e5e7eb",
        blue: "#083869",
      },
      backgroundImage: {
        "gradient-bg": "linear-gradient(to right, #280B31, #04213D)",
        "gradient-btn": "linear-gradient(to top, #632969,#9D38D2, #2E8DEC)",
      },
    },
  },
  plugins: [],
};
