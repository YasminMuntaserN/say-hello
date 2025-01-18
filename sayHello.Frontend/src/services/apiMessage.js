import { apiClient } from "./apiClient";
import { addEntity, getAllBy } from "./BaseApi";

export const addMessage = async (message) =>
  await addEntity(message, "Messages");

export const getMessagesInChatRoom = async ({ senderId, receiverId }) => {
  try {
    const res = await apiClient(`Messages/all/${senderId}/${receiverId}`);

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

export const GetMessagesInChatRoomForGroup = async (groupId) =>
  await getAllBy("Messages", `${groupId}`);
