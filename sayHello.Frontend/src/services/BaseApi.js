const API_URL = import.meta.env.VITE_API_URL;

export async function addEntity(entityData, entityName) {
  try {
    let response;
    if (entityName === "Users") {
      console.log(`Adding user with data:`, entityData);
      response = await fetch("https://localhost:7201/Users", {
        method: "POST",
        headers: { Accept: "text/plain" },
        body: entityData,
      });
    } else {
      response = await fetch(`${API_URL}/${entityName}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(entityData),
      });
    }

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`${errorText}`);
    }

    const contentType = response.headers.get("content-type");
    if (contentType && contentType.includes("application/json")) {
      return await response.json();
    } else if (contentType && contentType.includes("text/plain")) {
      return await response.text();
    }
  } catch (error) {
    console.error(`Error in addEntity for ${entityName}:`, error);
    throw error;
  }
}
