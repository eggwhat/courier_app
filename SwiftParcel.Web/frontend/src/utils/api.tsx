import axios from "axios";
import { getUserIdFromStorage, getUserInfo, saveUserInfo } from "./storage";


const API_BASE_URL = 'http://localhost:6001';


const api = axios.create({
  baseURL: "http://localhost:5292",
  withCredentials: true,
});

api.interceptors.response.use(
  response => response,
  error => {
    if (axios.isAxiosError(error) && error.response?.status === 401) {
      //// saveUserInfo(null);
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

const getAuthHeader = () => {
  const userInfo = getUserInfo();
  if (!userInfo || !userInfo.accessToken) {
    console.warn('No user token found. Redirecting to login.');
    window.location.href = '/login';
    return null;
  }
  console.log({Authorization: `Bearer ${userInfo.accessToken}`})
  return { Authorization: `${userInfo.accessToken}` };
};

const defaultPageLimit = 10;

export const login = async (email: string, password: string) => {
  try {
    const response = await api.post('/identity/sign-in', { email, password });
    const { accessToken, refreshToken, role, expires } = response.data;

    saveUserInfo({ token: accessToken, refreshToken, role, expires }); 
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error('Error during login:', error.response?.data || error.message);
    } else {
      console.error('Error during login:', error);
    }
    throw error;
  }
};

export const register = async (
  username: string,
  password: string,
  email: string
) => {
  try {
    const response = await api.post(`/identity/sign-up`, {
      username,
      password,
      email,
    });
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error('Error during registration (Axios error):', error.response?.data || error.message);
    } else {
      console.error('Error during registration:', error);
    }
    throw error;
  }
};

export const logout = async () => {
  try {
    const headers = getAuthHeader();
    await api.get('/identity/logout', { headers });
  } catch (error) {
    console.error('Logout failed:', error);
  } finally {
    saveUserInfo(null);
    window.location.href = '/login';
  }
};

export const getProfile = async () => {
  try {
    const headers = getAuthHeader();
    const userId = getUserIdFromStorage(); 
    if (!userId) {
      throw new Error('User ID not found');
    }

    const response = await api.get(`identity/users/${userId}`, { headers });
    console.log("identity/users/${userId}: ", response.data)
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error('Error fetching profile:', error.response?.data || error.message);
      if (error.response?.status === 401) {
        saveUserInfo(null);
        window.location.href = '/login';
      }
    } else {
      console.error('Error fetching profile:', error);
    }
    throw error;
  }
};


export const getUsers = async (
  page: number,
  perPage: number = defaultPageLimit,
  extra = ""
) => {

  // in the case for the test reason: using the backend in the node
  // const token = getUserInfo()?.token;
  // const res = await api.get(`/users?page=${page}&size=${perPage}${extra}`, {
  //   headers: {
  //     Authorization: `Bearer ${token}`,
  //   },
  // });

  const response = await api.get(`/users?page=${page}&size=${perPage}`, { headers: getAuthHeader() });

  if (response.status === 401) {
    //// saveUserInfo(null);
    return Promise.reject();
  }

  return response;
};

export const createInquiry = async (
  description: string,
  width: number,
  height: number,
  depth: number,
  weight: number,
  sourceStreet: string,
  sourceBuildingNumber: string,
  sourceApartmentNumber: string,
  sourceCity: string,
  sourceZipCode: string,
  sourceCountry: string,
  destinationStreet: string,
  destinationBuildingNumber: string,
  destinationApartmentNumber: string,
  destinationCity: string,
  destinationZipCode: string,
  destinationCountry: string,
  priority: string,
  atWeekend: boolean,
  pickupDate: string,
  deliveryDate: string,
  isCompany: boolean,
  vipPackage: boolean
) => {
  try {
    const response = await api.post(`/parcels`, {
      description,
      width,
      height,
      depth,
      weight,
      sourceStreet,
      sourceBuildingNumber,
      sourceApartmentNumber,
      sourceCity,
      sourceZipCode,
      sourceCountry,
      destinationStreet,
      destinationBuildingNumber,
      destinationApartmentNumber,
      destinationCity,
      destinationZipCode,
      destinationCountry,
      priority,
      atWeekend,
      pickupDate,
      deliveryDate,
      isCompany,
      vipPackage
    });
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error('Error during inquiry creation (Axios error):', error.response?.data || error.message);
    } else {
      console.error('Error during inquiry creation:', error);
    }
    throw error;
  }
};

export const getInquiries = async () => {
  try {
    const response = await api.get(` parcels-service/parcels`);
    return response.data;
  } catch (error) {
    console.error('Error during getting inquiries:', error);
    throw error;
  }
};

export const createCar = async (data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.post(`/cars`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const updateCar = async (carId: number, data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.put(`/cars/${carId}`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const deleteCar = async (carId: number) => {
  const token = getUserInfo()?.token;
  const res = await api.delete(`/cars/${carId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    //// saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getCar = async (carId: number) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/cars/${carId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getCarPersonal = async () => {
  const token = getUserInfo()?.token;
  const carId = getUserInfo()?.courier?.carId;

  if (!carId || !token) return Promise.reject();

  const res = await api.get(`/cars/${carId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getCars = async (
  page: number,
  perPage: number = defaultPageLimit,
  extra = ""
) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/cars?page=${page}&size=${perPage}${extra}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const createParcel = async (data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.post(`/parcels`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const updateParcel = async (parcelId: string, data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.put(`/parcels/${parcelId}`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const deleteParcel = async (parcelId: string) => {
  const token = getUserInfo()?.token;
  const res = await api.delete(`/parcels/${parcelId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getParcel = async (parcelId: string) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/parcels/${parcelId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getParcels = async (
  page: number,
  perPage: number = defaultPageLimit,
  extra = ""
) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/parcels?page=${page}&size=${perPage}${extra}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getParcelsForCourier = async (
  courierId: number,
  page: number,
  perPage: number = defaultPageLimit
) => {
  const token = getUserInfo()?.token;
  const res = await api.get(
    `/couriers/${courierId}/parcels?page=${page}&size=${perPage}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getParcelsForCar = async (
  carId: number,
  page: number,
  perPage: number = defaultPageLimit
) => {
  const token = getUserInfo()?.token;
  const res = await api.get(
    `/cars/${carId}/parcels?page=${page}&size=${perPage}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const createCourier = async (data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.post(`/couriers`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const updateCourier = async (courierId: number, data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.put(`/couriers/${courierId}`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const deleteCourier = async (courierId: number) => {
  const token = getUserInfo()?.token;
  const res = await api.delete(`/couriers/${courierId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getCourier = async (courierId: number) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/couriers/${courierId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getCouriers = async (page: number, perPage: number = 10) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/couriers?page=${page}&size=${perPage}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    // saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getCoordinates = async (address: string) => {
  const res = await axios.get(
    `https://nominatim.openstreetmap.org/search?q=${address}&format=json`
  );

  return res;
};
