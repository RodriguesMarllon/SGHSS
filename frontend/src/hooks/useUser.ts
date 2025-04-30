import { useEffect, useState } from "react";
import { createUser, getAllUser } from "../services/userService";
import type IUser from "../interfaces/User";

export function useUserTypes() {
  const [user, setUser] = useState<IUser[]>(() => {
    const stored = localStorage.getItem("user");
    return stored ? JSON.parse(stored) : [];
  });

  useEffect(() => {
    localStorage.setItem("user", JSON.stringify(user));
  }, [user]);

  useEffect(() => {
    async function loadUser() {
      const apiUsers = await getAllUser();
      if (apiUsers.data != null) {
        const mappedUsers = apiUsers.data.map((user) => ({
          ...user,
        }));
        setUser(mappedUsers);
      }
      console.log("apiUsers.data is null");
    }

    loadUser();
  }, []);

  function onAddUserSubmit(user: IUser) {
    createUser(user);
    setUser((prev) => [...prev, user]);
  }

  return {
    onAddUserSubmit,
  };
}
