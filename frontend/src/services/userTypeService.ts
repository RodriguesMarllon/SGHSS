import type { ResponseBase } from "../types/responseBase";
import { api } from "./api";

export interface UserType {
  id: string;
  name: string;
}

export async function createUserType(name: string) {
  const reponse = await api.post("/UserType/create", { Name: name });
  return reponse.data;
}

export async function GetAllUserTypes(): Promise<ResponseBase<UserType[]>> {
  const response = await api.get<ResponseBase<UserType[]>>("/UserType/get-all");
  return response.data;
}

export async function UpdateUserType(id: string, name: string): Promise<ResponseBase<UserType>> {
  const response = await api.put("/UserType/update", { Id: id, Name: name });
  return response.data;
}

export async function DeleteUserType(id: string): Promise<ResponseBase<UserType>> {
  const reponse = await api.delete("/UserType/delete", { params: { id } });
  return reponse.data;
}
