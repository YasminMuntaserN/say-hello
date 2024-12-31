import { addEntity, getAllBy } from "./BaseApi";

//https://localhost:7201/Group
export const addGroup = async (group) => await addEntity(group, "Group");

export const addGroupMember = async (groupMember) =>
  await addEntity(groupMember, "GroupMember");

//https://localhost:7201/Group/all/2
export const getAllGroups = async (id) => await getAllBy("Group", id);
