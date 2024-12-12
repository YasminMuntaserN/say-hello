const API_URL = import.meta.env.VITE_API_URL;

export async function addEntity(entityData, entityName) {
  console.log(entityData);
  try {
    let response;
    if (entityName === "Users") {
      response = await fetch("https://localhost:7201/Users", {
        method: "POST",
        headers: {
          Accept: "text/plain",
        },
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

    if (response.ok) {
      const contentType = response.headers.get("content-type");
      if (contentType && contentType.includes("application/json")) {
        const data = await response.json();
        return data;
      } else if (contentType && contentType.includes("text/plain")) {
        const data = await response.text();
        return data.split("/").pop();
      }
    } else {
      const errorText = await response.text();
      console.log("Errors:", errorText);
      return errorText.errors;
    }
  } catch (error) {
    console.error(`Error adding ${entityName}:`, error);
    if (error.errors) {
      console.log("Errors:", error.errors);
    }
    throw error;
  }
}
