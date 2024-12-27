import {
  addEntity,
  DeleteBy,
  EditEntity,
  getAll,
  getAllBy,
  getBy,
  getCount,
  IsExist,
} from "./BaseApi";

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

export const getAllBlockedUsersCount = async (id) =>
  await getCount("BlockedUsers", id);

export const getAllBArchivedUsersCount = async (id) =>
  await getCount("ArchivedUsers", id);

//https://localhost:7201/BlockedUsers/all/14
export const getAllBlockedUsers = async (id) =>
  await getAllBy("BlockedUsers", id);

export const getAllBArchivedUsers = async (id) =>
  await getAllBy("ArchivedUsers", id);

export const editUser = async (user, id) =>
  await EditEntity("Users/updateUser", user, id);

export const isBlockedUser = async (BlockedUserId, BlockedByUserId) =>
  await IsExist(
    "BlockedUsers/BlockedUserExists",
    `${BlockedUserId}/${BlockedByUserId}`
  );

export const isArchivedUser = async (ArchivedUserId, ArchivedByUserId) =>
  await IsExist(
    "ArchivedUsers/ArchivedUserExists",
    `${ArchivedUserId}/${ArchivedByUserId}`
  );

//https://localhost:7201/ArchivedUsers/deleteArchivedUser/17/14
export const DeleteBlockedUser = async ({ BlockedUserId, BlockedByUserId }) =>
  await DeleteBy(
    "BlockedUsers/deleteBlockedUser",
    `${BlockedUserId}/${BlockedByUserId}`
  );

export const DeleteArchivedUser = async ({
  ArchivedUserId,
  ArchivedByUserId,
}) =>
  await DeleteBy(
    "ArchivedUsers/deleteArchivedUser",
    `${ArchivedUserId}/${ArchivedByUserId}`
  );
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
