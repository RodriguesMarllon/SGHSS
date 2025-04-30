export default function Navbar() {
  return (
    <div className="bg-slate-600 border border-slate-600 rounded-md px-4 py-2">
      <ul className="flex space-x-4">
        <li className="text-xl text-slate-100 font-bold cursor-pointer">
          UserType
        </li>
        <li className="text-xl text-slate-100 font-bold cursor-pointer">
          User
        </li>
      </ul>
    </div>
  );
}
