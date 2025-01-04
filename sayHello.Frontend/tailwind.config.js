/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      extend: {
        animation: {
          l3: "l3 1s infinite linear",
        },
        keyframes: {
          l3: {
            "20%": { backgroundPosition: "0% 0%, 50% 50%, 100% 50%" },
            "40%": { backgroundPosition: "0% 100%, 50% 0%, 100% 50%" },
            "60%": { backgroundPosition: "0% 50%, 50% 100%, 100% 0%" },
            "80%": { backgroundPosition: "0% 50%, 50% 50%, 100% 100%" },
          },
        },
        spacing: {
          "loader-width": "60px",
        },
        aspectRatio: {
          loader: "2",
        },
      },
      fontFamily: {
        sans: ["Titillium Web", "sans-serif"],
        regular: ["Titillium Web", "sans-serif"],
        semibold: ["Titillium Web", "sans-serif"],
      },
      colors: {
        basic: "#091d2f",
        secondary: "#513c5a",
        lightText: "#818388",
        purple: "#632969",
        gray: "#e5e7eb",
        blue: "#083869",
      },
      backgroundImage: {
        "gradient-bg": "linear-gradient(to right, #280B31, #04213D)",
        "secondary-bg":
          "linear-gradient(to top ,#632969,#9D38D2, #04213D,#2E8DEC,#280B31)",
        "gradient-btn": "linear-gradient(to top, #632969,#9D38D2, #2E8DEC)",
        "gradient-message":
          "linear-gradient(to right, #632969,#9D38D2, #2E8DEC)",
      },
    },
  },
  plugins: [],
};
