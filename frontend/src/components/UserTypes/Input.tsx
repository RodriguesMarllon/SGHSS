import type React from "react";

interface Props {
  type: string;
  placeholder: string;
  value: string;
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function Input(props: Props) {
  return (
    <input
      className="bg-white border border-slate-300 outline-slate-400 px-4 py-2 rounded-md w-full"
      {...props}
    />
  );
}
