import { Check } from "lucide-react";
import { useState } from "react";
import { createUserType } from "../../services/userTypeService";
import useUserTypesStore from "../../store/UserTypesStore";
import Input from "./Input";

interface Props {
    setCreateUserType: (createUserType: boolean) => void;
}

export default function CreateType({setCreateUserType}: Props) {

    const {addUserType} = useUserTypesStore();
    const [name, setName] = useState("");

    async function handleAddUserType() {
        if (!name.trim()) {
          setName("");
          return alert("Preencha o nome do tipo de usuário!");
        }
        const reponse = await createUserType(name);
    
        if (reponse.statusCode == 201) {
          addUserType({id: reponse.data.id, name: reponse.data.name});
        }
      }

    return (
        <div className="space-x-2 flex">
            <Input type="text" placeholder="Nome do tipo de usuário" value={name} onChange={(e) => setName(e.target.value)}/>
            <button onClick={() => {
                handleAddUserType();
                setCreateUserType(false);
            }} className="bg-[#50FA7B] p-2 rounded-md text-[#282A36] cursor-pointer"><Check /></button>
        </div>
    )
}

