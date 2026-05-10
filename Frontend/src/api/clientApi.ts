const VITE_API_URL = import.meta.env.VITE_API_URL;

export const apiClient = async (
  endpoint: string,
  options: RequestInit = {}
) => {
   try {
    const response = await fetch(`${VITE_API_URL}${endpoint}`, {
      ...options,
      headers: {
        "Content-Type": "application/json",
        ...(options.headers || {}),
      },
    });

    const responseText = await response.text();

    try {
      return JSON.parse(responseText);
    } catch (e) {
      return responseText;
    }
  } catch (error) {
    console.error("API request failed:", error);
    return null;
  }
};