import { addEntity, EditEntity, getAll, getBy, getCount } from "./BaseApi";

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

export const addArchivedUser = async (user) =>
  await addEntity(user, "ArchivedUsers");

export const addBlockedUser = async (user) =>
  await addEntity(user, "BlockedUsers");

export const getAllBySenderId = async (senderId) =>
  await getBy("Messages", "allBySenderId", senderId);

export const getAllUsers = async () => await getAll("Users");

//https://localhost:7201/BlockedUsers/count?id=16
export const getAllBlockedUsersCount = async (id) =>
  await getCount("BlockedUsers", id);

export const getAllBArchivedUsersCount = async (id) =>
  await getCount("ArchivedUsers", id);

//https://localhost:7201/Users/updateUser/4
export const editUser = async (user, id) =>
  await EditEntity("Users/updateUser", user, id);

export async function handleConfirmationEmail(email) {
  try {
    const res = await fetch(
      `https://localhost:7201/Users/send-confirmation?email=${encodeURIComponent(
        email
      )}`,
      {
        method: "POST",
        headers: {
          Accept: "*/*",
        },
      }
    );

    if (res.status === 404) {
      console.warn("The Email is not confirmed.");
      return false;
    }

    if (res.ok) {
      const data = await res.text();
      return data === "Confirmation email sent!";
    }

    throw new Error(`Unexpected response: ${res.status}`);
  } catch (error) {
    console.error("Error fetching Confirmation Email:", error);
    throw error;
  }
}
