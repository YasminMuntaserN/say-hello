import { addEntity, getAllBy } from "./BaseApi";

//https://localhost:7201/Messages
export const addMessage = async (message) =>
  await addEntity(message, "Messages");

//https://localhost:7201/Messages/all/16/17
export const getMessagesInChatRoom = async ({ senderId, receiverId }) => {
  // await getAllBy("Messages", `${senderId}/${receiverId}`);
  console.log(`https://localhost:7201/Messages/all/${senderId}/${receiverId}`);
  try {
    const res = await fetch(
      `https://localhost:7201/Messages/all/${senderId}/${receiverId}`
    );

    if (!res.ok) {
      throw new Error(`Failed to fetch All Messages`);
    }

    const data = await res.json();

    return data;
  } catch (error) {
    console.error(`Error fetching All Messages :`, error);
    throw error;
  }
};
