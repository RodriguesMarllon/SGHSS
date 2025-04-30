import type IUser from "../interfaces/User";
import type { ResponseBase } from "../types/responseBase";
import { api } from "./api";

export async function createUser(user: Omit<IUser, "id">) {
  await api.post("/User/create", user);
}

export async function getAllUser(): Promise<ResponseBase<IUser[]>> {
  const response = await api.get<ResponseBase<IUser[]>>("/User/get-all");
  return response.data;
}
