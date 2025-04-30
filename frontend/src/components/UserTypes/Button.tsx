import type { MouseEventHandler } from "react";

interface Props {
  children: string;
  onClick: MouseEventHandler;
  // className: string;
}

export default function Button(props: Props) {
  return (
    <button
      className="bg-slate-500 text-white px-4 py-2 rounded-md font-medium cursor-pointer"
      {...props}
    >
      {props.children}
    </button>
  );
}
