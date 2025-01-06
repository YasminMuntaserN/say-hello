import { useEffect } from "react";
import { useAllGroupMembers } from "../Groups/hooks/useGroupMember";
import GroupMemberCard from "./GroupMemberCard";
import Line from "../../ui/Line";
import { useChat } from "../../context/UserContext";
import SpinnerMini from "../../ui/SpinnerMini";
import { useGroup } from "../../context/GroupContext";

function GroupMembers({ groupId, setGroupMemberId ,setGroupMembers}) {
  const { user } = useChat();
  const {updateGroupMembers}=useGroup();
  const { mutate, isLoading, error, AllGroupMembers } = useAllGroupMembers();

  useEffect(() => {
    mutate(groupId);
  }, [mutate, groupId ,updateGroupMembers ]);

  useEffect(() => {
    if (AllGroupMembers && AllGroupMembers.length > 0) {
      setGroupMembers(AllGroupMembers);
      const groupMember = AllGroupMembers.find((member) => member.userId === user.userId);
      if (groupMember) {
        setGroupMemberId(groupMember.id);
      }
    }
  }, [setGroupMembers,AllGroupMembers, user, setGroupMemberId]);

  if (error) {
    return <p>Error loading group members: {error.message}</p>;
  }

  return (
    <>
      <Line />
      <p className="text-[#248d3a] text-xl ml-10 font-semibold my-5">
        {AllGroupMembers?.length || 0} participants in this group
      </p>
      <div className="flex-grow overflow-y-auto max-h-[200px] h-full">
        {isLoading ? (
          <SpinnerMini />
        ) : (
          AllGroupMembers?.map((member) => (
              member.userId !== user.userId && <GroupMemberCard key={member.id} member={member} />
          ))
        )}
      </div>
    </>
  );
}

export default GroupMembers;
