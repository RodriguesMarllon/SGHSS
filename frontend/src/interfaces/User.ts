import type { UserType } from "../services/userTypeService";

export default interface IUser {
  id: string;
  name: string;
  email: string;
  password: string;
  userType: UserType;
  isActive: boolean;
  createdAt: Date;
}
