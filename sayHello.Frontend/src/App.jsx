import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import PageNotFound from "./pages/PageNotFound";
import Dashboard from "./pages/Dashboard";
import Signup from "./pages/Signup";
import VerifyEmail from "./pages/VerifyEmail";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { Toaster } from "react-hot-toast";

function App() {
  return (
    <>
      <ReactQueryDevtools initialIsOpen={true} />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/dashboard/:username" element={<Dashboard />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/verify-email" element={<VerifyEmail />} />
          <Route path="*" element={<PageNotFound />} />
        </Routes>
      </BrowserRouter>
      <Toaster
        position="top-right"
        gutter={12}
        containerStyle={{ margin: "8px" }}
        toastOptions={{
          icon: false,
          success: { duration: 3000 },
          error: { duration: 5000 },
          style: {
            zIndex: 1000,
            fontSize: "24px",
            fontWeight: "bold",
            width: "500px",
            padding: "24px 24px",
            background: "linear-gradient(to right, #cdb7de ,#d4aeef ,#81688b,#6c5c7e)",
          },
        }}
      />
    </>
  );
}

export default App;
