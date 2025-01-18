const API_URL = import.meta.env.VITE_API_URL;

const storeTokens = (accessToken, refreshToken) => {
  localStorage.setItem('accessToken', accessToken);
  localStorage.setItem('refreshToken', refreshToken);
};


const removeTokens = () => {
  localStorage.removeItem('accessToken');
  localStorage.removeItem('refreshToken');
};


const getStoredTokens = () => ({
  accessToken: localStorage.getItem('accessToken'),
  refreshToken: localStorage.getItem('refreshToken'),
});


const refreshAccessToken = async () => {
  try {
    const { refreshToken } = getStoredTokens();
    if (!refreshToken) throw new Error('No refresh token available');

    const response = await fetch(`${API_URL}/Auth/refresh`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ refreshToken }),
    });

    if (!response.ok) throw new Error('Failed to refresh token');

    const data = await response.json();
    storeTokens(data.accessToken, data.refreshToken);
    return data.accessToken;
  } catch (error) {
    removeTokens();
    throw error;
  }
};

const login = async (email, password) => {
  const response = await fetch(`${API_URL}/Auth/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({email, password }),
  });

  if (!response.ok) {
    throw new Error('Login failed');
  }

  const data = await response.json();
  storeTokens(data.accessToken, data.refreshToken);
  return data;
};


const logout = async () => {
  try {
    const { accessToken } = getStoredTokens();
    if (accessToken) {
      await fetch(`${API_URL}/Auth/revoke`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${accessToken}`,
        },
      });
    }
  } finally {
    removeTokens();
  }
};

export { login, logout, getStoredTokens, refreshAccessToken };