import {
  addEntity,
  DeleteBy,
  EditEntity,
  FindBy,
  getAll,
  getAllBy,
  getBy,
  getCount,
  IsExist,
} from "./BaseApi";

export const handleCheckUserByEmailAndPassword = async (Email, Password) =>
  FindBy("Users", "EmailAndPassword", `${Email}/${Password}`);

export const handleCheckUserByEmail = async (Email) =>
  FindBy("Users", "Email", `${Email}`);

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
export const DeleteUser = async (id) => await DeleteBy("Users", id);

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

//'https://localhost:7201/Users/restorePassword/jsd%40dd.com'
export async function handleRestorePassword(email) {
  try {
    const res = await fetch(
      `https://localhost:7201/Users/restorePassword/${encodeURIComponent(
        email
      )}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
      }
    );

    if (res.ok) {
      const data = await res.json();
      return data;
    }

    if (res.ok) {
      const data = await res.json();
      return data;
    }

    if (res.status === 400) {
      const errorDetails = await res.text();
      throw new Error(`Bad Request: ${errorDetails}`);
    }

    throw new Error(`Unexpected response: ${res.status}`);
  } catch (error) {
    console.error("Error fetching Confirmation Email:", error);
    throw error;
  }
}

export async function ChangePassword(id, newPassword) {
  console.log(
    `https://localhost:7201/Users/changePassword/${id}?newPassword=${newPassword}`
  );
  try {
    const res = await fetch(
      `https://localhost:7201/Users/changePassword/${id}?newPassword=${newPassword}`,
      {
        method: "PUT",
      }
    );

    if (!res.ok) {
      throw new Error(`Failed to change password`);
    }

    const data = await res.text();
    return data === "true";
  } catch (error) {
    console.error(`Error Delete change password :`, error);
    throw error;
  }
}
