import { PencilIcon, TrashIcon } from "lucide-react";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { DeleteUserType, GetAllUserTypes } from "../../services/userTypeService";
import useUserTypesStore from "../../store/UserTypesStore";
import Button from "./Button";
import CreateType from "./CreateType";

export default function Types() {
  
  const {userType, setUserType, removeUserType} = useUserTypesStore();
  const [createUserType, setCreateUserType] = useState(false);

  const navigate = useNavigate();

  function onEditUserTypeClick(type: { Id: string; Name: string }) {
    const query = new URLSearchParams();
    query.set("name", type.Name);
    query.set("id", type.Id);
    navigate(`/editusertype?${query}`);
  }

  async function handleDeleteUserType(userTypeId: string) {
    const response = await DeleteUserType(userTypeId);
    if (response.statusCode == 200) {
      removeUserType(userTypeId);
    }
  }

  useEffect(() => {
    async function loadUser() {
      const response = await GetAllUserTypes();
      if (response.statusCode == 200) 
      {
        setUserType(response.data);
      }
    }

    loadUser();
  }, [setUserType]);

  return (
    <ul className="space-y-4 p-6 bg-[#44475A] rounded-md shadow">      
      
      {userType.length == 0 && (
        <h1 className="text-center text-slate-700 font-bold">Nenhum tipo de usuário encontrado</h1>
      )}
      
      {userType.map((type) => (
        <li key={type.id} className="flex gap-2">
          <button
            className={`bg-slate-200 text-left w-full text-[#0b0b0c] p-2 rounded-md `}
          >
            {type.name}
          </button>
          <button
            onClick={() => {
              onEditUserTypeClick({Id: type.id, Name: type.name});
            }}
            className="bg-[#F1FA8C] p-2 rounded-md text-[#282A36] cursor-pointer"
          >
            <PencilIcon />
          </button>
          <button
            onClick={() => handleDeleteUserType(type.id)}
            className="bg-[#FF5555] p-2 rounded-md text-[#282A36] cursor-pointer"
          >
            <TrashIcon />
          </button>
        </li>
      ))}

      {createUserType ? (
        <CreateType setCreateUserType={setCreateUserType} />
      ): <Button onClick={() => 
        {
        setCreateUserType(true)
        }}>Criar novo tipo de usuário</Button>}

    </ul>
  );
}
