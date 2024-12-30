export function setStoredUser(user) {
  localStorage.setItem("user", JSON.stringify(user));
}

export function getStoredUser() {
  const user = localStorage.getItem("user");
  try {
    return user ? JSON.parse(user) : null;
  } catch (error) {
    console.error("Invalid user data in localStorage", error);
    return null;
  }
}

export function removeStoredUser() {
  localStorage.removeItem("user");
}
