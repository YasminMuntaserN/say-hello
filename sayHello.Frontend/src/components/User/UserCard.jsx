import { MdEmail } from "react-icons/md";
import Image from "../../ui/Image";

function UserCard({user ,withBio=false}) {
  console.log(user);  
  return (
    <>
    <div className="h-20 bg-lightText relative" />
      <div className={`border-light-100 absolute ${!withBio ? "top-10 left-10" :""}`}>
          <Image src={user?.profilePictureUrl} size="w-20 h-20" />
          <p className="text-secondary font-bold text-2xl mt-1">
            {user?.username}
          </p>
          <p className="text-basic font-light text-sm flex justify-items-center ml-2">
          <MdEmail className="mt-1" />{user?.email}
          </p>
          {withBio && <p>{user?.bio}</p>}
      </div>
    </>
  )
}

export default UserCard
