import { apiClient } from "./apiClient";

// Helper function to handle response based on content type
const handleResponse = async (response) => {
  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText);
  }

  const contentType = response.headers.get("content-type");
  if (contentType?.includes("application/json")) {
    return response.json();
  } else if (contentType?.includes("text/plain")) {
    return response.text();
  }
};

export async function addEntity(entityData, entityName) {
  try {
    let options = {
      method: "POST",
    };

    if (entityName === "Users") {
      options.headers = { Accept: "text/plain" };
      options.body = entityData;
    } else if (entityName === "Group") {
      options.headers = { Accept: "application/json" };
      options.body = entityData;
    } else if (entityName === "GroupMember") {
      const formData = new FormData();
      formData.append("groupId", entityData.groupId);
      formData.append("userId", entityData.userId);
      options.headers = { Accept: "text/plain" };
      options.body = formData;
    } else {
      options.headers = {
        "Content-Type": "application/json",
        Accept: "application/json",
      };
      options.body = JSON.stringify(entityData);
    }

    const response = await apiClient(entityName, options);
    return handleResponse(response);
  } catch (error) {
    console.error(`Error in addEntity for ${entityName}:`, error);
    throw error;
  }
}

export async function getBy(entityName, type, value) {
  try {
    const response = await apiClient(`${entityName}/${type}/${value}`);
    return handleResponse(response);
  } catch (error) {
    console.error(`Error fetching ${entityName}:`, error);
    throw error;
  }
}

export async function getAll(entityName) {
  try {
    const response = await apiClient(`${entityName}/all`);
    return handleResponse(response);
  } catch (error) {
    console.error(`Error fetching All ${entityName}:`, error);
    throw error;
  }
}

export async function getAllBy(entityName, value) {
  try {
    const response = await apiClient(`${entityName}/all/${value}`);
    return handleResponse(response);
  } catch (error) {
    console.error(`Error fetching All ${entityName}:`, error);
    throw error;
  }
}

export async function EditEntity(entityName, entityData, id) {
  try {
    let options = {
      method: "PUT",
    };

    if (entityName === "Users/updateUser") {
      options.body = entityData;
    } else {
      options.headers = {
        "Content-Type": "application/json",
        Accept: "application/json",
      };
      options.body = JSON.stringify(entityData);
    }

    const response = await apiClient(`${entityName}/${id}`, options);
    return handleResponse(response);
  } catch (error) {
    console.error(`Error Updating ${entityName}:`, error);
    throw error;
  }
}

export async function getCount(entityName, value) {
  try {
    const response = await apiClient(`${entityName}/count?id=${value}`);
    const data = await handleResponse(response);
    return parseInt(data, 10);
  } catch (error) {
    console.error(`Error fetching count of ${entityName}:`, error);
    throw error;
  }
}

export async function IsExist(entityName, value) {
  try {
    const response = await apiClient(`${entityName}/${value}`);
    const data = await handleResponse(response);
    return data;
  } catch (error) {
    console.error(`Error checking if the ${entityName} Exists:`, error);
    throw error;
  }
}

export async function DeleteBy(entityName, value) {
  try {
    const response = await apiClient(`${entityName}/${value}`, {
      method: "DELETE",
    });
    const data = await handleResponse(response);
    return data === "true";
  } catch (error) {
    console.error(`Error Delete ${entityName} :`, error);
    throw error;
  }
}

export async function FindBy(entityName, findBy, values) {
  try {
    const response = await apiClient(`${entityName}/findBy${findBy}/${values}`);

    if (response.status === 404) {
      console.warn(`No data found for the given ${findBy}`);
      return null;
    }

    return handleResponse(response);
  } catch (error) {
    console.error(`No data found for the given ${findBy}`, findBy);
    throw error;
  }
}
