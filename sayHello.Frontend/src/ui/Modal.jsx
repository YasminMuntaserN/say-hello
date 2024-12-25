import { cloneElement, createContext, useContext, useState } from "react";
import { createPortal } from "react-dom";

const ModalContext = createContext();

function Modal({ children }) {
  const [openName, setOpenName] = useState("");

  const close = () => setOpenName("");
  const open = setOpenName;

  return (
    <ModalContext.Provider value={{ openName, close, open }}>
      {children}
    </ModalContext.Provider>
  );
}

function Open({ children, opens: opensWindowName }) {
  const { open } = useContext(ModalContext);

  return cloneElement(children, { 
    onClick: () => open(opensWindowName)
  });
}

function Window({ children, name }) {
  const { openName, close } = useContext(ModalContext);
  
  if (name !== openName) return null;

  return createPortal(
    <div className="fixed top-0 left-0 w-full h-screen bg-[rgba(0,0,0,0.5)] backdrop-blur-md z-[1000] transition-all duration-500">
      <div className="fixed top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-white rounded-lg shadow-lg z-[1001]">
        {cloneElement(children, { onClose: close })}
      </div>
    </div>,
    document.body
  );
}

Modal.Open = Open;
Modal.Window = Window;

export default Modal;