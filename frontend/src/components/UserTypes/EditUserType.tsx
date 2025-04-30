import { ChevronLeftIcon } from "lucide-react";
import { useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import useUserTypesStore from "../../store/UserTypesStore";
import Button from "./Button";
import Input from "./Input";
import { UpdateUserType } from "../../services/userTypeService";

export default function EditUserType() {
  const {editUserType} = useUserTypesStore();

  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const paramId = searchParams.get("id");
  const paramName = searchParams.get("name");
  const [name, setName] = useState("");

  async function handleEditUserType(){
    if (paramId != null) {
      const response = await UpdateUserType(paramId, name);
      if (response.statusCode == 200) {
        editUserType(paramId, {id: paramId, name});
        navigate(-1);
      }
    }
  }

  return (
    <div className="w-[500px] space-y-4">
      <div className="flex justify-center relative mb-6">
        <button
          onClick={() => {
            navigate(-1);
          }}
          className="absolute left-0 top-0 bottom-0 text-slate-100"
        >
          <ChevronLeftIcon />
        </button>

        <h1 className="text-3xl text-slate-100 front-bold text-center">
          {" "}
          Editar {paramName}
        </h1>
      </div>

      <div className="space-y-4 p-6 bg-slate-200 rounded-md shadow flex flex-col">
        <Input
          type="text"
          placeholder="Digite o novo nome"
          value={name}
          onChange={(event) => {
            setName(event.target.value);
          }}
        ></Input>
        <Button
          onClick={handleEditUserType}
        >
          Atualizar
        </Button>
      </div>
    </div>
  );
}
