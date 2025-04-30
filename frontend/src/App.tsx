import Navbar from "./components/Navbar";
import Types from "./components/UserTypes/Types";

export default function App() {
  return (
    <div className="w-screen h-screen bg-[#282A36] flex justify-center p-6">
      <div className="w-[500px] space-y-4">
        <Navbar />
        <h1 className="text-3xl text-slate-100 front-bold text-center">
          {" "}
          Tipos de Usuários
        </h1>
        <Types/>
      </div>
    </div>
  );
}
