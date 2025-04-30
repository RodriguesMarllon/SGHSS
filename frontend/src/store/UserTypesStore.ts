import { create } from "zustand";
import { UserType } from "../services/userTypeService";

type UserTypesStore = {
    userType: UserType[];
    addUserType: (userType: UserType) => void;
    setUserType: (userType: UserType[]) => void;
    removeUserType: (userTypeId: string) => void;
    editUserType: (userTypeId: string, userType: UserType) => void;
}

const useUserTypesStore = create<UserTypesStore>((set) => {
    return {
        userType: [],
        setUserType: (userType: UserType[]) => set({ userType }),
        addUserType: (userType: UserType) => set((state) => ({ userType: [...state.userType, userType] })),
        removeUserType: (userTypeId: string) => set((state) => ({ userType: state.userType.filter((type) => type.id !== userTypeId) })),
        editUserType: (userTypeId: string, userType: UserType) => set((state) => ({ userType: state.userType.map((type) => type.id === userTypeId ? userType : type) })),
    }
});

export default useUserTypesStore;
