import axios from "axios";
import { getUserInfo, saveUserInfo } from "./storage";

const api = axios.create({
  baseURL: "https://parcel-delivery-system-ng.herokuapp.com/api",
});

const defaultPageLimit = 10;

export const login = async (username: string, password: string) => {
  return await api.post("/auth/login", {
    username,
    password,
  });
};

export const register = async (
  username: string,
  password: string,
  email: string
) => {
  return await api.post("/auth/register", {
    username,
    password,
    email,
  });
};

export const logout = async () => {
  const token = getUserInfo()?.token;
  saveUserInfo(null);
  if (!token) return Promise.reject();

  await api.get("/auth/logout", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
};

export const getProfile = async () => {
  const token = getUserInfo()?.token;
  if (!token) return Promise.reject();

  const res = await api.get("/auth/me", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const getUsers = async (
  page: number,
  perPage: number = defaultPageLimit,
  extra = ""
) => {
  const token = getUserInfo()?.token;
  const res = await api.get(`/users?page=${page}&size=${perPage}${extra}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    saveUserInfo(null);
    return Promise.reject();
  }

  return res;
};

export const createCar = async (data: any) => {
  const token = getUserInfo()?.token;
  const res = await api.post(`/cars`, data, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (res.status === 401) {
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
    saveUserInfo(null);
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
