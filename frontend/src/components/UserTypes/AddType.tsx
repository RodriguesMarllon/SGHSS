import { useState } from "react";
import Input from "./Input";
import Button from "./Button";
import useUserTypesStore from "../../store/UserTypesStore";
import { createUserType } from "../../services/userTypeService";


export default function AddType() {
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
    <div className="space-y-4 p-6 bg-slate-200 rounded-md shadow flex flex-col">
      <Input
        type="text"
        placeholder="Digite o nome do tipo de usuário"
        value={name}
        onChange={(event) => setName(event.target.value)}
      />
      <Button
        onClick={handleAddUserType}
      >
        Adicionar
      </Button>
    </div>
  );
}
