export function setStoredUser(user) {
  localStorage.setItem("user", JSON.stringify(user));
}

export function getStoredUser() {
  const user = localStorage.getItem("user");
  return user ? JSON.parse(user) : null;
}

export function removeStoredUser() {
  localStorage.removeItem("user");
}
