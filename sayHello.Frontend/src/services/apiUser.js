import { addEntity } from "./BaseApi";

//const API_URL = import.meta.env.API_URL;
export async function handleCheckUserByEmailAndPassword(Email, Password) {
  console.log(`${Email} - ${Password}`);
  try {
    const res = await fetch(
      `https://localhost:7201/Users/findByEmailAndPassword/${Email}/${Password}`
    );

    if (res.status === 404) {
      console.warn("No data found for the given email and password.");
      return null;
    }

    if (res.ok) {
      const data = await res.json();
      return data;
    }

    throw new Error(`Unexpected response: ${res.status}`);
  } catch (error) {
    console.error("Error fetching user by email and password:", error);
    throw error;
  }
}

export const addUser = async (user) => {
  console.log("Calling addUser with:", user);
  return await addEntity(user, "Users");
};

export async function handleConfirmationEmail(email) {
  try {
    const res = await fetch(
      `https://localhost:7201/Users/send-confirmation?email=${encodeURIComponent(
        email
      )}`, // Ensure the email is part of the URL query
      {
        method: "POST", // Send as POST request
        headers: {
          Accept: "*/*", // Accept all response types (can be adjusted)
        },
      }
    );

    if (res.status === 404) {
      console.warn("The Email is not confirmed.");
      return false;
    }

    if (res.ok) {
      const data = await res.text(); // Since the server sends plain text, not JSON
      return data === "Confirmation email sent!";
    }

    throw new Error(`Unexpected response: ${res.status}`);
  } catch (error) {
    console.error("Error fetching Confirmation Email:", error);
    throw error;
  }
}
