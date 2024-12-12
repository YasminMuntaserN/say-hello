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

export const addUser = async (user) => await addEntity(user, "Users");
