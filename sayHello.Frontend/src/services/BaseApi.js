const API_URL = import.meta.env.VITE_API_URL;

export async function addEntity(entityData, entityName) {
  try {
    let response;
    if (entityName === "Users") {
      // Handle Users
      response = await fetch(`${API_URL}/${entityName}`, {
        method: "POST",
        headers: { Accept: "text/plain" },
        body: entityData,
      });
    } else if (entityName === "Group") {
      // Handle Groups
      response = await fetch(`${API_URL}/${entityName}`, {
        method: "POST",
        headers: { Accept: "application/json" },
        body: entityData,
      });
    } else if (entityName === "GroupMember") {
      // Handle GroupMember
      const formData = new FormData();
      formData.append("groupId", entityData.groupId);
      formData.append("userId", entityData.userId);
      response = await fetch(`${API_URL}/${entityName}`, {
        method: "POST",
        headers: { Accept: "text/plain" },
        body: formData,
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

export async function getBy(entityName, type, value) {
  try {
    const res = await fetch(`${API_URL}/${entityName}/${type}/${value}`);

    if (!res.ok) {
      throw new Error(`Failed to fetch ${entityName}`);
    }

    const data = await res.json();

    return data;
  } catch (error) {
    console.error(`Error fetching ${entityName}:`, error);
    throw error;
  }
}

export async function getAll(entityName) {
  try {
    const res = await fetch(`${API_URL}/${entityName}/all`);

    if (!res.ok) {
      throw new Error(`Failed to fetch All ${entityName}`);
    }

    const data = await res.json();

    return data;
  } catch (error) {
    console.error(`Error fetching All ${entityName}:`, error);
    throw error;
  }
}

export async function getAllBy(entityName, value) {
  try {
    const res = await fetch(`${API_URL}/${entityName}/all/${value}`);

    if (!res.ok) {
      throw new Error(`Failed to fetch All ${entityName}`);
    }

    const data = await res.json();

    return data;
  } catch (error) {
    console.error(`Error fetching All ${entityName}:`, error);
    throw error;
  }
}

export async function EditEntity(entityName, entityData, id) {
  try {
    let response;
    if (entityName === "Users/updateUser") {
      response = await fetch(`${API_URL}/${entityName}/${id}`, {
        method: "PUT",
        body: entityData,
      });
    } else {
      response = await fetch(`${API_URL}/${entityName}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(entityData),
      });
    }
    if (response.ok) {
      const data = await response.json();

      return data;
    } else {
      const errorText = await response.text();
      throw new Error(`Error Updating : ${errorText}`);
    }
  } catch (error) {
    console.error(`Error Updating ${entityName}:`, error);
    throw error;
  }
}

export async function getCount(entityName, value) {
  try {
    const res = await fetch(`${API_URL}/${entityName}/count?id=${value}`);

    if (!res.ok) {
      throw new Error(`Failed to fetch count of ${entityName}`);
    }

    const data = await res.text();
    return parseInt(data, 10);
  } catch (error) {
    console.error(`Error fetching count of ${entityName}:`, error);
    throw error;
  }
}

export async function IsExist(entityName, value) {
  try {
    const res = await fetch(`${API_URL}/${entityName}/${value}`);

    if (!res.ok) {
      throw new Error(`Failed to check if the ${entityName} Exists`);
    }

    const data = await res.text();
    return data === "true";
  } catch (error) {
    console.error(`Error checking  if the ${entityName} Exists:`, error);
    throw error;
  }
}

export async function DeleteBy(entityName, value) {
  try {
    const res = await fetch(`${API_URL}/${entityName}/${value}`, {
      method: "DELETE",
    });

    if (!res.ok) {
      throw new Error(`Failed to Delete ${entityName}`);
    }

    const data = await res.text();
    return data === "true";
  } catch (error) {
    console.error(`Error Delete ${entityName} :`, error);
    throw error;
  }
}

export async function FindBy(entityName, findBy, values) {
  try {
    const res = await fetch(
      `https://localhost:7201/${entityName}/findBy${findBy}/${values}`
    );

    if (res.status === 404) {
      console.warn(`No data found for the given ${findBy}`);
      return null;
    }

    if (res.ok) {
      const data = await res.json();
      return data;
    }

    throw new Error(`Unexpected response: ${res.status}`);
  } catch (error) {
    console.error("No data found for the given ${findBy}", findBy);
    throw error;
  }
}
