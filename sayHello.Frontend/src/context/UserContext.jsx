import { createContext, useContext } from "react";
import { useState } from "react";

const UserContext = createContext();

function UserInfoProvider({ children }) {
  const [user, setUser] = useState(null);

  const login = (userInfo) => {
    setUser(userInfo);
  };

  const logout = () => {
    setUser(null);
  };
  return (
    <UserContext.Provider value={{ user, login, logout }}>
      {children}
    </UserContext.Provider>
  );
}

function useUser() {
  const context = useContext(UserContext);
  if (context === undefined) {
    throw new Error("useUser must be used within a UserInfoProvider");
  }
  return context;
}

export { UserInfoProvider, useUser };
