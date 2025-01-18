import { getStoredTokens, refreshAccessToken } from "./authService";

const API_URL = import.meta.env.VITE_API_URL;

/**
 * API Client that handles authentication and token refresh
 * @param {string} endpoint - The API endpoint to call
 * @param {object} options - Fetch options (method, headers, body, etc.)
 * @returns {Promise<Response>} - Fetch response
 */
export const apiClient = async (endpoint, options = {}) => {
  // Get the current access token
  const { accessToken } = getStoredTokens();

  // If we have an access token, add it to the headers
  if (accessToken) {
    options.headers = {
      ...options.headers,
      Authorization: `Bearer ${accessToken}`,
    };
  }
  try {
    // Make the initial API call
    const response = await fetch(`${API_URL}/${endpoint}`, options);

    // If we get a 401 (Unauthorized) and we have an access token,
    // it means our token has expired. Try to refresh it.
    if (response.status === 401 && accessToken) {
      try {
        // Get a new access token
        const newAccessToken = await refreshAccessToken();

        // Update the Authorization header with the new token
        options.headers = {
          ...options.headers,
          Authorization: `Bearer ${newAccessToken}`,
        };

        // Retry the original request with the new token
        return fetch(`${API_URL}/${endpoint}`, options);
      } catch (refreshError) {
        // If refresh fails, the user needs to login again
        throw new Error("Session expired. Please login again.");
      }
    }

    return response;
  } catch (error) {
    throw error;
  }
};
