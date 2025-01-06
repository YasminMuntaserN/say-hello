import { addEntity, DeleteBy, getAllBy, getCount } from "./BaseApi";

export const addGroup = async (group) => await addEntity(group, "Group");

export const addGroupMember = async (groupMember) =>
  await addEntity(groupMember, "GroupMember");

export const getAllGroups = async (id) => await getAllBy("Group", id);

export const getAllGroupMembers = async (id) =>
  await getAllBy("GroupMember", id);

export const getGroupsCount = async (id) => await getCount("GroupMember", id);

export const deleteGroupMember = async (id) =>
  await DeleteBy("GroupMember/deleteGroupMember", id);
